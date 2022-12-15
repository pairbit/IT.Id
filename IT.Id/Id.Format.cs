using IT.Internal;
using System;

namespace IT;

public readonly partial struct Id : IFormattable
#if NET6_0_OR_GREATER
, ISpanFormattable
#endif
{
    /// <exception cref="FormatException"/>
    public static int GetLength(Idf format) => format switch
    {
        Idf.Base85 => 15,
        Idf.Base64 or Idf.Base64Url => 16,
        Idf.Base58 => 17,
        Idf.Base64Path2 => 18,
        Idf.Base64Path3 => 19,
        Idf.Base32 or Idf.Base32Upper => 20,
        Idf.Hex or Idf.HexUpper => 24,
        _ => throw Ex.InvalidFormat(format)
    };

    /// <summary>
    /// Hex -> "h" <br/>
    /// Hex Upper -> "H" <br/>
    /// Base32 -> "v" <br/>
    /// Base32 Upper -> "V" <br/>
    /// Base58 -> "i" <br/>
    /// Base64 -> "/" <br/>
    /// Base64 Url -> "_" <br/>
    /// Base64 Path2 -> "//" <br/>
    /// Base64 Path3 -> "///" <br/>
    /// Base85 -> "|" <br/>
    /// </summary>
    /// <exception cref="FormatException"/>
    public static string GetFormat(Idf format) => format switch
    {
        Idf.Base85 => "|",
        Idf.Base64 => "/",
        Idf.Base64Url => "_",
        Idf.Base58 => "i",
        Idf.Base64Path2 => "//",
        Idf.Base64Path3 => "///",
        Idf.Base32 => "v",
        Idf.Base32Upper => "V",
        Idf.Hex => "h",
        Idf.HexUpper => "H",
        _ => throw Ex.InvalidFormat(format)
    };

    /// <exception cref="FormatException"/>
    public static Idf GetFormat(int length) => length switch
    {
        15 => Idf.Base85,
        16 => Idf.Base64,
        17 => Idf.Base58,
        18 => Idf.Base64Path2,
        19 => Idf.Base64Path3,
        20 => Idf.Base32,
        24 => Idf.Hex,
        _ => throw Ex.InvalidLength(length)
    };

    public static bool TryGetFormat(int length, out Idf format)
    {
        if (length == 15)
        {
            format = Idf.Base85;
            return true;
        }

        if (length == 16)
        {
            format = Idf.Base64;
            return true;
        }

        if (length == 17)
        {
            format = Idf.Base58;
            return true;
        }

        if (length == 18)
        {
            format = Idf.Base64Path2;
            return true;
        }

        if (length == 19)
        {
            format = Idf.Base64Path3;
            return true;
        }

        if (length == 20)
        {
            format = Idf.Base32;
            return true;
        }

        if (length == 24)
        {
            format = Idf.Hex;
            return true;
        }

        format = default;
        return false;
    }

    /// <param name="format">
    /// null or "_" -> Base64 Url (YqhPZ0Ax541HT-I_) <br/>
    /// "h" -> Hex Lower (62a84f674031e78d474fe23f) <br/>
    /// "H" -> Hex Upper (62A84F674031E78D474FE23F) <br/>
    /// "v" -> Base32 Lower (ce0ytmyc14fgvd7358b0) <br/>
    /// "V" -> Base32 Upper (CE0YTMYC14FGVD7358B0) <br/>
    /// "i" -> Base58 (2ryw1nk6d1eiGQSL6) <br/>
    /// "/" -> Base64 (YqhPZ0Ax541HT+I/) <br/>
    /// "//" -> Base64 Path2 (_/I/-TH145xA0ZPhqY) <br/>
    /// "///" -> Base64 Path3 (_/I/-/TH145xA0ZPhqY) <br/>
    /// "|" -> Base85 (v{IV^PiNKcFO_~|) <br/>
    /// </param>
    /// <exception cref="FormatException"/>
    public string ToString(
#if NET7_0_OR_GREATER
        //[StringSyntax(StringSyntaxAttribute.GuidFormat)] 
#endif
        string? format,
        IFormatProvider? provider = null) => format switch
        {
            null or "_" => ToBase64Url(),
            "h" => ToHex(),
            "H" => ToHexUpper(),
            "v" => ToBase32(),
            "V" => ToBase32Upper(),
            "i" => ToBase58(),
            "/" => ToBase64(),
            "//" => ToBase64Path2(),
            "///" => ToBase64Path3(),
            "|" => ToBase85(),
            _ => throw Ex.InvalidFormat(format),
        };

    /// <exception cref="FormatException"/>
    public string ToString(Idf format) => format switch
    {
        Idf.Hex => ToHex(),
        Idf.HexUpper => ToHexUpper(),
        Idf.Base32 => ToBase32(),
        Idf.Base32Upper => ToBase32Upper(),
        Idf.Base58 => ToBase58(),
        Idf.Base64 => ToBase64(),
        Idf.Base64Url => ToBase64Url(),
        Idf.Base85 => ToBase85(),
        Idf.Base64Path2 => ToBase64Path2(),
        Idf.Base64Path3 => ToBase64Path3(),
        _ => throw Ex.InvalidFormat(format),
    };

    public bool TryFormat(Span<Byte> bytes, out Int32 written, Idf format)
    {
        if (format == Idf.Hex)
        {
            if (!TryToHex(bytes)) goto fail;

            written = 24;
            return true;
        }

        if (format == Idf.HexUpper)
        {
            if (!TryToHexUpper(bytes)) goto fail;

            written = 24;
            return true;
        }

        if (format == Idf.Base32)
        {
            if (!TryToBase32(bytes)) goto fail;

            written = 20;
            return true;
        }

        if (format == Idf.Base32Upper)
        {
            if (!TryToBase32Upper(bytes)) goto fail;

            written = 20;
            return true;
        }

        if (format == Idf.Base58)
        {
            if (!TryToBase58(bytes)) goto fail;

            written = 17;
            return true;
        }

        if (format == Idf.Base64)
        {
            if (!TryToBase64(bytes)) goto fail;

            written = 16;
            return true;
        }

        if (format == Idf.Base64Url)
        {
            if (!TryToBase64Url(bytes)) goto fail;

            written = 16;
            return true;
        }

        if (format == Idf.Base85)
        {
            if (!TryToBase85(bytes)) goto fail;

            written = 15;
            return true;
        }

        if (format == Idf.Base64Path2)
        {
            if (!TryToBase64Path2(bytes)) goto fail;

            written = 18;
            return true;
        }

        if (format == Idf.Base64Path3)
        {
            if (!TryToBase64Path3(bytes)) goto fail;

            written = 19;
            return true;
        }

        throw Ex.InvalidFormat(format.ToString());

    fail:
        written = 0;
        return false;
    }

    public bool TryFormat(Span<Char> chars, out Int32 written, Idf format)
    {
        if (format == Idf.Hex)
        {
            if (!TryToHex(chars)) goto fail;

            written = 24;
            return true;
        }

        if (format == Idf.HexUpper)
        {
            if (!TryToHexUpper(chars)) goto fail;

            written = 24;
            return true;
        }

        if (format == Idf.Base32)
        {
            if (!TryToBase32(chars)) goto fail;

            written = 20;
            return true;
        }

        if (format == Idf.Base32Upper)
        {
            if (!TryToBase32Upper(chars)) goto fail;

            written = 20;
            return true;
        }

        if (format == Idf.Base58)
        {
            if (!TryToBase58(chars)) goto fail;

            written = 17;
            return true;
        }

        if (format == Idf.Base64)
        {
            if (!TryToBase64(chars)) goto fail;

            written = 16;
            return true;
        }

        if (format == Idf.Base64Url)
        {
            if (!TryToBase64Url(chars)) goto fail;

            written = 16;
            return true;
        }

        if (format == Idf.Base85)
        {
            if (!TryToBase85(chars)) goto fail;

            written = 15;
            return true;
        }

        if (format == Idf.Base64Path2)
        {
            if (!TryToBase64Path2(chars)) goto fail;

            written = 18;
            return true;
        }

        if (format == Idf.Base64Path3)
        {
            if (!TryToBase64Path3(chars)) goto fail;

            written = 19;
            return true;
        }

        throw Ex.InvalidFormat(format.ToString());

    fail:
        written = 0;
        return false;
    }

    /// <param name="format">
    /// null or "_" -> Base64 Url (YqhPZ0Ax541HT-I_) <br/>
    /// "h" -> Hex Lower (62a84f674031e78d474fe23f) <br/>
    /// "H" -> Hex Upper (62A84F674031E78D474FE23F) <br/>
    /// "v" -> Base32 Lower (ce0ytmyc14fgvd7358b0) <br/>
    /// "V" -> Base32 Upper (CE0YTMYC14FGVD7358B0) <br/>
    /// "i" -> Base58 (2ryw1nk6d1eiGQSL6) <br/>
    /// "/" -> Base64 (YqhPZ0Ax541HT+I/) <br/>
    /// "//" -> Base64 Path2 (_/I/-TH145xA0ZPhqY) <br/>
    /// "///" -> Base64 Path3 (_/I/-/TH145xA0ZPhqY) <br/>
    /// "|" -> Base85 (v{IV^PiNKcFO_~|) <br/>
    /// </param>
    /// <exception cref="FormatException"/>
    public bool TryFormat(Span<Char> chars, out Int32 written,
#if NET7_0_OR_GREATER
        //[StringSyntax("/")] 
#endif
        ReadOnlySpan<Char> format
#if NET6_0_OR_GREATER
        , IFormatProvider? provider = null)
#else
        )
#endif
    {
        var len = format.Length;

        if (len == 0)
        {
            if (!TryToBase64Url(chars)) goto fail;

            written = 16;
            return true;
        }

        if (len == 1)
        {
            var f = format[0];

            if (f == Base64.Format)
            {
                if (!TryToBase64(chars)) goto fail;

                written = 16;
                return true;
            }

            if (f == Base64.FormatUrl)
            {
                if (!TryToBase64Url(chars)) goto fail;

                written = 16;
                return true;
            }

            if (f == Hex.FormatLower)
            {
                if (!TryToHex(chars)) goto fail;

                written = 24;
                return true;
            }

            if (f == Hex.FormatUpper)
            {
                if (!TryToHexUpper(chars)) goto fail;

                written = 24;
                return true;
            }

            if (f == Base32.FormatLower)
            {
                if (!TryToBase32(chars)) goto fail;

                written = 20;
                return true;
            }

            if (f == Base32.FormatUpper)
            {
                if (!TryToBase32Upper(chars)) goto fail;

                written = 20;
                return true;
            }

            if (f == Base58.Format)
            {
                if (!TryToBase58(chars)) goto fail;

                written = 17;
                return true;
            }

            if (f == Base85.Format)
            {
                if (!TryToBase85(chars)) goto fail;

                written = 15;
                return true;
            }
        }

        else if (len == 2)
        {
            if (format[0] == '/' && format[1] == '/')
            {
                if (!TryToBase64Path2(chars)) goto fail;

                written = 18;
                return true;
            }
        }

        else if (len == 3)
        {
            if (format[0] == '/' && format[1] == '/' && format[2] == '/')
            {
                if (!TryToBase64Path3(chars)) goto fail;

                written = 19;
                return true;
            }
        }

        throw Ex.InvalidFormat(format.ToString());

    fail:
        written = 0;
        return false;
    }
}