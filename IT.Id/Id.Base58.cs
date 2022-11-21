namespace System;

public readonly partial struct Id
{
    public String ToBase58()
    {
        var result = new string('\0', 17);

        unsafe
        {
            fixed (char* output = result)
            {
                ToBase58(output);
            }
        }

        return result;
    }

    private unsafe void ToBase58(Span<Char> destination)
    {
        fixed (char* output = destination)
        {
            ToBase58(output);
        }
    }

    private unsafe void ToBase58(char* output)
    {
        fixed (char* alphabetPtr = Base58._alphabet)
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
            int carry = (byte)(_timestamp >> 24);

            if (carry != 0)
            {
                carry = Math.DivRem(carry, 58, out r16);

                if (carry != 0)
                {
                    carry = Math.DivRem(carry, 58, out r15);
                    if (carry != 0) throw CarryException(1, 2, carry);
                    length = 2;
                }
                else length = 1;
            }

            //2
            carry = (byte)(_timestamp >> 16);

            if (carry != 0 || length > 0)
            {
                carry = Math.DivRem(carry + (r16 << 8), 58, out r16);

                if (carry != 0 || length > 1)
                {
                    carry = Math.DivRem(carry + (r15 << 8), 58, out r15);

                    if (carry != 0)
                    {
                        carry = Math.DivRem(carry + (r14 << 8), 58, out r14);
                        if (carry != 0) throw CarryException(2, 3, carry);
                        length = 3;
                    }
                    else length = 2;
                }
                else length = 1;
            }

            //3
            carry = (byte)(_timestamp >> 8);

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
                                if (carry != 0) throw CarryException(3, 5, carry);
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
            carry = (byte)_timestamp;

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
                                    if (carry != 0) throw CarryException(4, 6, carry);
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
            carry = (byte)(_b >> 24);

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
                                        if (carry != 0) throw CarryException(5, 7, carry);
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
            carry = (byte)(_b >> 16);

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
                                                if (carry != 0) throw CarryException(6, 9, carry);
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
            carry = (byte)(_b >> 8);

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
                                                    if (carry != 0) throw CarryException(7, 10, carry);
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
            carry = (byte)_b;

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
                                                        if (carry != 0) throw CarryException(8, 11, carry);
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
            carry = (byte)(_c >> 24);

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
                                                                if (carry != 0) throw CarryException(9, 13, carry);
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
            carry = (byte)(_c >> 16);

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
                                                                    if (carry != 0) throw CarryException(10, 14, carry);
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
            carry = (byte)(_c >> 8);

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
                                                                            if (carry != 0) throw CarryException(11, 16, carry);
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
            carry = (byte)_c;

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
                                                                                if (carry != 0) throw CarryException(12, 17, carry);
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

            *output = alphabetPtr[r0];
            *(output + 1) = alphabetPtr[r1];
            *(output + 2) = alphabetPtr[r2];
            *(output + 3) = alphabetPtr[r3];
            *(output + 4) = alphabetPtr[r4];
            *(output + 5) = alphabetPtr[r5];
            *(output + 6) = alphabetPtr[r6];
            *(output + 7) = alphabetPtr[r7];
            *(output + 8) = alphabetPtr[r8];
            *(output + 9) = alphabetPtr[r9];
            *(output + 10) = alphabetPtr[r10];
            *(output + 11) = alphabetPtr[r11];
            *(output + 12) = alphabetPtr[r12];
            *(output + 13) = alphabetPtr[r13];
            *(output + 14) = alphabetPtr[r14];
            *(output + 15) = alphabetPtr[r15];
            *(output + 16) = alphabetPtr[r16];
        }
    }

    private static Exception CarryException(Int32 bytes, Int32 length, Int32 carry)
        => new InvalidOperationException($"{bytes} bytes, {length} length, carry ({carry}) != 0");

    private static Id ParseBase58(ReadOnlySpan<Char> value)
    {
        var len = value.Length;
        if (len < 12 || len > 17) throw new ArgumentOutOfRangeException(nameof(value), len, "String must be 12 to 17 characters long");

        Span<Byte> bytes = stackalloc Byte[18];

        Base58.Decode(value, bytes, out var written);

        bytes = bytes.Slice(written - 12, 12);

        FromByteArray(bytes, 0, out var timestamp, out var b, out var c);

        return new Id(timestamp, b, c);
    }
}