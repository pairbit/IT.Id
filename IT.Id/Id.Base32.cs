using Internal;
using System.Runtime.CompilerServices;

namespace System;

public readonly partial struct Id
{
    private String ToBase32()
    {
        var base32 = new string((char)0, 20);
        unsafe
        {
            fixed (char* dest = base32)
            fixed (char* map = Base32.Alphabet)
            {
                ulong value = (byte)(_timestamp >> 24);
                value = (value << 8) | (byte)(_timestamp >> 16);
                value = (value << 8) | (byte)(_timestamp >> 8);
                value = (value << 8) | (byte)_timestamp;
                value = (value << 8) | (byte)(_b >> 24);

                //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

                dest[0] = map[value >> 35];
                dest[1] = map[(value >> 30) & 0x1F];
                dest[2] = map[(value >> 25) & 0x1F];
                dest[3] = map[(value >> 20) & 0x1F];
                dest[4] = map[(value >> 15) & 0x1F];
                dest[5] = map[(value >> 10) & 0x1F];
                dest[6] = map[(value >> 5) & 0x1F];
                dest[7] = map[value & 0x1F];

                value = (byte)(_b >> 16);
                value = (value << 8) | (byte)(_b >> 8);
                value = (value << 8) | (byte)_b;
                value = (value << 8) | (byte)(_c >> 24);
                value = (value << 8) | (byte)(_c >> 16);

                dest[8] = map[value >> 35];
                dest[9] = map[(value >> 30) & 0x1F];
                dest[10] = map[(value >> 25) & 0x1F];
                dest[11] = map[(value >> 20) & 0x1F];
                dest[12] = map[(value >> 15) & 0x1F];
                dest[13] = map[(value >> 10) & 0x1F];
                dest[14] = map[(value >> 5) & 0x1F];
                dest[15] = map[value & 0x1F];

                value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

                dest[16] = map[value >> 15];
                dest[17] = map[(value >> 10) & 0x1F];
                dest[18] = map[(value >> 5) & 0x1F];
                dest[19] = map[value & 0x1F];
            }
        }

        return base32;
    }

    private unsafe void ToBase32(Span<Char> chars)
    {
        fixed (char* dest = chars)
        fixed (char* map = Base32.Alphabet)
        {
            ulong value = (byte)(_timestamp >> 24);
            value = (value << 8) | (byte)(_timestamp >> 16);
            value = (value << 8) | (byte)(_timestamp >> 8);
            value = (value << 8) | (byte)_timestamp;
            value = (value << 8) | (byte)(_b >> 24);

            //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            dest[0] = map[value >> 35];
            dest[1] = map[(value >> 30) & 0x1F];
            dest[2] = map[(value >> 25) & 0x1F];
            dest[3] = map[(value >> 20) & 0x1F];
            dest[4] = map[(value >> 15) & 0x1F];
            dest[5] = map[(value >> 10) & 0x1F];
            dest[6] = map[(value >> 5) & 0x1F];
            dest[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            dest[8] = map[value >> 35];
            dest[9] = map[(value >> 30) & 0x1F];
            dest[10] = map[(value >> 25) & 0x1F];
            dest[11] = map[(value >> 20) & 0x1F];
            dest[12] = map[(value >> 15) & 0x1F];
            dest[13] = map[(value >> 10) & 0x1F];
            dest[14] = map[(value >> 5) & 0x1F];
            dest[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            dest[16] = map[value >> 15];
            dest[17] = map[(value >> 10) & 0x1F];
            dest[18] = map[(value >> 5) & 0x1F];
            dest[19] = map[value & 0x1F];
        }
    }

    private unsafe void ToBase32(Span<Byte> bytes)
    {
        fixed (byte* dest = bytes)
        fixed (byte* map = Base32.EncodeMap)
        {
            ulong value = (byte)(_timestamp >> 24);
            value = (value << 8) | (byte)(_timestamp >> 16);
            value = (value << 8) | (byte)(_timestamp >> 8);
            value = (value << 8) | (byte)_timestamp;
            value = (value << 8) | (byte)(_b >> 24);

            //ulong value = ((ulong)_timestamp << 8) | (byte)(_b >> 24);

            dest[0] = map[value >> 35];
            dest[1] = map[(value >> 30) & 0x1F];
            dest[2] = map[(value >> 25) & 0x1F];
            dest[3] = map[(value >> 20) & 0x1F];
            dest[4] = map[(value >> 15) & 0x1F];
            dest[5] = map[(value >> 10) & 0x1F];
            dest[6] = map[(value >> 5) & 0x1F];
            dest[7] = map[value & 0x1F];

            value = (byte)(_b >> 16);
            value = (value << 8) | (byte)(_b >> 8);
            value = (value << 8) | (byte)_b;
            value = (value << 8) | (byte)(_c >> 24);
            value = (value << 8) | (byte)(_c >> 16);

            dest[8] = map[value >> 35];
            dest[9] = map[(value >> 30) & 0x1F];
            dest[10] = map[(value >> 25) & 0x1F];
            dest[11] = map[(value >> 20) & 0x1F];
            dest[12] = map[(value >> 15) & 0x1F];
            dest[13] = map[(value >> 10) & 0x1F];
            dest[14] = map[(value >> 5) & 0x1F];
            dest[15] = map[value & 0x1F];

            value = (((ulong)(byte)(_c >> 8) << 8) | (byte)_c) << 4;

            dest[16] = map[value >> 15];
            dest[17] = map[(value >> 10) & 0x1F];
            dest[18] = map[(value >> 5) & 0x1F];
            dest[19] = map[value & 0x1F];
        }
    }

    private static unsafe Id ParseBase32(ReadOnlySpan<Char> chars)
    {
        if (chars.Length != 20) throw new ArgumentException("The id must be 20 characters long", nameof(chars));

        fixed (char* src = chars)
        {
            ulong v = Decode(src[0]);
            v = (v << 5) | Decode(src[1]);
            v = (v << 5) | Decode(src[2]);
            v = (v << 5) | Decode(src[3]);
            v = (v << 5) | Decode(src[4]);
            v = (v << 5) | Decode(src[5]);
            v = (v << 5) | Decode(src[6]);
            v = (v << 5) | Decode(src[7]);

            var timestamp = (byte)(v >> 32) << 24 | (byte)(v >> 24) << 16 | (byte)(v >> 16) << 8 | (byte)(v >> 8);

            var b = (int)(byte)v;

            v = (v << 5) | Decode(src[8]);
            v = (v << 5) | Decode(src[9]);
            v = (v << 5) | Decode(src[10]);
            v = (v << 5) | Decode(src[11]);
            v = (v << 5) | Decode(src[12]);
            v = (v << 5) | Decode(src[13]);
            v = (v << 5) | Decode(src[14]);
            v = (v << 5) | Decode(src[15]);

            b = b << 24 | (byte)(v >> 32) << 16 | (byte)(v >> 24) << 8 | (byte)(v >> 16);

            var c = (byte)(v >> 8) << 24 | (byte)v << 16;

            v = Decode(src[16]);
            v = (v << 5) | Decode(src[17]);
            v = (v << 5) | Decode(src[18]);
            v = (v << 5) | Decode(src[19]);

            c |= (byte)(v >> 12) << 8 | (byte)(v >> 4);

            return new Id(timestamp, b, c);
        }
    }

    private static unsafe Id ParseBase32(ReadOnlySpan<Byte> bytes)
    {
        if (bytes.Length != 20) throw new ArgumentException("The id must be 20 bytes long", nameof(bytes));

        fixed (byte* src = bytes)
        {
            ulong v = Decode(src[0]);
            v = (v << 5) | Decode(src[1]);
            v = (v << 5) | Decode(src[2]);
            v = (v << 5) | Decode(src[3]);
            v = (v << 5) | Decode(src[4]);
            v = (v << 5) | Decode(src[5]);
            v = (v << 5) | Decode(src[6]);
            v = (v << 5) | Decode(src[7]);

            var timestamp = (byte)(v >> 32) << 24 | (byte)(v >> 24) << 16 | (byte)(v >> 16) << 8 | (byte)(v >> 8);

            var b = (int)(byte)v;

            v = (v << 5) | Decode(src[8]);
            v = (v << 5) | Decode(src[9]);
            v = (v << 5) | Decode(src[10]);
            v = (v << 5) | Decode(src[11]);
            v = (v << 5) | Decode(src[12]);
            v = (v << 5) | Decode(src[13]);
            v = (v << 5) | Decode(src[14]);
            v = (v << 5) | Decode(src[15]);

            b = b << 24 | (byte)(v >> 32) << 16 | (byte)(v >> 24) << 8 | (byte)(v >> 16);

            var c = (byte)(v >> 8) << 24 | (byte)v << 16;

            v = Decode(src[16]);
            v = (v << 5) | Decode(src[17]);
            v = (v << 5) | Decode(src[18]);
            v = (v << 5) | Decode(src[19]);

            c |= (byte)(v >> 12) << 8 | (byte)(v >> 4);

            return new Id(timestamp, b, c);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Byte Decode(int c)
    {
        if (c < Base32.Min || c > Base32.Max) throw new FormatException($"Char '{(char)c}' not found Base32 1");

        var item = Base32.DecodeMap[c];

        if (item == -1) throw new FormatException($"Char '{(char)c}' not found Base32 2");

        return (byte)item;
    }
}