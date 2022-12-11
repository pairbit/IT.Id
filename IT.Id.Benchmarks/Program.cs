using IT;
using IT.IdBenchmarks;
using System.Diagnostics;

//var random = new Random(123);
//var high = random.Next();
//var low = random.Next();

//Console.WriteLine($"{high} - {low}");

Console.WriteLine($"SizeOf Id - {System.Runtime.InteropServices.Marshal.SizeOf<Id>()} bytes");
//Console.WriteLine($"SizeOf Id8 - {System.Runtime.InteropServices.Marshal.SizeOf<Id8>()} bytes");
//Console.WriteLine($"SizeOf Id16 - {System.Runtime.InteropServices.Marshal.SizeOf<Id16>()} bytes");
Console.WriteLine($"SizeOf Id32 - {System.Runtime.InteropServices.Marshal.SizeOf<Id32>()} bytes");
Console.WriteLine($"SizeOf Guid - {System.Runtime.InteropServices.Marshal.SizeOf<Guid>()} bytes");
Console.WriteLine($"SizeOf Id40 - {System.Runtime.InteropServices.Marshal.SizeOf<Id40>()} bytes");
//Console.WriteLine($"SizeOf Id8i - {System.Runtime.InteropServices.Marshal.SizeOf<Id6i>()} bytes");
//Console.WriteLine($"SizeOf Id12i - {System.Runtime.InteropServices.Marshal.SizeOf<Id12i>()} bytes");

//Console.WriteLine($"{Id.GetMachineHash()} - {Id.GetMachineXXHash()}");


var id = Id.Parse("62A84F674031E78D474FE23F");
//var id = Id.New();

var idCopy2 = new Id(id.Timestamp, id.Machine, id.Pid, id.Increment);

var machine = Id.New().Machine;

if (!id.Equals(idCopy2) || machine != Id.MachineHash24) throw new InvalidOperationException();

var process = Process.GetCurrentProcess();

Console.WriteLine($"{id} = {process.Id} == {id.Pid}, machine = {id.Machine}");

//byte value6 = 60;

//var id8 = new Id6(id, value6);
//if (!id8.Id.Equals(id) || id8.Value != value6)
//    throw new InvalidOperationException();

//var index = Id6i.MaxIndex - 215;
//var id8i = new Id6i(id, value6, index);
//if (!id8i.Id.Equals(id) || id8i.Value != value6 || id8i.Index != index)
//    throw new InvalidOperationException();

//ushort value12 = (64 * 64) - 10;

//var id16 = new Id12(id, value12);
//if (!id16.Id.Equals(id) || id16.Value != value12)
//    throw new InvalidOperationException();

var ids = new IdStringBenchmark();

var ulid = ids.Ulid_Decode();

var bytes = ulid.ToByteArray();

#if NETCOREAPP3_1_OR_GREATER
var base32 = SimpleBase.Base32.Crockford.Encode(bytes);

if (!base32.Equals(ids._ulidString))
    Console.WriteLine($"Ulid '{ids._ulidString}' != Crockford base32 '{base32}'");
#endif

//var base32_2 = Wiry.Base32.Base32Encoding.Base32.GetString(bytes);

var id1 = ids.Id_Decode_Hex();
var id2 = ids.Id_Decode_Base64();
var id3 = ids.Id_Decode_Base85();
var id4 = ids.Id_Decode_Base64Path2();
var id5 = ids.Id_Decode_Base64Path3();
var id6 = ids.Id_Decode_Base32();
var id7 = ids.Id_Decode_Base58();

//if (!idb._idBase32.Equals("CDF3X28R6E0ACQ4NEVR0")) throw new InvalidOperationException();

if (!id1.Equals(id2) || !id1.Equals(id3) || !id1.Equals(id4) || 
    !id1.Equals(id5) || !id1.Equals(id6) || !id1.Equals(id7)) throw new InvalidOperationException();

var idb = new IdBytesBenchmark();

id1 = idb.Id_Decode_Hex();
id2 = idb.Id_Decode_Base32();
id3 = idb.Id_Decode_Base58();
id4 = idb.Id_Decode_Base64();
id5 = idb.Id_Decode_Base64Path2();
id6 = idb.Id_Decode_Base64Path3();
id7 = idb.Id_Decode_Base85();

if (!id1.Equals(id2) || !id1.Equals(id3) || !id1.Equals(id4) ||
    !id1.Equals(id5) || !id1.Equals(id6) || !id1.Equals(id7)) throw new InvalidOperationException();

id1 = idb.Id_TryDecode_Hex();
id2 = idb.Id_TryDecode_Base32();
id3 = idb.Id_TryDecode_Base58();
id4 = idb.Id_TryDecode_Base64();
id5 = idb.Id_TryDecode_Base64Path2();
id6 = idb.Id_TryDecode_Base64Path3();
id7 = idb.Id_TryDecode_Base85();

if (!id1.Equals(id2) || !id1.Equals(id3) || !id1.Equals(id4) ||
    !id1.Equals(id5) || !id1.Equals(id6) || !id1.Equals(id7)) throw new InvalidOperationException();

var idc = new IdTryCharsBenchmark();

id1 = idc.Id_Decode_Hex();
id2 = idc.Id_Decode_Base32();
id3 = idc.Id_Decode_Base58();
id4 = idc.Id_Decode_Base64();
id5 = idc.Id_Decode_Base64Path2();
id6 = idc.Id_Decode_Base64Path3();
id7 = idc.Id_Decode_Base85();

if (!id1.Equals(id2) || !id1.Equals(id3) || !id1.Equals(id4) ||
    !id1.Equals(id5) || !id1.Equals(id6) || !id1.Equals(id7)) throw new InvalidOperationException();

Console.WriteLine("Ok");

var idBytes = id.ToByteArray();

for (int i = 0; i < 10000; i++)
{
    var idn = Id.New();
    //if (idn.XXH32() != XXH32.DigestOf(idn.ToByteArray())) throw new InvalidOperationException("XXH32");

    //if (idn.XXH64() != XXH64.DigestOf(idn.ToByteArray())) throw new InvalidOperationException("XXH64");

    if (Id.Parse(idn.ToString(Idf.Base32)) != idn) throw new InvalidOperationException("Base32");
    if (Id.Parse(idn.ToString(Idf.HexUpper)) != idn) throw new InvalidOperationException("HexUpper");
    if (Id.Parse(idn.ToString(Idf.Hex)) != idn) throw new InvalidOperationException("Hex");
    if (Id.Parse(idn.ToString(Idf.Base85)) != idn) throw new InvalidOperationException("Base85");
    if (Id.Parse(idn.ToString(Idf.Base64)) != idn) throw new InvalidOperationException("Base64");
    if (Id.Parse(idn.ToString(Idf.Base64Path2)) != idn) throw new InvalidOperationException("Path2");
    if (Id.Parse(idn.ToString(Idf.Base64Path3)) != idn) throw new InvalidOperationException("Path3");
    if (Id.Parse(idn.ToString(Idf.Base58)) != idn) throw new InvalidOperationException("Base58");
}

for (int i = 0; i < 12; i++)
{
    idBytes[i] = 0;
    id = new Id(idBytes);

    var id58 = $"{id:i}";

    if (id58.Length != 17 || !id.ToString("i").Equals(id58)) throw new InvalidOperationException();

    if (!id.Equals(Id.Parse(id58))) throw new InvalidOperationException();

    Console.Write($"{i,2} -> {id.Created,19} -> {id58,17}");

#if NETCOREAPP3_1_OR_GREATER

    var id58o = SimpleBase.Base58.Bitcoin.Encode(idBytes);

    if (!id58.EndsWith(id58o)) throw new InvalidOperationException();

    if (!id.Equals(Id.Parse(id58o, Idf.Base58))) throw new InvalidOperationException();

    //Console.WriteLine($"{i,2} -> {id.Created,19} -> {id,17:58}");

    Console.Write($" -> {id58o,17} -> {id58o.Length}");
#endif

    Console.WriteLine();
}

/*
 0 -> 15.03.1970 19:16:33 ->  1R8QhKd5DFL1skqt -> 16
 1 -> 01.01.1970  5:26:41 ->  115JVswC8Es7Q54S -> 16
 2 -> 01.01.1970  0:02:25 ->  1112rDCsNHWP4hhg -> 16
 3 -> 01.01.1970  0:00:00 ->   1111AQNTfShuUfg -> 15
 4 -> 01.01.1970  0:00:00 ->   1111136ntErHRgi -> 15
 5 -> 01.01.1970  0:00:00 ->    111111uTNTr2FQ -> 14
 6 -> 01.01.1970  0:00:00 ->    1111111AjDiNpE -> 14
 7 -> 01.01.1970  0:00:00 ->    111111112TzzYW -> 14
 8 -> 01.01.1970  0:00:00 ->     111111111VhUr -> 13
 9 -> 01.01.1970  0:00:00 ->     11111111119Yi -> 13
10 -> 01.01.1970  0:00:00 ->     111111111112N -> 13
11 -> 01.01.1970  0:00:00 ->      111111111111 -> 12
 */

Console.WriteLine("Ok");

BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdBenchmark));

//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdBytesBenchmark));

//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryCharsBenchmark));

//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdStringBenchmark));

class MyClass
{
    //[JsonConverter(typeof(DateTimeOffsetJsonConverter))]
    public Id Id { get; set; }

    public Int32 Value { get; set; }
}