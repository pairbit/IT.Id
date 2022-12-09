using IT.Internal;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading;

namespace IT;

[Serializable]
[StructLayout(LayoutKind.Explicit, Size = 12)]
[DebuggerDisplay("{ToString(),nq}")]
[System.Text.Json.Serialization.JsonConverter(typeof(Json.Converters.JsonIdConverter))]
public readonly partial struct Id : IComparable<Id>, IEquatable<Id>, IFormattable
#if NET6_0_OR_GREATER
, ISpanFormattable
#endif
#if NET7_0_OR_GREATER
, System.Numerics.IMinMaxValue<Id>, ISpanParsable<Id>
#endif
{
    #region Fields

    //DateTime.UnixEpoch
    private static readonly DateTime _unixEpoch = new(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static readonly Int64 _unixEpochTicks = _unixEpoch.Ticks;

    private static readonly Int16 _pid = GetPid();
    private static readonly Int32 _machinePid = (GetMachineXXHash() << 8) | ((_pid >> 8) & 0xff);
    private static readonly Int64 _random = CalculateRandomValue();
    private static Int32 _staticIncrement = new Random().Next();
    private static IDictionary<Int32, String>? _machines;

    /// <summary>
    /// First 3 bytes of machine name hash
    /// </summary>
    public static readonly Int32 MachineHash24 = GetMachineXXHash();
    public static readonly Id Empty = default;
    public static readonly Id Min = new(0, 0, 0);
    public static readonly Id Max = new(-1, -1, -1);

    [FieldOffset(0)]
    internal readonly Int32 _timestamp;

    [FieldOffset(4)]
    internal readonly Int32 _b;

    [FieldOffset(8)]
    internal readonly Int32 _c;

    #endregion Fields

    #region Ctors

    public Id(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 12) throw new ArgumentException("The byte array must be 12 bytes long.", nameof(bytes));

        _timestamp = (bytes[0] << 24) | (bytes[1] << 16) | (bytes[2] << 8) | bytes[3];
        _b = (bytes[4] << 24) | (bytes[5] << 16) | (bytes[6] << 8) | bytes[7];
        _c = (bytes[8] << 24) | (bytes[9] << 16) | (bytes[10] << 8) | bytes[11];
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

    public String? MachineName => _machines == null ? null : (_machines.TryGetValue(Machine, out var name) ? name : null);

    public Int16 Pid => (short)(((_b << 8) & 0xff00) | ((_c >> 24) & 0x00ff));

    public Int32 Increment => _c & 0xffffff;

    public DateTimeOffset Created => _unixEpoch.AddSeconds((uint)_timestamp);

#if NET7_0_OR_GREATER

    static Id System.Numerics.IMinMaxValue<Id>.MaxValue => Max;

    static Id System.Numerics.IMinMaxValue<Id>.MinValue => Min;

#endif

    #endregion Props

    #region Operators

    public static Boolean operator <(Id left, Id right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id left, Id right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id left, Id right) => left.Equals(right);

    public static Boolean operator !=(Id left, Id right) => !left.Equals(right);

    public static Boolean operator >=(Id left, Id right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id left, Id right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Public Methods

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

    public static Int32 GetLength(Idf format) => format switch
    {
        Idf.Base85 => 15,
        Idf.Base64 or Idf.Base64Url => 16,
        Idf.Base58 => 17,
        Idf.Base64Path2 => 18,
        Idf.Base64Path3 => 19,
        Idf.Base32 or Idf.Base32Upper => 20,
        Idf.Hex or Idf.HexUpper => 24,
        _ => throw Ex.InvalidFormat(format)
    };

    /// <summary>
    /// Hex -> "h" <br/>
    /// Hex Upper -> "H" <br/>
    /// Base32 -> "v" <br/>
    /// Base32 Upper -> "V" <br/>
    /// Base58 -> "i" <br/>
    /// Base64 -> "/" <br/>
    /// Base64 Url -> "_" <br/>
    /// Base64 Path2 -> "//" <br/>
    /// Base64 Path3 -> "///" <br/>
    /// Base85 -> "|" <br/>
    /// </summary>
    public static String GetFormatString(Idf format) => format switch
    {
        Idf.Base85 => "|",
        Idf.Base64 => "/",
        Idf.Base64Url => "_",
        Idf.Base58 => "i",
        Idf.Base64Path2 => "//",
        Idf.Base64Path3 => "///",
        Idf.Base32 => "v",
        Idf.Base32Upper => "V",
        Idf.Hex => "h",
        Idf.HexUpper => "H",
        _ => throw Ex.InvalidFormat(format)
    };

    public static Idf GetFormat(Int32 length) => length switch
    {
        15 => Idf.Base85,
        16 => Idf.Base64,
        17 => Idf.Base58,
        18 => Idf.Base64Path2,
        19 => Idf.Base64Path3,
        20 => Idf.Base32,
        24 => Idf.Hex,
        _ => throw Ex.InvalidLength(length)
    };

    public static Boolean TryGetFormat(Int32 length, out Idf format)
    {
        if (length == 15)
        {
            format = Idf.Base85;
            return true;
        }

        if (length == 16)
        {
            format = Idf.Base64;
            return true;
        }

        if (length == 17)
        {
            format = Idf.Base58;
            return true;
        }

        if (length == 18)
        {
            format = Idf.Base64Path2;
            return true;
        }

        if (length == 19)
        {
            format = Idf.Base64Path3;
            return true;
        }

        if (length == 20)
        {
            format = Idf.Base32;
            return true;
        }

        if (length == 24)
        {
            format = Idf.Hex;
            return true;
        }

        format = default;
        return false;
    }

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

    #region Parse

#if NET7_0_OR_GREATER

    static Id IParsable<Id>.Parse(String s, IFormatProvider? provider) => Parse(s.AsSpan());

    static Id ISpanParsable<Id>.Parse(ReadOnlySpan<Char> chars, IFormatProvider? provider) => Parse(chars);

#endif

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> chars) => chars.Length switch
    {
        15 => ParseBase85(chars),
        16 => ParseBase64(chars),
        17 => ParseBase58(chars),
        18 => ParseBase64Path2(chars),
        19 => ParseBase64Path3(chars),
        20 => ParseBase32(chars),
        24 => ParseHex(chars),
        _ => throw Ex.InvalidLengthChars(chars.Length)
    };

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> chars, Idf format)
    {
        if (format == Idf.Hex || format == Idf.HexUpper)
        {
            if (chars.Length != 24) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseHex(chars);
        }

        if (format == Idf.Base32 || format == Idf.Base32Upper)
        {
            if (chars.Length != 20) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseBase32(chars);
        }

        if (format == Idf.Base58)
        {
            var len = chars.Length;
            if (len < 12 || len > 17) throw Ex.InvalidLengthChars(format, len, 12, 17);
            return ParseBase58(chars);
        }

        if (format == Idf.Base64 || format == Idf.Base64Url)
        {
            if (chars.Length != 16) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseBase64(chars);
        }

        if (format == Idf.Base85)
        {
            if (chars.Length != 15) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseBase85(chars);
        }

        if (format == Idf.Base64Path2)
        {
            if (chars.Length != 18) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseBase64Path2(chars);
        }

        if (format == Idf.Base64Path3)
        {
            if (chars.Length != 19) throw Ex.InvalidLengthChars(format, chars.Length);
            return ParseBase64Path3(chars);
        }

        throw Ex.InvalidFormat(format);
    }

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Byte> bytes) => bytes.Length switch
    {
        15 => ParseBase85(bytes),
        16 => ParseBase64(bytes),
        17 => ParseBase58(bytes),
        18 => ParseBase64Path2(bytes),
        19 => ParseBase64Path3(bytes),
        20 => ParseBase32(bytes),
        24 => ParseHex(bytes),
        _ => throw Ex.InvalidLengthBytes(bytes.Length)
    };

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Byte> bytes, Idf format)
    {
        if (format == Idf.Hex || format == Idf.HexUpper)
        {
            if (bytes.Length != 24) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseHex(bytes);
        }

        if (format == Idf.Base32 || format == Idf.Base32Upper)
        {
            if (bytes.Length != 20) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseBase32(bytes);
        }

        if (format == Idf.Base58)
        {
            var len = bytes.Length;
            if (len < 12 || len > 17) throw Ex.InvalidLengthBytes(format, len, 12, 17);
            return ParseBase58(bytes);
        }

        if (format == Idf.Base64 || format == Idf.Base64Url)
        {
            if (bytes.Length != 16) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseBase64(bytes);
        }

        if (format == Idf.Base85)
        {
            if (bytes.Length != 15) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseBase85(bytes);
        }

        if (format == Idf.Base64Path2)
        {
            if (bytes.Length != 18) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseBase64Path2(bytes);
        }

        if (format == Idf.Base64Path3)
        {
            if (bytes.Length != 19) throw Ex.InvalidLengthBytes(format, bytes.Length);
            return ParseBase64Path3(bytes);
        }

        throw Ex.InvalidFormat(format);
    }

    #endregion Parse

    #region TryParse

#if NET7_0_OR_GREATER

    static Boolean IParsable<Id>.TryParse(String? s, IFormatProvider? provider, out Id id) => TryParse(s, out id);

    static Boolean ISpanParsable<Id>.TryParse(ReadOnlySpan<Char> chars, IFormatProvider? provider, out Id id) => TryParse(chars, out id);

#endif

    public static Boolean TryParse(ReadOnlySpan<Char> chars, out Id id)
    {
        var len = chars.Length;

        if (len == 15) return TryParseBase85(chars, out id);

        if (len == 16) return TryParseBase64(chars, out id);

        if (len == 17) return TryParseBase58(chars, out id);

        if (len == 18) return TryParseBase64Path2(chars, out id);

        if (len == 19) return TryParseBase64Path3(chars, out id);

        if (len == 20) return TryParseBase32(chars, out id);

        if (len == 24) return TryParseHex(chars, out id);

        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<Char> chars, Idf format, out Id id)
    {
        if (format == Idf.Hex || format == Idf.HexUpper)
        {
            if (chars.Length != 24) goto fail;
            return TryParseHex(chars, out id);
        }

        if (format == Idf.Base32 || format == Idf.Base32Upper)
        {
            if (chars.Length != 20) goto fail;
            return TryParseBase32(chars, out id);
        }

        if (format == Idf.Base58)
        {
            var len = chars.Length;
            if (len < 12 || len > 17) goto fail;
            return TryParseBase58(chars, out id);
        }

        if (format == Idf.Base64 || format == Idf.Base64Url)
        {
            if (chars.Length != 16) goto fail;
            return TryParseBase64(chars, out id);
        }

        if (format == Idf.Base85)
        {
            if (chars.Length != 15) goto fail;
            return TryParseBase85(chars, out id);
        }

        if (format == Idf.Base64Path2)
        {
            if (chars.Length != 18) goto fail;
            return TryParseBase64Path2(chars, out id);
        }

        if (format == Idf.Base64Path3)
        {
            if (chars.Length != 19) goto fail;
            return TryParseBase64Path3(chars, out id);
        }

    fail:
        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<Byte> bytes, out Id id)
    {
        var len = bytes.Length;

        if (len == 15) return TryParseBase85(bytes, out id);

        if (len == 16) return TryParseBase64(bytes, out id);

        if (len == 17) return TryParseBase58(bytes, out id);

        if (len == 18) return TryParseBase64Path2(bytes, out id);

        if (len == 19) return TryParseBase64Path3(bytes, out id);

        if (len == 20) return TryParseBase32(bytes, out id);

        if (len == 24) return TryParseHex(bytes, out id);

        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<Byte> bytes, Idf format, out Id id)
    {
        if (format == Idf.Hex || format == Idf.HexUpper)
        {
            if (bytes.Length != 24) goto fail;
            return TryParseHex(bytes, out id);
        }

        if (format == Idf.Base32 || format == Idf.Base32Upper)
        {
            if (bytes.Length != 20) goto fail;
            return TryParseBase32(bytes, out id);
        }

        if (format == Idf.Base58)
        {
            var len = bytes.Length;
            if (len < 12 || len > 17) goto fail;
            return TryParseBase58(bytes, out id);
        }

        if (format == Idf.Base64 || format == Idf.Base64Url)
        {
            if (bytes.Length != 16) goto fail;
            return TryParseBase64(bytes, out id);
        }

        if (format == Idf.Base85)
        {
            if (bytes.Length != 15) goto fail;
            return TryParseBase85(bytes, out id);
        }

        if (format == Idf.Base64Path2)
        {
            if (bytes.Length != 18) goto fail;
            return TryParseBase64Path2(bytes, out id);
        }

        if (format == Idf.Base64Path3)
        {
            if (bytes.Length != 19) goto fail;
            return TryParseBase64Path3(bytes, out id);
        }

    fail:
        id = default;
        return false;
    }

    #endregion TryParse

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
        (byte)_timestamp,
        (byte)(_b >> 24),
        (byte)(_b >> 16),
        (byte)(_b >> 8),
        (byte)_b,
        (byte)(_c >> 24),
        (byte)(_c >> 16),
        (byte)(_c >> 8),
        (byte)_c
    };

    public void Write(Span<Byte> bytes)
    {
        if (bytes.Length < 12) throw new ArgumentException("There is not enough space in the destination buffer. A minimum of 12 bytes is required.", nameof(bytes));

        bytes[0] = (byte)(_timestamp >> 24);
        bytes[1] = (byte)(_timestamp >> 16);
        bytes[2] = (byte)(_timestamp >> 8);
        bytes[3] = (byte)_timestamp;
        bytes[4] = (byte)(_b >> 24);
        bytes[5] = (byte)(_b >> 16);
        bytes[6] = (byte)(_b >> 8);
        bytes[7] = (byte)_b;
        bytes[8] = (byte)(_c >> 24);
        bytes[9] = (byte)(_c >> 16);
        bytes[10] = (byte)(_c >> 8);
        bytes[11] = (byte)_c;
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

    /// <param name="format">
    /// null or "_" -> Base64 Url (YqhPZ0Ax541HT-I_) <br/>
    /// "h" -> Hex Lower (62a84f674031e78d474fe23f) <br/>
    /// "H" -> Hex Upper (62A84F674031E78D474FE23F) <br/>
    /// "v" -> Base32 Lower (ce0ytmyc14fgvd7358b0) <br/>
    /// "V" -> Base32 Upper (CE0YTMYC14FGVD7358B0) <br/>
    /// "i" -> Base58 (2ryw1nk6d1eiGQSL6) <br/>
    /// "/" -> Base64 (YqhPZ0Ax541HT+I/) <br/>
    /// "//" -> Base64 Path2 (_/I/-TH145xA0ZPhqY) <br/>
    /// "///" -> Base64 Path3 (_/I/-/TH145xA0ZPhqY) <br/>
    /// "|" -> Base85 (v{IV^PiNKcFO_~|) <br/>
    /// </param>
    /// <exception cref="FormatException"/>
    public String ToString(
#if NET7_0_OR_GREATER
        //[StringSyntax(StringSyntaxAttribute.GuidFormat)] 
#endif
        String? format,
        IFormatProvider? provider = null) => format switch
        {
            null or "_" => ToBase64Url(),
            "h" => ToHexLower(),
            "H" => ToHexUpper(),
            "v" => ToBase32(Base32.LowerAlphabet),
            "V" => ToBase32(Base32.UpperAlphabet),
            "i" => ToBase58(),
            "/" => ToBase64(),
            "//" => ToBase64Path2(),
            "///" => ToBase64Path3(),
            "|" => ToBase85(),
            _ => throw Ex.InvalidFormat(format),
        };

    /// <exception cref="FormatException"/>
    public String ToString(Idf format) => format switch
    {
        Idf.Hex => ToHexLower(),
        Idf.HexUpper => ToHexUpper(),
        Idf.Base32 => ToBase32(Base32.LowerAlphabet),
        Idf.Base32Upper => ToBase32(Base32.UpperAlphabet),
        Idf.Base58 => ToBase58(),
        Idf.Base64 => ToBase64(),
        Idf.Base64Url => ToBase64Url(),
        Idf.Base85 => ToBase85(),
        Idf.Base64Path2 => ToBase64Path2(),
        Idf.Base64Path3 => ToBase64Path3(),
        _ => throw Ex.InvalidFormat(format),
    };

    public OperationStatus TryFormat(Span<Byte> bytes, out Int32 written, Idf format)
    {
        if (format == Idf.Hex)
        {
            if (bytes.Length < 24)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            unsafe
            {
                ToHex(bytes, Hex.Lower16.Map);
            }
            written = 24;
            return OperationStatus.Done;
        }

        if (format == Idf.HexUpper)
        {
            if (bytes.Length < 24)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            unsafe
            {
                ToHex(bytes, Hex.Upper16.Map);
            }
            written = 24;
            return OperationStatus.Done;
        }

        if (format == Idf.Base32)
        {
            if (bytes.Length < 20)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase32(bytes, Base32.LowerEncodeMap);
            written = 20;
            return OperationStatus.Done;
        }

        if (format == Idf.Base32Upper)
        {
            if (bytes.Length < 20)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase32(bytes, Base32.UpperEncodeMap);
            written = 20;
            return OperationStatus.Done;
        }

        if (format == Idf.Base58)
        {
            if (bytes.Length < 17)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase58(bytes);
            written = 17;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64)
        {
            if (bytes.Length < 16)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64(bytes, Base64.bytes);
            written = 16;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Url)
        {
            if (bytes.Length < 16)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64(bytes, Base64.bytesUrl);
            written = 16;
            return OperationStatus.Done;
        }

        if (format == Idf.Base85)
        {
            if (bytes.Length < 15)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase85(bytes);
            written = 15;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Path2)
        {
            if (bytes.Length < 18)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64Path2(bytes);
            written = 18;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Path3)
        {
            if (bytes.Length < 19)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64Path3(bytes);
            written = 19;
            return OperationStatus.Done;
        }

        throw Ex.InvalidFormat(format.ToString());
    }

    public OperationStatus TryFormat(Span<Char> chars, out Int32 written, Idf format)
    {
        if (format == Idf.Hex)
        {
            if (chars.Length < 24)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            unsafe
            {
                ToHex(chars, Hex.Lower32.Map);
            }
            written = 24;
            return OperationStatus.Done;
        }

        if (format == Idf.HexUpper)
        {
            if (chars.Length < 24)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            unsafe
            {
                ToHex(chars, Hex.Upper32.Map);
            }
            written = 24;
            return OperationStatus.Done;
        }

        if (format == Idf.Base32)
        {
            if (chars.Length < 20)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase32(chars, Base32.LowerAlphabet);
            written = 20;
            return OperationStatus.Done;
        }

        if (format == Idf.Base32Upper)
        {
            if (chars.Length < 20)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase32(chars, Base32.UpperAlphabet);
            written = 20;
            return OperationStatus.Done;
        }

        if (format == Idf.Base58)
        {
            if (chars.Length < 17)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase58(chars);
            written = 17;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64)
        {
            if (chars.Length < 16)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64(chars, Base64.table);
            written = 16;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Url)
        {
            if (chars.Length < 16)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64(chars, Base64.tableUrl);
            written = 16;
            return OperationStatus.Done;
        }

        if (format == Idf.Base85)
        {
            if (chars.Length < 15)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase85(chars);
            written = 15;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Path2)
        {
            if (chars.Length < 18)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64Path2(chars);
            written = 18;
            return OperationStatus.Done;
        }

        if (format == Idf.Base64Path3)
        {
            if (chars.Length < 19)
            {
                written = 0;
                return OperationStatus.DestinationTooSmall;
            }
            ToBase64Path3(chars);
            written = 19;
            return OperationStatus.Done;
        }

        throw Ex.InvalidFormat(format.ToString());
    }

    /// <param name="format">
    /// null or "_" -> Base64 Url (YqhPZ0Ax541HT-I_) <br/>
    /// "h" -> Hex Lower (62a84f674031e78d474fe23f) <br/>
    /// "H" -> Hex Upper (62A84F674031E78D474FE23F) <br/>
    /// "v" -> Base32 Lower (ce0ytmyc14fgvd7358b0) <br/>
    /// "V" -> Base32 Upper (CE0YTMYC14FGVD7358B0) <br/>
    /// "i" -> Base58 (2ryw1nk6d1eiGQSL6) <br/>
    /// "/" -> Base64 (YqhPZ0Ax541HT+I/) <br/>
    /// "//" -> Base64 Path2 (_/I/-TH145xA0ZPhqY) <br/>
    /// "///" -> Base64 Path3 (_/I/-/TH145xA0ZPhqY) <br/>
    /// "|" -> Base85 (v{IV^PiNKcFO_~|) <br/>
    /// </param>
    /// <exception cref="FormatException"/>
    public Boolean TryFormat(Span<Char> chars, out Int32 written,
#if NET7_0_OR_GREATER
        //[StringSyntax("/")] 
#endif
        ReadOnlySpan<Char> format = default,
        IFormatProvider? provider = null)
    {
        var len = format.Length;

        if (len == 0)
        {
            if (chars.Length < 16)
            {
                written = 0;
                return false;
            }
            written = 16;
            ToBase64(chars, Base64.tableUrl);
            return true;
        }

        if (len == 1)
        {
            var f = format[0];

            if (f == Base64.Format)
            {
                if (chars.Length < 16)
                {
                    written = 0;
                    return false;
                }
                written = 16;
                ToBase64(chars, Base64.table);
                return true;
            }

            if (f == Base64.FormatUrl)
            {
                if (chars.Length < 16)
                {
                    written = 0;
                    return false;
                }
                written = 16;
                ToBase64(chars, Base64.tableUrl);
                return true;
            }

            if (f == Hex.FormatLower)
            {
                if (chars.Length < 24)
                {
                    written = 0;
                    return false;
                }
                written = 24;
                unsafe
                {
                    ToHex(chars, Hex.Lower32.Map);
                }
                return true;
            }

            if (f == Hex.FormatUpper)
            {
                if (chars.Length < 24)
                {
                    written = 0;
                    return false;
                }
                written = 24;
                unsafe
                {
                    ToHex(chars, Hex.Upper32.Map);
                }
                return true;
            }

            if (f == Base32.FormatLower)
            {
                if (chars.Length < 20)
                {
                    written = 0;
                    return false;
                }
                written = 20;
                ToBase32(chars, Base32.LowerAlphabet);
                return true;
            }

            if (f == Base32.FormatUpper)
            {
                if (chars.Length < 20)
                {
                    written = 0;
                    return false;
                }
                written = 20;
                ToBase32(chars, Base32.UpperAlphabet);
                return true;
            }

            if (f == Base58.Format)
            {
                if (chars.Length < 17)
                {
                    written = 0;
                    return false;
                }
                written = 17;
                ToBase58(chars);
                return true;
            }

            if (f == Base85.Format)
            {
                if (chars.Length < 15)
                {
                    written = 0;
                    return false;
                }
                written = 15;
                ToBase85(chars);
                return true;
            }
        }

        else if (len == 2)
        {
            if (format[0] == '/' && format[1] == '/')
            {
                if (chars.Length < 18)
                {
                    written = 0;
                    return false;
                }
                written = 18;
                ToBase64Path2(chars);
                return true;
            }
        }

        else if (len == 3)
        {
            if (format[0] == '/' && format[1] == '/' && format[2] == '/')
            {
                if (chars.Length < 19)
                {
                    written = 0;
                    return false;
                }
                written = 19;
                ToBase64Path3(chars);
                return true;
            }
        }

        throw Ex.InvalidFormat(format.ToString());
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