using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdStringBenchmark
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

    public IdStringBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_ToString_HexLower();
        _idBase32 = Id_ToString_Base32Lower();
        _idBase58 = Id_ToString_Base58();
        _idBase64Url = Id_ToString_Base64();
        _idBase64Path2 = Id_ToString_Base64Path2();
        _idBase64Path3 = Id_ToString_Base64Path3();
        _idBase85 = Id_ToString_Base85();

        _ulid = Ulid.NewUlid();
        _ulidString = Ulid_ToString();

        _guid = Guid.NewGuid();
        _guidString = Guid_ToString();
    }

    #region ToString

    //[Benchmark]
    public String Id_ToString_HexLower() => _id.ToString(Idf.Hex);

    //[Benchmark]
    public String Id_ToHexLower() => _id.ToHex();

    //[Benchmark]
    public String Id_ToString_HexUpper() => _id.ToString(Idf.HexUpper);

    //[Benchmark]
    public String Id_ToHexUpper() => _id.ToHexUpper();

    //[Benchmark]
    public String Id_ToString_Base32Lower() => _id.ToString(Idf.Base32);

    //[Benchmark]
    public String Id_ToBase32Lower() => _id.ToBase32();

    //[Benchmark]
    public String Id_ToString_Base32Upper() => _id.ToString(Idf.Base32Upper);

    //[Benchmark]
    public String Id_ToBase32Upper() => _id.ToBase32Upper();

    //[Benchmark]
    public String Id_ToString_Base58() => _id.ToString(Idf.Base58);

    //[Benchmark]
    public String Id_ToBase58() => _id.ToBase58();

    //[Benchmark]
    public String Id_ToString() => _id.ToString();

    //[Benchmark]
    public String Id_ToString_Base64Url() => _id.ToString(Idf.Base64Url);

    //[Benchmark]
    public String Id_ToBase64Url() => _id.ToBase64Url();

    //[Benchmark]
    public String Id_ToString_Base64() => _id.ToString(Idf.Base64);

    //[Benchmark]
    public String Id_ToBase64() => _id.ToBase64();

    //[Benchmark]
    public String Id_ToString_Base85() => _id.ToString(Idf.Base85);

    //[Benchmark]
    public String Id_ToBase85() => _id.ToBase85();

    //[Benchmark]
    public String Id_ToString_Base64Path2() => _id.ToString(Idf.Base64Path2);

    //[Benchmark]
    public String Id_ToBase64Path2() => _id.ToBase64Path2();

    //[Benchmark]
    public String Id_ToString_Base64Path3() => _id.ToString(Idf.Base64Path3);

    //[Benchmark]
    public String Id_ToBase64Path3() => _id.ToBase64Path3();

    //[Benchmark]
    public String Ulid_ToString() => _ulid.ToString();

    //[Benchmark]
    public String Guid_ToString() => _guid.ToString();

    #endregion ToString

#if NETCOREAPP3_1_OR_GREATER

    //[Benchmark]
    public String Id_ToString_SimpleBase_Base58() => SimpleBase.Base58.Bitcoin.Encode(_id.ToByteArray());

    //[Benchmark]
    public Id Id_Parse_SimpleBase_Base58() => new(SimpleBase.Base58.Bitcoin.Decode(_idBase58));

#endif

    #region Parse

    [Benchmark]
    public Id Id_Parse_Hex() => Id.Parse(_idHexLower, Idf.Hex);

    [Benchmark]
    public Id Id_ParseHex() => Id.ParseHex(_idHexLower);

    [Benchmark]
    public Id Id_Parse_Base32() => Id.Parse(_idBase32, Idf.Base32);

    [Benchmark]
    public Id Id_ParseBase32() => Id.ParseBase32(_idBase32);

    [Benchmark]
    public Id Id_Parse_Base58() => Id.Parse(_idBase58, Idf.Base58);

    [Benchmark]
    public Id Id_ParseBase58() => Id.ParseBase58(_idBase58);

    [Benchmark]
    public Id Id_Parse_Base64() => Id.Parse(_idBase64Url, Idf.Base64);

    [Benchmark]
    public Id Id_ParseBase64() => Id.ParseBase64(_idBase64Url);

    [Benchmark]
    public Id Id_Parse_Base64Path2() => Id.Parse(_idBase64Path2, Idf.Base64Path2);

    [Benchmark]
    public Id Id_ParseBase64Path2() => Id.ParseBase64Path2(_idBase64Path2);

    [Benchmark]
    public Id Id_Parse_Base64Path3() => Id.Parse(_idBase64Path3, Idf.Base64Path3);

    [Benchmark]
    public Id Id_ParseBase64Path3() => Id.ParseBase64Path3(_idBase64Path3);

    [Benchmark]
    public Id Id_Parse_Base85() => Id.Parse(_idBase85, Idf.Base85);

    [Benchmark]
    public Id Id_ParseBase85() => Id.ParseBase85(_idBase85.AsSpan());

    [Benchmark]
    public Id Id_ParseBase85_1() => Id.ParseBase85_1(_idBase85.AsSpan());

    [Benchmark]
    public Id Id_ParseBase85_2() => Id.ParseBase85_2(_idBase85.AsSpan());

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidString);

    [Benchmark]
    public Guid Guid_Parse() => Guid.Parse(_guidString);

    #endregion Parse

    #region Parse By Length

    [Benchmark]
    public Id Id_Parse_ByLen_Hex() => Id.Parse(_idHexLower);

    [Benchmark]
    public Id Id_Parse_ByLen_Base32() => Id.Parse(_idBase32);

    [Benchmark]
    public Id Id_Parse_ByLen_Base58() => Id.Parse(_idBase58);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64() => Id.Parse(_idBase64Url);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64Path2() => Id.Parse(_idBase64Path2);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64Path3() => Id.Parse(_idBase64Path3);

    [Benchmark]
    public Id Id_Parse_ByLen_Base85() => Id.Parse(_idBase85);

    #endregion Parse By Length
}