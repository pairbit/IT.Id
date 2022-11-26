using System.Text;

namespace Internal;

internal static class Base85
{
    public const int Min = 33;
    public const int Max = 126;

    public static readonly string Alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$#";

    public static readonly byte[] EncodeMap = Encoding.UTF8.GetBytes(Alphabet);

    public static readonly sbyte[] DecodeMap = new sbyte[] {
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
    68, //33 -> !
    -1, //34
    84, //35 -> #
    83, //36 -> $
    82, //37 -> %
    72, //38 -> &
    -1, //39
    75, //40 -> (
    76, //41 -> )
    70, //42 -> *
    65, //43 -> +
    -1, //44
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
    -1, //59
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
    -1, //92
    78, //93 -> ]
    67, //94 -> ^
    72, //95 -> _
    -1, //96
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