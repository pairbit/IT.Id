﻿using IT;
using System;
using System.Buffers;

namespace System.Text.Json.Serialization.Converters;

public class IdConverter : JsonConverter<Id>
{
    private Idf _format = Idf.Base64Url;

    public Idf Format { get => _format; set => _format = value; }

    public override Id Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String) throw new JsonException("Expected string");

        if (reader.HasValueSequence)
        {
            var seq = reader.ValueSequence;

            var llen = seq.Length;

            if (llen < 15 || llen != 24 && llen > 20) throw new JsonException($"The id cannot be {llen} bytes long. The id must be 24 bytes long or between 15 and 20");

            Span<byte> buffer = stackalloc byte[(int)llen];

            seq.CopyTo(buffer);

            return Parse(buffer);
        }

        return Parse(reader.ValueSpan);
    }

    public override void Write(Utf8JsonWriter writer, Id value, JsonSerializerOptions options)
    {
        Span<byte> bytes = stackalloc byte[Id.GetLength(_format)];

        if (!value.TryFormat(bytes, out _, _format))
            throw new JsonException($"The id does not support the '{_format}' format");

        writer.WriteStringValue(bytes);
    }

    private static Id Parse(ReadOnlySpan<byte> bytes)
    {
        try
        {
            return Id.Parse(bytes);
        }
        catch (ArgumentException ex)
        {
            throw new JsonException($"Id invalid: {ex.Message}");
        }
        catch (FormatException ex)
        {
            throw new JsonException($"Id invalid: {ex.Message}");
        }
    }
}