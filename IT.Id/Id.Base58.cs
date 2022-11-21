namespace System;

public readonly partial struct Id
{
    public String ToBase58()
    {
        Span<Byte> buffer = stackalloc[] {
            (byte)(_timestamp >> 24),
            (byte)(_timestamp >> 16),
            (byte)(_timestamp >> 8),
            (byte)(_timestamp),
            (byte)(_b >> 24),
            (byte)(_b >> 16),
            (byte)(_b >> 8),
            (byte)(_b),
            (byte)(_c >> 24),
            (byte)(_c >> 16),
            (byte)(_c >> 8),
            (byte)(_c)
        };
        return Base58.Encode_FAST(buffer);
    }

    public String ToBase58_New()
    {
        Span<Byte> buffer = stackalloc[] {
            (byte)(_timestamp >> 24),
            (byte)(_timestamp >> 16),
            (byte)(_timestamp >> 8),
            (byte)(_timestamp),
            (byte)(_b >> 24),
            (byte)(_b >> 16),
            (byte)(_b >> 8),
            (byte)(_b),
            (byte)(_c >> 24),
            (byte)(_c >> 16),
            (byte)(_c >> 8),
            (byte)(_c)
        };
        return Base58.Encode_New(buffer);
    }

    private static Id ParseBase58(ReadOnlySpan<Char> value)
    {
        var len = value.Length;
        if (len < 12 || len > 17) throw new ArgumentOutOfRangeException(nameof(value), len, "String must be 12 to 17 characters long");

        Span<Byte> bytes = stackalloc Byte[18];

        Base58.Decode(value, bytes, out var written);

        bytes = bytes.Slice(written - 12, 12);

        FromByteArray(bytes, 0, out var timestamp, out var b, out var c);

        return new Id(timestamp, b, c);
    }


}