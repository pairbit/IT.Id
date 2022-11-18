namespace System;

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

    public UInt32 XXH32()
    {
        var b0 = (uint)((byte)_timestamp << 24 | (byte)(_timestamp >> 8) << 16 | (byte)(_timestamp >> 16) << 8 | (byte)(_timestamp >> 24));
        var b1 = (uint)((byte)_b << 24 | (byte)(_b >> 8) << 16 | (byte)(_b >> 16) << 8 | (byte)(_b >> 24));
        var b2 = (uint)((byte)_c << 24 | (byte)(_c >> 8) << 16 | (byte)(_c >> 16) << 8 | (byte)(_c >> 24));
        var x = PRIME32_5 + 12 + b0 * PRIME32_3;
        uint h32 = (x << 17 | x >> 15) * PRIME32_4;
        x = h32 + b1 * PRIME32_3;
        h32 = (x << 17 | x >> 15) * PRIME32_4;
        x = h32 + b2 * PRIME32_3;
        h32 = (x << 17 | x >> 15) * PRIME32_4;
        h32 = (h32 ^ (h32 >> 15)) * PRIME32_2;
        h32 = (h32 ^ (h32 >> 13)) * PRIME32_3;
        return h32 ^ (h32 >> 16);
    }

    public UInt64 XXH64()
    {
        ulong h64 = PRIME64_5 + 12;
        var b0 = (uint)((byte)_timestamp << 24 | (byte)(_timestamp >> 8) << 16 | (byte)(_timestamp >> 16) << 8 | (byte)(_timestamp >> 24));
        var b1 = (uint)((byte)_b << 24 | (byte)(_b >> 8) << 16 | (byte)(_b >> 16) << 8 | (byte)(_b >> 24));
        var uint64 = ((ulong)b1 << 32) | (ulong)b0;
        var x = uint64 * PRIME64_2;
        h64 ^= (x << 31 | x >> 33) * PRIME64_1;
        h64 = (h64 << 27 | h64 >> 37) * PRIME64_1 + PRIME64_4;
        var b2 = (uint)((byte)_c << 24 | (byte)(_c >> 8) << 16 | (byte)(_c >> 16) << 8 | (byte)(_c >> 24));
        h64 ^= b2 * PRIME64_1;
        h64 = (h64 << 23 | h64 >> 41) * PRIME64_2 + PRIME64_3;
        h64 = (h64 ^ (h64 >> 33)) * PRIME64_2;
        h64 = (h64 ^ (h64 >> 29)) * PRIME64_3;
        return h64 ^ (h64 >> 32);
    }
}