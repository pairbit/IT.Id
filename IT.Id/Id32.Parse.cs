using IT.Internal;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace IT;

public readonly partial struct Id32
#if NET7_0_OR_GREATER
//: ISpanParsable<Id>
#endif
{
//#if NET7_0_OR_GREATER

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    static Id IParsable<Id>.Parse(String s, IFormatProvider? provider) => Parse(s ?? throw new ArgumentNullException(nameof(s)));

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    static Id ISpanParsable<Id>.Parse(ReadOnlySpan<Char> chars, IFormatProvider? provider) => Parse(chars);

//#endif

#if NETSTANDARD2_0

    public static Id32 Parse(String str) => Parse(str.AsSpan());

#endif

    public static Id32 Parse(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new FormatException();

        var version = chars[len - 1];

        if (version == Base64.Format || version == Base64.FormatUrl) return ParseBase64(chars);
        if (version == Hex.FormatLower || version == Hex.FormatUpper) return ParseHex(chars);

        throw new FormatException();
    }

//#if NET7_0_OR_GREATER

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    static Boolean IParsable<Id>.TryParse(String? s, IFormatProvider? provider, out Id id) => TryParse(s, out id);

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    static Boolean ISpanParsable<Id>.TryParse(ReadOnlySpan<Char> chars, IFormatProvider? provider, out Id id) => TryParse(chars, out id);

//#endif
}