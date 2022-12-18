using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
#if NETCOREAPP3_0_OR_GREATER
using MongoDB.Bson;
#endif

namespace IT.Benchmarks;

[MemoryDiagnoser]
[MinColumn, MaxColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
public class IdBenchmark
{
    private readonly static Id _id = Id.NewObjectId();
    private readonly static Id _id2 = new(_id.Timestamp, _id.Machine, _id.Pid, _id.Increment);
    private readonly static byte[] _idArray = _id.ToByteArray();

    private readonly static Ulid _ulid = Ulid.NewUlid();
    private readonly static Ulid _ulid2 = new(_ulid.ToByteArray());
    private readonly static byte[] _ulidArray = _ulid.ToByteArray();

    private readonly static Guid _guid = Guid.NewGuid();
    private readonly static Guid _guid2 = new(_guid.ToByteArray());
    private readonly static byte[] _guidArray = _guid.ToByteArray();

#if NETCOREAPP3_0_OR_GREATER

    private readonly static ObjectId _objectId = ObjectId.GenerateNewId();
    private readonly static ObjectId _objectId2 = new(_objectId.ToByteArray());
    private readonly static byte[] _objectIdArray = _objectId.ToByteArray();

#endif
    //internal static Int32 _inc;

    #region Experimental

    //[Benchmark]
    //public int Id_Timestamp() => _id.Timestamp;

    //[Benchmark]
    //public int Id_Timestamp2() => _id.Timestamp2;

    //[Benchmark]
    //public int Id_Timestamp3() => _id.Timestamp3;

    //[Benchmark]
    //public int Id_Machine() => _id.Machine;

    //[Benchmark]
    //public int Id_Machine2() => _id.Machine2;

    //[Benchmark]
    //public int Id_Pid() => _id.Pid;

    //[Benchmark]
    //public int Id_Pid2() => _id.Pid2;

    //[Benchmark]
    //public int Id_Increment() => _id.Increment;

    //[Benchmark]
    //public int Id_Increment2() => _id.Increment2;

    #endregion Experimental

    #region Hashing

    //[Benchmark]
    //public UInt32 Id_XXH32() => _id.XXH32();
    //[Benchmark]
    //public UInt64 Id_XXH64() => _id.XXH64();

    //[Benchmark]
    //public UInt64 Id_XXH64_2() => _id.XXH64_2();

    #endregion Hashing

    #region New

    //[Benchmark]
    //public long Ticks_Increment() => DateTime.UtcNow.Ticks + Interlocked.Increment(ref _inc);

    //[Benchmark]
    //public Id Id_New() => Id.New();

    //[Benchmark]
    //public Id Id_NewOld() => Id.New_Old();

    //[Benchmark]
    //public Id Id_NewObjectId() => Id.NewObjectId();

    //[Benchmark]
    //public Id Id_NewObjectIdOld() => Id.NewObjectIdOld();

    //[Benchmark]
    //public Ulid Ulid_NewUlid() => Ulid.NewUlid();

    //[Benchmark]
    //public Guid Guid_NewGuid() => Guid.NewGuid();

    //[Benchmark]
    //public ObjectId ObjectId_GenerateNewId() => ObjectId.GenerateNewId();

    #endregion New

    #region GetHashCode

    //    [Benchmark]
    //    public int Id_GetHashCode() => _id.GetHashCode();

    //    [Benchmark]
    //    public int Id_GetHashCode2() => _id.GetHashCode2();

    //    [Benchmark]
    //    public int Ulid_GetHashCode() => _ulid.GetHashCode();

    //    [Benchmark]
    //    public int Guid_GetHashCode() => _guid.GetHashCode();

    //#if NETCOREAPP3_0_OR_GREATER

    //    [Benchmark]
    //    public int ObjectId_GetHashCode() => _objectId.GetHashCode();

    //#endif

    #endregion GetHashCode

    #region Equals

    [Benchmark]
    public bool Id_Equals_op() => _id == _id2;

    [Benchmark]
    public bool Id_Equals_opne() => _id != _id2;

    [Benchmark]
    public bool Id_Equals() => _id.Equals(_id2);

    [Benchmark]
    public bool Id_Equals2() => _id.Equals2(_id2);

    [Benchmark]
    public bool Id_Equals3() => _id.Equals3(_id2);
    
    [Benchmark]
    public bool Id_Equals4() => _id.Equals4(_id2);

    //[Benchmark]
    public bool Ulid_Equals() => _ulid.Equals(_ulid2);

    //[Benchmark]
    public bool Guid_Equals() => _guid.Equals(_guid2);

#if NETCOREAPP3_0_OR_GREATER

    //[Benchmark]
    public bool ObjectId_Equals() => _objectId.Equals(_objectId2);

#endif

    #endregion Equals

    #region CompareTo

    //[Benchmark]
    //public int Id_CompareTo() => _id.CompareTo(_id2);

    //[Benchmark]
    //public int Ulid_CompareTo() => _ulid.CompareTo(_ulid2);

    //[Benchmark]
    //public int Guid_CompareTo() => _guid.CompareTo(_guid2);

    //[Benchmark]
    //public int ObjectId_CompareTo() => _objectId.CompareTo(_objectId2);

    #endregion CompareTo

    #region ToByteArray

    //[Benchmark]
    //public byte[] Id_ToByteArray() => _id.ToByteArray();

    //[Benchmark]
    //public byte[] Id_ToByteArray2() => _id.ToByteArray2();

    //[Benchmark]
    //public byte[] Id_ToByteArray3() => _id.ToByteArray3();

    //[Benchmark]
    //public byte[] Ulid_ToByteArray() => _ulid.ToByteArray();

    //[Benchmark]
    //public byte[] Guid_ToByteArray() => _guid.ToByteArray();

    //[Benchmark]
    //public byte[] ObjectId_ToByteArray() => _objectId.ToByteArray();

    #endregion ToByteArray

    #region TryWrite

    //    [Benchmark]
    //    public Id Id_Write()
    //    {
    //        Span<byte> array = stackalloc byte[12];

    //        _id.TryWrite(array);

    //        return new Id(array);
    //    }

    //    [Benchmark]
    //    public Id Id_Write2()
    //    {
    //        Span<byte> array = stackalloc byte[12];

    //        _id.TryWrite2(array);

    //        return new Id(array);
    //    }

    //    [Benchmark]
    //    public Id Id_Write3()
    //    {
    //        Span<byte> array = stackalloc byte[12];

    //        _id.TryWrite3(array);

    //        return new Id(array);
    //    }

    //    [Benchmark]
    //    public Ulid Ulid_Write()
    //    {
    //        Span<byte> array = stackalloc byte[16];

    //        _ulid.TryWriteBytes(array);

    //        return new Ulid(array);
    //    }

    //#if NETCOREAPP3_0_OR_GREATER

    //        [Benchmark]
    //        public Guid Guid_Write()
    //        {
    //            Span<byte> array = stackalloc byte[16];

    //            _guid.TryWriteBytes(array);

    //            return new Guid(array);
    //        }

    //#endif

    //    [Benchmark]
    //    public ObjectId ObjectId_Write()
    //    {
    //        var array = new byte[12];

    //        _objectId.ToByteArray(array, 0);

    //        return new ObjectId(array);
    //    }

    #endregion TryWrite

    #region Ctors

    //[Benchmark]
    //public Id Id_CtorInt() => new(_id.Timestamp, _id.Machine, _id.Pid, _id.Increment);

    //[Benchmark]
    //public Id Id_Ctor() => new(_idArray);

    //[Benchmark]
    //public Ulid Ulid_Ctor() => new(_ulidArray);

    //[Benchmark]
    //public Guid Guid_Ctor() => new(_guidArray);

    //[Benchmark]
    //public ObjectId ObjectId_Ctor() => new(_objectIdArray);

    #endregion Ctors
}