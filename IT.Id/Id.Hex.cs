namespace System;

public readonly partial struct Id
{
    private String ToHexLower()
    {
        var result = new string((char)0, 24);
        unsafe
        {
            var lookupP = Hex._lowerLookup32UnsafeP;
            fixed (char* resultP = result)
            {
                uint* resultP2 = (uint*)resultP;
                resultP2[0] = lookupP[(byte)(_timestamp >> 24)];
                resultP2[1] = lookupP[(byte)(_timestamp >> 16)];
                resultP2[2] = lookupP[(byte)(_timestamp >> 8)];
                resultP2[3] = lookupP[(byte)(_timestamp)];
                resultP2[4] = lookupP[(byte)(_b >> 24)];
                resultP2[5] = lookupP[(byte)(_b >> 16)];
                resultP2[6] = lookupP[(byte)(_b >> 8)];
                resultP2[7] = lookupP[(byte)(_b)];
                resultP2[8] = lookupP[(byte)(_c >> 24)];
                resultP2[9] = lookupP[(byte)(_c >> 16)];
                resultP2[10] = lookupP[(byte)(_c >> 8)];
                resultP2[11] = lookupP[(byte)(_c)];
            }
        }
        return result;
    }

    private String ToHexUpper()
    {
        var result = new string((char)0, 24);
        unsafe
        {
            var lookupP = Hex._upperLookup32UnsafeP;
            fixed (char* resultP = result)
            {
                uint* resultP2 = (uint*)resultP;
                resultP2[0] = lookupP[(byte)(_timestamp >> 24)];
                resultP2[1] = lookupP[(byte)(_timestamp >> 16)];
                resultP2[2] = lookupP[(byte)(_timestamp >> 8)];
                resultP2[3] = lookupP[(byte)(_timestamp)];
                resultP2[4] = lookupP[(byte)(_b >> 24)];
                resultP2[5] = lookupP[(byte)(_b >> 16)];
                resultP2[6] = lookupP[(byte)(_b >> 8)];
                resultP2[7] = lookupP[(byte)(_b)];
                resultP2[8] = lookupP[(byte)(_c >> 24)];
                resultP2[9] = lookupP[(byte)(_c >> 16)];
                resultP2[10] = lookupP[(byte)(_c >> 8)];
                resultP2[11] = lookupP[(byte)(_c)];
            }
        }
        return result;
    }

    private unsafe void ToHex(Span<Char> destination, uint* lookupP)
    {
        fixed (char* resultP = destination)
        {
            uint* resultP2 = (uint*)resultP;
            resultP2[0] = lookupP[(byte)(_timestamp >> 24)];
            resultP2[1] = lookupP[(byte)(_timestamp >> 16)];
            resultP2[2] = lookupP[(byte)(_timestamp >> 8)];
            resultP2[3] = lookupP[(byte)(_timestamp)];
            resultP2[4] = lookupP[(byte)(_b >> 24)];
            resultP2[5] = lookupP[(byte)(_b >> 16)];
            resultP2[6] = lookupP[(byte)(_b >> 8)];
            resultP2[7] = lookupP[(byte)(_b)];
            resultP2[8] = lookupP[(byte)(_c >> 24)];
            resultP2[9] = lookupP[(byte)(_c >> 16)];
            resultP2[10] = lookupP[(byte)(_c >> 8)];
            resultP2[11] = lookupP[(byte)(_c)];
        }
    }

    private unsafe void ToHex(Span<Byte> destination, ushort* lookupP)
    {
        fixed (byte* resultP = destination)
        {
            ushort* resultP2 = (ushort*)resultP;
            resultP2[0] = lookupP[(byte)(_timestamp >> 24)];
            resultP2[1] = lookupP[(byte)(_timestamp >> 16)];
            resultP2[2] = lookupP[(byte)(_timestamp >> 8)];
            resultP2[3] = lookupP[(byte)(_timestamp)];
            resultP2[4] = lookupP[(byte)(_b >> 24)];
            resultP2[5] = lookupP[(byte)(_b >> 16)];
            resultP2[6] = lookupP[(byte)(_b >> 8)];
            resultP2[7] = lookupP[(byte)(_b)];
            resultP2[8] = lookupP[(byte)(_c >> 24)];
            resultP2[9] = lookupP[(byte)(_c >> 16)];
            resultP2[10] = lookupP[(byte)(_c >> 8)];
            resultP2[11] = lookupP[(byte)(_c)];
        }
    }

    private static Id ParseHex(ReadOnlySpan<Char> value)
    {
        if (value.Length != 24) throw new ArgumentException("String must be 24 characters long", nameof(value));

        var map = Hex._charToHex;

        var b0 = (byte)((map[value[0]] << 4) | map[value[1]]);
        var b1 = (byte)((map[value[2]] << 4) | map[value[3]]);
        var b2 = (byte)((map[value[4]] << 4) | map[value[5]]);
        var b3 = (byte)((map[value[6]] << 4) | map[value[7]]);
        var b4 = (byte)((map[value[8]] << 4) | map[value[9]]);
        var b5 = (byte)((map[value[10]] << 4) | map[value[11]]);
        var b6 = (byte)((map[value[12]] << 4) | map[value[13]]);
        var b7 = (byte)((map[value[14]] << 4) | map[value[15]]);
        var b8 = (byte)((map[value[16]] << 4) | map[value[17]]);
        var b9 = (byte)((map[value[18]] << 4) | map[value[19]]);
        var b10 = (byte)((map[value[20]] << 4) | map[value[21]]);
        var b11 = (byte)((map[value[22]] << 4) | map[value[23]]);

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }

    private static Id ParseHex(ReadOnlySpan<Byte> value)
    {
        if (value.Length != 24) throw new ArgumentException("String must be 24 characters long", nameof(value));

        var map = Hex._charToHex;

        var b0 = (byte)((map[value[0]] << 4) | map[value[1]]);
        var b1 = (byte)((map[value[2]] << 4) | map[value[3]]);
        var b2 = (byte)((map[value[4]] << 4) | map[value[5]]);
        var b3 = (byte)((map[value[6]] << 4) | map[value[7]]);
        var b4 = (byte)((map[value[8]] << 4) | map[value[9]]);
        var b5 = (byte)((map[value[10]] << 4) | map[value[11]]);
        var b6 = (byte)((map[value[12]] << 4) | map[value[13]]);
        var b7 = (byte)((map[value[14]] << 4) | map[value[15]]);
        var b8 = (byte)((map[value[16]] << 4) | map[value[17]]);
        var b9 = (byte)((map[value[18]] << 4) | map[value[19]]);
        var b10 = (byte)((map[value[20]] << 4) | map[value[21]]);
        var b11 = (byte)((map[value[22]] << 4) | map[value[23]]);

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }
}