﻿using IT.Internal;
using System;
using System.Runtime.CompilerServices;

namespace IT;

public readonly partial struct Id
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe string ToBase58()
    {
        var base58 = new string('\0', 17);

        fixed (char* dest = base58)
        {
            ToBase58(dest);
        }

        return base58;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe bool TryToBase58(Span<char> chars)
    {
        if (chars.Length < 17) return false;

        fixed (char* dest = chars)
        {
            ToBase58(dest);
        }

        return true;
    }

    public unsafe bool TryToBase58(Span<byte> bytes)
    {
        if (bytes.Length < 17) return false;

        fixed (byte* dest = bytes)
        fixed (byte* map = Base58.EncodeMap)
        {
            int length = 0;

            int r16 = 0,
                r15 = 0,
                r14 = 0,
                r13 = 0,
                r12 = 0,
                r11 = 0,
                r10 = 0,
                r9 = 0,
                r8 = 0,
                r7 = 0,
                r6 = 0,
                r5 = 0,
                r4 = 0,
                r3 = 0,
                r2 = 0,
                r1 = 0,
                r0 = 0;

            //1
            int carry = _timestamp0;

            if (carry != 0)
            {
                carry = Math.DivRem(carry, 58, out r16);

                if (carry != 0)
                {
                    carry = Math.DivRem(carry, 58, out r15);
                    if (carry != 0) throw Ex.InvalidCarry(1, 2, carry);
                    length = 2;
                }
                else length = 1;
            }

            //2
            carry = _timestamp1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);
                        if (carry != 0) throw Ex.InvalidCarry(2, 3, carry);
                        length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //3
            carry = _timestamp2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);
                                if (carry != 0) throw Ex.InvalidCarry(3, 5, carry);
                                length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //4
            carry = _timestamp3;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);
                                    if (carry != 0) throw Ex.InvalidCarry(4, 6, carry);
                                    length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //5
            carry = _machine0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);
                                        if (carry != 0) throw Ex.InvalidCarry(5, 7, carry);
                                        length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //6
            carry = _machine1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);
                                                if (carry != 0) throw Ex.InvalidCarry(6, 9, carry);
                                                length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //7
            carry = _machine2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);
                                                    if (carry != 0) throw Ex.InvalidCarry(7, 10, carry);
                                                    length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //8
            carry = _pid0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);
                                                        if (carry != 0) throw Ex.InvalidCarry(8, 11, carry);
                                                        length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //9
            carry = _pid1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);
                                                                if (carry != 0) throw Ex.InvalidCarry(9, 13, carry);
                                                                length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //10
            carry = _increment0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);
                                                                    if (carry != 0) throw Ex.InvalidCarry(10, 14, carry);
                                                                    length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //11
            carry = _increment1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0 || length > 13)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);

                                                                    if (carry != 0 || length > 14)
                                                                    {
                                                                        carry = Math.DivRem(carry + (r2 << 8), 58, out r2);

                                                                        if (carry != 0)
                                                                        {
                                                                            carry = Math.DivRem(carry + (r1 << 8), 58, out r1);
                                                                            if (carry != 0) throw Ex.InvalidCarry(11, 16, carry);
                                                                            length = 16;
                                                                        }
                                                                        else length = 15;
                                                                    }
                                                                    else length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //12
            carry = _increment2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0 || length > 13)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);

                                                                    if (carry != 0 || length > 14)
                                                                    {
                                                                        carry = Math.DivRem(carry + (r2 << 8), 58, out r2);

                                                                        if (carry != 0 || length > 15)
                                                                        {
                                                                            carry = Math.DivRem(carry + (r1 << 8), 58, out r1);

                                                                            if (carry != 0)
                                                                            {
                                                                                carry = Math.DivRem(carry + (r0 << 8), 58, out r0);
                                                                                if (carry != 0) throw Ex.InvalidCarry(12, 17, carry);
                                                                                length = 17;
                                                                            }
                                                                            else length = 16;
                                                                        }
                                                                        else length = 15;
                                                                    }
                                                                    else length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            *dest = map[r0];
            *(dest + 1) = map[r1];
            *(dest + 2) = map[r2];
            *(dest + 3) = map[r3];
            *(dest + 4) = map[r4];
            *(dest + 5) = map[r5];
            *(dest + 6) = map[r6];
            *(dest + 7) = map[r7];
            *(dest + 8) = map[r8];
            *(dest + 9) = map[r9];
            *(dest + 10) = map[r10];
            *(dest + 11) = map[r11];
            *(dest + 12) = map[r12];
            *(dest + 13) = map[r13];
            *(dest + 14) = map[r14];
            *(dest + 15) = map[r15];
            *(dest + 16) = map[r16];
        }

        return true;
    }

    public static bool TryParseBase58(ReadOnlySpan<char> chars, out Id id)
    {
        var len = chars.Length;
        if (len < 12 || len > 17)
        {
            id = default;
            return false;
        }

        var map = Base58.DecodeMap;

        byte b0 = 0, b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0, b11 = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            char ch = chars[i];

            if (ch < Base58.Min || ch > Base58.Max)
            {
                id = default;
                return false;
            }

            int carry = map[ch];

            if (carry == 0xFF)
            {
                id = default;
                return false;
            }

            //1
            carry += 58 * b11;
            b11 = (byte)carry;

            //2
            carry = (carry >> 8) + (58 * b10);
            b10 = (byte)carry;

            //3
            carry = (carry >> 8) + (58 * b9);
            b9 = (byte)carry;

            //4
            carry = (carry >> 8) + (58 * b8);
            b8 = (byte)carry;

            //5
            carry = (carry >> 8) + (58 * b7);
            b7 = (byte)carry;

            //6
            carry = (carry >> 8) + (58 * b6);
            b6 = (byte)carry;

            //7
            carry = (carry >> 8) + (58 * b5);
            b5 = (byte)carry;

            //8
            carry = (carry >> 8) + (58 * b4);
            b4 = (byte)carry;

            //9
            carry = (carry >> 8) + (58 * b3);
            b3 = (byte)carry;

            //10
            carry = (carry >> 8) + (58 * b2);
            b2 = (byte)carry;

            //11
            carry = (carry >> 8) + (58 * b1);
            b1 = (byte)carry;

            //12
            b0 = (byte)((carry >> 8) + (58 * b0));
        }

        id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, b0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), b1);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), b2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), b3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), b4);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), b5);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), b6);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), b7);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), b8);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), b9);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), b10);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), b11);

        return true;
    }

    public static bool TryParseBase58(ReadOnlySpan<byte> bytes, out Id id)
    {
        var len = bytes.Length;
        if (len < 12 || len > 17)
        {
            id = default;
            return false;
        }

        var map = Base58.DecodeMap;

        byte b0 = 0, b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0, b11 = 0;

        for (int i = 0; i < bytes.Length; i++)
        {
            int carry = map[bytes[i]];

            if (carry == 0xFF)
            {
                id = default;
                return false;
            }

            //1
            carry += 58 * b11;
            b11 = (byte)carry;

            //2
            carry = (carry >> 8) + (58 * b10);
            b10 = (byte)carry;

            //3
            carry = (carry >> 8) + (58 * b9);
            b9 = (byte)carry;

            //4
            carry = (carry >> 8) + (58 * b8);
            b8 = (byte)carry;

            //5
            carry = (carry >> 8) + (58 * b7);
            b7 = (byte)carry;

            //6
            carry = (carry >> 8) + (58 * b6);
            b6 = (byte)carry;

            //7
            carry = (carry >> 8) + (58 * b5);
            b5 = (byte)carry;

            //8
            carry = (carry >> 8) + (58 * b4);
            b4 = (byte)carry;

            //9
            carry = (carry >> 8) + (58 * b3);
            b3 = (byte)carry;

            //10
            carry = (carry >> 8) + (58 * b2);
            b2 = (byte)carry;

            //11
            carry = (carry >> 8) + (58 * b1);
            b1 = (byte)carry;

            //12
            b0 = (byte)((carry >> 8) + (58 * b0));
        }

        id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, b0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), b1);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), b2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), b3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), b4);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), b5);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), b6);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), b7);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), b8);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), b9);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), b10);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), b11);

        return true;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseBase58(ReadOnlySpan<char> chars)
    {
        var len = chars.Length;
        if (len < 12 || len > 17) throw Ex.InvalidLengthChars(Idf.Base58, len, 12, 17);

        var map = Base58.DecodeMap;

        byte b0 = 0, b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0, b11 = 0;

        for (int i = 0; i < chars.Length; i++)
        {
            char ch = chars[i];

            if (ch < Base58.Min || ch > Base58.Max) throw Ex.InvalidChar(Idf.Base58, ch);

            int carry = map[ch];

            if (carry == 0xFF) throw Ex.InvalidChar(Idf.Base58, ch);

            //1
            carry += 58 * b11;
            b11 = (byte)carry;

            //2
            carry = (carry >> 8) + (58 * b10);
            b10 = (byte)carry;

            //3
            carry = (carry >> 8) + (58 * b9);
            b9 = (byte)carry;

            //4
            carry = (carry >> 8) + (58 * b8);
            b8 = (byte)carry;

            //5
            carry = (carry >> 8) + (58 * b7);
            b7 = (byte)carry;

            //6
            carry = (carry >> 8) + (58 * b6);
            b6 = (byte)carry;

            //7
            carry = (carry >> 8) + (58 * b5);
            b5 = (byte)carry;

            //8
            carry = (carry >> 8) + (58 * b4);
            b4 = (byte)carry;

            //9
            carry = (carry >> 8) + (58 * b3);
            b3 = (byte)carry;

            //10
            carry = (carry >> 8) + (58 * b2);
            b2 = (byte)carry;

            //11
            carry = (carry >> 8) + (58 * b1);
            b1 = (byte)carry;

            //12
            b0 = (byte)((carry >> 8) + (58 * b0));
        }

        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, b0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), b1);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), b2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), b3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), b4);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), b5);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), b6);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), b7);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), b8);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), b9);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), b10);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), b11);

        return id;
    }

    /// <exception cref="FormatException"/>
    public static Id ParseBase58(ReadOnlySpan<byte> bytes)
    {
        var len = bytes.Length;
        if (len < 12 || len > 17) throw Ex.InvalidLengthBytes(Idf.Base58, len, 12, 17);

        var map = Base58.DecodeMap;

        byte b0 = 0, b1 = 0, b2 = 0, b3 = 0, b4 = 0, b5 = 0, b6 = 0, b7 = 0, b8 = 0, b9 = 0, b10 = 0, b11 = 0;

        for (int i = 0; i < bytes.Length; i++)
        {
            int carry = map[bytes[i]];

            if (carry == 0xFF) throw Ex.InvalidByte(Idf.Base58, bytes[i]);

            //1
            carry += 58 * b11;
            b11 = (byte)carry;

            //2
            carry = (carry >> 8) + (58 * b10);
            b10 = (byte)carry;

            //3
            carry = (carry >> 8) + (58 * b9);
            b9 = (byte)carry;

            //4
            carry = (carry >> 8) + (58 * b8);
            b8 = (byte)carry;

            //5
            carry = (carry >> 8) + (58 * b7);
            b7 = (byte)carry;

            //6
            carry = (carry >> 8) + (58 * b6);
            b6 = (byte)carry;

            //7
            carry = (carry >> 8) + (58 * b5);
            b5 = (byte)carry;

            //8
            carry = (carry >> 8) + (58 * b4);
            b4 = (byte)carry;

            //9
            carry = (carry >> 8) + (58 * b3);
            b3 = (byte)carry;

            //10
            carry = (carry >> 8) + (58 * b2);
            b2 = (byte)carry;

            //11
            carry = (carry >> 8) + (58 * b1);
            b1 = (byte)carry;

            //12
            b0 = (byte)((carry >> 8) + (58 * b0));
        }

        Id id = default;

        ref var b = ref Unsafe.As<Id, byte>(ref id);

        Unsafe.WriteUnaligned(ref b, b0);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 1), b1);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 2), b2);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 3), b3);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 4), b4);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 5), b5);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 6), b6);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 7), b7);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 8), b8);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 9), b9);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 10), b10);
        Unsafe.WriteUnaligned(ref Unsafe.Add(ref b, 11), b11);

        return id;
    }

#if NETSTANDARD2_0

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseBase58(String? str, out Id id) => TryParseBase58(str.AsSpan(), out id);

    /// <exception cref="ArgumentNullException"/>
    /// <exception cref="FormatException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Id ParseBase58(String str) => ParseBase58((str ?? throw new ArgumentNullException(nameof(str))).AsSpan());

#endif

    private unsafe void ToBase58(char* dest)
    {
        fixed (char* map = Base58.Alphabet)
        {
            int length = 0;

            int r16 = 0,
                r15 = 0,
                r14 = 0,
                r13 = 0,
                r12 = 0,
                r11 = 0,
                r10 = 0,
                r9 = 0,
                r8 = 0,
                r7 = 0,
                r6 = 0,
                r5 = 0,
                r4 = 0,
                r3 = 0,
                r2 = 0,
                r1 = 0,
                r0 = 0;

            //1
            int carry = _timestamp0;

            if (carry != 0)
            {
                carry = Math.DivRem(carry, 58, out r16);

                if (carry != 0)
                {
                    carry = Math.DivRem(carry, 58, out r15);
                    if (carry != 0) throw Ex.InvalidCarry(1, 2, carry);
                    length = 2;
                }
                else length = 1;
            }

            //2
            carry = _timestamp1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);
                        if (carry != 0) throw Ex.InvalidCarry(2, 3, carry);
                        length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //3
            carry = _timestamp2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);
                                if (carry != 0) throw Ex.InvalidCarry(3, 5, carry);
                                length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //4
            carry = _timestamp3;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);
                                    if (carry != 0) throw Ex.InvalidCarry(4, 6, carry);
                                    length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //5
            carry = _machine0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);
                                        if (carry != 0) throw Ex.InvalidCarry(5, 7, carry);
                                        length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //6
            carry = _machine1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);
                                                if (carry != 0) throw Ex.InvalidCarry(6, 9, carry);
                                                length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //7
            carry = _machine2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);
                                                    if (carry != 0) throw Ex.InvalidCarry(7, 10, carry);
                                                    length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //8
            carry = _pid0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);
                                                        if (carry != 0) throw Ex.InvalidCarry(8, 11, carry);
                                                        length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //9
            carry = _pid1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);
                                                                if (carry != 0) throw Ex.InvalidCarry(9, 13, carry);
                                                                length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //10
            carry = _increment0;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);
                                                                    if (carry != 0) throw Ex.InvalidCarry(10, 14, carry);
                                                                    length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //11
            carry = _increment1;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0 || length > 13)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);

                                                                    if (carry != 0 || length > 14)
                                                                    {
                                                                        carry = Math.DivRem(carry + (r2 << 8), 58, out r2);

                                                                        if (carry != 0)
                                                                        {
                                                                            carry = Math.DivRem(carry + (r1 << 8), 58, out r1);
                                                                            if (carry != 0) throw Ex.InvalidCarry(11, 16, carry);
                                                                            length = 16;
                                                                        }
                                                                        else length = 15;
                                                                    }
                                                                    else length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //12
            carry = _increment2;

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0 || length > 2)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);

                        if (carry != 0 || length > 3)
                        {
                            carry = Math.DivRem(carry + (r13 << 8), 58, out r13);

                            if (carry != 0 || length > 4)
                            {
                                carry = Math.DivRem(carry + (r12 << 8), 58, out r12);

                                if (carry != 0 || length > 5)
                                {
                                    carry = Math.DivRem(carry + (r11 << 8), 58, out r11);

                                    if (carry != 0 || length > 6)
                                    {
                                        carry = Math.DivRem(carry + (r10 << 8), 58, out r10);

                                        if (carry != 0 || length > 7)
                                        {
                                            carry = Math.DivRem(carry + (r9 << 8), 58, out r9);

                                            if (carry != 0 || length > 8)
                                            {
                                                carry = Math.DivRem(carry + (r8 << 8), 58, out r8);

                                                if (carry != 0 || length > 9)
                                                {
                                                    carry = Math.DivRem(carry + (r7 << 8), 58, out r7);

                                                    if (carry != 0 || length > 10)
                                                    {
                                                        carry = Math.DivRem(carry + (r6 << 8), 58, out r6);

                                                        if (carry != 0 || length > 11)
                                                        {
                                                            carry = Math.DivRem(carry + (r5 << 8), 58, out r5);

                                                            if (carry != 0 || length > 12)
                                                            {
                                                                carry = Math.DivRem(carry + (r4 << 8), 58, out r4);

                                                                if (carry != 0 || length > 13)
                                                                {
                                                                    carry = Math.DivRem(carry + (r3 << 8), 58, out r3);

                                                                    if (carry != 0 || length > 14)
                                                                    {
                                                                        carry = Math.DivRem(carry + (r2 << 8), 58, out r2);

                                                                        if (carry != 0 || length > 15)
                                                                        {
                                                                            carry = Math.DivRem(carry + (r1 << 8), 58, out r1);

                                                                            if (carry != 0)
                                                                            {
                                                                                carry = Math.DivRem(carry + (r0 << 8), 58, out r0);
                                                                                if (carry != 0) throw Ex.InvalidCarry(12, 17, carry);
                                                                                length = 17;
                                                                            }
                                                                            else length = 16;
                                                                        }
                                                                        else length = 15;
                                                                    }
                                                                    else length = 14;
                                                                }
                                                                else length = 13;
                                                            }
                                                            else length = 12;
                                                        }
                                                        else length = 11;
                                                    }
                                                    else length = 10;
                                                }
                                                else length = 9;
                                            }
                                            else length = 8;
                                        }
                                        else length = 7;
                                    }
                                    else length = 6;
                                }
                                else length = 5;
                            }
                            else length = 4;
                        }
                        else length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            *dest = map[r0];
            *(dest + 1) = map[r1];
            *(dest + 2) = map[r2];
            *(dest + 3) = map[r3];
            *(dest + 4) = map[r4];
            *(dest + 5) = map[r5];
            *(dest + 6) = map[r6];
            *(dest + 7) = map[r7];
            *(dest + 8) = map[r8];
            *(dest + 9) = map[r9];
            *(dest + 10) = map[r10];
            *(dest + 11) = map[r11];
            *(dest + 12) = map[r12];
            *(dest + 13) = map[r13];
            *(dest + 14) = map[r14];
            *(dest + 15) = map[r15];
            *(dest + 16) = map[r16];
        }
    }
}