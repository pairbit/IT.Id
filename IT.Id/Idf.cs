namespace System;

public enum Idf
{
    /// <summary>
    /// Base16 Lower
    /// </summary>
    /// <example>62a84f674031e78d474fe23f</example>
    /// <remarks>
    /// Alphabet: 0123456789abcdefg <br/>
    /// Example: 62a84f674031e78d474fe23f <br/>
    /// Length: 24 <br/>
    /// Format: h
    /// </remarks>
    Hex = 15,

    /// <summary>
    /// Base16 Upper
    /// </summary>
    /// <example>62A84F674031E78D474FE23F</example>
    /// <remarks>
    /// Alphabet: 0123456789ABCDEFG <br/>
    /// Example: 62A84F674031E78D474FE23F <br/>
    /// Length: 24 <br/>
    /// Format: H
    /// </remarks>
    HexUpper = 16,

    /// <summary>
    /// Base32 Crockford's Lower
    /// </summary>
    /// <example>ce0ytmyc14fgvd7358b0</example>
    /// <remarks>
    /// Alphabet: 0123456789abcdefghjkmnpqrstvwxyz <br/>
    /// Example: ce0ytmyc14fgvd7358b0 <br/>
    /// Length: 20 <br/>
    /// Format: v
    /// </remarks>
    Base32 = 32,

    /// <summary>
    /// Base32 Crockford's Classic
    /// </summary>
    /// <example>CE0YTMYC14FGVD7358B0</example>
    /// <remarks>
    /// Alphabet: 0123456789ABCDEFGHJKMNPQRSTVWXYZ <br/>
    /// Example: CE0YTMYC14FGVD7358B0 <br/>
    /// Length: 20 <br/>
    /// Format: V
    /// </remarks>
    Base32Upper = 33,

    //Base36 = 36,

    /// <summary>
    /// RFC 9285 <br/>
    /// https://datatracker.ietf.org/doc/html/rfc9285
    /// </summary>
    //Base45 = 45,

    /// <summary>
    /// Base58 Bitcoin Fixed
    /// </summary>
    /// <example>2ryw1nk6d1eiGQSL6</example>
    /// <remarks>
    /// Alphabet: 123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz <br/>
    /// Example: 2ryw1nk6d1eiGQSL6 <br/>
    /// Length: 17 <br/>
    /// Format: i
    /// </remarks>
    Base58 = 58,

    /// <summary>
    /// Base64 Classic
    /// </summary>
    /// <example>YqhPZ0Ax541HT+I/</example>
    /// <remarks>
    /// Alphabet: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/ <br/>
    /// Example: YqhPZ0Ax541HT+I/ <br/>
    /// Length: 16 <br/>
    /// Format: /
    /// </remarks>
    Base64 = 64,

    /// <summary>
    /// RFC 7515 <br/>
    /// Char '/' repalce to '_', '+' repalce to '-'
    /// </summary>
    /// <remarks>
    /// Alphabet: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_ <br/>
    /// Example: YqhPZ0Ax541HT-I_ <br/>
    /// Length: 16 <br/>
    /// Format: _ (default)
    /// </remarks>
    /// <example>YqhPZ0Ax541HT-I_</example>
    /// <see cref="https://datatracker.ietf.org/doc/html/rfc7515#appendix-C"/>
    Base64Url = 65,

    /// <summary>
    /// Win   = 38^2 + 38 = 1 482 max <br/>
    /// Linux = 64^2 + 64 = 4 160 max
    /// </summary>
    /// <example>_\I\-TH145xA0ZPhqY</example>
    /// <remarks>
    /// Alphabet: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\ <br/>
    /// Example for Win: _\I\-TH145xA0ZPhqY <br/>
    /// Example for Linux: _/I/-TH145xA0ZPhqY <br/>
    /// Length: 18 <br/>
    /// Format: //
    /// </remarks>
    Path2 = 66,

    /// <summary>
    /// Win   = 38^3 + 38^2 + 38 =  56 354 max <br/>
    /// Linux = 64^3 + 64^2 + 64 = 266 304 max
    /// </summary>
    /// <example>_\I\-\TH145xA0ZPhqY</example>
    /// <remarks>
    /// Alphabet: ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\ <br/>
    /// Example for Win: _\I\-\TH145xA0ZPhqY <br/>
    /// Example for Linux: _/I/-/TH145xA0ZPhqY <br/>
    /// Length: 19 <br/>
    /// Format: ///
    /// </remarks>
    Path3 = 67,

    /// <summary>
    /// Z85 Xml <br/>
    /// Char '&amp;' repalce to '_', '&lt;' repalce to '~', '&gt;' repalce to '|'
    /// </summary>
    /// <example>v{IV^PiNKcFO_~|</example>
    /// <remarks>
    /// Alphabet: 0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$# <br/>
    /// Example: v{IV^PiNKcFO_~| <br/>
    /// Length: 15 <br/>
    /// Format: |
    /// </remarks>
    Base85 = 85
}