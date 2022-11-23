using System.Runtime.InteropServices;

namespace System;

[StructLayout(LayoutKind.Explicit, Size = 14)]
internal readonly struct Id16
{
    [FieldOffset(0)]
    private readonly Id _id;

    [FieldOffset(12)]
    private readonly UInt16 _value;

    public static readonly Id16 Empty = default;
    public static readonly Id16 Min = new(Id.Min, UInt16.MinValue);
    public static readonly Id16 Max = new(Id.Max, UInt16.MaxValue);

    public Id Id => _id;

    public UInt16 Value => _value;

    public Id16(Id id, UInt16 value)
    {
        _id = id;
        _value = value;
    }
}