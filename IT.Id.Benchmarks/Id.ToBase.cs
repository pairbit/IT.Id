using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdToBase
{
    private readonly Guid _guid;
    private readonly Ulid _ulid;
    private readonly Id _id;

    public IdToBase()
    {
        _id = Id.NewObjectId();
        _ulid = Ulid.NewUlid();
        _guid = Guid.NewGuid();
    }

    //[Benchmark]
    public String Id_ToHexLower() => _id.ToHex();

    //[Benchmark]
    public String Id_ToHexUpper() => _id.ToHexUpper();

    //[Benchmark]
    public String Id_ToBase32Lower() => _id.ToBase32();

    //[Benchmark]
    public String Id_ToBase32Upper() => _id.ToBase32Upper();

    //[Benchmark]
    public String Id_ToBase58() => _id.ToBase58();

    //[Benchmark]
    public String Id_ToBase64Url() => _id.ToBase64Url();

    //[Benchmark]
    public String Id_ToBase64() => _id.ToBase64();

    //[Benchmark]
    public String Id_ToBase64Path2() => _id.ToBase64Path2();

    //[Benchmark]
    public String Id_ToBase64Path3() => _id.ToBase64Path3();

    [Benchmark]
    public String Id_ToBase85() => _id.ToBase85();

    //[Benchmark]
    public String Ulid_ToBase32() => _ulid.ToString();

    //[Benchmark]
    public String Guid_ToHex() => _guid.ToString();

#if NETCOREAPP3_1_OR_GREATER

    //[Benchmark]
    public String Id_SimpleBase_ToBase58() => SimpleBase.Base58.Bitcoin.Encode(_id.ToByteArray());

#endif
}