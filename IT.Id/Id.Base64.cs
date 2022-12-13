using IT.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT;

public readonly partial struct Id
{
    public unsafe string ToBase64Url()
    {
        var base64Url = new string((char)0, 16);

        fixed (char* dest = base64Url)
        fixed (char* map = Base64.tableUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = map[(byte0 & 0xfc) >> 2];
            dest[1] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = map[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = map[(byte3 & 0xfc) >> 2];
            dest[5] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = map[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = map[(byte6 & 0xfc) >> 2];
            dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = map[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = map[(byte9 & 0xfc) >> 2];
            dest[13] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = map[byte11 & 0x3f];
        }

        return base64Url;
    }

    public unsafe string ToBase64()
    {
        var base64 = new string((char)0, 16);

        fixed (char* dest = base64)
        fixed (char* map = Base64.table)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = map[(byte0 & 0xfc) >> 2];
            dest[1] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = map[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = map[(byte3 & 0xfc) >> 2];
            dest[5] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = map[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = map[(byte6 & 0xfc) >> 2];
            dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = map[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = map[(byte9 & 0xfc) >> 2];
            dest[13] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = map[byte11 & 0x3f];
        }

        return base64;
    }

    public unsafe bool TryToBase64Url(Span<char> chars)
    {
        if (chars.Length < 16) return false;

        fixed (char* dest = chars)
        fixed (char* m = Base64.tableUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }

        return true;
    }

    public unsafe bool TryToBase64(Span<char> chars)
    {
        if (chars.Length < 16) return false;

        fixed (char* dest = chars)
        fixed (char* m = Base64.table)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }

        return true;
    }

    public unsafe bool TryToBase64Url(Span<byte> bytes)
    {
        if (bytes.Length < 16) return false;

        fixed (byte* dest = bytes)
        fixed (byte* m = Base64.bytesUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }

        return true;
    }

    public unsafe bool TryToBase64(Span<byte> bytes)
    {
        if (bytes.Length < 16) return false;

        fixed (byte* dest = bytes)
        fixed (byte* m = Base64.bytes)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[0] = m[(byte0 & 0xfc) >> 2];
            dest[1] = m[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[2] = m[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[3] = m[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[4] = m[(byte3 & 0xfc) >> 2];
            dest[5] = m[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[6] = m[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[7] = m[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[8] = m[(byte6 & 0xfc) >> 2];
            dest[9] = m[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[10] = m[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[11] = m[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[12] = m[(byte9 & 0xfc) >> 2];
            dest[13] = m[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[14] = m[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[15] = m[byte11 & 0x3f];
        }

        return true;
    }

    public static unsafe bool TryParseBase64(ReadOnlySpan<char> chars, out Id id)
    {
        if (chars.Length != 16) goto fail;

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

        id = new Id();

        fixed (void* p = &id)
        {
            var b = (byte*)p;

            Unsafe.WriteUnaligned(b, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 1, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 2, (byte)val);

            i0 = Unsafe.Add(ref src, 4);
            i1 = Unsafe.Add(ref src, 5);
            i2 = Unsafe.Add(ref src, 6);
            i3 = Unsafe.Add(ref src, 7);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 3, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 4, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 5, (byte)val);

            i0 = Unsafe.Add(ref src, 8);
            i1 = Unsafe.Add(ref src, 9);
            i2 = Unsafe.Add(ref src, 10);
            i3 = Unsafe.Add(ref src, 11);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 6, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 7, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 8, (byte)val);

            i0 = Unsafe.Add(ref src, 12);
            i1 = Unsafe.Add(ref src, 13);
            i2 = Unsafe.Add(ref src, 14);
            i3 = Unsafe.Add(ref src, 15);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 9, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 10, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 11, (byte)val);
        }

        return true;

    fail:
        id = default;
        return false;
    }

    public static unsafe bool TryParseBase64(ReadOnlySpan<byte> bytes, out Id id)
    {
        if (bytes.Length != 16) goto fail;

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

        id = new Id();

        fixed (void* p = &id)
        {
            var b = (byte*)p;

            Unsafe.WriteUnaligned(b, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 1, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 2, (byte)val);

            i0 = Unsafe.Add(ref src, 4);
            i1 = Unsafe.Add(ref src, 5);
            i2 = Unsafe.Add(ref src, 6);
            i3 = Unsafe.Add(ref src, 7);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 3, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 4, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 5, (byte)val);

            i0 = Unsafe.Add(ref src, 8);
            i1 = Unsafe.Add(ref src, 9);
            i2 = Unsafe.Add(ref src, 10);
            i3 = Unsafe.Add(ref src, 11);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 6, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 7, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 8, (byte)val);

            i0 = Unsafe.Add(ref src, 12);
            i1 = Unsafe.Add(ref src, 13);
            i2 = Unsafe.Add(ref src, 14);
            i3 = Unsafe.Add(ref src, 15);

            if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

            val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

            if (val < 0) goto fail;

            Unsafe.WriteUnaligned(b + 9, (byte)(val >> 16));
            Unsafe.WriteUnaligned(b + 10, (byte)(val >> 8));
            Unsafe.WriteUnaligned(b + 11, (byte)val);
        }

        return true;

    fail:
        id = default;
        return false;
    }

    /// <exception cref="FormatException"/>
    public static unsafe Id ParseBase64(ReadOnlySpan<char> chars)
    {
        if (chars.Length != 16) throw Ex.InvalidLengthChars(Idf.Base64, chars.Length);

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

        var id = new Id();

        var b = (byte*)&id;

        Unsafe.WriteUnaligned(b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 1, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 2, (byte)val);

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 3, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 4, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 5, (byte)val);

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 6, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 7, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 8, (byte)val);

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 9, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 10, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 11, (byte)val);

        return id;
    }

    /// <exception cref="FormatException"/>
    public static unsafe Id ParseBase64(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 16) throw Ex.InvalidLengthBytes(Idf.Base64, bytes.Length);

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

        var id = new Id();

        var b = (byte*)&id;

        Unsafe.WriteUnaligned(b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 1, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 2, (byte)val);

        i0 = Unsafe.Add(ref src, 4);
        i1 = Unsafe.Add(ref src, 5);
        i2 = Unsafe.Add(ref src, 6);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 3, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 4, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 5, (byte)val);

        i0 = Unsafe.Add(ref src, 8);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 10);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 6, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 7, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 8, (byte)val);

        i0 = Unsafe.Add(ref src, 12);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 14);
        i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(b + 9, (byte)(val >> 16));
        Unsafe.WriteUnaligned(b + 10, (byte)(val >> 8));
        Unsafe.WriteUnaligned(b + 11, (byte)val);

        return id;
    }

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseBase64(string? str, out Id id) => TryParseBase64(str.AsSpan(), out id);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id ParseBase64(string str) => ParseBase64((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

#endif
}