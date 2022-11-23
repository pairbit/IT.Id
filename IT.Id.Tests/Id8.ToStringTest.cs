namespace Tests;

public class Id8ToStringTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Run()
    {
        var id = Id.New();
        var id8 = new Id8(id, 63);

        Assert.That(id8, Is.Not.EqualTo(new Id8(Id.New(), 63)));

        Assert.That(id8, Is.EqualTo(new Id8(id, 63)));

        var s = id8.ToString();

        Assert.That(Id8.Parse(s), Is.EqualTo(id8));

        id8 = new Id8(id, 65);

        s = id8.ToString();

        Assert.That(Id8.Parse(s), Is.EqualTo(id8));

        id8 = new Id8(id, 255);

        s = id8.ToString();

        Assert.That(Id8.Parse(s), Is.EqualTo(id8));
    }
}