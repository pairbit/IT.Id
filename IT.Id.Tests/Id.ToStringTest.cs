namespace Tests;

public class IdToStringTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void ExamplesFromComments()
    {
        CheckId(Id.Min);
        CheckId(Id.Max);

        Assert.That(CheckId(Id.Parse("62a84f674031e78d474fe23f")).ToString(Idf.Hex), Is.EqualTo("62a84f674031e78d474fe23f"));

        Assert.That(CheckId(Id.Parse("62A84F674031E78D474FE23F")).ToString(Idf.HexUpper), Is.EqualTo("62A84F674031E78D474FE23F"));

        Assert.That(CheckId(Id.Parse("CAM4YST067KRTHTFW8ZG")).ToString(Idf.Base32), Is.EqualTo("CAM4YST067KRTHTFW8ZG"));

        Assert.That(CheckId(Id.Parse("2ryw1nk6d1eiGQSL6")).ToString(Idf.Base58), Is.EqualTo("2ryw1nk6d1eiGQSL6"));

        Assert.That(CheckId(Id.Parse("YqhPZ0Ax541HT+I/")).ToString(Idf.Base64), Is.EqualTo("YqhPZ0Ax541HT+I/"));

        Assert.That(CheckId(Id.Parse("YqhPZ0Ax541HT-I_")).ToString(Idf.Base64Url), Is.EqualTo("YqhPZ0Ax541HT-I_"));

        Assert.That(CheckId(Id.Parse("v{IV^PiNKcFO_~|")).ToString(Idf.Base85), Is.EqualTo("v{IV^PiNKcFO_~|"));

        //Win = \, Linux = /
        var p = Path.DirectorySeparatorChar;

        Assert.That(CheckId(Id.Parse("_/I/-TH145xA0ZPhqY")).ToString(Idf.Path2), Is.EqualTo($"_{p}I{p}-TH145xA0ZPhqY"));

        Assert.That(CheckId(Id.Parse("_/I/-/TH145xA0ZPhqY")).ToString(Idf.Path3), Is.EqualTo($"_{p}I{p}-{p}TH145xA0ZPhqY"));
    }

    [Test]
    public void NewId1000()
    {
        Id id;
        Id prev = default;

        for (int i = 0; i < 1000; i++)
        {
            id = Id.New();

            Assert.That(prev, Is.Not.EqualTo(id));

            CheckId(id);

            prev = id;
        }
    }

    private static Id CheckId(Id id)
    {
        byte[] bytes = id.ToByteArray();

        var base85 = SimpleBase.Base85.Z85.Encode(bytes).Replace('&', '_').Replace('<', '~').Replace('>', '|');

        CheckString(id, Idf.Base85, 15, 85, "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$#",
            id.ToString("B85"), $"{id:B85}", base85);

        var base64 = Convert.ToBase64String(bytes);

        CheckString(id, Idf.Base64, 16, 64, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
            id.ToString("B64"), $"{id:B64}", base64);

        var base64Url = base64.Replace('/', '_').Replace('+', '-');

        CheckString(id, Idf.Base64Url, 16, 64, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_",
            id.ToString(), id.ToString(null), $"{id}", base64Url);

        var base58var = SimpleBase.Base58.Bitcoin.Encode(bytes);
        var base58 = new string('1', 17 - base58var.Length) + base58var;

        CheckString(id, Idf.Base58, 17, 58, "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz",
            id.ToString("B58"), $"{id:B58}", base58);

        var path2 = new string(base64Url.Reverse().ToArray()).Insert(2, Path.DirectorySeparatorChar.ToString()).Insert(1, Path.DirectorySeparatorChar.ToString());

        CheckString(id, Idf.Path2, 18, 66, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\\",
            id.ToString("P2"), $"{id:P2}", path2);

        var path3 = path2.Insert(5, Path.DirectorySeparatorChar.ToString());

        CheckString(id, Idf.Path3, 19, 66, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\\",
            id.ToString(Idf.Path3), id.ToString("P3"), $"{id:P3}", path3);

        var base32 = SimpleBase.Base32.Crockford.Encode(bytes);

        CheckString(id, Idf.Base32, 20, 32, "0123456789ABCDEFGHJKMNPQRSTVWXYZ", id.ToString("B32"), $"{id:B32}", base32);

        var hexUpper = Convert.ToHexString(bytes);

        CheckString(id, Idf.HexUpper, 24, 16, "0123456789ABCDEF", id.ToString("H"), $"{id:H}", hexUpper);

        var hex = hexUpper.ToLowerInvariant();

        CheckString(id, Idf.Hex, 24, 16, "0123456789abcdef", id.ToString("h"), $"{id:h}", hex);

        return id;
    }

    private static void CheckString(Id id, Idf format, int length, int alphabetLength, ReadOnlySpan<char> alphabet, params string[] strings)
    {
        Assert.That(strings, Is.Not.Null);
        Assert.That(strings, Is.Not.Empty);
        Assert.That(alphabet.Length, Is.EqualTo(alphabetLength));
        Assert.That(length, Is.GreaterThan(0));
        Assert.That(Id.GetLength(format), Is.EqualTo(length));

        var s = id.ToString(format);

        Assert.That(s, Has.Length.EqualTo(length));

        foreach (var ch in s.ToCharArray())
        {
            Assert.That(alphabet.Contains(ch));
        }

        Assert.That(Id.Parse(s).ToString(format), Is.EqualTo(s));

        foreach (var str in strings)
        {
            Assert.That(s, Is.EqualTo(str));
        }
    }
}