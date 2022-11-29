using Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

public readonly partial struct Id
{
    private String ToBase64Url()
    {
        var base64Url = new string((char)0, 16);

        unsafe
        {
            fixed (char* dest = base64Url)
            fixed (char* map = Base64.tableUrl)
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                dest[0] = map[(byte0 & 0xfc) >> 2];
                dest[1] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                dest[2] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                dest[3] = map[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                dest[4] = map[(byte3 & 0xfc) >> 2];
                dest[5] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                dest[6] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                dest[7] = map[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                dest[8] = map[(byte6 & 0xfc) >> 2];
                dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                dest[10] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                dest[11] = map[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                dest[12] = map[(byte9 & 0xfc) >> 2];
                dest[13] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                dest[14] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                dest[15] = map[byte11 & 0x3f];
            }
        }
        return base64Url;
    }

    private String ToBase64()
    {
        var base64 = new string((char)0, 16);

        unsafe
        {
            fixed (char* dest = base64)
            fixed (char* map = Base64.table)
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                dest[0] = map[(byte0 & 0xfc) >> 2];
                dest[1] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                dest[2] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                dest[3] = map[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                dest[4] = map[(byte3 & 0xfc) >> 2];
                dest[5] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                dest[6] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                dest[7] = map[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                dest[8] = map[(byte6 & 0xfc) >> 2];
                dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                dest[10] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                dest[11] = map[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                dest[12] = map[(byte9 & 0xfc) >> 2];
                dest[13] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                dest[14] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                dest[15] = map[byte11 & 0x3f];
            }
        }
        return base64;
    }

    private unsafe void ToBase64(Span<Char> chars, Char[] map)
    {
        fixed (char* dest = chars)
        fixed (char* m = &map[0])
        {
            var byte0 = (byte)(_timestamp >> 24);
            var byte1 = (byte)(_timestamp >> 16);
            var byte2 = (byte)(_timestamp >> 8);

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = (byte)(_timestamp);
            var byte4 = (byte)(_b >> 24);
            var byte5 = (byte)(_b >> 16);

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = (byte)(_b >> 8);
            var byte7 = (byte)(_b);
            var byte8 = (byte)(_c >> 24);

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = (byte)(_c >> 16);
            var byte10 = (byte)(_c >> 8);
            var byte11 = (byte)(_c);

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }
    }

    private unsafe void ToBase64(Span<Byte> bytes, Byte[] map)
    {
        fixed (byte* dest = bytes)
        fixed (byte* m = &map[0])
        {
            var byte0 = (byte)(_timestamp >> 24);
            var byte1 = (byte)(_timestamp >> 16);
            var byte2 = (byte)(_timestamp >> 8);

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = (byte)(_timestamp);
            var byte4 = (byte)(_b >> 24);
            var byte5 = (byte)(_b >> 16);

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = (byte)(_b >> 8);
            var byte7 = (byte)(_b);
            var byte8 = (byte)(_c >> 24);

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = (byte)(_c >> 16);
            var byte10 = (byte)(_c >> 8);
            var byte11 = (byte)(_c);

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }
    }

    private static bool TryParseBase64(ReadOnlySpan<Char> chars, out Id id)
    {
        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 0);
        int i1 = Unsafe.Add(ref src, 1);
        int i2 = Unsafe.Add(ref src, 2);
        int i3 = Unsafe.Add(ref src, 3);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        id = new Id(timestamp, b, c | val);
        return true;

    fail:
        id = default;
        return false;
    }

    private static bool TryParseBase64(ReadOnlySpan<Byte> bytes, out Id id)
    {
        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 0);
        int i1 = Unsafe.Add(ref src, 1);
        int i2 = Unsafe.Add(ref src, 2);
        int i3 = Unsafe.Add(ref src, 3);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        id = new Id(timestamp, b, c | val);
        return true;

    fail:
        id = default;
        return false;
    }

    private static Id ParseBase64(ReadOnlySpan<Char> chars)
    {
        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 0);
        int i1 = Unsafe.Add(ref src, 1);
        int i2 = Unsafe.Add(ref src, 2);
        int i3 = Unsafe.Add(ref src, 3);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        return new Id(timestamp, b, c | val);
    }

    private static Id ParseBase64(ReadOnlySpan<Byte> bytes)
    {
        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 0);
        int i1 = Unsafe.Add(ref src, 1);
        int i2 = Unsafe.Add(ref src, 2);
        int i3 = Unsafe.Add(ref src, 3);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        return new Id(timestamp, b, c | val);
    }
}