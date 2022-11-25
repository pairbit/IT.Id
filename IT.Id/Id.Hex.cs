using Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

public readonly partial struct Id
{
    private String ToHexLower()
    {
        var hexLower = new string((char)0, 24);
        unsafe
        {
            var map = Hex.Lower32.Map;
            fixed (char* hexLowerP = hexLower)
            {
                uint* dest = (uint*)hexLowerP;
                dest[0] = map[(byte)(_timestamp >> 24)];
                dest[1] = map[(byte)(_timestamp >> 16)];
                dest[2] = map[(byte)(_timestamp >> 8)];
                dest[3] = map[(byte)(_timestamp)];
                dest[4] = map[(byte)(_b >> 24)];
                dest[5] = map[(byte)(_b >> 16)];
                dest[6] = map[(byte)(_b >> 8)];
                dest[7] = map[(byte)(_b)];
                dest[8] = map[(byte)(_c >> 24)];
                dest[9] = map[(byte)(_c >> 16)];
                dest[10] = map[(byte)(_c >> 8)];
                dest[11] = map[(byte)(_c)];
            }
        }
        return hexLower;
    }

    private String ToHexUpper()
    {
        var hexUpper = new string((char)0, 24);
        unsafe
        {
            var map = Hex.Upper32.Map;
            fixed (char* hexUpperP = hexUpper)
            {
                uint* dest = (uint*)hexUpperP;
                dest[0] = map[(byte)(_timestamp >> 24)];
                dest[1] = map[(byte)(_timestamp >> 16)];
                dest[2] = map[(byte)(_timestamp >> 8)];
                dest[3] = map[(byte)(_timestamp)];
                dest[4] = map[(byte)(_b >> 24)];
                dest[5] = map[(byte)(_b >> 16)];
                dest[6] = map[(byte)(_b >> 8)];
                dest[7] = map[(byte)(_b)];
                dest[8] = map[(byte)(_c >> 24)];
                dest[9] = map[(byte)(_c >> 16)];
                dest[10] = map[(byte)(_c >> 8)];
                dest[11] = map[(byte)(_c)];
            }
        }
        return hexUpper;
    }

    private unsafe void ToHex(Span<Char> chars, uint* map)
    {
        fixed (char* charsP = chars)
        {
            uint* dest = (uint*)charsP;
            dest[0] = map[(byte)(_timestamp >> 24)];
            dest[1] = map[(byte)(_timestamp >> 16)];
            dest[2] = map[(byte)(_timestamp >> 8)];
            dest[3] = map[(byte)_timestamp];
            dest[4] = map[(byte)(_b >> 24)];
            dest[5] = map[(byte)(_b >> 16)];
            dest[6] = map[(byte)(_b >> 8)];
            dest[7] = map[(byte)_b];
            dest[8] = map[(byte)(_c >> 24)];
            dest[9] = map[(byte)(_c >> 16)];
            dest[10] = map[(byte)(_c >> 8)];
            dest[11] = map[(byte)_c];
        }
    }

    private unsafe void ToHex(Span<Byte> bytes, ushort* map)
    {
        fixed (byte* bytesP = bytes)
        {
            ushort* dest = (ushort*)bytesP;
            dest[0] = map[(byte)(_timestamp >> 24)];
            dest[1] = map[(byte)(_timestamp >> 16)];
            dest[2] = map[(byte)(_timestamp >> 8)];
            dest[3] = map[(byte)_timestamp];
            dest[4] = map[(byte)(_b >> 24)];
            dest[5] = map[(byte)(_b >> 16)];
            dest[6] = map[(byte)(_b >> 8)];
            dest[7] = map[(byte)_b];
            dest[8] = map[(byte)(_c >> 24)];
            dest[9] = map[(byte)(_c >> 16)];
            dest[10] = map[(byte)(_c >> 8)];
            dest[11] = map[(byte)_c];
        }
    }

    private static Id ParseHex(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 24) throw new ArgumentException("The id must be 24 bytes long", nameof(chars));

        ReadOnlySpan<sbyte> mapSpan = Hex.DecodeMap;
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        var b0 = (byte)((Map16(ref map, chars[0]) << 4) | Map16(ref map, chars[1]));
        var b1 = (byte)((Map16(ref map, chars[2]) << 4) | Map16(ref map, chars[3]));
        var b2 = (byte)((Map16(ref map, chars[4]) << 4) | Map16(ref map, chars[5]));
        var b3 = (byte)((Map16(ref map, chars[6]) << 4) | Map16(ref map, chars[7]));
        var b4 = (byte)((Map16(ref map, chars[8]) << 4) | Map16(ref map, chars[9]));
        var b5 = (byte)((Map16(ref map, chars[10]) << 4) | Map16(ref map, chars[11]));
        var b6 = (byte)((Map16(ref map, chars[12]) << 4) | Map16(ref map, chars[13]));
        var b7 = (byte)((Map16(ref map, chars[14]) << 4) | Map16(ref map, chars[15]));
        var b8 = (byte)((Map16(ref map, chars[16]) << 4) | Map16(ref map, chars[17]));
        var b9 = (byte)((Map16(ref map, chars[18]) << 4) | Map16(ref map, chars[19]));
        var b10 = (byte)((Map16(ref map, chars[20]) << 4) | Map16(ref map, chars[21]));
        var b11 = (byte)((Map16(ref map, chars[22]) << 4) | Map16(ref map, chars[23]));

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }

    private static Id ParseHex(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 24) throw new ArgumentException("The id must be 24 bytes long", nameof(bytes));

        ReadOnlySpan<sbyte> mapSpan = Hex.DecodeMap;
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        var b0 = (byte)((Map16(ref map, bytes[0]) << 4) | Map16(ref map, bytes[1]));
        var b1 = (byte)((Map16(ref map, bytes[2]) << 4) | Map16(ref map, bytes[3]));
        var b2 = (byte)((Map16(ref map, bytes[4]) << 4) | Map16(ref map, bytes[5]));
        var b3 = (byte)((Map16(ref map, bytes[6]) << 4) | Map16(ref map, bytes[7]));
        var b4 = (byte)((Map16(ref map, bytes[8]) << 4) | Map16(ref map, bytes[9]));
        var b5 = (byte)((Map16(ref map, bytes[10]) << 4) | Map16(ref map, bytes[11]));
        var b6 = (byte)((Map16(ref map, bytes[12]) << 4) | Map16(ref map, bytes[13]));
        var b7 = (byte)((Map16(ref map, bytes[14]) << 4) | Map16(ref map, bytes[15]));
        var b8 = (byte)((Map16(ref map, bytes[16]) << 4) | Map16(ref map, bytes[17]));
        var b9 = (byte)((Map16(ref map, bytes[18]) << 4) | Map16(ref map, bytes[19]));
        var b10 = (byte)((Map16(ref map, bytes[20]) << 4) | Map16(ref map, bytes[21]));
        var b11 = (byte)((Map16(ref map, bytes[22]) << 4) | Map16(ref map, bytes[23]));

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte Map16(ref sbyte map, int c)
    {
        if (c < Hex.Min || c > Hex.Max) throw NewFormatException((char)c, Idf.Hex);

        var value = Unsafe.Add(ref map, c);

        if (value == -1) throw NewFormatException((char)c, Idf.Hex);

        return (byte)value;
    }
}