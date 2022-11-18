namespace System;

public readonly partial struct Id
{
    private String ToBase32()
    {
        var result = new string((char)0, 20);

        unsafe
        {
            fixed (char* resultP = result)
            fixed (char* map = Base32.ALPHABET)
            {
                ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

                resultP[0] = map[value >> 35];
                resultP[1] = map[(value >> 30) & 0x1F];
                resultP[2] = map[(value >> 25) & 0x1F];
                resultP[3] = map[(value >> 20) & 0x1F];
                resultP[4] = map[(value >> 15) & 0x1F];
                resultP[5] = map[(value >> 10) & 0x1F];
                resultP[6] = map[(value >> 5) & 0x1F];
                resultP[7] = map[value & 0x1F];

                value = (byte)(_b >> 16);
                value = (value << 8) | (byte)(_b >> 8);
                value = (value << 8) | (byte)_b;
                value = (value << 8) | (byte)(_c >> 24);
                value = (value << 8) | (byte)(_c >> 16);

                resultP[8] = map[value >> 35];
                resultP[9] = map[(value >> 30) & 0x1F];
                resultP[10] = map[(value >> 25) & 0x1F];
                resultP[11] = map[(value >> 20) & 0x1F];
                resultP[12] = map[(value >> 15) & 0x1F];
                resultP[13] = map[(value >> 10) & 0x1F];
                resultP[14] = map[(value >> 5) & 0x1F];
                resultP[15] = map[value & 0x1F];

                value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

                resultP[16] = map[value >> 15];
                resultP[17] = map[(value >> 10) & 0x1F];
                resultP[18] = map[(value >> 5) & 0x1F];
                resultP[19] = map[value & 0x1F];
            }
        }

        return result;
    }

    private unsafe void ToBase32(Span<Char> destination)
    {
        fixed (char* resultP = destination)
        fixed (char* map = Base32.ALPHABET)
        {
            ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            resultP[0] = map[value >> 35];
            resultP[1] = map[(value >> 30) & 0x1F];
            resultP[2] = map[(value >> 25) & 0x1F];
            resultP[3] = map[(value >> 20) & 0x1F];
            resultP[4] = map[(value >> 15) & 0x1F];
            resultP[5] = map[(value >> 10) & 0x1F];
            resultP[6] = map[(value >> 5) & 0x1F];
            resultP[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            resultP[8] = map[value >> 35];
            resultP[9] = map[(value >> 30) & 0x1F];
            resultP[10] = map[(value >> 25) & 0x1F];
            resultP[11] = map[(value >> 20) & 0x1F];
            resultP[12] = map[(value >> 15) & 0x1F];
            resultP[13] = map[(value >> 10) & 0x1F];
            resultP[14] = map[(value >> 5) & 0x1F];
            resultP[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            resultP[16] = map[value >> 15];
            resultP[17] = map[(value >> 10) & 0x1F];
            resultP[18] = map[(value >> 5) & 0x1F];
            resultP[19] = map[value & 0x1F];
        }
    }

    private unsafe void ToBase32(Span<Byte> destination)
    {
        fixed (byte* resultP = destination)
        fixed (byte* map = Base32.Bytes)
        {
            ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            resultP[0] = map[value >> 35];
            resultP[1] = map[(value >> 30) & 0x1F];
            resultP[2] = map[(value >> 25) & 0x1F];
            resultP[3] = map[(value >> 20) & 0x1F];
            resultP[4] = map[(value >> 15) & 0x1F];
            resultP[5] = map[(value >> 10) & 0x1F];
            resultP[6] = map[(value >> 5) & 0x1F];
            resultP[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            resultP[8] = map[value >> 35];
            resultP[9] = map[(value >> 30) & 0x1F];
            resultP[10] = map[(value >> 25) & 0x1F];
            resultP[11] = map[(value >> 20) & 0x1F];
            resultP[12] = map[(value >> 15) & 0x1F];
            resultP[13] = map[(value >> 10) & 0x1F];
            resultP[14] = map[(value >> 5) & 0x1F];
            resultP[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            resultP[16] = map[value >> 15];
            resultP[17] = map[(value >> 10) & 0x1F];
            resultP[18] = map[(value >> 5) & 0x1F];
            resultP[19] = map[value & 0x1F];
        }
    }

    private static unsafe Id ParseBase32(ReadOnlySpan<Char> encoded)
    {
        if (encoded.Length != 20) throw new ArgumentException("String must be 20 characters long", nameof(encoded));

        fixed (char* pEncoded = encoded)
        {
            ulong value = Base32.GetByte(pEncoded[0]);
            value = (value << 5) | Base32.GetByte(pEncoded[1]);
            value = (value << 5) | Base32.GetByte(pEncoded[2]);
            value = (value << 5) | Base32.GetByte(pEncoded[3]);
            value = (value << 5) | Base32.GetByte(pEncoded[4]);
            value = (value << 5) | Base32.GetByte(pEncoded[5]);
            value = (value << 5) | Base32.GetByte(pEncoded[6]);
            value = (value << 5) | Base32.GetByte(pEncoded[7]);

            var timestamp = (byte)(value >> 32) << 24 | (byte)(value >> 24) << 16 | (byte)(value >> 16) << 8 | (byte)(value >> 8);

            var b = (int)(byte)value;

            value = (value << 5) | Base32.GetByte(pEncoded[8]);
            value = (value << 5) | Base32.GetByte(pEncoded[9]);
            value = (value << 5) | Base32.GetByte(pEncoded[10]);
            value = (value << 5) | Base32.GetByte(pEncoded[11]);
            value = (value << 5) | Base32.GetByte(pEncoded[12]);
            value = (value << 5) | Base32.GetByte(pEncoded[13]);
            value = (value << 5) | Base32.GetByte(pEncoded[14]);
            value = (value << 5) | Base32.GetByte(pEncoded[15]);

            b = b << 24 | (byte)(value >> 32) << 16 | (byte)(value >> 24) << 8 | (byte)(value >> 16);

            var c = (byte)(value >> 8) << 24 | (byte)value << 16;

            value = Base32.GetByte(pEncoded[16]);
            value = (value << 5) | Base32.GetByte(pEncoded[17]);
            value = (value << 5) | Base32.GetByte(pEncoded[18]);
            value = (value << 5) | Base32.GetByte(pEncoded[19]);

            c |= (byte)(value >> 12) << 8 | (byte)(value >> 4);

            return new Id(timestamp, b, c);
        }
    }

    private static unsafe Id ParseBase32(ReadOnlySpan<Byte> encoded)
    {
        if (encoded.Length != 20) throw new ArgumentException("String must be 20 characters long", nameof(encoded));

        fixed (byte* pEncoded = encoded)
        {
            ulong value = Base32.GetByte(pEncoded[0]);
            value = (value << 5) | Base32.GetByte(pEncoded[1]);
            value = (value << 5) | Base32.GetByte(pEncoded[2]);
            value = (value << 5) | Base32.GetByte(pEncoded[3]);
            value = (value << 5) | Base32.GetByte(pEncoded[4]);
            value = (value << 5) | Base32.GetByte(pEncoded[5]);
            value = (value << 5) | Base32.GetByte(pEncoded[6]);
            value = (value << 5) | Base32.GetByte(pEncoded[7]);

            var timestamp = (byte)(value >> 32) << 24 | (byte)(value >> 24) << 16 | (byte)(value >> 16) << 8 | (byte)(value >> 8);

            var b = (int)(byte)value;

            value = (value << 5) | Base32.GetByte(pEncoded[8]);
            value = (value << 5) | Base32.GetByte(pEncoded[9]);
            value = (value << 5) | Base32.GetByte(pEncoded[10]);
            value = (value << 5) | Base32.GetByte(pEncoded[11]);
            value = (value << 5) | Base32.GetByte(pEncoded[12]);
            value = (value << 5) | Base32.GetByte(pEncoded[13]);
            value = (value << 5) | Base32.GetByte(pEncoded[14]);
            value = (value << 5) | Base32.GetByte(pEncoded[15]);

            b = b << 24 | (byte)(value >> 32) << 16 | (byte)(value >> 24) << 8 | (byte)(value >> 16);

            var c = (byte)(value >> 8) << 24 | (byte)value << 16;

            value = Base32.GetByte(pEncoded[16]);
            value = (value << 5) | Base32.GetByte(pEncoded[17]);
            value = (value << 5) | Base32.GetByte(pEncoded[18]);
            value = (value << 5) | Base32.GetByte(pEncoded[19]);

            c |= (byte)(value >> 12) << 8 | (byte)(value >> 4);

            return new Id(timestamp, b, c);
        }
    }
}