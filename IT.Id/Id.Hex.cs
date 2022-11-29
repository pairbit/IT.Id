using Internal;

namespace System;

public readonly partial struct Id
{
    private String ToHexLower()
    {
        var hexLower = new string((char)0, 24);
        unsafe
        {
            var map = Hex.Lower32.Map;
            fixed (char* hexLowerP = hexLower)
            {
                uint* dest = (uint*)hexLowerP;
                dest[0] = map[(byte)(_timestamp >> 24)];
                dest[1] = map[(byte)(_timestamp >> 16)];
                dest[2] = map[(byte)(_timestamp >> 8)];
                dest[3] = map[(byte)(_timestamp)];
                dest[4] = map[(byte)(_b >> 24)];
                dest[5] = map[(byte)(_b >> 16)];
                dest[6] = map[(byte)(_b >> 8)];
                dest[7] = map[(byte)(_b)];
                dest[8] = map[(byte)(_c >> 24)];
                dest[9] = map[(byte)(_c >> 16)];
                dest[10] = map[(byte)(_c >> 8)];
                dest[11] = map[(byte)(_c)];
            }
        }
        return hexLower;
    }

    private String ToHexUpper()
    {
        var hexUpper = new string((char)0, 24);
        unsafe
        {
            var map = Hex.Upper32.Map;
            fixed (char* hexUpperP = hexUpper)
            {
                uint* dest = (uint*)hexUpperP;
                dest[0] = map[(byte)(_timestamp >> 24)];
                dest[1] = map[(byte)(_timestamp >> 16)];
                dest[2] = map[(byte)(_timestamp >> 8)];
                dest[3] = map[(byte)(_timestamp)];
                dest[4] = map[(byte)(_b >> 24)];
                dest[5] = map[(byte)(_b >> 16)];
                dest[6] = map[(byte)(_b >> 8)];
                dest[7] = map[(byte)(_b)];
                dest[8] = map[(byte)(_c >> 24)];
                dest[9] = map[(byte)(_c >> 16)];
                dest[10] = map[(byte)(_c >> 8)];
                dest[11] = map[(byte)(_c)];
            }
        }
        return hexUpper;
    }

    private unsafe void ToHex(Span<Char> chars, uint* map)
    {
        fixed (char* charsP = chars)
        {
            uint* dest = (uint*)charsP;
            dest[0] = map[(byte)(_timestamp >> 24)];
            dest[1] = map[(byte)(_timestamp >> 16)];
            dest[2] = map[(byte)(_timestamp >> 8)];
            dest[3] = map[(byte)_timestamp];
            dest[4] = map[(byte)(_b >> 24)];
            dest[5] = map[(byte)(_b >> 16)];
            dest[6] = map[(byte)(_b >> 8)];
            dest[7] = map[(byte)_b];
            dest[8] = map[(byte)(_c >> 24)];
            dest[9] = map[(byte)(_c >> 16)];
            dest[10] = map[(byte)(_c >> 8)];
            dest[11] = map[(byte)_c];
        }
    }

    private unsafe void ToHex(Span<Byte> bytes, ushort* map)
    {
        fixed (byte* bytesP = bytes)
        {
            ushort* dest = (ushort*)bytesP;
            dest[0] = map[(byte)(_timestamp >> 24)];
            dest[1] = map[(byte)(_timestamp >> 16)];
            dest[2] = map[(byte)(_timestamp >> 8)];
            dest[3] = map[(byte)_timestamp];
            dest[4] = map[(byte)(_b >> 24)];
            dest[5] = map[(byte)(_b >> 16)];
            dest[6] = map[(byte)(_b >> 8)];
            dest[7] = map[(byte)_b];
            dest[8] = map[(byte)(_c >> 24)];
            dest[9] = map[(byte)(_c >> 16)];
            dest[10] = map[(byte)(_c >> 8)];
            dest[11] = map[(byte)_c];
        }
    }

    private static bool TryParseHex(ReadOnlySpan<Char> chars, out Id id)
    {
        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var cHi = chars[0];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        var cLo = chars[1];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        var bHi = map[cHi];
        var bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        var timestamp = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[2];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[3];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        timestamp |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[4];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[5];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        timestamp |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[6];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[7];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        timestamp |= (byte)((bHi << 4) | bLo);

        cHi = chars[8];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[9];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        var b = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[10];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[11];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        b |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[12];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[13];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        b |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[14];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[15];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        b |= (byte)((bHi << 4) | bLo);

        cHi = chars[16];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[17];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        var c = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[18];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[19];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        c |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[20];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[21];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        c |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[22];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;

        cLo = chars[23];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;

        c |= (byte)((bHi << 4) | bLo);

        id = new Id(timestamp, b, c);
        return true;

    fail:
        id = default;
        return false;
    }

    private static bool TryParseHex(ReadOnlySpan<Byte> bytes, out Id id)
    {
        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var h = map[bytes[0]];
        var l = map[bytes[1]];
        if ((h | l) == 0xFF) goto fail;
        var timestamp = (byte)((h << 4) | l) << 24;

        h = map[bytes[2]];
        l = map[bytes[3]];
        if ((h | l) == 0xFF) goto fail;
        timestamp |= (byte)((h << 4) | l) << 16;

        h = map[bytes[4]];
        l = map[bytes[5]];
        if ((h | l) == 0xFF) goto fail;
        timestamp |= (byte)((h << 4) | l) << 8;

        h = map[bytes[6]];
        l = map[bytes[7]];
        if ((h | l) == 0xFF) goto fail;
        timestamp |= (byte)((h << 4) | l);

        h = map[bytes[8]];
        l = map[bytes[9]];
        if ((h | l) == 0xFF) goto fail;
        var b = (byte)((h << 4) | l) << 24;

        h = map[bytes[10]];
        l = map[bytes[11]];
        if ((h | l) == 0xFF) goto fail;
        b |= (byte)((h << 4) | l) << 16;

        h = map[bytes[12]];
        l = map[bytes[13]];
        if ((h | l) == 0xFF) goto fail;
        b |= (byte)((h << 4) | l) << 8;

        h = map[bytes[14]];
        l = map[bytes[15]];
        if ((h | l) == 0xFF) goto fail;
        b |= (byte)((h << 4) | l);

        h = map[bytes[16]];
        l = map[bytes[17]];
        if ((h | l) == 0xFF) goto fail;
        var c = (byte)((h << 4) | l) << 24;

        h = map[bytes[18]];
        l = map[bytes[19]];
        if ((h | l) == 0xFF) goto fail;
        c |= (byte)((h << 4) | l) << 16;

        h = map[bytes[20]];
        l = map[bytes[21]];
        if ((h | l) == 0xFF) goto fail;
        c |= (byte)((h << 4) | l) << 8;

        h = map[bytes[22]];
        l = map[bytes[23]];
        if ((h | l) == 0xFF) goto fail;
        c |= (byte)((h << 4) | l);

        id = new Id(timestamp, b, c);
        return true;

    fail:
        id = default;
        return false;
    }

    private static Id ParseHex(ReadOnlySpan<Char> chars)
    {
        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var cHi = chars[0];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        var cLo = chars[1];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        var bHi = map[cHi];
        var bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        var timestamp = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[2];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[3];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        timestamp |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[4];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[5];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        timestamp |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[6];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[7];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        timestamp |= (byte)((bHi << 4) | bLo);

        cHi = chars[8];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[9];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        var b = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[10];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[11];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        b |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[12];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[13];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        b |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[14];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[15];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        b |= (byte)((bHi << 4) | bLo);

        cHi = chars[16];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[17];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        var c = (byte)((bHi << 4) | bLo) << 24;

        cHi = chars[18];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[19];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        c |= (byte)((bHi << 4) | bLo) << 16;

        cHi = chars[20];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[21];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        c |= (byte)((bHi << 4) | bLo) << 8;

        cHi = chars[22];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);

        cLo = chars[23];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);

        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);

        c |= (byte)((bHi << 4) | bLo);

        return new Id(timestamp, b, c);
    }

    private static Id ParseHex(ReadOnlySpan<Byte> bytes)
    {
        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var h = map[bytes[0]];
        var l = map[bytes[1]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[0], bytes[1]);
        var timestamp = (byte)((h << 4) | l) << 24;

        h = map[bytes[2]];
        l = map[bytes[3]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[2], bytes[3]);
        timestamp |= (byte)((h << 4) | l) << 16;

        h = map[bytes[4]];
        l = map[bytes[5]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[4], bytes[5]);
        timestamp |= (byte)((h << 4) | l) << 8;

        h = map[bytes[6]];
        l = map[bytes[7]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[6], bytes[7]);
        timestamp |= (byte)((h << 4) | l);

        h = map[bytes[8]];
        l = map[bytes[9]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[8], bytes[9]);
        var b = (byte)((h << 4) | l) << 24;

        h = map[bytes[10]];
        l = map[bytes[11]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[10], bytes[11]);
        b |= (byte)((h << 4) | l) << 16;

        h = map[bytes[12]];
        l = map[bytes[13]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[12], bytes[13]);
        b |= (byte)((h << 4) | l) << 8;

        h = map[bytes[14]];
        l = map[bytes[15]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[14], bytes[15]);
        b |= (byte)((h << 4) | l);

        h = map[bytes[16]];
        l = map[bytes[17]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[16], bytes[17]);
        var c = (byte)((h << 4) | l) << 24;

        h = map[bytes[18]];
        l = map[bytes[19]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[18], bytes[19]);
        c |= (byte)((h << 4) | l) << 16;

        h = map[bytes[20]];
        l = map[bytes[21]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[20], bytes[21]);
        c |= (byte)((h << 4) | l) << 8;

        h = map[bytes[22]];
        l = map[bytes[23]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[22], bytes[23]);
        c |= (byte)((h << 4) | l);

        return new Id(timestamp, b, c);
    }
}