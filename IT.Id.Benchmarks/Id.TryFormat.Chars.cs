using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdTryFormatChars
{
    private readonly Id _id = Id.NewObjectId();
    private readonly Ulid _ulid = Ulid.NewUlid();

#if NETCOREAPP3_1_OR_GREATER
    private readonly Guid _guid = Guid.NewGuid();
#endif

    [Benchmark]
    public char[] Id_TryFormat_HexLower()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.Hex);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_HexUpper()
    {
        var chars = new char[24];
        _id.TryFormat(chars, out _, Idf.HexUpper);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base32Lower()
    {
        var chars = new char[20];
        _id.TryFormat(chars, out _, Idf.Base32);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base32Upper()
    {
        var chars = new char[20];
        _id.TryFormat(chars, out _, Idf.Base32Upper);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base58()
    {
        var chars = new char[17];
        _id.TryFormat(chars, out _, Idf.Base58);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base64()
    {
        var chars = new char[16];
        _id.TryFormat(chars, out _, Idf.Base64Url);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base64Path2()
    {
        var chars = new char[18];
        _id.TryFormat(chars, out _, Idf.Base64Path2);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base64Path3()
    {
        var chars = new char[19];
        _id.TryFormat(chars, out _, Idf.Base64Path3);
        return chars;
    }

    [Benchmark]
    public char[] Id_TryFormat_Base85()
    {
        var chars = new char[15];
        _id.TryFormat(chars, out _, Idf.Base85);
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