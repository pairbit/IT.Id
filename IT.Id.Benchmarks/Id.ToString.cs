using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdToString
{
    private readonly Guid _guid;
    private readonly Ulid _ulid;
    private readonly Id _id;

    public IdToString()
    {
        _id = Id.NewObjectId();
        _ulid = Ulid.NewUlid();
        _guid = Guid.NewGuid();
    }

    [Benchmark]
    public String Id_ToString_HexLower() => _id.ToString(Idf.Hex);

    [Benchmark]
    public String Id_ToString_HexUpper() => _id.ToString(Idf.HexUpper);

    [Benchmark]
    public String Id_ToString_Base32Lower() => _id.ToString(Idf.Base32);

    [Benchmark]
    public String Id_ToString_Base32Upper() => _id.ToString(Idf.Base32Upper);

    [Benchmark]
    public String Id_ToString_Base58() => _id.ToString(Idf.Base58);

    [Benchmark]
    public String Id_ToString() => _id.ToString();

    [Benchmark]
    public String Id_ToString_Base64Url() => _id.ToString(Idf.Base64Url);

    [Benchmark]
    public String Id_ToString_Base64() => _id.ToString(Idf.Base64);

    [Benchmark]
    public String Id_ToString_Base85() => _id.ToString(Idf.Base85);

    [Benchmark]
    public String Id_ToString_Base64Path2() => _id.ToString(Idf.Base64Path2);

    [Benchmark]
    public String Id_ToString_Base64Path3() => _id.ToString(Idf.Base64Path3);

    [Benchmark]
    public String Ulid_ToBase32() => _ulid.ToString();

    [Benchmark]
    public String Guid_ToHex() => _guid.ToString();
}