using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdParseBytes
{
    private readonly Ulid _ulid;
    internal readonly byte[] _ulidBase32;

    internal readonly Id _id;
    private readonly byte[] _idHexLower;
    internal readonly byte[] _idBase32;
    private readonly byte[] _idBase58;
    private readonly byte[] _idBase64;
    private readonly byte[] _idBase64Path2;
    private readonly byte[] _idBase64Path3;
    private readonly byte[] _idBase85;

    public IdParseBytes()
    {
        _id = Id.NewObjectId();
        _idHexLower = _id.ToHex().ToArray().Select(x => (byte)x).ToArray();
        _idBase32 = _id.ToBase32().ToArray().Select(x => (byte)x).ToArray();
        _idBase58 = _id.ToBase58().ToArray().Select(x => (byte)x).ToArray();
        _idBase64 = _id.ToBase64Url().ToArray().Select(x => (byte)x).ToArray();
        _idBase64Path2 = _id.ToBase64Path2().ToArray().Select(x => (byte)x).ToArray();
        _idBase64Path3 = _id.ToBase64Path3().ToArray().Select(x => (byte)x).ToArray();
        _idBase85 = _id.ToBase85().ToArray().Select(x => (byte)x).ToArray();

        _ulid = Ulid.NewUlid();
        _ulidBase32 = _ulid.ToString().ToArray().Select(x => (byte)x).ToArray();
    }

    [Benchmark]
    public Id Id_Parse_Hex() => Id.Parse(_idHexLower, Idf.Hex);

    [Benchmark]
    public Id Id_Parse_Base32() => Id.Parse(_idBase32, Idf.Base32);

    [Benchmark]
    public Id Id_Parse_Base58() => Id.Parse(_idBase58, Idf.Base58);

    [Benchmark]
    public Id Id_Parse_Base64() => Id.Parse(_idBase64, Idf.Base64);

    [Benchmark]
    public Id Id_Parse_Base64Path2() => Id.Parse(_idBase64Path2, Idf.Base64Path2);

    [Benchmark]
    public Id Id_Parse_Base64Path3() => Id.Parse(_idBase64Path3, Idf.Base64Path3);

    [Benchmark]
    public Id Id_Parse_Base85() => Id.Parse(_idBase85, Idf.Base85);

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidBase32);
}