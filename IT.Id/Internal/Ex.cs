﻿using System;

namespace Internal;

internal static class Ex
{
    public static Exception Format(Idf format, params int[] codes)
    {
        var min = GetMin(format);
        var max = GetMax(format);
        var map = GetMap(format);

        foreach (var code in codes)
        {
            if (code < min || code > max || map[code] == -1)
                return Format(format, code);
        }

        return new FormatException();
    }

    //public static Exception Format32(Idf format, uint chars)
    //{
    //    var char1 = (char)chars;
    //    var char2 = (char)(chars >> 16);
        
    //    return Format(format, char1, char2);
    //}

    public static Exception Format(Idf format, int code)
        => new FormatException($"Char '{(char)code}' not found {format}");

    private static int GetMin(Idf format) => format switch
    {
        Idf.Base85 => Base85.Min,
        Idf.Base64 => Base64.Min,
        Idf.Base58 => Base58.Min,
        Idf.Path2 => Base64.Min,
        Idf.Path3 => Base64.Min,
        Idf.Base32 => Base32.Min,
        Idf.Hex => Hex.Min,
        _ => throw new NotImplementedException()
    };

    private static int GetMax(Idf format) => format switch
    {
        Idf.Base85 => Base85.Max,
        Idf.Base64 => Base64.Max,
        Idf.Base58 => Base58.Max,
        Idf.Path2 => Base64.Max,
        Idf.Path3 => Base64.Max,
        Idf.Base32 => Base32.Max,
        Idf.Hex => Hex.Max,
        _ => throw new NotImplementedException()
    };

    private static sbyte[] GetMap(Idf format) => format switch
    {
        Idf.Base85 => Base85.DecodeMap,
        Idf.Base64 => Base64.DecodeMap,
        Idf.Base58 => Base58.DecodeMap,
        Idf.Path2 => Base64.DecodeMap,
        Idf.Path3 => Base64.DecodeMap,
        Idf.Base32 => Base32.DecodeMap,
        Idf.Hex => Hex.DecodeMap,
        _ => throw new NotImplementedException()
    };
}