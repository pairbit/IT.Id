namespace System;

public readonly partial struct Id
{
    public UInt32 Hash32() => XXH32.DigestOf(ToByteArray());

    public UInt64 Hash64() => XXH64.DigestOf(ToByteArray());

    public unsafe UInt64 Hash64_Fast()
    {
        ulong h64 = XXH64.PRIME64_5 + 12;

        fixed (byte* bytesP = stackalloc[] {
            (byte)(_timestamp >> 24),
            (byte)(_timestamp >> 16),
            (byte)(_timestamp >> 8),
            (byte)(_timestamp),
            (byte)(_b >> 24),
            (byte)(_b >> 16),
            (byte)(_b >> 8),
            (byte)(_b),
            (byte)(_c >> 24),
            (byte)(_c >> 16),
            (byte)(_c >> 8),
            (byte)(_c)
        })
        {
            var uint64 = *(ulong*)bytesP;//10225195631648942178

            var x = uint64 * XXH64.PRIME64_2;

            h64 ^= (x << 31 | x >> 33) * XXH64.PRIME64_1;

            h64 = (h64 << 27 | h64 >> 37) * XXH64.PRIME64_1 + XXH64.PRIME64_4;

            var uint32 = *(uint*)(bytesP + 8);//1071796039

            h64 ^= uint32 * XXH64.PRIME64_1;
        }

        h64 = (h64 << 23 | h64 >> 41) * XXH64.PRIME64_2 + XXH64.PRIME64_3;

        //h64 ^= h64 >> 33;
        //h64 *= XXH64.PRIME64_2;
        //h64 ^= h64 >> 29;
        //h64 *= XXH64.PRIME64_3;
        //h64 ^= h64 >> 32;

        h64 = (h64 ^ (h64 >> 33)) * XXH64.PRIME64_2;
        h64 = (h64 ^ (h64 >> 29)) * XXH64.PRIME64_3;
        h64 = h64 ^ (h64 >> 32);

        return h64;
    }
}