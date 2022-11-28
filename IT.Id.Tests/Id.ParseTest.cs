using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using SimpleBase;
using System;
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
        FormatException(() => Id.GetFormat(1),
            $"The length of System.Id cannot be 1. It must be 24 or between 15 and 20.");

        FormatException(() => Id.Parse(default(ReadOnlySpan<char>)), 
            $"The length of System.Id cannot be 0 characters. It must be 24 or between 15 and 20 characters.");

        FormatException(() => Id.Parse("1234"),
            $"The length of System.Id cannot be 4 characters. It must be 24 or between 15 and 20 characters.");

        FormatException(() => Id.Parse(default(ReadOnlySpan<byte>)), 
            $"The length of System.Id cannot be 0 bytes. It must be 24 or between 15 and 20 bytes.");

        FormatException(() => Id.Parse(new byte[] { 1, 2, 3, 4, 5 }),
            $"The length of System.Id cannot be 5 bytes. It must be 24 or between 15 and 20 bytes.");

        InvalidLength(Idf.Hex, "abc憨");
        InvalidLength(Idf.HexUpper, "abc憨");
        InvalidLength(Idf.Base32, "abc憨");
        InvalidLength(Idf.Base32Upper, "abc憨");
        InvalidLength(Idf.Base64, "abc憨");
        InvalidLength(Idf.Base64Url, "abc憨");
        InvalidLength(Idf.Base64Path2, "abc憨");
        InvalidLength(Idf.Base64Path3, "abc憨");
        InvalidLength(Idf.Base85, "abc憨");

        FormatException(() => Id.Parse("abc", Idf.Base58),
            $"The length of System.Id in Base58 format cannot be 3 characters. It must be between 12 to 17 characters long.");

        FormatException(() => Id.Parse(new byte[] { 1, 2, 3, 4 }, Idf.Base58),
            $"The length of System.Id in Base58 format cannot be 4 bytes. It must be between 12 to 17 bytes long.");

        var format = (Idf)123123;

        InvalidFormat(format.ToString(), () => Id.Parse(Array.Empty<char>(), format));
        InvalidFormat(format.ToString(), () => Id.Parse(Array.Empty<byte>(), format));
        InvalidFormat(format.ToString(), () => Id.GetLength(format));
        InvalidFormat(format.ToString(), () => Id.New().ToString(format));
        InvalidFormat(format.ToString(), () => Id.New().TryFormat(Array.Empty<char>(), out _, format));
        InvalidFormat(format.ToString(), () => Id.New().TryFormat(Array.Empty<byte>(), out _, format));
        InvalidFormat("nf", () => Id.New().ToString("nf"));
        InvalidFormat("nf", () => Id.New().TryFormat(Array.Empty<char>(), out _, "nf"));

        RequiredChars(() => Id.Parse("_aI/-TH145xA0ZPhqY", Idf.Base64Path2), Idf.Base64Path2.ToString(), 'a', 1);
        RequiredChars(() => Id.Parse("_/Ib-TH145xA0ZPhqY", Idf.Base64Path2), Idf.Base64Path2.ToString(), 'b', 3);

        RequiredBytes(() => Id.Parse(Encoding.UTF8.GetBytes("_aI/-TH145xA0ZPhqY"), Idf.Base64Path2), Idf.Base64Path2.ToString(), 'a', 1);
        RequiredBytes(() => Id.Parse(Encoding.UTF8.GetBytes("_/Ib-TH145xA0ZPhqY"), Idf.Base64Path2), Idf.Base64Path2.ToString(), 'b', 3);

        RequiredChars(() => Id.Parse("_cI/-/TH145xA0ZPhqY", Idf.Base64Path3), Idf.Base64Path3.ToString(), 'c', 1);
        RequiredChars(() => Id.Parse("_/Id-/TH145xA0ZPhqY", Idf.Base64Path3), Idf.Base64Path3.ToString(), 'd', 3);
        RequiredChars(() => Id.Parse("_/I/-eTH145xA0ZPhqY", Idf.Base64Path3), Idf.Base64Path3.ToString(), 'e', 5);

        RequiredBytes(() => Id.Parse(Encoding.UTF8.GetBytes("_cI/-/TH145xA0ZPhqY"), Idf.Base64Path3), Idf.Base64Path3.ToString(), 'c', 1);
        RequiredBytes(() => Id.Parse(Encoding.UTF8.GetBytes("_/Id-/TH145xA0ZPhqY"), Idf.Base64Path3), Idf.Base64Path3.ToString(), 'd', 3);
        RequiredBytes(() => Id.Parse(Encoding.UTF8.GetBytes("_/I/-eTH145xA0ZPhqY"), Idf.Base64Path3), Idf.Base64Path3.ToString(), 'e', 5);

    }

    [Test]
    public void InvalidChar()
    {
        //Base16 -> 62a84f674031e78d474fe23f
        InvalidChar("62a84F674031e/8d474fe23f", '/');//47
        InvalidChar("62A84f674031e78d474fe23G", 'G');//71
        InvalidChar("g2a84f674031e78d474fe23f", 'g');//103
        InvalidChar("62A84憨674031e78d474fe23f", '憨');//25000
        InvalidChar("62a84f674031e78d4￼4fe23f", '￼');//65532
        InvalidChar("6􀀀84f674031e78d474fe23f", '\udbc0');//56256

        //Base32 -> CDZ6ZZEC14FS687T52V0
        InvalidChar("CdZ6ZZEC14FS687T52V/", '/');//47
        InvalidChar("cDZ6ZZEC_4FS687T52V0", '_');//95
        InvalidChar("Cdz6{ZEC14FS687T52V0", '{');//123
        InvalidChar("cdZ6ZZEC14F憨687T52V0", '憨');//25000
        InvalidChar("CD￼6ZZEC14FS687T52V0", '￼');//65532

        //Base58 -> 2su1yC5sA8ji2ZrSo
        InvalidChar("2su1/C5sA8ji2ZrSo", '/');//47
        InvalidChar("2su1yC5sA8ji_ZrSo", '_');//95
        InvalidChar("2{u1yC5sA8ji2ZrSo", '{');//123
        InvalidChar("2su憨yC5sA8ji2ZrSo", '憨');//25000
        InvalidChar("2su1yC5sA8ji2ZrS￼", '￼');//65532

        //Base64 -> YqhPZ0Ax541HT+I/
        InvalidChar("Y*hPZ0Ax541HT+I/", '*');//42
        InvalidChar("YqhPZ0Ax541HT+^/", '^');//94
        InvalidChar("YqhPZ0Ax5{1HT+I/", '{');//123
        InvalidChar("YqhP憨0Ax541HT+I/", '憨');//25000
        InvalidChar("YqhPZ0Ax541HT+I￼", '￼');//65532

        //Base64 Path2 -> _/I/-TH145xA0ZPhqY
        InvalidChar("_/*/-TH145xA0ZPhqY", '*');//42
        InvalidChar("_/I/-T^145xA0ZPhqY", '^');//94
        InvalidChar("_/I/-TH145xA0ZP{qY", '{');//123
        InvalidChar("_/I/-TH145憨A0ZPhqY", '憨');//25000
        InvalidChar("￼/I/-TH145xA0ZPhqY", '￼');//65532

        //Base64 Path3 -> _/I/-/TH145xA0ZPhqY
        InvalidChar("_/I/-/TH*45xA0ZPhqY", '*');//42
        InvalidChar("_/I/-/TH145xA^ZPhqY", '^');//94
        InvalidChar("_/{/-/TH145xA0ZPhqY", '{');//123
        InvalidChar("_/I/-/TH145xA0ZPhĀY", 'Ā');//256
        InvalidChar("_/I/-/TH145xA0憨PhqY", '憨');//25000
        InvalidChar("_/I/￼/TH145xA0ZPhqY", '￼');//65532

        //Base85 -> v{IV^PiNKcFO_~|
        InvalidChar("v{IV^PiNK FO_~|", ' ');//32
        InvalidChar("v{I,^PiNKcFO_~|", ',');//44
        InvalidChar("v{IV^PiNKcFO_~;", ';');//59
        InvalidChar("v{IV^\u007fiNKcFO_~|", '\u007f');//127
        InvalidChar("v{IV^PiNKcFO憨~|", '憨');//25000
        InvalidChar("v￼IV^PiNKcFO_~|", '￼');//65532
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

        Assert.That(Id.Parse("_\\I\\-TH145xA0ZPhqY").ToString(Idf.Base64Path2), Is.EqualTo(path2));
        Assert.That(Id.Parse("_/I/-TH145xA0ZPhqY").ToString(Idf.Base64Path2), Is.EqualTo(path2));
        Assert.That(Id.Parse("//I\\+TH145xA0ZPhqY").ToString(Idf.Base64Path2), Is.EqualTo(path2));

        var path3 = $"_{p}I{p}-{p}TH145xA0ZPhqY";

        Assert.That(Id.Parse("_\\I\\-\\TH145xA0ZPhqY").ToString(Idf.Base64Path3), Is.EqualTo(path3));
        Assert.That(Id.Parse("_/I/-/TH145xA0ZPhqY").ToString(Idf.Base64Path3), Is.EqualTo(path3));
        Assert.That(Id.Parse("/\\I/+\\TH145xA0ZPhqY").ToString(Idf.Base64Path3), Is.EqualTo(path3));

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

    private void InvalidChar(string str, char code)
    {
        var format = Id.GetFormat(str.Length);

        var message = $"The System.Id in {format} format cannot contain character code {(int)code}.";

        var ex = Assert.Throws<FormatException>(() => Id.Parse(str));
        Assert.That(ex.Message, Is.EqualTo(message));

        ex = Assert.Throws<FormatException>(() => Id.Parse(str, format));
        Assert.That(ex.Message, Is.EqualTo(message));

        var codeBytes = Encoding.UTF8.GetBytes(code.ToString());

        if (codeBytes.Length == 1)
        {
            message = $"The System.Id in {format} format cannot contain byte {(int)code}.";

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
        FormatException(() => Id.Parse(str, format),
            $"The length of System.Id in {format} format cannot be {str.Length} characters. It must be {Id.GetLength(format)} characters long.");

        var bytes = Encoding.UTF8.GetBytes(str);

        FormatException(() => Id.Parse(bytes, format),
            $"The length of System.Id in {format} format cannot be {bytes.Length} bytes. It must be {Id.GetLength(format)} bytes long.");
    }

    private void InvalidFormat<T>(string format, Func<T> func)
    {
        FormatException(() => func(), $"The System.Id does not contain '{format}' format.");
    }

    private void RequiredChars<T>(Func<T> func, string format, int code, int index)
    {
        FormatException(() => func(), $"The System.Id in {format} format cannot contain character code {code} at position {index}. It must contain one of characters '/', '\\'.");
    }

    private void RequiredBytes<T>(Func<T> func, string format, int code, int index)
    {
        FormatException(() => func(), $"The System.Id in {format} format cannot contain byte {code} at position {index}. It must contain one of bytes 47, 92.");
    }

    private void FormatException<T>(Func<T> func, string message)
    {
        var ex = Assert.Throws<FormatException>(() => func());

        Assert.That(ex.Message, Is.EqualTo(message));
    }
}