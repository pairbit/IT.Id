using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace IT;

[StructLayout(LayoutKind.Explicit, Size = 12)]
[DebuggerDisplay("{ToString(),nq}")]
public readonly partial struct Id : IComparable<Id>, IEquatable<Id>
#if NET7_0_OR_GREATER
, System.Numerics.IMinMaxValue<Id>
#endif
{
    #region Fields

    //DateTime.UnixEpoch
    private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly Int64 _unixEpochTicks = _unixEpoch.Ticks;

    private static readonly Int16 _pid = GetPid();
    private static readonly Int32 _pid24 = GetPid() << 24;
    private static readonly Int32 _machinePid = (GetMachineXXHash() << 8) | ((_pid >> 8) & 0xff);
    private static readonly Int32 _machinePidReverse = BinaryPrimitives.ReverseEndianness((GetMachineXXHash() << 8) | ((_pid >> 8) & 0xff));
    private static readonly Int64 _random = CalculateRandomValue();
    private static readonly Int32 _random24 = (int)(_random << 24);
    private static readonly Int32 _random8Reverse = BinaryPrimitives.ReverseEndianness((int)(_random >> 8));
    internal static Int32 _staticIncrement = new Random().Next();
    private static IDictionary<Int32, String>? _machines;

    /// <summary>
    /// First 3 bytes of machine name hash
    /// </summary>
    public static readonly Int32 MachineHash24 = GetMachineXXHash();
    public static readonly Id Empty = default;
    public static readonly Id Min = new(0, 0, 0);
    public static readonly Id Max = new(-1, -1, -1);

    [FieldOffset(0)] private readonly byte _timestamp0;
    [FieldOffset(1)] private readonly byte _timestamp1;
    [FieldOffset(2)] private readonly byte _timestamp2;
    [FieldOffset(3)] private readonly byte _timestamp3;

    [FieldOffset(4)] private readonly byte _machine0;
    [FieldOffset(5)] private readonly byte _machine1;
    [FieldOffset(6)] private readonly byte _machine2;

    [FieldOffset(7)] private readonly byte _pid0;
    [FieldOffset(8)] private readonly byte _pid1;

    [FieldOffset(9)] private readonly byte _increment0;
    [FieldOffset(10)] private readonly byte _increment1;
    [FieldOffset(11)] private readonly byte _increment2;

    #endregion Fields

    #region Ctors

    public Id(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 12) ThrowArgumentException();

        this = Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(bytes));

        //https://github.com/dotnet/runtime/pull/78446
        [StackTraceHidden]
        static void ThrowArgumentException() => throw new ArgumentException("The byte array must be 12 bytes long.", nameof(bytes));
    }

    public Id(DateTime timestamp, Int32 machine, Int16 pid, Int32 increment)
        : this(GetTimestampFromDateTime(timestamp), machine, pid, increment)
    {
    }

    public Id(Int32 timestamp, Int32 machine, Int16 pid, Int32 increment)
    {
        if ((machine & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(machine), "The machine value must be between 0 and 16777215 (it must fit in 3 bytes).");

        if ((increment & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");

        Unsafe.WriteUnaligned(ref _timestamp0, BinaryPrimitives.ReverseEndianness(timestamp));

        //_timestamp0 = (byte)(timestamp >> 24);
        //_timestamp1 = (byte)(timestamp >> 16);
        //_timestamp2 = (byte)(timestamp >> 8);
        //_timestamp3 = (byte)timestamp;

        _machine0 = (byte)(machine >> 16);
        _machine1 = (byte)(machine >> 8);
        _machine2 = (byte)machine;

        _pid0 = (byte)(pid >> 8);
        _pid1 = (byte)pid;

        _increment0 = (byte)(increment >> 16);
        _increment1 = (byte)(increment >> 8);
        _increment2 = (byte)increment;

        //Unsafe.WriteUnaligned(ref _machine0, BinaryPrimitives.ReverseEndianness((machine << 8) | ((pid >> 8) & 0xff)));
        //Unsafe.WriteUnaligned(ref _pid1, BinaryPrimitives.ReverseEndianness((pid << 24) | increment));
    }

    public Id(Int32 timestamp, Int32 machinePid, Int32 pidIncrement)
    {
        //_timestamp0 = (byte)(timestamp >> 24);
        //_timestamp1 = (byte)(timestamp >> 16);
        //_timestamp2 = (byte)(timestamp >> 8);
        //_timestamp3 = (byte)timestamp;

        //_machine0 = (byte)(machinePid >> 24);
        //_machine1 = (byte)(machinePid >> 16);
        //_machine2 = (byte)(machinePid >> 8);

        //_pid0 = (byte)machinePid;
        //_pid1 = (byte)(pidIncrement >> 24);

        //_increment0 = (byte)(pidIncrement >> 16);
        //_increment1 = (byte)(pidIncrement >> 8);
        //_increment2 = (byte)pidIncrement;

        Unsafe.WriteUnaligned(ref _timestamp0, BinaryPrimitives.ReverseEndianness(timestamp));
        Unsafe.WriteUnaligned(ref _machine0, BinaryPrimitives.ReverseEndianness(machinePid));
        Unsafe.WriteUnaligned(ref _pid1, BinaryPrimitives.ReverseEndianness(pidIncrement));
    }

    #endregion Ctors

    #region Props

#if NET7_0_OR_GREATER

    static Id System.Numerics.IMinMaxValue<Id>.MaxValue => Max;

    static Id System.Numerics.IMinMaxValue<Id>.MinValue => Min;

#endif

    public Int32 Timestamp => BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<Int32>(ref Unsafe.AsRef(in _timestamp0)));

    //internal Int32 Timestamp3 => _timestamp0 << 24 | _timestamp1 << 16 | _timestamp2 << 8 | _timestamp3;

    public Int32 Machine => (BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<Int32>(ref Unsafe.AsRef(in _machine0))) >> 8) & 0xffffff;

    //internal Int32 Machine3 => (_machine0 << 16 | _machine1 << 8 | _machine2) & 0xffffff;

    public String? MachineName => _machines == null ? null : (_machines.TryGetValue(Machine, out var name) ? name : null);

    public Int16 Pid => BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<Int16>(ref Unsafe.AsRef(in _pid0)));

    //internal Int16 Pid3 => (short)(_pid0 << 8 | _pid1);

    public Int32 Increment => (BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<Int32>(ref Unsafe.AsRef(in _increment0))) >> 8) & 0xffffff;

    //internal Int32 Increment3 => (_increment0 << 16 | _increment1 << 8 | _increment2) & 0xffffff;

    public DateTimeOffset Created => _unixEpoch.AddSeconds((uint)Timestamp);

    #endregion Props

    #region Public Methods

    public Byte[] ToByteArray()
    {
        var bytes = new byte[12];

        Unsafe.WriteUnaligned(ref bytes[0], this);

        return bytes;
    }

    public Boolean TryWrite(Span<Byte> bytes)
    {
        if (bytes.Length < 12) return false;

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), this);

        return true;
    }

    public Int32 CompareTo(Id id)
    {
        if (_timestamp0 != id._timestamp0) return _timestamp0 < id._timestamp0 ? -1 : 1;
        if (_timestamp1 != id._timestamp1) return _timestamp1 < id._timestamp1 ? -1 : 1;
        if (_timestamp2 != id._timestamp2) return _timestamp2 < id._timestamp2 ? -1 : 1;
        if (_timestamp3 != id._timestamp3) return _timestamp3 < id._timestamp3 ? -1 : 1;

        if (_machine0 != id._machine0) return _machine0 < id._machine0 ? -1 : 1;
        if (_machine1 != id._machine1) return _machine1 < id._machine1 ? -1 : 1;
        if (_machine2 != id._machine2) return _machine2 < id._machine2 ? -1 : 1;

        if (_pid0 != id._pid0) return _pid0 < id._pid0 ? -1 : 1;
        if (_pid1 != id._pid1) return _pid1 < id._pid1 ? -1 : 1;

        if (_increment0 != id._increment0) return _increment0 < id._increment0 ? -1 : 1;
        if (_increment1 != id._increment1) return _increment1 < id._increment1 ? -1 : 1;
        if (_increment2 != id._increment2) return _increment2 < id._increment2 ? -1 : 1;

        return 0;
    }

    public Boolean Equals(Id id)
    {
        ref int l = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in _timestamp0));
        ref int r = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in id._timestamp0));

        return l == r && Unsafe.Add(ref l, 1) == Unsafe.Add(ref r, 1) && Unsafe.Add(ref l, 2) == Unsafe.Add(ref r, 2);
    }

    //internal Boolean Equals4(Id id)
    //{
    //    ref byte bl = ref Unsafe.AsRef(in _timestamp0);
    //    ref byte br = ref Unsafe.AsRef(in id._timestamp0);

    //    return Unsafe.As<byte, long>(ref bl) == Unsafe.As<byte, long>(ref br) &&
    //           Unsafe.As<byte, int>(ref Unsafe.Add(ref bl, 8)) == Unsafe.As<byte, int>(ref Unsafe.Add(ref br, 8));
    //}

#if !NETSTANDARD2_0
    public byte[] ToUtf8String()
    {
        Span<byte> bytes = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), this);

        var base64 = new byte[16];

        if (System.Buffers.Text.Base64.EncodeToUtf8(bytes, base64, out _, out _) != System.Buffers.OperationStatus.Done)
            throw new InvalidOperationException();

        return base64;
    }
#endif

    public override string ToString()
    {
#if NETSTANDARD2_0
        return Convert.ToBase64String(ToByteArray());
#else
        Span<byte> bytes = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), this);
        return Convert.ToBase64String(bytes);
#endif
    }

    public override bool Equals(object? obj) => obj is Id id && EqualsCore(in this, in id);

    public override int GetHashCode()
    {
        ref int r = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in _timestamp0));
        return r ^ Unsafe.Add(ref r, 1) ^ Unsafe.Add(ref r, 2);
    }

#if NETSTANDARD2_0
    public static Id Parse(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length != 16) throw new ArgumentOutOfRangeException(nameof(value));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(Convert.FromBase64String(value).AsSpan()));
    }
#else
    public static Id Parse(ReadOnlySpan<char> chars)
    {
        if (chars.Length != 16) throw new ArgumentOutOfRangeException(nameof(chars));

        Span<byte> bytes = stackalloc byte[12];

        if (!Convert.TryFromBase64Chars(chars, bytes, out _)) throw new ArgumentOutOfRangeException(nameof(chars));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(bytes));
    }

    public static Id Parse(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16) throw new ArgumentOutOfRangeException(nameof(bytes));

        Span<byte> decoded = stackalloc byte[12];

        if (System.Buffers.Text.Base64.DecodeFromUtf8(bytes, decoded, out _, out _) != System.Buffers.OperationStatus.Done)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(decoded));
    }
#endif

    public static void Machines(params String[] names)
    {
        if (_machines != null) throw new InvalidOperationException("Machines already initialized");
        if (names == null) throw new ArgumentNullException(nameof(names));
        if (names.Length == 0) throw new ArgumentException("names is empty", nameof(names));

        var machines = new Dictionary<Int32, String>();

        foreach (var name in names)
        {
            var hash = XXHash24(name);

            if (machines.ContainsKey(hash)) throw new ArgumentException($"Machine name '{name}' has already been added", nameof(names));

            machines.Add(hash, name);
        }

        _machines = machines;
    }

    #region New

    //https://github.com/dotnet/runtime/blob/e66a6a319cc372f30c3dad9f491ac636c0ce03e4/src/libraries/Common/src/Interop/Unix/System.Native/Interop.GetSystemTimeAsTicks.cs#L12

    //[DllImport("libSystem.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    //internal static extern long GetSystemTimeAsTicks();

    //[LibraryImport("libSystem.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    //internal static partial long GetSystemTimeAsTicks();

    public static Id New()
    {
        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, BinaryPrimitives.ReverseEndianness((int)(uint)(long)Math.Floor((double)(DateTime.UtcNow.Ticks - _unixEpochTicks) / TimeSpan.TicksPerSecond)));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), _machinePidReverse);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), BinaryPrimitives.ReverseEndianness(_pid24 | (Interlocked.Increment(ref _staticIncrement) & 0x00ffffff)));

        return id;
    }

    //internal static Id New_Old()
    //{
    //    // only use low order 3 bytes
    //    int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;

    //    var pidIncrement = (_pid << 24) | increment;

    //    return new Id(GetTimestamp(), _machinePid, pidIncrement);
    //}

    public static Id New(DateTime timestamp) => New(GetTimestampFromDateTime(timestamp));

    public static Id New(Int32 timestamp)
    {
        // only use low order 3 bytes
        int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;

        var pidIncrement = (_pid << 24) | increment;

        return new Id(timestamp, _machinePid, pidIncrement);
    }

    #endregion New

    #region NewObjectId

    /// <summary>
    /// https://www.mongodb.com/docs/manual/reference/method/ObjectId/
    /// </summary>
    public static Id NewObjectId()
    {
        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, BinaryPrimitives.ReverseEndianness((int)(uint)(long)Math.Floor((double)(DateTime.UtcNow.Ticks - _unixEpochTicks) / TimeSpan.TicksPerSecond)));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), _random8Reverse);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), BinaryPrimitives.ReverseEndianness(_random24 | (Interlocked.Increment(ref _staticIncrement) & 0x00ffffff)));

        return id;
    }

    internal static Id NewObjectIdOld()
    {
        // only use low order 3 bytes
        int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;

        var random = _random;

        var b = (int)(random >> 8); // first 4 bytes of random
        var c = (int)(random << 24) | increment; // 5th byte of random and 3 byte increment

        return new Id(GetTimestamp(), b, c);
    }

    public static Id NewObjectId(DateTime timestamp) => NewObjectId(GetTimestampFromDateTime(timestamp));

    public static Id NewObjectId(Int32 timestamp)
    {
        // only use low order 3 bytes
        int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;
        return Create(timestamp, _random, increment);
    }

    #endregion NewObjectId

    #endregion Public Methods

    #region Operators

    public static Boolean operator <(Id left, Id right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id left, Id right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id left, Id right) => EqualsCore(in left, in right);

    public static Boolean operator !=(Id left, Id right) => !EqualsCore(in left, in right);

    public static Boolean operator >=(Id left, Id right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id left, Id right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Private Methods

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsCore(in Id left, in Id right)
    {
        ref int l = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in left._timestamp0));
        ref int r = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in right._timestamp0));

        return l == r && Unsafe.Add(ref l, 1) == Unsafe.Add(ref r, 1) && Unsafe.Add(ref l, 2) == Unsafe.Add(ref r, 2);
    }

    private static long CalculateRandomValue()
    {
        var seed = (int)DateTime.UtcNow.Ticks ^ GetMachineHash() ^ GetPid();
        var random = new Random(seed);
        var high = random.Next();
        var low = random.Next();
        var combined = (long)((ulong)(uint)high << 32 | (ulong)(uint)low);
        return combined & 0xffffffffff; // low order 5 bytes
    }

    private static Id Create(int timestamp, long random, int increment)
    {
        if (random < 0 || random > 0xffffffffff) throw new ArgumentOutOfRangeException(nameof(random), "The random value must be between 0 and 1099511627775 (it must fit in 5 bytes).");

        if (increment < 0 || increment > 0xffffff) throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");

        var b = (int)(random >> 8); // first 4 bytes of random
        var c = (int)(random << 24) | increment; // 5th byte of random and 3 byte increment
        return new Id(timestamp, b, c);
    }

    /// <summary>
    /// Gets the current process id.  This method exists because of how CAS operates on the call stack, checking
    /// for permissions before executing the method.  Hence, if we inlined this call, the calling method would not execute
    /// before throwing an exception requiring the try/catch at an even higher level that we don't necessarily control.
    /// </summary>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static int GetCurrentProcessId()
    {
#if NET6_0_OR_GREATER
        return Environment.ProcessId;
#else
        return Process.GetCurrentProcess().Id;
#endif
    }

    private static int GetMachineHash()
    {
        // use instead of Dns.HostName so it will work offline
        var machineName = Environment.MachineName;
        return 0x00ffffff & machineName.GetHashCode(); // use first 3 bytes of hash
    }

    private static int GetMachineXXHash() => XXHash24(Environment.MachineName);

    private static int XXHash24(String machineName)
    {
        var bytes = Encoding.UTF8.GetBytes(machineName);
        var hash = (int)Internal.XXH32.DigestOf(bytes);
        return 0x00ffffff & hash; // use first 3 bytes of hash
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Int32 GetTimestamp()
    {
        var totalSeconds = (double)(DateTime.UtcNow.Ticks - _unixEpochTicks) / TimeSpan.TicksPerSecond;

        return (int)(uint)(long)Math.Floor(totalSeconds);
    }

    private static short GetPid()
    {
        try
        {
            return (short)GetCurrentProcessId(); // use low order two bytes only
        }
        catch (SecurityException)
        {
            return 0;
        }
    }

    private static int GetTimestampFromDateTime(DateTime timestamp)
    {
        var secondsSinceEpoch = (long)Math.Floor((ToUniversalTime(timestamp) - _unixEpoch).TotalSeconds);

        if (secondsSinceEpoch < uint.MinValue || secondsSinceEpoch > uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(timestamp));

        return (int)(uint)secondsSinceEpoch;
    }

    private static DateTime ToUniversalTime(DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue) return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

        if (dateTime == DateTime.MaxValue) return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

        return dateTime.ToUniversalTime();
    }

    #endregion Private Methods
}