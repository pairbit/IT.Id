using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System;

internal static class Hex
{
    //https://stackoverflow.com/questions/311165/how-do-you-convert-a-byte-array-to-a-hexadecimal-string-and-vice-versa/24343727#24343727
    private static readonly uint[] _lowerLookup32Unsafe = CreateLookup32Unsafe("x2");
    private static readonly uint[] _upperLookup32Unsafe = CreateLookup32Unsafe("X2");

    private static readonly ushort[] _lowerLookup16Unsafe = CreateLookup16Unsafe("x2");
    private static readonly ushort[] _upperLookup16Unsafe = CreateLookup16Unsafe("X2");

    internal static readonly unsafe uint* _lowerLookup32UnsafeP = (uint*)GCHandle.Alloc(_lowerLookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();
    internal static readonly unsafe uint* _upperLookup32UnsafeP = (uint*)GCHandle.Alloc(_upperLookup32Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

    internal static readonly unsafe ushort* _lowerLookup16UnsafeP = (ushort*)GCHandle.Alloc(_lowerLookup16Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();
    internal static readonly unsafe ushort* _upperLookup16UnsafeP = (ushort*)GCHandle.Alloc(_upperLookup16Unsafe, GCHandleType.Pinned).AddrOfPinnedObject();

    #region Number

    internal static readonly Char[] _numLower = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
    internal static readonly Char[] _numUpper = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };

    public const UInt32 NumCount1 = 16;
    public const UInt32 NumCount2 = NumCount1 * NumCount1;
    public const UInt32 NumSum2 = NumCount2 + NumCount1;
    public const UInt32 NumCount3 = NumCount2 * NumCount1;
    public const UInt32 NumSum3 = NumCount3 + NumSum2;
    public const UInt32 NumCount4 = NumCount3 * NumCount1;
    public const UInt32 NumSum4 = NumCount4 + NumSum3;
    public const UInt32 NumCount5 = NumCount4 * NumCount1;
    public const UInt32 NumSum5 = NumCount5 + NumSum4;
    public const UInt32 NumCount6 = NumCount5 * NumCount1;
    public const UInt32 NumSum6 = NumCount6 + NumSum5;
    public const UInt32 NumCount7 = NumCount6 * NumCount1;
    public const UInt32 NumSum7 = NumCount7 + NumSum6;
    public const UInt64 NumCount8 = (UInt64)NumCount7 * NumCount1;
    //public const UInt64 NumSum8 = NumCount8 + NumSum7;

    public static OperationStatus TryWrite(Span<Char> chars, UInt32 value, Char[] map)
    {
        if (chars.Length == 0) return OperationStatus.DestinationTooSmall;

        if (value < NumCount1)
        {
            chars[0] = map[value];

            return OperationStatus.Done;
        }

        if (value < NumSum2)
        {
            if (chars.Length < 2) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumCount1) >> 4];
            chars[1] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum3)
        {
            if (chars.Length < 3) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum2) >> 8];
            chars[1] = map[(value &= NumCount2 - 1) >> 4];
            chars[2] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum4)
        {
            if (chars.Length < 4) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum3) >> 12];
            chars[1] = map[(value &= NumCount3 - 1) >> 8];
            chars[2] = map[(value &= NumCount2 - 1) >> 4];
            chars[3] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum5)
        {
            if (chars.Length < 5) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum4) >> 16];
            chars[1] = map[(value &= NumCount4 - 1) >> 12];
            chars[2] = map[(value &= NumCount3 - 1) >> 8];
            chars[3] = map[(value &= NumCount2 - 1) >> 4];
            chars[4] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum6)
        {
            if (chars.Length < 6) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum5) >> 20];
            chars[1] = map[(value &= NumCount5 - 1) >> 16];
            chars[2] = map[(value &= NumCount4 - 1) >> 12];
            chars[3] = map[(value &= NumCount3 - 1) >> 8];
            chars[4] = map[(value &= NumCount2 - 1) >> 4];
            chars[5] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum7)
        {
            if (chars.Length < 7) return OperationStatus.DestinationTooSmall;

            chars[0] = map[(value -= NumSum6) >> 24];
            chars[1] = map[(value &= NumCount6 - 1) >> 20];
            chars[2] = map[(value &= NumCount5 - 1) >> 16];
            chars[3] = map[(value &= NumCount4 - 1) >> 12];
            chars[4] = map[(value &= NumCount3 - 1) >> 8];
            chars[5] = map[(value &= NumCount2 - 1) >> 4];
            chars[6] = map[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (chars.Length < 8) return OperationStatus.DestinationTooSmall;

        chars[0] = map[(value -= NumSum7) >> 28];
        chars[1] = map[(value &= NumCount7 - 1) >> 24];
        chars[2] = map[(value &= NumCount6 - 1) >> 20];
        chars[3] = map[(value &= NumCount5 - 1) >> 16];
        chars[4] = map[(value &= NumCount4 - 1) >> 12];
        chars[5] = map[(value &= NumCount3 - 1) >> 8];
        chars[6] = map[(value &= NumCount2 - 1) >> 4];
        chars[7] = map[value & NumCount1 - 1];

        return OperationStatus.Done;
    }

    public static Byte ToByte(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new ArgumentException(nameof(chars));

        if (len > 2) throw new ArgumentOutOfRangeException(nameof(chars), "Length 1-2");

        if (len == 1) return GetIndexNumByte(chars[0]);

        return checked((byte)((GetIndexNumByte(chars[0]) << 4) + GetIndexNumByte(chars[1]) + NumCount1));
    }

    public static UInt32 ToUInt32(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new ArgumentException(nameof(chars));

        if (len > 8) throw new ArgumentOutOfRangeException(nameof(chars), "Length 1-6");

        if (len == 1) return GetIndexNum(chars[0]);

        if (len == 2) return (GetIndexNum(chars[0]) << 4) + GetIndexNum(chars[1]) + NumCount1;

        if (len == 3) return (GetIndexNum(chars[0]) << 8) + (GetIndexNum(chars[1]) << 4) + GetIndexNum(chars[2]) + NumSum2;

        if (len == 4) return (GetIndexNum(chars[0]) << 12) + (GetIndexNum(chars[1]) << 8) + (GetIndexNum(chars[2]) << 4) + GetIndexNum(chars[3]) + NumSum3;

        if (len == 5) return (GetIndexNum(chars[0]) << 16) + (GetIndexNum(chars[1]) << 12) + (GetIndexNum(chars[2]) << 8) + (GetIndexNum(chars[3]) << 4) + GetIndexNum(chars[4]) + NumSum4;

        if (len == 6) return (GetIndexNum(chars[0]) << 20) + (GetIndexNum(chars[1]) << 16) + (GetIndexNum(chars[2]) << 12) + (GetIndexNum(chars[3]) << 8) + (GetIndexNum(chars[4]) << 4) + GetIndexNum(chars[5]) + NumSum5;

        if (len == 7) return (GetIndexNum(chars[0]) << 24) + (GetIndexNum(chars[1]) << 20) + (GetIndexNum(chars[2]) << 16) + (GetIndexNum(chars[3]) << 12) + (GetIndexNum(chars[4]) << 8) + (GetIndexNum(chars[5]) << 4) + GetIndexNum(chars[6]) + NumSum6;

        return checked((GetIndexNum(chars[0]) << 28) + (GetIndexNum(chars[1]) << 24) + (GetIndexNum(chars[2]) << 20) + (GetIndexNum(chars[3]) << 16) + (GetIndexNum(chars[4]) << 12) + (GetIndexNum(chars[5]) << 8) + (GetIndexNum(chars[6]) << 4) + GetIndexNum(chars[7]) + NumSum7);
    }

    public static Int32 GetLength(UInt32 value)
    {
        if (value < NumCount1) return 1;

        if (value < NumSum2) return 2;

        if (value < NumSum3) return 3;

        if (value < NumSum4) return 4;

        if (value < NumSum5) return 5;

        if (value < NumSum6) return 6;

        if (value < NumSum7) return 7;

        return 8;
    }

    public static UInt32 GetIndexNum(Char ch) => ch switch
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

    public static Byte GetIndexNumByte(Char ch) => ch switch
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

    #endregion

    private static uint[] CreateLookup32Unsafe(string format)
    {
        var result = new uint[256];
        for (int i = 0; i < 256; i++)
        {
            string s = i.ToString(format);
            if (BitConverter.IsLittleEndian)
                result[i] = s[0] + ((uint)s[1] << 16);
            else
                result[i] = s[1] + ((uint)s[0] << 16);
        }
        return result;
    }

    private static ushort[] CreateLookup16Unsafe(string format)
    {
        var result = new ushort[256];
        for (int i = 0; i < 256; i++)
        {
            string s = i.ToString(format);
            if (BitConverter.IsLittleEndian)
                result[i] = (ushort)(s[0] + (s[1] << 8));
            else
                result[i] = (ushort)(s[1] + (s[0] << 8));
        }
        return result;
    }

    public static void Decode(ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        bytes[0] = (byte)((_charToHex[chars[0]] << 4) | _charToHex[chars[1]]);
        bytes[1] = (byte)((_charToHex[chars[2]] << 4) | _charToHex[chars[3]]);
        bytes[2] = (byte)((_charToHex[chars[4]] << 4) | _charToHex[chars[5]]);
        bytes[3] = (byte)((_charToHex[chars[6]] << 4) | _charToHex[chars[7]]);
        bytes[4] = (byte)((_charToHex[chars[8]] << 4) | _charToHex[chars[9]]);
        bytes[5] = (byte)((_charToHex[chars[10]] << 4) | _charToHex[chars[11]]);
        bytes[6] = (byte)((_charToHex[chars[12]] << 4) | _charToHex[chars[13]]);
        bytes[7] = (byte)((_charToHex[chars[14]] << 4) | _charToHex[chars[15]]);
        bytes[8] = (byte)((_charToHex[chars[16]] << 4) | _charToHex[chars[17]]);
        bytes[9] = (byte)((_charToHex[chars[18]] << 4) | _charToHex[chars[19]]);
        bytes[10] = (byte)((_charToHex[chars[20]] << 4) | _charToHex[chars[21]]);
        bytes[11] = (byte)((_charToHex[chars[22]] << 4) | _charToHex[chars[23]]);
    }

    public static void Decode(ReadOnlySpan<byte> chars, Span<byte> bytes)
    {
        bytes[0] = (byte)((_charToHex[chars[0]] << 4) | _charToHex[chars[1]]);
        bytes[1] = (byte)((_charToHex[chars[2]] << 4) | _charToHex[chars[3]]);
        bytes[2] = (byte)((_charToHex[chars[4]] << 4) | _charToHex[chars[5]]);
        bytes[3] = (byte)((_charToHex[chars[6]] << 4) | _charToHex[chars[7]]);
        bytes[4] = (byte)((_charToHex[chars[8]] << 4) | _charToHex[chars[9]]);
        bytes[5] = (byte)((_charToHex[chars[10]] << 4) | _charToHex[chars[11]]);
        bytes[6] = (byte)((_charToHex[chars[12]] << 4) | _charToHex[chars[13]]);
        bytes[7] = (byte)((_charToHex[chars[14]] << 4) | _charToHex[chars[15]]);
        bytes[8] = (byte)((_charToHex[chars[16]] << 4) | _charToHex[chars[17]]);
        bytes[9] = (byte)((_charToHex[chars[18]] << 4) | _charToHex[chars[19]]);
        bytes[10] = (byte)((_charToHex[chars[20]] << 4) | _charToHex[chars[21]]);
        bytes[11] = (byte)((_charToHex[chars[22]] << 4) | _charToHex[chars[23]]);
    }

    public static bool TryDecodeFromUtf16(ReadOnlySpan<char> chars, Span<byte> bytes)
    {
        var byteHi = FromChar(chars[0]);
        var byteLo = FromChar(chars[1]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[0] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[2]);
        byteLo = FromChar(chars[3]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[1] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[4]);
        byteLo = FromChar(chars[5]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[2] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[6]);
        byteLo = FromChar(chars[7]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[3] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[8]);
        byteLo = FromChar(chars[9]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[4] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[10]);
        byteLo = FromChar(chars[11]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[5] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[12]);
        byteLo = FromChar(chars[13]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[6] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[14]);
        byteLo = FromChar(chars[15]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[7] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[16]);
        byteLo = FromChar(chars[17]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[8] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[18]);
        byteLo = FromChar(chars[19]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[9] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[20]);
        byteLo = FromChar(chars[21]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[10] = (byte)((byteHi << 4) | byteLo);

        byteHi = FromChar(chars[22]);
        byteLo = FromChar(chars[23]);

        if ((byteLo | byteHi) == 0xFF) return false;

        bytes[11] = (byte)((byteHi << 4) | byteLo);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FromChar(int c)
    {
        return c >= _charToHex.Length ? 0xFF : _charToHex[c];
    }

    /// <summary>Map from an ASCII char to its hex value, e.g. arr['b'] == 11. 0xFF means it's not a hex digit.</summary>
    internal static readonly byte[] _charToHex = new byte[]
    {
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 15
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 31
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 47
            0x0,  0x1,  0x2,  0x3,  0x4,  0x5,  0x6,  0x7,  0x8,  0x9,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 63
            0xFF, 0xA,  0xB,  0xC,  0xD,  0xE,  0xF,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 79
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 95
            0xFF, 0xa,  0xb,  0xc,  0xd,  0xe,  0xf,  0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 111
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 127
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 143
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 159
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 175
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 191
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 207
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 223
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, // 239
            0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF  // 255
    };
}