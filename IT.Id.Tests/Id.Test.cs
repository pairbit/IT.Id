namespace IT.Tests;

public class IdTest
{
    [SetUp]
    public void Setup()
    {
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
            //Assert.That(Id.ParseBase64(id.ToString().AsSpan()), Is.EqualTo(Id.ParseBase64_2(id.ToString().AsSpan())));

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