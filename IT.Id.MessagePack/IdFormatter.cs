using IT;
using System;
using System.Buffers;

namespace MessagePack.Formatters;

public class IdFormatter : IMessagePackFormatter<Id>
{
    private const int Length = 12;

    public readonly static IMessagePackFormatter<Id> Instance = new IdFormatter();

    public Id Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
    {
        var bin = reader.ReadBytes();

        if (bin == null) throw new MessagePackSerializationException(
                $"Unexpected msgpack code {MessagePackCode.Nil} ({MessagePackCode.ToFormatName(MessagePackCode.Nil)}) encountered.");

        var seq = bin.Value;

        if (seq.IsSingleSegment)
        {
            return new Id(seq.First.Span);
        }
        else
        {
            Span<byte> buf = stackalloc byte[Length];

            seq.CopyTo(buf);

            return new Id(buf);
        }
    }

    public void Serialize(ref MessagePackWriter writer, Id id, MessagePackSerializerOptions options)
    {
        writer.WriteBinHeader(Length);

        var buffer = writer.GetSpan(Length);

        id.TryWrite(buffer);

        writer.Advance(Length);
    }
}