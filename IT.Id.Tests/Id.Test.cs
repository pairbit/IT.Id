namespace Tests;

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
    public void IdMachine()
    {
        var id = Id.New();
        var objectId = Id.NewObjectId();

        Assert.IsNull(id.MachineName);
        Assert.IsNull(objectId.MachineName);

        Assert.That(id.Machine, Is.EqualTo(Id.MachineHash24));
        Assert.That(objectId.Machine, Is.Not.EqualTo(Id.MachineHash24));

        Assert.Throws<ArgumentNullException>(() => Id.Machines(null));
        Assert.Throws<ArgumentException>(() => Id.Machines());
        Assert.Throws<ArgumentException>(() => Id.Machines("abc", "abc"));

        Id.Machines(Environment.MachineName, "IT2");
        Assert.Throws<InvalidOperationException>(() => Id.Machines("IT3"));

        Assert.That(id.MachineName, Is.EqualTo(Environment.MachineName));
        Assert.IsNull(objectId.MachineName);
    }
}