using IT.Internal;
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id32
{
    public string ToHex()
    {
        var value = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _value0)));
        var len = 24 + GetLengthHex(value) + 1;
        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                _id.TryToHex(chars);

                TryToHex(chars.Slice(24), value);

                chars[len - 1] = Hex.FormatLower;
            }
        }

        return str;
    }

    public string ToHexUpper()
    {
        var value = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _value0)));
        var len = 24 + GetLengthHex(value) + 1;
        var str = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = str)
            {
                var chars = new Span<Char>(ptr, len);

                _id.TryToHexUpper(chars);

                TryToHexUpper(chars.Slice(24), value);

                chars[len - 1] = Hex.FormatUpper;
            }
        }

        return str;
    }

    public static Id32 ParseHex(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;
        if (len == 0) throw new FormatException();

        var version = chars[len - 1];

        if (version != Hex.FormatLower && version != Hex.FormatUpper) throw new FormatException();

        len -= 25;

        if (len == 0) throw new FormatException();

        Id32 id32 = default;

        ref var b = ref Unsafe.As<Id32, byte>(ref id32);

        Unsafe.WriteUnaligned(ref b, Id.ParseHex(chars.Slice(0, 24)));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 12), BinaryPrimitives.ReverseEndianness(Hex.ToUInt32(chars.Slice(24, len))));

        return id32;
    }
}