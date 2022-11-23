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
        //Assert.That(Id.Parse("CAM4YST067KRTHTFW8ZG").ToString(Idf.Base32), Is.EqualTo("CAM4YST067KRTHTFW8ZG"));

        var base32 = "CDZ6ZZEC14FS687T52V0";

        //Upper == Lower

        Assert.That(Id.Parse("CDZ6ZZEC14FS687T52V0").ToString(Idf.Base32), Is.EqualTo(base32));
        Assert.That(Id.Parse("cdz6zzec14fs687t52v0").ToString(Idf.Base32), Is.EqualTo(base32));

        //'o','O' -> 0
        //'i','I','l','L' -> 1
        //'u','U' -> 'V'

        Assert.That(Id.Parse("CdZ6ZZECi4fs687t52Vo").ToString(Idf.Base32), Is.EqualTo(base32));
        Assert.That(Id.Parse("CDz6zZECI4FS687T52uO").ToString(Idf.Base32), Is.EqualTo(base32));
        Assert.That(Id.Parse("cDZ6ZzeCl4FS687T52Uo").ToString(Idf.Base32), Is.EqualTo(base32));
        Assert.That(Id.Parse("CDZ6ZZECL4FS687T52VO").ToString(Idf.Base32), Is.EqualTo(base32));

        //2su1yV4i8igdQn7io
        //2su1zmdTirSKxnURo
        Assert.That(Id.Parse("2su1yC5sA8ji2ZrSo").ToString(Idf.Base58), Is.EqualTo("2su1yC5sA8ji2ZrSo"));
        Assert.That(Id.Parse("2su1yC5sA8ji2ZrSo").ToString(Idf.Base58), Is.EqualTo("2su1yC5sA8ji2ZrSo"));
        Assert.That(Id.Parse("2su1yC5sA8ji2ZrSo").ToString(Idf.Base58), Is.EqualTo("2su1yC5sA8ji2ZrSo"));

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