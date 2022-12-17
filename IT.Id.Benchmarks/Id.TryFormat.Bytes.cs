using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryFormatBytes
{
    private readonly Ulid _ulid = Ulid.NewUlid();
    internal readonly Id _id = Id.NewObjectId();

    [Benchmark]
    public byte[] Id_TryFormat_HexLower()
    {
        var bytes = new byte[24];
        _id.TryFormat(bytes, out _, Idf.Hex);
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
    public byte[] Id_TryFormat_Base32Lower()
    {
        var bytes = new byte[20];
        _id.TryFormat(bytes, out _, Idf.Base32);
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
    public byte[] Id_TryFormat_Base58()
    {
        var bytes = new byte[17];
        _id.TryFormat(bytes, out _, Idf.Base58);
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
    public byte[] Id_TryFormat_Base64Path2()
    {
        var bytes = new byte[18];
        _id.TryFormat(bytes, out _, Idf.Base64Path2);
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
    public byte[] Id_TryFormat_Base85()
    {
        var bytes = new byte[15];
        _id.TryFormat(bytes, out _, Idf.Base85);
        return bytes;
    }

    [Benchmark]
    public byte[] Ulid_TryFormat()
    {
        var bytes = new byte[26];
        _ulid.TryWriteStringify(bytes);
        return bytes;
    }
}