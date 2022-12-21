using System.Buffers.Binary;
using System.Buffers.Text;
using System.Runtime.CompilerServices;

namespace IT.Tests;

public class Id32ToStringTest
{
    [SetUp]
    public void Setup()
    {

    }

    [Test]
    public void TypeWithIndex()
    {
        var id = Id.New();

        short type = 17;
        short index = 10;

        var id32 = new Id32(id, type, index);

        var value = id32.Value;

        ref var b = ref Unsafe.As<Id32, byte>(ref id32);

        Assert.That(BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<short>(ref Unsafe.Add(ref b, 12))), Is.EqualTo(type));
        Assert.That(BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<short>(ref Unsafe.Add(ref b, 14))), Is.EqualTo(index));

        Assert.That((short)(value >> 16), Is.EqualTo(type));
        Assert.That((short)value, Is.EqualTo(index));

        var id32base64 = id32.ToBase64Url();

        var idbase64 = id.ToBase64Url();
        var valuebase64 = Id32.ToBase64Url((uint)value);

        Assert.That($"{idbase64}{valuebase64}_", Is.EqualTo(id32base64));

        id = Id.Parse("Y6MQTcwJH1V46fft");

        // 1 length
        Assert.That(ToBase64Url(id, 0, 0), Is.EqualTo("0"));
        Assert.That(ToBase64Url(id, 0, 63), Is.EqualTo("_"));

        //3 length
        Assert.That(ToBase64Url(id, 0, 64), Is.EqualTo("00_"));
        Assert.That(ToBase64Url(id, 0, 64 * 64 + 63), Is.EqualTo("___"));
        
        //4 length
        Assert.That(ToBase64Url(id, 0, ushort.MaxValue), Is.EqualTo("e-__"));

        Assert.That(ToBase64Url(id, 3, 0), Is.EqualTo("K_0_"));
        Assert.That(ToBase64Url(id, 3, 63), Is.EqualTo("K___"));
        Assert.That(ToBase64Url(id, 3, ushort.MaxValue), Is.EqualTo("--__"));

        //5 length
        Assert.That(ToBase64Url(id, 17, 0), Is.EqualTo("3e_0_"));
        Assert.That(ToBase64Url(id, 17, 10), Is.EqualTo("3e_a_"));
        Assert.That(ToBase64Url(id, 17, 11), Is.EqualTo("3e_b_"));
        Assert.That(ToBase64Url(id, 17, 32000), Is.EqualTo("3mP0_"));
        Assert.That(ToBase64Url(id, 17, ushort.MaxValue), Is.EqualTo("3u-__"));
        Assert.That(ToBase64Url(id, 259, ushort.MaxValue), Is.EqualTo("_--__"));

        //6 length
        Assert.That(ToBase64Url(id, 16643, ushort.MaxValue), Is.EqualTo("__--__"));
        
        //7 length
        Assert.That(ToBase64Url(id, 32000, 0), Is.EqualTo("0X--_0_"));
        Assert.That(ToBase64Url(id, 32000, 10), Is.EqualTo("0X--_a_"));
        Assert.That(ToBase64Url(id, 32000, 63), Is.EqualTo("0X--___"));
        Assert.That(ToBase64Url(id, 32000, 64), Is.EqualTo("0X-_00_"));
        Assert.That(ToBase64Url(id, ushort.MaxValue, ushort.MaxValue), Is.EqualTo("2----__"));
    }

    [Test]
    public void Sizeof16()
    {
        Assert.That(System.Runtime.InteropServices.Marshal.SizeOf<Id32>(), Is.EqualTo(16));
    }

    [Test]
    public void Run()
    {
        var id = Id.New();
        var id32 = new Id32(id, 63);

        Assert.That(id32, Is.Not.EqualTo(new Id32(Id.New(), 63)));
        Assert.That(id32, Is.EqualTo(new Id32(id, 63)));

        TestId(new Id32(id, 0));
        TestId(new Id32(id, 15));
        TestId(new Id32(id, 16));
        TestId(new Id32(id, 63));
        TestId(new Id32(id, 65));
        TestId(new Id32(id, 255));
    }

    private void TestId(Id32 id)
    {
        var base64 = id.ToString(Idf.Base64Url);
        var hexUpper = id.ToString(Idf.HexUpper);
        var hexLower = id.ToString(Idf.Hex);

        Assert.That(Id32.Parse(base64), Is.EqualTo(id));
        Assert.That(Id32.Parse(hexUpper), Is.EqualTo(id));
        Assert.That(Id32.Parse(hexLower), Is.EqualTo(id));
    }

    private static string ToBase64Url(Id id, ushort type, ushort index) => new Id32(id, (short)type, (short)index).ToBase64Url().Substring(16);
}