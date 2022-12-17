using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryParseBaseBytes
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

    public IdTryParseBaseBytes()
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
    public Id Id_TryParseHex() => Id.TryParseHex(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase32() => Id.TryParseBase32(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase58() => Id.TryParseBase58(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64() => Id.TryParseBase64(_idBase64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64Path2() => Id.TryParseBase64Path2(_idBase64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64Path3() => Id.TryParseBase64Path3(_idBase64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase85() => Id.TryParseBase85(_idBase85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Ulid Ulid_TryParse() => Ulid.TryParse(_ulidBase32, out var ulid) ? ulid : throw new FormatException();
}