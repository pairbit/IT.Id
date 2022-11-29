using System.Text;

namespace Internal;

internal static class Base85
{
    public const char Format = '|';
    public const int Min = 33;
    public const int Max = 126;

    public static readonly string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$#";

    public static readonly byte[] EncodeMap = Encoding.UTF8.GetBytes(Alphabet);

    public static readonly byte[] DecodeMap = new byte[] {
       255, //0
       255, //1
       255, //2
       255, //3
       255, //4
       255, //5
       255, //6
       255, //7
       255, //8
       255, //9
       255, //10
       255, //11
       255, //12
       255, //13
       255, //14
       255, //15
       255, //16
       255, //17
       255, //18
       255, //19
       255, //20
       255, //21
       255, //22
       255, //23
       255, //24
       255, //25
       255, //26
       255, //27
       255, //28
       255, //29
       255, //30
       255, //31
       255, //32
        68, //33 -> !
       255, //34
        84, //35 -> #
        83, //36 -> $
        82, //37 -> %
        72, //38 -> &
       255, //39
        75, //40 -> (
        76, //41 -> )
        70, //42 -> *
        65, //43 -> +
       255, //44
        63, //45 -> -
        62, //46 -> .
        69, //47 -> /
         0, //48 -> 0
         1, //49 -> 1
         2, //50 -> 2
         3, //51 -> 3
         4, //52 -> 4
         5, //53 -> 5
         6, //54 -> 6
         7, //55 -> 7
         8, //56 -> 8
         9, //57 -> 9
        64, //58 -> :
       255, //59
        73, //60 -> <
        66, //61 -> =
        74, //62 -> >
        71, //63 -> ?
        81, //64 -> @
        36, //65 -> A
        37, //66 -> B
        38, //67 -> C
        39, //68 -> D
        40, //69 -> E
        41, //70 -> F
        42, //71 -> G
        43, //72 -> H
        44, //73 -> I
        45, //74 -> J
        46, //75 -> K
        47, //76 -> L
        48, //77 -> M
        49, //78 -> N
        50, //79 -> O
        51, //80 -> P
        52, //81 -> Q
        53, //82 -> R
        54, //83 -> S
        55, //84 -> T
        56, //85 -> U
        57, //86 -> V
        58, //87 -> W
        59, //88 -> X
        60, //89 -> Y
        61, //90 -> Z
        77, //91 -> [
       255, //92
        78, //93 -> ]
        67, //94 -> ^
        72, //95 -> _
       255, //96
        10, //97 -> a
        11, //98 -> b
        12, //99 -> c
        13, //100 -> d
        14, //101 -> e
        15, //102 -> f
        16, //103 -> g
        17, //104 -> h
        18, //105 -> i
        19, //106 -> j
        20, //107 -> k
        21, //108 -> l
        22, //109 -> m
        23, //110 -> n
        24, //111 -> o
        25, //112 -> p
        26, //113 -> q
        27, //114 -> r
        28, //115 -> s
        29, //116 -> t
        30, //117 -> u
        31, //118 -> v
        32, //119 -> w
        33, //120 -> x
        34, //121 -> y
        35, //122 -> z
        79, //123 -> {
        74, //124 -> |
        80, //125 -> }
        73, //126 -> ~
       255, //127
       255, //128
       255, //129
       255, //130
       255, //131
       255, //132
       255, //133
       255, //134
       255, //135
       255, //136
       255, //137
       255, //138
       255, //139
       255, //140
       255, //141
       255, //142
       255, //143
       255, //144
       255, //145
       255, //146
       255, //147
       255, //148
       255, //149
       255, //150
       255, //151
       255, //152
       255, //153
       255, //154
       255, //155
       255, //156
       255, //157
       255, //158
       255, //159
       255, //160
       255, //161
       255, //162
       255, //163
       255, //164
       255, //165
       255, //166
       255, //167
       255, //168
       255, //169
       255, //170
       255, //171
       255, //172
       255, //173
       255, //174
       255, //175
       255, //176
       255, //177
       255, //178
       255, //179
       255, //180
       255, //181
       255, //182
       255, //183
       255, //184
       255, //185
       255, //186
       255, //187
       255, //188
       255, //189
       255, //190
       255, //191
       255, //192
       255, //193
       255, //194
       255, //195
       255, //196
       255, //197
       255, //198
       255, //199
       255, //200
       255, //201
       255, //202
       255, //203
       255, //204
       255, //205
       255, //206
       255, //207
       255, //208
       255, //209
       255, //210
       255, //211
       255, //212
       255, //213
       255, //214
       255, //215
       255, //216
       255, //217
       255, //218
       255, //219
       255, //220
       255, //221
       255, //222
       255, //223
       255, //224
       255, //225
       255, //226
       255, //227
       255, //228
       255, //229
       255, //230
       255, //231
       255, //232
       255, //233
       255, //234
       255, //235
       255, //236
       255, //237
       255, //238
       255, //239
       255, //240
       255, //241
       255, //242
       255, //243
       255, //244
       255, //245
       255, //246
       255, //247
       255, //248
       255, //249
       255, //250
       255, //251
       255, //252
       255, //253
       255, //254
       255, //255
    };

    //static Base85()
    //{
    //    //BuildDict(Alphabet);

    //    for (sbyte i = 0; i < DecodeMap.Length; i++)
    //    {
    //        var ch = (char)i;
    //        if (ch == '_')
    //        {
    //            DecodeMap['&'] = DecodeMap[i];
    //        }
    //        if (ch == '~')
    //        {
    //            DecodeMap['<'] = DecodeMap[i];
    //        }
    //        if (ch == '|')
    //        {
    //            DecodeMap['>'] = DecodeMap[i];
    //        }
    //    }

    //    Console.WriteLine("{");
    //    for (int i = 0; i < DecodeMap.Length; i++)
    //    {
    //        var code = DecodeMap[i];

    //        if (code == -1)
    //            Console.WriteLine($"{code,2}, //{i}");
    //        else
    //            Console.WriteLine($"{code,2}, //{i} -> {(char)i}");
    //    }
    //    Console.WriteLine("};");
    //}

    //private static void BuildDict(string digits)
    //{
    //    for (int i = 0; i < _char2Byte.Length; i++)
    //    {
    //        _char2Byte[i] = -1;
    //    }

    //    for (var i = 0; i < digits.Length; i++)
    //    {
    //        var c = digits[i];
    //        var d = (sbyte)i;
    //        if (c > MAX_DIGIT) throw new ArgumentException($"Invalid character '{c}'");

    //        _byte2Byte[d] = (byte)c;
    //        _byte2Char[d] = c;
    //        _char2Byte[c] = d;
    //    }
    //}

    //private const uint U85P1 = 85u;
    //private const uint U85P2 = 85u * 85u;
    //private const uint U85P3 = 85u * 85u * 85u;
    //private const uint U85P4 = 85u * 85u * 85u * 85u;


    //public static unsafe void Decode(ReadOnlySpan<char> source, Span<byte> target)
    //{
    //	fixed (char* sourceP = source)
    //	fixed (byte* targetP = target)
    //	fixed (byte* map = _char2Byte)
    //	{
    //		var value0 = Decode1(map, *sourceP) * U85P4 +
    //					 Decode1(map, *(sourceP + 1)) * U85P3 +
    //					 Decode1(map, *(sourceP + 2)) * U85P2 +
    //					 Decode1(map, *(sourceP + 3)) * U85P1 +
    //					 Decode1(map, *(sourceP + 4));

    //		*(targetP + 0) = (byte)(value0 >> 24);
    //		*(targetP + 1) = (byte)(value0 >> 16);
    //		*(targetP + 2) = (byte)(value0 >> 8);
    //		*(targetP + 3) = (byte)value0;

    //		var value1 = Decode1(map, *(sourceP + 5)) * U85P4 +
    //					 Decode1(map, *(sourceP + 6)) * U85P3 +
    //					 Decode1(map, *(sourceP + 7)) * U85P2 +
    //					 Decode1(map, *(sourceP + 8)) * U85P1 +
    //					 Decode1(map, *(sourceP + 9));

    //		*(targetP + 4) = (byte)(value1 >> 24);
    //		*(targetP + 5) = (byte)(value1 >> 16);
    //		*(targetP + 6) = (byte)(value1 >> 8);
    //		*(targetP + 7) = (byte)value1;

    //		var value2 = Decode1(map, *(sourceP + 10)) * U85P4 +
    //					 Decode1(map, *(sourceP + 11)) * U85P3 +
    //					 Decode1(map, *(sourceP + 12)) * U85P2 +
    //					 Decode1(map, *(sourceP + 13)) * U85P1 +
    //					 Decode1(map, *(sourceP + 14));

    //		*(targetP + 8) = (byte)(value2 >> 24);
    //		*(targetP + 9) = (byte)(value2 >> 16);
    //		*(targetP + 10) = (byte)(value2 >> 8);
    //		*(targetP + 11) = (byte)value2;
    //	}
    //}

    //public static unsafe void Decode(ReadOnlySpan<byte> source, Span<byte> target)
    //{
    //	fixed (byte* sourceP = source)
    //	fixed (byte* targetP = target)
    //	fixed (byte* map = _char2Byte)
    //	{
    //		var value0 = *(map + *sourceP) * U85P4 +
    //					 *(map + *(sourceP + 1)) * U85P3 +
    //					 *(map + *(sourceP + 2)) * U85P2 +
    //					 *(map + *(sourceP + 3)) * U85P1 +
    //					 *(map + *(sourceP + 4));

    //		*(targetP + 0) = (byte)(value0 >> 24);
    //		*(targetP + 1) = (byte)(value0 >> 16);
    //		*(targetP + 2) = (byte)(value0 >> 8);
    //		*(targetP + 3) = (byte)value0;

    //		var value1 = *(map + *(sourceP + 5)) * U85P4 +
    //					 *(map + *(sourceP + 6)) * U85P3 +
    //					 *(map + *(sourceP + 7)) * U85P2 +
    //					 *(map + *(sourceP + 8)) * U85P1 +
    //					 *(map + *(sourceP + 9));

    //		*(targetP + 4) = (byte)(value1 >> 24);
    //		*(targetP + 5) = (byte)(value1 >> 16);
    //		*(targetP + 6) = (byte)(value1 >> 8);
    //		*(targetP + 7) = (byte)value1;

    //		var value2 = *(map + *(sourceP + 10)) * U85P4 +
    //					 *(map + *(sourceP + 11)) * U85P3 +
    //					 *(map + *(sourceP + 12)) * U85P2 +
    //					 *(map + *(sourceP + 13)) * U85P1 +
    //					 *(map + *(sourceP + 14));

    //		*(targetP + 8) = (byte)(value2 >> 24);
    //		*(targetP + 9) = (byte)(value2 >> 16);
    //		*(targetP + 10) = (byte)(value2 >> 8);
    //		*(targetP + 11) = (byte)value2;
    //	}
    //}

    //public static unsafe void Encode(ReadOnlySpan<byte> source, Span<char> target)
    //{
    //	fixed (byte* sourceP = source)
    //	fixed (char* targetP = target)
    //		Encode(sourceP, targetP);
    //}

    //public static unsafe void Encode(byte* sourceP, char* targetP)
    //{
    //	fixed (char* map = ByteToChar)
    //	{
    //		var value0 = (uint)*(sourceP + 0) << 24 | (uint)*(sourceP + 1) << 16 | (uint)*(sourceP + 2) << 8 | *(sourceP + 3);

    //		*(targetP + 0) = *(map + (value0 / U85P4).Mod85());
    //		*(targetP + 1) = *(map + (value0 / U85P3).Mod85());
    //		*(targetP + 2) = *(map + (value0 / U85P2).Mod85());
    //		*(targetP + 3) = *(map + (value0 / U85P1).Mod85());
    //		*(targetP + 4) = *(map + value0.Mod85());

    //		var value1 = (uint)*(sourceP + 4) << 24 | (uint)*(sourceP + 5) << 16 | (uint)*(sourceP + 6) << 8 | *(sourceP + 7);

    //		*(targetP + 5) = *(map + (value1 / U85P4).Mod85());
    //		*(targetP + 6) = *(map + (value1 / U85P3).Mod85());
    //		*(targetP + 7) = *(map + (value1 / U85P2).Mod85());
    //		*(targetP + 8) = *(map + (value1 / U85P1).Mod85());
    //		*(targetP + 9) = *(map + value1.Mod85());

    //		var value2 = (uint)*(sourceP + 8) << 24 | (uint)*(sourceP + 9) << 16 | (uint)*(sourceP + 10) << 8 | *(sourceP + 11);

    //		*(targetP + 10) = *(map + (value2 / U85P4).Mod85());
    //		*(targetP + 11) = *(map + (value2 / U85P3).Mod85());
    //		*(targetP + 12) = *(map + (value2 / U85P2).Mod85());
    //		*(targetP + 13) = *(map + (value2 / U85P1).Mod85());
    //		*(targetP + 14) = *(map + value2.Mod85());
    //	}
    //}

    //public static unsafe void Encode(ReadOnlySpan<byte> source, Span<byte> target)
    //{
    //	fixed (byte* sourceP = source)
    //	fixed (byte* targetP = target)
    //	fixed (byte* map = _byte2Byte)
    //	{
    //		var value0 = (uint)*(sourceP + 0) << 24 | (uint)*(sourceP + 1) << 16 | (uint)*(sourceP + 2) << 8 | *(sourceP + 3);

    //		*(targetP + 0) = *(map + (value0 / U85P4).Mod85());
    //		*(targetP + 1) = *(map + (value0 / U85P3).Mod85());
    //		*(targetP + 2) = *(map + (value0 / U85P2).Mod85());
    //		*(targetP + 3) = *(map + (value0 / U85P1).Mod85());
    //		*(targetP + 4) = *(map + value0.Mod85());

    //		var value1 = (uint)*(sourceP + 4) << 24 | (uint)*(sourceP + 5) << 16 | (uint)*(sourceP + 6) << 8 | *(sourceP + 7);

    //		*(targetP + 5) = *(map + (value1 / U85P4).Mod85());
    //		*(targetP + 6) = *(map + (value1 / U85P3).Mod85());
    //		*(targetP + 7) = *(map + (value1 / U85P2).Mod85());
    //		*(targetP + 8) = *(map + (value1 / U85P1).Mod85());
    //		*(targetP + 9) = *(map + value1.Mod85());

    //		var value2 = (uint)*(sourceP + 8) << 24 | (uint)*(sourceP + 9) << 16 | (uint)*(sourceP + 10) << 8 | *(sourceP + 11);

    //		*(targetP + 10) = *(map + (value2 / U85P4).Mod85());
    //		*(targetP + 11) = *(map + (value2 / U85P3).Mod85());
    //		*(targetP + 12) = *(map + (value2 / U85P2).Mod85());
    //		*(targetP + 13) = *(map + (value2 / U85P1).Mod85());
    //		*(targetP + 14) = *(map + value2.Mod85());
    //	}
    //}

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static unsafe byte Decode1(byte* map, char c) => *(map + (byte)c);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //private static uint Mod85(this uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}