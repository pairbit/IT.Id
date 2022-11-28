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
}