using IT.Internal;
using System;

namespace IT;

public readonly partial struct Id32
{
    public static int GetLengthHex(uint value)
    {
        if (value < Hex.NumCount1) return 1;

        if (value < Hex.NumSum2) return 2;

        if (value < Hex.NumSum3) return 3;

        if (value < Hex.NumSum4) return 4;

        if (value < Hex.NumSum5) return 5;

        if (value < Hex.NumSum6) return 6;

        if (value < Hex.NumSum7) return 7;

        return 8;
    }

    public static bool TryToHex(Span<char> chars, uint value)
    {
        var map = Hex._numLower;

        if (chars.Length == 0) return false;

        if (value < Hex.NumCount1)
        {
            chars[0] = map[value];

            return true;
        }

        if (value < Hex.NumSum2)
        {
            if (chars.Length < 2) return false;

            chars[0] = map[(value -= Hex.NumCount1) >> 4];
            chars[1] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum3)
        {
            if (chars.Length < 3) return false;

            chars[0] = map[(value -= Hex.NumSum2) >> 8];
            chars[1] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[2] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum4)
        {
            if (chars.Length < 4) return false;

            chars[0] = map[(value -= Hex.NumSum3) >> 12];
            chars[1] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[2] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[3] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum5)
        {
            if (chars.Length < 5) return false;

            chars[0] = map[(value -= Hex.NumSum4) >> 16];
            chars[1] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[2] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[3] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[4] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum6)
        {
            if (chars.Length < 6) return false;

            chars[0] = map[(value -= Hex.NumSum5) >> 20];
            chars[1] = map[(value &= Hex.NumCount5 - 1) >> 16];
            chars[2] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[3] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[4] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[5] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum7)
        {
            if (chars.Length < 7) return false;

            chars[0] = map[(value -= Hex.NumSum6) >> 24];
            chars[1] = map[(value &= Hex.NumCount6 - 1) >> 20];
            chars[2] = map[(value &= Hex.NumCount5 - 1) >> 16];
            chars[3] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[4] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[5] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[6] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (chars.Length < 8) return false;

        chars[0] = map[(value -= Hex.NumSum7) >> 28];
        chars[1] = map[(value &= Hex.NumCount7 - 1) >> 24];
        chars[2] = map[(value &= Hex.NumCount6 - 1) >> 20];
        chars[3] = map[(value &= Hex.NumCount5 - 1) >> 16];
        chars[4] = map[(value &= Hex.NumCount4 - 1) >> 12];
        chars[5] = map[(value &= Hex.NumCount3 - 1) >> 8];
        chars[6] = map[(value &= Hex.NumCount2 - 1) >> 4];
        chars[7] = map[value & Hex.NumCount1 - 1];

        return true;
    }

    public static bool TryToHexUpper(Span<char> chars, uint value)
    {
        var map = Hex._numUpper;

        if (chars.Length == 0) return false;

        if (value < Hex.NumCount1)
        {
            chars[0] = map[value];

            return true;
        }

        if (value < Hex.NumSum2)
        {
            if (chars.Length < 2) return false;

            chars[0] = map[(value -= Hex.NumCount1) >> 4];
            chars[1] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum3)
        {
            if (chars.Length < 3) return false;

            chars[0] = map[(value -= Hex.NumSum2) >> 8];
            chars[1] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[2] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum4)
        {
            if (chars.Length < 4) return false;

            chars[0] = map[(value -= Hex.NumSum3) >> 12];
            chars[1] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[2] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[3] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum5)
        {
            if (chars.Length < 5) return false;

            chars[0] = map[(value -= Hex.NumSum4) >> 16];
            chars[1] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[2] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[3] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[4] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum6)
        {
            if (chars.Length < 6) return false;

            chars[0] = map[(value -= Hex.NumSum5) >> 20];
            chars[1] = map[(value &= Hex.NumCount5 - 1) >> 16];
            chars[2] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[3] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[4] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[5] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (value < Hex.NumSum7)
        {
            if (chars.Length < 7) return false;

            chars[0] = map[(value -= Hex.NumSum6) >> 24];
            chars[1] = map[(value &= Hex.NumCount6 - 1) >> 20];
            chars[2] = map[(value &= Hex.NumCount5 - 1) >> 16];
            chars[3] = map[(value &= Hex.NumCount4 - 1) >> 12];
            chars[4] = map[(value &= Hex.NumCount3 - 1) >> 8];
            chars[5] = map[(value &= Hex.NumCount2 - 1) >> 4];
            chars[6] = map[value & Hex.NumCount1 - 1];

            return true;
        }

        if (chars.Length < 8) return false;

        chars[0] = map[(value -= Hex.NumSum7) >> 28];
        chars[1] = map[(value &= Hex.NumCount7 - 1) >> 24];
        chars[2] = map[(value &= Hex.NumCount6 - 1) >> 20];
        chars[3] = map[(value &= Hex.NumCount5 - 1) >> 16];
        chars[4] = map[(value &= Hex.NumCount4 - 1) >> 12];
        chars[5] = map[(value &= Hex.NumCount3 - 1) >> 8];
        chars[6] = map[(value &= Hex.NumCount2 - 1) >> 4];
        chars[7] = map[value & Hex.NumCount1 - 1];

        return true;
    }

    public static UInt32 ParseHexUInt(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new ArgumentException(nameof(chars));

        if (len > 8) throw new ArgumentOutOfRangeException(nameof(chars), "Length 1-6");

        if (len == 1) return GetIndexNum(chars[0]);

        if (len == 2) return (GetIndexNum(chars[0]) << 4) + GetIndexNum(chars[1]) + Hex.NumCount1;

        if (len == 3) return (GetIndexNum(chars[0]) << 8) + (GetIndexNum(chars[1]) << 4) + GetIndexNum(chars[2]) + Hex.NumSum2;

        if (len == 4) return (GetIndexNum(chars[0]) << 12) + (GetIndexNum(chars[1]) << 8) + (GetIndexNum(chars[2]) << 4) + GetIndexNum(chars[3]) + Hex.NumSum3;

        if (len == 5) return (GetIndexNum(chars[0]) << 16) + (GetIndexNum(chars[1]) << 12) + (GetIndexNum(chars[2]) << 8) + (GetIndexNum(chars[3]) << 4) + GetIndexNum(chars[4]) + Hex.NumSum4;

        if (len == 6) return (GetIndexNum(chars[0]) << 20) + (GetIndexNum(chars[1]) << 16) + (GetIndexNum(chars[2]) << 12) + (GetIndexNum(chars[3]) << 8) + (GetIndexNum(chars[4]) << 4) + GetIndexNum(chars[5]) + Hex.NumSum5;

        if (len == 7) return (GetIndexNum(chars[0]) << 24) + (GetIndexNum(chars[1]) << 20) + (GetIndexNum(chars[2]) << 16) + (GetIndexNum(chars[3]) << 12) + (GetIndexNum(chars[4]) << 8) + (GetIndexNum(chars[5]) << 4) + GetIndexNum(chars[6]) + Hex.NumSum6;

        return checked((GetIndexNum(chars[0]) << 28) + (GetIndexNum(chars[1]) << 24) + (GetIndexNum(chars[2]) << 20) + (GetIndexNum(chars[3]) << 16) + (GetIndexNum(chars[4]) << 12) + (GetIndexNum(chars[5]) << 8) + (GetIndexNum(chars[6]) << 4) + GetIndexNum(chars[7]) + Hex.NumSum7);
    }

    private static UInt32 GetIndexNum(Char ch) => ch switch
    {
        '0' => 0,
        '1' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'a' or 'A' => 10,
        'b' or 'B' => 11,
        'c' or 'C' => 12,
        'd' or 'D' => 13,
        'e' or 'E' => 14,
        'f' or 'F' => 15,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), $"Char '{ch}' is not HEX")
    };

    //public static uint GetMaxValueHex(uint length)
    //{
    //    if (length == 1) return Hex.NumCount1;
    //    if (length == 2) return Hex.NumSum2;
    //    if (length == 3) return Hex.NumSum3;
    //    if (length == 4) return Hex.NumSum4;
    //    if (length == 5) return Hex.NumSum5;
    //    if (length == 6) return Hex.NumSum6;
    //    if (length == 7) return Hex.NumSum7;

    //    return 8;
    //}
}