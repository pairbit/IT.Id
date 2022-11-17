﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace System;

[Serializable]
[StructLayout(LayoutKind.Explicit, Size = 12)]
[DebuggerDisplay("{ToString(),nq}")]
[Text.Json.Serialization.JsonConverter(typeof(Text.Json.Serialization.IdJsonConverter))]
public readonly partial struct Id : IComparable<Id>, IEquatable<Id>, IFormattable
#if NET6_0_OR_GREATER
, ISpanFormattable
#endif
{
    //DateTime.UnixEpoch
    private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly Int64 _unixEpochTicks = _unixEpoch.Ticks;

    private static readonly Int16 _pid = GetPid();
    private static readonly Int32 _machinePid = (GetMachineXXHash() << 8) | ((_pid >> 8) & 0xff);
    private static readonly Int64 _random = CalculateRandomValue();
    private static Int32 _staticIncrement = new Random().Next();
    public static readonly Id Empty = default;
    public static readonly Id Min = new(0, 0, 0);
    public static readonly Id Max = new(-1, -1, -1);

    [FieldOffset(0)]
    internal readonly Int32 _timestamp;

    [FieldOffset(4)]
    internal readonly Int32 _b;

    [FieldOffset(8)]
    internal readonly Int32 _c;

    #region Ctors

    public Id(ReadOnlySpan<Byte> bytes)
    {
        if (bytes == null) throw new ArgumentNullException(nameof(bytes));

        if (bytes.Length != 12) throw new ArgumentException("Byte array must be 12 bytes long", nameof(bytes));

        FromByteArray(bytes, 0, out _timestamp, out _b, out _c);
    }

    public Id(DateTime timestamp, Int32 machine, Int16 pid, Int32 increment)
        : this(GetTimestampFromDateTime(timestamp), machine, pid, increment)
    {
    }

    public Id(Int32 timestamp, Int32 machine, Int16 pid, Int32 increment)
    {
        if ((machine & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(machine), "The machine value must be between 0 and 16777215 (it must fit in 3 bytes).");

        if ((increment & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");

        _timestamp = timestamp;
        _b = (machine << 8) | ((pid >> 8) & 0xff);
        _c = (pid << 24) | increment;
    }

    public Id(Int32 timestamp, Int32 b, Int32 c)
    {
        _timestamp = timestamp;
        _b = b;
        _c = c;
    }

    #endregion Ctors

    #region Props

    public Int32 Timestamp => _timestamp;

    public Int32 B => _b;

    public Int32 C => _c;

    public Int32 Machine => (_b >> 8) & 0xffffff;

    public Int16 Pid => (short)(((_b << 8) & 0xff00) | ((_c >> 24) & 0x00ff));

    public Int32 Increment => _c & 0xffffff;

    public DateTimeOffset Created => _unixEpoch.AddSeconds((uint)_timestamp);

    #endregion Props

    #region Operators

    public static Boolean operator <(Id left, Id right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id left, Id right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id left, Id right) => left.Equals(right);

    public static Boolean operator !=(Id left, Id right) => !(left == right);

    public static Boolean operator >=(Id left, Id right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id left, Id right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Public Methods

    //[DllImport(Interop.Libraries.SystemNative, EntryPoint = "SystemNative_GetSystemTimeAsTicks")]
    //internal static extern long GetSystemTimeAsTicks();
    //https://github.com/dotnet/runtime/blob/4aeec6397c7dd6198129219ed806f93d6ed675c0/src/libraries/Common/src/Interop/Unix/System.Native/Interop.GetSystemTimeAsTicks.cs

    #region New

    public static Id New()
    {
        // only use low order 3 bytes
        int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;

        var c = (_pid << 24) | increment;

        return new Id(GetTimestamp(), _machinePid, c);
    }

    public static Id New(DateTime timestamp) => New(GetTimestampFromDateTime(timestamp));

    public static Id New(Int32 timestamp)
    {
        // only use low order 3 bytes
        int increment = Interlocked.Increment(ref _staticIncrement) & 0x00ffffff;

        var c = (_pid << 24) | increment;

        return new Id(timestamp, _machinePid, c);
    }

    #endregion New

    #region NewObjectId

    /// <summary>
    /// https://www.mongodb.com/docs/manual/reference/method/ObjectId/
    /// </summary>
    public static Id NewObjectId()
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

    //public static Byte[] Pack(Int32 timestamp, Int32 machine, Int16 pid, Int32 increment)
    //{
    //    if ((machine & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(machine), "The machine value must be between 0 and 16777215 (it must fit in 3 bytes).");

    //    if ((increment & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(increment), "The increment value must be between 0 and 16777215 (it must fit in 3 bytes).");

    //    byte[] bytes = new byte[12];
    //    bytes[0] = (byte)(timestamp >> 24);
    //    bytes[1] = (byte)(timestamp >> 16);
    //    bytes[2] = (byte)(timestamp >> 8);
    //    bytes[3] = (byte)(timestamp);
    //    bytes[4] = (byte)(machine >> 16);
    //    bytes[5] = (byte)(machine >> 8);
    //    bytes[6] = (byte)(machine);
    //    bytes[7] = (byte)(pid >> 8);
    //    bytes[8] = (byte)(pid);
    //    bytes[9] = (byte)(increment >> 16);
    //    bytes[10] = (byte)(increment >> 8);
    //    bytes[11] = (byte)(increment);
    //    return bytes;
    //}

    public static Int32 GetLength(Idf format) => format switch
    {
        Idf.Base85 => 15,
        Idf.Base64 or Idf.Base64Url => 16,
        Idf.Base58 => 17,
        Idf.Path2 => 18,
        Idf.Path3 => 19,
        Idf.Base32 => 20,
        Idf.Hex or Idf.HexUpper => 24,
        _ => throw new NotImplementedException($"id format '{format}' not supported")
    };

    public static Idf? TryGetFormat(Int32 length) => length switch
    {
        15 => Idf.Base85,
        16 => Idf.Base64,
        17 => Idf.Base58,
        18 => Idf.Path2,
        19 => Idf.Path3,
        20 => Idf.Base32,
        24 => Idf.Hex,
        _ => null
    };

    /// <exception cref="ArgumentException"/>
    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> value) => value.Length switch
    {
        15 => ParseBase85(value),
        16 => ParseBase64(value),
        17 => ParseBase58(value),
        18 => ParsePath2(value),
        19 => ParsePath3(value),
        20 => ParseBase32(value),
        24 => ParseHex(value),
        _ => throw new FormatException("The id must be between 15 and 20 or 24")
    };

    /// <exception cref="ArgumentException"/>
    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Byte> value) => value.Length switch
    {
        15 => ParseBase85(value),
        16 => ParseBase64(value),
        //17 => ParseBase58(value),
        //18 => ParsePath2(value),
        //19 => ParsePath3(value),
        //20 => ParseBase32(value),
        24 => ParseHex(value),
        _ => throw new FormatException("The id must be between 15 and 20 or 24")
    };

    /// <exception cref="ArgumentException"/>
    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> value, Idf format) => format switch
    {
        Idf.Hex or Idf.HexUpper => ParseHex(value),
        Idf.Base32 => ParseBase32(value),
        Idf.Base58 => ParseBase58(value),
        Idf.Base64 or Idf.Base64Url => ParseBase64(value),
        Idf.Base85 => ParseBase85(value),
        Idf.Path2 => ParsePath2(value),
        Idf.Path3 => ParsePath3(value),
        _ => throw new FormatException()
    };

    //public static void Unpack(Byte[] bytes, out Int32 timestamp, out Int32 machine, out Int16 pid, out Int32 increment)
    //{
    //    if (bytes == null) throw new ArgumentNullException(nameof(bytes));
    //    if (bytes.Length != 12) throw new ArgumentOutOfRangeException(nameof(bytes), "Byte array must be 12 bytes long.");

    //    timestamp = (bytes[0] << 24) + (bytes[1] << 16) + (bytes[2] << 8) + bytes[3];
    //    machine = (bytes[4] << 16) + (bytes[5] << 8) + bytes[6];
    //    pid = (short)((bytes[7] << 8) + bytes[8]);
    //    increment = (bytes[9] << 16) + (bytes[10] << 8) + bytes[11];
    //}

    public Byte[] ToByteArray() => new[] {
        (byte)(_timestamp >> 24),
        (byte)(_timestamp >> 16),
        (byte)(_timestamp >> 8),
        (byte)(_timestamp),
        (byte)(_b >> 24),
        (byte)(_b >> 16),
        (byte)(_b >> 8),
        (byte)(_b),
        (byte)(_c >> 24),
        (byte)(_c >> 16),
        (byte)(_c >> 8),
        (byte)(_c)
    };

    public void ToByteArray(Span<Byte> destination, Int32 offset)
    {
        if (offset + 12 > destination.Length) throw new ArgumentException("Not enough room in destination buffer.", nameof(offset));

        destination[offset + 0] = (byte)(_timestamp >> 24);
        destination[offset + 1] = (byte)(_timestamp >> 16);
        destination[offset + 2] = (byte)(_timestamp >> 8);
        destination[offset + 3] = (byte)(_timestamp);
        destination[offset + 4] = (byte)(_b >> 24);
        destination[offset + 5] = (byte)(_b >> 16);
        destination[offset + 6] = (byte)(_b >> 8);
        destination[offset + 7] = (byte)(_b);
        destination[offset + 8] = (byte)(_c >> 24);
        destination[offset + 9] = (byte)(_c >> 16);
        destination[offset + 10] = (byte)(_c >> 8);
        destination[offset + 11] = (byte)(_c);
    }

    public Int32 CompareTo(Id other)
    {
        int result = ((uint)_timestamp).CompareTo((uint)other._timestamp);

        if (result != 0) return result;

        result = ((uint)_b).CompareTo((uint)other._b);
        if (result != 0) return result;

        return ((uint)_c).CompareTo((uint)other._c);
    }

    public Boolean Equals(Id id) => _timestamp == id._timestamp && _b == id._b && _c == id._c;

    public override String ToString() => ToBase64Url();

    public String ToString(String? format, IFormatProvider? formatProvider = null) => format switch
    {
        "h" or "b16" or "16" => ToHexLower(),
        "H" or "B16" => ToHexUpper(),
        "32" => ToBase32(),
        "58" => ToBase58(),
        "b64" or "64" => ToBase64(),
        "u64" or null => ToBase64Url(),
        "85" => ToBase85(),
        "p2" => ToPath2(),
        "p3" => ToPath3(),
        _ => throw new FormatException($"The '{format}' format string is not supported."),
    };

    public String ToString(Idf format) => format switch
    {
        Idf.Hex => ToHexLower(),
        Idf.HexUpper => ToHexUpper(),
        Idf.Base32 => ToBase32(),
        Idf.Base58 => ToBase58(),
        Idf.Base64 => ToBase64(),
        Idf.Base64Url => ToBase64Url(),
        Idf.Base85 => ToBase85(),
        Idf.Path2 => ToPath2(),
        Idf.Path3 => ToPath3(),
        _ => throw new FormatException($"The '{format}' format string is not supported."),
    };

    public Boolean TryFormat(Span<Byte> destination, out Int32 charsWritten, Idf format)
    {
        if (format == Idf.Hex && destination.Length >= 24)
        {
            unsafe
            {
                ToHex(destination, Hex._lowerLookup16UnsafeP);
            }
            charsWritten = 24;
            return true;
        }

        if (format == Idf.HexUpper && destination.Length >= 24)
        {
            unsafe
            {
                ToHex(destination, Hex._upperLookup16UnsafeP);
            }
            charsWritten = 24;
            return true;
        }

        if (format == Idf.Base64 && destination.Length >= 16)
        {
            ToBase64(destination, Base64.bytes);
            charsWritten = 16;
            return true;
        }

        if (format == Idf.Base64Url && destination.Length >= 16)
        {
            ToBase64(destination, Base64.bytesUrl);
            charsWritten = 16;
            return true;
        }

        if (format == Idf.Base85 && destination.Length >= 15)
        {
            Base85.Encode(ToByteArray(), destination);
            charsWritten = 15;
            return true;
        }

        charsWritten = 0;
        return false;
    }

    public Boolean TryFormat(Span<Char> destination, out Int32 charsWritten, ReadOnlySpan<Char> format, IFormatProvider? provider)
    {
        //check destination.Length

        if (format.IsEmpty || format.SequenceEqual("u64"))
        {
            charsWritten = 16;
            ToBase64(destination, Base64.tableUrl);
            return true;
        }

        if (format.SequenceEqual("b64") || format.SequenceEqual("64"))
        {
            charsWritten = 16;
            ToBase64(destination, Base64.table);
            return true;
        }

        if (format[0] == 'h' || format.SequenceEqual("b16") || format.SequenceEqual("16"))
        {
            charsWritten = 24;
            unsafe
            {
                ToHex(destination, Hex._lowerLookup32UnsafeP);
            }

            return true;
        }

        if (format[0] == 'H' || format.SequenceEqual("B16"))
        {
            charsWritten = 24;
            unsafe
            {
                ToHex(destination, Hex._upperLookup32UnsafeP);
            }

            return true;
        }

        if (format.SequenceEqual("32"))
        {
            charsWritten = 20;
            ToBase32(destination);
            return true;
        }

        if (format.SequenceEqual("58"))
        {
            var res = Base58.Encode(ToByteArray(), destination);
            charsWritten = 17;
            return res;
        }

        if (format.SequenceEqual("85"))
        {
            charsWritten = 15;
            ToBase85(destination);

            return true;
        }

        if (format.SequenceEqual("p2"))
        {
            charsWritten = 18;
            ToPath2(destination);

            return true;
        }

        if (format.SequenceEqual("p3"))
        {
            charsWritten = 19;
            ToPath3(destination);

            return true;
        }

        charsWritten = 0;
        return false;
    }

    public override Boolean Equals(Object? obj) => obj is Id id && Equals(id);

    public override Int32 GetHashCode()
    {
        int hash = 17;
        hash = 37 * hash + _timestamp.GetHashCode();
        hash = 37 * hash + _b.GetHashCode();
        hash = 37 * hash + _c.GetHashCode();
        return hash;
    }

    #endregion Public Methods

    #region Private Methods

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

    private static int GetMachineXXHash()
    {
        var machineName = Environment.MachineName;
        var bytes = Encoding.UTF8.GetBytes(machineName);
        var hash = (int)XXH32.DigestOf(bytes);
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
        if (secondsSinceEpoch < uint.MinValue || secondsSinceEpoch > uint.MaxValue)
        {
            throw new ArgumentOutOfRangeException(nameof(timestamp));
        }
        return (int)(uint)secondsSinceEpoch;
    }

    private static void FromByteArray(ReadOnlySpan<byte> bytes, int offset, out int timestamp, out int b, out int c)
    {
        timestamp = (bytes[offset] << 24) | (bytes[offset + 1] << 16) | (bytes[offset + 2] << 8) | bytes[offset + 3];
        b = (bytes[offset + 4] << 24) | (bytes[offset + 5] << 16) | (bytes[offset + 6] << 8) | bytes[offset + 7];
        c = (bytes[offset + 8] << 24) | (bytes[offset + 9] << 16) | (bytes[offset + 10] << 8) | bytes[offset + 11];
    }

    private static DateTime ToUniversalTime(DateTime dateTime)
    {
        if (dateTime == DateTime.MinValue)
        {
            return DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Utc);
        }
        else if (dateTime == DateTime.MaxValue)
        {
            return DateTime.SpecifyKind(DateTime.MaxValue, DateTimeKind.Utc);
        }
        else
        {
            return dateTime.ToUniversalTime();
        }
    }

    #endregion Private Methods
}