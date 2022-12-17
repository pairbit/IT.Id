using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography;

namespace IT.Tests;

public class IdTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void New()
    {
        var start = Id._staticIncrement;

        //var ta = Id.GetSystemTimeAsTicks();

        var id = Id.New();

        Assert.That(id.GetHashCode(), Is.EqualTo(id.GetHashCode2()));

        Assert.That(id.Timestamp, Is.EqualTo(id.Timestamp2));
        Assert.That(id.Timestamp, Is.EqualTo(id.Timestamp3));
        Assert.That(id.Machine, Is.EqualTo(id.Machine2));
        Assert.That(id.Pid, Is.EqualTo(id.Pid2));
        Assert.That(id.Increment, Is.EqualTo(id.Increment2));

        var id2 = Id.New_Old();

        var end = Id._staticIncrement;

        var count = end - start;

        Assert.That(count, Is.EqualTo(2));

        Assert.That(id, Is.Not.EqualTo(id2));

        Assert.That(id.Timestamp, Is.EqualTo(id2.Timestamp));

        Assert.That(id.Machine, Is.EqualTo(id2.Machine));

        Assert.That(id.Pid, Is.EqualTo(id2.Pid));

        Assert.That(id.Increment, Is.Not.EqualTo(id2.Increment));

        Assert.That(id.Increment, Is.EqualTo(id2.Increment - 1));
    }

    //[Test]
    public void New_Collision()
    {
        var start = Id._staticIncrement;

        var ids = new Id[256 * 256 * 256];

        for (int i = 0; i < ids.Length; i++)
        {
            ids[i] = Id.New();
        }

        var end = Id._staticIncrement;

        var count = end - start;

        Assert.That(count, Is.EqualTo(ids.Length));

        var hash = new HashSet<Id>(ids);

        Assert.That(count, Is.EqualTo(hash.Count));

        var id = ids[0];

        Assert.That(hash.Add(id), Is.False);
        Assert.That(count, Is.EqualTo(hash.Count));

        var timestamp = id.Timestamp;

        for (int i = 0; i < ids.Length; i++)
        {
            if (ids[i].Timestamp != timestamp)
            {
                Console.WriteLine($"[{i:N0}] {ids[i]} ({ids[i].Created.ToLocalTime()}) != {id} ({id.Created.ToLocalTime()})");
                return;
            }
        }

        Console.WriteLine("Ok");
    }

    [Test]
    public void Sizeof12()
    {
        Assert.That(System.Runtime.InteropServices.Marshal.SizeOf<Id>(), Is.EqualTo(12));
    }

    [Test]
    public void Ctor()
    {
        var id = Id.New();

        Assert.Multiple(() =>
        {
            Assert.That(Id.Parse(id.ToString()), Is.EqualTo(id));

            Assert.That(new Id(id.Timestamp, id.Machine, id.Pid, id.Increment), Is.EqualTo(id));

            Assert.That(new Id(id.ToByteArray()), Is.EqualTo(id));

            Span<byte> span = stackalloc byte[12];
            id.TryWrite(span);
            Assert.That(new Id(span), Is.EqualTo(id));
        });
    }

    [Test]
    public void XXH()
    {
        for (int i = 0; i < 1000; i++)
        {
            var id = Id.New();

            var array = id.ToByteArray();

            Assert.Multiple(() =>
            {
                Assert.That(Internal.XXH32.DigestOf(array), Is.EqualTo(id.XXH32()));
                Assert.That(Internal.XXH64.DigestOf(array), Is.EqualTo(id.XXH64()));

                Assert.That(id.XXH64_2(), Is.EqualTo(id.XXH64()));
            });
        }
    }

    [Test]
    public void IdMachine()
    {
        var id = Id.New();
        var objectId = Id.NewObjectId();
        Assert.Multiple(() =>
        {
            Assert.That(id.MachineName, Is.Null);
            Assert.That(objectId.MachineName, Is.Null);
            Assert.That(id.Machine, Is.EqualTo(Id.MachineHash24));
            Assert.That(objectId.Machine, Is.Not.EqualTo(Id.MachineHash24));
        });
        string[] names = null!;

        Assert.Throws<ArgumentNullException>(() => Id.Machines(names));
        Assert.Throws<ArgumentException>(() => Id.Machines());
        Assert.Throws<ArgumentException>(() => Id.Machines("abc", "abc"));

        Id.Machines(Environment.MachineName, "IT2");
        Assert.Throws<InvalidOperationException>(() => Id.Machines("IT3"));
        Assert.Multiple(() =>
        {
            Assert.That(id.MachineName, Is.EqualTo(Environment.MachineName));
            Assert.That(objectId.MachineName, Is.Null);
        });
    }
}