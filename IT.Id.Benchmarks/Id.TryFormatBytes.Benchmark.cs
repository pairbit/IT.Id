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
    private readonly byte[] _idBase64;
    private readonly byte[] _idBase64Path2;
    private readonly byte[] _idBase64Path3;
    private readonly byte[] _idBase85;

    public IdTryFormatBytesBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_TryFormat_HexLower();
        _idBase32 = Id_TryFormat_Base32Lower();
        _idBase58 = Id_TryFormat_Base58();
        _idBase64 = Id_TryFormat_Base64();
        _idBase64Path2 = Id_TryFormat_Base64Path2();
        _idBase64Path3 = Id_TryFormat_Base64Path3();
        _idBase85 = Id_TryFormat_Base85();

        _ulid = Ulid.NewUlid();
        _ulidBase32 = Ulid_TryFormat();
    }

    #region TryFormat

    [Benchmark]
    public byte[] Id_TryFormat_HexLower()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.Hex);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToHexLower()
    {
        var bytes = new byte[24];
        _id.TryToHex(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_HexUpper()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.HexUpper);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToHexUpper()
    {
        var bytes = new byte[24];
        _id.TryToHexUpper(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base32Lower()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase32Lower()
    {
        var bytes = new byte[20];
        _id.TryToBase32(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base32Upper()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32Upper);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase32Upper()
    {
        var bytes = new byte[20];
        _id.TryToBase32Upper(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base58()
    {
        var bytes = new byte[17];
        _id.TryFormat(bytes, out _, Idf.Base58);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase58()
    {
        var bytes = new byte[17];
        _id.TryToBase58(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base64()
    {
        var bytes = new byte[16];
        _id.TryFormat(bytes, out _, Idf.Base64Url);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase64()
    {
        var bytes = new byte[16];
        _id.TryToBase64Url(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base64Path2()
    {
        var bytes = new byte[18];
        _id.TryFormat(bytes, out _, Idf.Base64Path2);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase64Path2()
    {
        var bytes = new byte[18];
        _id.TryToBase64Path2(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base64Path3()
    {
        var bytes = new byte[19];
        _id.TryFormat(bytes, out _, Idf.Base64Path3);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase64Path3()
    {
        var bytes = new byte[19];
        _id.TryToBase64Path3(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryFormat_Base85()
    {
        var bytes = new byte[15];
        _id.TryFormat(bytes, out _, Idf.Base85);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_TryToBase85()
    {
        var bytes = new byte[15];
        _id.TryToBase85(bytes);
        return bytes;
    }

    [Benchmark]
    public byte[] Ulid_TryFormat()
    {
        var bytes = new byte[26];
        _ulid.TryWriteStringify(bytes);
        return bytes;
    }

    #endregion TryFormat

    #region TryParse

    [Benchmark]
    public Id Id_TryParse_Hex() => Id.TryParse(_idHexLower, Idf.Hex, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseHex() => Id.TryParseHex(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base32() => Id.TryParse(_idBase32, Idf.Base32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase32() => Id.TryParseBase32(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base58() => Id.TryParse(_idBase58, Idf.Base58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase58() => Id.TryParseBase58(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base64() => Id.TryParse(_idBase64, Idf.Base64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64() => Id.TryParseBase64(_idBase64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base64Path2() => Id.TryParse(_idBase64Path2, Idf.Base64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64Path2() => Id.TryParseBase64Path2(_idBase64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base64Path3() => Id.TryParse(_idBase64Path3, Idf.Base64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase64Path3() => Id.TryParseBase64Path3(_idBase64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_Base85() => Id.TryParse(_idBase85, Idf.Base85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseBase85() => Id.TryParseBase85(_idBase85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Ulid Ulid_TryDecode() => Ulid.TryParse(_ulidBase32, out var ulid) ? ulid : throw new FormatException();

    #endregion TryParse

    #region TryParse By Length

    [Benchmark]
    public Id Id_TryParse_ByLen_Hex() => Id.TryParse(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base32() => Id.TryParse(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base58() => Id.TryParse(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base64() => Id.TryParse(_idBase64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base64Path2() => Id.TryParse(_idBase64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base64Path3() => Id.TryParse(_idBase64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParse_ByLen_Base85() => Id.TryParse(_idBase85, out var id) ? id : throw new FormatException();

    #endregion TryParse By Length

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
    public Id Id_Parse_Base64() => Id.Parse(_idBase64, Idf.Base64);

    [Benchmark]
    public Id Id_ParseBase64() => Id.ParseBase64(_idBase64);

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
    public Id Id_ParseBase85() => Id.ParseBase85(_idBase85);

    [Benchmark]
    public Ulid Ulid_Parse() => Ulid.Parse(_ulidBase32);

    #endregion Parse

    #region Parse By Length

    [Benchmark]
    public Id Id_Parse_ByLen_Hex() => Id.Parse(_idHexLower);

    [Benchmark]
    public Id Id_Parse_ByLen_Base32() => Id.Parse(_idBase32);

    [Benchmark]
    public Id Id_Parse_ByLen_Base58() => Id.Parse(_idBase58);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64() => Id.Parse(_idBase64);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64Path2() => Id.Parse(_idBase64Path2);

    [Benchmark]
    public Id Id_Parse_ByLen_Base64Path3() => Id.Parse(_idBase64Path3);

    [Benchmark]
    public Id Id_Parse_ByLen_Base85() => Id.Parse(_idBase85);


    #endregion Parse By Length
}