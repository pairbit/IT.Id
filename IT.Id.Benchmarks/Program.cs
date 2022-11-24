﻿using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;

//var random = new Random(123);
//var high = random.Next();
//var low = random.Next();

//Console.WriteLine($"{high} - {low}");

//Console.WriteLine($"SizeOf Id - {System.Runtime.InteropServices.Marshal.SizeOf<Id>()} bytes");
//Console.WriteLine($"SizeOf Id8 - {System.Runtime.InteropServices.Marshal.SizeOf<Id6>()} bytes");
//Console.WriteLine($"SizeOf Id16 - {System.Runtime.InteropServices.Marshal.SizeOf<Id12>()} bytes");
//Console.WriteLine($"SizeOf Id8i - {System.Runtime.InteropServices.Marshal.SizeOf<Id6i>()} bytes");
//Console.WriteLine($"SizeOf Id12i - {System.Runtime.InteropServices.Marshal.SizeOf<Id12i>()} bytes");

//Console.WriteLine($"{Id.GetMachineHash()} - {Id.GetMachineXXHash()}");

//var b = new byte[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 };
var b = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 58 };
//var b = new byte[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
var a = new Id(b);
Console.WriteLine($"{a:B58} == {SimpleBase.Base58.Bitcoin.Encode(b)}");

//Console.ReadLine();

var id = Id.Parse("62A84F674031E78D474FE23F");
//var id = Id.New();

#region Json

var serializerOptions = new JsonSerializerOptions();
serializerOptions.Converters.Add(new JsonIdConverter { Format = Idf.Base58 });
serializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

var ids = JsonSerializer.Serialize(id, serializerOptions);

var idd = JsonSerializer.Deserialize<Id>(ids);

if (!idd.Equals(id)) throw new InvalidOperationException();

var myobj = new MyClass { Id = id, Value = 234 };

var myobjser = JsonSerializer.Serialize(myobj, serializerOptions);

myobj = JsonSerializer.Deserialize<MyClass>(myobjser);

if (myobj == null || !myobj.Id.Equals(id)) throw new InvalidOperationException();

#endregion Json

var idCopy2 = new Id(id.Timestamp, id.B, id.C);
var idCopy3 = new Id(id.Timestamp, id.Machine, id.Pid, id.Increment);

var machine = Id.New().Machine;

if (!id.Equals(idCopy2) || !id.Equals(idCopy3) || machine != Id.MachineHash24) throw new InvalidOperationException();

var process = Process.GetCurrentProcess();

Console.WriteLine($"{id} = {process.Id} == {id.Pid}, machine = {id.Machine}");

var pid = (short)process.Id;

if (!process.Id.Equals(Environment.ProcessId)) throw new InvalidOperationException();

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

var idb = new IT.Id.Benchmarks.IdBenchmark();

var ulid = idb.Ulid_Decode();

var bytes = ulid.ToByteArray();

var base32 = SimpleBase.Base32.Crockford.Encode(bytes);

if (!base32.Equals(idb._ulidString))
    Console.WriteLine($"Ulid '{idb._ulidString}' != Crockford base32 '{base32}'");

//var base32_2 = Wiry.Base32.Base32Encoding.Base32.GetString(bytes);

var id1 = idb.Id_Decode_Hex();
var id2 = idb.Id_Decode_Hex();
var id3 = idb.Id_Decode_Base85();
var id4 = idb.Id_Decode_Path2();
var id5 = idb.Id_Decode_Path3();
var id6 = idb.Id_Decode_Base32();
var id7 = idb.Id_Decode_Base58();

//if (!idb._idBase32.Equals("CDF3X28R6E0ACQ4NEVR0")) throw new InvalidOperationException();

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
    if (Id.Parse(idn.ToString(Idf.Path2)) != idn) throw new InvalidOperationException("Path2");
    if (Id.Parse(idn.ToString(Idf.Path3)) != idn) throw new InvalidOperationException("Path3");
    if (Id.Parse(idn.ToString(Idf.Base58)) != idn) throw new InvalidOperationException("Base58");
}

var f1 = id.ToString(Idf.Base64Url);
var f2 = id.ToString(null);
var f3 = $"{id}";
var f4 = id.ToString();

if (!f1.Equals(f2) || !f1.Equals(f3) || !f1.Equals(f4) || !id.Equals(Id.Parse(f4)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Base64);
f2 = id.ToString("B64");
f3 = $"{id:B64}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Base85);
f2 = id.ToString("B85");
f3 = $"{id:B85}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Path2);
f2 = id.ToString("P2");
f3 = $"{id:P2}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Path3);
f2 = id.ToString("P3");
f3 = $"{id:P3}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Hex);
f2 = id.ToString("h");
f3 = $"{id:h}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)) || !id.Equals(Id.Parse(f3, Idf.Hex)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.HexUpper);
f2 = id.ToString("H");
f3 = $"{id:H}";

if (!f1.Equals(f2) || !f1.Equals(f3) || !id.Equals(Id.Parse(f3)) || !id.Equals(Id.Parse(f3, Idf.HexUpper)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Base32);
f2 = id.ToString("B32");
f3 = $"{id:B32}";
f4 = SimpleBase.Base32.Crockford.Encode(id.ToByteArray());

if (!f1.Equals(f2) || !f1.Equals(f3) || !f1.Equals(f4) || 
    !id.Equals(Id.Parse(f2)) || !id.Equals(Id.Parse(f2, Idf.Base32)))
    throw new InvalidOperationException();

f1 = id.ToString(Idf.Base58);
f2 = id.ToString("B58");
f3 = $"{id:B58}";
f4 = SimpleBase.Base58.Bitcoin.Encode(id.ToByteArray());

if (f1.Length != 17 || !f1.Equals(f2) || !f1.Equals(f3) || !f1.Equals(f4) ||
    !id.Equals(Id.Parse(f2)) || !id.Equals(Id.Parse(f2, Idf.Base58)))
    throw new InvalidOperationException();

for (int i = 0; i < 12; i++)
{
    idBytes[i] = 0;
    id = new Id(idBytes);

    var id58 = $"{id:B58}";

    var id58o = SimpleBase.Base58.Bitcoin.Encode(idBytes);

    if (id58.Length != 17 || !id.ToString("B58").Equals(id58)) throw new InvalidOperationException();

    if (!id58.EndsWith(id58o)) throw new InvalidOperationException();

    if (!id.Equals(Id.Parse(id58o, Idf.Base58))) throw new InvalidOperationException();

    if (!id.Equals(Id.Parse(id58))) throw new InvalidOperationException();

    //Console.WriteLine($"{i,2} -> {id.Created,19} -> {id,17:58}");

    Console.WriteLine($"{i,2} -> {id.Created,19} -> {id58,17} -> {id58o,17} -> {id58o.Length}");
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
id = Id.Min;
//id = Id.New(new DateTime(1998, 3, 5));
f1 = id.ToString(Idf.Base58);
f2 = id.ToString("B58");
f3 = $"{id:B58}";
f4 = SimpleBase.Base58.Bitcoin.Encode(id.ToByteArray());

if (!f1.Equals(f2) || !f1.Equals(f3) || !f1.EndsWith(f4))
    throw new InvalidOperationException();

if (!id.Equals(Id.Parse(f2, Idf.Base58)))
    throw new InvalidOperationException();

if (f1.Length != 17) throw new InvalidOperationException();

Console.WriteLine("Ok");

BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IT.Id.Benchmarks.IdBenchmark));

class MyClass
{
    //[JsonConverter(typeof(DateTimeOffsetJsonConverter))]
    public Id Id { get; set; }

    public Int32 Value { get; set; }
}