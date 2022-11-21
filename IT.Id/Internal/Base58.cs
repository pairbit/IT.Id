using System.Diagnostics;

namespace System;

internal static class Base58
{
    private const int AlphabetLength = 58;
    private const int AlphabetMaxLength = 127;
    private const int reductionFactor = 733; // https://github.com/bitcoin/bitcoin/blob/master/src/base58.cpp#L48
    private const string Zero = "11111111111111111";
    private const Char ZeroChar = '1';

    internal static readonly String _alphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";
    private static readonly char _zeroChar;
    private static readonly byte[] _lookupTable;

    static Base58()
    {
        _zeroChar = _alphabet[0];

        var lookupTable = new byte[AlphabetMaxLength];
        for (short i = 0; i < _alphabet.Length; i++)
        {
            var c = _alphabet[i];

            //Debug.Assert(c < AlphabetMaxLength, $"Alphabet contains character above {AlphabetMaxLength}");

            lookupTable[c] = (byte)(i + 1);
        }

        _lookupTable = lookupTable;
    }

    public static int GetSafeByteCountForDecoding(int textLen, int numZeroes)
    {
        //Debug.Assert(textLen >= numZeroes, "Number of zeroes cannot be longer than text length");
        return numZeroes + ((textLen - numZeroes + 1) * reductionFactor / 1000) + 1;
    }

    public static unsafe string Encode(ReadOnlySpan<byte> bytes)
    {
        int numZeroes = getZeroCount(bytes, 12);
        if (numZeroes == 12) return Zero;
        /*
         0,1 -> 17
         2,4 -> 16
         5,6 -> 15
         7,8,9 -> 14
         10,11 -> 13
         12 -> 12
         */
        //int outputLen = numZeroes + ((12 - numZeroes) * 138 / 100) + 1;
        var output = new string('\0', 17);

        // 29.70µs (64.9x slower)   | 31.63µs (40.8x slower)
        // 30.93µs (first tryencode impl)
        // 29.36µs (single pass translation/copy + shift over multiply)
        // 31.04µs (70x slower)     | 24.71µs (34.3x slower)
        fixed (byte* inputPtr = bytes)
        fixed (char* outputPtr = output)
        {
            if (!internalEncode(inputPtr, outputPtr, numZeroes, out int length))
                throw new InvalidOperationException("Output buffer with insufficient size generated");

            if (length != 17)
            {
                output = new string(ZeroChar, 17 - length) + output.Substring(0, length);
            }

            return output;
        }
    }

    /// <inheritdoc/>
    public static unsafe bool Decode(ReadOnlySpan<char> input, Span<byte> output, out int numBytesWritten)
    {
        int zeroCount = getPrefixCount(input, input.Length, _zeroChar);
        fixed (char* inputPtr = input)
        fixed (byte* outputPtr = output)
        {
            return internalDecode(
                inputPtr,
                input.Length,
                outputPtr,
                output.Length,
                zeroCount, out numBytesWritten);
        }
    }

    private static unsafe bool internalDecode(
            char* inputPtr,
            int inputLen,
            byte* outputPtr,
            int outputLen,
            int numZeroes,
            out int numBytesWritten)
    {
        char* pInputEnd = inputPtr + inputLen;
        char* pInput = inputPtr + numZeroes;
        if (pInput == pInputEnd)
        {
            if (numZeroes > outputLen)
            {
                numBytesWritten = 0;
                return false;
            }

            byte* pOutput = outputPtr;
            for (int i = 0; i < numZeroes; i++)
            {
                *pOutput++ = 0;
            }

            numBytesWritten = numZeroes;
            return true;
        }

        var table = _lookupTable;
        byte* pOutputEnd = outputPtr + outputLen - 1;
        byte* pMinOutput = pOutputEnd;
        while (pInput != pInputEnd)
        {
            char c = *pInput;
            int carry = table[c] - 1;
            if (carry < 0)
            {
                throw new ArgumentException($"Invalid character: {c}");
            }

            for (byte* pOutput = pOutputEnd; pOutput >= outputPtr; pOutput--)
            {
                carry += 58 * (*pOutput);
                *pOutput = (byte)carry;
                if (pMinOutput > pOutput && carry != 0)
                {
                    pMinOutput = pOutput;
                }

                carry >>= 8;
            }

            pInput++;
        }

        int startIndex = (int)(pMinOutput - numZeroes - outputPtr);
        numBytesWritten = outputLen - startIndex;
        Buffer.MemoryCopy(outputPtr + startIndex, outputPtr, numBytesWritten, numBytesWritten);
        return true;
    }

    private static unsafe bool internalEncode(
        byte* inputPtr,
        char* outputPtr,
        int numZeroes,
        out int numCharsWritten)
    {
        fixed (char* alphabetPtr = _alphabet)
        {
            byte* pInput = inputPtr + numZeroes;
            byte* pInputEnd = inputPtr + 12;

            int length = 0;
            char* pOutput = outputPtr;
            char* pLastChar = pOutput + 16;

            while (pInput != pInputEnd)
            {
                int carry = *pInput;
                int i = 0;
                for (char* pDigit = pLastChar; (carry != 0 || i < length) && pDigit >= outputPtr; pDigit--, i++)
                {
                    carry += *pDigit << 8;
                    carry = Math.DivRem(carry, 58, out int remainder);
                    *pDigit = (char)remainder;
                }

                length = i;
                pInput++;
            }

            var pOutputEnd = pOutput + 17;

            // copy the characters to the beginning of the buffer
            // and translate them at the same time. if no copying
            // is needed, this only acts as the translation phase.
            for (char* a = outputPtr + numZeroes, b = pOutputEnd - length;
                b != pOutputEnd;
                a++, b++)
            {
                *a = alphabetPtr[*b];
            }

            // translate the zeroes at the start
            while (pOutput != pOutputEnd)
            {
                char c = *pOutput;
                if (c != '\0')
                {
                    break;
                }

                *pOutput = alphabetPtr[c];
                pOutput++;
            }

            int actualLen = numZeroes + length;

            numCharsWritten = actualLen;
            return true;
        }
    }

    private static unsafe bool internalEncode_FAST(byte* input, char* output, out int written)
    {
        fixed (char* alphabetPtr = _alphabet)
        {
            byte* pInputEnd = input + 12;

            int length = 0;
            char* lastChar = output + 16;

            //Console.Write($"{length} | ");

            while (input != pInputEnd)
            {
                int carry = *input++;
                int i = 0;
                for (char* pDigit = lastChar; (carry != 0 || i < length) && pDigit >= output; pDigit--, i++)
                {
                    carry = Math.DivRem(carry + (*pDigit << 8), 58, out int remainder);
                    *pDigit = (char)remainder;
                }

                length = i;

                //Console.Write($"{length} | ");
            }

            //Console.WriteLine();

            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output++];
            *output = alphabetPtr[*output];

            written = length;

            return true;
        }
    }

    public static string Encode_New(ReadOnlySpan<byte> bytes)
    {
        int numZeroes = getZeroCount(bytes, 12);
        if (numZeroes == 12) return Zero;

        Span<char> output = stackalloc char[17];

        if (!internalEncode(bytes, output, numZeroes, out int length))
            throw new InvalidOperationException("Output buffer with insufficient size generated");

        if (length != 17)
        {
            return new string(ZeroChar, 17 - length) + output.Slice(0, length).ToString();
        }

        return output.ToString();
    }

    private static bool internalEncode(
        ReadOnlySpan<byte> input,
        Span<char> output,
        int numZeroes,
        out int numCharsWritten)
    {
        ReadOnlySpan<char> alphabet = _alphabet.AsSpan();

        int numDigits = 0;
        int index = numZeroes;
        while (index < input.Length)
        {
            int carry = input[index++];
            int i = 0;
            for (int j = output.Length - 1; (carry != 0 || i < numDigits) && j >= 0; j--, i++)
            {
                carry += output[j] << 8;
                carry = Math.DivRem(carry, 58, out int remainder);
                output[j] = (char)remainder;
            }

            numDigits = i;
        }

        translatedCopy(output.Slice(output.Length - numDigits), output.Slice(numZeroes), alphabet);
        if (numZeroes > 0)
        {
            output.Slice(0, numZeroes).Fill(alphabet[0]);
        }

        numCharsWritten = numZeroes + numDigits;
        return true;
    }

    private static void translatedCopy(
    ReadOnlySpan<char> source,
    Span<char> destination,
    ReadOnlySpan<char> alphabet)
    {
        Debug.Assert(source.Length <= destination.Length, "source is too big");
        for (int n = 0; n < source.Length; n++)
        {
            destination[n] = alphabet[source[n]];
        }
    }

    private static unsafe int getZeroCount(ReadOnlySpan<byte> bytes, int bytesLen)
    {
        int numZeroes = 0;
        fixed (byte* inputPtr = bytes)
        {
            var pInput = inputPtr;
            while (*pInput == 0 && numZeroes < bytesLen)
            {
                numZeroes++;
                pInput++;
            }
        }

        return numZeroes;
    }

    private static unsafe int getPrefixCount(ReadOnlySpan<char> input, int length, char value)
    {
        if (length == 0)
        {
            return 0;
        }

        int numZeroes = 0;
        fixed (char* inputPtr = input)
        {
            var pInput = inputPtr;
            while (*pInput == value && numZeroes < length)
            {
                numZeroes++;
                pInput++;
            }
        }

        return numZeroes;
    }
}