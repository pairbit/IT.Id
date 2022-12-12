using System;

namespace IT;

public readonly partial struct Id
{
    private const uint PRIME32_1 = 2654435761u;
    private const uint PRIME32_2 = 2246822519u;
    private const uint PRIME32_3 = 3266489917u;
    private const uint PRIME32_4 = 668265263u;
    private const uint PRIME32_5 = 374761393u;

    private const ulong PRIME64_1 = 11400714785074694791ul;
    private const ulong PRIME64_2 = 14029467366897019727ul;
    private const ulong PRIME64_3 = 1609587929392839161ul;
    private const ulong PRIME64_4 = 9650029242287828579ul;
    private const ulong PRIME64_5 = 2870177450012600261ul;

    internal UInt32 XXH32_2()
    {
        var b0 = (uint)(_timestamp3 << 24 | _timestamp2 << 16 | _timestamp1 << 8 | _timestamp0);
        var b1 = (uint)(_pid0 << 24 | _machine2 << 16 | _machine1 << 8 | _machine0);
        var b2 = (uint)(_increment2 << 24 | _increment1 << 16 | _increment0 << 8 | _pid1);
        var h32 = PRIME32_5 + 12 + b0 * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + b1 * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + b2 * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4;
        h32 = (h32 ^ (h32 >> 15)) * PRIME32_2;
        h32 = (h32 ^ (h32 >> 13)) * PRIME32_3;
        return h32 ^ (h32 >> 16);
    }

    public unsafe UInt32 XXH32()
    {
        fixed (byte* p = &_timestamp0)
        {
            var i = (uint*)p;
            var h32 = PRIME32_5 + 12 + (*i) * PRIME32_3;
            h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + (*(i + 1)) * PRIME32_3;
            h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + (*(i + 2)) * PRIME32_3;
            h32 = (h32 << 17 | h32 >> 15) * PRIME32_4;
            h32 = (h32 ^ (h32 >> 15)) * PRIME32_2;
            h32 = (h32 ^ (h32 >> 13)) * PRIME32_3;
            return h32 ^ (h32 >> 16);
        }
    }

    internal UInt64 XXH64_2()
    {
        var b0 = (uint)(_timestamp3 << 24 | _timestamp2 << 16 | _timestamp1 << 8 | _timestamp0);
        var b1 = (uint)(_pid0 << 24 | _machine2 << 16 | _machine1 << 8 | _machine0);
        ulong h64 = (((ulong)b1 << 32) | b0) * PRIME64_2;
        h64 = (PRIME64_5 + 12) ^ ((h64 << 31 | h64 >> 33) * PRIME64_1);
        h64 = ((h64 << 27 | h64 >> 37) * PRIME64_1 + PRIME64_4) ^ ((uint)(_increment2 << 24 | _increment1 << 16 | _increment0 << 8 | _pid1) * PRIME64_1);
        h64 = (h64 << 23 | h64 >> 41) * PRIME64_2 + PRIME64_3;
        h64 = (h64 ^ (h64 >> 33)) * PRIME64_2;
        h64 = (h64 ^ (h64 >> 29)) * PRIME64_3;
        return h64 ^ (h64 >> 32);
    }

    public unsafe UInt64 XXH64()
    {
        fixed (byte* p = &_timestamp0)
        {
            ulong h64 = *(ulong*)p * PRIME64_2;
            h64 = (PRIME64_5 + 12) ^ ((h64 << 31 | h64 >> 33) * PRIME64_1);
            h64 = ((h64 << 27 | h64 >> 37) * PRIME64_1 + PRIME64_4) ^ (*(uint*)(p + 8) * PRIME64_1);
            h64 = (h64 << 23 | h64 >> 41) * PRIME64_2 + PRIME64_3;
            h64 = (h64 ^ (h64 >> 33)) * PRIME64_2;
            h64 = (h64 ^ (h64 >> 29)) * PRIME64_3;
            return h64 ^ (h64 >> 32);
        }
    }

    internal unsafe UInt64 XXH64_3()
    {
        fixed (byte* p = &_timestamp0)
        {
            var i = (uint*)p;
            ulong h64 = (((ulong)(*(i + 1)) << 32) | (*i)) * PRIME64_2;
            h64 = (PRIME64_5 + 12) ^ ((h64 << 31 | h64 >> 33) * PRIME64_1);
            h64 = ((h64 << 27 | h64 >> 37) * PRIME64_1 + PRIME64_4) ^ (*(i + 2) * PRIME64_1);
            h64 = (h64 << 23 | h64 >> 41) * PRIME64_2 + PRIME64_3;
            h64 = (h64 ^ (h64 >> 33)) * PRIME64_2;
            h64 = (h64 ^ (h64 >> 29)) * PRIME64_3;
            return h64 ^ (h64 >> 32);
        }
    }
}