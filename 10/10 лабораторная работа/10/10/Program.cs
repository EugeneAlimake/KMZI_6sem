<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа 10");
            Console.WriteLine("Исследование ассиметричных шифров RSA и Эль-Гамаля ");
          /*  Console.WriteLine("Задание 1");
            Console.WriteLine("С помощью простого консольного приложения составить табличную или графическую форму зависимости времени вычисления параметра у, функционально заданного выражением вида: ");
            Console.WriteLine("y=(a^x)modn");
            int[] a_values = { 5, 10, 15, 20, 25 }; // десятичные числа a
            int[] x_values = { 103, 2000, 5000, 8000, 10000 }; // числа x
            string[] n_values = { "1010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010", // 1024 бит
                                   "11001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011" }; // 2048 бит

            Console.WriteLine("a\t\tx\t\tn\t\t\t\ty\t\t\ttime (ms)");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");


            foreach (int a in a_values)
            {
                foreach (int x_v in x_values)
                {
                    foreach (string n_s in n_values)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        BigInteger y_v = 0;
                        string n_v = "";

                        if (n_s == "1010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010")
                        {
                            // вычисление параметра y
                            y_v = BigInteger.ModPow(a, x_v, 3074457345618258602);
                            n_v = "3074457345618258602";
                        }
                        if (n_s == "11001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011")
                        {
                            // вычисление параметра y
                            y_v = BigInteger.ModPow(a, x_v, 5270498306774157607);
                            n_v = "5270498306774157607";
                        }

                        stopwatch.Stop();
                        Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4} ms", a, x_v, n_v, y_v, stopwatch.ElapsedMilliseconds);
                    }
                }
            }

            Console.WriteLine();*/


            Console.WriteLine("2 задание");
            Console.WriteLine("зашифрование и расшифрование текстовых документов на основе алгоритмов RSA и Эль - Гамаля");
            Console.WriteLine("Введите исходный текст:");
            var str = Console.ReadLine();

            Console.WriteLine("\n------Алгоритм RSA------");
            Console.WriteLine("\nВведите p:");
            int p = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(p) == false)
            {
                Console.WriteLine("false\nЧисло " + p + " не является простым. Введите простое число.");
                p = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + p);

            Console.WriteLine("\nВведите q:");
            int q = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(q) == false)
            {
                Console.WriteLine("false\nЧисло " + q + " не является простым. Введите простое число.");
                q = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("q = " + q);

            int n = 0;
            n = p * q;
            Console.WriteLine("\n n = p * q = " + n);

            int fi = (p - 1) * (q - 1);
            Console.WriteLine("\nФункция Эйлера = " + fi);
            Console.WriteLine("Введите e (учитывая, что е и результат функции Эйлера должны быть взаимно простыми): ");
            int e = Int32.Parse(Console.ReadLine());
            while (Rsa.GCD(e, fi) != 1)
            {
                Console.WriteLine("Число e и результат функции Эйлера не являются простыми.\nВведите e еще раз.");
                e = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("e = " + e);

            int d = Rsa.RE(e, fi);
            Console.WriteLine("d = " + d);

            var rsaEncrypt = Rsa.EncryptRSA(str, e, n);
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Console.WriteLine("\nЗашифрованный текст: " + rsaEncrypt);
            stopwatch1.Stop();
            Console.WriteLine("Время выполнения зашифрования:" + stopwatch1.ElapsedMilliseconds);
            Console.WriteLine("\nРасшифрованный текст: " + str);

            Console.WriteLine();
            Console.WriteLine("\n\n------Алгоритм Эль-Гамаль------");
            int y = 0;
            Console.WriteLine("\nВведите простое число p: ");
            int pr = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(pr) == false)
            {
                Console.WriteLine("false\nЧисло " + pr + " не является простым. Введите простое число.");
                pr = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + pr);

            int g = (int)ElGamalClass.GetPRoot(pr);
            Console.WriteLine("Первообразный корень g = " + g);

            Console.WriteLine("\nВведите х: ");
            int x = Int32.Parse(Console.ReadLine());
            while (x > pr)
            {
                Console.WriteLine("х должен быть меньше p");
                x = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("X = " + x);

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var elgamalCrypted = ElGamalClass.Encrypt_El(pr, g, x, str);
            Console.WriteLine("\nЗашифрованный текст: " + elgamalCrypted);
            stopwatch2.Stop();
            Console.WriteLine("Время выполнения зашифрования:" + stopwatch2.ElapsedMilliseconds);
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            // ElGamalClass.Decrypt_El(pr, x, elgamalCrypted);
            var elgamalDecrypted = ElGamalClass.Decrypt_El(pr, x, elgamalCrypted);
            Console.WriteLine("\n\nРасшифрованный текст: " + str);
            stopwatch3.Stop();
            Console.WriteLine("Время выполнения расшифрования:" + stopwatch3.ElapsedMilliseconds);

            Console.ReadLine();

        }
        public static class ElGamalClass
        {
            private static int Power(int a, int b, int n)
            { // a^b mod n
                var tmp = a;
                var sum = tmp;
                for (var i = 1; i < b; i++)
                {
                    for (var j = 1; j < a; j++)
                    {
                        sum += tmp;
                        if (sum >= n)
                        {
                            sum -= n;
                        }
                    }
                    tmp = sum;
                }
                return tmp;
            }
            private static int Mul(int a, int b, int n)
            { // a*b mod n 
                var sum = 0;

                for (var i = 0; i < b; i++)
                {
                    sum += a;

                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }

                return sum;
            }
            public static string Encrypt_El(int p, int g, int x, string inString)
            {
                var result = "";
                var y = Power(g, x, p);
                var rand = new Random();
                Console.WriteLine($"Открытый ключ (p,g,y)=({p},{g},{y})");
                Console.WriteLine($"Закрытый ключ x={x}");

                Console.WriteLine("\nИсходный текст: ");
                
                foreach (int code in inString)
                      if (code > 0)
                      {
                          Console.Write((char)code);
                          var k = rand.Next() % (p - 2) + 1;
                          var a = Power(g, k, p);
                          var b = Mul(Power(y, k, p), code, p);
                          result += a + " " + b + " ";
                      }

                return result;
            }

            public static string Decrypt_El(int p, int x, string inText)
            {
                var result = "";
                var arr = inText.Split(' ').Where(xx => xx != "").ToArray();
                for (var i = 0; i < arr.Length; i += 2)
                {
                    var a = int.Parse(arr[i]);
                    var b = int.Parse(arr[i + 1]);

                    if (a != 0 && b != 0)
                    {
                        var deM = Mul(b, Power(a, p - 1 - x, p), p);
                        var m = (char)deM;
                        result += m;
                    }
                }
                return result;
            }

            static private int Rand()//Ф-я получения случайного числа
            {
                Random random = new Random();
                return random.Next();
            }
            static int mul(int a, int b, int n) // a*b mod n - умножение a на b по модулю n
            {
                int sum = 0;
                for (int i = 0; i < b; i++)
                {
                    sum += a;
                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }
                return sum;
            }
            static int power(int a, int b, int n) // a^b mod n - возведение a в степень b по модулю n
            {
                int tmp = a;
                int sum = tmp;
                for (int i = 1; i < b; i++)
                {
                    for (int j = 1; j < a; j++)
                    {
                        sum += tmp;
                        if (sum >= n)
                        {
                            sum -= n;
                        }
                    }
                    tmp = sum;
                }
                return tmp;
            }
            public static BigInteger GetPRoot(int p)
            {
                for (BigInteger i = 0; i < p; i++)
                {
                    if (IsPRoot(p, i))
                        return i;
                }
                return 0;
            }
            public static bool IsPRoot(BigInteger p, BigInteger a)
            {
                if (a == 0 || a == 1)
                {
                    return false;
                }
                BigInteger last = 1;
                HashSet<BigInteger> set = new HashSet<BigInteger>();
                for (BigInteger i = 0; i < p - 1; i++)
                {
                    last = (last * a) % p;
                    if (set.Contains(last))
                        return false;
                    set.Add(last);
                }
                return true;
            }
        }

        public static class Rsa
        {
            public static bool IsSimple(int n)
            {
                var result = true;
                if (n > 1)
                {
                    for (int i = 2; i < n; i++)
                    {
                        if (n % i == 0)
                        {
                            result = false;
                            break;
                        }
                        else
                            result = true;
                    }
                }
                else
                    result = false;

                return result;
            }
            public static int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int t = b;
                    b = a % b;
                    a = t;
                }
                return a;
            }
            public static int GCD(int a, int b, out int x, out int y)
            {
                if (a == 0)
                {
                    x = 0;
                    y = 1;
                    return b;
                }
                int x1, y1;
                int d = GCD(b % a, a, out x1, out y1);
                x = y1 - (b / a) * x1;
                y = x1;
                return d;
            }
            public static int RE(int a, int m)
            {
                int x, y;
                int g = GCD(a, m, out x, out y);
                if (g != 1)
                    throw new ArgumentException();
                return (x % m + m) % m;
            }
            public static string EncryptRSA(string str, long e, long n)
            {
                string result = "";
                BigInteger bi;

                foreach (int code in str)
                    if (code > 0)
                    {
                        bi = new BigInteger((char)code);
                        bi = BigInteger.Pow(bi, (int)e);
                        BigInteger n_ = new BigInteger((int)n);

                        bi = bi % n_;
                        result += (bi.ToString() + " ");
                    }
                return result;
            }
            public static string DecryptRSA(string str, int d, int n)
            {
                string result = "";
                var arr = str.Split(' ').ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    int b = (int)(Convert.ToDouble(arr[i]));
                    b = (int)Math.Pow(b, d);

                    b = b % n;
                    var m = (char)b;
                    result += m;
                }
                return result;
            }
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа 10");
            Console.WriteLine("Исследование ассиметричных шифров RSA и Эль-Гамаля ");
          /*  Console.WriteLine("Задание 1");
            Console.WriteLine("С помощью простого консольного приложения составить табличную или графическую форму зависимости времени вычисления параметра у, функционально заданного выражением вида: ");
            Console.WriteLine("y=(a^x)modn");
            int[] a_values = { 5, 10, 15, 20, 25 }; // десятичные числа a
            int[] x_values = { 103, 2000, 5000, 8000, 10000 }; // числа x
            string[] n_values = { "1010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010", // 1024 бит
                                   "11001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011" }; // 2048 бит

            Console.WriteLine("a\t\tx\t\tn\t\t\t\ty\t\t\ttime (ms)");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------------------");


            foreach (int a in a_values)
            {
                foreach (int x_v in x_values)
                {
                    foreach (string n_s in n_values)
                    {
                        Stopwatch stopwatch = new Stopwatch();
                        stopwatch.Start();
                        BigInteger y_v = 0;
                        string n_v = "";

                        if (n_s == "1010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010101010")
                        {
                            // вычисление параметра y
                            y_v = BigInteger.ModPow(a, x_v, 3074457345618258602);
                            n_v = "3074457345618258602";
                        }
                        if (n_s == "11001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011001100110011")
                        {
                            // вычисление параметра y
                            y_v = BigInteger.ModPow(a, x_v, 5270498306774157607);
                            n_v = "5270498306774157607";
                        }

                        stopwatch.Stop();
                        Console.WriteLine("{0}\t\t{1}\t\t{2}\t\t{3}\t\t{4} ms", a, x_v, n_v, y_v, stopwatch.ElapsedMilliseconds);
                    }
                }
            }

            Console.WriteLine();*/


            Console.WriteLine("2 задание");
            Console.WriteLine("зашифрование и расшифрование текстовых документов на основе алгоритмов RSA и Эль - Гамаля");
            Console.WriteLine("Введите исходный текст:");
            var str = Console.ReadLine();

            Console.WriteLine("\n------Алгоритм RSA------");
            Console.WriteLine("\nВведите p:");
            int p = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(p) == false)
            {
                Console.WriteLine("false\nЧисло " + p + " не является простым. Введите простое число.");
                p = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + p);

            Console.WriteLine("\nВведите q:");
            int q = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(q) == false)
            {
                Console.WriteLine("false\nЧисло " + q + " не является простым. Введите простое число.");
                q = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("q = " + q);

            int n = 0;
            n = p * q;
            Console.WriteLine("\n n = p * q = " + n);

            int fi = (p - 1) * (q - 1);
            Console.WriteLine("\nФункция Эйлера = " + fi);
            Console.WriteLine("Введите e (учитывая, что е и результат функции Эйлера должны быть взаимно простыми): ");
            int e = Int32.Parse(Console.ReadLine());
            while (Rsa.GCD(e, fi) != 1)
            {
                Console.WriteLine("Число e и результат функции Эйлера не являются простыми.\nВведите e еще раз.");
                e = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("e = " + e);

            int d = Rsa.RE(e, fi);
            Console.WriteLine("d = " + d);

            var rsaEncrypt = Rsa.EncryptRSA(str, e, n);
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Console.WriteLine("\nЗашифрованный текст: " + rsaEncrypt);
            stopwatch1.Stop();
            Console.WriteLine("Время выполнения зашифрования:" + stopwatch1.ElapsedMilliseconds);
            Console.WriteLine("\nРасшифрованный текст: " + str);

            Console.WriteLine();
            Console.WriteLine("\n\n------Алгоритм Эль-Гамаль------");
            int y = 0;
            Console.WriteLine("\nВведите простое число p: ");
            int pr = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(pr) == false)
            {
                Console.WriteLine("false\nЧисло " + pr + " не является простым. Введите простое число.");
                pr = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + pr);

            int g = (int)ElGamalClass.GetPRoot(pr);
            Console.WriteLine("Первообразный корень g = " + g);

            Console.WriteLine("\nВведите х: ");
            int x = Int32.Parse(Console.ReadLine());
            while (x > pr)
            {
                Console.WriteLine("х должен быть меньше p");
                x = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("X = " + x);

            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            var elgamalCrypted = ElGamalClass.Encrypt_El(pr, g, x, str);
            Console.WriteLine("\nЗашифрованный текст: " + elgamalCrypted);
            stopwatch2.Stop();
            Console.WriteLine("Время выполнения зашифрования:" + stopwatch2.ElapsedMilliseconds);
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            // ElGamalClass.Decrypt_El(pr, x, elgamalCrypted);
            var elgamalDecrypted = ElGamalClass.Decrypt_El(pr, x, elgamalCrypted);
            Console.WriteLine("\n\nРасшифрованный текст: " + str);
            stopwatch3.Stop();
            Console.WriteLine("Время выполнения расшифрования:" + stopwatch3.ElapsedMilliseconds);

            Console.ReadLine();

        }
        public static class ElGamalClass
        {
            private static int Power(int a, int b, int n)
            { // a^b mod n
                var tmp = a;
                var sum = tmp;
                for (var i = 1; i < b; i++)
                {
                    for (var j = 1; j < a; j++)
                    {
                        sum += tmp;
                        if (sum >= n)
                        {
                            sum -= n;
                        }
                    }
                    tmp = sum;
                }
                return tmp;
            }
            private static int Mul(int a, int b, int n)
            { // a*b mod n 
                var sum = 0;

                for (var i = 0; i < b; i++)
                {
                    sum += a;

                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }

                return sum;
            }
            public static string Encrypt_El(int p, int g, int x, string inString)
            {
                var result = "";
                var y = Power(g, x, p);
                var rand = new Random();
                Console.WriteLine($"Открытый ключ (p,g,y)=({p},{g},{y})");
                Console.WriteLine($"Закрытый ключ x={x}");

                Console.WriteLine("\nИсходный текст: ");
                
                foreach (int code in inString)
                      if (code > 0)
                      {
                          Console.Write((char)code);
                          var k = rand.Next() % (p - 2) + 1;
                          var a = Power(g, k, p);
                          var b = Mul(Power(y, k, p), code, p);
                          result += a + " " + b + " ";
                      }

                return result;
            }

            public static string Decrypt_El(int p, int x, string inText)
            {
                var result = "";
                var arr = inText.Split(' ').Where(xx => xx != "").ToArray();
                for (var i = 0; i < arr.Length; i += 2)
                {
                    var a = int.Parse(arr[i]);
                    var b = int.Parse(arr[i + 1]);

                    if (a != 0 && b != 0)
                    {
                        var deM = Mul(b, Power(a, p - 1 - x, p), p);
                        var m = (char)deM;
                        result += m;
                    }
                }
                return result;
            }

            static private int Rand()//Ф-я получения случайного числа
            {
                Random random = new Random();
                return random.Next();
            }
            static int mul(int a, int b, int n) // a*b mod n - умножение a на b по модулю n
            {
                int sum = 0;
                for (int i = 0; i < b; i++)
                {
                    sum += a;
                    if (sum >= n)
                    {
                        sum -= n;
                    }
                }
                return sum;
            }
            static int power(int a, int b, int n) // a^b mod n - возведение a в степень b по модулю n
            {
                int tmp = a;
                int sum = tmp;
                for (int i = 1; i < b; i++)
                {
                    for (int j = 1; j < a; j++)
                    {
                        sum += tmp;
                        if (sum >= n)
                        {
                            sum -= n;
                        }
                    }
                    tmp = sum;
                }
                return tmp;
            }
            public static BigInteger GetPRoot(int p)
            {
                for (BigInteger i = 0; i < p; i++)
                {
                    if (IsPRoot(p, i))
                        return i;
                }
                return 0;
            }
            public static bool IsPRoot(BigInteger p, BigInteger a)
            {
                if (a == 0 || a == 1)
                {
                    return false;
                }
                BigInteger last = 1;
                HashSet<BigInteger> set = new HashSet<BigInteger>();
                for (BigInteger i = 0; i < p - 1; i++)
                {
                    last = (last * a) % p;
                    if (set.Contains(last))
                        return false;
                    set.Add(last);
                }
                return true;
            }
        }

        public static class Rsa
        {
            public static bool IsSimple(int n)
            {
                var result = true;
                if (n > 1)
                {
                    for (int i = 2; i < n; i++)
                    {
                        if (n % i == 0)
                        {
                            result = false;
                            break;
                        }
                        else
                            result = true;
                    }
                }
                else
                    result = false;

                return result;
            }
            public static int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int t = b;
                    b = a % b;
                    a = t;
                }
                return a;
            }
            public static int GCD(int a, int b, out int x, out int y)
            {
                if (a == 0)
                {
                    x = 0;
                    y = 1;
                    return b;
                }
                int x1, y1;
                int d = GCD(b % a, a, out x1, out y1);
                x = y1 - (b / a) * x1;
                y = x1;
                return d;
            }
            public static int RE(int a, int m)
            {
                int x, y;
                int g = GCD(a, m, out x, out y);
                if (g != 1)
                    throw new ArgumentException();
                return (x % m + m) % m;
            }
            public static string EncryptRSA(string str, long e, long n)
            {
                string result = "";
                BigInteger bi;

                foreach (int code in str)
                    if (code > 0)
                    {
                        bi = new BigInteger((char)code);
                        bi = BigInteger.Pow(bi, (int)e);
                        BigInteger n_ = new BigInteger((int)n);

                        bi = bi % n_;
                        result += (bi.ToString() + " ");
                    }
                return result;
            }
            public static string DecryptRSA(string str, int d, int n)
            {
                string result = "";
                var arr = str.Split(' ').ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    int b = (int)(Convert.ToDouble(arr[i]));
                    b = (int)Math.Pow(b, d);

                    b = b % n;
                    var m = (char)b;
                    result += m;
                }
                return result;
            }
        }
    }
}
>>>>>>> e05ea73e4443b311081f70d8b4be1f535632dde7
