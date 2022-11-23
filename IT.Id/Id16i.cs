using System.Runtime.InteropServices;

namespace System;

[StructLayout(LayoutKind.Explicit, Size = 16)]
internal readonly struct Id16i
{
    [FieldOffset(0)]
    private readonly Id _id;

    [FieldOffset(12)]
    private readonly UInt16 _value;

    [FieldOffset(14)]
    private readonly UInt16 _index;

    public static readonly Id16i Empty = default;
    public static readonly Id16i Min = new(Id.Min, UInt16.MinValue, UInt16.MinValue);
    public static readonly Id16i Max = new(Id.Max, UInt16.MaxValue, UInt16.MaxValue);

    public Id Id => _id;

    public UInt16 Value => _value;

    public Int32 Index => _index;

    public Id16i(Id id, UInt16 value, UInt16 index)
    {
        _id = id;
        _value = value;
        _index = index;
    }
}