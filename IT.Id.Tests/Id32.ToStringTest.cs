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
        Assert.That(new Id32(id, 0, 0).ToBase64Url().Substring(16), Is.EqualTo("0_"));
        Assert.That(new Id32(id, 0, 63).ToBase64Url().Substring(16), Is.EqualTo("__"));

        //3 length
        Assert.That(new Id32(id, 0, 64).ToBase64Url().Substring(16), Is.EqualTo("00_"));

        //5 length
        Assert.That(new Id32(id, 17, 0).ToBase64Url().Substring(16), Is.EqualTo("3e_0_"));
        Assert.That(new Id32(id, 17, 10).ToBase64Url().Substring(16), Is.EqualTo("3e_a_"));
        Assert.That(new Id32(id, 17, 11).ToBase64Url().Substring(16), Is.EqualTo("3e_b_"));
        Assert.That(new Id32(id, 17, 32000).ToBase64Url().Substring(16), Is.EqualTo("3mP0_"));
        Assert.That(new Id32(id, 17, short.MaxValue).ToBase64Url().Substring(16), Is.EqualTo("3m-__"));

        //7 length
        Assert.That(new Id32(id, 32000, 0).ToBase64Url().Substring(16), Is.EqualTo("0X--_0_"));
        Assert.That(new Id32(id, 32000, 10).ToBase64Url().Substring(16), Is.EqualTo("0X--_a_"));
        Assert.That(new Id32(id, 32000, 63).ToBase64Url().Substring(16), Is.EqualTo("0X--___"));
        Assert.That(new Id32(id, 32000, 64).ToBase64Url().Substring(16), Is.EqualTo("0X-_00_"));
        Assert.That(new Id32(id, short.MaxValue, short.MaxValue).ToBase64Url().Substring(16), Is.EqualTo("0--S-__"));
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
}