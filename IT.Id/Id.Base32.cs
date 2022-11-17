namespace System;

public readonly partial struct Id
{
    private String ToBase32()
    {
        var result = new string((char)0, 20);

        unsafe
        {
            fixed (char* resultP = result)
            fixed (char* pAlphabet = Base32.ALPHABET)
            {
                ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

                resultP[0] = pAlphabet[value >> 35];
                resultP[1] = pAlphabet[(value >> 30) & 0x1F];
                resultP[2] = pAlphabet[(value >> 25) & 0x1F];
                resultP[3] = pAlphabet[(value >> 20) & 0x1F];
                resultP[4] = pAlphabet[(value >> 15) & 0x1F];
                resultP[5] = pAlphabet[(value >> 10) & 0x1F];
                resultP[6] = pAlphabet[(value >> 5) & 0x1F];
                resultP[7] = pAlphabet[value & 0x1F];

                var byte5 = (byte)(_b >> 16);
                var byte6 = (byte)(_b >> 8);
                var byte7 = (byte)(_b);
                var byte8 = (byte)(_c >> 24);
                var byte9 = (byte)(_c >> 16);

                value = byte5;
                value = (value << 8) | byte6;
                value = (value << 8) | byte7;
                value = (value << 8) | byte8;
                value = (value << 8) | byte9;

                var byte10 = (byte)(_c >> 8);
                var byte11 = (byte)(_c);

                resultP[8] = pAlphabet[value >> 35];
                resultP[9] = pAlphabet[(value >> 30) & 0x1F];
                resultP[10] = pAlphabet[(value >> 25) & 0x1F];
                resultP[11] = pAlphabet[(value >> 20) & 0x1F];
                resultP[12] = pAlphabet[(value >> 15) & 0x1F];
                resultP[13] = pAlphabet[(value >> 10) & 0x1F];
                resultP[14] = pAlphabet[(value >> 5) & 0x1F];
                resultP[15] = pAlphabet[value & 0x1F];

                value = (((ulong)byte10 << 8) | byte11) << 4;

                resultP[16] = pAlphabet[value >> 15];
                resultP[17] = pAlphabet[(value >> 10) & 0x1F];
                resultP[18] = pAlphabet[(value >> 5) & 0x1F];
                resultP[19] = pAlphabet[value & 0x1F];
            }
        }

        return result;
    }

    private void ToBase32(Span<Char> destination)
    {
        Base32.Encode(ToByteArray(), destination);
    }

    private static Id ParseBase32(ReadOnlySpan<Char> value)
    {
        if (value.Length != 20) throw new ArgumentException("String must be 20 characters long", nameof(value));

        Span<Byte> bytes = stackalloc Byte[12];

        Base32.Decode(value, bytes);

        FromByteArray(bytes, 0, out var timestamp, out var b, out var c);

        return new Id(timestamp, b, c);
    }
}