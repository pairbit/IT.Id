using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Id.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryCharsBenchmark
{
    private readonly Ulid _ulid;
    internal readonly char[] _ulidChars;

    private readonly System.Id _id;
    private readonly char[] _idHexLower;
    private readonly char[] _idHexUpper;
    internal readonly char[] _idBase32;
    private readonly char[] _idBase58;
    private readonly char[] _idBase64;
    private readonly char[] _idBase85;
    private readonly char[] _idPath2;
    private readonly char[] _idPath3;

    public IdTryCharsBenchmark()
    {
        //_id = System.Id.Parse("Y14-iRgzgKZclXbw");
        _id = System.Id.NewObjectId();
        _idHexUpper = Id_Encode_HexUpper();
        _idHexLower = Id_Encode_HexLower();
        _idBase32 = Id_Encode_Base32();
        _idBase58 = Id_Encode_Base58();
        _idBase64 = Id_Encode_Base64();
        _idBase85 = Id_Encode_Base85();
        _idPath2 = Id_Encode_Base64Path2();
        _idPath3 = Id_Encode_Base64Path3();

        _ulid = Ulid.NewUlid();
        _ulidChars = Ulid_Encode();
    }

    [Benchmark]
    public char[] Id_Encode_HexLower()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.Hex);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Hex() => System.Id.TryParse(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_HexUpper()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.HexUpper);
        return chars;
    }

    [Benchmark]
    public char[] Id_Encode_Base32()
    {
        var chars = new char[20];
        _id.TryFormat(chars, out _, Idf.Base32);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base32() => System.Id.TryParse(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_Base58()
    {
        var chars = new char[17];
        _id.TryFormat(chars, out _, Idf.Base58);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base58() => System.Id.TryParse(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_Base64()
    {
        var chars = new char[16];
        _id.TryFormat(chars, out _, Idf.Base64Url);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64() => System.Id.TryParse(_idBase64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_Base85()
    {
        var chars = new char[15];
        _id.TryFormat(chars, out _, Idf.Base85);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base85() => System.Id.TryParse(_idBase85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_Base64Path2()
    {
        var chars = new char[18];
        _id.TryFormat(chars, out _, Idf.Base64Path2);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64Path2() => System.Id.TryParse(_idPath2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Id_Encode_Base64Path3()
    {
        var chars = new char[19];
        _id.TryFormat(chars, out _, Idf.Base64Path3);
        return chars;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64Path3() => System.Id.TryParse(_idPath3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public char[] Ulid_Encode()
    {
        var chars = new char[26];
        _ulid.TryWriteStringify(chars);
        return chars;
    }

    [Benchmark]
    public Ulid Ulid_Decode() => Ulid.TryParse(_ulidChars, out var ulid) ? ulid : throw new FormatException();
}