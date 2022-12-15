using IT.Internal;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT;

public readonly partial struct Id
{
    public unsafe string ToBase64Path3(char separator = DirectorySeparatorChar)
    {
        var path3 = new string((char)0, 19);

        fixed (char* dest = path3)
        fixed (char* map = Base64.tableUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[18] = map[(byte0 & 0xfc) >> 2];
            dest[17] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[16] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[15] = map[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[14] = map[(byte3 & 0xfc) >> 2];
            dest[13] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[12] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[11] = map[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[10] = map[(byte6 & 0xfc) >> 2];
            dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[8] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[7] = map[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[6] = map[(byte9 & 0xfc) >> 2];
            dest[5] = separator;
            dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[3] = separator;
            dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[1] = separator;
            dest[0] = map[byte11 & 0x3f];
        }
        return path3;
    }

    public unsafe bool TryToBase64Path3(Span<Char> chars, char separator = DirectorySeparatorChar)
    {
        if (chars.Length < 19) return false;

        fixed (char* dest = chars)
        fixed (char* map = Base64.tableUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[18] = map[(byte0 & 0xfc) >> 2];
            dest[17] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[16] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[15] = map[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[14] = map[(byte3 & 0xfc) >> 2];
            dest[13] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[12] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[11] = map[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[10] = map[(byte6 & 0xfc) >> 2];
            dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[8] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[7] = map[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[6] = map[(byte9 & 0xfc) >> 2];
            dest[5] = separator;
            dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[3] = separator;
            dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[1] = separator;
            dest[0] = map[byte11 & 0x3f];
        }

        return true;
    }

    public unsafe bool TryToBase64Path3(Span<Byte> bytes, byte separator = DirectorySeparatorByte)
    {
        if (bytes.Length < 19) return false;

        fixed (byte* dest = bytes)
        fixed (byte* map = Base64.bytesUrl)
        {
            var byte0 = _timestamp0;
            var byte1 = _timestamp1;
            var byte2 = _timestamp2;

            dest[18] = map[(byte0 & 0xfc) >> 2];
            dest[17] = map[((byte0 & 0x03) << 4) | ((byte1 & 0xf0) >> 4)];
            dest[16] = map[((byte1 & 0x0f) << 2) | ((byte2 & 0xc0) >> 6)];
            dest[15] = map[byte2 & 0x3f];

            var byte3 = _timestamp3;
            var byte4 = _machine0;
            var byte5 = _machine1;

            dest[14] = map[(byte3 & 0xfc) >> 2];
            dest[13] = map[((byte3 & 0x03) << 4) | ((byte4 & 0xf0) >> 4)];
            dest[12] = map[((byte4 & 0x0f) << 2) | ((byte5 & 0xc0) >> 6)];
            dest[11] = map[byte5 & 0x3f];

            var byte6 = _machine2;
            var byte7 = _pid0;
            var byte8 = _pid1;

            dest[10] = map[(byte6 & 0xfc) >> 2];
            dest[9] = map[((byte6 & 0x03) << 4) | ((byte7 & 0xf0) >> 4)];
            dest[8] = map[((byte7 & 0x0f) << 2) | ((byte8 & 0xc0) >> 6)];
            dest[7] = map[byte8 & 0x3f];

            var byte9 = _increment0;
            var byte10 = _increment1;
            var byte11 = _increment2;

            dest[6] = map[(byte9 & 0xfc) >> 2];
            dest[5] = separator;
            dest[4] = map[((byte9 & 0x03) << 4) | ((byte10 & 0xf0) >> 4)];
            dest[3] = separator;
            dest[2] = map[((byte10 & 0x0f) << 2) | ((byte11 & 0xc0) >> 6)];
            dest[1] = separator;
            dest[0] = map[byte11 & 0x3f];
        }

        return true;
    }

    public static bool TryParseBase64Path3(ReadOnlySpan<Char> chars, out Id id)
    {
        if (chars.Length != 19) goto fail;

        var sep = chars[1];
        if (sep != '\\' && sep != '/') goto fail;

        sep = chars[3];
        if (sep != '\\' && sep != '/') goto fail;

        sep = chars[5];
        if (sep != '\\' && sep != '/') goto fail;

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 18);
        int i1 = Unsafe.Add(ref src, 17);
        int i2 = Unsafe.Add(ref src, 16);
        int i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)val);

        i0 = Unsafe.Add(ref src, 14);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 12);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)val);

        i0 = Unsafe.Add(ref src, 10);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 8);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)val);

        i0 = Unsafe.Add(ref src, 6);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)val);

        return true;

    fail:
        id = default;
        return false;
    }

    public static bool TryParseBase64Path3(ReadOnlySpan<Byte> bytes, out Id id)
    {
        if (bytes.Length != 19) goto fail;

        var sep = bytes[1];
        if (sep != '\\' && sep != '/') goto fail;

        sep = bytes[3];
        if (sep != '\\' && sep != '/') goto fail;

        sep = bytes[5];
        if (sep != '\\' && sep != '/') goto fail;

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 18);
        int i1 = Unsafe.Add(ref src, 17);
        int i2 = Unsafe.Add(ref src, 16);
        int i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)val);

        i0 = Unsafe.Add(ref src, 14);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 12);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)val);

        i0 = Unsafe.Add(ref src, 10);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 8);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)val);

        i0 = Unsafe.Add(ref src, 6);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) goto fail;

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) goto fail;

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)val);

        return true;

    fail:
        id = default;
        return false;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseBase64Path3(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 19) throw Ex.InvalidLengthChars(Idf.Base64Path3, chars.Length);

        var sep = chars[1];
        if (sep != '\\' && sep != '/') throw Ex.InvalidCharIndex(Idf.Base64Path3, 1, sep, '/', '\\');

        sep = chars[3];
        if (sep != '\\' && sep != '/') throw Ex.InvalidCharIndex(Idf.Base64Path3, 3, sep, '/', '\\');

        sep = chars[5];
        if (sep != '\\' && sep != '/') throw Ex.InvalidCharIndex(Idf.Base64Path3, 5, sep, '/', '\\');

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref char src = ref MemoryMarshal.GetReference(chars);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 18);
        int i1 = Unsafe.Add(ref src, 17);
        int i2 = Unsafe.Add(ref src, 16);
        int i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)val);

        i0 = Unsafe.Add(ref src, 14);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 12);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)val);

        i0 = Unsafe.Add(ref src, 10);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 8);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)val);

        i0 = Unsafe.Add(ref src, 6);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidChar(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)val);

        return id;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseBase64Path3(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 19) throw Ex.InvalidLengthBytes(Idf.Base64Path3, bytes.Length);

        var sep = bytes[1];
        if (sep != '\\' && sep != '/') throw Ex.InvalidByteIndex(Idf.Base64Path3, 1, sep, '/', '\\');

        sep = bytes[3];
        if (sep != '\\' && sep != '/') throw Ex.InvalidByteIndex(Idf.Base64Path3, 3, sep, '/', '\\');

        sep = bytes[5];
        if (sep != '\\' && sep != '/') throw Ex.InvalidByteIndex(Idf.Base64Path3, 5, sep, '/', '\\');

        ReadOnlySpan<sbyte> mapSpan = Base64.DecodeMap;
        ref byte src = ref MemoryMarshal.GetReference(bytes);
        ref sbyte map = ref MemoryMarshal.GetReference(mapSpan);

        int i0 = Unsafe.Add(ref src, 18);
        int i1 = Unsafe.Add(ref src, 17);
        int i2 = Unsafe.Add(ref src, 16);
        int i3 = Unsafe.Add(ref src, 15);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        var val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)val);

        i0 = Unsafe.Add(ref src, 14);
        i1 = Unsafe.Add(ref src, 13);
        i2 = Unsafe.Add(ref src, 12);
        i3 = Unsafe.Add(ref src, 11);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)val);

        i0 = Unsafe.Add(ref src, 10);
        i1 = Unsafe.Add(ref src, 9);
        i2 = Unsafe.Add(ref src, 8);
        i3 = Unsafe.Add(ref src, 7);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)val);

        i0 = Unsafe.Add(ref src, 6);
        i1 = Unsafe.Add(ref src, 4);
        i2 = Unsafe.Add(ref src, 2);
        i3 = Unsafe.Add(ref src, 0);

        if (((i0 | i1 | i2 | i3) & 0xffffff00) != 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        val = (Unsafe.Add(ref map, i0) << 18) | (Unsafe.Add(ref map, i1) << 12) | Unsafe.Add(ref map, i2) << 6 | (int)Unsafe.Add(ref map, i3);

        if (val < 0) throw Ex.InvalidByte(Idf.Base64Path3, i0, i1, i2, i3);

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)(val >> 16));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)(val >> 8));
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)val);

        return id;
    }

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseBase64Path3(string? str, out Id id) => TryParseBase64Path3(str.AsSpan(), out id);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id ParseBase64Path3(string str) => ParseBase64Path3((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

#endif
}