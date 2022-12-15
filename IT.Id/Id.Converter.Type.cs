using IT;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.ComponentModel;

public class IdConverter : TypeConverter
{
    private static readonly Type StringType = typeof(string);
    private static readonly Type CharsType = typeof(char[]);
    private static readonly Type MemoryCharsType = typeof(Memory<char>);
    private static readonly Type ReadOnlyMemoryCharsType = typeof(ReadOnlyMemory<char>);
    private static readonly Type BytesType = typeof(byte[]);
    private static readonly Type MemoryBytesType = typeof(Memory<byte>);
    private static readonly Type ReadOnlyMemoryBytesType = typeof(ReadOnlyMemory<byte>);

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == StringType ||
           sourceType == CharsType || sourceType == MemoryCharsType || sourceType == ReadOnlyMemoryCharsType ||
           sourceType == BytesType || sourceType == MemoryBytesType || sourceType == ReadOnlyMemoryBytesType ||
           base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string str) return Id.Parse(str);

        if (value is char[] chars) return Id.Parse(chars);

        if (value is ReadOnlyMemory<char> readOnlyMemoryChars) return Id.Parse(readOnlyMemoryChars.Span);

        if (value is Memory<char> memoryChars) return Id.Parse(memoryChars.Span);

        if (value is byte[] bytes)
        {
            if (bytes.Length == 12) return Unsafe.ReadUnaligned<Id>(ref bytes[0]);

            return Id.Parse(bytes);
        }

        if (value is ReadOnlyMemory<byte> readOnlyMemoryBytes)
        {
            var span = readOnlyMemoryBytes.Span;

            if (span.Length == 12) return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(span));

            return Id.Parse(span);
        }

        if (value is Memory<byte> memoryBytes)
        {
            var span = memoryBytes.Span;

            if (span.Length == 12) return Unsafe.ReadUnaligned<Id>(ref MemoryMarshal.GetReference(span));

            return Id.Parse(span);
        }

        return base.ConvertFrom(context, culture, value);
    }
}