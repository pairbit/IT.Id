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

    private static unsafe Id ParseBase32(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 20) throw new ArgumentException("The id must be 20 characters long", nameof(chars));

        fixed (char* src = chars)
        {
            ulong v = Base32.GetByte(src[0]);
            v = (v << 5) | Base32.GetByte(src[1]);
            v = (v << 5) | Base32.GetByte(src[2]);
            v = (v << 5) | Base32.GetByte(src[3]);
            v = (v << 5) | Base32.GetByte(src[4]);
            v = (v << 5) | Base32.GetByte(src[5]);
            v = (v << 5) | Base32.GetByte(src[6]);
            v = (v << 5) | Base32.GetByte(src[7]);

            var timestamp = (byte)(v >> 32) << 24 | (byte)(v >> 24) << 16 | (byte)(v >> 16) << 8 | (byte)(v >> 8);

            var b = (int)(byte)v;

            v = (v << 5) | Base32.GetByte(src[8]);
            v = (v << 5) | Base32.GetByte(src[9]);
            v = (v << 5) | Base32.GetByte(src[10]);
            v = (v << 5) | Base32.GetByte(src[11]);
            v = (v << 5) | Base32.GetByte(src[12]);
            v = (v << 5) | Base32.GetByte(src[13]);
            v = (v << 5) | Base32.GetByte(src[14]);
            v = (v << 5) | Base32.GetByte(src[15]);

            b = b << 24 | (byte)(v >> 32) << 16 | (byte)(v >> 24) << 8 | (byte)(v >> 16);

            var c = (byte)(v >> 8) << 24 | (byte)v << 16;

            v = Base32.GetByte(src[16]);
            v = (v << 5) | Base32.GetByte(src[17]);
            v = (v << 5) | Base32.GetByte(src[18]);
            v = (v << 5) | Base32.GetByte(src[19]);

            c |= (byte)(v >> 12) << 8 | (byte)(v >> 4);

            return new Id(timestamp, b, c);
        }
    }

    private static unsafe Id ParseBase32(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 20) throw new ArgumentException("The id must be 20 bytes long", nameof(bytes));

        fixed (byte* src = bytes)
        {
            ulong v = Base32.GetByte(src[0]);
            v = (v << 5) | Base32.GetByte(src[1]);
            v = (v << 5) | Base32.GetByte(src[2]);
            v = (v << 5) | Base32.GetByte(src[3]);
            v = (v << 5) | Base32.GetByte(src[4]);
            v = (v << 5) | Base32.GetByte(src[5]);
            v = (v << 5) | Base32.GetByte(src[6]);
            v = (v << 5) | Base32.GetByte(src[7]);

            var timestamp = (byte)(v >> 32) << 24 | (byte)(v >> 24) << 16 | (byte)(v >> 16) << 8 | (byte)(v >> 8);

            var b = (int)(byte)v;

            v = (v << 5) | Base32.GetByte(src[8]);
            v = (v << 5) | Base32.GetByte(src[9]);
            v = (v << 5) | Base32.GetByte(src[10]);
            v = (v << 5) | Base32.GetByte(src[11]);
            v = (v << 5) | Base32.GetByte(src[12]);
            v = (v << 5) | Base32.GetByte(src[13]);
            v = (v << 5) | Base32.GetByte(src[14]);
            v = (v << 5) | Base32.GetByte(src[15]);

            b = b << 24 | (byte)(v >> 32) << 16 | (byte)(v >> 24) << 8 | (byte)(v >> 16);

            var c = (byte)(v >> 8) << 24 | (byte)v << 16;

            v = Base32.GetByte(src[16]);
            v = (v << 5) | Base32.GetByte(src[17]);
            v = (v << 5) | Base32.GetByte(src[18]);
            v = (v << 5) | Base32.GetByte(src[19]);

            c |= (byte)(v >> 12) << 8 | (byte)(v >> 4);

            return new Id(timestamp, b, c);
        }
    }
}