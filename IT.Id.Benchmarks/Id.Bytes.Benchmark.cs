using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Id.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdBytesBenchmark
{
    private readonly Ulid _ulid;
    internal readonly byte[] _ulidBase32;

    private readonly System.Id _id;
    private readonly byte[] _idHexLower;
    private readonly byte[] _idHexUpper;
    internal readonly byte[] _idBase32;
    private readonly byte[] _idBase58;
    private readonly byte[] _idBase64Url;
    private readonly byte[] _idBase85;
    private readonly byte[] _idPath2;
    private readonly byte[] _idPath3;

    public IdBytesBenchmark()
    {
        //_id = System.Id.Parse("Y14-iRgzgKZclXbw");
        _id = System.Id.NewObjectId();
        _idHexUpper = Id_Encode_HexUpper();
        _idHexLower = Id_Encode_HexLower();
        _idBase32 = Id_Encode_Base32();
        _idBase58 = Id_Encode_Base58();
        _idBase64Url = Id_Encode_Base64Url();
        _idBase85 = Id_Encode_Base85();
        _idPath2 = Id_Encode_Base64Path2();
        _idPath3 = Id_Encode_Base64Path3();

        _ulid = Ulid.NewUlid();
        _ulidBase32 = Ulid_Encode();
    }

    [Benchmark]
    public byte[] Id_Encode_HexLower()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.Hex);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Hex() => System.Id.Parse(_idHexLower);

    [Benchmark]
    public byte[] Id_Encode_HexUpper()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.HexUpper);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_Encode_Base32()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base32() => System.Id.Parse(_idBase32);

    [Benchmark]
    public byte[] Id_Encode_Base58()
    {
        var bytes = new byte[17];
        _id.TryFormat(bytes, out _, Idf.Base58);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base58() => System.Id.Parse(_idBase58);

    [Benchmark]
    public byte[] Id_Encode_Base64Url()
    {
        var bytes = new byte[16];
        _id.TryFormat(bytes, out _, Idf.Base64Url);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64() => System.Id.Parse(_idBase64Url);

    [Benchmark]
    public byte[] Id_Encode_Base85()
    {
        var bytes = new byte[15];
        _id.TryFormat(bytes, out _, Idf.Base85);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base85() => System.Id.Parse(_idBase85);

    [Benchmark]
    public byte[] Id_Encode_Base64Path2()
    {
        var bytes = new byte[18];
        _id.TryFormat(bytes, out _, Idf.Base64Path2);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64Path2() => System.Id.Parse(_idPath2);

    [Benchmark]
    public byte[] Id_Encode_Base64Path3()
    {
        var bytes = new byte[19];
        _id.TryFormat(bytes, out _, Idf.Base64Path3);
        return bytes;
    }

    [Benchmark]
    public System.Id Id_Decode_Base64Path3() => System.Id.Parse(_idPath3);

    [Benchmark]
    public byte[] Ulid_Encode()
    {
        var bytes = new byte[26];
        _ulid.TryWriteStringify(bytes);
        return bytes;
    }

    [Benchmark]
    public Ulid Ulid_Decode() => Ulid.Parse(_ulidBase32);
}