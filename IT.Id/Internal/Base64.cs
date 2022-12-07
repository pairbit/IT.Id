using System;
using System.Buffers;
using System.Text;

namespace IT.Internal;

internal static class Base64
{
    public const char Format = '/';
    public const char FormatUrl = '_';
    public const int Min = 43;
    public const int Max = 122;

    internal static readonly char[] table = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                                              'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                                              'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                                              't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
                                              '8', '9', '+', '/', '=' };

    internal static readonly byte[] bytes = Encoding.UTF8.GetBytes(table);

    internal static readonly char[] tableNum = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e',
                                                 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                                                 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
                                                 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X',
                                                 'Y', 'Z', '-', '_' };

    internal static readonly char[] tableUrl = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                                                 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd',
                                                 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                                                 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7',
                                                 '8', '9', '-', '_', '=' };

    internal static readonly byte[] bytesUrl = Encoding.UTF8.GetBytes(tableUrl);

    internal static readonly sbyte[] DecodeMap = new sbyte[] {
    -1, //0
    -1, //1
    -1, //2
    -1, //3
    -1, //4
    -1, //5
    -1, //6
    -1, //7
    -1, //8
    -1, //9
    -1, //10
    -1, //11
    -1, //12
    -1, //13
    -1, //14
    -1, //15
    -1, //16
    -1, //17
    -1, //18
    -1, //19
    -1, //20
    -1, //21
    -1, //22
    -1, //23
    -1, //24
    -1, //25
    -1, //26
    -1, //27
    -1, //28
    -1, //29
    -1, //30
    -1, //31
    -1, //32
    -1, //33
    -1, //34
    -1, //35
    -1, //36
    -1, //37
    -1, //38
    -1, //39
    -1, //40
    -1, //41
    -1, //42
    62, //43 -> +
    -1, //44
    62, //45 -> -
    -1, //46
    63, //47 -> /
    52, //48 -> 0
    53, //49 -> 1
    54, //50 -> 2
    55, //51 -> 3
    56, //52 -> 4
    57, //53 -> 5
    58, //54 -> 6
    59, //55 -> 7
    60, //56 -> 8
    61, //57 -> 9
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
     0, //65 -> A
     1, //66 -> B
     2, //67 -> C
     3, //68 -> D
     4, //69 -> E
     5, //70 -> F
     6, //71 -> G
     7, //72 -> H
     8, //73 -> I
     9, //74 -> J
    10, //75 -> K
    11, //76 -> L
    12, //77 -> M
    13, //78 -> N
    14, //79 -> O
    15, //80 -> P
    16, //81 -> Q
    17, //82 -> R
    18, //83 -> S
    19, //84 -> T
    20, //85 -> U
    21, //86 -> V
    22, //87 -> W
    23, //88 -> X
    24, //89 -> Y
    25, //90 -> Z
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    63, //95 -> _
    -1, //96
    26, //97 -> a
    27, //98 -> b
    28, //99 -> c
    29, //100 -> d
    30, //101 -> e
    31, //102 -> f
    32, //103 -> g
    33, //104 -> h
    34, //105 -> i
    35, //106 -> j
    36, //107 -> k
    37, //108 -> l
    38, //109 -> m
    39, //110 -> n
    40, //111 -> o
    41, //112 -> p
    42, //113 -> q
    43, //114 -> r
    44, //115 -> s
    45, //116 -> t
    46, //117 -> u
    47, //118 -> v
    48, //119 -> w
    49, //120 -> x
    50, //121 -> y
    51, //122 -> z
    -1, //123
    -1, //124
    -1, //125
    -1, //126
    -1, //127
    -1, //128
    -1, //129
    -1, //130
    -1, //131
    -1, //132
    -1, //133
    -1, //134
    -1, //135
    -1, //136
    -1, //137
    -1, //138
    -1, //139
    -1, //140
    -1, //141
    -1, //142
    -1, //143
    -1, //144
    -1, //145
    -1, //146
    -1, //147
    -1, //148
    -1, //149
    -1, //150
    -1, //151
    -1, //152
    -1, //153
    -1, //154
    -1, //155
    -1, //156
    -1, //157
    -1, //158
    -1, //159
    -1, //160
    -1, //161
    -1, //162
    -1, //163
    -1, //164
    -1, //165
    -1, //166
    -1, //167
    -1, //168
    -1, //169
    -1, //170
    -1, //171
    -1, //172
    -1, //173
    -1, //174
    -1, //175
    -1, //176
    -1, //177
    -1, //178
    -1, //179
    -1, //180
    -1, //181
    -1, //182
    -1, //183
    -1, //184
    -1, //185
    -1, //186
    -1, //187
    -1, //188
    -1, //189
    -1, //190
    -1, //191
    -1, //192
    -1, //193
    -1, //194
    -1, //195
    -1, //196
    -1, //197
    -1, //198
    -1, //199
    -1, //200
    -1, //201
    -1, //202
    -1, //203
    -1, //204
    -1, //205
    -1, //206
    -1, //207
    -1, //208
    -1, //209
    -1, //210
    -1, //211
    -1, //212
    -1, //213
    -1, //214
    -1, //215
    -1, //216
    -1, //217
    -1, //218
    -1, //219
    -1, //220
    -1, //221
    -1, //222
    -1, //223
    -1, //224
    -1, //225
    -1, //226
    -1, //227
    -1, //228
    -1, //229
    -1, //230
    -1, //231
    -1, //232
    -1, //233
    -1, //234
    -1, //235
    -1, //236
    -1, //237
    -1, //238
    -1, //239
    -1, //240
    -1, //241
    -1, //242
    -1, //243
    -1, //244
    -1, //245
    -1, //246
    -1, //247
    -1, //248
    -1, //249
    -1, //250
    -1, //251
    -1, //252
    -1, //253
    -1, //254
    -1, //255
    };

    //internal static readonly sbyte[] _decodingMap = new sbyte[]
    //{
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, 62, -1, 62, -1, 63,         // 62 is placed at index 43 (for +) and 45 (for -), 63 at index 47 (for /)
    //        52, 53, 54, 55, 56, 57, 58, 59, 60, 61, -1, -1, -1, -1, -1, -1,         // 52-61 are placed at index 48-57 (for 0-9), 64 at index 61 (for =)
    //        -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14,
    //        15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, -1, -1, -1, -1, 63,         // 0-25 are placed at index 65-90 (for A-Z), 63 at index 95 (for _)
    //        -1, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40,
    //        41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, -1, -1, -1, -1, -1,         // 26-51 are placed at index 97-122 (for a-z)
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,         // Bytes over 122 ('z') are invalid and cannot be decoded
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,         // Hence, padding the map with 255, which indicates invalid input
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //        -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1,
    //};

    //static Base64()
    //{
    //    Console.WriteLine("{");
    //    for (int i = 0; i < _decodingMap.Length; i++)
    //    {
    //        var code = _decodingMap[i];
    //        if (code == -1)
    //            Console.WriteLine($"{code,2}, //{i}");
    //        else
    //            Console.WriteLine($"{code,2}, //{i} -> {(char)i}");
    //    }
    //    Console.WriteLine("};");
    //}

    #region Numbers

    /// <summary>
    /// 2^6
    /// </summary>
    public const UInt32 NumCount1 = 64;

    /// <summary>
    /// 2^12 = 4 096
    /// </summary>
    public const UInt32 NumCount2 = NumCount1 * NumCount1;

    /// <summary>
    /// 4 160
    /// </summary>
    public const UInt32 NumSum2 = NumCount2 + NumCount1;

    /// <summary>
    /// 2^18 = 262 144
    /// </summary>
    public const UInt32 NumCount3 = NumCount2 * NumCount1;

    /// <summary>
    /// 266 304
    /// </summary>
    public const UInt32 NumSum3 = NumCount3 + NumSum2;

    /// <summary>
    /// 2^24 = 16 777 216
    /// </summary>
    public const UInt32 NumCount4 = NumCount3 * NumCount1;

    /// <summary>
    /// 17 043 520
    /// </summary>
    public const UInt32 NumSum4 = NumCount4 + NumSum3;

    /// <summary>
    /// 2^30 = 1 073 741 824
    /// </summary>
    public const UInt32 NumCount5 = NumCount4 * NumCount1;

    /// <summary>
    /// 1 090 785 344
    /// </summary>
    public const UInt32 NumSum5 = NumCount5 + NumSum4;

    /// <summary>
    /// 2^36 = 68 719 476 736
    /// </summary>
    public const UInt64 NumCount6 = (UInt64)NumCount5 * NumCount1;

    /// <summary>
    /// 69 810 262 080
    /// </summary>
    public const UInt64 NumSum6 = NumCount6 + NumSum5;

    /// <summary>
    /// 2^42 = 4 398 046 511 104
    /// </summary>
    public const UInt64 NumCount7 = NumCount6 * NumCount1;

    /// <summary>
    /// 4 467 856 773 184
    /// </summary>
    public const UInt64 NumSum7 = NumCount7 + NumSum6;

    /// <summary>
    /// 2^48 = 281 474 976 710 656
    /// </summary>
    public const UInt64 NumCount8 = NumCount7 * NumCount1;

    /// <summary>
    /// 285 942 833 483 840
    /// </summary>
    public const UInt64 NumSum8 = NumCount8 + NumSum7;

    /// <summary>
    /// 2^54 = 18 014 398 509 481 984
    /// </summary>
    public const UInt64 NumCount9 = NumCount8 * NumCount1;

    /// <summary>
    /// 18 300 341 342 965 824
    /// </summary>
    public const UInt64 NumSum9 = NumCount9 + NumSum8;

    /// <summary>
    /// 2^60 = 1 152 921 504 606 846 976
    /// </summary>
    public const UInt64 NumCount10 = NumCount9 * NumCount1;

    /// <summary>
    /// 1 171 221 845 949 812 800
    /// </summary>
    public const UInt64 NumSum10 = NumCount10 + NumSum9;

    /// <summary>
    /// 2^66 = 73 786 976 294 838 206 464
    /// </summary>
    public const Decimal NumCount11 = (Decimal)NumCount10 * NumCount1;

    /// <summary>
    /// 74 958 198 140 788 019 264
    /// </summary>
    public const Decimal NumSum11 = NumCount11 + NumSum10;

    public static UInt32 ToUInt32(ReadOnlySpan<Char> base64num)
    {
        var len = base64num.Length;

        if (len == 0) throw new ArgumentException(nameof(base64num));

        if (len > 6) throw new ArgumentOutOfRangeException(nameof(base64num), "Length 1-6");

        var span = base64num;

        if (len == 1) return GetIndexNum(span[0]);

        if (len == 2) return (GetIndexNum(span[0]) << 6) + GetIndexNum(span[1]) + NumCount1;

        if (len == 3) return (GetIndexNum(span[0]) << 12) + (GetIndexNum(span[1]) << 6) + GetIndexNum(span[2]) + NumSum2;

        if (len == 4) return (GetIndexNum(span[0]) << 18) + (GetIndexNum(span[1]) << 12) + (GetIndexNum(span[2]) << 6) + GetIndexNum(span[3]) + NumSum3;

        if (len == 5) return (GetIndexNum(span[0]) << 24) + (GetIndexNum(span[1]) << 18) + (GetIndexNum(span[2]) << 12) + (GetIndexNum(span[3]) << 6) + GetIndexNum(span[4]) + NumSum4;

        return checked((GetIndexNum(span[0]) << 30) + (GetIndexNum(span[1]) << 24) + (GetIndexNum(span[2]) << 18) + (GetIndexNum(span[3]) << 12) + (GetIndexNum(span[4]) << 6) + GetIndexNum(span[5]) + NumSum5);
    }

    public static Byte ToByte(ReadOnlySpan<Char> base64num)
    {
        var len = base64num.Length;

        if (len == 0) throw new ArgumentException(nameof(base64num));

        if (len > 6) throw new ArgumentOutOfRangeException(nameof(base64num), "Length 1-6");

        var span = base64num;

        if (len == 1) return GetIndexNumByte(span[0]);

        return checked((byte)((GetIndexNumByte(span[0]) << 6) + GetIndexNumByte(span[1]) + NumCount1));
    }

    public static Int32 ToInt32(ReadOnlySpan<Char> base64num) => (Int32)ToUInt32(base64num);

    public static UInt64 ToUInt64(ReadOnlySpan<Char> base64num)
    {
        var len = base64num.Length;

        if (len == 0) throw new ArgumentException(nameof(base64num));

        if (len > 11) throw new ArgumentOutOfRangeException(nameof(base64num), "Length 1-11");

        if (len < 6) return ToUInt32(base64num);

        var span = base64num;

        if (len == 6) return ((UInt64)GetIndexNum(span[0]) << 30) + (GetIndexNum(span[1]) << 24) + (GetIndexNum(span[2]) << 18) +
                             (GetIndexNum(span[3]) << 12) + (GetIndexNum(span[4]) << 6) + GetIndexNum(span[5]) + NumSum5;

        if (len == 7) return ((UInt64)GetIndexNum(span[0]) << 36) + ((UInt64)GetIndexNum(span[1]) << 30) + (GetIndexNum(span[2]) << 24) +
                             (GetIndexNum(span[3]) << 18) + (GetIndexNum(span[4]) << 12) + (GetIndexNum(span[5]) << 6) + GetIndexNum(span[6]) + NumSum6;

        if (len == 8) return ((UInt64)GetIndexNum(span[0]) << 42) + ((UInt64)GetIndexNum(span[1]) << 36) + ((UInt64)GetIndexNum(span[2]) << 30) + (GetIndexNum(span[3]) << 24) +
                             (GetIndexNum(span[4]) << 18) + (GetIndexNum(span[5]) << 12) + (GetIndexNum(span[6]) << 6) + GetIndexNum(span[7]) + NumSum7;

        if (len == 9) return ((UInt64)GetIndexNum(span[0]) << 48) + ((UInt64)GetIndexNum(span[1]) << 42) + ((UInt64)GetIndexNum(span[2]) << 36) + ((UInt64)GetIndexNum(span[3]) << 30) +
                             (GetIndexNum(span[4]) << 24) + (GetIndexNum(span[5]) << 18) + (GetIndexNum(span[6]) << 12) + (GetIndexNum(span[7]) << 6) +
                             GetIndexNum(span[8]) + NumSum8;

        if (len == 10) return ((UInt64)GetIndexNum(span[0]) << 54) + ((UInt64)GetIndexNum(span[1]) << 48) + ((UInt64)GetIndexNum(span[2]) << 42) + ((UInt64)GetIndexNum(span[3]) << 36) +
                              ((UInt64)GetIndexNum(span[4]) << 30) + (GetIndexNum(span[5]) << 24) + (GetIndexNum(span[6]) << 18) + (GetIndexNum(span[7]) << 12) +
                              (GetIndexNum(span[8]) << 6) + GetIndexNum(span[9]) + NumSum9;

        return checked(((UInt64)GetIndexNum(span[0]) << 60) + ((UInt64)GetIndexNum(span[1]) << 54) + ((UInt64)GetIndexNum(span[2]) << 48) + ((UInt64)GetIndexNum(span[3]) << 42) +
               ((UInt64)GetIndexNum(span[4]) << 36) + ((UInt64)GetIndexNum(span[5]) << 30) + (GetIndexNum(span[6]) << 24) + (GetIndexNum(span[7]) << 18) +
               (GetIndexNum(span[8]) << 12) + (GetIndexNum(span[9]) << 6) + GetIndexNum(span[10]) + NumSum10);
    }

    /*

1 char |             64 :             0 -             63
2 char |          4 096 :            64 -          4 159
3 char |        262 144 :         4 160 -        266 303
4 char |     16 777 216 :       266 304 -     17 043 519
5 char |  1 073 741 824 :    17 043 520 -  1 090 785 343
6 char | 68 719 476 736 : 1 090 785 344 - 69 810 262 079

 Int16 |         32 767
UInt16 |         65 535
 Int32 |  2 147 483 647
UInt32 |  4 294 967 295

=======================================================

 7 char |          4 398 046 511 104
 8 char |        281 474 976 710 656
 9 char |     18 014 398 509 481 984
10 char |  1 152 921 504 606 846 976
11 char | 73 786 976 294 838 206 464

  Int64 |  9 223 372 036 854 775 807
 UInt64 | 18 446 744 073 709 551 615

=======================================================
15 char |  1 237 940 039 285 380 274 899 124 224
16 char | 79 228 162 514 264 337 593 543 950 336
Decimal | 79 228 162 514 264 337 593 543 950 335
         */

    public static String ToString(SByte value) => ToString((UInt32)(Byte)value);

    public static String ToString(Byte value) => ToString((UInt32)value);

    public static String ToString(Int16 value) => ToString((UInt32)(UInt16)value);

    public static String ToString(UInt16 value) => ToString((UInt32)value);

    public static String ToString(Int32 value) => ToString((UInt32)value);

    public static OperationStatus TryWrite(Span<Char> chars, Byte value)
        => TryWrite(chars, (UInt32)value);

    public static OperationStatus TryWrite(Span<Char> chars, UInt32 value)
    {
        if (chars.Length == 0) return OperationStatus.DestinationTooSmall;

        if (value < NumCount1)
        {
            chars[0] = tableNum[value];

            return OperationStatus.Done;
        }

        if (value < NumSum2)
        {
            if (chars.Length < 2) return OperationStatus.DestinationTooSmall;

            chars[0] = tableNum[(value -= NumCount1) >> 6];
            chars[1] = tableNum[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum3)
        {
            if (chars.Length < 3) return OperationStatus.DestinationTooSmall;

            chars[0] = tableNum[(value -= NumSum2) >> 12];
            chars[1] = tableNum[(value &= NumCount2 - 1) >> 6];
            chars[2] = tableNum[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum4)
        {
            if (chars.Length < 4) return OperationStatus.DestinationTooSmall;

            chars[0] = tableNum[(value -= NumSum3) >> 18];
            chars[1] = tableNum[(value &= NumCount3 - 1) >> 12];
            chars[2] = tableNum[(value &= NumCount2 - 1) >> 6];
            chars[3] = tableNum[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (value < NumSum5)
        {
            if (chars.Length < 5) return OperationStatus.DestinationTooSmall;

            chars[0] = tableNum[(value -= NumSum4) >> 24];
            chars[1] = tableNum[(value &= NumCount4 - 1) >> 18];
            chars[2] = tableNum[(value &= NumCount3 - 1) >> 12];
            chars[3] = tableNum[(value &= NumCount2 - 1) >> 6];
            chars[4] = tableNum[value & NumCount1 - 1];

            return OperationStatus.Done;
        }

        if (chars.Length < 6) return OperationStatus.DestinationTooSmall;

        chars[0] = tableNum[(value -= NumSum5) >> 30];
        chars[1] = tableNum[(value &= NumCount5 - 1) >> 24];
        chars[2] = tableNum[(value &= NumCount4 - 1) >> 18];
        chars[3] = tableNum[(value &= NumCount3 - 1) >> 12];
        chars[4] = tableNum[(value &= NumCount2 - 1) >> 6];
        chars[5] = tableNum[value & NumCount1 - 1];

        return OperationStatus.Done;
    }

    public static String ToString(UInt32 value)
    {
        if (value < NumCount1) return tableNum[value].ToString();

        if (value < NumSum2) return new String(new Char[] {
            tableNum[(value -= NumCount1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum3) return new String(new Char[] {
            tableNum[(value -= NumSum2) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum4) return new String(new Char[] {
            tableNum[(value -= NumSum3) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum5) return new String(new Char[] {
            tableNum[(value -= NumSum4) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        return new String(new Char[] {
            tableNum[(value -= NumSum5) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });
    }

    public static String ToString(Int64 value) => ToString((UInt64)value);

    public static String ToString(UInt64 value)
    {
        if (value <= UInt32.MaxValue) return ToString((UInt32)value);

        if (value < NumSum6) return new String(new Char[] {
            tableNum[(value -= NumSum5) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum7) return new String(new Char[] {
            tableNum[(value -= NumSum6) >> 36],
            tableNum[(value &= NumCount6 - 1) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum8) return new String(new Char[] {
            tableNum[(value -= NumSum7) >> 42],
            tableNum[(value &= NumCount7 - 1) >> 36],
            tableNum[(value &= NumCount6 - 1) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum9) return new String(new Char[] {
            tableNum[(value -= NumSum8) >> 48],
            tableNum[(value &= NumCount8 - 1) >> 42],
            tableNum[(value &= NumCount7 - 1) >> 36],
            tableNum[(value &= NumCount6 - 1) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        if (value < NumSum10) return new String(new Char[] {
            tableNum[(value -= NumSum9) >> 54],
            tableNum[(value &= NumCount9 - 1) >> 48],
            tableNum[(value &= NumCount8 - 1) >> 42],
            tableNum[(value &= NumCount7 - 1) >> 36],
            tableNum[(value &= NumCount6 - 1) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });

        return new String(new Char[] {
            tableNum[(value -= NumSum10) >> 60],
            tableNum[(value &= NumCount10 - 1) >> 54],
            tableNum[(value &= NumCount9 - 1) >> 48],
            tableNum[(value &= NumCount8 - 1) >> 42],
            tableNum[(value &= NumCount7 - 1) >> 36],
            tableNum[(value &= NumCount6 - 1) >> 30],
            tableNum[(value &= NumCount5 - 1) >> 24],
            tableNum[(value &= NumCount4 - 1) >> 18],
            tableNum[(value &= NumCount3 - 1) >> 12],
            tableNum[(value &= NumCount2 - 1) >> 6],
            tableNum[value & NumCount1 - 1]
        });
    }

    //public static String ToString(Decimal value)
    //{
    //    if (value >= Int32.MinValue && value <= UInt32.MaxValue) return ToString((UInt32)value);
    //    if (value >= Int64.MinValue && value <= UInt64.MaxValue) return ToString((UInt64)value);

    //    return new string(new Char[] {
    //        tableNum[(value -= NumSum10) >> 60],
    //        tableNum[(value &= NumCount10 - 1) >> 54],
    //        tableNum[(value &= NumCount9 - 1) >> 48],
    //        tableNum[(value &= NumCount8 - 1) >> 42],
    //        tableNum[(value &= NumCount7 - 1) >> 36],
    //        tableNum[(value &= NumCount6 - 1) >> 30],
    //        tableNum[(value &= NumCount5 - 1) >> 24],
    //        tableNum[(value &= NumCount4 - 1) >> 18],
    //        tableNum[(value &= NumCount3 - 1) >> 12],
    //        tableNum[(value &= NumCount2 - 1) >> 6],
    //        tableNum[value & (NumCount1 - 1)]
    //    });
    //}

    public static Int32 GetLength(UInt32 value)
    {
        if (value < NumCount1) return 1;

        if (value < NumSum2) return 2;

        if (value < NumSum3) return 3;

        if (value < NumSum4) return 4;

        if (value < NumSum5) return 5;

        return 5;
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
        'a' => 10,
        'b' => 11,
        'c' => 12,
        'd' => 13,
        'e' => 14,
        'f' => 15,
        'g' => 16,
        'h' => 17,
        'i' => 18,
        'j' => 19,
        'k' => 20,
        'l' => 21,
        'm' => 22,
        'n' => 23,
        'o' => 24,
        'p' => 25,
        'q' => 26,
        'r' => 27,
        's' => 28,
        't' => 29,
        'u' => 30,
        'v' => 31,
        'w' => 32,
        'x' => 33,
        'y' => 34,
        'z' => 35,
        'A' => 36,
        'B' => 37,
        'C' => 38,
        'D' => 39,
        'E' => 40,
        'F' => 41,
        'G' => 42,
        'H' => 43,
        'I' => 44,
        'J' => 45,
        'K' => 46,
        'L' => 47,
        'M' => 48,
        'N' => 49,
        'O' => 50,
        'P' => 51,
        'Q' => 52,
        'R' => 53,
        'S' => 54,
        'T' => 55,
        'U' => 56,
        'V' => 57,
        'W' => 58,
        'X' => 59,
        'Y' => 60,
        'Z' => 61,
        '-' => 62,
        '_' => 63,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), $"Char '{ch}' is not Base64Num")
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
        'a' => 10,
        'b' => 11,
        'c' => 12,
        'd' => 13,
        'e' => 14,
        'f' => 15,
        'g' => 16,
        'h' => 17,
        'i' => 18,
        'j' => 19,
        'k' => 20,
        'l' => 21,
        'm' => 22,
        'n' => 23,
        'o' => 24,
        'p' => 25,
        'q' => 26,
        'r' => 27,
        's' => 28,
        't' => 29,
        'u' => 30,
        'v' => 31,
        'w' => 32,
        'x' => 33,
        'y' => 34,
        'z' => 35,
        'A' => 36,
        'B' => 37,
        'C' => 38,
        'D' => 39,
        'E' => 40,
        'F' => 41,
        'G' => 42,
        'H' => 43,
        'I' => 44,
        'J' => 45,
        'K' => 46,
        'L' => 47,
        'M' => 48,
        'N' => 49,
        'O' => 50,
        'P' => 51,
        'Q' => 52,
        'R' => 53,
        'S' => 54,
        'T' => 55,
        'U' => 56,
        'V' => 57,
        'W' => 58,
        'X' => 59,
        'Y' => 60,
        'Z' => 61,
        '-' => 62,
        '_' => 63,
        _ => throw new ArgumentOutOfRangeException(nameof(ch), $"Char '{ch}' is not Base64Num")
    };

    #endregion Numbers
}