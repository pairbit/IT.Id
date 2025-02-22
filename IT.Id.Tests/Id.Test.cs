using System.IO.Hashing;

namespace IT.Tests;

public class IdTest
{
    [SetUp]
    public void Setup()
    {
    }

    //[Test]
    public void StaticID()
    {
        var timestamp = (uint)1739867386;
        var machine = (uint)13371679;
        var pid = unchecked((ushort)-70000);
        var inc = (uint)(Id._staticIncrement + 1) & 0x00ffffff;

        var id = Id.New(timestamp);
        Assert.That(id.Timestamp, Is.EqualTo(timestamp));
        Assert.That(id.Machine, Is.EqualTo(machine));
        Assert.That(id.Pid, Is.EqualTo(pid));
        Assert.That(id.Increment, Is.EqualTo(inc));

        Assert.That(id.ToString(), Is.EqualTo("Z7RE+swJH+6Q/u6R"));
        Assert.That(id.ToUtf8String().AsSpan().SequenceEqual("Z7RE+swJH+6Q/u6R"u8), Is.True);
    }

    //[Test]
    public void StaticObjectID()
    {
        var timestamp = (uint)1739867386;
        var rand = (ulong)842072483480;
        var inc = (uint)(Id._staticIncrement + 1) & 0x00ffffff;

        var objectId = Id.NewObjectId(timestamp);
        Assert.That(objectId.Timestamp, Is.EqualTo(timestamp));
        Assert.That(objectId.Random, Is.EqualTo(rand));
        Assert.That(objectId.Increment, Is.EqualTo(inc));

        Assert.That(objectId.ToString(), Is.EqualTo("Z7RE+sQPbmaY/u6R"));
        Assert.That(objectId.ToUtf8String().AsSpan().SequenceEqual("Z7RE+sQPbmaY/u6R"u8), Is.True);
    }

    [Test]
    public void ToStringAndParse()
    {
        Span<byte> bytes = stackalloc byte[16];
        Span<char> chars = stackalloc char[16];

        for (int i = 0; i < 1000; i++)
        {
            var id = Id.New();
            var str = id.ToString();
            var utf8str = id.ToUtf8String();

            id.ToUtf8String(bytes);
            Assert.That(bytes.SequenceEqual(utf8str), Is.True);

            bytes.Clear();
            Assert.That(id.TryFormat(bytes, out var written), Is.True);
            Assert.That(bytes.SequenceEqual(utf8str), Is.True);
            Assert.That(written, Is.EqualTo(16));

            Assert.That(Id.Parse(str), Is.EqualTo(id));

            Assert.That(Id.Parse(utf8str), Is.EqualTo(id));
            Assert.That(Id.TryParse(utf8str, out var id3), Is.True);
            Assert.That(id3, Is.EqualTo(id));

#if NET
            id.ToString(chars);
            Assert.That(chars.SequenceEqual(str), Is.True);

            Assert.That(id.TryFormat(chars, out var written2), Is.True);
            Assert.That(chars.SequenceEqual(str), Is.True);
            Assert.That(written2, Is.EqualTo(16));

            Assert.That(Id.TryParse(str, out var id2), Is.True);
            Assert.That(id2, Is.EqualTo(id));
#endif
        }
    }

    [Test]
    public void NewTest()
    {
        var id = Id.New();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.New(id.Timestamp);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Random, Is.EqualTo(nextId.Random));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));

            Assert.That(id.IsCurrentMachine, Is.True);
            Assert.That(id.IsCurrentPid, Is.True);
            Assert.That(id.IsCurrentRandom, Is.False);
        }

        id = Id.NewObjectId();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.NewObjectId(id.Timestamp);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Random, Is.EqualTo(nextId.Random));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));

            Assert.That(id.IsCurrentMachine, Is.False);
            Assert.That(id.IsCurrentPid, Is.False);
            Assert.That(id.IsCurrentRandom, Is.True);
        }
    }

    [Test]
    public void MinMax()
    {
        var min = Id.Min;

        Assert.That(new Id(min.Timestamp, min.Machine, min.Pid, min.Increment), Is.EqualTo(min));
        Assert.That(new Id(min.Timestamp, min.Random, min.Increment), Is.EqualTo(min));

        var max = Id.Max;
        Assert.That(new Id(max.Timestamp, max.Machine, max.Pid, max.Increment), Is.EqualTo(max));
        Assert.That(new Id(max.Timestamp, max.Random, max.Increment), Is.EqualTo(max));
    }

    [Test]
    public void NextTest()
    {
        var id = Id.New();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.Next(id);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Random, Is.EqualTo(nextId.Random));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));

            Assert.That(id.IsCurrentMachine, Is.True);
            Assert.That(id.IsCurrentPid, Is.True);
            Assert.That(id.IsCurrentRandom, Is.False);
        }

        id = Id.NewObjectId();
        for (int i = 1; i < 1000; i++)
        {
            var nextId = Id.Next(id);
            Assert.That(id.Timestamp, Is.EqualTo(nextId.Timestamp));
            Assert.That(id.Random, Is.EqualTo(nextId.Random));
            Assert.That(id.Machine, Is.EqualTo(nextId.Machine));
            Assert.That(id.Pid, Is.EqualTo(nextId.Pid));
            Assert.That(unchecked(id.Increment + i), Is.EqualTo(nextId.Increment));

            Assert.That(id.IsCurrentMachine, Is.False);
            Assert.That(id.IsCurrentPid, Is.False);
            Assert.That(id.IsCurrentRandom, Is.True);
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
        Span<byte> span = stackalloc byte[12];
        for (int i = 0; i < 1000; i++)
        {
            var id = Id.New();
            //Assert.That(new Id(id.Created.Date, id.Machine, id.Pid, id.Increment), Is.EqualTo(id));
            Assert.That(new Id(id.Timestamp, id.Machine, id.Pid, id.Increment), Is.EqualTo(id));
            Assert.That(new Id(id.Timestamp, id.Random, id.Increment), Is.EqualTo(id));

            Assert.That(new Id(id.ToByteArray()), Is.EqualTo(id));

            id.TryWrite(span);
            Assert.That(new Id(span), Is.EqualTo(id));
            span.Clear();
        }

        for (int i = 0; i < 1000; i++)
        {
            var id = Id.NewObjectId();
            //Assert.That(new Id(id.Created.Date, id.Machine, id.Pid, id.Increment), Is.EqualTo(id));
            Assert.That(new Id(id.Timestamp, id.Machine, id.Pid, id.Increment), Is.EqualTo(id));
            Assert.That(new Id(id.Timestamp, id.Random, id.Increment), Is.EqualTo(id));

            Assert.That(new Id(id.ToByteArray()), Is.EqualTo(id));

            id.TryWrite(span);
            Assert.That(new Id(span), Is.EqualTo(id));
            span.Clear();
        }
    }

    [Test]
    public void XXH()
    {
        var id = Id.New();
        Span<byte> raw = stackalloc byte[12];
        for (int i = 0; i < 1000; i++)
        {
            id = Id.New();

            Assert.That(id.TryWrite(raw), Is.True); ;

            var xxh32 = id.XXH32();
            var xxh64 = id.XXH64();

            Assert.That(Internal.XXH32.DigestOf(raw), Is.EqualTo(xxh32));
            Assert.That(Internal.XXH64.DigestOf(raw), Is.EqualTo(xxh64));

            Assert.That(XxHash32.HashToUInt32(raw), Is.EqualTo(xxh32));
            Assert.That(XxHash64.HashToUInt64(raw), Is.EqualTo(xxh64));

            raw.Clear();
        }
    }
}