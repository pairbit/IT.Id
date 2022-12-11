using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace IT.IdBenchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdBenchmark
{
    private readonly static Id _id = Id.NewObjectId();
    private readonly static Id _id2 = new(_id.Timestamp, _id.B, _id.C);

    [Benchmark]
    public Id Id_New() => Id.New();

    [Benchmark]
    public Id Id_NewObjectId() => Id.NewObjectId();

    [Benchmark]
    public int Id_GetHashCode() => _id.GetHashCode();

    [Benchmark]
    public bool Id_Equals() => _id.Equals(_id2);

    [Benchmark]
    public int Id_CompareTo() => _id.CompareTo(_id2);

    [Benchmark]
    public byte[] Id_ToByteArray() => _id.ToByteArray();

    [Benchmark]
    public Id Id_Write()
    {
        Span<byte> array = stackalloc byte[12];

        _id.Write(array);

        return new Id(array);
    }

    [Benchmark]
    public Id Id_Ctor() => new(_id.Timestamp, _id.B, _id.C);

    [Benchmark]
    public UInt32 Id_XXH32() => _id.XXH32();

    [Benchmark]
    public UInt64 Id_XXH64() => _id.XXH64();
}