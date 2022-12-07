using IT.Internal;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id
{
    private const uint U85P1 = 85u;
    private const uint U85P2 = 85u * 85u;
    private const uint U85P3 = 85u * 85u * 85u;
    private const uint U85P4 = 85u * 85u * 85u * 85u;

    private String ToBase85()
    {
        var base85 = new string((char)0, 15);
        unsafe
        {
            fixed (char* dest = base85)
            fixed (char* map = Base85.Alphabet)
            {
                uint value0 = (uint)_timestamp;

                dest[0] = map[Mod85(value0 / U85P4)];
                dest[1] = map[Mod85(value0 / U85P3)];
                dest[2] = map[Mod85(value0 / U85P2)];
                dest[3] = map[Mod85(value0 / U85P1)];
                dest[4] = map[Mod85(value0)];

                var value1 = (uint)_b;

                dest[5] = map[Mod85(value1 / U85P4)];
                dest[6] = map[Mod85(value1 / U85P3)];
                dest[7] = map[Mod85(value1 / U85P2)];
                dest[8] = map[Mod85(value1 / U85P1)];
                dest[9] = map[Mod85(value1)];

                var value2 = (uint)_c;

                dest[10] = map[Mod85(value2 / U85P4)];
                dest[11] = map[Mod85(value2 / U85P3)];
                dest[12] = map[Mod85(value2 / U85P2)];
                dest[13] = map[Mod85(value2 / U85P1)];
                dest[14] = map[Mod85(value2)];
            }
        }
        return base85;
    }

    private unsafe void ToBase85(Span<Char> chars)
    {
        fixed (char* dest = chars)
        fixed (char* map = Base85.Alphabet)
        {
            uint value0 = (uint)_timestamp;

            dest[0] = map[Mod85(value0 / U85P4)];
            dest[1] = map[Mod85(value0 / U85P3)];
            dest[2] = map[Mod85(value0 / U85P2)];
            dest[3] = map[Mod85(value0 / U85P1)];
            dest[4] = map[Mod85(value0)];

            var value1 = (uint)_b;

            dest[5] = map[Mod85(value1 / U85P4)];
            dest[6] = map[Mod85(value1 / U85P3)];
            dest[7] = map[Mod85(value1 / U85P2)];
            dest[8] = map[Mod85(value1 / U85P1)];
            dest[9] = map[Mod85(value1)];

            var value2 = (uint)_c;

            dest[10] = map[Mod85(value2 / U85P4)];
            dest[11] = map[Mod85(value2 / U85P3)];
            dest[12] = map[Mod85(value2 / U85P2)];
            dest[13] = map[Mod85(value2 / U85P1)];
            dest[14] = map[Mod85(value2)];
        }
    }

    private unsafe void ToBase85(Span<Byte> bytes)
    {
        fixed (byte* dest = bytes)
        fixed (byte* map = Base85.EncodeMap)
        {
            uint value0 = (uint)_timestamp;

            dest[0] = map[Mod85(value0 / U85P4)];
            dest[1] = map[Mod85(value0 / U85P3)];
            dest[2] = map[Mod85(value0 / U85P2)];
            dest[3] = map[Mod85(value0 / U85P1)];
            dest[4] = map[Mod85(value0)];

            var value1 = (uint)_b;

            dest[5] = map[Mod85(value1 / U85P4)];
            dest[6] = map[Mod85(value1 / U85P3)];
            dest[7] = map[Mod85(value1 / U85P2)];
            dest[8] = map[Mod85(value1 / U85P1)];
            dest[9] = map[Mod85(value1)];

            var value2 = (uint)_c;

            dest[10] = map[Mod85(value2 / U85P4)];
            dest[11] = map[Mod85(value2 / U85P3)];
            dest[12] = map[Mod85(value2 / U85P2)];
            dest[13] = map[Mod85(value2 / U85P1)];
            dest[14] = map[Mod85(value2)];
        }
    }

    private static unsafe bool TryParseBase85(ReadOnlySpan<Char> chars, out Id id)
    {
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

    private static unsafe bool TryParseBase85(ReadOnlySpan<Byte> bytes, out Id id)
    {
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

    private static unsafe Id ParseBase85(ReadOnlySpan<Char> chars)
    {
        fixed (char* src = chars)
        fixed (byte* map = Base85.DecodeMap)
        {
            var timestamp = Map85(map, *src) * U85P4 +
                            Map85(map, *(src + 1)) * U85P3 +
                            Map85(map, *(src + 2)) * U85P2 +
                            Map85(map, *(src + 3)) * U85P1 +
                            Map85(map, *(src + 4));

            var b = Map85(map, *(src + 5)) * U85P4 +
                    Map85(map, *(src + 6)) * U85P3 +
                    Map85(map, *(src + 7)) * U85P2 +
                    Map85(map, *(src + 8)) * U85P1 +
                    Map85(map, *(src + 9));


            var c = Map85(map, *(src + 10)) * U85P4 +
                    Map85(map, *(src + 11)) * U85P3 +
                    Map85(map, *(src + 12)) * U85P2 +
                    Map85(map, *(src + 13)) * U85P1 +
                    Map85(map, *(src + 14));

            return new Id((int)timestamp, (int)b, (int)c);
        }
    }

    private static unsafe Id ParseBase85(ReadOnlySpan<Byte> bytes)
    {
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe byte Map85(byte* map, char c)
    {
        if (c < Base85.Min || c > Base85.Max) throw Ex.InvalidChar(Idf.Base85, c);

        var value = *(map + (byte)c);

        if (value == 0xFF) throw Ex.InvalidChar(Idf.Base85, c);

        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Mod85(uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}