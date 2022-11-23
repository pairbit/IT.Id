namespace System;

internal readonly struct Id8i
{
    private readonly Id _id;
    private readonly UInt16 _index1;
    private readonly Byte _index2;
    private readonly Byte _value;

    public static readonly Int32 MinIndex = 0;
    public static readonly Int32 MaxIndex = 16777215;//3 bytes
    public static readonly Id8i Empty = default;
    public static readonly Id8i Min = new(Id.Min, Byte.MinValue, MinIndex);
    public static readonly Id8i Max = new(Id.Max, Byte.MaxValue, MaxIndex);

    public Id Id => _id;

    public Byte Value => _value;

    public Int32 Index => (_index1 << 8) + _index2;

    /// <param name="index">3 bytes</param>
    /// <exception cref="ArgumentOutOfRangeException">The index value must be between 0 and 16777215 (it must fit in 3 bytes)</exception>
    public Id8i(Id id, Byte value, Int32 index)
    {
        if ((index & 0xff000000) != 0) throw new ArgumentOutOfRangeException(nameof(index), "The index value must be between 0 and 16777215 (it must fit in 3 bytes)");

        _id = id;
        _value = value;
        _index1 = (ushort)(index >> 8);//first 2 bytes
        _index2 = (byte)index;//3rd byte
    }
}