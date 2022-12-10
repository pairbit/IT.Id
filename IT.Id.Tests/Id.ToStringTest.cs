using IT.Json.Converters;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace IT.Tests;

public class IdToStringTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TestLazyHex()
    {
        //Console.WriteLine($"HasUpper16 : {Internal.Hex.HasUpper16}");
        //Console.WriteLine($"HasUpper32 : {Internal.Hex.HasUpper32}");
        //Console.WriteLine($"HasLower16 : {Internal.Hex.HasLower16}");
        //Console.WriteLine($"HasLower32 : {Internal.Hex.HasLower32}");
        //Console.WriteLine("---------");

        var id = Id.Parse("62a84f674031e78d474fe23f");

        //Assert.That(id.ToString(Idf.Hex), Is.EqualTo("62a84f674031e78d474fe23f"));

        //Assert.That(id.ToString(Idf.HexUpper), Is.EqualTo("62A84F674031E78D474FE23F"));

        var serializerOptions = new JsonSerializerOptions();
        serializerOptions.Converters.Add(new JsonIdConverter { Format = Idf.Hex });
        serializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        JsonSerializer.Serialize(id, serializerOptions);

        //Console.WriteLine($"HasUpper16 : {Internal.Hex.HasUpper16}");
        //Console.WriteLine($"HasUpper32 : {Internal.Hex.HasUpper32}");
        //Console.WriteLine($"HasLower16 : {Internal.Hex.HasLower16}");
        //Console.WriteLine($"HasLower32 : {Internal.Hex.HasLower32}");
    }

    [Test]
    public void ExamplesFromComments()
    {
        CheckId(Id.Min);
        CheckId(Id.Max);

        Assert.Multiple(() =>
        {
            Assert.That(CheckId(Id.Parse("62a84f674031e78d474fe23f")).ToString(Idf.Hex), Is.EqualTo("62a84f674031e78d474fe23f"));

            Assert.That(CheckId(Id.Parse("62A84F674031E78D474FE23F")).ToString(Idf.HexUpper), Is.EqualTo("62A84F674031E78D474FE23F"));

            Assert.That(CheckId(Id.Parse("ce0ytmyc14fgvd7358b0")).ToString(Idf.Base32), Is.EqualTo("ce0ytmyc14fgvd7358b0"));

            Assert.That(CheckId(Id.Parse("CE0YTMYC14FGVD7358B0")).ToString(Idf.Base32Upper), Is.EqualTo("CE0YTMYC14FGVD7358B0"));

            Assert.That(CheckId(Id.Parse("2ryw1nk6d1eiGQSL6")).ToString(Idf.Base58), Is.EqualTo("2ryw1nk6d1eiGQSL6"));

            Assert.That(CheckId(Id.Parse("YqhPZ0Ax541HT+I/")).ToString(Idf.Base64), Is.EqualTo("YqhPZ0Ax541HT+I/"));

            Assert.That(CheckId(Id.Parse("YqhPZ0Ax541HT-I_")).ToString(Idf.Base64Url), Is.EqualTo("YqhPZ0Ax541HT-I_"));

            Assert.That(CheckId(Id.Parse("v{IV^PiNKcFO_~|")).ToString(Idf.Base85), Is.EqualTo("v{IV^PiNKcFO_~|"));

            //Win = \, Linux = /
            //var p = Path.DirectorySeparatorChar;

            Assert.That(CheckId(Id.Parse("_/I/-TH145xA0ZPhqY")).ToString(Idf.Base64Path2), Is.EqualTo($"_/I/-TH145xA0ZPhqY"));

            Assert.That(CheckId(Id.Parse("_/I/-/TH145xA0ZPhqY")).ToString(Idf.Base64Path3), Is.EqualTo($"_/I/-/TH145xA0ZPhqY"));
        });
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
            id.ToString("|"), $"{id:|}", base85);

        var base64 = Convert.ToBase64String(bytes);

        CheckString(id, Idf.Base64, 16, 64, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/",
            id.ToString("/"), $"{id:/}", base64);

        var base64Url = base64.Replace('/', '_').Replace('+', '-');

        CheckString(id, Idf.Base64Url, 16, 64, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_",
            id.ToString(), id.ToString(null), id.ToString("_"), $"{id}", $"{id:_}", base64Url);

        var base58var = SimpleBase.Base58.Bitcoin.Encode(bytes);
        var base58 = new string('1', 17 - base58var.Length) + base58var;

        CheckString(id, Idf.Base58, 17, 58, "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz",
            id.ToString("i"), $"{id:i}", base58);

        var path2 = new string(base64Url.Reverse().ToArray()).Insert(2, "/").Insert(1, "/");

        CheckString(id, Idf.Base64Path2, 18, 66, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\\",
            id.ToString("//"), $"{id://}", path2);

        var path3 = path2.Insert(5, "/");

        CheckString(id, Idf.Base64Path3, 19, 66, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_/\\",
            id.ToString(Idf.Base64Path3), id.ToString("///"), $"{id:///}", path3);

        var base32Upper = SimpleBase.Base32.Crockford.Encode(bytes);

        CheckString(id, Idf.Base32Upper, 20, 32, "0123456789ABCDEFGHJKMNPQRSTVWXYZ", id.ToString("V"), $"{id:V}", base32Upper);

        var base32 = base32Upper.ToLowerInvariant();

        CheckString(id, Idf.Base32, 20, 32, "0123456789abcdefghjkmnpqrstvwxyz", id.ToString("v"), $"{id:v}", base32);

#if NET6_0_OR_GREATER
        var hexUpper = Convert.ToHexString(bytes);
#else
        var hexUpper = SimpleBase.Base16.UpperCase.Encode(bytes);
#endif

        CheckString(id, Idf.HexUpper, 24, 16, "0123456789ABCDEF", id.ToString("H"), $"{id:H}", hexUpper);

        var hex = hexUpper.ToLowerInvariant();

        CheckString(id, Idf.Hex, 24, 16, "0123456789abcdef", id.ToString("h"), $"{id:h}", hex);

        return id;
    }

    private static void CheckString(Id id, Idf format, int length, int alphabetLength, ReadOnlySpan<char> alphabet, params string[] strings)
    {
        Assert.That(alphabet.Length, Is.EqualTo(alphabetLength));
        
        Assert.Multiple(() =>
        {
            Assert.That(strings, Is.Not.Null);
            Assert.That(strings, Is.Not.Empty);
            Assert.That(length, Is.GreaterThan(0));
            Assert.That(Id.GetLength(format), Is.EqualTo(length));
        });

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

        var formatString = Id.GetFormatString(format);

        //TryFormat Chars

        Span<char> chars = stackalloc char[length];
        var isDone = id.TryFormat(chars, out var written, formatString);
        Assert.That(chars.SequenceEqual(s));
        Assert.Multiple(() =>
        {
            Assert.That(isDone, Is.EqualTo(true));
            Assert.That(written, Is.EqualTo(length));
        });

        //TryFormat Chars

        Span<char> chars2 = stackalloc char[length];
        var status = id.TryFormat(chars2, out written, format);
        Assert.That(chars2.SequenceEqual(s));
        Assert.Multiple(() =>
        {
            Assert.That(status, Is.EqualTo(OperationStatus.Done));
            Assert.That(written, Is.EqualTo(length));
        });

        //TryFormat Bytes

        var b = Encoding.UTF8.GetBytes(s);

        Span<byte> bytes = stackalloc byte[length];
        status = id.TryFormat(bytes, out written, format);
        Assert.That(bytes.SequenceEqual(b), Is.True);
        Assert.Multiple(() =>
        {
            Assert.That(status, Is.EqualTo(OperationStatus.Done));
            Assert.That(written, Is.EqualTo(length));
        });

        //DestinationTooSmall -> true

        isDone = id.TryFormat(stackalloc char[length - 1], out written, formatString);
        Assert.Multiple(() =>
        {
            Assert.That(isDone, Is.EqualTo(false));
            Assert.That(written, Is.EqualTo(0));
        });
        status = id.TryFormat(stackalloc char[length - 1], out written, format);
        Assert.Multiple(() =>
        {
            Assert.That(status, Is.EqualTo(OperationStatus.DestinationTooSmall));
            Assert.That(written, Is.EqualTo(0));
        });
        status = id.TryFormat(stackalloc byte[length - 1], out written, format);
        Assert.Multiple(() =>
        {
            Assert.That(status, Is.EqualTo(OperationStatus.DestinationTooSmall));
            Assert.That(written, Is.EqualTo(0));
        });

        //Json
        var serializerOptions = new JsonSerializerOptions();
        serializerOptions.Converters.Add(new JsonIdConverter { Format = format });
        serializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

        var idJson = JsonSerializer.Serialize(id, serializerOptions);
        Assert.That(idJson, Is.EqualTo("\"" + s + "\""));

        var idfromJson = JsonSerializer.Deserialize<Id>(idJson);
        Assert.That(idfromJson, Is.EqualTo(id));
    }
}