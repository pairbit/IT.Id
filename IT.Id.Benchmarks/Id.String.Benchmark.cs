using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdStringBenchmark
{
    private readonly Ulid _ulid;
    internal readonly String _ulidString;

    private readonly Id _id;
    private readonly String _idHexLower;
    internal readonly String _idBase32;
    private readonly String _idBase58;
    private readonly String _idBase64Url;
    private readonly String _idBase85;
    private readonly String _idPath2;
    private readonly String _idPath3;

    public IdStringBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_Encode_HexLower();
        _idBase32 = Id_Encode_Base32();
        _idBase58 = Id_Encode_Base58();
        _idBase64Url = Id_Encode_Base64();
        _idBase85 = Id_Encode_Base85();
        _idPath2 = Id_Encode_Base64Path2();
        _idPath3 = Id_Encode_Base64Path3();

        _ulid = Ulid.NewUlid();
        _ulidString = Ulid_Encode();
    }

    [Benchmark]
    public String Id_Encode_HexLower() => _id.ToString(Idf.Hex);

    [Benchmark]
    public String Id_EncodeHexLower() => _id.ToHexLower();

    //[Benchmark]
    public Id Id_Decode_Auto_Hex() => Id.Parse(_idHexLower);

    //[Benchmark]
    public Id Id_Decode_Hex() => Id.Parse(_idHexLower, Idf.Hex);

    //[Benchmark]
    public String Id_Encode_HexUpper() => _id.ToString(Idf.HexUpper);

    //[Benchmark]
    public String Id_Encode_Base32() => _id.ToString(Idf.Base32);

    //[Benchmark]
    public String Id_Encode_Base32Upper() => _id.ToString(Idf.Base32Upper);

    //[Benchmark]
    public Id Id_Decode_Auto_Base32() => Id.Parse(_idBase32);

    //[Benchmark]
    public Id Id_Decode_Base32() => Id.Parse(_idBase32, Idf.Base32);

    //[Benchmark]
    public String Id_Encode_Base58() => _id.ToString(Idf.Base58);

#if NETCOREAPP3_1_OR_GREATER
    //[Benchmark]
    public String Id_Encode_Base58_SimpleBase() => SimpleBase.Base58.Bitcoin.Encode(_id.ToByteArray());

    //[Benchmark]
    public Id Id_Decode_Base58_SimpleBase() => new(SimpleBase.Base58.Bitcoin.Decode(_idBase58));
#endif

    //[Benchmark]
    public Id Id_Decode_Auto_Base58() => Id.Parse(_idBase58);

    //[Benchmark]
    public Id Id_Decode_Base58() => Id.Parse(_idBase58, Idf.Base58);

    //[Benchmark]
    public String Id_Encode_Base64() => _id.ToString();

    [Benchmark]
    public Id Id_Decode_Auto_Base64() => Id.Parse(_idBase64Url);

    [Benchmark]
    public Id Id_Decode_Base64() => Id.Parse(_idBase64Url, Idf.Base64);

    [Benchmark]
    public Id Id_DecodeBase64() => Id.ParseBase64(_idBase64Url.AsSpan());

    //[Benchmark]
    public String Id_Encode_Base85() => _id.ToString(Idf.Base85);

    //[Benchmark]
    public Id Id_Decode_Auto_Base85() => Id.Parse(_idBase85);

    //[Benchmark]
    public Id Id_Decode_Base85() => Id.Parse(_idBase85, Idf.Base85);

    //[Benchmark]
    public String Id_Encode_Base64Path2() => _id.ToString(Idf.Base64Path2);

    [Benchmark]
    public Id Id_Decode_Auto_Base64Path2() => Id.Parse(_idPath2);

    [Benchmark]
    public Id Id_Decode_Base64Path2() => Id.Parse(_idPath2, Idf.Base64Path2);

    [Benchmark]
    public Id Id_DecodeBase64Path2() => Id.ParseBase64Path2(_idPath2.AsSpan());

    //[Benchmark]
    public String Id_Encode_Base64Path3() => _id.ToString(Idf.Base64Path3);

    //[Benchmark]
    public Id Id_Decode_Auto_Base64Path3() => Id.Parse(_idPath3);

    //[Benchmark]
    public Id Id_Decode_Base64Path3() => Id.Parse(_idPath3, Idf.Base64Path3);

    //[Benchmark]
    public String Ulid_Encode() => _ulid.ToString();

    //[Benchmark]
    public Ulid Ulid_Decode() => Ulid.Parse(_ulidString);
}