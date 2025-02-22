﻿using System.Runtime.CompilerServices;

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

    public uint XXH32()
    {
        ref uint i = ref Unsafe.As<byte, uint>(ref Unsafe.AsRef(in _timestamp0));
        var h32 = PRIME32_5 + 12 + i * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + Unsafe.Add(ref i, 1) * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4 + Unsafe.Add(ref i, 2) * PRIME32_3;
        h32 = (h32 << 17 | h32 >> 15) * PRIME32_4;
        h32 = (h32 ^ (h32 >> 15)) * PRIME32_2;
        h32 = (h32 ^ (h32 >> 13)) * PRIME32_3;
        return h32 ^ (h32 >> 16);
    }

    public ulong XXH64()
    {
        ref byte b = ref Unsafe.AsRef(in _timestamp0);
        ulong h64 = Unsafe.As<byte, ulong>(ref b) * PRIME64_2;
        h64 = (PRIME64_5 + 12) ^ ((h64 << 31 | h64 >> 33) * PRIME64_1);
        h64 = ((h64 << 27 | h64 >> 37) * PRIME64_1 + PRIME64_4) ^ (Unsafe.As<byte, uint>(ref Unsafe.Add(ref b, 8)) * PRIME64_1);
        h64 = (h64 << 23 | h64 >> 41) * PRIME64_2 + PRIME64_3;
        h64 = (h64 ^ (h64 >> 33)) * PRIME64_2;
        h64 = (h64 ^ (h64 >> 29)) * PRIME64_3;
        return h64 ^ (h64 >> 32);
    }
}