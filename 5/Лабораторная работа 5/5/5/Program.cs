using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    class Program
    {
       
        static void Main(string[] args)
        {
            RouteEncryption routeEncryption = new RouteEncryption();
            Console.WriteLine("ИССЛЕДОВАНИЕ КРИПТОГРАФИЧЕСКИХ ШИФРОВ НА ОСНОВЕ ПЕРЕСТАНОВКИ СИМВОЛОВ");
            Console.WriteLine();
            Console.WriteLine("1 задание");
            Console.WriteLine("1. Маршрутная перестановка (маршрут: запись – по строкам, считывание – по столбцам таблицы; параметры таблицы – по указанию преподавателя)");
            string file = "text.txt";
            var encoding = Encoding.GetEncoding(1251);
            string text="";

            using (StreamReader sr = new StreamReader(file, encoding))
            {
                text = sr.ReadToEnd();
            }
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            //Console.WriteLine(text);
            routeEncryption.SetKey(5, text.Length);
            var res = routeEncryption.Encrypt(text);
           // Console.WriteLine(res);
            stopwatch1.Stop();
            Console.WriteLine();
            Console.WriteLine("Time " + stopwatch1.ElapsedMilliseconds+" ms");
            Console.WriteLine();

            Console.WriteLine("Расшифровка");
            Stopwatch stopwatch2 = new Stopwatch();
            stopwatch2.Start();
            string res1 = routeEncryption.Decrypt(res);
            //Console.WriteLine(res1);
            stopwatch2.Stop();
            Console.WriteLine("Time " + stopwatch2.ElapsedMilliseconds + " ms");


            Console.WriteLine("2 задание");
            Console.WriteLine("2. Множественная перестановка, ключевые слова – собственные имя и фамилия");
            Stopwatch stopwatch3 = new Stopwatch();
            stopwatch3.Start();
            string encrypt1 = MultiplePermutationEncoding(text);
           // Console.WriteLine(encrypt1);
            stopwatch3.Stop();
            Console.WriteLine("Time " + stopwatch3.ElapsedMilliseconds + " ms");

            Console.WriteLine();
            Stopwatch stopwatch4 = new Stopwatch();
            stopwatch4.Start();
            string decrypt1 = MultiplePermutationDecoding(encrypt1);
            //Console.WriteLine(decrypt1);
            stopwatch4.Stop();
            Console.WriteLine("Time " + stopwatch4.ElapsedMilliseconds + " ms");

            Console.ReadLine();
        }
        class RouteEncryption
        {
            private int s;
            private int k;
            char[,] table;
            public void SetKey(int countString, int lengthMessage)
            {
                s = countString;
                k = (lengthMessage - 1) / countString + 1;
            }
            public string Encrypt(string input)
            {
                var map1 = new Dictionary<char, int>();
                foreach (char c in input)
                {
                    if (!map1.ContainsKey(c))
                        map1.Add(c, 1);
                    else
                        map1[c] += 1;
                }
                int len1 = input.Length;
                int l = 0;
                string result = "";
                table = new char[k, s];
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < s; j++)
                    {
                        if (l < input.Length)
                        {
                            table[i, j] = input[l];
                            l++;
                        }
                        else
                        {
                            table[i, j] = ' ';
                        }
                    }
                }
                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        result += table[j, i];
                    }
                }
                var map2 = new Dictionary<char, int>();
                foreach (char c in result)
                {
                    if (!map2.ContainsKey(c))
                        map2.Add(c, 1);
                    else
                        map2[c] += 1;
                }
                int len2 = result.Length;
                return result;
            }
            public string Decrypt(string output)
            {
                int p = 0;
                string result = "";
                table = new char[k, s];
                for (int i = 0; i < s; i++)
                {
                    for (int j = 0; j < k; j++)
                    {
                        if (p < output.Length)
                        {
                            table[j, i] = output[p];
                            p++;
                        }
                        else
                        {
                            table[j, i] = ' ';
                        }

                    }
                }
                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < s; j++)
                    {
                        result += table[i, j];
                    }

                }
                return result;
            }
        }
      
         private static string MultiplePermutationEncoding(string text)
         {
             string secretKey1 = "Женя";
             string secretKey2 = "Нiкалаева";
             string result = "";
             int n = secretKey1.Length, m = secretKey2.Length;

             var key1sorted = secretKey1.ToCharArray().OrderBy(x => x.ToString());
             var key2sorted = secretKey2.ToCharArray().OrderBy(x => x.ToString());

             var separatedText = Split(text, m * n);

             foreach (var substring in separatedText)
             {
                 var localResult = substring;

                 int temp = 0;
                 char lastch = '`';
                 var locseckey1 = secretKey1;
                 foreach (var ch in key1sorted)
                 {
                     if (lastch != ch)
                     {
                         SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                     }
                     else
                     {
                         SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                     }
                     lastch = ch;
                 }

                 temp = 0;
                 lastch = '`';
                 var locseckey2 = secretKey2;
                 foreach (var ch in key2sorted)
                 {
                     if (lastch != ch)
                     {
                         SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                     }
                     else
                     {
                         SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                     }
                     lastch = ch;
                 }

                 result += localResult;
             }


             return result;

         }
         private static string MultiplePermutationDecoding(string text)
         {
            string secretKey1 = "Женя";
            string secretKey2 = "Нiкалаева";
            string result = "";
             int n = secretKey1.Length, m = secretKey2.Length;

             var key1sorted = secretKey1.ToCharArray().OrderBy(x => x.ToString());
             var key2sorted = secretKey2.ToCharArray().OrderBy(x => x.ToString());

             var separatedText = Split(text, m * n);

             foreach (var substring in separatedText)
             {
                 var localResult = substring;

                 int temp = 0;
                 char lastch = '`';
                 var locseckey1 = String.Concat(key1sorted.Where(c => key1sorted.Contains(c)));
                 foreach (var ch in secretKey1)
                 {
                     if (lastch != ch)
                     {
                         SwapRow(ref localResult, locseckey1.IndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey1, locseckey1.IndexOf(ch), temp++);
                     }
                     else
                     {
                         SwapRow(ref localResult, locseckey1.LastIndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey1, locseckey1.LastIndexOf(ch), temp++);
                     }
                     lastch = ch;
                 }

                 temp = 0;
                 lastch = '`';
                 var locseckey2 = String.Concat(key2sorted.Where(c => key2sorted.Contains(c)));
                 foreach (var ch in secretKey2)
                 {
                     if (lastch != ch)
                     {
                         SwapColumn(ref localResult, locseckey2.IndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey2, locseckey2.IndexOf(ch), temp++);
                     }
                     else
                     {
                         SwapColumn(ref localResult, locseckey2.LastIndexOf(ch), temp, n, m);
                         SwapCharacters(ref locseckey2, locseckey2.LastIndexOf(ch), temp++);
                     }
                     lastch = ch;
                 }
                 result += localResult;
             }
             return result.Replace('#', '\0'); ;
         }
         private static void SwapCharacters(ref string str, int poschar1, int poschar2)
         {
             var aStringBuilder = new StringBuilder(str);
             char ch1 = str[poschar1];
             char ch2 = str[poschar2];
             aStringBuilder.Remove(poschar1, 1);
             aStringBuilder.Insert(poschar1, ch2);
             aStringBuilder.Remove(poschar2, 1);
             aStringBuilder.Insert(poschar2, ch1);
             str = aStringBuilder.ToString();
         }


         private static void SwapColumn(ref string str, int column1, int column2, int n, int m)
         {
             for (int i = 0; i < n; i++)
             {
                 SwapCharacters(ref str, i * m + column1, i * m + column2);
             }
         }

         private static void SwapRow(ref string str, int row1, int row2, int n, int m)
         {
             for (int i = 0; i < m; i++)
             {
                 SwapCharacters(ref str, row1 * m + i, row2 * m + i);
             }
         }
         static IEnumerable<string> Split(string str, int chunkSize)
         {
             return Enumerable.Range(0, str.Length / chunkSize)
                 .Select(i => str.Substring(i * chunkSize, chunkSize));
         }
    }
}
