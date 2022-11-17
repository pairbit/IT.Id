﻿namespace System;

public enum Idf
{
    /// <summary>
    /// Base16 Lower
    /// </summary>
    /// <example>62a84f674031e78d474fe23f</example>
    Hex,

    /// <summary>
    /// Base16 Upper
    /// </summary>
    /// <example>62A84F674031E78D474FE23F</example>
    HexUpper,

    /// <summary>
    /// Crockford's (0123456789ABCDEFGHJKMNPQRSTVWXYZ)
    /// </summary>
    /// <example>CAM4YST067KRTHTFW8ZG</example>
    Base32,

    /// <summary>
    /// Bitcoin (123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz)
    /// </summary>
    /// <example>2ryw1nk6d1eiGQSL6</example>
    Base58,

    /// <example>YqhPZ0Ax541HT+I/</example>
    Base64,

    /// <summary>
    /// RFC 7515 (https://datatracker.ietf.org/doc/html/rfc7515#appendix-C) <br/>
    /// Char '/' repalce to '_', '+' repalce to '-'
    /// </summary>
    /// <example>YqhPZ0Ax541HT-I_</example>
    Base64Url,

    /// <summary>
    /// Z85Xml (0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$#) <br/>
    /// Char '&amp;' repalce to '_', '&lt;' repalce to '~', '&gt;' repalce to '|'
    /// </summary>
    /// <example>v{IV^PiNKcFO_~|</example>
    Base85,

    /// <summary>
    /// Win   = 38^2 + 38 = 1 482 max <br/>
    /// Linux = 64^2 + 64 = 4 160 max
    /// </summary>
    /// <example>_\I\-TH145xA0ZPhqY</example>
    Path2,

    /// <summary>
    /// Win   = 38^3 + 38^2 + 38 =  56 354 max <br/>
    /// Linux = 64^3 + 64^2 + 64 = 266 304 max
    /// </summary>
    /// <example>_\I\-\TH145xA0ZPhqY</example>
    Path3
}