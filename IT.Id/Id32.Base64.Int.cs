using IT.Internal;
using System;

namespace IT;

public readonly partial struct Id32
{
    public static uint GetMaxValueBase64(uint length)
    {
        if (length == 1) return Base64.NumCount1 - 1;
        if (length == 2) return Base64.NumSum2 - 1;
        if (length == 3) return Base64.NumSum3 - 1;
        if (length == 4) return Base64.NumSum4 - 1;
        if (length == 5) return Base64.NumSum5 - 1;
        if (length == 6) return uint.MaxValue;

        throw new ArgumentOutOfRangeException();
    }

    public static int GetLengthBase64(uint value)
    {
        if (value < Base64.NumCount1) return 1;
        if (value < Base64.NumSum2) return 2;
        if (value < Base64.NumSum3) return 3;
        if (value < Base64.NumSum4) return 4;
        if (value < Base64.NumSum5) return 5;
        return 6;
    }

    public static string ToBase64(uint value)
    {
        var len = GetLengthBase64(value);

        var base64 = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = base64)
            {
                var chars = new Span<Char>(ptr, len);

                TryToBase64(chars, value);
            }
        }
        return base64;
    }

    public static string ToBase64Url(uint value)
    {
        var len = GetLengthBase64(value);

        var base64 = new string('\0', len);

        unsafe
        {
            fixed (char* ptr = base64)
            {
                var chars = new Span<Char>(ptr, len);

                TryToBase64Url(chars, value);
            }
        }
        return base64;
    }

    public static bool TryToBase64(Span<Char> chars, uint value)
    {
        if (chars.Length == 0) return false;

        var map = Base64.tableNum;

        if (value < Base64.NumCount1)
        {
            chars[0] = map[value];

            return true;
        }

        if (value < Base64.NumSum2)
        {
            if (chars.Length < 2) return false;

            chars[0] = map[(value -= Base64.NumCount1) >> 6];
            chars[1] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum3)
        {
            if (chars.Length < 3) return false;

            chars[0] = map[(value -= Base64.NumSum2) >> 12];
            chars[1] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[2] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum4)
        {
            if (chars.Length < 4) return false;

            chars[0] = map[(value -= Base64.NumSum3) >> 18];
            chars[1] = map[(value &= Base64.NumCount3 - 1) >> 12];
            chars[2] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[3] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum5)
        {
            if (chars.Length < 5) return false;

            chars[0] = map[(value -= Base64.NumSum4) >> 24];
            chars[1] = map[(value &= Base64.NumCount4 - 1) >> 18];
            chars[2] = map[(value &= Base64.NumCount3 - 1) >> 12];
            chars[3] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[4] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (chars.Length < 6) return false;

        chars[0] = map[(value -= Base64.NumSum5) >> 30];
        chars[1] = map[(value &= Base64.NumCount5 - 1) >> 24];
        chars[2] = map[(value &= Base64.NumCount4 - 1) >> 18];
        chars[3] = map[(value &= Base64.NumCount3 - 1) >> 12];
        chars[4] = map[(value &= Base64.NumCount2 - 1) >> 6];
        chars[5] = map[value & Base64.NumCount1 - 1];

        return true;
    }

    public static bool TryToBase64Url(Span<Char> chars, uint value)
    {
        if (chars.Length == 0) return false;

        var map = Base64.tableNumUrl;

        if (value < Base64.NumCount1)
        {
            chars[0] = map[value];

            return true;
        }

        if (value < Base64.NumSum2)
        {
            if (chars.Length < 2) return false;

            chars[0] = map[(value -= Base64.NumCount1) >> 6];
            chars[1] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum3)
        {
            if (chars.Length < 3) return false;

            chars[0] = map[(value -= Base64.NumSum2) >> 12];
            chars[1] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[2] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum4)
        {
            if (chars.Length < 4) return false;

            chars[0] = map[(value -= Base64.NumSum3) >> 18];
            chars[1] = map[(value &= Base64.NumCount3 - 1) >> 12];
            chars[2] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[3] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (value < Base64.NumSum5)
        {
            if (chars.Length < 5) return false;

            chars[0] = map[(value -= Base64.NumSum4) >> 24];
            chars[1] = map[(value &= Base64.NumCount4 - 1) >> 18];
            chars[2] = map[(value &= Base64.NumCount3 - 1) >> 12];
            chars[3] = map[(value &= Base64.NumCount2 - 1) >> 6];
            chars[4] = map[value & Base64.NumCount1 - 1];

            return true;
        }

        if (chars.Length < 6) return false;

        chars[0] = map[(value -= Base64.NumSum5) >> 30];
        chars[1] = map[(value &= Base64.NumCount5 - 1) >> 24];
        chars[2] = map[(value &= Base64.NumCount4 - 1) >> 18];
        chars[3] = map[(value &= Base64.NumCount3 - 1) >> 12];
        chars[4] = map[(value &= Base64.NumCount2 - 1) >> 6];
        chars[5] = map[value & Base64.NumCount1 - 1];

        return true;
    }
}