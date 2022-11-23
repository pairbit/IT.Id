using System.Buffers.Text;

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

        TestId(new Id8(id, 0));
        TestId(new Id8(id, 15));
        TestId(new Id8(id, 16));
        TestId(new Id8(id, 63));
        TestId(new Id8(id, 65));
        TestId(new Id8(id, 255));
    }

    private void TestId(Id8 id)
    {
        var base64 = id.ToString(Idf.Base64Url);
        var hexUpper = id.ToString(Idf.HexUpper);
        var hexLower = id.ToString(Idf.Hex);

        Assert.That(Id8.Parse(base64), Is.EqualTo(id));
        Assert.That(Id8.Parse(hexUpper), Is.EqualTo(id));
        Assert.That(Id8.Parse(hexLower), Is.EqualTo(id));
    }
}