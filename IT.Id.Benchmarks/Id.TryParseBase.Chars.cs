using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryParseBaseChars
{
    private readonly Ulid _ulid;
    internal readonly char[] _ulidChars;

    internal readonly Id _id;
    private readonly char[] _idHexLower;
    internal readonly char[] _idBase32;
    private readonly char[] _idBase58;
    private readonly char[] _idBase64;
    private readonly char[] _idBase64Path2;
    private readonly char[] _idBase64Path3;
    private readonly char[] _idBase85;

    private readonly Guid _guid;
    internal readonly string _guidChars;

    public IdTryParseBaseChars()
    {
        _id = Id.NewObjectId();
        _idHexLower = _id.ToHex().ToArray();
        _idBase32 = _id.ToBase32().ToArray();
        _idBase58 = _id.ToBase58().ToArray();
        _idBase64 = _id.ToBase64Url().ToArray();
        _idBase64Path2 = _id.ToBase64Path2().ToArray();
        _idBase64Path3 = _id.ToBase64Path3().ToArray();
        _idBase85 = _id.ToBase85().ToArray();

        _ulid = Ulid.NewUlid();
        _ulidChars = _ulid.ToString().ToArray();

        _guid = Guid.NewGuid();
        _guidChars = _guid.ToString();
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
    public Ulid Ulid_TryParse() => Ulid.TryParse(_ulidChars, out var ulid) ? ulid : throw new FormatException();

    [Benchmark]
    public Guid Guid_TryParse() => Guid.TryParse(_guidChars, out var guid) ? guid : throw new FormatException();
}