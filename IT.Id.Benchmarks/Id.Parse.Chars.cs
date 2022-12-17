using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdParseChars
{
    private readonly Guid _guid;
    internal readonly String _guidString;

    private readonly Ulid _ulid;
    internal readonly String _ulidString;

    internal readonly Id _id;
    private readonly String _idHexLower;
    internal readonly String _idBase32;
    private readonly String _idBase58;
    private readonly String _idBase64Url;
    private readonly String _idBase64Path2;
    private readonly String _idBase64Path3;
    private readonly String _idBase85;

    public IdParseChars()
    {
        _id = Id.NewObjectId();
        _idHexLower = _id.ToHex();
        _idBase32 = _id.ToBase32();
        _idBase58 = _id.ToBase58();
        _idBase64Url = _id.ToBase64Url();
        _idBase64Path2 = _id.ToBase64Path2();
        _idBase64Path3 = _id.ToBase64Path3();
        _idBase85 = _id.ToBase85();

        _ulid = Ulid.NewUlid();
        _ulidString = _ulid.ToString();

        _guid = Guid.NewGuid();
        _guidString = _guid.ToString();
    }

    [Benchmark]
    public Id Id_Parse_Hex() => Id.Parse(_idHexLower, Idf.Hex);

    [Benchmark]
    public Id Id_Parse_Base32() => Id.Parse(_idBase32, Idf.Base32);

    [Benchmark]
    public Id Id_Parse_Base58() => Id.Parse(_idBase58, Idf.Base58);

    [Benchmark]
    public Id Id_Parse_Base64() => Id.Parse(_idBase64Url, Idf.Base64);

    [Benchmark]
    public Id Id_Parse_Base64Path2() => Id.Parse(_idBase64Path2, Idf.Base64Path2);

    [Benchmark]
    public Id Id_Parse_Base64Path3() => Id.Parse(_idBase64Path3, Idf.Base64Path3);

    [Benchmark]
    public Id Id_Parse_Base85() => Id.Parse(_idBase85, Idf.Base85);

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidString);

    [Benchmark]
    public Guid Guid_Parse() => Guid.Parse(_guidString);

#if NETCOREAPP3_1_OR_GREATER

    //[Benchmark]
    public Id Id_Parse_SimpleBase_Base58() => new(SimpleBase.Base58.Bitcoin.Decode(_idBase58));

#endif
}