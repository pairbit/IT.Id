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

    private static Id ParseHex(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 24) throw new ArgumentException("The id must be 24 characters long", nameof(chars));

        var map = Hex._charToHex;

        var b0 = (byte)((map[chars[0]] << 4) | map[chars[1]]);
        var b1 = (byte)((map[chars[2]] << 4) | map[chars[3]]);
        var b2 = (byte)((map[chars[4]] << 4) | map[chars[5]]);
        var b3 = (byte)((map[chars[6]] << 4) | map[chars[7]]);
        var b4 = (byte)((map[chars[8]] << 4) | map[chars[9]]);
        var b5 = (byte)((map[chars[10]] << 4) | map[chars[11]]);
        var b6 = (byte)((map[chars[12]] << 4) | map[chars[13]]);
        var b7 = (byte)((map[chars[14]] << 4) | map[chars[15]]);
        var b8 = (byte)((map[chars[16]] << 4) | map[chars[17]]);
        var b9 = (byte)((map[chars[18]] << 4) | map[chars[19]]);
        var b10 = (byte)((map[chars[20]] << 4) | map[chars[21]]);
        var b11 = (byte)((map[chars[22]] << 4) | map[chars[23]]);

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }

    private static Id ParseHex(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 24) throw new ArgumentException("The id must be 24 bytes long", nameof(bytes));

        var map = Hex._charToHex;

        var b0 = (byte)((map[bytes[0]] << 4) | map[bytes[1]]);
        var b1 = (byte)((map[bytes[2]] << 4) | map[bytes[3]]);
        var b2 = (byte)((map[bytes[4]] << 4) | map[bytes[5]]);
        var b3 = (byte)((map[bytes[6]] << 4) | map[bytes[7]]);
        var b4 = (byte)((map[bytes[8]] << 4) | map[bytes[9]]);
        var b5 = (byte)((map[bytes[10]] << 4) | map[bytes[11]]);
        var b6 = (byte)((map[bytes[12]] << 4) | map[bytes[13]]);
        var b7 = (byte)((map[bytes[14]] << 4) | map[bytes[15]]);
        var b8 = (byte)((map[bytes[16]] << 4) | map[bytes[17]]);
        var b9 = (byte)((map[bytes[18]] << 4) | map[bytes[19]]);
        var b10 = (byte)((map[bytes[20]] << 4) | map[bytes[21]]);
        var b11 = (byte)((map[bytes[22]] << 4) | map[bytes[23]]);

        var timestamp = b0 << 24 | b1 << 16 | b2 << 8 | b3;
        var b = b4 << 24 | b5 << 16 | b6 << 8 | b7;
        var c = b8 << 24 | b9 << 16 | b10 << 8 | b11;

        return new Id(timestamp, b, c);
    }
}