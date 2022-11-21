using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

public readonly partial struct Id
{
    private String ToPath2()
    {
        var result = new string((char)0, 18);
        var table = Base64.tableUrl;
        var sep = Path.DirectorySeparatorChar;
        unsafe
        {
            fixed (char* resultP = result)
            fixed (char* base64 = &table[0])
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                resultP[17] = base64[(byte0 & 0xfc) >> 2];
                resultP[16] = base64[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                resultP[15] = base64[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                resultP[14] = base64[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                resultP[13] = base64[(byte3 & 0xfc) >> 2];
                resultP[12] = base64[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                resultP[11] = base64[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                resultP[10] = base64[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                resultP[9] = base64[(byte6 & 0xfc) >> 2];
                resultP[8] = base64[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                resultP[7] = base64[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                resultP[6] = base64[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                resultP[5] = base64[(byte9 & 0xfc) >> 2];
                resultP[4] = base64[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                resultP[3] = sep;
                resultP[2] = base64[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                resultP[1] = sep;
                resultP[0] = base64[byte11 & 0x3f];
            }
        }
        return result;
    }

    private void ToPath2(Span<Char> destination)
    {
        var sep = Path.DirectorySeparatorChar;
        unsafe
        {
            fixed (char* resultP = destination)
            fixed (char* map = Base64.tableUrl)
            {
                var byte0 = (byte)(_timestamp >> 24);
                var byte1 = (byte)(_timestamp >> 16);
                var byte2 = (byte)(_timestamp >> 8);

                resultP[17] = map[(byte0 & 0xfc) >> 2];
                resultP[16] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
                resultP[15] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
                resultP[14] = map[byte2 & 0x3f];

                var byte3 = (byte)(_timestamp);
                var byte4 = (byte)(_b >> 24);
                var byte5 = (byte)(_b >> 16);

                resultP[13] = map[(byte3 & 0xfc) >> 2];
                resultP[12] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
                resultP[11] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
                resultP[10] = map[byte5 & 0x3f];

                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);

                resultP[9] = map[(byte6 & 0xfc) >> 2];
                resultP[8] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
                resultP[7] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
                resultP[6] = map[byte8 & 0x3f];

                var byte9 = (byte)(_c >> 16);
                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                resultP[5] = map[(byte9 & 0xfc) >> 2];
                resultP[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
                resultP[3] = sep;
                resultP[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
                resultP[1] = sep;
                resultP[0] = map[byte11 & 0x3f];
            }
        }
    }

    private unsafe void ToPath2(Span<Byte> destination, byte sep)
    {
        fixed (byte* resultP = destination)
        fixed (byte* map = Base64.bytesUrl)
        {
            var byte0 = (byte)(_timestamp >> 24);
            var byte1 = (byte)(_timestamp >> 16);
            var byte2 = (byte)(_timestamp >> 8);

            resultP[17] = map[(byte0 & 0xfc) >> 2];
            resultP[16] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            resultP[15] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            resultP[14] = map[byte2 & 0x3f];

            var byte3 = (byte)(_timestamp);
            var byte4 = (byte)(_b >> 24);
            var byte5 = (byte)(_b >> 16);

            resultP[13] = map[(byte3 & 0xfc) >> 2];
            resultP[12] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            resultP[11] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            resultP[10] = map[byte5 & 0x3f];

            var byte6 = (byte)(_b >> 8);
            var byte7 = (byte)_b;
            var byte8 = (byte)(_c >> 24);

            resultP[9] = map[(byte6 & 0xfc) >> 2];
            resultP[8] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            resultP[7] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            resultP[6] = map[byte8 & 0x3f];

            var byte9 = (byte)(_c >> 16);
            var byte10 = (byte)(_c >> 8);
            var byte11 = (byte)_c;

            resultP[5] = map[(byte9 & 0xfc) >> 2];
            resultP[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            resultP[3] = sep;
            resultP[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            resultP[1] = sep;
            resultP[0] = map[byte11 & 0x3f];
        }
    }

    private static Id ParsePath2(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 18) throw new ArgumentException("The id must be 18 characters long", nameof(chars));

        var c1 = chars[1];
        var c3 = chars[3];

        if ((c1 != '\\' && c1 != '/') || (c3 != '\\' && c3 != '/')) throw new FormatException();

        //_\I\-TH145xA0ZPhqY

        ReadOnlySpan<sbyte> mapSpan = Base64._decodingMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 17);
        int i1 = Unsafe.Add(ref src, 16);
        int i2 = Unsafe.Add(ref src, 15);
        int i3 = Unsafe.Add(ref src, 14);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        var timestamp = (byte)(i0 >> 16) << 24 | (byte)(i0 >> 8) << 16 | (byte)i0 << 8;

        i0 = Unsafe.Add(ref src, 13);
        i1 = Unsafe.Add(ref src, 12);
        i2 = Unsafe.Add(ref src, 11);
        i3 = Unsafe.Add(ref src, 10);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        timestamp |= (byte)(i0 >> 16);

        var b = (byte)(i0 >> 8) << 24 | (byte)i0 << 16;

        i0 = Unsafe.Add(ref src, 9);
        i1 = Unsafe.Add(ref src, 8);
        i2 = Unsafe.Add(ref src, 7);
        i3 = Unsafe.Add(ref src, 6);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        b |= (byte)(i0 >> 16) << 8 | (byte)(i0 >> 8);

        var c = (byte)i0 << 24;

        i0 = Unsafe.Add(ref src, 5);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        return new Id(timestamp, b, c | i0);
    }

    private static Id ParsePath2(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 18) throw new ArgumentException("The id must be 18 bytes long", nameof(bytes));

        var c1 = bytes[1];
        var c3 = bytes[3];

        if ((c1 != '\\' && c1 != '/') || (c3 != '\\' && c3 != '/')) throw new FormatException();

        //_\I\-TH145xA0ZPhqY

        ReadOnlySpan<sbyte> mapSpan = Base64._decodingMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 17);
        int i1 = Unsafe.Add(ref src, 16);
        int i2 = Unsafe.Add(ref src, 15);
        int i3 = Unsafe.Add(ref src, 14);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        var timestamp = (byte)(i0 >> 16) << 24 | (byte)(i0 >> 8) << 16 | (byte)i0 << 8;

        i0 = Unsafe.Add(ref src, 13);
        i1 = Unsafe.Add(ref src, 12);
        i2 = Unsafe.Add(ref src, 11);
        i3 = Unsafe.Add(ref src, 10);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        timestamp |= (byte)(i0 >> 16);

        var b = (byte)(i0 >> 8) << 24 | (byte)i0 << 16;

        i0 = Unsafe.Add(ref src, 9);
        i1 = Unsafe.Add(ref src, 8);
        i2 = Unsafe.Add(ref src, 7);
        i3 = Unsafe.Add(ref src, 6);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        b |= (byte)(i0 >> 16) << 8 | (byte)(i0 >> 8);

        var c = (byte)i0 << 24;

        i0 = Unsafe.Add(ref src, 5);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw new FormatException();

        i0 = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (i0 < 0) throw new FormatException();

        return new Id(timestamp, b, c | i0);
    }
}