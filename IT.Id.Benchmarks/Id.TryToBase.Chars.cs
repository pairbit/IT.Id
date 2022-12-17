using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryToBaseChars
{
    private readonly Id _id = Id.NewObjectId();
    private readonly Ulid _ulid = Ulid.NewUlid();

#if NETCOREAPP3_1_OR_GREATER
    private readonly Guid _guid = Guid.NewGuid();
#endif

    [Benchmark]
    public char[] Id_TryToHexLower()
    {
        var chars = new char[24];
        _id.TryToHex(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToHexUpper()
    {
        var chars = new char[24];
        _id.TryToHexUpper(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase32Lower()
    {
        var chars = new char[20];
        _id.TryToBase32(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase32Upper()
    {
        var chars = new char[20];
        _id.TryToBase32Upper(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase58()
    {
        var chars = new char[17];
        _id.TryToBase58(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase64()
    {
        var chars = new char[16];
        _id.TryToBase64Url(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase64Path2()
    {
        var chars = new char[18];
        _id.TryToBase64Path2(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase64Path3()
    {
        var chars = new char[19];
        _id.TryToBase64Path3(chars);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryToBase85()
    {
        var chars = new char[15];
        _id.TryToBase85(chars);
        return chars;
    }

    [Benchmark]
    public char[] Ulid_TryFormat()
    {
        var chars = new char[26];
        _ulid.TryWriteStringify(chars);
        return chars;
    }

#if NETCOREAPP3_1_OR_GREATER

    [Benchmark]
    public char[] Guid_TryFormat()
    {
        var chars = new char[36];
        _guid.TryFormat(chars, out _);
        return chars;
    }

#endif
}