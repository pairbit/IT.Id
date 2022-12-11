#if NETCOREAPP3_1_OR_GREATER
using MemoryPack;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Tests;

public class IdMemoryPackTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void SerializeDeserialize()
    {
        var id = Id.NewObjectId();

        Span<byte> bytes = stackalloc byte[Unsafe.SizeOf<Id>()];
        ref byte pointer = ref MemoryMarshal.GetReference(bytes);
        Unsafe.WriteUnaligned(ref pointer, id);

        var serialized = MemoryPackSerializer.Serialize(id);

        Assert.That(bytes.SequenceEqual(serialized), Is.True);

        id.TryWrite(bytes);

        Assert.That(bytes.SequenceEqual(serialized), Is.True);

        Assert.That(id.ToByteArray().SequenceEqual(serialized), Is.True);

        var id2 = MemoryPackSerializer.Deserialize<Id>(serialized);

        Assert.That(id, Is.EqualTo(id2));
    }

    [Test]
    public void SerializeDeserialize_Nullable()
    {
        Id? id = Id.NewObjectId();

        var serialized = MemoryPackSerializer.Serialize(id);

        var id2 = MemoryPackSerializer.Deserialize<Id?>(serialized);

        Assert.That(id, Is.EqualTo(id2));
    }

    [Test]
    public void SerializeDeserialize_MyRecord()
    {
        var obj = new MyRecord { Id = Id.NewObjectId(), Name = "Ivan" };

        var serialized = MemoryPackSerializer.Serialize(obj);

        var obj2 = MemoryPackSerializer.Deserialize<MyRecord>(serialized);

        Assert.That(obj, Is.EqualTo(obj2));

        obj2 = null;

        MemoryPackSerializer.Deserialize(serialized, ref obj2);

        Assert.That(obj, Is.EqualTo(obj2));
    }
}
#endif