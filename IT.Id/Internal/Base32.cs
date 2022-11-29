using System.Text;

namespace Internal;

internal static class Base32
{
    public const char FormatLower = 'v';
    public const char FormatUpper = 'V';
    public const int Min = 48;
    public const int Max = 122;

    public static readonly string LowerAlphabet = "0123456789abcdefghjkmnpqrstvwxyz";
    public static readonly byte[] LowerEncodeMap = Encoding.UTF8.GetBytes(LowerAlphabet);

    public static readonly string UpperAlphabet = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    public static readonly byte[] UpperEncodeMap = Encoding.UTF8.GetBytes(UpperAlphabet);

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
       255, //33
       255, //34
       255, //35
       255, //36
       255, //37
       255, //38
       255, //39
       255, //40
       255, //41
       255, //42
       255, //43
       255, //44
       255, //45
       255, //46
       255, //47
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
       255, //58
       255, //59
       255, //60
       255, //61
       255, //62
       255, //63
       255, //64
        10, //65 -> A
        11, //66 -> B
        12, //67 -> C
        13, //68 -> D
        14, //69 -> E
        15, //70 -> F
        16, //71 -> G
        17, //72 -> H
         1, //73 -> I
        18, //74 -> J
        19, //75 -> K
         1, //76 -> L
        20, //77 -> M
        21, //78 -> N
         0, //79 -> O
        22, //80 -> P
        23, //81 -> Q
        24, //82 -> R
        25, //83 -> S
        26, //84 -> T
        27, //85 -> U
        27, //86 -> V
        28, //87 -> W
        29, //88 -> X
        30, //89 -> Y
        31, //90 -> Z
       255, //91
       255, //92
       255, //93
       255, //94
       255, //95
       255, //96
        10, //97 -> a
        11, //98 -> b
        12, //99 -> c
        13, //100 -> d
        14, //101 -> e
        15, //102 -> f
        16, //103 -> g
        17, //104 -> h
         1, //105 -> i
        18, //106 -> j
        19, //107 -> k
         1, //108 -> l
        20, //109 -> m
        21, //110 -> n
         0, //111 -> o
        22, //112 -> p
        23, //113 -> q
        24, //114 -> r
        25, //115 -> s
        26, //116 -> t
        27, //117 -> u
        27, //118 -> v
        28, //119 -> w
        29, //120 -> x
        30, //121 -> y
        31, //122 -> z
       255, //123
       255, //124
       255, //125
       255, //126
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
}

//static Base32()
//{
//    var table = new sbyte[123];

//    for (int i = 0; i < table.Length; i++)
//        table[i] = LookupTableNullItem;

//    for (sbyte i = 0; i < Chars.Length; i++)
//    {
//        var ch = Chars[i];

//        var chlower = Char.ToLowerInvariant(ch);

//        table[ch] = i;

//        table[chlower] = i;

//        if (ch == '0')
//        {
//            table['o'] = i;
//            table['O'] = i;
//        }

//        if (ch == 'V')
//        {
//            table['u'] = i;
//            table['U'] = i;
//        }

//        if (ch == '1')
//        {
//            table['i'] = i;
//            table['I'] = i;
//            table['l'] = i;
//            table['L'] = i;
//        }
//    }
//    Console.WriteLine("new sbyte[] {");
//    for (int i = 0; i < table.Length; i++)
//    {
//        var code = table[i];
//        if (code == -1)
//            Console.WriteLine($"-1, //{i}");
//        else
//            Console.WriteLine($"{code,2}, //{i} -> {(char)i}");
//    }
//    Console.WriteLine("}");
//    _lookupValues = table;
//}

//    public static String Encode6(ReadOnlySpan<byte> bytes)
//    {
//#if NETSTANDARD2_0
//        throw new NotImplementedException();
//#else
//        unsafe
//        {
//            fixed (byte* dataPtr = bytes)
//            {
//                return String.Create(20, (IntPtr)dataPtr, (encoded, state) =>
//                {
//                    Encode(new ReadOnlySpan<Byte>((Byte*)state, 12), encoded);
//                });
//            }
//        }
//#endif
//    }

//public static unsafe void Encode(ReadOnlySpan<byte> input, Span<char> output)
//{
//    fixed (byte* pInput = input)
//    fixed (char* pOutput = output)
//    fixed (char* pAlphabet = Alphabet)
//    {
//        ToBase32GroupsUnsafe(pInput, pOutput, pAlphabet);
//    }
//}

//private static unsafe void ToBase32GroupsUnsafe(byte* pInput, char* pOutput, char* pAlphabet)
//{
//    ulong value = *pInput++;
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);

//    *pOutput++ = pAlphabet[value >> 35];
//    *pOutput++ = pAlphabet[(value >> 30) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 25) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 20) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 15) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
//    *pOutput++ = pAlphabet[value & 0x1F];

//    value = *pInput++;
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);
//    value = (value << 8) | (*pInput++);

//    *pOutput++ = pAlphabet[value >> 35];
//    *pOutput++ = pAlphabet[(value >> 30) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 25) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 20) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 15) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
//    *pOutput++ = pAlphabet[value & 0x1F];

//    value = (((ulong)(*pInput++) << 8) | *pInput) << 4;

//    *pOutput++ = pAlphabet[value >> 15];
//    *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
//    *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
//    *pOutput = pAlphabet[value & 0x1F];
//}

//public static unsafe void Decode(ReadOnlySpan<char> encoded, Span<byte> output)
//{
//    fixed (char* pEncoded = encoded)
//    fixed (byte* pOutput = output)
//    {
//        ToBytesGroupsUnsafe(pEncoded, pOutput);
//    }
//}

//private static unsafe void ToBytesGroupsUnsafe(char* pEncoded, byte* pOutput)
//{
//    ulong value = GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);

//    *pOutput++ = (byte)(value >> 32);
//    *pOutput++ = (byte)(value >> 24);
//    *pOutput++ = (byte)(value >> 16);
//    *pOutput++ = (byte)(value >> 8);
//    *pOutput++ = (byte)value;

//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);

//    *pOutput++ = (byte)(value >> 32);
//    *pOutput++ = (byte)(value >> 24);
//    *pOutput++ = (byte)(value >> 16);
//    *pOutput++ = (byte)(value >> 8);
//    *pOutput++ = (byte)value;

//    value = GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded++);
//    value = (value << 5) | GetByte(*pEncoded);

//    *pOutput++ = (byte)(value >> 12);
//    *pOutput = (byte)(value >> 4);
//}