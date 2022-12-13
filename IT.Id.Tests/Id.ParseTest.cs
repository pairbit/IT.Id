using System.Text;

namespace IT.Tests;

public class IdParseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void InvalidLength()
    {
        Assert.IsFalse(Id.TryGetFormat(1, out var format));
        Assert.That(format, Is.EqualTo((Idf)0));

        FormatException(() => Id.GetFormat(1),
            $"The length of Id cannot be 1. It must be 24 or between 15 and 20.");

        FormatException(() => Id.Parse(default(ReadOnlySpan<char>)), 
            $"The length of Id cannot be 0 characters. It must be 24 or between 15 and 20 characters.");

        FormatException(() => Id.Parse("1234"),
            $"The length of Id cannot be 4 characters. It must be 24 or between 15 and 20 characters.");

        FormatException(() => Id.Parse(default(ReadOnlySpan<byte>)), 
            $"The length of Id cannot be 0 bytes. It must be 24 or between 15 and 20 bytes.");

        FormatException(() => Id.Parse(new byte[] { 1, 2, 3, 4, 5 }),
            $"The length of Id cannot be 5 bytes. It must be 24 or between 15 and 20 bytes.");

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
            $"The length of Id in Base58 format cannot be 3 characters. It must be between 12 to 17 characters long.");

        FormatException(() => Id.Parse(new byte[] { 1, 2, 3, 4 }, Idf.Base58),
            $"The length of Id in Base58 format cannot be 4 bytes. It must be between 12 to 17 bytes long.");

        format = (Idf)123123;

        InvalidFormat(format.ToString(), () => Id.Parse(Array.Empty<char>(), format));
        InvalidFormat(format.ToString(), () => Id.Parse(Array.Empty<byte>(), format));
        InvalidFormat(format.ToString(), () => Id.GetLength(format));
        InvalidFormat(format.ToString(), () => Id.New().ToString(format));
        InvalidFormat(format.ToString(), () => Id.New().TryFormat(Array.Empty<char>(), out _, format));
        InvalidFormat(format.ToString(), () => Id.New().TryFormat(Array.Empty<byte>(), out _, format));
        InvalidFormat("nf", () => Id.New().ToString("nf"));
        InvalidFormat("nf", () => Id.New().TryFormat(Array.Empty<char>(), out _, "nf".AsSpan()));

        InvalidPath("_aI/-TH145xA0ZPhqY", Idf.Base64Path2, 'a', 1);
        InvalidPath("_/Ib-TH145xA0ZPhqY", Idf.Base64Path2, 'b', 3);
        InvalidPath("_cI/-/TH145xA0ZPhqY", Idf.Base64Path3, 'c', 1);
        InvalidPath("_/Id-/TH145xA0ZPhqY", Idf.Base64Path3, 'd', 3);
        InvalidPath("_/I/-eTH145xA0ZPhqY", Idf.Base64Path3, 'e', 5);
    }

    [Test]
    public void InvalidAllChar()
    {
        InvalidAllChar("62a84f674031e78d474fe23f", '/', 'G', 'g');

        InvalidAllChar("CDZ6ZZEC14FS687T52V0", '/', '_', '{');

        InvalidAllChar("2su1yC5sA8ji2ZrSo", '/', '_', '{');

        InvalidAllChar("YqhPZ0Ax541HT+I/", '*', '^', '{');

        InvalidAllChar("_/I/-TH145xA0ZPhqY", new[] { 1, 3 }, '*', '^', '{');

        InvalidAllChar("_/I/-/TH145xA0ZPhqY", new[] { 1, 3, 5 }, '*', '^', '{');

        InvalidAllChar("v{IV^PiNKcFO_~|", ' ', ',', ';', '\u007f');
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

        Valid("62a84f674031e78d474fe23f", Idf.Hex, base16);
        Valid("62A84F674031E78D474FE23F", Idf.Hex, base16);

        var base32 = "cdz6zzec14fs687t52v0";

        //Upper == Lower

        Valid("CDZ6ZZEC14FS687T52V0", Idf.Base32, base32);
        Valid("cdz6zzec14fs687t52v0", Idf.Base32, base32);

        //'o','O' -> 0
        //'i','I','l','L' -> 1
        //'u','U' -> 'V'

        Valid("CdZ6ZZECi4fs687t52Vo", Idf.Base32, base32);
        Valid("CDz6zZECI4FS687T52uO", Idf.Base32, base32);
        Valid("cDZ6ZzeCl4FS687T52Uo", Idf.Base32, base32);
        Valid("CDZ6ZZECL4FS687T52VO", Idf.Base32, base32);

        var base58 = "2su1yC5sA8ji2ZrSo";

        //'O','0' -> 'o'
        //'I','l' -> '1'
        Valid("2sulyC5sA8ji2ZrSO", Idf.Base58, base58);
        Valid("2suIyC5sA8ji2ZrS0", Idf.Base58, base58);
        Valid("2su1yC5sA8ji2ZrSo", Idf.Base58, base58);

        var base64 = "YqhPZ0Ax541HT+I/";

        Valid(base64, Idf.Base64, base64);
        Valid("YqhPZ0Ax541HT-I_", Idf.Base64, base64);
        Valid("YqhPZ0Ax541HT-I/", Idf.Base64, base64);
        Valid("YqhPZ0Ax541HT+I_", Idf.Base64, base64);

        //Win = \, Linux = /
        //var p = Path.DirectorySeparatorChar;

        var path2 = $"_/I/-TH145xA0ZPhqY";

        Valid("_\\I\\-TH145xA0ZPhqY", Idf.Base64Path2, path2);
        Valid("_/I/-TH145xA0ZPhqY", Idf.Base64Path2, path2);
        Valid("//I\\+TH145xA0ZPhqY", Idf.Base64Path2, path2);

        var path3 = $"_/I/-/TH145xA0ZPhqY";

        Valid("_\\I\\-\\TH145xA0ZPhqY", Idf.Base64Path3, path3);
        Valid("_/I/-/TH145xA0ZPhqY", Idf.Base64Path3, path3);
        Valid("/\\I/+\\TH145xA0ZPhqY", Idf.Base64Path3, path3);

        var base85 = "v{IV^PiNKcFO_~|";

        //Z85
        //'&' -> '_'
        //'<' -> '~'
        //'>' -> '|'
        Valid("v{IV^PiNKcFO&<>", Idf.Base85, base85);
        Valid("v{IV^PiNKcFO_<>", Idf.Base85, base85);
        Valid("v{IV^PiNKcFO&~>", Idf.Base85, base85);
        Valid("v{IV^PiNKcFO&<|", Idf.Base85, base85);
        Valid("v{IV^PiNKcFO_~|", Idf.Base85, base85);
    }

    private void Valid(string str, Idf format, string tostring)
    {
        var id = Id.Parse(str, format);
        var bytes = Encoding.UTF8.GetBytes(str);

        Assert.Multiple(() =>
        {
            Assert.That(id.ToString(format), Is.EqualTo(tostring));
            Assert.That(id, Is.EqualTo(Id.Parse(str)));

            Assert.That(Id.TryParse(str, format, out var id2), Is.True);
            Assert.That(id, Is.EqualTo(id2));

            Assert.That(Id.TryParse(str, out var id3), Is.True);
            Assert.That(id, Is.EqualTo(id3));

            Assert.That(id, Is.EqualTo(Id.Parse(bytes, format)));
            Assert.That(id, Is.EqualTo(Id.Parse(bytes)));

            Assert.That(Id.TryParse(bytes, format, out var id4), Is.True);
            Assert.That(id, Is.EqualTo(id4));

            Assert.That(Id.TryParse(bytes, out var id5), Is.True);
            Assert.That(id, Is.EqualTo(id5));
        });
    }

    private void InvalidAllChar(string str, params char[] codes)
    {
        foreach (var code in codes)
        {
            for (int i = 0; i < str.Length; i++)
            {
                var span = str.ToArray();
                span[i] = code;
                InvalidChar(new string(span), code);
            }
        }
    }

    private void InvalidAllChar(string str, int[] skip, params char[] codes)
    {
        foreach (var code in codes)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (skip.Contains(i)) continue;
                var span = str.ToArray();
                span[i] = code;
                InvalidChar(new string(span), code);
            }
        }
    }

    private void InvalidChar(string str, char code)
    {
        var format = Id.GetFormat(str.Length);

        Assert.IsTrue(Id.TryGetFormat(str.Length, out var format2));

        Assert.That(format, Is.EqualTo(format2));

        Assert.IsFalse(Id.TryParse(str, out var id));
        Assert.That(id, Is.EqualTo((Id)default));

        Assert.IsFalse(Id.TryParse(str, format, out id));
        Assert.That(id, Is.EqualTo((Id)default));

        var message = $"The Id in {format} format cannot contain character code {(int)code}.";

        var ex = Assert.Throws<FormatException>(() => Id.Parse(str));
        Assert.That(ex.Message, Is.EqualTo(message));

        ex = Assert.Throws<FormatException>(() => Id.Parse(str, format));
        Assert.That(ex.Message, Is.EqualTo(message));

        var codeBytes = Encoding.UTF8.GetBytes(code.ToString());

        if (codeBytes.Length == 1)
        {
            message = $"The Id in {format} format cannot contain byte {(int)code}.";

            var bytes = Encoding.UTF8.GetBytes(str);
            Assert.That(bytes, Has.Length.EqualTo(str.Length));

            Assert.IsFalse(Id.TryParse(bytes, out id));
            Assert.That(id, Is.EqualTo((Id)default));

            Assert.IsFalse(Id.TryParse(bytes, format, out id));
            Assert.That(id, Is.EqualTo((Id)default));

            ex = Assert.Throws<FormatException>(() => Id.Parse(bytes));
            Assert.That(ex.Message, Is.EqualTo(message));

            ex = Assert.Throws<FormatException>(() => Id.Parse(bytes, format));
            Assert.That(ex.Message, Is.EqualTo(message));
        }
    }

    private void InvalidLength(Idf format, string str)
    {
        var len = Id.GetLength(format);
        var f = Id.GetFormat(len);
        var bytes = Encoding.UTF8.GetBytes(str);

        FormatException(() => Id.Parse(str, format),
            $"The length of Id in {f} format cannot be {str.Length} characters. It must be {len} characters long.");

        FormatException(() => Id.Parse(bytes, format),
            $"The length of Id in {f} format cannot be {bytes.Length} bytes. It must be {len} bytes long.");
        
        Assert.Multiple(() =>
        {
            Assert.That(Id.TryParse(str, out var id), Is.False);
            Assert.That(id, Is.EqualTo((Id)default));

            Assert.That(Id.TryParse(str, format, out id), Is.False);

            Assert.That(id, Is.EqualTo((Id)default));

            Assert.That(Id.TryParse(bytes, out id), Is.False);
            Assert.That(id, Is.EqualTo((Id)default));

            Assert.That(Id.TryParse(bytes, format, out id), Is.False);
            Assert.That(id, Is.EqualTo((Id)default));
        });
    }

    private void InvalidPath(string str, Idf format, int code, int index)
    {
        Assert.IsFalse(Id.TryParse(str, out var id));
        Assert.That(id, Is.EqualTo((Id)default));

        Assert.IsFalse(Id.TryParse(str, format, out id));
        Assert.That(id, Is.EqualTo((Id)default));

        RequiredChars(() => Id.Parse(str), format.ToString(), code, index);
        RequiredChars(() => Id.Parse(str, format), format.ToString(), code, index);

        var bytes = Encoding.UTF8.GetBytes(str);

        Assert.IsFalse(Id.TryParse(bytes, out id));
        Assert.That(id, Is.EqualTo((Id)default));

        Assert.IsFalse(Id.TryParse(bytes, format, out id));
        Assert.That(id, Is.EqualTo((Id)default));

        RequiredBytes(() => Id.Parse(bytes), format.ToString(), code, index);
        RequiredBytes(() => Id.Parse(bytes, format), format.ToString(), code, index);
    }

    private void InvalidFormat<T>(string format, Func<T> func)
    {
        FormatException(() => func(), $"The Id does not contain '{format}' format.");
    }

    private void RequiredChars<T>(Func<T> func, string format, int code, int index)
    {
        FormatException(() => func(), $"The Id in {format} format cannot contain character code {code} at position {index}. It must contain one of characters '/', '\\'.");
    }

    private void RequiredBytes<T>(Func<T> func, string format, int code, int index)
    {
        FormatException(() => func(), $"The Id in {format} format cannot contain byte {code} at position {index}. It must contain one of bytes 47, 92.");
    }

    private void FormatException<T>(Func<T> func, string message)
    {
        var ex = Assert.Throws<FormatException>(() => func());

        Assert.That(ex.Message, Is.EqualTo(message));
    }
}