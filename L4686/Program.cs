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
            // -- 3dn : Product of three distinct primes (2)

            int[] tinyPrimes = { 2, 3, 5, 7, 11, 13 };

            var poss_dn03 = new AnswerList();
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    for (int k = j + 1; k < 6; k++)
                    {
                        int product = tinyPrimes[i] * tinyPrimes[j] * tinyPrimes[k];
                        if (product < 100 && ValidateNumber(product))
                        {
                            poss_dn03.Add(product);
                        }
                    }
                }
            }
            //var dn03 = DisplayValuesAndAssign(" 3dn", poss_dn03);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 1ac : Prime (4)

            var poss_ac01 = new AnswerList();
            foreach (int work_ac01 in FourDigitPrimes1to6)
            {
                foreach (var work_dn03 in poss_dn03.GetAnswers())
                    if (digitAtPosition(work_ac01, 2) == digitAtPosition(work_dn03.GetValue(), 0))
                        poss_ac01.Add(work_ac01);
            }
            //var ac01 = DisplayValuesAndAssign(" 1ac", poss_ac01);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 7dn : Power (2)

            var poss_dn07 = new AnswerList();
            var poss_ac04 = new AnswerList();

            // -- 1st digit of 7dn is same as last digit of 4ac
            foreach (int work_dn07 in TwoDigitPower)
            {
                int dig1 = digitAtPosition(work_dn07, 0);
                foreach (int work_ac04 in FourDigitPower)
                {
                    if (dig1 == digitAtPosition(work_ac04, 3))
                    {
                        poss_dn07.Add(work_dn07);
                        poss_ac04.Add(work_ac04);
                    }
                }
            }
            var dn07 = DisplayValuesAndAssign(" 7dn", poss_dn07);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 6dn : 7dn + 18dn (4)

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
            var dn06 = DisplayValuesAndAssign(" 6dn", poss_dn06);
            var dn18 = DisplayValuesAndAssign("18dn", poss_dn18);

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

            // ------------------------------------------------------------------------------------------------------------------
            // -- 24dn : Sum of two consecutive squares (3)

            var poss_dn24 = new AnswerList();
            for (int i = 1; (i * i) + ((i + 1) * (i + 1)) < 700; i++)
            {
                int sum = (i * i) + ((i + 1) * (i + 1));
                if (sum > 100 && ValidateNumber(sum))
                    poss_dn24.Add(sum);
            }
            var dn24 = DisplayValuesAndAssign("24dn", poss_dn24);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 12ac : Even Square (3)

            var poss_ac12 = new AnswerList();
            for (int i = 12; i * i < 700; i += 2)
            {
                if (ValidateNumber(i * i))
                    poss_ac12.Add(i * i);
            }
//            var ac12 = DisplayValuesAndAssign("12ac", poss_ac12);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 29ac : Multiple of a power (2)

            var poss_ac29 = new AnswerList();
            digit = digitAtPosition(dn18, 3);
            foreach (int root in new int[] { 2, 3, 5 })
            {
                var power = root * root * root;
                while (power < 100)
                {
                    var work_ac29 = 2 * power;
                    while (work_ac29 < 100)
                    {
                        if (ValidateNumber(work_ac29))
                            if (digitAtPosition(work_ac29, 0) == digit)
                                poss_ac29.Add(work_ac29);
                        work_ac29 += power;
                    }
                    power *= root;
                }
            }
            var ac29 = DisplayValuesAndAssign("29ac", poss_ac29);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 19ac : Triangular number - 29ac (2)

            var poss_ac19 = new AnswerList();
            for (int i = 0; allTriangular[i] - ac29 < 70; i++)
            {
                int work_ac19 = allTriangular[i] - ac29;
                if (work_ac19 >= 10 && ValidateNumber(work_ac19))
                    poss_ac19.Add(work_ac19);
            }
            var ac19 = DisplayValuesAndAssign("19ac", poss_ac19);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 11dn : Twice a Square (3)

            var poss_dn11 = new AnswerList();
            int ind = 8;
            while (2 * ind * ind < 700)
            {
                if (ValidateNumber(2 * ind * ind))
                {
                    poss_dn11.Add(2 * ind * ind);
                }
                ind++;
            }
            var dn11 = DisplayValuesAndAssign("11dn", poss_dn11);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 18ac : Triangular number (5)

            int digit0 = digitAtPosition(dn18, 0);
            int digit2 = digitAtPosition(poss_dn11.GetAnswers()[0].GetValue(), 2);

            var poss_ac18 = new AnswerList();
            foreach (var a in poss_dn11.GetAnswers())
                if (digitAtPosition(a.GetValue(), 2) != digit2)
                    digit2 = 0; // this will cause an exception when they don't all have the same digit

            ind = 0;
            while (allTriangular[ind] <= 70000)
            {
                int work_ac18 = allTriangular[ind++];
                if (work_ac18 > 10000)
                    if (ValidateNumber(work_ac18))
                        if (digitAtPosition(work_ac18, 0) == digit0)
                            if (digitAtPosition(work_ac18, 2) == digit2)
                                poss_ac18.Add(work_ac18);
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
            //var ac08 = DisplayValuesAndAssign(" 8ac", poss_ac08);

            // ------------------------------------------------------------------------------------------------------------------
            //-- 9dn : Triangular number (7)

            var poss_dn09 = new AnswerList();
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

            poss_ac08.OnlyIncludeDigitAtPosition(digitAtPosition(poss_dn09.GetAnswers()[0].GetValue(), 0), 1);
            var ac08 = DisplayValuesAndAssign(" 8ac", poss_ac08);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 3dn : Product of three distinct primes (2)

            poss_dn03.OnlyIncludeDigitAtPosition(digitAtPosition(ac08, 0), 1);
            var dn03 = DisplayValuesAndAssign(" 3dn", poss_dn03);

            // ------------------------------------------------------------------------------------------------------------------
            // -- 1ac : Prime (4)

            poss_ac01.OnlyIncludeDigitAtPosition(digitAtPosition(dn03, 0), 2);
            var ac01 = DisplayValuesAndAssign(" 1ac", poss_ac01);

            // -- 9dn
            // -- 11ac
            //// ------------------------------------------------------------------------------------------------------------------
            //// -- 13dn : Square(7)
            //Console.WriteLine("--------------------------------------");
            //Console.WriteLine("13dn : Square(7)");
            //Console.WriteLine();

            //for (int i = 1000; i * i < 6666667; i++)
            //{
            //    int n = i * i;
            //    if (ValidateNumber(n))
            //    {
            //        if ("25".Contains(n.ToString()[0]))
            //            if (n.ToString()[2] == '5')
            //                Console.WriteLine(n);
            //    }
            //}
            //Console.WriteLine();

            ////-- 30ac : Square + 24dn (3)
            //Console.WriteLine("--------------------------------------");
            //Console.WriteLine("30ac : Square + 24dn");
            //Console.WriteLine();
            //int dig = 1;
            //while (dig * dig + d24 < 1000)
            //{
            //    int sum = dig * dig + d24;
            //    if (sum.ToString()[1] == '1')
            //        Console.WriteLine(sum);
            //    dig++;
            //}
            //int a30 = 313;

            //Console.WriteLine();
        }

        //-------------------------------------------------------
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
