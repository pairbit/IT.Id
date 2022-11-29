using Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

public readonly partial struct Id
{
    private String ToBase32(String abc)
    {
        var base32 = new string((char)0, 20);
        unsafe
        {
            fixed (char* dest = base32)
            fixed (char* map = abc)
            {
                ulong value = (byte)(_timestamp >> 24);
                value = (value << 8) | (byte)(_timestamp >> 16);
                value = (value << 8) | (byte)(_timestamp >> 8);
                value = (value << 8) | (byte)_timestamp;
                value = (value << 8) | (byte)(_b >> 24);

                //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

                dest[0] = map[value >> 35];
                dest[1] = map[(value >> 30) & 0x1F];
                dest[2] = map[(value >> 25) & 0x1F];
                dest[3] = map[(value >> 20) & 0x1F];
                dest[4] = map[(value >> 15) & 0x1F];
                dest[5] = map[(value >> 10) & 0x1F];
                dest[6] = map[(value >> 5) & 0x1F];
                dest[7] = map[value & 0x1F];

                value = (byte)(_b >> 16);
                value = (value << 8) | (byte)(_b >> 8);
                value = (value << 8) | (byte)_b;
                value = (value << 8) | (byte)(_c >> 24);
                value = (value << 8) | (byte)(_c >> 16);

                dest[8] = map[value >> 35];
                dest[9] = map[(value >> 30) & 0x1F];
                dest[10] = map[(value >> 25) & 0x1F];
                dest[11] = map[(value >> 20) & 0x1F];
                dest[12] = map[(value >> 15) & 0x1F];
                dest[13] = map[(value >> 10) & 0x1F];
                dest[14] = map[(value >> 5) & 0x1F];
                dest[15] = map[value & 0x1F];

                value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

                dest[16] = map[value >> 15];
                dest[17] = map[(value >> 10) & 0x1F];
                dest[18] = map[(value >> 5) & 0x1F];
                dest[19] = map[value & 0x1F];
            }
        }

        return base32;
    }

    private unsafe void ToBase32(Span<Char> chars, String abc)
    {
        fixed (char* dest = chars)
        fixed (char* map = abc)
        {
            ulong value = (byte)(_timestamp >> 24);
            value = (value << 8) | (byte)(_timestamp >> 16);
            value = (value << 8) | (byte)(_timestamp >> 8);
            value = (value << 8) | (byte)_timestamp;
            value = (value << 8) | (byte)(_b >> 24);

            //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            dest[0] = map[value >> 35];
            dest[1] = map[(value >> 30) & 0x1F];
            dest[2] = map[(value >> 25) & 0x1F];
            dest[3] = map[(value >> 20) & 0x1F];
            dest[4] = map[(value >> 15) & 0x1F];
            dest[5] = map[(value >> 10) & 0x1F];
            dest[6] = map[(value >> 5) & 0x1F];
            dest[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            dest[8] = map[value >> 35];
            dest[9] = map[(value >> 30) & 0x1F];
            dest[10] = map[(value >> 25) & 0x1F];
            dest[11] = map[(value >> 20) & 0x1F];
            dest[12] = map[(value >> 15) & 0x1F];
            dest[13] = map[(value >> 10) & 0x1F];
            dest[14] = map[(value >> 5) & 0x1F];
            dest[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            dest[16] = map[value >> 15];
            dest[17] = map[(value >> 10) & 0x1F];
            dest[18] = map[(value >> 5) & 0x1F];
            dest[19] = map[value & 0x1F];
        }
    }

    private unsafe void ToBase32(Span<Byte> bytes, byte[] abc)
    {
        fixed (byte* dest = bytes)
        fixed (byte* map = abc)
        {
            ulong value = (byte)(_timestamp >> 24);
            value = (value << 8) | (byte)(_timestamp >> 16);
            value = (value << 8) | (byte)(_timestamp >> 8);
            value = (value << 8) | (byte)_timestamp;
            value = (value << 8) | (byte)(_b >> 24);

            //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            dest[0] = map[value >> 35];
            dest[1] = map[(value >> 30) & 0x1F];
            dest[2] = map[(value >> 25) & 0x1F];
            dest[3] = map[(value >> 20) & 0x1F];
            dest[4] = map[(value >> 15) & 0x1F];
            dest[5] = map[(value >> 10) & 0x1F];
            dest[6] = map[(value >> 5) & 0x1F];
            dest[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            dest[8] = map[value >> 35];
            dest[9] = map[(value >> 30) & 0x1F];
            dest[10] = map[(value >> 25) & 0x1F];
            dest[11] = map[(value >> 20) & 0x1F];
            dest[12] = map[(value >> 15) & 0x1F];
            dest[13] = map[(value >> 10) & 0x1F];
            dest[14] = map[(value >> 5) & 0x1F];
            dest[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            dest[16] = map[value >> 15];
            dest[17] = map[(value >> 10) & 0x1F];
            dest[18] = map[(value >> 5) & 0x1F];
            dest[19] = map[value & 0x1F];
        }
    }

    private static bool TryParseBase32(ReadOnlySpan<Char> chars, out Id id)
    {
        ReadOnlySpan<byte> map = Base32.DecodeMap;

        var ch = chars[0];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        var by = map[ch];
        if (by == 0xFF) goto fail;
        ulong val = by;

        ch = chars[1];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[2];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[3];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[4];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[5];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[6];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[7];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        var timestamp = (byte)(val >> 32) << 24 | (byte)(val >> 24) << 16 | (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var b = (int)(byte)val;

        ch = chars[8];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[9];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[10];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[11];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[12];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[13];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[14];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[15];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        b = b << 24 | (byte)(val >> 32) << 16 | (byte)(val >> 24) << 8 | (byte)(val >> 16);

        var c = (byte)(val >> 8) << 24 | (byte)val << 16;

        ch = chars[16];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[17];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[18];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        ch = chars[19];
        if (ch < Base32.Min || ch > Base32.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        c |= (byte)(val >> 12) << 8 | (byte)(val >> 4);

        id = new Id(timestamp, b, c);
        return true;

    fail:
        id = default;
        return false;
    }

    private static bool TryParseBase32(ReadOnlySpan<Byte> bytes, out Id id)
    {
        ReadOnlySpan<byte> map = Base32.DecodeMap;

        var by = map[bytes[0]];
        if (by == 0xFF) goto fail;
        ulong val = by;

        by = map[bytes[1]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[2]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[3]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[4]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[5]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[6]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[7]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        var timestamp = (byte)(val >> 32) << 24 | (byte)(val >> 24) << 16 | (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var b = (int)(byte)val;

        by = map[bytes[8]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[9]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[10]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[11]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[12]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[13]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[14]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[15]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        b = b << 24 | (byte)(val >> 32) << 16 | (byte)(val >> 24) << 8 | (byte)(val >> 16);

        var c = (byte)(val >> 8) << 24 | (byte)val << 16;

        by = map[bytes[16]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[17]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[18]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        by = map[bytes[19]];
        if (by == 0xFF) goto fail;
        val = (val << 5) | by;

        c |= (byte)(val >> 12) << 8 | (byte)(val >> 4);

        id = new Id(timestamp, b, c);
        return true;

    fail:
        id = default;
        return false;
    }

    private static Id ParseBase32(ReadOnlySpan<Char> chars)
    {
        ReadOnlySpan<byte> mapSpan = Base32.DecodeMap;
        ref byte map = ref MemoryMarshal.GetReference(mapSpan);

        ulong v = Map32(ref map, chars[0]);
        v = (v << 5) | Map32(ref map, chars[1]);
        v = (v << 5) | Map32(ref map, chars[2]);
        v = (v << 5) | Map32(ref map, chars[3]);
        v = (v << 5) | Map32(ref map, chars[4]);
        v = (v << 5) | Map32(ref map, chars[5]);
        v = (v << 5) | Map32(ref map, chars[6]);
        v = (v << 5) | Map32(ref map, chars[7]);

        var timestamp = (byte)(v >> 32) << 24 | (byte)(v >> 24) << 16 | (byte)(v >> 16) << 8 | (byte)(v >> 8);

        var b = (int)(byte)v;

        v = (v << 5) | Map32(ref map, chars[8]);
        v = (v << 5) | Map32(ref map, chars[9]);
        v = (v << 5) | Map32(ref map, chars[10]);
        v = (v << 5) | Map32(ref map, chars[11]);
        v = (v << 5) | Map32(ref map, chars[12]);
        v = (v << 5) | Map32(ref map, chars[13]);
        v = (v << 5) | Map32(ref map, chars[14]);
        v = (v << 5) | Map32(ref map, chars[15]);

        b = b << 24 | (byte)(v >> 32) << 16 | (byte)(v >> 24) << 8 | (byte)(v >> 16);

        var c = (byte)(v >> 8) << 24 | (byte)v << 16;

        v = Map32(ref map, chars[16]);
        v = (v << 5) | Map32(ref map, chars[17]);
        v = (v << 5) | Map32(ref map, chars[18]);
        v = (v << 5) | Map32(ref map, chars[19]);

        c |= (byte)(v >> 12) << 8 | (byte)(v >> 4);

        return new Id(timestamp, b, c);
    }

    private static Id ParseBase32(ReadOnlySpan<Byte> bytes)
    {
        ReadOnlySpan<byte> map = Base32.DecodeMap;

        var by = map[bytes[0]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[0]);
        ulong val = by;

        by = map[bytes[1]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[1]);
        val = (val << 5) | by;

        by = map[bytes[2]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[2]);
        val = (val << 5) | by;

        by = map[bytes[3]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[3]);
        val = (val << 5) | by;

        by = map[bytes[4]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[4]);
        val = (val << 5) | by;

        by = map[bytes[5]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[5]);
        val = (val << 5) | by;

        by = map[bytes[6]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[6]);
        val = (val << 5) | by;

        by = map[bytes[7]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[7]);
        val = (val << 5) | by;

        var timestamp = (byte)(val >> 32) << 24 | (byte)(val >> 24) << 16 | (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var b = (int)(byte)val;

        by = map[bytes[8]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[8]);
        val = (val << 5) | by;

        by = map[bytes[9]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[9]);
        val = (val << 5) | by;

        by = map[bytes[10]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[10]);
        val = (val << 5) | by;

        by = map[bytes[11]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[11]);
        val = (val << 5) | by;

        by = map[bytes[12]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[12]);
        val = (val << 5) | by;

        by = map[bytes[13]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[13]);
        val = (val << 5) | by;

        by = map[bytes[14]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[14]);
        val = (val << 5) | by;

        by = map[bytes[15]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[15]);
        val = (val << 5) | by;

        b = b << 24 | (byte)(val >> 32) << 16 | (byte)(val >> 24) << 8 | (byte)(val >> 16);

        var c = (byte)(val >> 8) << 24 | (byte)val << 16;

        by = map[bytes[16]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[16]);
        val = (val << 5) | by;

        by = map[bytes[17]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[17]);
        val = (val << 5) | by;

        by = map[bytes[18]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[18]);
        val = (val << 5) | by;

        by = map[bytes[19]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base32, bytes[19]);
        val = (val << 5) | by;

        c |= (byte)(val >> 12) << 8 | (byte)(val >> 4);

        return new Id(timestamp, b, c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte Map32(ref byte map, char c)
    {
        if (c < Base32.Min || c > Base32.Max) throw Ex.InvalidChar(Idf.Base32, c);

        var value = Unsafe.Add(ref map, c);

        if (value == 0xFF) throw Ex.InvalidChar(Idf.Base32, c);

        return value;
    }
}