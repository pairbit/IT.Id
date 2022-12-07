using System.Buffers.Text;

namespace IT.Tests;

public class Id32ToStringTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void Run()
    {
        var id = Id.New();
        var id32 = new Id32(id, 63);

        Assert.That(id32, Is.Not.EqualTo(new Id32(Id.New(), 63)));
        Assert.That(id32, Is.EqualTo(new Id32(id, 63)));

        TestId(new Id32(id, 0));
        TestId(new Id32(id, 15));
        TestId(new Id32(id, 16));
        TestId(new Id32(id, 63));
        TestId(new Id32(id, 65));
        TestId(new Id32(id, 255));
    }

    private void TestId(Id32 id)
    {
        var base64 = id.ToString(Idf.Base64Url);
        var hexUpper = id.ToString(Idf.HexUpper);
        var hexLower = id.ToString(Idf.Hex);

        Assert.That(Id32.Parse(base64), Is.EqualTo(id));
        Assert.That(Id32.Parse(hexUpper), Is.EqualTo(id));
        Assert.That(Id32.Parse(hexLower), Is.EqualTo(id));
    }
}