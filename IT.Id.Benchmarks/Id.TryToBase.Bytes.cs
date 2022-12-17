using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryToBaseBytes
{
    private readonly Ulid _ulid = Ulid.NewUlid();
    internal readonly Id _id = Id.NewObjectId();

    [Benchmark]
    public byte[] Id_TryToHexLower()
    {
        var bytes = new byte[24];
        _id.TryToHex(bytes);
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
    public byte[] Id_TryToBase32Lower()
    {
        var bytes = new byte[20];
        _id.TryToBase32(bytes);
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
    public byte[] Id_TryToBase58()
    {
        var bytes = new byte[17];
        _id.TryToBase58(bytes);
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
    public byte[] Id_TryToBase64Path2()
    {
        var bytes = new byte[18];
        _id.TryToBase64Path2(bytes);
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
}