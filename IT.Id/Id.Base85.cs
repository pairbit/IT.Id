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
        var result = new string((char)0, 15);
        unsafe
        {
            fixed (char* resultP = result)
            fixed (char* map = Base85._byte2Char)
            {
                uint value0 = (uint)_timestamp;

                resultP[0] = map[Mod85(value0 / U85P4)];
                resultP[1] = map[Mod85(value0 / U85P3)];
                resultP[2] = map[Mod85(value0 / U85P2)];
                resultP[3] = map[Mod85(value0 / U85P1)];
                resultP[4] = map[Mod85(value0)];

                var value1 = (uint)_b;

                resultP[5] = map[Mod85(value1 / U85P4)];
                resultP[6] = map[Mod85(value1 / U85P3)];
                resultP[7] = map[Mod85(value1 / U85P2)];
                resultP[8] = map[Mod85(value1 / U85P1)];
                resultP[9] = map[Mod85(value1)];

                var value2 = (uint)_c;

                resultP[10] = map[Mod85(value2 / U85P4)];
                resultP[11] = map[Mod85(value2 / U85P3)];
                resultP[12] = map[Mod85(value2 / U85P2)];
                resultP[13] = map[Mod85(value2 / U85P1)];
                resultP[14] = map[Mod85(value2)];
            }
        }

        return result;
    }

    private unsafe void ToBase85(Span<Char> destination)
    {
        fixed (char* resultP = destination)
        fixed (char* map = Base85._byte2Char)
        {
            uint value0 = (uint)_timestamp;

            resultP[0] = map[Mod85(value0 / U85P4)];
            resultP[1] = map[Mod85(value0 / U85P3)];
            resultP[2] = map[Mod85(value0 / U85P2)];
            resultP[3] = map[Mod85(value0 / U85P1)];
            resultP[4] = map[Mod85(value0)];

            var value1 = (uint)_b;

            resultP[5] = map[Mod85(value1 / U85P4)];
            resultP[6] = map[Mod85(value1 / U85P3)];
            resultP[7] = map[Mod85(value1 / U85P2)];
            resultP[8] = map[Mod85(value1 / U85P1)];
            resultP[9] = map[Mod85(value1)];

            var value2 = (uint)_c;

            resultP[10] = map[Mod85(value2 / U85P4)];
            resultP[11] = map[Mod85(value2 / U85P3)];
            resultP[12] = map[Mod85(value2 / U85P2)];
            resultP[13] = map[Mod85(value2 / U85P1)];
            resultP[14] = map[Mod85(value2)];
        }
    }

    private unsafe void ToBase85(Span<Byte> destination)
    {
        fixed (byte* resultP = destination)
        fixed (byte* map = Base85._byte2Byte)
        {
            uint value0 = (uint)_timestamp;

            resultP[0] = map[Mod85(value0 / U85P4)];
            resultP[1] = map[Mod85(value0 / U85P3)];
            resultP[2] = map[Mod85(value0 / U85P2)];
            resultP[3] = map[Mod85(value0 / U85P1)];
            resultP[4] = map[Mod85(value0)];

            var value1 = (uint)_b;

            resultP[5] = map[Mod85(value1 / U85P4)];
            resultP[6] = map[Mod85(value1 / U85P3)];
            resultP[7] = map[Mod85(value1 / U85P2)];
            resultP[8] = map[Mod85(value1 / U85P1)];
            resultP[9] = map[Mod85(value1)];

            var value2 = (uint)_c;

            resultP[10] = map[Mod85(value2 / U85P4)];
            resultP[11] = map[Mod85(value2 / U85P3)];
            resultP[12] = map[Mod85(value2 / U85P2)];
            resultP[13] = map[Mod85(value2 / U85P1)];
            resultP[14] = map[Mod85(value2)];
        }
    }

    private static unsafe Id ParseBase85(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 15) throw new ArgumentException("The id must be 15 characters long", nameof(chars));

        fixed (char* src = chars)
        fixed (byte* map = Base85._char2Byte)
        {
            var timestamp = Decode1(map, *src) * U85P4 +
                            Decode1(map, *(src + 1)) * U85P3 +
                            Decode1(map, *(src + 2)) * U85P2 +
                            Decode1(map, *(src + 3)) * U85P1 +
                            Decode1(map, *(src + 4));

            var b = Decode1(map, *(src + 5)) * U85P4 +
                    Decode1(map, *(src + 6)) * U85P3 +
                    Decode1(map, *(src + 7)) * U85P2 +
                    Decode1(map, *(src + 8)) * U85P1 +
                    Decode1(map, *(src + 9));


            var c = Decode1(map, *(src + 10)) * U85P4 +
                    Decode1(map, *(src + 11)) * U85P3 +
                    Decode1(map, *(src + 12)) * U85P2 +
                    Decode1(map, *(src + 13)) * U85P1 +
                    Decode1(map, *(src + 14));

            return new Id((int)timestamp, (int)b, (int)c);
        }
    }

    private static unsafe Id ParseBase85(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 15) throw new ArgumentException("The id must be 15 bytes long", nameof(bytes));

        fixed (byte* src = bytes)
        fixed (byte* map = Base85._char2Byte)
        {
            var timestamp = *(map + *src) * U85P4 +
                            *(map + *(src + 1)) * U85P3 +
                            *(map + *(src + 2)) * U85P2 +
                            *(map + *(src + 3)) * U85P1 +
                            *(map + *(src + 4));

            var b = *(map + *(src + 5)) * U85P4 +
                    *(map + *(src + 6)) * U85P3 +
                    *(map + *(src + 7)) * U85P2 +
                    *(map + *(src + 8)) * U85P1 +
                    *(map + *(src + 9));

            var c = *(map + *(src + 10)) * U85P4 +
                    *(map + *(src + 11)) * U85P3 +
                    *(map + *(src + 12)) * U85P2 +
                    *(map + *(src + 13)) * U85P1 +
                    *(map + *(src + 14));

            return new Id((int)timestamp, (int)b, (int)c);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static unsafe byte Decode1(byte* map, char c) => *(map + (byte)c);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Mod85(uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}