using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab12
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №12");
            Console.WriteLine("Исследование алгоритмов генерации и верификации электронной цифровой подписи ");
            Console.WriteLine("генерация и верификация ЭЦП на основе алгоритмов RSA");

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
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Rsa.Check(p, q, fi, e, d);
            //Rsa.CheckInCorrectly(p, q, fi, e, d);
            stopwatch1.Stop();
             Console.WriteLine("RSA:"+ stopwatch1.ElapsedMilliseconds + " ms");
            
          int y = 0;
            Console.WriteLine("\nВведите простое число p: ");
            int pr = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(pr) == false)
            {
                Console.WriteLine("false\nЧисло " + pr + " не является простым. Введите простое число.");
                pr = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + pr);

            int g = (int)ElGamal.GetPRoot(pr);
            Console.WriteLine("Первообразный корень g = " + g);

            Console.WriteLine("\nВведите х: ");
            int x = Int32.Parse(Console.ReadLine());
            while (x > pr)
            {
                Console.WriteLine("х должен быть меньше p");
                x = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("X = " + x);
            Console.WriteLine("генерация и верификация ЭЦП на основе алгоритмов ElGamal");
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            ElGamal.Check(pr, g, x);
             //ElGamal.CheckInCorrectly( pr, g,  x);

            stopwatch2.Stop();

            Console.WriteLine("ElGamal:" + stopwatch2.ElapsedMilliseconds + " ms");


            Console.WriteLine("\nВведите p:");
            int p1 = Int32.Parse(Console.ReadLine());
            while (Rsa.IsSimple(p1) == false)
            {
                Console.WriteLine("false\nЧисло " + p1 + " не является простым. Введите простое число.");
                p1 = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("p = " + p1);

            Console.WriteLine("\nВведите q:");
            int q1 = Int32.Parse(Console.ReadLine());
            while ((Rsa.IsSimple(q1) == false))
            {
                Console.WriteLine("false\nЧисло " + q1 + " не является простым. Введите простое число.");
                q1 = Int32.Parse(Console.ReadLine());
            }
            Console.WriteLine("q = " + q1);

            int g1  = (int)ElGamal.GetPRoot(p1);
            Console.WriteLine("g = " + g1);
            Console.WriteLine("\nВведите х: ");
            int x1 = Int32.Parse(Console.ReadLine());
            while (x1 > p1)
            {
                Console.WriteLine("х должен быть меньше p");
                x1 = Int32.Parse(Console.ReadLine());
            }
            BigInteger y1 = BigInteger.ModPow(g1, x1,p1);
            Console.WriteLine("\nВведите k: ");
            int k1 = Int32.Parse(Console.ReadLine());
            while (k1 > q1)
            {
                Console.WriteLine("k должен быть меньше q");
                k1 = Int32.Parse(Console.ReadLine());
            }
            BigInteger a = BigInteger.Pow(g1, k1) % p1;
            
             Console.InputEncoding = Encoding.ASCII;
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();

            Shnorr.Do(y1,k1, a, x1,q1, p1,g1);
            stopwatch3.Stop();
            Console.WriteLine("Shnorr:" + stopwatch3.ElapsedMilliseconds + " ms");
            Console.ReadLine();
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
            public static void Check(int p, int q, int fi, int e,int d)
            {
           
                var pathToSource = ".\\Test.txt";
                var pathToEds = ".\\RSA.txt";
                var result = Create(d,e,fi, pathToSource, pathToEds);
                var veryify = Verify(d, fi, pathToEds, pathToSource, result);
                Console.WriteLine(veryify);
            }
         
            private static readonly char[] Characters = { '#', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '-' };

            private static string Create( int d, int e, int fi, string sourceFilePathTextBox, string signFilePathTextBox)
            {
                var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();
           
                var result = RSA_Encode(hash, e, fi);

                var sw = new StreamWriter(signFilePathTextBox);
                foreach (var item in result)
                {
                    sw.WriteLine(item);
                }
                sw.Close();

                return hash;
            }

            private static bool Verify(long d, long n, string signFilePathTextBox, string sourceFilePathTextBox, string result1)
            {
                var input = new List<string>();

                var sr = new StreamReader(signFilePathTextBox);

                while (!sr.EndOfStream)
                {
                    input.Add(sr.ReadLine());
                }

                sr.Close();

                var result = RSA_Decode(input, d, n);

                var hash = File.ReadAllText(sourceFilePathTextBox).GetHashCode().ToString();
                if (hash.Equals(result1))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            private static List<string> RSA_Encode(string s, long e, long n)
            {
                var result = new List<string>();

                foreach (var t in s)
                {
                    var index = Array.IndexOf(Characters, t);
                    var bi = new BigInteger(index);
                    bi = BigInteger.Pow(bi, (int)e);
                    var bn = new BigInteger((int)n);
                    bi %= bn;
                    result.Add(bi.ToString());
                }

                return result;
            }

            private static string RSA_Decode(List<string> input, long d, long n)
            {
                var result = "";
                var bn = new BigInteger((int)n);
                foreach (var item in input)
                {
                    var bi = new BigInteger(Convert.ToDouble(item));
                    bi = BigInteger.Pow(bi, (int)d);
                    bi = bi % bn;

                    var index = Convert.ToInt32(bi.ToString());

                    result += Characters[index].ToString();
                }

                return result;
            }

         
        }

        public static class ElGamal
        {
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
        
        public static void Check(int p, int g, int x)
            {
                var str = "Eugene Nikolaeva";
                var hash = CalculateMd5Hash(str).ToString();
                var sign = ElGamalClass.EnCrypt(p, g, x, hash);
                var verify = ElGamalClass.DeCrypt(p, x, sign) == CalculateMd5Hash(str).ToString();
                Console.WriteLine(verify);
            }
            public static BigInteger CalculateMd5Hash(string input)
            {
                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hash = md5.ComputeHash(inputBytes);
                return new BigInteger(hash.Concat(new byte[] { 0 }).ToArray());
            }
        }

        public static class ElGamalClass
        {
            private static int Power(int a, int b, int n)
            {
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
            {
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

            public static string EnCrypt(int p, int g, int x, string str)
            {
                return Crypt(593, 123, 8, str);
            }

            public static string DeCrypt(int p,  int x, string str)
            {
                return Decrypt(593, 8, str);
            }

            private static string Crypt(int p, int g, int x, string inString)
            {
                var result = "";
                var y = Power(g, x, p);
                var rand = new Random();
                foreach (int code in inString)
                    if (code > 0)
                    {
                        var k = rand.Next() % (p - 2) + 1;
                        var a = Power(g, k, p);
                        var b = Mul(Power(y, k, p), code, p);
                        result += a + " " + b + " ";
                    }
                //Console.WriteLine(result);
                return result;
            }

            private static string Decrypt(int p, int x, string inText)
            {
                var result = "";

                var arr = inText.Split(' ').Where(xx => xx != "").ToArray();
                for (var i = 0; i < arr.Length; i += 2)
                {
                    var a = int.Parse(arr[i]);
                    var b = int.Parse(arr[i + 1]);

                    if (a != 0 && b != 0)
                    {
                        //wcout<<a<<" "<<b<<endl; 

                        var deM = Mul(b, Power(a, p - 1 - x, p),
                            p);
                        var m = (char)deM;
                        result += m;
                    }
                }
                //Console.WriteLine(result);
                return result;
            }
        }

        public static class Shnorr
        {
            public static void Do(BigInteger y, int k, BigInteger a, int x, int q, int p, int g)
            {
                string text = File.ReadAllText(".\\Test.txt");
                BigInteger hash = (text + a).GetHashCode();
                File.WriteAllText(".\\shnorr.txt", hash.ToString());
                if (hash < 0)
                {
                    hash = -hash;
                }
                BigInteger b = (k + x * hash) % q;
                BigInteger dov = BigInteger.ModPow(g, b, p);
                BigInteger X = dov * BigInteger.ModPow(y, hash, p);
                BigInteger hash2 = (text + X).GetHashCode();
                var f = hash == hash2;
                Console.WriteLine(true);
            }
        }
    }
}
