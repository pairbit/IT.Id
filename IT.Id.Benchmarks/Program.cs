﻿using IT;
using IT.Benchmarks;
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

var bs = new IdParseByLenChars();

var ulid = bs.Ulid_Parse();

var bytes = ulid.ToByteArray();

#if NETCOREAPP3_1_OR_GREATER
var base32 = SimpleBase.Base32.Crockford.Encode(bytes);

if (!base32.Equals(bs._ulidString))
    Console.WriteLine($"Ulid '{bs._ulidString}' != Crockford base32 '{base32}'");
#endif

//var base32_2 = Wiry.Base32.Base32Encoding.Base32.GetString(bytes);

if (!bs.Id_Parse_ByLen_Hex().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base32().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base58().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base64().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base64Path2().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base64Path3().Equals(bs._id)) throw new InvalidOperationException();
if (!bs.Id_Parse_ByLen_Base85().Equals(bs._id)) throw new InvalidOperationException();

var ba = new IdParseChars();
if (!ba.Id_Parse_Hex().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base32().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base58().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base64().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base64Path2().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base64Path3().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base85().Equals(ba._id)) throw new InvalidOperationException();
if (!ba.Id_Parse_Base85().Equals(ba._id)) throw new InvalidOperationException();

var bc = new IdTryParseChars();

if (!bc.Id_TryParse_Hex().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base32().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base58().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base64().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base64Path2().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base64Path3().Equals(bc._id)) throw new InvalidOperationException();
if (!bc.Id_TryParse_Base85().Equals(bc._id)) throw new InvalidOperationException();

var bd = new IdTryParseByLenChars();
if (!bd.Id_TryParse_ByLen_Hex().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base32().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base58().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base64().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base64Path2().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base64Path3().Equals(bd._id)) throw new InvalidOperationException();
if (!bd.Id_TryParse_ByLen_Base85().Equals(bd._id)) throw new InvalidOperationException();

var bb = new IdParseBytes();

if (!bb.Id_Parse_Hex().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base32().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base58().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base64().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base64Path2().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base64Path3().Equals(bb._id)) throw new InvalidOperationException();
if (!bb.Id_Parse_Base85().Equals(bb._id)) throw new InvalidOperationException();

var be = new IdTryParseBytes();

if (!be.Id_TryParse_Hex().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base32().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base58().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base64().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base64Path2().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base64Path3().Equals(be._id)) throw new InvalidOperationException();
if (!be.Id_TryParse_Base85().Equals(be._id)) throw new InvalidOperationException();

var bi = new IdTryParseByLenBytes();

if (!bi.Id_TryParse_ByLen_Hex().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base32().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base58().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base64().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base64Path2().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base64Path3().Equals(bi._id)) throw new InvalidOperationException();
if (!bi.Id_TryParse_ByLen_Base85().Equals(bi._id)) throw new InvalidOperationException();

var bh = new IdParseByLenBytes();

if (!bh.Id_Parse_ByLen_Hex().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base32().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base58().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base64().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base64Path2().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base64Path3().Equals(bh._id)) throw new InvalidOperationException();
if (!bh.Id_Parse_ByLen_Base85().Equals(bh._id)) throw new InvalidOperationException();

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

//To Base Chars
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdToBase));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryToBaseChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseBaseChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseBaseChars));

////To Base Bytes
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryToBaseBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseBaseBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseBaseBytes));

////To Base Chars (Wraps)
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdToString));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryFormatChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseByLenChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseChars));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseByLenChars));

////To Base Bytes (Wraps)
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryFormatBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdParseByLenBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseBytes));
//BenchmarkDotNet.Running.BenchmarkRunner.Run(typeof(IdTryParseByLenBytes));

class MyClass
{
    //[JsonConverter(typeof(DateTimeOffsetJsonConverter))]
    public Id Id { get; set; }

    public Int32 Value { get; set; }
}