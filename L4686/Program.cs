using System;
using System.Linq;
using System.Collections.Generic;

namespace L4686
{
    class Program
    {
        static void Main(string[] args)
        {
            // ------------------------------------------------------------------------------------------------------------------
            // -- Initialise everything in sight/site
            // -- Create list of ALL triangular numbers

            var allTriangular = new List<int>();
            var triangular1to6 = new List<int>();
            for (int i = 1; i * (i + 1) / 2 < 10000000; i++)
            {
                int tri = i * (i + 1) / 2;
                allTriangular.Add(tri);
                if (ValidateNumber(tri))
                    triangular1to6.Add(tri);
            }

            // -- Create list of 4 digit primes containing 1-6 only

            List<int> FourDigitPrimes1to6 = new List<int>();
            foreach (var prime in Primes.AllPrimes)
                if (prime > 1000 && prime < 7000 && ValidateNumber(prime))
                    FourDigitPrimes1to6.Add(prime);

            // -- Create List of 4 digit squares

            List<int> FourDigitSquares = new List<int>();
            for (int i = 32; i * i < 10000; i++)
                if (ValidateNumber(i * i))
                    FourDigitSquares.Add(i * i);

            // -- Create lists of 2-digit and 4-digit powers

            HashSet<int> TwoDigitPower = new HashSet<int>();
            HashSet<int> FourDigitPower = new HashSet<int>();
            for (int i = 2; i * i * i < 6667; i++)
            {
                int power = i * i * i;
                do
                {
                    if (ValidateNumber(power))
                    {
                        if (power >= 1000)
                        {
                            FourDigitPower.Add(power);
                        }
                        if (power < 67)
                        {
                            TwoDigitPower.Add(power);
                        }
                    }
                    power *= i;
                } while (power < 6667);
            }

            // ------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------------
            // ------------------------------------------------------------------------------------------------------------------
            // --  7dn : Power (2)

            var poss_dn07 = new AnswerList();
            var poss_ac04 = new AnswerList();

            // -- 1st digit of 7dn is same as last digit of 4ac
            foreach (int work_dn07 in TwoDigitPower)
            {
                foreach (int work_ac04 in FourDigitPower)
                {
                    if (digitAtPosition(work_dn07, 0) == digitAtPosition(work_ac04, 3))
                    {
                        poss_dn07.Add(work_dn07);
                        poss_ac04.Add(work_ac04);
                    }
                }
            }
            var dn07 = DisplayValuesAndAssign(" 7dn", poss_dn07);

            // ------------------------------------------------------------------------------------------------------------------
            // --   6dn : 7dn + 18dn (4)
            // --  18dn : Power (4)
            // --   4ac : Power (4)

            var poss_dn06 = new AnswerList();
            var poss_dn18 = new AnswerList();

            foreach (var work_dn18 in FourDigitPower)
            {
                var work_dn06 = dn07 + work_dn18;
                if (ValidateNumber(work_dn06))
                {
                    poss_dn06.Add(work_dn06);
                    poss_dn18.Add(work_dn18);
                }
            }
            var dn18 = DisplayValuesAndAssign("18dn", poss_dn18);
            FourDigitPower.Remove(dn18);

            var dn06 = DisplayValuesAndAssign(" 6dn", poss_dn06);

            poss_ac04.OnlyIncludeDigitAtPosition(digitAtPosition(dn06, 0), 2);
            var ac04 = DisplayValuesAndAssign(" 4ac", poss_ac04);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 14ac : Triangular number (2)

            var poss_ac14 = new AnswerList();
            int digit = digitAtPosition(dn06, 2);
            for (int i = 0; allTriangular[i] < 100; i++)
            {
                if (allTriangular[i] > 10 && digitAtPosition(allTriangular[i], 0) == digit)
                    poss_ac14.Add(allTriangular[i]);
            }
            var ac14 = DisplayValuesAndAssign("14ac", poss_ac14);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 15dn : Square (4)

            var poss_dn15 = new AnswerList();
            digit = digitAtPosition(ac14, 1);
            foreach (var work_dn15 in FourDigitSquares)
            {
                if (digitAtPosition(work_dn15, 0) == digit)
                    poss_dn15.Add(work_dn15);
            }
            var dn15 = DisplayValuesAndAssign("15dn", poss_dn15);

            FourDigitSquares.Remove(dn15);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 11dn : Twice a Square (3)

            var poss_dn11 = new AnswerList();
            int ind = 8; // start at 2*8*8 = 128
            while (2 * ind * ind < 667)
            {
                if (ValidateNumber(2 * ind * ind))
                {
                    poss_dn11.Add(2 * ind * ind);
                }
                ind++;
            }

            // ------------------------------------------------------------------------------------------------------------------
            // -- 18ac : Triangular number (5)

            var poss_ac18 = new AnswerList();

            // -- we know we have a definite last digit, but make this search generic

            int lastdigitof_dn11 = digitAtPosition(poss_dn11.GetAnswers()[0].GetValue(), 2);
            foreach (var a in poss_dn11.GetAnswers())
                if (digitAtPosition(a.GetValue(), 2) != lastdigitof_dn11)
                    lastdigitof_dn11 = 0;

            if (lastdigitof_dn11 != 0)
            {
                ind = 0;
                while (triangular1to6[ind] <= 70000)
                {
                    int work_ac18 = triangular1to6[ind++];

                    if (work_ac18 < 10000) continue;
                    if (digitAtPosition(work_ac18, 0) != digitAtPosition(dn18, 0)) continue;
                    if (digitAtPosition(work_ac18, 2) != lastdigitof_dn11) continue;

                    poss_ac18.Add(work_ac18);
                }
            }
            var ac18 = DisplayValuesAndAssign("18ac", poss_ac18);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 8ac : Triangular number - 18ac (4)

            var poss_ac08 = new AnswerList();
            foreach (var work_ac18 in poss_ac18.GetAnswers())
            {
                ind = 0;
                var work_ac08 = allTriangular[ind++] - work_ac18.GetValue();
                while (work_ac08 < 6667)
                {
                    if (ValidateNumber(work_ac08) && work_ac08 > 1110)
                    {
                        poss_ac08.Add(work_ac08);
                    }
                    work_ac08 = allTriangular[ind++] - work_ac18.GetValue();
                }
            }

            // ------------------------------------------------------------------------------------------------------------------
            // -- 12ac : Even Square (3)
            // --  9dn : Triangular number (7)

            var poss_ac12 = new AnswerList();
            var poss_dn09 = new AnswerList();

            for (int i = 12; i * i < 700; i += 2)
            {
                if (ValidateNumber(i * i))
                    poss_ac12.Add(i * i);
            }

            foreach (int work_dn09 in allTriangular.Where(x => x > 1114110))
            {
                if (!ValidateNumber(work_dn09)) continue;

                foreach (var work_ac08 in poss_ac08.GetAnswers())
                {
                    if (digitAtPosition(work_dn09, 0) != digitAtPosition(work_ac08.GetValue(), 1)) continue;
                    if (digitAtPosition(work_dn09, 3) != digitAtPosition(ac18, 3)) continue;
                    //-- note that 25 across is PRIME therefore 6th digit is 1 or 3
                    if (!"13".Contains(work_dn09.ToString()[5])) continue;

                    poss_dn09.Add(work_dn09);
                }
            }

            var poss_digits = new List<int>();
            foreach (var work_dn09 in poss_dn09.GetAnswers())
            {
                digit = digitAtPosition(work_dn09.GetValue(), 1);
                foreach (var work_ac12 in poss_ac12.GetAnswers())
                {
                    if (digit != digitAtPosition(work_ac12.GetValue(), 0)) continue;
                    if (poss_digits.Contains(digit)) continue;
                    poss_digits.Add(digit);
                }
            }
            if (poss_digits.Count == 1)
            {
                poss_dn09.OnlyIncludeDigitAtPosition(poss_digits[0], 1);
                poss_ac12.OnlyIncludeDigitAtPosition(poss_digits[0], 0);
            }
            var dn09 = DisplayValuesAndAssign(" 9dn", poss_dn09);
            var ac12 = DisplayValuesAndAssign("12ac", poss_ac12);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 8ac : Triangular number - 18ac (4)

            poss_ac08.OnlyIncludeDigitAtPosition(digitAtPosition(dn09, 0), 1);
            var ac08 = DisplayValuesAndAssign(" 8ac", poss_ac08);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 17ac : Multiple of 7dn (4)

            int remainder = digitAtPosition(dn06, 3) * 10 + digitAtPosition(dn15, 1);
            var poss_ac17 = new AnswerList();
            int work_ac17 = 10 * dn07;
            while (work_ac17 < 6667)
            {
                work_ac17 += dn07;
                if (work_ac17 < 1111) continue;
                if (!ValidateNumber(work_ac17)) continue;
                if (work_ac17 % 100 != remainder) continue;
                poss_ac17.Add(work_ac17);
            }

            // ------------------------------------------------------------------------------------------------------------------
            // -- 3dn : Product of three distinct primes (2);

            int[] tinyPrimes = { 2, 3, 5, 7, 11, 13 };
            int tinyLen = tinyPrimes.Length;

            var poss_dn03 = new AnswerList();
            for (int i = 0; i < tinyLen - 2; i++)
            {
                for (int j = i + 1; j < tinyLen - 1; j++)
                {
                    for (int k = j + 1; k < tinyLen; k++)
                    {
                        int product = tinyPrimes[i] * tinyPrimes[j] * tinyPrimes[k];
                        if (product > 100) continue;
                        if (!ValidateNumber(product)) continue;
                        if (digitAtPosition(product, 1) != digitAtPosition(ac08, 0)) continue;
                        poss_dn03.Add(product);
                    }
                }
            }
            var dn03 = DisplayValuesAndAssign(" 3dn", poss_dn03);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 13dn : Square (7)

            var dig2 = new HashSet<int>();

            var poss_dn13 = new AnswerList();
            for (int i = 1000; i * i < 6666667; i++)
            {
                int work_dn13 = i * i;
                if (!ValidateNumber(work_dn13)) continue;

                if (digitAtPosition(ac12, 1) != digitAtPosition(work_dn13, 0)) continue;
                if (digitAtPosition(ac18, 4) != digitAtPosition(work_dn13, 2)) continue;
                poss_dn13.Add(work_dn13);
                dig2.Add(digitAtPosition(work_dn13, 1));
            }
            //var dn13 = DisplayValuesAndAssign("13dn", poss_dn13);

            poss_ac17.OnlyIncludeDigitAtPosition(dig2, 0);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 5dn : Product of three distinct primes (5)

            var poss_dn05 = new AnswerList();
            for (int i = 0; Primes.AllPrimes[i] < 40; i++)
            {
                int p1 = Primes.AllPrimes[i];
                for (int j = i + 1; Primes.AllPrimes[j] * p1 < 1640; j++)
                {
                    int p2 = Primes.AllPrimes[j];
                    for (int k = j + 1; Primes.AllPrimes[k] * p1 * p2 < 66667; k++)
                    {
                        var p3 = Primes.AllPrimes[k];
                        int product = p1 * p2 * p3;
                        if (product > 10000 && product < 100000 && ValidateNumber(product))
                        {
                            if (digitAtPosition(product, 0) != digitAtPosition(ac04, 1)) continue;
                            if (digitAtPosition(product, 1) != digitAtPosition(ac08, 3)) continue;
                            if (digitAtPosition(product, 2) != digitAtPosition(ac12, 2)) continue;
                            poss_dn05.Add(product);
                        }
                    }
                }
            }

            dig2 = new HashSet<int>();
            foreach (var work in poss_ac17.GetAnswers())
            {
                dig2.Add(digitAtPosition(work.GetValue(), 1));
            }
            poss_dn05.OnlyIncludeDigitAtPosition(dig2, 3);
            var dn05 = DisplayValuesAndAssign(" 5dn", poss_dn05);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 29ac : Multiple of a power (2)
            // -- 19ac : Triangular number - 29ac (2)

            var poss_ac29 = new AnswerList();
            foreach (int root in new int[] { 2, 3, 5 })
            {
                var power = root * root * root;
                while (power < 100)
                {
                    var work_ac29 = 2 * power;
                    while (work_ac29 < 100)
                    {
                        if (ValidateNumber(work_ac29))
                            if (digitAtPosition(work_ac29, 0) == digitAtPosition(dn18, 3))
                                poss_ac29.Add(work_ac29);
                        work_ac29 += power;
                    }
                    power *= root;
                }
            }

            digit = digitAtPosition(dn05, 4);
            int triang = 0;
            var poss_ac19 = new AnswerList();
            foreach (var work_ac29 in poss_ac29.GetAnswers())
            {
                for (int i = 1; i < 7; i++)
                {
                    var work_ac19 = digit * 10 + i;

                    if (!allTriangular.Contains(work_ac19 + work_ac29.GetValue())) continue;
                    triang = work_ac19 + work_ac29.GetValue();
                    if (!ValidateNumber(work_ac19)) continue;
                    poss_ac19.Add(work_ac19);
                }
            }
            var ac19 = DisplayValuesAndAssign("19ac", poss_ac19);
            if (poss_ac19.GetAnswers().Count == 1)
            {
                poss_ac29 = new AnswerList();
                poss_ac29.Add(triang - ac19);
            }
            var ac29 = DisplayValuesAndAssign("29ac", poss_ac29);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 32dn : 19ac - 7dn (2)

            var poss_dn32 = new AnswerList();
            poss_dn32.Add(ac19 - dn07);
            var dn32 = DisplayValuesAndAssign("32dn", poss_dn32);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 25ac : Prime (4)
            // -- 35ac : Triangular number (4)
            // -- 26dn : Half of a triangular number (4)

            var poss_ac25 = new AnswerList();
            var poss_ac35 = new AnswerList();
            var poss_dn26 = new AnswerList();
            foreach (var tri in allTriangular.Where(x => x % 2 == 0 && x / 2 > 1000 && x / 2 < 6667))
            {
                var work_dn26 = tri / 2;
                if (!ValidateNumber(work_dn26)) continue;
                if (digitAtPosition(work_dn26, 1) != digitAtPosition(ac29, 1)) continue;

                foreach (var work_ac35 in triangular1to6.Where(x => x > 1000 && x < 6667))
                {
                    if (digitAtPosition(work_ac35, 0) != digitAtPosition(dn32, 1)) continue;
                    if (digitAtPosition(work_ac35, 1) != digitAtPosition(work_dn26, 3)) continue;

                    foreach (var work_ac25 in FourDigitPrimes1to6)
                    {
                        if (digitAtPosition(work_ac25, 0) != digitAtPosition(dn18, 2)) continue;
                        if (digitAtPosition(work_ac25, 1) != digitAtPosition(work_dn26, 0)) continue;
                        if (digitAtPosition(work_ac25, 3) != digitAtPosition(dn09, 5)) continue;

                        poss_ac25.Add(work_ac25);
                        poss_dn26.Add(work_dn26);
                        poss_ac35.Add(work_ac35);
                    }
                }
            }
            //var ac25 = DisplayValuesAndAssign("25ac", poss_ac25);
            //var ac35 = DisplayValuesAndAssign("35ac", poss_ac35);
            //var dn26 = DisplayValuesAndAssign("26dn", poss_dn26);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 21ac : Power (2) 
            // -- 22dn : Multiple of the square of 32dn (5)

            var sqr = dn32 * dn32;
            var work_dn22 = sqr;

            var poss_ac21 = new AnswerList();
            var poss_dn22 = new AnswerList();
            while (work_dn22 < 66667)
            {
                work_dn22 += sqr;

                if (work_dn22 < 11111) continue;
                if (!ValidateNumber(work_dn22)) continue;
                foreach (var work_ac25 in poss_ac25.GetAnswers())
                {
                    if (digitAtPosition(work_dn22, 1) != digitAtPosition(work_ac25.GetValue(), 2)) continue;

                    foreach (var work_ac21 in TwoDigitPower)
                    {
                        if (digitAtPosition(work_dn22, 0) == digitAtPosition(work_ac21, 1))
                        {
                            poss_ac21.Add(work_ac21);
                            poss_dn22.Add(work_dn22);
                        }
                    }
                }
            }
            var ac21 = DisplayValuesAndAssign("21ac", poss_ac21);
            var dn22 = DisplayValuesAndAssign("22dn", poss_dn22);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 25ac : Prime (4)

            poss_ac25.OnlyIncludeDigitAtPosition(digitAtPosition(dn22, 1), 2);
            var ac25 = DisplayValuesAndAssign("25ac", poss_ac25);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 35ac : Triangular number (4)

            poss_ac35.OnlyIncludeDigitAtPosition(digitAtPosition(dn22, 4), 2);
            var ac35 = DisplayValuesAndAssign("35ac", poss_ac35);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 26dn : Half of a triangular number (4)

            poss_dn26.OnlyIncludeDigitAtPosition(digitAtPosition(ac35, 1), 3);
            var dn26 = DisplayValuesAndAssign("26dn", poss_dn26);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 30ac : Square + 24dn (3)
            // -- 24dn : Sum of two consecutive squares (3)

            var poss_dn24 = new AnswerList();
            for (int i = 1; (i * i) + ((i + 1) * (i + 1)) < 700; i++)
            {
                int sum = (i * i) + ((i + 1) * (i + 1));
                if (sum > 100 && ValidateNumber(sum))
                    poss_dn24.Add(sum);
            }

            var poss_ac30 = new AnswerList();
            var next_dn24 = new AnswerList();
            foreach (var work_dn24 in poss_dn24.GetAnswers())
            {
                var work_ac30 = work_dn24.GetValue();
                for (int i = 1; work_ac30 + i * i < 667; i++)
                {
                    var n = work_ac30 + i * i;
                    if (n > 666) continue;
                    if (!ValidateNumber(n)) continue;
                    if (digitAtPosition(n, 0) != digitAtPosition(dn22, 2)) continue;
                    if (digitAtPosition(n, 1) != digitAtPosition(dn09, 6)) continue;

                    next_dn24.Add(work_dn24.GetValue());
                    poss_ac30.Add(n);
                }
            }
            poss_dn24 = next_dn24;
            var ac30 = DisplayValuesAndAssign("30ac", poss_ac30);
            //            var dn24 = DisplayValuesAndAssign("24dn", poss_dn24);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 13dn : Square (7)

            poss_dn13.OnlyIncludeDigitAtPosition(digitAtPosition(ac30, 2), 5);
            var dn13 = DisplayValuesAndAssign("13dn", poss_dn13);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 17ac : Multiple of 7dn (4)

            poss_ac17.OnlyIncludeDigitAtPosition(digitAtPosition(dn13, 1), 0);
            var ac17 = DisplayValuesAndAssign("17ac", poss_ac17);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 33ac : Multiple of 30ac (4)

            var poss_ac33 = new AnswerList();
            for (int i = 2; i * ac30 < 6667; i++)
            {
                int work_ac33 = i * ac30;
                if (work_ac33 < 1000) continue;
                if (!ValidateNumber(work_ac33)) continue;
                if (digitAtPosition(work_ac33, 0) != digitAtPosition(dn22, 3)) continue;
                if (digitAtPosition(work_ac33, 2) != digitAtPosition(dn13, 6)) continue;

                poss_ac33.Add(work_ac33);
            }
            var ac33 = DisplayValuesAndAssign("33ac", poss_ac33);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 34dn : Prime (2)

            var poss_dn34 = new AnswerList();
            foreach (var work_dn34 in Primes.AllPrimes.Where(x => x < 67))
            {
                if (work_dn34 < 10) continue;
                if (!ValidateNumber(work_dn34)) continue;
                if (digitAtPosition(work_dn34, 0) != digitAtPosition(ac33, 3)) continue;

                poss_dn34.Add(work_dn34);
            }
            var dn34 = DisplayValuesAndAssign("34dn", poss_dn34);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 23ac : Multiple of 32dn (5)

            var digits_dn24 = new HashSet<int>();
            foreach (var work_dn24 in poss_dn24.GetAnswers())
            {
                digits_dn24.Add(digitAtPosition(work_dn24.GetValue(), 0));
            }

            var poss_ac23 = new AnswerList();
            for (int i = 2; i * dn32 < 66667; i++)
            {
                var work_ac23 = i * dn32;
                if (work_ac23 < 10000) continue;
                if (!ValidateNumber(work_ac23)) continue;
                if (digitAtPosition(work_ac23, 0) != digitAtPosition(dn09, 4)) continue;
                if (digitAtPosition(work_ac23, 1) != digitAtPosition(dn13, 3)) continue;
                if (digitAtPosition(work_ac23, 4) != digitAtPosition(dn15, 3)) continue;
                poss_ac23.Add(work_ac23);

            }
            poss_ac23.OnlyIncludeDigitAtPosition(digits_dn24, 2);

            var digits_ac23 = new HashSet<int>();
            foreach (var work_ac23 in poss_ac23.GetAnswers())
            {
                digits_ac23.Add(digitAtPosition(work_ac23.GetValue(), 2));
            }
            poss_dn24.OnlyIncludeDigitAtPosition(digits_ac23, 0);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 16ac : Triangular number - 31ac (3)
            // -- 31ac : Square - 16ac (3)

            // -- ... therefore we are looking (initially) for a tri == square with three digits
            // -- ... thereafter the sum of 16ac/31ac == tri == square

            var sumAc31Ac16 = 0;
            var poss_ac31 = new AnswerList();
            var poss_ac16 = new AnswerList();
            for (int i = 11; i * i < 1400; i++)
            {
                int sum = i * i;
                if (allTriangular.Contains(sum))
                {
                    foreach (var work_dn24 in poss_dn24.GetAnswers())
                    {
                        var dig = digitAtPosition(work_dn24.GetValue(), 2);
                        for (int j = 11; j < 67; j++)
                        {
                            var work_ac31 = 100 * dig + j;
                            var work_ac16 = sum - work_ac31;
                            if (!ValidateNumber(work_ac31)) continue;
                            if (!ValidateNumber(work_ac16)) continue;

                            foreach (var work_dn11 in poss_dn11.GetAnswers())
                            {
                                if (digitAtPosition(work_ac16, 2) != digitAtPosition(work_dn11.GetValue(), 1)) continue;

                                poss_ac31.Add(work_ac31);
                                poss_ac16.Add(work_ac16);

                                sumAc31Ac16 = sum;
                            }
                        }
                    }
                }
            }
            //DisplayValuesAndAssign("16ac", poss_ac16);
            //DisplayValuesAndAssign("31ac", poss_ac31);

            poss_dn24.OnlyIncludeDigitAtPosition(poss_ac31, 0, 2);
            poss_ac23.OnlyIncludeDigitAtPosition(poss_dn24, 0, 2);

            var dn24 = DisplayValuesAndAssign("24dn", poss_dn24);
            var ac23 = DisplayValuesAndAssign("23ac", poss_ac23);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 20dn : Square (6)

            var poss_dn20 = new AnswerList();
            foreach (var ans in poss_ac31.GetAnswers())
            {
                for (int i = 1; i < 7; i++)
                    for (int j = 1; j < 7; j++)
                        for (int k = 1; k < 7; k++)
                        {
                            var work_dn20
                                = digitAtPosition(ac19, 1) * 100000
                                + digitAtPosition(ac23, 3) * 10000
                                + i * 1000
                                + digitAtPosition(ans.GetValue(), 1) * 100
                                + j * 10
                                + k;
                            int root = (int)Math.Sqrt(work_dn20);
                            if (root * root == work_dn20)
                                poss_dn20.Add(work_dn20);
                        }
            }
            var dn20 = DisplayValuesAndAssign("20dn", poss_dn20);

            // ------------------------------------------------------------------------------------------------------------------
            // --  1dn : Square (4)

            var poss_dn01 = new AnswerList();
            foreach (var work_dn01 in FourDigitSquares)
            {
                foreach (var work_ac16 in poss_ac16.GetAnswers())
                {
                    if (digitAtPosition(work_dn01, 3) != digitAtPosition(work_ac16.GetValue(), 0)) continue;
                    if (doesNotContainsDigit(work_dn01, digitAtPosition(dn34, 1))) continue;
                    if (doesNotContainsDigit(work_dn01, digitAtPosition(dn20, 5))) continue;

                    poss_dn01.Add(work_dn01);
                }
            }
            var dn01 = DisplayValuesAndAssign(" 1dn", poss_dn01);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 27ac : Square - 16ac (3)
            // -- 31ac : Square - 16ac (3)
            // -- 28dn : Square (4)
            // -- 16ac : Triangular number - 31ac (3)

            var poss_ac27 = new AnswerList();
            var poss_dn28 = new AnswerList();

            foreach (var work_dn28 in FourDigitSquares.Where(x => ValidateNumber(x)))
            {
                var lastdigitof28dn = digitAtPosition(work_dn28, 3);
                if (digitAtPosition(dn01, 0) != lastdigitof28dn
                    && digitAtPosition(dn01, 1) != lastdigitof28dn
                    && digitAtPosition(dn01, 2) != lastdigitof28dn
                    && digitAtPosition(dn01, 3) != lastdigitof28dn) continue;
                foreach (var work_ac31 in poss_ac31.GetAnswers())
                {
                    var work_ac16 = sumAc31Ac16 - work_ac31.GetValue();
                    if (digitAtPosition(work_ac31.GetValue(), 2) != digitAtPosition(work_dn28, 1)) continue;

                    var work_ac27
                        = digitAtPosition(dn24, 1) * 100
                        + digitAtPosition(dn20, 2) * 10
                        + digitAtPosition(work_dn28, 0);
                    int root = (int)Math.Sqrt(work_ac27 + work_ac16);
                    if (root * root - work_ac16 != work_ac27) continue;

                    root = (int)Math.Sqrt(work_ac31.GetValue() + work_ac16);
                    if (root * root - work_ac16 != work_ac31.GetValue()) continue;

                    poss_ac27.Add(work_ac27);
                    poss_dn28.Add(work_dn28);
                }
            }
            var ac27 = DisplayValuesAndAssign("27ac", poss_ac27);
            var dn28 = DisplayValuesAndAssign("28dn", poss_dn28);

            poss_ac31.OnlyIncludeDigitAtPosition(digitAtPosition(dn28, 1), 2);
            var ac31 = DisplayValuesAndAssign("31ac", poss_ac31);

            poss_ac16.OnlyIncludeDigitAtPosition(digitAtPosition(sumAc31Ac16 - ac31, 2), 2);
            var ac16 = DisplayValuesAndAssign("16ac", poss_ac16);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 11dn : Twice a square (3)

            poss_dn11.OnlyIncludeDigitAtPosition(poss_ac16, 2, 1);
            var dn11 = DisplayValuesAndAssign("11dn", poss_dn11);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 36ac : Jumble of 1dn (4)

            var poss_ac36 = new AnswerList();

            var work_str = dn01.ToString();
            int work_ac36
                = digitAtPosition(dn34, 1) * 100
                + digitAtPosition(dn20, 5) * 10
                + digitAtPosition(dn28, 3);

            foreach (var ch in (digitAtPosition(dn34, 1) * 100 + digitAtPosition(dn20, 5) * 10 + digitAtPosition(dn28, 3)).ToString())
            {
                work_str = work_str.Remove(work_str.IndexOf($"{ch}"), 1);
            }
            poss_ac36.Add(int.Parse(work_str) * 1000 + work_ac36);
            var ac36 = DisplayValuesAndAssign("ac36", poss_ac36);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 10ac : Multiple of 34dn (3)

            var poss_ac10 = new AnswerList();
            for (int i = 1; i < 7; i++)
            {
                var work_ac10 = digitAtPosition(dn01, 2) * 100 + i * 10 + digitAtPosition(dn11, 0);
                if (work_ac10 % dn34 != 0) continue;

                poss_ac10.Add(work_ac10);
            }
            var ac10 = DisplayValuesAndAssign("10ac", poss_ac10);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 1ac : Prime (4)
            // -- 2dn : Square (6)

            var poss_ac01 = new AnswerList();
            var poss_dn02 = new AnswerList();
            foreach (int work_ac01 in FourDigitPrimes1to6)
            {
                if (digitAtPosition(work_ac01, 0) != digitAtPosition(dn01, 0)) continue;
                if (digitAtPosition(work_ac01, 2) != digitAtPosition(dn03, 0)) continue;

                for (int i = 1; i < 7; i++)
                {
                    var work_dn02
                        = digitAtPosition(work_ac01, 1) * 100000
                        + i * 10000
                        + digitAtPosition(ac10, 1) * 1000
                        + digitAtPosition(ac16, 1) * 100
                        + digitAtPosition(ac18, 1) * 10
                        + digitAtPosition(ac21, 0);
                    var root = (int)Math.Sqrt(work_dn02);
                    if (work_dn02 - root * root != 0) continue;

                    poss_ac01.Add(work_ac01);
                    poss_dn02.Add(work_dn02);
                }
            }
            var ac01 = DisplayValuesAndAssign(" 1ac", poss_ac01);
            var db02 = DisplayValuesAndAssign(" 2dn", poss_dn02);
        }

        // ------------------------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------------------------------------------------------------
        private static bool doesNotContainsDigit(int number, int digit)
        {
            return (!number.ToString().Contains(digit.ToString()));
        }

        public static int digitAtPosition(int num, int pos)
        {
            return int.Parse($"{num.ToString()[pos]}");
        }

        static bool ValidateNumber(int num)
        {
            foreach (char d in num.ToString())
            {
                if (d > '6' || d == '0')
                    return false;
            }
            return true;
        }
        static int DisplayValuesAndAssign(string clue, AnswerList poss)
        {
            Console.WriteLine("------------------------------------------------------");
            if (poss.GetAnswers().Count != 1)
            {
                Console.WriteLine($"{clue} ----- {poss.GetAnswers().Count} possible values");
                if (poss.GetAnswers().Count < 6)
                {
                    foreach (var a in poss.GetAnswers())
                        Console.WriteLine($"---- {a}");
                }
            }
            else
            {
                Console.WriteLine($"{clue} {poss.GetAnswers()[0].GetValue()}");

            }

            if (poss.GetAnswers().Count != 1)
                return 0;
            else
                return poss.GetAnswers()[0].GetValue();
        }

    }
}
