using System;
using System.Linq;
using System.Collections.Generic;

namespace L4686
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> Used = new List<int>();

            //-- Create list of triangular numbers
            List<int> Triangular = new List<int>();

            for (int i = 1; i * (i + 1) / 2 < 10000000; i++)
            {
                int tri = i * (i + 1) / 2;
                Triangular.Add(tri);
                //if ( tri > 1000000 && ValidateNumber(tri))
                //Console.WriteLine(tri);
            }

            //-- Create list of 4 digit primes containing 1-6 only
            List<int> FourDigitPrimes1to6 = new List<int>();

            foreach (var q in Primes.AllPrimes)
                if (ValidateNumber(q) && q > 1000 && q < 10000)
                    FourDigitPrimes1to6.Add(q);

            //-- Create lists of 2-digit and 4-digit powers
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
                        if (power < 67 && !TwoDigitPower.Contains(power))
                        {
                            TwoDigitPower.Add(power);
                        }
                    }
                    power *= i;
                } while (power < 6667);
            }

            //-- 7dn Power (2)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine(" 7dn Power (2)");
            Console.WriteLine(" 1st digit is same as last digit of 4ac");
            Console.WriteLine();

            foreach (int q in TwoDigitPower)
            {
                int dig1 = q / 10;
                foreach (int r in FourDigitPower)
                {
                    if (dig1 == r % 10)
                        Console.WriteLine($"7dn={q} 4ac={r}");
                }
            }
            Console.WriteLine();
            int d07 = 16;

            //-- 6dn : 7dn + 18db (4)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("6dn : 7dn + 18dn (4)");
            Console.WriteLine();
            foreach (var y in FourDigitPower)
            {
                if (ValidateNumber(d07 + y))
                {
                    Console.WriteLine($"6dn={d07 + y} 18dn={y}");
                }
            }
            int d06 = 3141;
            int d18 = 3125;
            int a04 = 1331;

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("14ac : Triangular number");
            Console.WriteLine();
            int digit = (d06 / 10) % 10;
            for (int i = 0; i < 10; i++)
            {
                if (Triangular[i] > 10 && Triangular[i] / 10 == digit)
                    Console.WriteLine(Triangular[i]);
            }
            int a14 = 45;

            Console.WriteLine("--------------------------------------");
            Console.WriteLine("15dn : Square (4)");
            Console.WriteLine();
            for (int i = 32; i * i < 6667; i++)
            {
                if (ValidateNumber(i * i))
                {
                    if ((i * i) / 1000 == a14 % 10)
                        Console.WriteLine($"15dn={i * i}");
                    else
                        Console.WriteLine($"     {i * i}");
                }
            }
            Console.WriteLine();
            int d15 = 5625;

            //-- 3dn - Product of three distinct primes
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("3dn : Product of three distinct primes (2)");
            Console.WriteLine();
            int[] p = { 2, 3, 5, 7, 11, 13 };

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    for (int k = j + 1; k < 6; k++)
                    {
                        int product = p[i] * p[j] * p[k];
                        if (product < 100 && ValidateNumber(product))
                            Console.WriteLine($"({p[i],2} * {p[j],2} * {p[k],2}) = {product}");
                    }
                }
            }
            Console.WriteLine();

            //-- 24dn - Sum of two consecutive squares
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("24dn - product of two consecutive squares");
            Console.WriteLine();
            for (int i = 0; i * i * (i + 1) * (i + 1) < 700; i++)
            {
                int sum = i * i * (i + 1) * (i + 1);
                if (sum >= 100 && ValidateNumber(sum))
                    Console.WriteLine(sum);
            }
            Console.WriteLine();
            int d24 = 144;

            //-- 12ac : Even Square
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("12ac : Even Square");
            Console.WriteLine();
            for (int i = 14; i * i < 1000; i += 2)
            {
                if (ValidateNumber(i * i))
                    Console.WriteLine(i * i);
            }
            Console.WriteLine();

            //-- 19ac : Triangular number - 29ac
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("19ac : Triangular number - 29ac");
            Console.WriteLine();
            for (int i = 0; i < 20; i++)
            {
                int tri = Triangular[i];
                if (tri - 54 > 10 && tri - 54 < 67)
                {
                    if (ValidateNumber(tri - 54))
                        Console.WriteLine($"29ac=54 19ac={tri - 54}");
                    if (ValidateNumber(tri - 56))
                        Console.WriteLine($"29ac=56 19ac={tri - 56}");
                }
            }
            Console.WriteLine();

            //-- 13dn : Square(7)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("13dn : Square(7)");
            Console.WriteLine();
            for (int i = 1000; i * i < 6666667; i++)
            {
                int n = i * i;
                if (ValidateNumber(n))
                {
                    if ("25".Contains(n.ToString()[0]))
                        if (n.ToString()[2] == '5')
                            Console.WriteLine(n);
                }
            }
            Console.WriteLine();

            //-- 11dn : Twice a Square (3)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("11dn : Twice a square (3)");
            Console.WriteLine();
            int w = 10;
            while (2 * w * w < 667)
            {
                if (ValidateNumber(2 * w * w))
                    Console.WriteLine($"11dn={2 * w * w}");
                w++;
            }
            Console.WriteLine();

            //-- 18ac : Triangular number (5)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("18ac : Triangular number (5)");
            Console.WriteLine("Last digit of 11dn is '2'");
            Console.WriteLine();
            w = 0;
            List<int> ac18 = new List<int>();
            while (Triangular[w] <= 36666)
            {
                int x = Triangular[w];
                if (x > 30000 && ValidateNumber(x))
                {
                    if ((x / 100) % 10 == 2)
                    {
                        ac18.Add(x);
                        Console.WriteLine($"18ac={x}");
                    }
                }
                w++;
            }
            Console.WriteLine();

            //-- 8ac : Triangular number - 18ac
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("8ac : Triangular number - 18ac");
            Console.WriteLine();
            foreach (int x in ac18)
            {
                int i = 0;
                while (Triangular[i] - x < 6667)
                {
                    if (Triangular[i] - x > 1110 && ValidateNumber(Triangular[i] - x))
                        if ("26".Contains((Triangular[i] - x).ToString()[0]))
                            Console.WriteLine($"8ac={Triangular[i] - x} 18ac={x}");
                    i++;
                }
            }
            Console.WriteLine();

            //-- 9dn : Triangular number (7)
            //-- note that 25 across is PRIME therefore 6th digit is 1 or 3
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("9dn : Triangular number (7)");
            Console.WriteLine();

            Console.WriteLine();
            foreach ( int tri in Triangular.Where(x=>x>1114110))
            {
                if ( ValidateNumber(tri))
                {
                    string s = tri.ToString();
                    if ( s[0] == '1' && s[3] == '4' && "13".Contains(s[5]) )
                        Console.WriteLine(tri);
                }
            }
            //-- 1ac - Prime (4)
            // third digit of this prime is same as first digit of 3dn - ie 4
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1ac - Prime (4)");
            Console.WriteLine();
            foreach (var q in FourDigitPrimes1to6)
            {
                int dig3 = (q / 10) % 10;
                if (dig3 == 4)
                    Console.WriteLine(q);
            }
            Console.WriteLine();

            //-- 30ac : Square + 24dn (3)
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("30ac : Square + 24dn");
            Console.WriteLine();
            int dig = 1;
            while ( dig*dig + d24 < 1000)
            {
                int sum = dig * dig + d24;
                if (sum.ToString()[1] == '1')
                    Console.WriteLine(sum);
                dig++;
            }
            int a30 = 313;

            Console.WriteLine();
        }

        //-------------------------------------------------------
        static bool ValidateNumber(int num)
        {
            foreach (char d in num.ToString())
            {
                if (d > '6' || d == '0')
                    return false;
            }
            return true;
        }
    }
}
