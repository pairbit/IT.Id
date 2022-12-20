using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace IT.Benchmarks;

[StructLayout(LayoutKind.Sequential, Pack = 1)]
public record Id40(Id Id, UInt32 value1, Byte value2);

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class StructBenchmark
{
    public const int Count = 100_000;
    private static readonly Id Id = Id.Min;

    [Benchmark]
    public int CopyInt32()
    {
        var max = 0; 
        var value = 123;
        for (int i = 0; i < Count; i++)
        {
            max = CopyLocal(value);
        }
        return max;
    }

    [Benchmark]
    public long CopyInt64()
    {
        var max = 0L;
        var value = 123;
        for (int i = 0; i < Count; i++)
        {
            max = CopyLocal(value);
        }
        return max;
    }

    //[Benchmark]
    //public Id8 CopyId8()
    //{
    //    Id8 max = default;
    //    var value = new Id8(Id, byte.MaxValue);
    //    for (int i = 0; i < Count; i++)
    //    {
    //        max = CopyLocal(value);
    //    }
    //    return max;
    //}

    //[Benchmark]
    //public Id16 CopyId16()
    //{
    //    Id16 max = default;
    //    var value = new Id16(Id, byte.MaxValue);
    //    for (int i = 0; i < Count; i++)
    //    {
    //        max = CopyLocal(value);
    //    }
    //    return max;
    //}

    //[Benchmark]
    //public Id32 CopyId32()
    //{
    //    Id32 max = default;
    //    var value = new Id32(Id, byte.MaxValue);
    //    for (int i = 0; i < Count; i++)
    //    {
    //        max = CopyLocal(value);
    //    }
    //    return max;
    //}

    [Benchmark]
    public Id40 CopyId40()
    {
        Id40 max = null!;
        var value = new Id40(Id, byte.MaxValue, byte.MaxValue);
        for (int i = 0; i < Count; i++)
        {
            max = CopyLocal(value);
        }
        return max;
    }

    [Benchmark]
    public Guid CopyGuid()
    {
        Guid max = default;
        var value = Guid.NewGuid();
        for (int i = 0; i < Count; i++)
        {
            max = CopyLocal(value);
        }
        return max;
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static T CopyLocal<T>(T value)
    {
        return value;
    }
}