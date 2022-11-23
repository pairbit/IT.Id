﻿using System.Runtime.InteropServices;

namespace System;

[StructLayout(LayoutKind.Explicit, Size = 13)]
public readonly struct Id8 : IComparable<Id8>, IEquatable<Id8>
{
    [FieldOffset(0)]
    private readonly Id _id;

    [FieldOffset(12)]
    private readonly Byte _value;

    public static readonly Id8 Empty = default;
    public static readonly Id8 Min = new(Id.Min, Byte.MinValue);
    public static readonly Id8 Max = new(Id.Max, Byte.MaxValue);

    public Id Id => _id;

    public Byte Value => _value;

    public Id8(Id id, Byte value)
    {
        _id = id;
        _value = value;
    }

    #region Operators

    public static Boolean operator <(Id8 left, Id8 right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id8 left, Id8 right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id8 left, Id8 right) => left.Equals(right);

    public static Boolean operator !=(Id8 left, Id8 right) => !left.Equals(right);

    public static Boolean operator >=(Id8 left, Id8 right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id8 left, Id8 right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Public Methods

    public Int32 CompareTo(Id8 other)
    {
        int result = _id.CompareTo(other._id);

        if (result != 0) return result;

        return _value.CompareTo(other._value);
    }

    public Boolean Equals(Id8 other) => _id == other._id && _value == other._value;

    public override Boolean Equals(Object? obj) => obj is Id8 id && Equals(id);

    public override Int32 GetHashCode()
    {
        int hash = 17;
        hash = 37 * hash + _id.GetHashCode();
        hash = 37 * hash + _value.GetHashCode();
        return hash;
    }

    public override String ToString()
    {
        var len = 16 + Base64.GetLength(_value) + 1;
        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                _id.TryFormat(chars, out _, Idf.Base64Url);

                Base64.TryWrite(chars.Slice(16), _value);

                chars[len - 1] = '6';
            }
        }

        return str;
    }

    public static Id8 Parse(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;
        if (len == 0) throw new FormatException();

        var b = chars[len - 1];

        if (b != '6') throw new FormatException();

        len -= 17;

        if (len == 0) throw new FormatException();

        var value = Base64.ToByte(chars.Slice(16, len));

        var id = Id.Parse(chars.Slice(0, 16));

        return new Id8(id, value);
    }

    #endregion Public Methods
}