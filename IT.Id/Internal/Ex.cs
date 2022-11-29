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

    public static Exception InvalidChar(Idf format, params int[] codes) => Invalid(false, format, codes);

    public static Exception InvalidChar(Idf format, int code) => Invalid(false, format, code);

    public static Exception InvalidCharIndex(Idf format, int index, int invalidCode, params int[] validCodes)
        => InvalidWithIndex(false, format, index, invalidCode, validCodes);

    public static Exception InvalidByte(Idf format, params int[] codes) => Invalid(true, format, codes);

    public static Exception InvalidByte(Idf format, int code) => Invalid(true, format, code);

    public static Exception InvalidByteIndex(Idf format, int index, int invalidCode, params int[] validCodes)
        => InvalidWithIndex(true, format, index, invalidCode, validCodes);

    private static Exception Invalid(bool isByte, Idf format, int code)
        => new FormatException(isByte
            ? $"The System.Id in {format} format cannot contain byte {code}."
            : $"The System.Id in {format} format cannot contain character code {code}.");

    private static Exception InvalidWithIndex(bool isByte, Idf format, int index, int invalidCode, int[] validCodes)
       => new FormatException(isByte
           ? $"The System.Id in {format} format cannot contain byte {invalidCode} at position {index}. It must contain one of bytes {String.Join(", ", validCodes)}."
           : $"The System.Id in {format} format cannot contain character code {invalidCode} at position {index}. It must contain one of characters {String.Join(", ", validCodes.Select(x => "'" + (char)x + "'"))}.");


    private static Exception Invalid(bool isByte, Idf format, int[] codes)
    {
        if (format == Idf.Hex || format == Idf.HexUpper)
        {
            var map = Hex.DecodeMap;
            foreach (var code in codes)
            {
                if (code < Hex.Min || code > Hex.Max || map[code] == 0xFF)
                    return Invalid(isByte, format, code);
            }
        }

        if (format == Idf.Base32 || format == Idf.Base32Upper)
        {
            var map = Base32.DecodeMap;
            foreach (var code in codes)
            {
                if (code < Base32.Min || code > Base32.Max || map[code] == 0xFF)
                    return Invalid(isByte, format, code);
            }
        }

        if (format == Idf.Base58)
        {
            var map = Base58.DecodeMap;
            foreach (var code in codes)
            {
                if (code < Base58.Min || code > Base58.Max || map[code] == 0xFF)
                    return Invalid(isByte, format, code);
            }
        }

        if (format == Idf.Base64 || format == Idf.Base64Path2 || format == Idf.Base64Path3)
        {
            var map = Base64.DecodeMap;
            foreach (var code in codes)
            {
                if (code < Base64.Min || code > Base64.Max || map[code] == -1)
                    return Invalid(isByte, format, code);
            }
        }

        if (format == Idf.Base85)
        {
            var map = Base85.DecodeMap;
            foreach (var code in codes)
            {
                if (code < Base85.Min || code > Base85.Max || map[code] == 0xFF)
                    return Invalid(isByte, format, code);
            }
        }

        throw new NotImplementedException();
    }
}