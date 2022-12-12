using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryFormatBytesBenchmark
{
    private readonly Ulid _ulid;
    internal readonly byte[] _ulidBase32;

    internal readonly Id _id;
    private readonly byte[] _idHexLower;
    internal readonly byte[] _idBase32;
    private readonly byte[] _idBase58;
    private readonly byte[] _idBase64Url;
    private readonly byte[] _idBase64Path2;
    private readonly byte[] _idBase64Path3;
    private readonly byte[] _idBase85;

    public IdTryFormatBytesBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_TryFormatBytes_HexLower();
        _idBase32 = Id_TryFormatBytes_Base32();
        _idBase58 = Id_TryFormatBytes_Base58();
        _idBase64Url = Id_TryFormatBytes_Base64();
        _idBase64Path2 = Id_TryFormatBytes_Base64Path2();
        _idBase64Path3 = Id_TryFormatBytes_Base64Path3();
        _idBase85 = Id_TryFormatBytes_Base85();

        _ulid = Ulid.NewUlid();
        _ulidBase32 = Ulid_TryFormatBytes();
    }

    #region TryFormatBytes

    [Benchmark]
    public byte[] Id_TryFormatBytes_HexLower()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.Hex);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_HexUpper()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.HexUpper);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base32()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base58()
    {
        var bytes = new byte[17];
        _id.TryFormat(bytes, out _, Idf.Base58);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base64()
    {
        var bytes = new byte[16];
        _id.TryFormat(bytes, out _, Idf.Base64Url);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base64Path2()
    {
        var bytes = new byte[18];
        _id.TryFormat(bytes, out _, Idf.Base64Path2);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base64Path3()
    {
        var bytes = new byte[19];
        _id.TryFormat(bytes, out _, Idf.Base64Path3);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormatBytes_Base85()
    {
        var bytes = new byte[15];
        _id.TryFormat(bytes, out _, Idf.Base85);
        return bytes;
    }

    [Benchmark]
    public byte[] Ulid_TryFormatBytes()
    {
        var bytes = new byte[26];
        _ulid.TryWriteStringify(bytes);
        return bytes;
    }

    #endregion TryFormatBytes

    #region TryParseBytes

    [Benchmark]
    public Id Id_TryParseBytes_Hex() => Id.TryParse(_idHexLower, Idf.Hex, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base32() => Id.TryParse(_idBase32, Idf.Base32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base58() => Id.TryParse(_idBase58, Idf.Base58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base64() => Id.TryParse(_idBase64Url, Idf.Base64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base64Path2() => Id.TryParse(_idBase64Path2, Idf.Base64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base64Path3() => Id.TryParse(_idBase64Path3, Idf.Base64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_Base85() => Id.TryParse(_idBase85, Idf.Base85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Ulid Ulid_TryDecode() => Ulid.TryParse(_ulidBase32, out var ulid) ? ulid : throw new FormatException();

    #endregion TryParseBytes

    #region TryParseBytes By Length

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Hex() => Id.TryParse(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base32() => Id.TryParse(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base58() => Id.TryParse(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base64() => Id.TryParse(_idBase64Url, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base64Path2() => Id.TryParse(_idBase64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base64Path3() => Id.TryParse(_idBase64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBytes_ByLen_Base85() => Id.TryParse(_idBase85, out var id) ? id : throw new FormatException();

    #endregion TryParseBytes By Length

    #region ParseBytes

    [Benchmark]
    public Id Id_ParseBytes_Hex() => Id.Parse(_idHexLower, Idf.Hex);

    [Benchmark]
    public Id Id_ParseBytes_Base32() => Id.Parse(_idBase32, Idf.Base32);

    [Benchmark]
    public Id Id_ParseBytes_Base58() => Id.Parse(_idBase58, Idf.Base58);

    [Benchmark]
    public Id Id_ParseBytes_Base64() => Id.Parse(_idBase64Url, Idf.Base64);

    [Benchmark]
    public Id Id_ParseBytes_Base64Path2() => Id.Parse(_idBase64Path2, Idf.Base64Path2);

    [Benchmark]
    public Id Id_ParseBytes_Base64Path3() => Id.Parse(_idBase64Path3, Idf.Base64Path3);

    [Benchmark]
    public Id Id_ParseBytes_Base85() => Id.Parse(_idBase85, Idf.Base85);

    [Benchmark]
    public Ulid Ulid_ParseBytes() => Ulid.Parse(_ulidBase32);

    #endregion ParseBytes

    #region ParseBytes By Length

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Hex() => Id.Parse(_idHexLower);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base32() => Id.Parse(_idBase32);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base58() => Id.Parse(_idBase58);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base64() => Id.Parse(_idBase64Url);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base64Path2() => Id.Parse(_idBase64Path2);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base64Path3() => Id.Parse(_idBase64Path3);

    [Benchmark]
    public Id Id_ParseBytes_ByLen_Base85() => Id.Parse(_idBase85);


    #endregion ParseBytes By Length
}