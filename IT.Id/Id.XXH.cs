namespace System;

public readonly partial struct Id
{
    public unsafe UInt32 Hash32()
    {
        var bytes = stackalloc[] {
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
        };
        return XXH32.DigestOf(bytes, 12);
    }

    //public unsafe UInt64 Hash64_slow()
    //{
    //    ulong h64 = XXH64.PRIME64_5 + 12;

    //    fixed (byte* bytesP = stackalloc[] {
    //        (byte)(_timestamp >> 24),
    //        (byte)(_timestamp >> 16),
    //        (byte)(_timestamp >> 8),
    //        (byte)(_timestamp),
    //        (byte)(_b >> 24),
    //        (byte)(_b >> 16),
    //        (byte)(_b >> 8),
    //        (byte)(_b),
    //        (byte)(_c >> 24),
    //        (byte)(_c >> 16),
    //        (byte)(_c >> 8),
    //        (byte)(_c)
    //    })
    //    {
    //        var uint64 = *(ulong*)bytesP;//10225195631648942178

    //        var x = uint64 * XXH64.PRIME64_2;

    //        h64 ^= (x << 31 | x >> 33) * XXH64.PRIME64_1;

    //        h64 = (h64 << 27 | h64 >> 37) * XXH64.PRIME64_1 + XXH64.PRIME64_4;

    //        var uint32 = *(uint*)(bytesP + 8);//1071796039

    //        h64 ^= uint32 * XXH64.PRIME64_1;
    //    }

    //    h64 = (h64 << 23 | h64 >> 41) * XXH64.PRIME64_2 + XXH64.PRIME64_3;
    //    h64 = (h64 ^ (h64 >> 33)) * XXH64.PRIME64_2;
    //    h64 = (h64 ^ (h64 >> 29)) * XXH64.PRIME64_3;
    //    return h64 ^ (h64 >> 32);
    //}

    public unsafe UInt64 Hash64()
    {
        ulong h64 = XXH64.PRIME64_5 + 12;

        var uint32_1 = (uint)((byte)(_timestamp) << 24 | (byte)(_timestamp >> 8) << 16 | (byte)(_timestamp >> 16) << 8 | (byte)(_timestamp >> 24));

        var uint32_2 = (uint)((byte)(_b) << 24 | (byte)(_b >> 8) << 16 | (byte)(_b >> 16) << 8 | (byte)(_b >> 24));

        var uint64 = ((ulong)uint32_2 << 32) | (ulong)uint32_1;

        var x = uint64 * XXH64.PRIME64_2;

        h64 ^= (x << 31 | x >> 33) * XXH64.PRIME64_1;
        h64 = (h64 << 27 | h64 >> 37) * XXH64.PRIME64_1 + XXH64.PRIME64_4;

        var uint32 = (uint)((byte)_c << 24 | (byte)(_c >> 8) << 16 | (byte)(_c >> 16) << 8 | (byte)(_c >> 24));

        h64 ^= uint32 * XXH64.PRIME64_1;

        h64 = (h64 << 23 | h64 >> 41) * XXH64.PRIME64_2 + XXH64.PRIME64_3;
        h64 = (h64 ^ (h64 >> 33)) * XXH64.PRIME64_2;
        h64 = (h64 ^ (h64 >> 29)) * XXH64.PRIME64_3;
        return h64 ^ (h64 >> 32);
    }
}