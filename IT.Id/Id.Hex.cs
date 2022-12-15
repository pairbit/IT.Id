using IT.Internal;
using System;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id
{
    public unsafe string ToHex()
    {
        var hexLower = new string((char)0, 24);
        var map = Hex.Lower32.Map;
        fixed (char* hexLowerP = hexLower)
        {
            uint* dest = (uint*)hexLowerP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }
        return hexLower;
    }

    public unsafe string ToHexUpper()
    {
        var hexUpper = new string((char)0, 24);
        var map = Hex.Upper32.Map;
        fixed (char* hexUpperP = hexUpper)
        {
            uint* dest = (uint*)hexUpperP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }
        return hexUpper;
    }

    public unsafe bool TryToHex(Span<char> chars)
    {
        if (chars.Length < 24) return false;

        uint* map = Hex.Lower32.Map;
        fixed (char* charsP = chars)
        {
            uint* dest = (uint*)charsP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }

        return true;
    }

    public unsafe bool TryToHexUpper(Span<char> chars)
    {
        if (chars.Length < 24) return false;

        uint* map = Hex.Upper32.Map;
        fixed (char* charsP = chars)
        {
            uint* dest = (uint*)charsP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }

        return true;
    }

    public unsafe bool TryToHex(Span<byte> bytes)
    {
        if (bytes.Length < 24) return false;

        ushort* map = Hex.Lower16.Map;
        fixed (byte* bytesP = bytes)
        {
            ushort* dest = (ushort*)bytesP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }

        return true;
    }

    public unsafe bool TryToHexUpper(Span<byte> bytes)
    {
        if (bytes.Length < 24) return false;

        ushort* map = Hex.Upper16.Map;
        fixed (byte* bytesP = bytes)
        {
            ushort* dest = (ushort*)bytesP;
            dest[0] = map[_timestamp0];
            dest[1] = map[_timestamp1];
            dest[2] = map[_timestamp2];
            dest[3] = map[_timestamp3];
            dest[4] = map[_machine0];
            dest[5] = map[_machine1];
            dest[6] = map[_machine2];
            dest[7] = map[_pid0];
            dest[8] = map[_pid1];
            dest[9] = map[_increment0];
            dest[10] = map[_increment1];
            dest[11] = map[_increment2];
        }

        return true;
    }

    public static bool TryParseHex(ReadOnlySpan<char> chars, out Id id)
    {
        if (chars.Length != 24) goto fail;

        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var cHi = chars[0];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        var cLo = chars[1];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        var bHi = map[cHi];
        var bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        id = default;
        ref var b = ref Unsafe.As<Id, byte>(ref id);
        Unsafe.WriteUnaligned(ref b, (byte)((bHi << 4) | bLo));

        cHi = chars[2];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[3];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)((bHi << 4) | bLo));

        cHi = chars[4];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[5];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)((bHi << 4) | bLo));

        cHi = chars[6];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[7];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)((bHi << 4) | bLo));

        cHi = chars[8];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[9];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)((bHi << 4) | bLo));

        cHi = chars[10];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[11];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)((bHi << 4) | bLo));

        cHi = chars[12];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[13];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)((bHi << 4) | bLo));

        cHi = chars[14];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[15];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)((bHi << 4) | bLo));

        cHi = chars[16];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[17];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)((bHi << 4) | bLo));

        cHi = chars[18];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[19];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)((bHi << 4) | bLo));

        cHi = chars[20];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[21];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)((bHi << 4) | bLo));

        cHi = chars[22];
        if (cHi < Hex.Min || cHi > Hex.Max) goto fail;
        cLo = chars[23];
        if (cLo < Hex.Min || cLo > Hex.Max) goto fail;
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)((bHi << 4) | bLo));

        return true;

    fail:
        id = default;
        return false;
    }

    public static bool TryParseHex(ReadOnlySpan<byte> bytes, out Id id)
    {
        if (bytes.Length != 24) goto fail;

        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var h = map[bytes[0]];
        var l = map[bytes[1]];
        if ((h | l) == 0xFF) goto fail;
        id = default;
        ref var b = ref Unsafe.As<Id, byte>(ref id);
        Unsafe.WriteUnaligned(ref b, (byte)((h << 4) | l));

        h = map[bytes[2]];
        l = map[bytes[3]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)((h << 4) | l));

        h = map[bytes[4]];
        l = map[bytes[5]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)((h << 4) | l));

        h = map[bytes[6]];
        l = map[bytes[7]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)((h << 4) | l));

        h = map[bytes[8]];
        l = map[bytes[9]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)((h << 4) | l));

        h = map[bytes[10]];
        l = map[bytes[11]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)((h << 4) | l));

        h = map[bytes[12]];
        l = map[bytes[13]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)((h << 4) | l));

        h = map[bytes[14]];
        l = map[bytes[15]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)((h << 4) | l));

        h = map[bytes[16]];
        l = map[bytes[17]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)((h << 4) | l));

        h = map[bytes[18]];
        l = map[bytes[19]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)((h << 4) | l));

        h = map[bytes[20]];
        l = map[bytes[21]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)((h << 4) | l));

        h = map[bytes[22]];
        l = map[bytes[23]];
        if ((h | l) == 0xFF) goto fail;
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)((h << 4) | l));

        return true;

    fail:
        id = default;
        return false;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseHex(ReadOnlySpan<char> chars)
    {
        if (chars.Length != 24) throw Ex.InvalidLengthChars(Idf.Hex, chars.Length);

        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var cHi = chars[0];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        var cLo = chars[1];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        var bHi = map[cHi];
        var bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Id id = default;
        ref var b = ref Unsafe.As<Id, byte>(ref id);
        Unsafe.WriteUnaligned(ref b, (byte)((bHi << 4) | bLo));

        cHi = chars[2];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[3];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)((bHi << 4) | bLo));

        cHi = chars[4];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[5];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)((bHi << 4) | bLo));

        cHi = chars[6];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[7];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)((bHi << 4) | bLo));

        cHi = chars[8];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[9];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)((bHi << 4) | bLo));

        cHi = chars[10];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[11];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)((bHi << 4) | bLo));

        cHi = chars[12];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[13];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)((bHi << 4) | bLo));

        cHi = chars[14];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[15];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)((bHi << 4) | bLo));

        cHi = chars[16];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[17];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)((bHi << 4) | bLo));

        cHi = chars[18];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[19];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)((bHi << 4) | bLo));

        cHi = chars[20];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[21];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)((bHi << 4) | bLo));

        cHi = chars[22];
        if (cHi < Hex.Min || cHi > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cHi);
        cLo = chars[23];
        if (cLo < Hex.Min || cLo > Hex.Max) throw Ex.InvalidChar(Idf.Hex, cLo);
        bHi = map[cHi];
        bLo = map[cLo];
        if ((bLo | bHi) == 0xFF) throw Ex.InvalidChar(Idf.Hex, cHi, cLo);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)((bHi << 4) | bLo));

        return id;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseHex(ReadOnlySpan<byte> bytes)
    {
        if (bytes.Length != 24) throw Ex.InvalidLengthBytes(Idf.Hex, bytes.Length);

        ReadOnlySpan<byte> map = Hex.DecodeMap;

        var h = map[bytes[0]];
        var l = map[bytes[1]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[0], bytes[1]);
        Id id = default;
        ref var b = ref Unsafe.As<Id, byte>(ref id);
        Unsafe.WriteUnaligned(ref b, (byte)((h << 4) | l));

        h = map[bytes[2]];
        l = map[bytes[3]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[2], bytes[3]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), (byte)((h << 4) | l));

        h = map[bytes[4]];
        l = map[bytes[5]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[4], bytes[5]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), (byte)((h << 4) | l));

        h = map[bytes[6]];
        l = map[bytes[7]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[6], bytes[7]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), (byte)((h << 4) | l));

        h = map[bytes[8]];
        l = map[bytes[9]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[8], bytes[9]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), (byte)((h << 4) | l));

        h = map[bytes[10]];
        l = map[bytes[11]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[10], bytes[11]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), (byte)((h << 4) | l));

        h = map[bytes[12]];
        l = map[bytes[13]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[12], bytes[13]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), (byte)((h << 4) | l));

        h = map[bytes[14]];
        l = map[bytes[15]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[14], bytes[15]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), (byte)((h << 4) | l));

        h = map[bytes[16]];
        l = map[bytes[17]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[16], bytes[17]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), (byte)((h << 4) | l));

        h = map[bytes[18]];
        l = map[bytes[19]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[18], bytes[19]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), (byte)((h << 4) | l));

        h = map[bytes[20]];
        l = map[bytes[21]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[20], bytes[21]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), (byte)((h << 4) | l));

        h = map[bytes[22]];
        l = map[bytes[23]];
        if ((h | l) == 0xFF) throw Ex.InvalidByte(Idf.Hex, bytes[22], bytes[23]);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), (byte)((h << 4) | l));

        return id;
    }

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseHex(String? str, out Id id) => TryParseHex(str.AsSpan(), out id);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id ParseHex(String str) => ParseHex((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

#endif
}