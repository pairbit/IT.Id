namespace IT.Tests;

public class IdTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NextTest()
    {
        var id = Id.New();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.Next(id);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            Assert.That(id.Machine, Is.EqualTo(Id.CurrentMachine));
            Assert.That(id.IsCurrentMachine, Is.True);
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));
        }

        id = Id.NewObjectId();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.Next(id);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            //Assert.That(id.Random, Is.EqualTo(Id.CurrentRandom));
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));
        }
    }

    [Test]
    public void EqualsTest()
    {
        var staticIncrement = int.MaxValue - 1000;
        var a = (uint)Interlocked.Increment(ref staticIncrement) & 0x00ffffff;

        var b = (uint)(Interlocked.Increment(ref staticIncrement) & 0x00ffffff);

        var id = Id.New();

        var bytes = id.ToByteArray();

        var id2 = new Id(bytes);

        Assert.That(id != id2, Is.False);
        Assert.That(id == id2, Is.True);
        Assert.That(id.Equals((object)id2), Is.True);
        Assert.That(id.Equals(id2), Is.True);
        //Assert.That(id.Equals2(id2), Is.True);
        //Assert.That(id.Equals3(id2), Is.True);
        //Assert.That(id.Equals4(id2), Is.True);

        for (int i = 0; i < bytes.Length; i++)
        {
            bytes = id.ToByteArray();
            bytes[i]++;
            id2 = new Id(bytes);

            Assert.That(id != id2, Is.True);
            Assert.That(id == id2, Is.False);
            Assert.That(id.Equals((object)id2), Is.False);
            Assert.That(id.Equals(id2), Is.False);
            //Assert.That(id.Equals2(id2), Is.False);
            //Assert.That(id.Equals3(id2), Is.False);
            //Assert.That(id.Equals4(id2), Is.False);
        }
    }

    //[Test]
    //public void New()
    //{
    //    var start = Id._staticIncrement;

    //    //var ta = Id.GetSystemTimeAsTicks();

    //    var id = Id.New();

    //    //Assert.That(id.GetHashCode(), Is.EqualTo(id.GetHashCode2()));

    //    //Assert.That(id.Timestamp, Is.EqualTo(id.Timestamp2));
    //    //Assert.That(id.Timestamp, Is.EqualTo(id.Timestamp3));
    //    //Assert.That(id.Machine, Is.EqualTo(id.Machine2));
    //    //Assert.That(id.Pid, Is.EqualTo(id.Pid2));
    //    //Assert.That(id.Increment, Is.EqualTo(id.Increment2));

    //    //var id2 = Id.New_Old();

    //    var end = Id._staticIncrement;

    //    var count = end - start;

    //    Assert.That(count, Is.EqualTo(2));

    //    Assert.That(id, Is.Not.EqualTo(id2));

    //    Assert.That(id.Timestamp, Is.EqualTo(id2.Timestamp));

    //    Assert.That(id.Machine, Is.EqualTo(id2.Machine));

    //    Assert.That(id.Pid, Is.EqualTo(id2.Pid));

    //    Assert.That(id.Increment, Is.Not.EqualTo(id2.Increment));

    //    Assert.That(id.Increment, Is.EqualTo(id2.Increment - 1));
    //}

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
#if NET
            Assert.That(Id.Parse(id.ToUtf8String()), Is.EqualTo(id));
#endif
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

                //Assert.That(id.XXH32_2(), Is.EqualTo(id.XXH32()));
                //Assert.That(id.XXH64_2(), Is.EqualTo(id.XXH64()));
            });
        }
    }

}