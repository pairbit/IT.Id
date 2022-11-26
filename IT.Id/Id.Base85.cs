using Internal;
using System.Runtime.CompilerServices;

namespace System;

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

    private static unsafe Id ParseBase85(ReadOnlySpan<Char> chars)
    {
        fixed (char* src = chars)
        fixed (sbyte* map = Base85.DecodeMap)
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
        fixed (byte* src = bytes)
        fixed (sbyte* map = Base85.DecodeMap)
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe byte Map85(sbyte* map, int c)
    {
        if (c < Base85.Min || c > Base85.Max) throw Ex.InvalidChar(Idf.Base85, c);

        var value = *(map + (byte)c);

        if (value == -1) throw Ex.InvalidChar(Idf.Base85, c);

        return (byte)value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Mod85(uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}