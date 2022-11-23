namespace Tests;

public class IdParseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        //Win = \, Linux = /
        var p = Path.DirectorySeparatorChar;

        var path2 = $"_{p}I{p}-TH145xA0ZPhqY";

        Assert.That(Id.Parse("_\\I\\-TH145xA0ZPhqY").ToString(Idf.Path2), Is.EqualTo(path2));
        Assert.That(Id.Parse("_/I/-TH145xA0ZPhqY").ToString(Idf.Path2), Is.EqualTo(path2));
        Assert.That(Id.Parse("//I\\+TH145xA0ZPhqY").ToString(Idf.Path2), Is.EqualTo(path2));

        var path3 = $"_{p}I{p}-{p}TH145xA0ZPhqY";

        Assert.That(Id.Parse("_\\I\\-\\TH145xA0ZPhqY").ToString(Idf.Path3), Is.EqualTo(path3));
        Assert.That(Id.Parse("_/I/-/TH145xA0ZPhqY").ToString(Idf.Path3), Is.EqualTo(path3));
        Assert.That(Id.Parse("/\\I/+\\TH145xA0ZPhqY").ToString(Idf.Path3), Is.EqualTo(path3));
    }
}