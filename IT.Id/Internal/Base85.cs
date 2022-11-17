using System.Runtime.CompilerServices;

namespace System;

internal static class Base85
{
	private const uint U85P1 = 85u;
	private const uint U85P2 = 85u * 85u;
	private const uint U85P3 = 85u * 85u * 85u;
	private const uint U85P4 = 85u * 85u * 85u * 85u;

	private const string Z85Xml = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ.-:+=^!/*?_~|()[]{}@%$#";

	private const int MAX_DIGIT = 255;

	private static readonly byte[] _byte2Byte = new byte[MAX_DIGIT + 1];
	private static readonly byte[] _char2Byte = new byte[MAX_DIGIT + 1];
	private static readonly char[] _byte2Char = new char[MAX_DIGIT + 1];
	private static readonly bool[] _validChar = new bool[MAX_DIGIT + 1];

	private static ReadOnlySpan<char> ByteToChar => _byte2Char.AsSpan();

	static Base85()
	{
		BuildDict(Z85Xml);
	}

	private static void BuildDict(string digits)
	{
		for (var i = 0; i < digits.Length; i++)
		{
			var c = digits[i];
			var d = (byte)i;
			if (c > MAX_DIGIT) throw new ArgumentException($"Invalid character '{c}'");
			if (_validChar[c]) throw new ArgumentException($"Character '{c}' is duplicated");
			
			_byte2Byte[d] = (byte)c;
			_byte2Char[d] = c;
			_char2Byte[c] = d;
			_validChar[c] = true;
		}
	}

	public static unsafe void Decode(ReadOnlySpan<char> source, Span<byte> target)
	{
		fixed (char* sourceP = source)
		fixed (byte* targetP = target)
		fixed (byte* map = _char2Byte)
		{
			var value0 = Decode1(map, *sourceP) * U85P4 +
						 Decode1(map, *(sourceP + 1)) * U85P3 +
						 Decode1(map, *(sourceP + 2)) * U85P2 +
						 Decode1(map, *(sourceP + 3)) * U85P1 +
						 Decode1(map, *(sourceP + 4));

			*(targetP + 0) = (byte)(value0 >> 24);
			*(targetP + 1) = (byte)(value0 >> 16);
			*(targetP + 2) = (byte)(value0 >> 8);
			*(targetP + 3) = (byte)value0;

			var value1 = Decode1(map, *(sourceP + 5)) * U85P4 +
						 Decode1(map, *(sourceP + 6)) * U85P3 +
						 Decode1(map, *(sourceP + 7)) * U85P2 +
						 Decode1(map, *(sourceP + 8)) * U85P1 +
						 Decode1(map, *(sourceP + 9));

			*(targetP + 4) = (byte)(value1 >> 24);
			*(targetP + 5) = (byte)(value1 >> 16);
			*(targetP + 6) = (byte)(value1 >> 8);
			*(targetP + 7) = (byte)value1;

			var value2 = Decode1(map, *(sourceP + 10)) * U85P4 +
						 Decode1(map, *(sourceP + 11)) * U85P3 +
						 Decode1(map, *(sourceP + 12)) * U85P2 +
						 Decode1(map, *(sourceP + 13)) * U85P1 +
						 Decode1(map, *(sourceP + 14));

			*(targetP + 8) = (byte)(value2 >> 24);
			*(targetP + 9) = (byte)(value2 >> 16);
			*(targetP + 10) = (byte)(value2 >> 8);
			*(targetP + 11) = (byte)value2;
		}
	}

	public static unsafe void Decode(ReadOnlySpan<byte> source, Span<byte> target)
	{
		fixed (byte* sourceP = source)
		fixed (byte* targetP = target)
		fixed (byte* map = _char2Byte)
		{
			var value0 = *(map + *sourceP) * U85P4 +
						 *(map + *(sourceP + 1)) * U85P3 +
						 *(map + *(sourceP + 2)) * U85P2 +
						 *(map + *(sourceP + 3)) * U85P1 +
						 *(map + *(sourceP + 4));

			*(targetP + 0) = (byte)(value0 >> 24);
			*(targetP + 1) = (byte)(value0 >> 16);
			*(targetP + 2) = (byte)(value0 >> 8);
			*(targetP + 3) = (byte)value0;

			var value1 = *(map + *(sourceP + 5)) * U85P4 +
						 *(map + *(sourceP + 6)) * U85P3 +
						 *(map + *(sourceP + 7)) * U85P2 +
						 *(map + *(sourceP + 8)) * U85P1 +
						 *(map + *(sourceP + 9));

			*(targetP + 4) = (byte)(value1 >> 24);
			*(targetP + 5) = (byte)(value1 >> 16);
			*(targetP + 6) = (byte)(value1 >> 8);
			*(targetP + 7) = (byte)value1;

			var value2 = *(map + *(sourceP + 10)) * U85P4 +
						 *(map + *(sourceP + 11)) * U85P3 +
						 *(map + *(sourceP + 12)) * U85P2 +
						 *(map + *(sourceP + 13)) * U85P1 +
						 *(map + *(sourceP + 14));

			*(targetP + 8) = (byte)(value2 >> 24);
			*(targetP + 9) = (byte)(value2 >> 16);
			*(targetP + 10) = (byte)(value2 >> 8);
			*(targetP + 11) = (byte)value2;
		}
	}

	public static unsafe void Encode(ReadOnlySpan<byte> source, Span<char> target)
	{
		fixed (byte* sourceP = source)
		fixed (char* targetP = target)
			Encode(sourceP, targetP);
	}

	public static unsafe void Encode(byte* sourceP, char* targetP)
	{
		fixed (char* map = ByteToChar)
		{
			var value0 = (uint)*(sourceP + 0) << 24 | (uint)*(sourceP + 1) << 16 | (uint)*(sourceP + 2) << 8 | *(sourceP + 3);

			*(targetP + 0) = *(map + (value0 / U85P4).Mod85());
			*(targetP + 1) = *(map + (value0 / U85P3).Mod85());
			*(targetP + 2) = *(map + (value0 / U85P2).Mod85());
			*(targetP + 3) = *(map + (value0 / U85P1).Mod85());
			*(targetP + 4) = *(map + value0.Mod85());

			var value1 = (uint)*(sourceP + 4) << 24 | (uint)*(sourceP + 5) << 16 | (uint)*(sourceP + 6) << 8 | *(sourceP + 7);

			*(targetP + 5) = *(map + (value1 / U85P4).Mod85());
			*(targetP + 6) = *(map + (value1 / U85P3).Mod85());
			*(targetP + 7) = *(map + (value1 / U85P2).Mod85());
			*(targetP + 8) = *(map + (value1 / U85P1).Mod85());
			*(targetP + 9) = *(map + value1.Mod85());

			var value2 = (uint)*(sourceP + 8) << 24 | (uint)*(sourceP + 9) << 16 | (uint)*(sourceP + 10) << 8 | *(sourceP + 11);

			*(targetP + 10) = *(map + (value2 / U85P4).Mod85());
			*(targetP + 11) = *(map + (value2 / U85P3).Mod85());
			*(targetP + 12) = *(map + (value2 / U85P2).Mod85());
			*(targetP + 13) = *(map + (value2 / U85P1).Mod85());
			*(targetP + 14) = *(map + value2.Mod85());
		}
	}

	public static unsafe void Encode(ReadOnlySpan<byte> source, Span<byte> target)
	{
		fixed (byte* sourceP = source)
		fixed (byte* targetP = target)
		fixed (byte* map = _byte2Byte)
		{
			var value0 = (uint)*(sourceP + 0) << 24 | (uint)*(sourceP + 1) << 16 | (uint)*(sourceP + 2) << 8 | *(sourceP + 3);

			*(targetP + 0) = *(map + (value0 / U85P4).Mod85());
			*(targetP + 1) = *(map + (value0 / U85P3).Mod85());
			*(targetP + 2) = *(map + (value0 / U85P2).Mod85());
			*(targetP + 3) = *(map + (value0 / U85P1).Mod85());
			*(targetP + 4) = *(map + value0.Mod85());

			var value1 = (uint)*(sourceP + 4) << 24 | (uint)*(sourceP + 5) << 16 | (uint)*(sourceP + 6) << 8 | *(sourceP + 7);

			*(targetP + 5) = *(map + (value1 / U85P4).Mod85());
			*(targetP + 6) = *(map + (value1 / U85P3).Mod85());
			*(targetP + 7) = *(map + (value1 / U85P2).Mod85());
			*(targetP + 8) = *(map + (value1 / U85P1).Mod85());
			*(targetP + 9) = *(map + value1.Mod85());

			var value2 = (uint)*(sourceP + 8) << 24 | (uint)*(sourceP + 9) << 16 | (uint)*(sourceP + 10) << 8 | *(sourceP + 11);

			*(targetP + 10) = *(map + (value2 / U85P4).Mod85());
			*(targetP + 11) = *(map + (value2 / U85P3).Mod85());
			*(targetP + 12) = *(map + (value2 / U85P2).Mod85());
			*(targetP + 13) = *(map + (value2 / U85P1).Mod85());
			*(targetP + 14) = *(map + value2.Mod85());
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static unsafe byte Decode1(byte* map, char c) => *(map + (byte)c);

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static uint Mod85(this uint value) => value - (uint)((value * 3233857729uL) >> 38) * 85;
}