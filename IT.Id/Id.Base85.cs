using System.Runtime.CompilerServices;

namespace System;

public readonly partial struct Id
{
    private String ToBase85()
    {
        var result = new string((char)0, 15);
        unsafe
        {
            fixed (char* resultP = result)
            fixed (char* map = Base85._byte2Char)
            {
                uint value0 = (uint)_timestamp;

                resultP[0] = map[Mod85(value0 / Base85.U85P4)];
                resultP[1] = map[Mod85(value0 / Base85.U85P3)];
                resultP[2] = map[Mod85(value0 / Base85.U85P2)];
                resultP[3] = map[Mod85(value0 / Base85.U85P1)];
                resultP[4] = map[Mod85(value0)];

                var value1 = (uint)_b;

                resultP[5] = map[Mod85(value1 / Base85.U85P4)];
                resultP[6] = map[Mod85(value1 / Base85.U85P3)];
                resultP[7] = map[Mod85(value1 / Base85.U85P2)];
                resultP[8] = map[Mod85(value1 / Base85.U85P1)];
                resultP[9] = map[Mod85(value1)];

                var value2 = (uint)_c;

                resultP[10] = map[Mod85(value2 / Base85.U85P4)];
                resultP[11] = map[Mod85(value2 / Base85.U85P3)];
                resultP[12] = map[Mod85(value2 / Base85.U85P2)];
                resultP[13] = map[Mod85(value2 / Base85.U85P1)];
                resultP[14] = map[Mod85(value2)];
            }
        }

        return result;
    }

    private void ToBase85(Span<Char> destination)
    {
        Base85.Encode(ToByteArray(), destination);
    }

    private static Id ParseBase85(ReadOnlySpan<Char> value)
    {
        if (value.Length != 15) throw new ArgumentException("String must be 15 characters long", nameof(value));

        Span<Byte> bytes = stackalloc Byte[12];

        Base85.Decode(value, bytes);

        FromByteArray(bytes, 0, out var timestamp, out var b, out var c);

        return new Id(timestamp, b, c);
    }

    private static Id ParseBase85(ReadOnlySpan<Byte> value)
    {
        if (value.Length != 15) throw new ArgumentException("String must be 15 characters long", nameof(value));

        Span<Byte> bytes = stackalloc Byte[12];

        Base85.Decode(value, bytes);

        FromByteArray(bytes, 0, out var timestamp, out var b, out var c);

        return new Id(timestamp, b, c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Mod85(uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}