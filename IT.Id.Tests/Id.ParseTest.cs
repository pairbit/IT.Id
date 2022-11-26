using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SimpleBase;
using System.Text;

namespace Tests;

public class IdParseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void InvalidLength()
    {
        InvalidLength(() => Id.GetFormat(1),
            $"The length of System.Id cannot be 1. It must be 24 or between 15 and 20.");

        InvalidLength(() => Id.Parse(default(ReadOnlySpan<char>)), 
            $"The length of System.Id cannot be 0 characters. It must be 24 or between 15 and 20 characters.");

        InvalidLength(() => Id.Parse("1234"),
            $"The length of System.Id cannot be 4 characters. It must be 24 or between 15 and 20 characters.");

        InvalidLength(() => Id.Parse(default(ReadOnlySpan<byte>)), 
            $"The length of System.Id cannot be 0 bytes. It must be 24 or between 15 and 20 bytes.");

        InvalidLength(() => Id.Parse(new byte[] { 1, 2, 3, 4, 5 }),
            $"The length of System.Id cannot be 5 bytes. It must be 24 or between 15 and 20 bytes.");

        InvalidLength(Idf.Hex, "abc憨");
        InvalidLength(Idf.HexUpper, "abc憨");
        InvalidLength(Idf.Base32, "abc憨");
        InvalidLength(Idf.Base32Upper, "abc憨");
        InvalidLength(Idf.Base64, "abc憨");
        InvalidLength(Idf.Base64Url, "abc憨");
        InvalidLength(Idf.Path2, "abc憨");
        InvalidLength(Idf.Path3, "abc憨");
        InvalidLength(Idf.Base85, "abc憨");

        InvalidLength(() => Id.Parse("abc", Idf.Base58),
            $"Invalid System.Id format. Base58 must be between 12 to 17 characters long. The actual length is 3 characters.");

        InvalidLength(() => Id.Parse(new byte[] { 1, 2, 3, 4 }, Idf.Base58),
            $"Invalid System.Id format. Base58 must be between 12 to 17 bytes long. The actual length is 4 bytes.");
    }

    [Test]
    public void Invalid()
    {
        //Base16 -> 62a84f674031e78d474fe23f
        InvalidFormat("62a84F674031e/8d474fe23f", '/');//47
        InvalidFormat("62A84f674031e78d474fe23G", 'G');//71
        InvalidFormat("g2a84f674031e78d474fe23f", 'g');//103
        InvalidFormat("62A84憨674031e78d474fe23f", '憨');//25000
        InvalidFormat("62a84f674031e78d4￼4fe23f", '￼');//65532
        InvalidFormat("6􀀀84f674031e78d474fe23f", '\udbc0');//56256

        //Base32 -> CDZ6ZZEC14FS687T52V0
        InvalidFormat("CdZ6ZZEC14FS687T52V/", '/');//47
        InvalidFormat("cDZ6ZZEC_4FS687T52V0", '_');//95
        InvalidFormat("Cdz6{ZEC14FS687T52V0", '{');//123
        InvalidFormat("cdZ6ZZEC14F憨687T52V0", '憨');//25000
        InvalidFormat("CD￼6ZZEC14FS687T52V0", '￼');//65532

        //Base58 -> 2su1yC5sA8ji2ZrSo
        InvalidFormat("2su1/C5sA8ji2ZrSo", '/');//47
        InvalidFormat("2su1yC5sA8ji_ZrSo", '_');//95
        InvalidFormat("2{u1yC5sA8ji2ZrSo", '{');//123
        InvalidFormat("2su憨yC5sA8ji2ZrSo", '憨');//25000
        InvalidFormat("2su1yC5sA8ji2ZrS￼", '￼');//65532

        //Base64 -> YqhPZ0Ax541HT+I/
        InvalidFormat("Y*hPZ0Ax541HT+I/", '*');//42
        InvalidFormat("YqhPZ0Ax541HT+^/", '^');//94
        InvalidFormat("YqhPZ0Ax5{1HT+I/", '{');//123
        InvalidFormat("YqhP憨0Ax541HT+I/", '憨');//25000
        InvalidFormat("YqhPZ0Ax541HT+I￼", '￼');//65532

        //Base85 -> v{IV^PiNKcFO_~|
        InvalidFormat("v{IV^PiNK FO_~|", ' ');//32
        InvalidFormat("v{I,^PiNKcFO_~|", ',');//44
        InvalidFormat("v{IV^PiNKcFO_~;", ';');//59
        InvalidFormat("v{IV^\u007fiNKcFO_~|", '\u007f');//127
        InvalidFormat("v{IV^PiNKcFO憨~|", '憨');//25000
        InvalidFormat("v￼IV^PiNKcFO_~|", '￼');//65532

        //Path2 -> _/I/-TH145xA0ZPhqY
        InvalidFormat("_/*/-TH145xA0ZPhqY", '*');//42
        InvalidFormat("_/I/-T^145xA0ZPhqY", '^');//94
        InvalidFormat("_/I/-TH145xA0ZP{qY", '{');//123
        InvalidFormat("_/I/-TH145憨A0ZPhqY", '憨');//25000
        InvalidFormat("￼/I/-TH145xA0ZPhqY", '￼');//65532

        //Path3 -> _/I/-/TH145xA0ZPhqY
        InvalidFormat("_/I/-/TH*45xA0ZPhqY", '*');//42
        InvalidFormat("_/I/-/TH145xA^ZPhqY", '^');//94
        InvalidFormat("_/{/-/TH145xA0ZPhqY", '{');//123
        InvalidFormat("_/I/-/TH145xA0ZPhĀY", 'Ā');//256
        InvalidFormat("_/I/-/TH145xA0憨PhqY", '憨');//25000
        InvalidFormat("_/I/￼/TH145xA0ZPhqY", '￼');//65532
    }

    [Test]
    public void Valid()
    {
        var base16 = "62a84f674031e78d474fe23f";

        //Upper == Lower

        Assert.That(Id.Parse("62a84f674031e78d474fe23f").ToString(Idf.Hex), Is.EqualTo(base16));
        Assert.That(Id.Parse("62A84F674031E78D474FE23F").ToString(Idf.Hex), Is.EqualTo(base16));

        var base32 = "cdz6zzec14fs687t52v0";

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

        var base64 = "YqhPZ0Ax541HT+I/";

        Assert.That(Id.Parse(base64).ToString(Idf.Base64), Is.EqualTo(base64));
        Assert.That(Id.Parse("YqhPZ0Ax541HT-I_").ToString(Idf.Base64), Is.EqualTo(base64));
        Assert.That(Id.Parse("YqhPZ0Ax541HT-I/").ToString(Idf.Base64), Is.EqualTo(base64));
        Assert.That(Id.Parse("YqhPZ0Ax541HT+I_").ToString(Idf.Base64), Is.EqualTo(base64));

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

        var base85 = "v{IV^PiNKcFO_~|";

        //Z85
        //'&' -> '_'
        //'<' -> '~'
        //'>' -> '|'
        Assert.That(Id.Parse("v{IV^PiNKcFO&<>").ToString(Idf.Base85), Is.EqualTo(base85));
        Assert.That(Id.Parse("v{IV^PiNKcFO_<>").ToString(Idf.Base85), Is.EqualTo(base85));
        Assert.That(Id.Parse("v{IV^PiNKcFO&~>").ToString(Idf.Base85), Is.EqualTo(base85));
        Assert.That(Id.Parse("v{IV^PiNKcFO&<|").ToString(Idf.Base85), Is.EqualTo(base85));
        Assert.That(Id.Parse("v{IV^PiNKcFO_~|").ToString(Idf.Base85), Is.EqualTo(base85));
    }

    private void InvalidFormat(string str, char code)
    {
        var format = Id.GetFormat(str.Length);

        var message = $"Invalid System.Id format. {format} does not contain a character with code {(int)code}.";

        var ex = Assert.Throws<FormatException>(() => Id.Parse(str));
        Assert.That(ex.Message, Is.EqualTo(message));

        ex = Assert.Throws<FormatException>(() => Id.Parse(str, format));
        Assert.That(ex.Message, Is.EqualTo(message));

        var codeBytes = Encoding.UTF8.GetBytes(code.ToString());

        if (codeBytes.Length == 1)
        {
            var bytes = Encoding.UTF8.GetBytes(str);
            Assert.That(bytes, Has.Length.EqualTo(str.Length));

            ex = Assert.Throws<FormatException>(() => Id.Parse(bytes));
            Assert.That(ex.Message, Is.EqualTo(message));

            ex = Assert.Throws<FormatException>(() => Id.Parse(bytes, format));
            Assert.That(ex.Message, Is.EqualTo(message));
        }
    }

    private void InvalidLength(Idf format, string str)
    {
        InvalidLength(() => Id.Parse(str, format), 
            $"Invalid System.Id format. {format} must be {Id.GetLength(format)} characters long. The actual length is {str.Length} characters.");

        var bytes = Encoding.UTF8.GetBytes(str);

        InvalidLength(() => Id.Parse(bytes, format),
            $"Invalid System.Id format. {format} must be {Id.GetLength(format)} bytes long. The actual length is {bytes.Length} bytes.");
    }

    private void InvalidLength<T>(Func<T> parse, string message)
    {
        var ex = Assert.Throws<FormatException>(() => parse());
        Assert.That(ex.Message, Is.EqualTo(message));
    }
}