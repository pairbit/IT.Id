using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdParseBaseBytes
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

    public IdParseBaseBytes()
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
    public Id Id_ParseHex() => Id.ParseHex(_idHexLower);

    [Benchmark]
    public Id Id_ParseBase32() => Id.ParseBase32(_idBase32);

    [Benchmark]
    public Id Id_ParseBase58() => Id.ParseBase58(_idBase58);

    [Benchmark]
    public Id Id_ParseBase64() => Id.ParseBase64(_idBase64);

    [Benchmark]
    public Id Id_ParseBase64Path2() => Id.ParseBase64Path2(_idBase64Path2);

    [Benchmark]
    public Id Id_ParseBase64Path3() => Id.ParseBase64Path3(_idBase64Path3);

    [Benchmark]
    public Id Id_ParseBase85() => Id.ParseBase85(_idBase85);

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidBase32);
}