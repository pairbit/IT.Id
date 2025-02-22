﻿using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Buffers.Text;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace IT;

[StructLayout(LayoutKind.Explicit, Size = 12)]
[DebuggerDisplay("{ToString(),nq}")]
public readonly partial struct Id : IComparable<Id>, IEquatable<Id>, IFormattable
#if NET6_0_OR_GREATER
, ISpanFormattable
#endif
#if NET7_0_OR_GREATER
, System.Numerics.IMinMaxValue<Id>
, ISpanParsable<Id>
#endif
#if NET8_0_OR_GREATER
, IUtf8SpanFormattable, IUtf8SpanParsable<Id>
#endif
{
    static class Message
    {
        public static string InvalidLength(long invalid, int valid) => $"{nameof(Id)} length cannot be {invalid}. {nameof(Id)} length must be {valid}";
    }

    #region Fields

    //DateTime.UnixEpoch
    private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly long _unixEpochTicks = _unixEpoch.Ticks;

    private static readonly uint _pid24;
    private static readonly int _machinePidReverse;
    private static readonly uint _random24;
    private static readonly uint _random8Reverse;
    internal static int _staticIncrement;

    /// <summary>
    /// First 3 bytes of machine name hash
    /// </summary>
    public static readonly uint CurrentMachine;
    public static readonly ushort CurrentPid;
    public static readonly ulong CurrentRandom;
    public static readonly Id Empty = default;
    public static readonly Id Min = new(0, 0, 0);
    public static readonly Id Max = new(uint.MaxValue, uint.MaxValue, uint.MaxValue);

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

    static Id()
    {
        uint machine = XXH24(Environment.MachineName);
        ushort pid = GetPid();
        int machinePid = ((int)machine << 8) | ((pid >> 8) & 0xff);

        _machinePidReverse = BinaryPrimitives.ReverseEndianness(machinePid);
        _pid24 = (uint)(pid << 24);
        CurrentMachine = machine;
        CurrentPid = pid;

        var seed = GetSeed();
        var random = CalculateRandomValue(seed);
        _random8Reverse = BinaryPrimitives.ReverseEndianness((uint)(random >> 8));
        _random24 = (uint)(random << 24);
        CurrentRandom = random;

        _staticIncrement = new Random().Next();
    }

    #region Ctors

    public Id(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 12) ThrowArgumentException();

        this = Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(bytes));

        //https://github.com/dotnet/runtime/pull/78446
        [StackTraceHidden]
        static void ThrowArgumentException() => throw new ArgumentException("The byte array must be 12 bytes long.", nameof(bytes));
    }

    public Id(DateTime timestamp, uint machine, ushort pid, uint increment)
        : this(GetTimestampFromDateTime(timestamp), machine, pid, increment)
    {
    }

    public Id(uint timestamp, uint machine, ushort pid, uint increment)
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

    public Id(uint timestamp, ulong random, uint increment)
    {
        if (random > 0xffffffffff) throw new ArgumentOutOfRangeException(nameof(random), "The random value must be between 0 and 1099511627775 (it must fit in 5 bytes).");

        if (increment > 0xffffff) throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");

        Unsafe.WriteUnaligned(ref _timestamp0, BinaryPrimitives.ReverseEndianness(timestamp));
        Unsafe.WriteUnaligned(ref _machine0, BinaryPrimitives.ReverseEndianness((uint)(random >> 8)));
        Unsafe.WriteUnaligned(ref _pid1, BinaryPrimitives.ReverseEndianness((uint)(random << 24) | increment));
    }

    public Id(uint timestamp, uint machinePid, uint pidIncrement)
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

    public uint Timestamp => BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _timestamp0)));

    //internal Int32 Timestamp3 => _timestamp0 << 24 | _timestamp1 << 16 | _timestamp2 << 8 | _timestamp3;

    public ulong Random => ((ulong)BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _machine0))) << 8) | _pid1;

    public bool IsCurrentRandom => Random == CurrentRandom;

    //internal Int32 Machine3 => (_machine0 << 16 | _machine1 << 8 | _machine2) & 0xffffff;

    public uint Machine => (BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _machine0))) >> 8) & 0xffffff;

    public bool IsCurrentMachine => Machine == CurrentMachine;

    public ushort Pid => BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<ushort>(ref Unsafe.AsRef(in _pid0)));

    public bool IsCurrentPid => Pid == CurrentPid;

    //internal Int16 Pid3 => (short)(_pid0 << 8 | _pid1);

    public uint Increment => (BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _increment0))) >> 8) & 0xffffff;

    //internal Int32 Increment3 => (_increment0 << 16 | _increment1 << 8 | _increment2) & 0xffffff;

    public DateTimeOffset Created => _unixEpoch.AddSeconds(Timestamp);

    #endregion Props

    public byte[] ToByteArray()
    {
        var bytes = new byte[12];

        Unsafe.WriteUnaligned(ref bytes[0], this);

        return bytes;
    }

    public Boolean TryWrite(Span<byte> bytes)
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

    public override bool Equals(object? obj) => obj is Id id && EqualsCore(in this, in id);

    public override int GetHashCode()
    {
        ref int r = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in _timestamp0));
        return r ^ Unsafe.Add(ref r, 1) ^ Unsafe.Add(ref r, 2);
    }

    #region ToString

    public override string ToString()
    {
#if NETSTANDARD2_0
        return Convert.ToBase64String(ToByteArray());
#else
        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);
        return Convert.ToBase64String(raw);
#endif
    }

#if !NETSTANDARD2_0
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ToString(Span<char> chars)
    {
        if (chars.Length < 16) throw new ArgumentOutOfRangeException(nameof(chars), chars.Length, Message.InvalidLength(chars.Length, 16));

        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);

        Convert.TryToBase64Chars(raw, chars, out _);
    }

    public bool TryFormat(Span<char> chars, out int written)
    {
        if (chars.Length < 16)
        {
            written = 0;
            return false;
        }

        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);

        Convert.TryToBase64Chars(raw, chars, out _);

        written = 16;
        return true;
    }
#endif

    public byte[] ToUtf8String()
    {
        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);

        var utf8 = new byte[16];

        if (Base64.EncodeToUtf8(raw, utf8, out _, out _) != OperationStatus.Done)
            throw new InvalidOperationException();

        return utf8;
    }

    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ToUtf8String(Span<byte> bytes)
    {
        if (bytes.Length < 16) throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length, Message.InvalidLength(bytes.Length, 16));

        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);

        if (Base64.EncodeToUtf8(raw, bytes, out _, out _) != OperationStatus.Done)
            throw new InvalidOperationException();
    }

    public bool TryFormat(Span<byte> bytes, out int written)
    {
        if (bytes.Length < 16)
        {
            written = 0;
            return false;
        }

        Span<byte> raw = stackalloc byte[12];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(raw), this);

        if (Base64.EncodeToUtf8(raw, bytes, out _, out _) != OperationStatus.Done)
            throw new InvalidOperationException();

        written = 16;
        return true;
    }

    #endregion ToString

    #region Formattable

#if NET8_0_OR_GREATER
    bool IUtf8SpanFormattable.TryFormat(Span<byte> utf8Destination, out int bytesWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (format.Length != 0) throw new FormatException();

        return TryFormat(utf8Destination, out bytesWritten);
    }
#endif

#if NET6_0_OR_GREATER
    bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    {
        if (format.Length != 0) throw new FormatException();

        return TryFormat(destination, out charsWritten);
    }
#endif

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider)
    {
        if (format != null) throw new FormatException();

        return ToString();
    }

    #endregion Formattable

    #region Parse

#if NETSTANDARD2_0
    public static Id Parse(string value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));
        if (value.Length != 16) throw new ArgumentOutOfRangeException(nameof(value));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(Convert.FromBase64String(value).AsSpan()));
    }
#else
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Id Parse(ReadOnlySpan<char> chars)
    {
        if (chars.Length != 16) throw new ArgumentOutOfRangeException(nameof(chars));

        Span<byte> raw = stackalloc byte[12];

        if (!Convert.TryFromBase64Chars(chars, raw, out _)) throw new ArgumentOutOfRangeException(nameof(chars));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(raw));
    }

    public static bool TryParse(ReadOnlySpan<char> chars, out Id id)
    {
        if (chars.Length != 16) goto fail;

        Span<byte> raw = stackalloc byte[12];

        if (!Convert.TryFromBase64Chars(chars, raw, out _)) goto fail;

        id = Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(raw));
        return true;

    fail:
        id = default;
        return false;
    }

#endif

    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Id Parse(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16) throw new ArgumentOutOfRangeException(nameof(bytes));

        Span<byte> raw = stackalloc byte[12];

        if (Base64.DecodeFromUtf8(bytes, raw, out _, out _) != OperationStatus.Done)
            throw new ArgumentOutOfRangeException(nameof(bytes));

        return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(raw));
    }

    public static bool TryParse(ReadOnlySpan<byte> bytes, out Id id)
    {
        if (bytes.Length != 16) goto fail;

        Span<byte> raw = stackalloc byte[12];

        if (Base64.DecodeFromUtf8(bytes, raw, out _, out _) != OperationStatus.Done)
            goto fail;

        id = Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(raw));
        return true;

    fail:
        id = default;
        return false;
    }

    #endregion Parse

    #region Parsable

#if NET8_0_OR_GREATER

    static Id IUtf8SpanParsable<Id>.Parse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider)
        => Parse(utf8Text);

    static bool IUtf8SpanParsable<Id>.TryParse(ReadOnlySpan<byte> utf8Text, IFormatProvider? provider, out Id result)
        => TryParse(utf8Text, out result);
#endif

#if NET7_0_OR_GREATER
    static Id ISpanParsable<Id>.Parse(ReadOnlySpan<char> s, IFormatProvider? provider)
        => Parse(s);

    static bool ISpanParsable<Id>.TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Id result)
        => TryParse(s, out result);

    static Id IParsable<Id>.Parse(string s, IFormatProvider? provider)
        => Parse(s);

    static bool IParsable<Id>.TryParse(string? s, IFormatProvider? provider, out Id result)
        => TryParse(s, out result);
#endif

    #endregion Parsable

    #region Operators

    public static Boolean operator <(Id left, Id right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id left, Id right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id left, Id right) => EqualsCore(in left, in right);

    public static Boolean operator !=(Id left, Id right) => !EqualsCore(in left, in right);

    public static Boolean operator >=(Id left, Id right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id left, Id right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region New

    //https://github.com/dotnet/runtime/blob/e66a6a319cc372f30c3dad9f491ac636c0ce03e4/src/libraries/Common/src/Interop/Unix/System.Native/Interop.GetSystemTimeAsTicks.cs#L12

    //[DllImport("libSystem.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    //internal static extern long GetSystemTimeAsTicks();

    //[LibraryImport("libSystem.Native", EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    //internal static partial long GetSystemTimeAsTicks();

    public static Id New(uint timestamp)
    {
        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, BinaryPrimitives.ReverseEndianness(timestamp));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), _machinePidReverse);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), BinaryPrimitives.ReverseEndianness(_pid24 | UInc()));

        return id;
    }

    public static Id New() => New(GetTimestamp());

    public static Id New(DateTime timestamp) => New(GetTimestampFromDateTime(timestamp));

    /// <summary>
    /// https://www.mongodb.com/docs/manual/reference/method/ObjectId/
    /// </summary>
    public static Id NewObjectId(uint timestamp)
    {
        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, BinaryPrimitives.ReverseEndianness(timestamp));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), _random8Reverse);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), BinaryPrimitives.ReverseEndianness(_random24 | UInc()));

        return id;
    }

    public static Id NewObjectId() => NewObjectId(GetTimestamp());

    public static Id NewObjectId(DateTime timestamp) => NewObjectId(GetTimestampFromDateTime(timestamp));

    public static Id Next(Id id)
    {
        ref var b = ref Unsafe.As<Id, byte>(ref id);

        var inc = Inc();

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)(inc >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)(inc >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)inc);

        return id;
    }

    #endregion New

    #region Private Methods

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsCore(in Id left, in Id right)
    {
        ref int l = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in left._timestamp0));
        ref int r = ref Unsafe.As<byte, int>(ref Unsafe.AsRef(in right._timestamp0));

        return l == r && Unsafe.Add(ref l, 1) == Unsafe.Add(ref r, 1) && Unsafe.Add(ref l, 2) == Unsafe.Add(ref r, 2);
    }

    private static ulong CalculateRandomValue(int seed)
    {
        var random = new Random(seed);
        var high = random.Next();
        var low = random.Next();
        var combined = (ulong)(uint)high << 32 | (uint)low;
        return combined & 0xffffffffff; // low order 5 bytes
    }

    private static int GetSeed() => (int)DateTime.UtcNow.Ticks ^ GetMachineHash() ^ GetPid();

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

    private static uint XXH24(string machineName)
    {
        var bytes = Encoding.UTF8.GetBytes(machineName);
        var hash = Internal.XXH32.DigestOf(bytes);
        return 0x00ffffff & hash; // use first 3 bytes of hash
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint GetTimestamp()
    {
        return (uint)(long)Math.Floor((double)(DateTime.UtcNow.Ticks - _unixEpochTicks) / TimeSpan.TicksPerSecond);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint UInc() => (uint)Inc();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int Inc()
    {
        // only use low order 3 bytes
        return Interlocked.Add(ref _staticIncrement, 1) & 0x00ffffff;
    }

    private static ushort GetPid()
    {
        try
        {
            return (ushort)GetCurrentProcessId(); // use low order two bytes only
        }
        catch (SecurityException)
        {
            return 0;
        }
    }

    private static uint GetTimestampFromDateTime(DateTime timestamp)
    {
        var secondsSinceEpoch = (long)Math.Floor((ToUniversalTime(timestamp) - _unixEpoch).TotalSeconds);

        if (secondsSinceEpoch < uint.MinValue || secondsSinceEpoch > uint.MaxValue) throw new ArgumentOutOfRangeException(nameof(timestamp));

        return (uint)secondsSinceEpoch;
    }

    private static DateTime ToUniversalTime(DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue) return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);

        if (dateTime == DateTime.MaxValue) return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);

        return dateTime.ToUniversalTime();
    }

    #endregion Private Methods
}