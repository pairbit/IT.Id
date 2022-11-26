using System;

namespace Internal;

internal static class Ex
{
    public static Exception InvalidLength(int length) 
        => new FormatException($"The length of System.Id cannot be {length}. It must be 24 or between 15 and 20.");

    public static Exception InvalidLengthChars(int length)
        => new FormatException($"The length of System.Id cannot be {length} characters. It must be 24 or between 15 and 20 characters.");

    public static Exception InvalidLengthBytes(int length)
        => new FormatException($"The length of System.Id cannot be {length} bytes. It must be 24 or between 15 and 20 bytes.");

    public static Exception InvalidLengthChars(Idf format, int length) 
        => new FormatException($"The length of System.Id in format {format} cannot be {length} characters. It must be {Id.GetLength(format)} characters long.");

    public static Exception InvalidLengthBytes(Idf format, int length)
        => new FormatException($"The length of System.Id in format {format} cannot be {length} bytes. It must be {Id.GetLength(format)} bytes long.");

    public static Exception InvalidLengthChars(Idf format, int length, int min, int max) 
        => new FormatException($"The length of System.Id in format {format} cannot be {length} characters. It must be between {min} to {max} characters long.");

    public static Exception InvalidLengthBytes(Idf format, int length, int min, int max)
        => new FormatException($"The length of System.Id in format {format} cannot be {length} bytes. It must be between {min} to {max} bytes long.");

    public static Exception InvalidFormat(Idf format, String type = "Id") => new FormatException($"System.{type} does not contain '{format}' format string.");

    public static Exception InvalidFormat(String format, String type = "Id") => new FormatException($"System.{type} does not contain '{format}' format string.");

    public static Exception InvalidChars(Idf format, params int[] codes)
    {
        var min = GetMin(format);
        var max = GetMax(format);
        var map = GetMap(format);

        foreach (var code in codes)
        {
            if (code < min || code > max || map[code] == -1)
                return InvalidChar(format, code);
        }

        return new FormatException();
    }

    public static Exception InvalidChar(Idf format, int code)
        => new FormatException($"The System.Id in format {format} does not contain a character with code {code}.");

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