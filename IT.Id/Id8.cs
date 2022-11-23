using System.Runtime.InteropServices;

namespace System;

[StructLayout(LayoutKind.Explicit, Size = 13)]
internal readonly struct Id8
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
}