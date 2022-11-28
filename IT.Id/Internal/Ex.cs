using System;
using System.Linq;

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
        => new FormatException($"The length of System.Id in {format} format cannot be {length} characters. It must be {Id.GetLength(format)} characters long.");

    public static Exception InvalidLengthBytes(Idf format, int length)
        => new FormatException($"The length of System.Id in {format} format cannot be {length} bytes. It must be {Id.GetLength(format)} bytes long.");

    public static Exception InvalidLengthChars(Idf format, int length, int min, int max)
        => new FormatException($"The length of System.Id in {format} format cannot be {length} characters. It must be between {min} to {max} characters long.");

    public static Exception InvalidLengthBytes(Idf format, int length, int min, int max)
        => new FormatException($"The length of System.Id in {format} format cannot be {length} bytes. It must be between {min} to {max} bytes long.");

    public static Exception InvalidFormat(Idf format, String type = "Id") => InvalidFormat(format.ToString(), type);

    public static Exception InvalidFormat(String format, String type = "Id") => new FormatException($"The System.{type} does not contain '{format}' format.");

    public static Exception InvalidChar(Idf format, params int[] codes)
    {
        var min = GetMin(format);
        var max = GetMax(format);
        var map = GetMap(format);

        foreach (var code in codes)
        {
            if (code < min || code > max || map[code] == -1)
                return InvalidChar(format, code);
        }

        throw new NotImplementedException();
    }

    public static Exception InvalidChar(Idf format, int code)
        => new FormatException($"The System.Id in {format} format cannot contain character code {code}.");

    public static Exception InvalidCharIndex(Idf format, int index, int invalidCode, params char[] validCodes)
        => new FormatException($"The System.Id in {format} format cannot contain character code {invalidCode} at position {index}. It must contain one of characters {String.Join(", ", validCodes.Select(x => "'" + x + "'"))}.");

    public static Exception InvalidByte(Idf format, params int[] codes)
    {
        var min = GetMin(format);
        var max = GetMax(format);
        var map = GetMap(format);

        foreach (var code in codes)
        {
            if (code < min || code > max || map[code] == -1)
                return InvalidByte(format, code);
        }

        throw new NotImplementedException();
    }

    public static Exception InvalidByte(Idf format, int code)
        => new FormatException($"The System.Id in {format} format cannot contain byte {code}.");

    public static Exception InvalidByteIndex(Idf format, int index, int invalidCode, params int[] validCodes)
        => new FormatException($"The System.Id in {format} format cannot contain byte {invalidCode} at position {index}. It must contain one of bytes {String.Join(", ", validCodes)}.");

    private static int GetMin(Idf format) => format switch
    {
        Idf.Hex => Hex.Min,
        Idf.Base32 => Base32.Min,
        Idf.Base58 => Base58.Min,
        Idf.Base64 or Idf.Base64Path2 or Idf.Base64Path3 => Base64.Min,
        Idf.Base85 => Base85.Min,
        _ => throw new NotImplementedException()
    };

    private static int GetMax(Idf format) => format switch
    {
        Idf.Hex => Hex.Max,
        Idf.Base32 => Base32.Max,
        Idf.Base58 => Base58.Max,
        Idf.Base64 or Idf.Base64Path2 or Idf.Base64Path3 => Base64.Max,
        Idf.Base85 => Base85.Max,
        _ => throw new NotImplementedException()
    };

    private static sbyte[] GetMap(Idf format) => format switch
    {
        Idf.Hex => Hex.DecodeMap,
        Idf.Base32 => Base32.DecodeMap,
        Idf.Base58 => Base58.DecodeMap,
        Idf.Base64 or Idf.Base64Path2 or Idf.Base64Path3 => Base64.DecodeMap,
        Idf.Base85 => Base85.DecodeMap,
        _ => throw new NotImplementedException()
    };
}