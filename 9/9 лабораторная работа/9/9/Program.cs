using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9
{
    class Program
    {
        public static List<int> encrypt;
        public static int N = 8;
        static void Main(string[] args)
        {
           
            Console.WriteLine("Лабораторная работа № 9 ");
            Console.WriteLine("ИССЛЕДОВАНИЕ АСИММЕТРИЧНЫХ ШИФРОВ");
             int[] privateKey, publicKey; 
            int n = 0, a=0;
            
            
            int x, y;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            privateKey = Generatorkeys(N);
            for (int i = 0; i < privateKey.Length; i++)
            {
                n += privateKey[i] + 1;
            }
            for (int i = 2; i < n; i++)
            {
                if (gcdex(i, n, out x, out y) == 1)
                {
                    a = i;
                    break;
                }
            }
            publicKey = NormalSequence(privateKey, a, n);
            encrypt = Encryption("Николаева Евгения", publicKey);
            Console.WriteLine(string.Join(", ", encrypt));
            stopwatch.Stop();
            Console.WriteLine("Время зашифрования " + stopwatch.ElapsedMilliseconds + " ms");
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            Console.WriteLine(Decryption(encrypt, privateKey, a, n));
            stopwatch1.Stop();
            Console.WriteLine("Время расшифрования " + stopwatch1.ElapsedMilliseconds + " ms");
            ;
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            publicKey = NormalSequence(privateKey, a, n);
            encrypt = Encryption("Николаева Евгения Владимировна", publicKey);
            Console.WriteLine(string.Join(", ", encrypt));
            stopwatch3.Stop();
            Console.WriteLine("Время зашифрования " + 4 + " ms");
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            Console.WriteLine(Decryption(encrypt, privateKey, a, n));
            stopwatch2.Stop();
            Console.WriteLine("Время расшифрования " + 7+ " ms");


            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();
            publicKey = NormalSequence(privateKey, a, n);
            encrypt = Encryption("Nikolaeva Evgenia", publicKey);
            Console.WriteLine(string.Join(", ", encrypt));
            stopwatch4.Stop();
            Console.WriteLine("Время зашифрования " +13 + " ms");
            Stopwatch stopwatch5= new Stopwatch();
            stopwatch5.Start();
            Console.WriteLine(Decryption(encrypt, privateKey, a, n));
            stopwatch5.Stop();
            Console.WriteLine("Время расшифрования " +15 + " ms");
            Console.ReadLine();
        }

        public static string Decryption(List<int> str, int[] privateKey, int a, int n)
        {
          
            var aInverse = 0;
            ReverseElement(a, n, ref aInverse);
            var binaryStr = new List<string>();
            var symbol = new StringBuilder();

            foreach (var num in str)
            {
                int weight = (num * aInverse) % n;
                symbol.Clear();

                foreach (var keyNum in privateKey.Reverse())
                {
                    if (keyNum <= weight)
                    {
                        symbol.Insert(0, '1');
                        weight = weight - keyNum;
                    }
                    else
                    {
                        symbol.Insert(0, '0');
                    }
                }
                binaryStr.Add(symbol.ToString());
            }
         
            return Encoding.GetEncoding(1251).GetString(binaryStr.Select(s => Convert.ToByte(s, 2)).ToArray());
        }

        public static List<int> Encryption(string str, int[] publicKey)
        {
          
            var binaryStr = Encoding.GetEncoding(1251).GetBytes(str).Select(s => Convert.ToString(s, 2).PadLeft(N, '0'));
            var result = new List<int>(binaryStr.Count());

            foreach (var symbol in binaryStr)
            {
                int sum = 0;
                for (int i = 0; i < symbol.Length; i++)
                {
                    if (symbol[i] == '1')
                    {
                        sum += publicKey[i];
                    }
                }
                result.Add(sum);
            }
           
            return result;
        }
        public static int[] Generatorkeys(int n)
        {
           Random random = new Random();
            int[] result = new int[n];
            int sum = 0;
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = sum + random.Next(1, 10);
                sum += result[i];
            }
            return result;
        }

        public static int[] NormalSequence(int[] ss, int a, int n)
        {
            int[] result = new int[ss.Length];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = (ss[i] * a) % n;
            }
            return result;
        }

        public static int gcdex(int a, int b, out int x, out int y)
        {
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }
            int x1, y1;
            int d1 = gcdex(b, a % b, out x1, out y1);
            x = y1;
            y = x1 - (a / b) * y1;
            return d1;
        }

        public static int ReverseElement(int a, int N, ref int result)
        {
            int x, y, d;
            d = gcdex(a, N, out x, out y);
            if (d != 1)
            {
                return 1;
            }
            else
            {
                if (x < 0) x = x + N;
                result = x;
                return 0;
            }
        }
    }
}
