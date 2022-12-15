using System.Globalization;

namespace System.ComponentModel;

public class IdConverter : TypeConverter
{
    private static readonly Type StringType = typeof(string);

    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
        => sourceType == StringType || base.CanConvertFrom(context, sourceType);

    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        if (value is string chars) return IT.Id.Parse(chars);

        return base.ConvertFrom(context, culture, value);
    }
}