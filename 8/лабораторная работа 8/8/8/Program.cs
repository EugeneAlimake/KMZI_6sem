using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _8
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа №8");
            Console.WriteLine("ИССЛЕДОВАНИЕ ПОТОКОВЫХ ШИФРОВ");
            Console.WriteLine("Задание 1: Линейный конгруэнтный генератор с параметрами а = 421, с = 1663, n = 7875 ");
            LinearСongruentGenerator.Xt = 1234;
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            for (int i = 0; i < 20; i++)
            {
                
                for (int j = 0; j < 10;j++)
                {

                    Console.Write(Convert.ToString(LinearСongruentGenerator.Generator()+" "));

                }
            
            }
            Console.WriteLine("Time " + stopwatch1.ElapsedMilliseconds + " ms");
            Console.WriteLine("Задание 2: алгоритм RC4 с параметрами 13, 19, 90, 92, 240 ");
            var key = new byte[] { 13, 19, 90, 92, 240 };
            string encoded, text = "пупу";
                Console.WriteLine("До шифрования: "+text);
            encoded = RC4Crypt.Crypt(text, key);
            Console.WriteLine("После шифрования:"+encoded);
            Console.WriteLine("После дешифрования:" + RC4Crypt.Crypt(encoded, key));
            
            Console.ReadLine();

        }
    }
    public static class RC4Crypt
    {
        public static string Crypt(string data, byte[] key)
        {
            return Encoding.GetEncoding(1251).GetString(Crypt(Encoding.GetEncoding(1251).GetBytes(data), key));
        }
        public static byte[] Crypt(byte[] data, byte[] key)
        {
            int a, i, j, k, tmp;
            int[] box = new int[256];
            // int[] box = new int[192];
            byte[] result = new byte[data.Length];

            for (i = 0; i < box.Length; i++)
            {
                box[i] = i;
            }

            for (j = i = 0; i < box.Length; i++)
            {
                j = (j + box[i] + key[i % key.Length]) % box.Length;
                tmp = box[i];
                box[i] = box[j];
                box[j] = tmp;
            }

            for (a = j = i = 0; i < data.Length; i++)
            {
                a = a + 1;
                a = a % box.Length;
                j = j + box[a];
                j = j % box.Length;
                tmp = box[a];
                box[a] = box[j];
                box[j] = tmp;
                k = box[(box[a] + box[j]) % box.Length];
                result[i] = (byte)(data[i] ^ k);
            }

            return result;
        }
    }
    public static class LinearСongruentGenerator
    {
        private const int a = 421;
        private const int c = 1663;
        private const int n = 7875;
        private static int x_t = 1;

        public static int Xt{ set{x_t = value;}}
        public static int Generator()
        {
            x_t = (a * x_t + c) % n;
            return x_t;
        }
    }
}
