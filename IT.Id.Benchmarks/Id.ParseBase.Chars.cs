using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdParseBaseChars
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

    public IdParseBaseChars()
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
    public Id Id_ParseHex() => Id.ParseHex(_idHexLower);

    [Benchmark]
    public Id Id_ParseBase32() => Id.ParseBase32(_idBase32);

    [Benchmark]
    public Id Id_ParseBase58() => Id.ParseBase58(_idBase58);

    [Benchmark]
    public Id Id_ParseBase64() => Id.ParseBase64(_idBase64Url);

    [Benchmark]
    public Id Id_ParseBase64Path2() => Id.ParseBase64Path2(_idBase64Path2);

    [Benchmark]
    public Id Id_ParseBase64Path3() => Id.ParseBase64Path3(_idBase64Path3);

    [Benchmark]
    public Id Id_ParseBase85() => Id.ParseBase85(_idBase85.AsSpan());

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidString);

    [Benchmark]
    public Guid Guid_Parse() => Guid.Parse(_guidString);

#if NETCOREAPP3_1_OR_GREATER

    //[Benchmark]
    public Id Id_Parse_SimpleBase_Base58() => new(SimpleBase.Base58.Bitcoin.Decode(_idBase58));

#endif
}