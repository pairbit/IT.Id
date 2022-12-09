using IT.Internal;
using System;
using System.Runtime.InteropServices;

namespace IT;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public readonly struct Id32 : IComparable<Id32>, IEquatable<Id32>
{
    [FieldOffset(0)]
    private readonly Id _id;

    [FieldOffset(12)]
    private readonly UInt32 _value;

    public static readonly Id32 Empty = default;
    public static readonly Id32 Min = new(Id.Min, UInt16.MinValue);
    public static readonly Id32 Max = new(Id.Max, UInt16.MaxValue);

    public Id Id => _id;

    public UInt32 Value => _value;

    public Id32(Id id, UInt32 value)
    {
        _id = id;
        _value = value;
    }

    #region Operators

    public static Boolean operator <(Id32 left, Id32 right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id32 left, Id32 right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id32 left, Id32 right) => left.Equals(right);

    public static Boolean operator !=(Id32 left, Id32 right) => !left.Equals(right);

    public static Boolean operator >=(Id32 left, Id32 right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id32 left, Id32 right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Public Methods

    public Int32 CompareTo(Id32 other)
    {
        int result = _id.CompareTo(other._id);

        if (result != 0) return result;

        return _value.CompareTo(other._value);
    }

    public Boolean Equals(Id32 other) => _id == other._id && _value == other._value;

    public override Boolean Equals(Object? obj) => obj is Id32 id && Equals(id);

    public override Int32 GetHashCode()
    {
        int hash = 17;
        hash = 37 * hash + _id.GetHashCode();
        hash = 37 * hash + _value.GetHashCode();
        return hash;
    }

    public override String ToString() => ToBase64();

    /// <exception cref="FormatException"/>
    public String ToString(Idf format) => format switch
    {
        Idf.Hex => ToHex(format),
        Idf.HexUpper => ToHex(format),
        Idf.Base64Url => ToBase64(),
        _ => throw Ex.InvalidFormat(format, nameof(Id32)),
    };

    public static Id32 Parse(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;
        if (len == 0) throw new FormatException();

        var b = chars[len - 1];

        if (b == '6') return ParseBase64(chars);
        if (b == '1') return ParseHex(chars);

        throw new FormatException();
    }

    #endregion Public Methods

    private String ToHex(Idf format)
    {
        var len = 24 + Hex.GetLength((uint)_value) + 1;
        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                _id.TryFormat(chars, out _, format);

                var map = format == Idf.HexUpper ? Hex._numUpper : Hex._numLower;

                Hex.TryWrite(chars.Slice(24), (uint)_value, map);

                chars[len - 1] = '1';
            }
        }

        return str;
    }

    private String ToBase64()
    {
        var len = 16 + Base64.GetLength((uint)_value) + 1;
        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                _id.TryFormat(chars, out _, Idf.Base64Url);

                Base64.TryWrite(chars.Slice(16), (uint)_value);

                chars[len - 1] = '6';
            }
        }

        return str;
    }

    private static Id32 ParseHex(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;
        if (len == 0) throw new FormatException();

        var b = chars[len - 1];

        if (b != '1') throw new FormatException();

        len -= 25;

        if (len == 0) throw new FormatException();

        var value = Hex.ToUInt32(chars.Slice(24, len));

        var id = Id.Parse(chars.Slice(0, 24));

        return new Id32(id, value);
    }

    private static Id32 ParseBase64(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;
        if (len == 0) throw new FormatException();

        var b = chars[len - 1];

        if (b != '6') throw new FormatException();

        len -= 17;

        if (len == 0) throw new FormatException();

        var value = Base64.ToUInt32(chars.Slice(16, len));

        var id = Id.Parse(chars.Slice(0, 16));

        return new Id32(id, value);
    }
}