using System.Text;

namespace IT.Internal;

internal static class Base58
{
    public const char Format = 'i';
    public const int Min = 48;
    public const int Max = 122;

    public static readonly string Alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
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
        46, //48 -> 0
         0, //49 -> 1
         1, //50 -> 2
         2, //51 -> 3
         3, //52 -> 4
         4, //53 -> 5
         5, //54 -> 6
         6, //55 -> 7
         7, //56 -> 8
         8, //57 -> 9
       255, //58
       255, //59
       255, //60
       255, //61
       255, //62
       255, //63
       255, //64
         9, //65 -> A
        10, //66 -> B
        11, //67 -> C
        12, //68 -> D
        13, //69 -> E
        14, //70 -> F
        15, //71 -> G
        16, //72 -> H
         0, //73 -> I
        17, //74 -> J
        18, //75 -> K
        19, //76 -> L
        20, //77 -> M
        21, //78 -> N
        46, //79 -> O
        22, //80 -> P
        23, //81 -> Q
        24, //82 -> R
        25, //83 -> S
        26, //84 -> T
        27, //85 -> U
        28, //86 -> V
        29, //87 -> W
        30, //88 -> X
        31, //89 -> Y
        32, //90 -> Z
       255, //91
       255, //92
       255, //93
       255, //94
       255, //95
       255, //96
        33, //97 -> a
        34, //98 -> b
        35, //99 -> c
        36, //100 -> d
        37, //101 -> e
        38, //102 -> f
        39, //103 -> g
        40, //104 -> h
        41, //105 -> i
        42, //106 -> j
        43, //107 -> k
         0, //108 -> l
        44, //109 -> m
        45, //110 -> n
        46, //111 -> o
        47, //112 -> p
        48, //113 -> q
        49, //114 -> r
        50, //115 -> s
        51, //116 -> t
        52, //117 -> u
        53, //118 -> v
        54, //119 -> w
        55, //120 -> x
        56, //121 -> y
        57, //122 -> z
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

//private const int reductionFactor = 733; // https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp#L48
//private const string Zero = "11111111111111111";
//private const Char ZeroChar = '1';


//private static readonly char _zeroChar;

//static Base58()
//{
//    _zeroChar = _alphabet[0];

//    var lookupTable = new sbyte[Max + 1];

//    for (int i = 0; i < lookupTable.Length; i++)
//    {
//        lookupTable[i] = -1;
//    }

//    for (sbyte i = 0; i < _alphabet.Length; i++)
//    {
//        var ch = _alphabet[i];

//        lookupTable[ch] = i;

//        if (ch == '1')
//        {
//            lookupTable['I'] = i;
//            lookupTable['l'] = i;
//        }

//        if (ch == 'o')
//        {
//            lookupTable['0'] = i;
//            lookupTable['O'] = i;
//        }
//    }

//    Console.WriteLine("new sbyte[] {");
//    for (int i = 0; i < lookupTable.Length; i++)
//    {
//        var code = lookupTable[i];
//        if (code == -1)
//            Console.WriteLine($"-1, //{i}");
//        else
//            Console.WriteLine($"{code,2}, //{i} -> {(char)i}");
//    }
//    Console.WriteLine("}");

//    _lookupTable = lookupTable;
//}

//public static int GetSafeByteCountForDecoding(int textLen, int numZeroes)
//{
//    //Debug.Assert(textLen >= numZeroes, "Number of zeroes cannot be longer than text length");
//    return numZeroes + ((textLen - numZeroes + 1) * reductionFactor / 1000) + 1;
//}

//public static unsafe string Encode(ReadOnlySpan<byte> bytes)
//{
//    int numZeroes = getZeroCount(bytes, 12);
//    if (numZeroes == 12) return Zero;
//    /*
//     0,1 -> 17
//     2,4 -> 16
//     5,6 -> 15
//     7,8,9 -> 14
//     10,11 -> 13
//     12 -> 12
//     */
//    //int outputLen = numZeroes + ((12 - numZeroes) * 138 / 100) + 1;
//    var output = new string('\0', 17);

//    // 29.70µs (64.9x slower)   | 31.63µs (40.8x slower)
//    // 30.93µs (first tryencode impl)
//    // 29.36µs (single pass translation/copy + shift over multiply)
//    // 31.04µs (70x slower)     | 24.71µs (34.3x slower)
//    fixed (byte* inputPtr = bytes)
//    fixed (char* outputPtr = output)
//    {
//        if (!internalEncode(inputPtr, outputPtr, numZeroes, out int length))
//            throw new InvalidOperationException("Output buffer with insufficient size generated");

//        if (length != 17)
//        {
//            output = new string(ZeroChar, 17 - length) + output.Substring(0, length);
//        }

//        return output;
//    }
//}

///// <inheritdoc/>
//public static unsafe bool Decode_Manual(ReadOnlySpan<char> input, Span<byte> output)
//{
//    fixed (char* inputPtr = input)
//    fixed (byte* outputPtr = output)
//    {
//        return internalDecode_Manual(inputPtr, input.Length, outputPtr);
//    }
//}

//private static unsafe bool internalDecode_Manual(char* input, int inputLen, byte* output)
//{
//    var table = _lookupTable;

//    char* inputEnd = input + inputLen;
//    byte* outputEnd = output + 11;

//    while (input != inputEnd)
//    {
//        char c = *input++;

//        int carry = table[c] - 1;

//        if (carry < 0) throw new ArgumentException($"Invalid character: {c}");

//        byte* p = outputEnd;

//        //1
//        carry += 58 * *p;
//        *p-- = (byte)carry;

//        //2
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //3
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //4
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //5
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //6
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //7
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //8
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //9
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //10
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //11
//        carry = (carry >> 8) + (58 * *p);
//        *p-- = (byte)carry;

//        //12
//        *p = (byte)((carry >> 8) + (58 * *p));
//    }

//    return true;
//}

//public static unsafe bool Decode(ReadOnlySpan<char> input, Span<byte> output, out int numBytesWritten)
//{
//    int zeroCount = getPrefixCount(input, input.Length, _zeroChar);
//    fixed (char* inputPtr = input)
//    fixed (byte* outputPtr = output)
//    {
//        return internalDecode(
//            inputPtr,
//            input.Length,
//            outputPtr,
//            output.Length,
//            zeroCount, out numBytesWritten);
//    }
//}

//private static unsafe bool internalDecode(
//        char* inputPtr,
//        int inputLen,
//        byte* outputPtr,
//        int outputLen,
//        int numZeroes,
//        out int numBytesWritten)
//{
//    char* pInputEnd = inputPtr + inputLen;
//    char* pInput = inputPtr + numZeroes;
//    if (pInput == pInputEnd)
//    {
//        if (numZeroes > outputLen)
//        {
//            numBytesWritten = 0;
//            return false;
//        }

//        byte* pOutput = outputPtr;
//        for (int i = 0; i < numZeroes; i++)
//        {
//            *pOutput++ = 0;
//        }

//        numBytesWritten = numZeroes;
//        return true;
//    }

//    var table = _lookupTable;
//    byte* pOutputEnd = outputPtr + outputLen - 1;
//    byte* pMinOutput = pOutputEnd;
//    while (pInput != pInputEnd)
//    {
//        char c = *pInput;
//        int carry = table[c] - 1;
//        if (carry < 0)
//        {
//            throw new ArgumentException($"Invalid character: {c}");
//        }

//        for (byte* pOutput = pOutputEnd; pOutput >= outputPtr; pOutput--)
//        {
//            carry += 58 * (*pOutput);
//            *pOutput = (byte)carry;
//            if (pMinOutput > pOutput && carry != 0)
//            {
//                pMinOutput = pOutput;
//            }

//            carry >>= 8;
//        }

//        pInput++;
//    }

//    int startIndex = (int)(pMinOutput - numZeroes - outputPtr);
//    numBytesWritten = outputLen - startIndex;
//    Buffer.MemoryCopy(outputPtr + startIndex, outputPtr, numBytesWritten, numBytesWritten);
//    return true;
//}

////private bool internalDecode_NEW(
////ReadOnlySpan<char> input,
////Span<byte> output,
////int numZeroes,
////out int numBytesWritten)
////{
////    if (numZeroes == input.Length)
////    {
////        return decodeZeroes(output, numZeroes, out numBytesWritten);
////    }

////    var table = _lookupTable;
////    int min = output.Length - 1;
////    for (int i = 0; i < input.Length; i++)
////    {
////        char c = input[i];
////        int carry = table[c] - 1;
////        if (carry < 0)
////        {
////            throw new ArgumentException($"Invalid character: {c}");
////        }

////        for (int o = output.Length - 1; o >= 0; o--)
////        {
////            carry += 58 * output[o];
////            output[o] = (byte)carry;
////            if (min > o && carry != 0)
////            {
////                min = o;
////            }

////            carry >>= 8;
////        }
////    }

////    int startIndex = min - numZeroes;
////    numBytesWritten = output.Length - startIndex;
////    output[startIndex..].CopyTo(output[..numBytesWritten]);
////    return true;
////}

////private static bool decodeZeroes(Span<byte> output, int length, out int numBytesWritten)
////{
////    if (length > output.Length)
////    {
////        numBytesWritten = 0;
////        return false;
////    }

////    output[..length].Fill(0);
////    numBytesWritten = length;
////    return true;
////}

//private static unsafe bool internalEncode(
//    byte* inputPtr,
//    char* outputPtr,
//    int numZeroes,
//    out int numCharsWritten)
//{
//    fixed (char* alphabetPtr = _alphabet)
//    {
//        byte* pInput = inputPtr + numZeroes;
//        byte* pInputEnd = inputPtr + 12;

//        int length = 0;
//        char* pOutput = outputPtr;
//        char* pLastChar = pOutput + 16;

//        while (pInput != pInputEnd)
//        {
//            int carry = *pInput;
//            int i = 0;
//            for (char* pDigit = pLastChar; (carry != 0 || i < length) && pDigit >= outputPtr; pDigit--, i++)
//            {
//                carry += *pDigit << 8;
//                carry = Math.DivRem(carry, 58, out int remainder);
//                *pDigit = (char)remainder;
//            }

//            length = i;
//            pInput++;
//        }

//        var pOutputEnd = pOutput + 17;

//        // copy the characters to the beginning of the buffer
//        // and translate them at the same time. if no copying
//        // is needed, this only acts as the translation phase.
//        for (char* a = outputPtr + numZeroes, b = pOutputEnd - length;
//            b != pOutputEnd;
//            a++, b++)
//        {
//            *a = alphabetPtr[*b];
//        }

//        // translate the zeroes at the start
//        while (pOutput != pOutputEnd)
//        {
//            char c = *pOutput;
//            if (c != '\0')
//            {
//                break;
//            }

//            *pOutput = alphabetPtr[c];
//            pOutput++;
//        }

//        int actualLen = numZeroes + length;

//        numCharsWritten = actualLen;
//        return true;
//    }
//}

//private static unsafe bool internalEncode_FAST(byte* input, char* output, out int written)
//{
//    fixed (char* alphabetPtr = _alphabet)
//    {
//        byte* pInputEnd = input + 12;

//        int length = 0;
//        char* lastChar = output + 16;

//        //Console.Write($"{length} | ");

//        while (input != pInputEnd)
//        {
//            int carry = *input++;
//            int i = 0;
//            for (char* pDigit = lastChar; (carry != 0 || i < length) && pDigit >= output; pDigit--, i++)
//            {
//                carry = Math.DivRem(carry + (*pDigit << 8), 58, out int remainder);
//                *pDigit = (char)remainder;
//            }

//            length = i;

//            //Console.Write($"{length} | ");
//        }

//        //Console.WriteLine();

//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output++];
//        *output = alphabetPtr[*output];

//        written = length;

//        return true;
//    }
//}

//public static string Encode_New(ReadOnlySpan<byte> bytes)
//{
//    int numZeroes = getZeroCount(bytes, 12);
//    if (numZeroes == 12) return Zero;

//    Span<char> output = stackalloc char[17];

//    if (!internalEncode(bytes, output, numZeroes, out int length))
//        throw new InvalidOperationException("Output buffer with insufficient size generated");

//    if (length != 17)
//    {
//        return new string(ZeroChar, 17 - length) + output.Slice(0, length).ToString();
//    }

//    return output.ToString();
//}

//private static bool internalEncode(
//    ReadOnlySpan<byte> input,
//    Span<char> output,
//    int numZeroes,
//    out int numCharsWritten)
//{
//    ReadOnlySpan<char> alphabet = _alphabet.AsSpan();

//    int numDigits = 0;
//    int index = numZeroes;
//    while (index < input.Length)
//    {
//        int carry = input[index++];
//        int i = 0;
//        for (int j = output.Length - 1; (carry != 0 || i < numDigits) && j >= 0; j--, i++)
//        {
//            carry += output[j] << 8;
//            carry = Math.DivRem(carry, 58, out int remainder);
//            output[j] = (char)remainder;
//        }

//        numDigits = i;
//    }

//    translatedCopy(output.Slice(output.Length - numDigits), output.Slice(numZeroes), alphabet);
//    if (numZeroes > 0)
//    {
//        output.Slice(0, numZeroes).Fill(alphabet[0]);
//    }

//    numCharsWritten = numZeroes + numDigits;
//    return true;
//}

//private static void translatedCopy(
//ReadOnlySpan<char> source,
//Span<char> destination,
//ReadOnlySpan<char> alphabet)
//{
//    Debug.Assert(source.Length <= destination.Length, "source is too big");
//    for (int n = 0; n < source.Length; n++)
//    {
//        destination[n] = alphabet[source[n]];
//    }
//}

//private static unsafe int getZeroCount(ReadOnlySpan<byte> bytes, int bytesLen)
//{
//    int numZeroes = 0;
//    fixed (byte* inputPtr = bytes)
//    {
//        var pInput = inputPtr;
//        while (*pInput == 0 && numZeroes < bytesLen)
//        {
//            numZeroes++;
//            pInput++;
//        }
//    }

//    return numZeroes;
//}

//private static unsafe int getPrefixCount(ReadOnlySpan<char> input, int length, char value)
//{
//    if (length == 0)
//    {
//        return 0;
//    }

//    int numZeroes = 0;
//    fixed (char* inputPtr = input)
//    {
//        var pInput = inputPtr;
//        while (*pInput == value && numZeroes < length)
//        {
//            numZeroes++;
//            pInput++;
//        }
//    }

//    return numZeroes;
//}