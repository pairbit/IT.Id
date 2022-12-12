using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryFormatCharsBenchmark
{
    private readonly Ulid _ulid;
    internal readonly char[] _ulidChars;

    private readonly Id _id;
    private readonly char[] _idHexLower;
    internal readonly char[] _idBase32;
    private readonly char[] _idBase58;
    private readonly char[] _idBase64;
    private readonly char[] _idBase64Path2;
    private readonly char[] _idBase64Path3;
    private readonly char[] _idBase85;

#if NETCOREAPP3_1_OR_GREATER
    private readonly Guid _guid;
    internal readonly char[] _guidChars;
#endif

    public IdTryFormatCharsBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_TryFormatChars_HexLower();
        _idBase32 = Id_TryFormatChars_Base32Lower();
        _idBase58 = Id_TryFormatChars_Base58();
        _idBase64 = Id_TryFormatChars_Base64();
        _idBase85 = Id_TryFormatChars_Base85();
        _idBase64Path2 = Id_TryFormatChars_Base64Path2();
        _idBase64Path3 = Id_TryFormatChars_Base64Path3();

        _ulid = Ulid.NewUlid();
        _ulidChars = Ulid_TryFormatChars();

#if NETCOREAPP3_1_OR_GREATER
        _guid = Guid.NewGuid();
        _guidChars = Guid_TryFormatChars();
#endif
    }

    #region TryFormatChars

    [Benchmark]
    public char[] Id_TryFormatChars_HexLower()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.Hex);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_HexUpper()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.HexUpper);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base32Lower()
    {
        var chars = new char[20];
        _id.TryFormat(chars, out _, Idf.Base32);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base32Upper()
    {
        var chars = new char[20];
        _id.TryFormat(chars, out _, Idf.Base32Upper);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base58()
    {
        var chars = new char[17];
        _id.TryFormat(chars, out _, Idf.Base58);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base64()
    {
        var chars = new char[16];
        _id.TryFormat(chars, out _, Idf.Base64Url);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base64Path2()
    {
        var chars = new char[18];
        _id.TryFormat(chars, out _, Idf.Base64Path2);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base64Path3()
    {
        var chars = new char[19];
        _id.TryFormat(chars, out _, Idf.Base64Path3);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormatChars_Base85()
    {
        var chars = new char[15];
        _id.TryFormat(chars, out _, Idf.Base85);
        return chars;
    }

    [Benchmark]
    public char[] Ulid_TryFormatChars()
    {
        var chars = new char[26];
        _ulid.TryWriteStringify(chars);
        return chars;
    }

#if NETCOREAPP3_1_OR_GREATER

    [Benchmark]
    public char[] Guid_TryFormatChars()
    {
        var chars = new char[36];
        _guid.TryFormat(chars, out _);
        return chars;
    }

#endif

    #endregion TryFormatChars

    #region TryParseChars

    [Benchmark]
    public Id Id_TryParseChars_Hex() => Id.TryParse(_idHexLower, Idf.Hex, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base32() => Id.TryParse(_idBase32, Idf.Base32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base58() => Id.TryParse(_idBase58, Idf.Base58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base64() => Id.TryParse(_idBase64, Idf.Base64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base64Path2() => Id.TryParse(_idBase64Path2, Idf.Base64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base64Path3() => Id.TryParse(_idBase64Path3, Idf.Base64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_Base85() => Id.TryParse(_idBase85, Idf.Base85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Ulid Ulid_TryParseChars() => Ulid.TryParse(_ulidChars, out var ulid) ? ulid : throw new FormatException();

#if NETCOREAPP3_1_OR_GREATER

    [Benchmark]
    public Guid Guid_TryParseChars() => Guid.TryParse(_guidChars, out var guid) ? guid : throw new FormatException();
#endif

    #endregion TryParseChars

    #region TryParseChars By Length

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Hex() => Id.TryParse(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base32() => Id.TryParse(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base58() => Id.TryParse(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base64() => Id.TryParse(_idBase64, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base64Path2() => Id.TryParse(_idBase64Path2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base64Path3() => Id.TryParse(_idBase64Path3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public Id Id_TryParseChars_ByLen_Base85() => Id.TryParse(_idBase85, out var id) ? id : throw new FormatException();

    #endregion TryParseChars By Length
}