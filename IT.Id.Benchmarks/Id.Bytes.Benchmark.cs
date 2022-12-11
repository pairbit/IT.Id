using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdBytesBenchmark
{
    private readonly Ulid _ulid;
    internal readonly byte[] _ulidBase32;

    private readonly Id _id;
    private readonly byte[] _idHexLower;
    internal readonly byte[] _idBase32;
    private readonly byte[] _idBase58;
    private readonly byte[] _idBase64Url;
    private readonly byte[] _idBase85;
    private readonly byte[] _idPath2;
    private readonly byte[] _idPath3;

    public IdBytesBenchmark()
    {
        //_id = Id.Parse("Y14-iRgzgKZclXbw");
        _id = Id.NewObjectId();
        _idHexLower = Id_Encode_HexLower();
        _idBase32 = Id_Encode_Base32();
        _idBase58 = Id_Encode_Base58();
        _idBase64Url = Id_Encode_Base64();
        _idBase85 = Id_Encode_Base85();
        _idPath2 = Id_Encode_Base64Path2();
        _idPath3 = Id_Encode_Base64Path3();

        _ulid = Ulid.NewUlid();
        _ulidBase32 = Ulid_Encode();
    }

    [Benchmark]
    public byte[] Id_Encode_HexLower()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.Hex);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Hex() => Id.Parse(_idHexLower);

    [Benchmark]
    public Id Id_TryDecode_Hex() => Id.TryParse(_idHexLower, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_HexUpper()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.HexUpper);
        return bytes;
    }

    [Benchmark]
    public byte[] Id_Encode_Base32()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base32() => Id.Parse(_idBase32);

    [Benchmark]
    public Id Id_TryDecode_Base32() => Id.TryParse(_idBase32, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_Base58()
    {
        var bytes = new byte[17];
        _id.TryFormat(bytes, out _, Idf.Base58);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base58() => Id.Parse(_idBase58);

    [Benchmark]
    public Id Id_TryDecode_Base58() => Id.TryParse(_idBase58, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_Base64()
    {
        var bytes = new byte[16];
        _id.TryFormat(bytes, out _, Idf.Base64Url);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base64() => Id.Parse(_idBase64Url);

    [Benchmark]
    public Id Id_TryDecode_Base64() => Id.TryParse(_idBase64Url, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_Base85()
    {
        var bytes = new byte[15];
        _id.TryFormat(bytes, out _, Idf.Base85);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base85() => Id.Parse(_idBase85);

    [Benchmark]
    public Id Id_TryDecode_Base85() => Id.TryParse(_idBase85, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_Base64Path2()
    {
        var bytes = new byte[18];
        _id.TryFormat(bytes, out _, Idf.Base64Path2);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base64Path2() => Id.Parse(_idPath2);

    [Benchmark]
    public Id Id_TryDecode_Base64Path2() => Id.TryParse(_idPath2, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Id_Encode_Base64Path3()
    {
        var bytes = new byte[19];
        _id.TryFormat(bytes, out _, Idf.Base64Path3);
        return bytes;
    }

    [Benchmark]
    public Id Id_Decode_Base64Path3() => Id.Parse(_idPath3);

    [Benchmark]
    public Id Id_TryDecode_Base64Path3() => Id.TryParse(_idPath3, out var id) ? id : throw new FormatException();

    [Benchmark]
    public byte[] Ulid_Encode()
    {
        var bytes = new byte[26];
        _ulid.TryWriteStringify(bytes);
        return bytes;
    }

    [Benchmark]
    public Ulid Ulid_Decode() => Ulid.Parse(_ulidBase32);

    [Benchmark]
    public Ulid Ulid_TryDecode() => Ulid.TryParse(_ulidBase32, out var ulid) ? ulid : throw new FormatException();
}