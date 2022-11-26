using Internal;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

public readonly partial struct Id
{
    private String ToPath2()
    {
        var path2 = new string((char)0, 18);
        var sep = Path.DirectorySeparatorChar;
        unsafe
        {
            fixed (char* dest = path2)
            fixed (char* map = Base64.tableUrl)
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                dest[17] = map[(byte0 & 0xfc) >> 2];
                dest[16] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                dest[15] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                dest[14] = map[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                dest[13] = map[(byte3 & 0xfc) >> 2];
                dest[12] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                dest[11] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                dest[10] = map[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                dest[9] = map[(byte6 & 0xfc) >> 2];
                dest[8] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                dest[7] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                dest[6] = map[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                dest[5] = map[(byte9 & 0xfc) >> 2];
                dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                dest[3] = sep;
                dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                dest[1] = sep;
                dest[0] = map[byte11 & 0x3f];
            }
        }
        return path2;
    }

    private void ToPath2(Span<Char> chars)
    {
        var sep = Path.DirectorySeparatorChar;
        unsafe
        {
            fixed (char* dest = chars)
            fixed (char* map = Base64.tableUrl)
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                dest[17] = map[(byte0 & 0xfc) >> 2];
                dest[16] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                dest[15] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                dest[14] = map[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                dest[13] = map[(byte3 & 0xfc) >> 2];
                dest[12] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                dest[11] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                dest[10] = map[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                dest[9] = map[(byte6 & 0xfc) >> 2];
                dest[8] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                dest[7] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                dest[6] = map[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                dest[5] = map[(byte9 & 0xfc) >> 2];
                dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                dest[3] = sep;
                dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                dest[1] = sep;
                dest[0] = map[byte11 & 0x3f];
            }
        }
    }

    private unsafe void ToPath2(Span<Byte> bytes, byte sep)
    {
        fixed (byte* dest = bytes)
        fixed (byte* map = Base64.bytesUrl)
        {
            var byte0 = (byte)(_timestamp >> 24);
            var byte1 = (byte)(_timestamp >> 16);
            var byte2 = (byte)(_timestamp >> 8);

            dest[17] = map[(byte0 & 0xfc) >> 2];
            dest[16] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[15] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[14] = map[byte2 & 0x3f];

            var byte3 = (byte)(_timestamp);
            var byte4 = (byte)(_b >> 24);
            var byte5 = (byte)(_b >> 16);

            dest[13] = map[(byte3 & 0xfc) >> 2];
            dest[12] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[11] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[10] = map[byte5 & 0x3f];

            var byte6 = (byte)(_b >> 8);
            var byte7 = (byte)_b;
            var byte8 = (byte)(_c >> 24);

            dest[9] = map[(byte6 & 0xfc) >> 2];
            dest[8] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[7] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[6] = map[byte8 & 0x3f];

            var byte9 = (byte)(_c >> 16);
            var byte10 = (byte)(_c >> 8);
            var byte11 = (byte)_c;

            dest[5] = map[(byte9 & 0xfc) >> 2];
            dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[3] = sep;
            dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[1] = sep;
            dest[0] = map[byte11 & 0x3f];
        }
    }

    private static Id ParsePath2(ReadOnlySpan<Char> chars)
    {
        var c1 = chars[1];
        var c3 = chars[3];

        if ((c1 != '\\' && c1 != '/') || (c3 != '\\' && c3 != '/')) throw new FormatException();

        //_\I\-TH145xA0ZPhqY

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 17);
        int i1 = Unsafe.Add(ref src, 16);
        int i2 = Unsafe.Add(ref src, 15);
        int i3 = Unsafe.Add(ref src, 14);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 13);
        i1 = Unsafe.Add(ref src, 12);
        i2 = Unsafe.Add(ref src, 11);
        i3 = Unsafe.Add(ref src, 10);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 9);
        i1 = Unsafe.Add(ref src, 8);
        i2 = Unsafe.Add(ref src, 7);
        i3 = Unsafe.Add(ref src, 6);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 5);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        return new Id(timestamp, b, c | val);
    }

    private static Id ParsePath2(ReadOnlySpan<Byte> bytes)
    {
        var c1 = bytes[1];
        var c3 = bytes[3];

        if ((c1 != '\\' && c1 != '/') || (c3 != '\\' && c3 != '/')) throw new FormatException();

        //_\I\-TH145xA0ZPhqY

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 17);
        int i1 = Unsafe.Add(ref src, 16);
        int i2 = Unsafe.Add(ref src, 15);
        int i3 = Unsafe.Add(ref src, 14);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        var timestamp = (byte)(val >> 16) << 24 | (byte)(val >> 8) << 16 | (byte)val << 8;

        i0 = Unsafe.Add(ref src, 13);
        i1 = Unsafe.Add(ref src, 12);
        i2 = Unsafe.Add(ref src, 11);
        i3 = Unsafe.Add(ref src, 10);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        timestamp |= (byte)(val >> 16);

        var b = (byte)(val >> 8) << 24 | (byte)val << 16;

        i0 = Unsafe.Add(ref src, 9);
        i1 = Unsafe.Add(ref src, 8);
        i2 = Unsafe.Add(ref src, 7);
        i3 = Unsafe.Add(ref src, 6);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        b |= (byte)(val >> 16) << 8 | (byte)(val >> 8);

        var c = (byte)val << 24;

        i0 = Unsafe.Add(ref src, 5);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChars(Idf.Path2, i0, i1, i2, i3);

        return new Id(timestamp, b, c | val);
    }
}