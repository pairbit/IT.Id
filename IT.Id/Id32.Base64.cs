using IT.Internal;
using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id32
{
    public String ToBase64Url()
    {
        var value = BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<uint>(ref Unsafe.AsRef(in _value0)));

        var len = GetLengthBase64(value);

        if (len == 1)
        {
            var base64Url = new string('\0', 17);

            unsafe
            {
                fixed (char* ptr = base64Url)
                {
                    var chars = new Span<Char>(ptr, 17);

                    _id.TryToBase64Url(chars);

                    chars[16] = Base64.tableNumUrl[value];
                }
            }

            return base64Url;
        }
        else
        {
            len += 17;

            var base64Url = new string('\0', len);

            unsafe
            {
                fixed (char* ptr = base64Url)
                {
                    var chars = new Span<Char>(ptr, len);

                    _id.TryToBase64Url(chars);

                    TryToBase64Url(chars.Slice(16), value);

                    chars[len - 1] = Base64.FormatUrl;
                }
            }

            return base64Url;
        }
    }

    public static Id32 ParseBase64(ReadOnlySpan<Char> chars)
    {
        var len = chars.Length;

        if (len == 0) throw new FormatException();

        if (len == 17)
        {
            len = 1;
        }
        else
        {
            var version = chars[len - 1];

            if (version != Base64.Format && version != Base64.FormatUrl) throw new FormatException();

            len -= 17;

            if (len == 0) throw new FormatException();
        }

        Id32 id32 = default;

        ref var b = ref Unsafe.As<Id32, byte>(ref id32);

        Unsafe.WriteUnaligned(ref b, Id.ParseBase64(chars.Slice(0, 16)));

        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 12), BinaryPrimitives.ReverseEndianness(Base64.ToUInt32(chars.Slice(16, len))));

        return id32;
    }
}