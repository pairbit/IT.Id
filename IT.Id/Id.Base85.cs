using IT.Internal;
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT;

public readonly partial struct Id
{
    private const uint U85P1 = 85u;
    private const uint U85P2 = 85u * 85u;
    private const uint U85P3 = 85u * 85u * 85u;
    private const uint U85P4 = 85u * 85u * 85u * 85u;

    public unsafe string ToBase85()
    {
        var base85 = new string((char)0, 15);

        //fixed (byte* p = &_timestamp0)
        fixed (char* dest = base85)
        fixed (char* map = Base85.Alphabet)
        {
            //uint value0 = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref *p));
            uint value0 = (uint)(_timestamp0 << 24 | _timestamp1 << 16 | _timestamp2 << 8 | _timestamp3);

            dest[0] = map[Mod85(value0 / U85P4)];
            dest[1] = map[Mod85(value0 / U85P3)];
            dest[2] = map[Mod85(value0 / U85P2)];
            dest[3] = map[Mod85(value0 / U85P1)];
            dest[4] = map[Mod85(value0)];

            //var value1 = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref *(p + 4)));
            var value1 = (uint)(_machine0 << 24 | _machine1 << 16 | _machine2 << 8 | _pid0);

            dest[5] = map[Mod85(value1 / U85P4)];
            dest[6] = map[Mod85(value1 / U85P3)];
            dest[7] = map[Mod85(value1 / U85P2)];
            dest[8] = map[Mod85(value1 / U85P1)];
            dest[9] = map[Mod85(value1)];

            //var value2 = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref *(p + 8)));
            var value2 = (uint)(_pid1 << 24 | _increment0 << 16 | _increment1 << 8 | _increment2);

            dest[10] = map[Mod85(value2 / U85P4)];
            dest[11] = map[Mod85(value2 / U85P3)];
            dest[12] = map[Mod85(value2 / U85P2)];
            dest[13] = map[Mod85(value2 / U85P1)];
            dest[14] = map[Mod85(value2)];
        }

        return base85;
    }

    public unsafe bool TryToBase85(Span<char> chars)
    {
        if (chars.Length < 15) return false;

        fixed (char* dest = chars)
        fixed (char* map = Base85.Alphabet)
        {
            uint value0 = (uint)(_timestamp0 << 24 | _timestamp1 << 16 | _timestamp2 << 8 | _timestamp3);

            dest[0] = map[Mod85(value0 / U85P4)];
            dest[1] = map[Mod85(value0 / U85P3)];
            dest[2] = map[Mod85(value0 / U85P2)];
            dest[3] = map[Mod85(value0 / U85P1)];
            dest[4] = map[Mod85(value0)];

            var value1 = (uint)(_machine0 << 24 | _machine1 << 16 | _machine2 << 8 | _pid0);

            dest[5] = map[Mod85(value1 / U85P4)];
            dest[6] = map[Mod85(value1 / U85P3)];
            dest[7] = map[Mod85(value1 / U85P2)];
            dest[8] = map[Mod85(value1 / U85P1)];
            dest[9] = map[Mod85(value1)];

            var value2 = (uint)(_pid1 << 24 | _increment0 << 16 | _increment1 << 8 | _increment2);

            dest[10] = map[Mod85(value2 / U85P4)];
            dest[11] = map[Mod85(value2 / U85P3)];
            dest[12] = map[Mod85(value2 / U85P2)];
            dest[13] = map[Mod85(value2 / U85P1)];
            dest[14] = map[Mod85(value2)];
        }

        return true;
    }

    public unsafe bool TryToBase85(Span<byte> bytes)
    {
        if (bytes.Length < 15) return false;

        fixed (byte* dest = bytes)
        fixed (byte* map = Base85.EncodeMap)
        {
            uint value0 = (uint)(_timestamp0 << 24 | _timestamp1 << 16 | _timestamp2 << 8 | _timestamp3);

            dest[0] = map[Mod85(value0 / U85P4)];
            dest[1] = map[Mod85(value0 / U85P3)];
            dest[2] = map[Mod85(value0 / U85P2)];
            dest[3] = map[Mod85(value0 / U85P1)];
            dest[4] = map[Mod85(value0)];

            var value1 = (uint)(_machine0 << 24 | _machine1 << 16 | _machine2 << 8 | _pid0);

            dest[5] = map[Mod85(value1 / U85P4)];
            dest[6] = map[Mod85(value1 / U85P3)];
            dest[7] = map[Mod85(value1 / U85P2)];
            dest[8] = map[Mod85(value1 / U85P1)];
            dest[9] = map[Mod85(value1)];

            var value2 = (uint)(_pid1 << 24 | _increment0 << 16 | _increment1 << 8 | _increment2);

            dest[10] = map[Mod85(value2 / U85P4)];
            dest[11] = map[Mod85(value2 / U85P3)];
            dest[12] = map[Mod85(value2 / U85P2)];
            dest[13] = map[Mod85(value2 / U85P1)];
            dest[14] = map[Mod85(value2)];
        }

        return true;
    }

    public static unsafe bool TryParseBase85(ReadOnlySpan<char> chars, out Id id)
    {
        if (chars.Length != 15) goto fail;

        var map = Base85.DecodeMap;

        var ch = chars[0];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        var by = map[ch];
        if (by == 0xFF) goto fail;
        var timestamp = by * U85P4;

        ch = chars[1];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P3;

        ch = chars[2];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P2;

        ch = chars[3];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P1;

        ch = chars[4];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        timestamp += by;

        ch = chars[5];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        var b = by * U85P4;

        ch = chars[6];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        b += by * U85P3;

        ch = chars[7];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        b += by * U85P2;

        ch = chars[8];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        b += by * U85P1;

        ch = chars[9];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        b += by;

        ch = chars[10];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        var c = by * U85P4;

        ch = chars[11];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        c += by * U85P3;

        ch = chars[12];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        c += by * U85P2;

        ch = chars[13];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        c += by * U85P1;

        ch = chars[14];
        if (ch < Base85.Min || ch > Base85.Max) goto fail;
        by = map[ch];
        if (by == 0xFF) goto fail;
        c += by;

        id = new Id((int)timestamp, (int)b, (int)c);
        return true;

    fail:
        id = default;
        return false;
    }

    public static unsafe bool TryParseBase85(ReadOnlySpan<byte> bytes, out Id id)
    {
        if (bytes.Length != 15) goto fail;

        var map = Base85.DecodeMap;

        var by = map[bytes[0]];
        if (by == 0xFF) goto fail;
        var timestamp = by * U85P4;

        by = map[bytes[1]];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P3;

        by = map[bytes[2]];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P2;

        by = map[bytes[3]];
        if (by == 0xFF) goto fail;
        timestamp += by * U85P1;

        by = map[bytes[4]];
        if (by == 0xFF) goto fail;
        timestamp += by;

        by = map[bytes[5]];
        if (by == 0xFF) goto fail;
        var b = by * U85P4;

        by = map[bytes[6]];
        if (by == 0xFF) goto fail;
        b += by * U85P3;

        by = map[bytes[7]];
        if (by == 0xFF) goto fail;
        b += by * U85P2;

        by = map[bytes[8]];
        if (by == 0xFF) goto fail;
        b += by * U85P1;

        by = map[bytes[9]];
        if (by == 0xFF) goto fail;
        b += by;

        by = map[bytes[10]];
        if (by == 0xFF) goto fail;
        var c = by * U85P4;

        by = map[bytes[11]];
        if (by == 0xFF) goto fail;
        c += by * U85P3;

        by = map[bytes[12]];
        if (by == 0xFF) goto fail;
        c += by * U85P2;

        by = map[bytes[13]];
        if (by == 0xFF) goto fail;
        c += by * U85P1;

        by = map[bytes[14]];
        if (by == 0xFF) goto fail;
        c += by;

        id = new Id((int)timestamp, (int)b, (int)c);
        return true;

    fail:
        id = default;
        return false;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseBase85(ReadOnlySpan<char> chars)
    {
        if (chars.Length != 15) throw Ex.InvalidLengthChars(Idf.Base85, chars.Length);

        ref byte map = ref Base85.DecodeMap[0];

        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, BinaryPrimitives.ReverseEndianness(
              Map85(ref map, chars[0]) * U85P4 +
              Map85(ref map, chars[1]) * U85P3 +
              Map85(ref map, chars[2]) * U85P2 +
              Map85(ref map, chars[3]) * U85P1 +
              Map85(ref map, chars[4])));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), BinaryPrimitives.ReverseEndianness(
              Map85(ref map, chars[5]) * U85P4 +
              Map85(ref map, chars[6]) * U85P3 +
              Map85(ref map, chars[7]) * U85P2 +
              Map85(ref map, chars[8]) * U85P1 +
              Map85(ref map, chars[9])));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), BinaryPrimitives.ReverseEndianness(
              Map85(ref map, chars[10]) * U85P4 +
              Map85(ref map, chars[11]) * U85P3 +
              Map85(ref map, chars[12]) * U85P2 +
              Map85(ref map, chars[13]) * U85P1 +
              Map85(ref map, chars[14])));

        return id;
    }

    /// <exception cref="FormatException"/>
    public static unsafe Id ParseBase85(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 15) throw Ex.InvalidLengthBytes(Idf.Base85, bytes.Length);

        var map = Base85.DecodeMap;

        var by = map[bytes[0]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[0]);
        var timestamp = by * U85P4;

        by = map[bytes[1]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[1]);
        timestamp += by * U85P3;

        by = map[bytes[2]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[2]);
        timestamp += by * U85P2;

        by = map[bytes[3]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[3]);
        timestamp += by * U85P1;

        by = map[bytes[4]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[4]);
        timestamp += by;

        by = map[bytes[5]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[5]);
        var b = by * U85P4;

        by = map[bytes[6]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[6]);
        b += by * U85P3;

        by = map[bytes[7]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[7]);
        b += by * U85P2;

        by = map[bytes[8]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[8]);
        b += by * U85P1;

        by = map[bytes[9]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[9]);
        b += by;

        by = map[bytes[10]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[10]);
        var c = by * U85P4;

        by = map[bytes[11]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[11]);
        c += by * U85P3;

        by = map[bytes[12]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[12]);
        c += by * U85P2;

        by = map[bytes[13]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[13]);
        c += by * U85P1;

        by = map[bytes[14]];
        if (by == 0xFF) throw Ex.InvalidByte(Idf.Base85, bytes[14]);
        c += by;

        return new Id((int)timestamp, (int)b, (int)c);
    }

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseBase85(String? str, out Id id) => TryParseBase85(str.AsSpan(), out id);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id ParseBase85(String str) => ParseBase85((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe byte Map85(byte* map, char c)
    {
        if (c < Base85.Min || c > Base85.Max) throw Ex.InvalidChar(Idf.Base85, c);

        var value = *(map + (byte)c);

        if (value == 0xFF) throw Ex.InvalidChar(Idf.Base85, c);

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte Map85(ref byte map, char c)
    {
        if (c < Base85.Min || c > Base85.Max) throw Ex.InvalidChar(Idf.Base85, c);

        var value = Unsafe.Add(ref map, (byte)c);

        if (value == 0xFF) throw Ex.InvalidChar(Idf.Base85, c);

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Mod85(uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}