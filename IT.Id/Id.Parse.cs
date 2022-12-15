using IT.Internal;
using System;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id
{
    #region Parse

#if NET7_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Id IParsable<Id>.Parse(String s, IFormatProvider? provider) => Parse(s ?? throw new ArgumentNullException(nameof(s)));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Id ISpanParsable<Id>.Parse(ReadOnlySpan<Char> chars, IFormatProvider? provider) => Parse(chars);

#endif

#if NETSTANDARD2_0

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id Parse(String str) => Parse((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id Parse(String str, Idf format) => Parse((str ?? throw new ArgumentNullException(nameof(str))).AsSpan(), format);

#endif

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 15) return ParseBase85(chars);
        if (len == 16) return ParseBase64(chars);
        if (len == 17) return ParseBase58(chars);
        if (len == 18) return ParseBase64Path2(chars);
        if (len == 19) return ParseBase64Path3(chars);
        if (len == 20) return ParseBase32(chars);
        if (len == 24) return ParseHex(chars);

        throw Ex.InvalidLengthChars(len);
    }

    //=> chars.Length switch
    //{
    //    15 => ParseBase85(chars),
    //    16 => ParseBase64(chars),
    //    17 => ParseBase58(chars),
    //    18 => ParseBase64Path2(chars),
    //    19 => ParseBase64Path3(chars),
    //    20 => ParseBase32(chars),
    //    24 => ParseHex(chars),
    //    _ => throw Ex.InvalidLengthChars(chars.Length)
    //};

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Char> chars, Idf format)
    {
        if (format == Idf.Hex || format == Idf.HexUpper) return ParseHex(chars);

        if (format == Idf.Base32 || format == Idf.Base32Upper) return ParseBase32(chars);

        if (format == Idf.Base58) return ParseBase58(chars);

        if (format == Idf.Base64 || format == Idf.Base64Url) return ParseBase64(chars);

        if (format == Idf.Base85) return ParseBase85(chars);

        if (format == Idf.Base64Path2) return ParseBase64Path2(chars);

        if (format == Idf.Base64Path3) return ParseBase64Path3(chars);

        throw Ex.InvalidFormat(format);
    }

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Byte> bytes) => bytes.Length switch
    {
        15 => ParseBase85(bytes),
        16 => ParseBase64(bytes),
        17 => ParseBase58(bytes),
        18 => ParseBase64Path2(bytes),
        19 => ParseBase64Path3(bytes),
        20 => ParseBase32(bytes),
        24 => ParseHex(bytes),
        _ => throw Ex.InvalidLengthBytes(bytes.Length)
    };

    /// <exception cref="FormatException"/>
    public static Id Parse(ReadOnlySpan<Byte> bytes, Idf format)
    {
        if (format == Idf.Hex || format == Idf.HexUpper) return ParseHex(bytes);

        if (format == Idf.Base32 || format == Idf.Base32Upper) return ParseBase32(bytes);

        if (format == Idf.Base58) return ParseBase58(bytes);

        if (format == Idf.Base64 || format == Idf.Base64Url) return ParseBase64(bytes);

        if (format == Idf.Base85) return ParseBase85(bytes);

        if (format == Idf.Base64Path2) return ParseBase64Path2(bytes);

        if (format == Idf.Base64Path3) return ParseBase64Path3(bytes);

        throw Ex.InvalidFormat(format);
    }

    #endregion Parse

    #region TryParse

#if NET7_0_OR_GREATER

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Boolean IParsable<Id>.TryParse(String? s, IFormatProvider? provider, out Id id) => TryParse(s, out id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static Boolean ISpanParsable<Id>.TryParse(ReadOnlySpan<Char> chars, IFormatProvider? provider, out Id id) => TryParse(chars, out id);

#endif

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Boolean TryParse(String str, out Id id) => TryParse(str.AsSpan(), out id);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Boolean TryParse(String str, Idf format, out Id id) => TryParse(str.AsSpan(), format, out id);

#endif

    public static Boolean TryParse(ReadOnlySpan<char> chars, out Id id)
    {
        var len = chars.Length;

        if (len == 15) return TryParseBase85(chars, out id);

        if (len == 16) return TryParseBase64(chars, out id);

        if (len == 17) return TryParseBase58(chars, out id);

        if (len == 18) return TryParseBase64Path2(chars, out id);

        if (len == 19) return TryParseBase64Path3(chars, out id);

        if (len == 20) return TryParseBase32(chars, out id);

        if (len == 24) return TryParseHex(chars, out id);

        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<char> chars, Idf format, out Id id)
    {
        if (format == Idf.Hex || format == Idf.HexUpper) return TryParseHex(chars, out id);

        if (format == Idf.Base32 || format == Idf.Base32Upper) return TryParseBase32(chars, out id);

        if (format == Idf.Base58) return TryParseBase58(chars, out id);

        if (format == Idf.Base64 || format == Idf.Base64Url) return TryParseBase64(chars, out id);

        if (format == Idf.Base85) return TryParseBase85(chars, out id);

        if (format == Idf.Base64Path2) return TryParseBase64Path2(chars, out id);

        if (format == Idf.Base64Path3) return TryParseBase64Path3(chars, out id);

        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<byte> bytes, out Id id)
    {
        var len = bytes.Length;

        if (len == 15) return TryParseBase85(bytes, out id);

        if (len == 16) return TryParseBase64(bytes, out id);

        if (len == 17) return TryParseBase58(bytes, out id);

        if (len == 18) return TryParseBase64Path2(bytes, out id);

        if (len == 19) return TryParseBase64Path3(bytes, out id);

        if (len == 20) return TryParseBase32(bytes, out id);

        if (len == 24) return TryParseHex(bytes, out id);

        id = default;
        return false;
    }

    public static Boolean TryParse(ReadOnlySpan<byte> bytes, Idf format, out Id id)
    {
        if (format == Idf.Hex || format == Idf.HexUpper) return TryParseHex(bytes, out id);

        if (format == Idf.Base32 || format == Idf.Base32Upper) return TryParseBase32(bytes, out id);

        if (format == Idf.Base58) return TryParseBase58(bytes, out id);

        if (format == Idf.Base64 || format == Idf.Base64Url) return TryParseBase64(bytes, out id);

        if (format == Idf.Base85) return TryParseBase85(bytes, out id);

        if (format == Idf.Base64Path2) return TryParseBase64Path2(bytes, out id);

        if (format == Idf.Base64Path3) return TryParseBase64Path3(bytes, out id);

        id = default;
        return false;
    }

    #endregion TryParse
}