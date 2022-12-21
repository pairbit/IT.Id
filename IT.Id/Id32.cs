using IT.Internal;
using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT;

[StructLayout(LayoutKind.Explicit, Size = 16)]
public readonly partial struct Id32 : IComparable<Id32>, IEquatable<Id32>
#if NET7_0_OR_GREATER
, System.Numerics.IMinMaxValue<Id32>
#endif
{
    #region Fields

    public static readonly Id32 Empty = default;
    public static readonly Id32 Min = new(Id.Min, Int32.MinValue);
    public static readonly Id32 Max = new(Id.Max, Int32.MaxValue);

    [FieldOffset(0)] private readonly Id _id;
    [FieldOffset(12)] private readonly byte _value0;
    [FieldOffset(13)] private readonly byte _value1;
    [FieldOffset(14)] private readonly byte _value2;
    [FieldOffset(15)] private readonly byte _value3;

    #endregion Fields

    #region Props

    public Id Id => _id;

    public Int32 Value => BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<Int32>(ref Unsafe.AsRef(in _value0)));

#if NET7_0_OR_GREATER

    static Id32 System.Numerics.IMinMaxValue<Id32>.MaxValue => Max;

    static Id32 System.Numerics.IMinMaxValue<Id32>.MinValue => Min;

#endif

    #endregion Props

    #region Ctors

    public Id32(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16) ThrowArgumentException();

        this = Unsafe.ReadUnaligned<Id32>(ref MemoryMarshal.GetReference(bytes));

        //https://github.com/dotnet/runtime/pull/78446
        [StackTraceHidden]
        static void ThrowArgumentException() => throw new ArgumentException("The byte array must be 16 bytes long.", nameof(bytes));
    }

    public Id32(Id id, Int32 value)
    {
        _id = id;
        Unsafe.WriteUnaligned(ref _value0, BinaryPrimitives.ReverseEndianness(value));
    }

    public Id32(Id id, short value0, short value1)
    {
        _id = id;
        Unsafe.WriteUnaligned(ref _value0, BinaryPrimitives.ReverseEndianness(value0));
        Unsafe.WriteUnaligned(ref _value2, BinaryPrimitives.ReverseEndianness(value1));
    }

    public Id32(Id id, byte value0, byte value1, byte value2, byte value3)
    {
        _id = id;
        _value0 = value0;
        _value1 = value1;
        _value2 = value2;
        _value3 = value3;
    }

    #endregion Ctors

    #region Operators

    public static Boolean operator <(Id32 left, Id32 right) => left.CompareTo(right) < 0;

    public static Boolean operator <=(Id32 left, Id32 right) => left.CompareTo(right) <= 0;

    public static Boolean operator ==(Id32 left, Id32 right) => EqualsCore(in left, in right);

    public static Boolean operator !=(Id32 left, Id32 right) => !EqualsCore(in left, in right);

    public static Boolean operator >=(Id32 left, Id32 right) => left.CompareTo(right) >= 0;

    public static Boolean operator >(Id32 left, Id32 right) => left.CompareTo(right) > 0;

    #endregion Operators

    #region Public Methods

    public Byte[] ToByteArray()
    {
        var bytes = new byte[16];

        Unsafe.WriteUnaligned(ref bytes[0], this);

        return bytes;
    }

    public Boolean TryWrite(Span<Byte> bytes)
    {
        if (bytes.Length < 16) return false;

        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), this);

        return true;
    }

    public Int32 CompareTo(Id32 id)
    {
        int result = _id.CompareTo(id._id);

        if (result != 0) return result;

        if (_value0 != id._value0) return _value0 < id._value0 ? -1 : 1;
        if (_value1 != id._value1) return _value1 < id._value1 ? -1 : 1;
        if (_value2 != id._value2) return _value2 < id._value2 ? -1 : 1;
        if (_value3 != id._value3) return _value3 < id._value3 ? -1 : 1;

        return 0;
    }

    public bool Equals(Id32 id)
    {
#if NET7_0_OR_GREATER
        if (System.Runtime.Intrinsics.Vector128.IsHardwareAccelerated)
        {
            return System.Runtime.Intrinsics.Vector128.LoadUnsafe(ref Unsafe.As<Id32, byte>(ref Unsafe.AsRef(in this))) == 
                   System.Runtime.Intrinsics.Vector128.LoadUnsafe(ref Unsafe.As<Id32, byte>(ref Unsafe.AsRef(in id)));
        }
#endif
        ref int l = ref Unsafe.As<Id32, int>(ref Unsafe.AsRef(in this));
        ref int r = ref Unsafe.As<Id32, int>(ref Unsafe.AsRef(in id));

        return l == r && Unsafe.Add(ref l, 1) == Unsafe.Add(ref r, 1) && Unsafe.Add(ref l, 2) == Unsafe.Add(ref r, 2) && Unsafe.Add(ref l, 3) == Unsafe.Add(ref r, 3);
    }

    public override bool Equals(object? obj) => obj is Id32 id && EqualsCore(in this, in id);

    public override int GetHashCode()
    {
        ref int r = ref Unsafe.As<Id32, int>(ref Unsafe.AsRef(in this));
        return r ^ Unsafe.Add(ref r, 1) ^ Unsafe.Add(ref r, 2) ^ Unsafe.Add(ref r, 3);
    }

    public override String ToString() => ToBase64Url();

    /// <exception cref="FormatException"/>
    public String ToString(Idf format) => format switch
    {
        Idf.Hex => ToHex(),
        Idf.HexUpper => ToHexUpper(),
        Idf.Base64Url => ToBase64Url(),
        _ => throw Ex.InvalidFormat(format, nameof(Id32)),
    };

    #endregion Public Methods

    #region Private Methods

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static bool EqualsCore(in Id32 left, in Id32 right)
    {
        ref int l = ref Unsafe.As<Id32, int>(ref Unsafe.AsRef(in left));
        ref int r = ref Unsafe.As<Id32, int>(ref Unsafe.AsRef(in right));

        return l == r && Unsafe.Add(ref l, 1) == Unsafe.Add(ref r, 1) && Unsafe.Add(ref l, 2) == Unsafe.Add(ref r, 2) && Unsafe.Add(ref l, 3) == Unsafe.Add(ref r, 3);
    }

    #endregion Private Methods
}