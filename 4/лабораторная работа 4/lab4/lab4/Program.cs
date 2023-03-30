using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("Лабораторная работа №4");
            Console.WriteLine("ИССЛЕДОВАНИЕ КРИПТОГРАФИЧЕСКИХ ШИФРОВ НА ОСНОВЕ ПОДСТАНОВКИ (ЗАМЕНЫ) СИМВОЛОВ ");
            Console.WriteLine();
            Console.WriteLine("1. Разработать авторское приложение в соответствии с цельюлабораторной работы.операции для реализации: ");
            Console.WriteLine("\nвыполнять зашифрование/расшифрование текстовых документов (объемом не менее 5 тысяч знаков)\nязыка в соответствии с нижеследующей таблицей вариантов задания;\nпри этом следует использовать шифры подстановки из третьего столбца данной таблицы");
            Console.WriteLine();
            Console.WriteLine("Язык : Белорусский");
            Console.WriteLine("1. Шифр Цезаря с ключевым словом, ключевое слово – інфарматыка, а = 2 \n 2.Таблица Трисемуса, ключевое слово – собственное имя(Женя)");
            Console.WriteLine();
            string file = "text.txt";
            var encoding = Encoding.GetEncoding(1251);
            string text;

            Console.WriteLine();
            Console.WriteLine("Зашифровка");
            Console.WriteLine();
            Console.WriteLine("Шифр Цезаря, ключевое слово – инфарматыка, а = 2");
            Console.WriteLine();
            using (StreamReader sr = new StreamReader(file, encoding))
            {
                text = sr.ReadToEnd();
            }
            for (int i = 0; i < 10; i++)
            {
                Caesar.NewAlpha("iнфарматыка", 2);

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                string encryptCaesar = Caesar.EncryptCaesar(text);
                //Console.WriteLine(encryptCaesar);
                Console.WriteLine();
                stopwatch.Stop();
                Console.WriteLine("Время зашифрования " + stopwatch.ElapsedMilliseconds + " ms");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Расшифровка");
                Console.WriteLine();
                Stopwatch stopwatch1 = new Stopwatch();
                stopwatch1.Start();
                string decryptCaesar = Caesar.DecryptCaesar(encryptCaesar);
                //Console.WriteLine(decrypt);
                stopwatch1.Stop();

                Console.WriteLine("Время расшифрования " + stopwatch1.ElapsedMilliseconds + " ms");
                Console.WriteLine();
            }
            Console.WriteLine("2.Таблица Трисемуса, ключевое слово – собственное имя(Женя)");
            Console.WriteLine();
            Console.WriteLine("Зашифровка");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine();
                Trisemus.NewTable("женя");
                Stopwatch stopwatch2 = new Stopwatch();
                stopwatch2.Start();
                string encryptTrisemus = Trisemus.EncryptTrisemus(text);
                //Console.WriteLine(encryptTrisemus);
                stopwatch2.Stop();
                Console.WriteLine();
                Console.WriteLine("Время зашифрования " + stopwatch2.ElapsedMilliseconds + " ms");
                Console.WriteLine();
                Console.WriteLine("Расшифровка");
                Console.WriteLine();
                Stopwatch stopwatch3 = new Stopwatch();
                stopwatch3.Start();
                string decryptTrisemus = Trisemus.DecryptTrisemus(encryptTrisemus);
                //Console.WriteLine(decryptTrisemus);
                stopwatch3.Stop();
                Console.WriteLine();
                Console.WriteLine("Время расшифрования " + stopwatch3.ElapsedMilliseconds + " ms");
            }
                Console.ReadLine();
            
        }
        class Trisemus
        {
            private static string alphabet = "абвгдеёжзiйклмнопрстуўфхцчшыьэюя";
            private static char[,] table = new char[4, 8];

            public static void NewTable(string Word)
            {
                bool findSame = false;
                char[] newAlpha = new char[32];
                int key = -1;
                key++;
                int beg = 0, current = key;

                for (int i = key; i < Word.Length + key; i++)
                {
                    for (int j = key; j < Word.Length + key; j++)
                    {
                        if (Word[i - key] == newAlpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        newAlpha[current] = Word[i - key];
                        current++;
                    }
                    findSame = false;
                }


                for (int i = 0; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < newAlpha.Length; j++)
                    {
                        if (alphabet[i] == newAlpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        newAlpha[current] = alphabet[i];
                        current++;
                    }
                    findSame = false;
                    if (current == newAlpha.Length)
                    {
                        beg = i;
                        break;
                    }
                }

                current = 0;
                for (int i = beg; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < newAlpha.Length; j++)
                    {
                        if (alphabet[i] == newAlpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        newAlpha[current] = alphabet[i];
                        current++;
                    }
                    findSame = false;
                }

                int index = 0;
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        table[i, j] = newAlpha[index++];
                    }
                }
            }

            internal static string EncryptTrisemus(string mes)
            {
                string message = mes.ToLower();
                var map = new Dictionary<char, int>();
                foreach (char c in message)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                int len = message.Length;

                var orderkey = from i in map orderby i.Key select i;
                foreach (var item in orderkey)
                {
                    Console.WriteLine($"Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len}\n");
                }
                string res = "";
                foreach (char ch in message)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ch != table[i, j])
                                continue;
                            if (i == 3)
                            {
                                res += table[0, j];
                                break;
                            }
                            res += table[i + 1, j];
                            break;
                        }
                    }
                }
                return res;
            }

            internal static string DecryptTrisemus(string message)
            {
                var map = new Dictionary<char, int>();
                foreach (char c in message)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                int len = message.Length;

                var orderkey = from i in map orderby i.Key select i;
                foreach (var item in orderkey)
                {
                    double chast = (double)item.Value / (double)len;
                    Console.WriteLine($"Количество символов {item.Key}  = {item.Value} частота {chast}\n");
                }
                string res = "";
                foreach (char ch in message)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (ch != table[i, j])
                                continue;
                            if (i == 0)
                            {
                                res += table[3, j];
                                break;
                            }
                            res += table[i - 1, j];
                            break;
                        }
                    }
                }
                return res;
            }

        }
        class Caesar
        {

            private static string alphabet = "абвгдеёжзiйклмнопрстуўфхцчшыьэюя";
            private static char[] Alpha = new char[32];
            public static string EncryptCaesar(string mes)
            {
                string Message = mes.ToLower();
                var map = new Dictionary<char, int>();
                foreach (char c in Message)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                int len = Message.Length;

                var orderkey = from i in map orderby i.Key select i;
                foreach (var item in orderkey)
                {
                    Console.WriteLine($"Количество символов {item.Key}  = {item.Value} частота {(double)item.Value / len}\n");
                }
                string res = "";
                foreach (char ch in Message)
                {
                    for (int i = 0; i < alphabet.Length; i++)
                    {
                        if (ch != alphabet[i])
                            continue;
                        res += Alpha[i];
                        break;
                    }
                }
                return res;
            }

            public static string DecryptCaesar(string Message)
            {
                var map = new Dictionary<char, int>();
                foreach (char c in Message)
                {
                    if (!map.ContainsKey(c))
                        map.Add(c, 1);
                    else
                        map[c] += 1;
                }

                int len = Message.Length;

                var orderkey = from i in map orderby i.Key select i;
                foreach (var item in orderkey)
                {
                    double chast = (double)item.Value / (double)len;
                    Console.WriteLine($"Количество символов {item.Key}  = {item.Value}  частота {chast}\n");

                }
                string res = "";
                foreach (char ch in Message)
                {
                    for (int i = 0; i < Alpha.Length; i++)
                    {
                        if (ch != Alpha[i])
                            continue;
                        res += alphabet[i];
                        break;
                    }
                }
                return res;
            }

            public static void NewAlpha(string Word, int key)
            {
                bool findSame = false;
                key++;
                int beg = 0, current = key;

                for (int i = key; i < Word.Length + key; i++)
                {
                    for (int j = key; j < Word.Length + key; j++)
                    {
                        if (Word[i - key] == Alpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        Alpha[current] = Word[i - key];
                        current++;
                    }
                    findSame = false;
                }


                for (int i = 0; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < Alpha.Length; j++)
                    {
                        if (alphabet[i] == Alpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        Alpha[current] = alphabet[i];
                        current++;
                    }
                    findSame = false;
                    if (current == Alpha.Length)
                    {
                        beg = i;
                        break;
                    }
                }

                current = 0;
                for (int i = beg; i < alphabet.Length; i++)
                {
                    for (int j = 0; j < Alpha.Length; j++)
                    {
                        if (alphabet[i] == Alpha[j])
                        {
                            findSame = true;
                            break;
                        }
                    }
                    if (!findSame)
                    {
                        Alpha[current] = alphabet[i];
                        current++;
                    }
                    findSame = false;
                }
            }

        }
    }
}
