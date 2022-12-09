using MessagePack;
using MessagePack.Resolvers;
using System.Runtime.Serialization;

namespace IT.Tests;

public class IdMessagePackTest
{
    [DataContract]
    public record MyRecord
    {
        [DataMember(Order = 0)]
        public Id Id { get; set; }

        [DataMember(Order = 1)]
        public string Name { get; set; }

        public string ToJsonArray() => $"[\"{Id:/}\",\"{Name}\"]";
    }

    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SerializeDeserialize()
    {
        var id = Id.NewObjectId();

        var bytes = id.ToByteArray();

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Serialize(id));

        var options = MessagePackSerializerOptions.Standard.WithResolver(MessagePack.Resolvers.IdResolver.Instance);

        var serialized = MessagePackSerializer.Serialize(id, options);

        var serializedSpan = serialized.AsSpan().Slice(2);

        Assert.IsTrue(serializedSpan.SequenceEqual(bytes));

        var json = MessagePackSerializer.ConvertToJson(serialized);

        Assert.That(json, Is.EqualTo($"\"{id:/}\""));

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Deserialize<Id>(serialized));

        var id2 = MessagePackSerializer.Deserialize<Id>(serialized, options);

        Assert.That(id, Is.EqualTo(id2));
    }

    [Test]
    public void SerializeDeserialize_Nullable()
    {
        Id? id = Id.NewObjectId();

        var bytes = id.Value.ToByteArray();

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Serialize(id));

        var options = MessagePackSerializerOptions.Standard.WithResolver(MessagePack.Resolvers.IdResolver.Instance);

        var serialized = MessagePackSerializer.Serialize(id, options);

        var serializedSpan = serialized.AsSpan().Slice(2);

        Assert.IsTrue(serializedSpan.SequenceEqual(bytes));

        var json = MessagePackSerializer.ConvertToJson(serialized);

        Assert.That(json, Is.EqualTo($"\"{id:/}\""));

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Deserialize<Id?>(serialized));

        var id2 = MessagePackSerializer.Deserialize<Id?>(serialized, options);

        Assert.That(id, Is.EqualTo(id2));
    }

    [Test]
    public void SerializeDeserialize_MyRecord()
    {
        var obj = new MyRecord { Id = Id.NewObjectId(), Name = "Ivan" };

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Serialize(obj));

        var resolver = CompositeResolver.Create(
            StandardResolver.Instance,
            IdResolver.Instance
            );

        var options = MessagePackSerializerOptions.Standard.WithResolver(resolver);

        var serialized = MessagePackSerializer.Serialize(obj, options);

        var json = MessagePackSerializer.ConvertToJson(serialized);

        Assert.That(json, Is.EqualTo(obj.ToJsonArray()));

        Assert.Throws<MessagePackSerializationException>(() => MessagePackSerializer.Deserialize<MyRecord>(serialized));

        var obj2 = MessagePackSerializer.Deserialize<MyRecord>(serialized, options);

        Assert.That(obj, Is.EqualTo(obj2));
    }
}