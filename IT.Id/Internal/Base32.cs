using System.Runtime.CompilerServices;
using System.Text;

namespace System;

internal static class Base32
{
    private static readonly sbyte[] _map = new sbyte[] {
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
    -1, //43
    -1, //44
    -1, //45
    -1, //46
    -1, //47
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
    -1, //58
    -1, //59
    -1, //60
    -1, //61
    -1, //62
    -1, //63
    -1, //64
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
    -1, //91
    -1, //92
    -1, //93
    -1, //94
    -1, //95
    -1, //96
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
    };

    internal static readonly string ALPHABET = "0123456789ABCDEFGHJKMNPQRSTVWXYZ";
    internal static readonly char[] Chars = ALPHABET.ToCharArray();
    internal static readonly byte[] Bytes = Encoding.UTF8.GetBytes(ALPHABET);

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

    public static unsafe void Encode(ReadOnlySpan<byte> input, Span<char> output)
    {
        fixed (byte* pInput = input)
        fixed (char* pOutput = output)
        fixed (char* pAlphabet = ALPHABET)
        {
            ToBase32GroupsUnsafe(pInput, pOutput, pAlphabet);
        }
    }

    private static unsafe void ToBase32GroupsUnsafe(byte* pInput, char* pOutput, char* pAlphabet)
    {
        ulong value = *pInput++;
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);

        *pOutput++ = pAlphabet[value >> 35];
        *pOutput++ = pAlphabet[(value >> 30) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 25) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 20) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 15) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
        *pOutput++ = pAlphabet[value & 0x1F];

        value = *pInput++;
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);
        value = (value << 8) | (*pInput++);

        *pOutput++ = pAlphabet[value >> 35];
        *pOutput++ = pAlphabet[(value >> 30) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 25) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 20) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 15) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
        *pOutput++ = pAlphabet[value & 0x1F];

        value = (((ulong)(*pInput++) << 8) | *pInput) << 4;

        *pOutput++ = pAlphabet[value >> 15];
        *pOutput++ = pAlphabet[(value >> 10) & 0x1F];
        *pOutput++ = pAlphabet[(value >> 5) & 0x1F];
        *pOutput = pAlphabet[value & 0x1F];
    }

    public static unsafe void Decode(ReadOnlySpan<char> encoded, Span<byte> output)
    {
        fixed (char* pEncoded = encoded)
        fixed (byte* pOutput = output)
        {
            ToBytesGroupsUnsafe(pEncoded, pOutput);
        }
    }

    private static unsafe void ToBytesGroupsUnsafe(char* pEncoded, byte* pOutput)
    {
        ulong value = GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);

        *pOutput++ = (byte)(value >> 32);
        *pOutput++ = (byte)(value >> 24);
        *pOutput++ = (byte)(value >> 16);
        *pOutput++ = (byte)(value >> 8);
        *pOutput++ = (byte)value;

        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);

        *pOutput++ = (byte)(value >> 32);
        *pOutput++ = (byte)(value >> 24);
        *pOutput++ = (byte)(value >> 16);
        *pOutput++ = (byte)(value >> 8);
        *pOutput++ = (byte)value;

        value = GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded++);
        value = (value << 5) | GetByte(*pEncoded);

        *pOutput++ = (byte)(value >> 12);
        *pOutput = (byte)(value >> 4);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Byte GetByte(int ch)
    {
        if (ch < 0 || ch >= 123) throw new FormatException($"Char '{(char)ch}' not found");

        var item = _map[ch];

        if (item == -1) throw new FormatException($"Char '{(char)ch}' not found");

        return (byte)item;
    }
}