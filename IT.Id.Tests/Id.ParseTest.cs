using SimpleBase;

namespace Tests;

public class IdParseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Invalid()
    {
        //Base16 -> 62a84f674031e78d474fe23f
        InvalidFormat("62a84f674031e/8d474fe23f", '/');//47
        InvalidFormat("62a84f674031e78d474fe23G", 'G');//71
        InvalidFormat("62a84f674031e78d474fe23g", 'g');//103

        //Base32
        InvalidFormat("CDZ6ZZEC14FS687T52V/", '/');//47
        InvalidFormat("CDZ6ZZEC_4FS687T52V0", '_');//95
        InvalidFormat("CDZ6{ZEC14FS687T52V0", '{');//123

        //Base58
        InvalidFormat("2su1/C5sA8ji2ZrSo", '/');//47
        InvalidFormat("2su1yC5sA8ji_ZrSo", '_');//95
        InvalidFormat("2{u1yC5sA8ji2ZrSo", '{');//123

        //Base64 -> YqhPZ0Ax541HT+I/
        InvalidFormat("Y*hPZ0Ax541HT+I/", '*');//42
        InvalidFormat("YqhPZ0Ax541HT+^/", '^');//94
        InvalidFormat("YqhPZ0Ax5{1HT+I/", '{');//123

        //Base85 -> v{IV^PiNKcFO_~|
        InvalidFormat("v{IV^PiNK FO_~|", ' ');//32
        InvalidFormat("v{I,^PiNKcFO_~|", ',');//44
        InvalidFormat("v{IV^PiNKcFO_~;", ';');//59
        InvalidFormat("v{IV^\u007fiNKcFO_~|", '\u007f');//127
    }

    [Test]
    public void Valid()
    {
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

        var base58 = "2su1yC5sA8ji2ZrSo";

        //'O','0' -> 'o'
        //'I','l' -> '1'
        Assert.That(Id.Parse("2sulyC5sA8ji2ZrSO").ToString(Idf.Base58), Is.EqualTo(base58));
        Assert.That(Id.Parse("2suIyC5sA8ji2ZrS0").ToString(Idf.Base58), Is.EqualTo(base58));
        Assert.That(Id.Parse("2su1yC5sA8ji2ZrSo").ToString(Idf.Base58), Is.EqualTo(base58));

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

    private void InvalidFormat(string str, char invalid)
    {
        var ex = Assert.Throws<FormatException>(() => Id.Parse(str));

        var format = Id.GetFormat(str.Length);

        Assert.That(ex.Message, Is.EqualTo($"Char '{invalid}' not found {format}"));
    }
}