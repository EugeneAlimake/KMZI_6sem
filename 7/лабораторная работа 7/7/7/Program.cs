using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7
{

    public class Program
    {
       
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа № 5");
            Console.WriteLine("ИССЛЕДОВАНИЕ БЛОЧНЫХ ШИФРОВ ");
            DES.Encript("01fe");
           
            DES.Decipher();
            Console.ReadLine();
            

        }
       public class DES
        {
            public static string keydesc="";
            public static int sizeOfBlock = 128; //в DES размер блока 64 бит, но поскольку в unicode символ в два раза длинее, то увеличим блок тоже в два раза
            public static int sizeOfChar = 16; //размер одного символа (in Unicode 16 bit)
            public static int shiftKey = 2; //сдвиг ключа 
            public static int quantityOfRounds = 16; //количество раундов
            public static string[] Blocks; //сами блоки в двоичном формате
            static string StringToRightLength(string input)
            {
                while (((input.Length * sizeOfChar) % sizeOfBlock) != 0)
                    input += "#";

                return input;
            }
            static void CutStringIntoBlocks(string input)
            {
                Blocks = new string[(input.Length * sizeOfChar) / sizeOfBlock];

                int lengthOfBlock = input.Length / Blocks.Length;

                for (int i = 0; i < Blocks.Length; i++)
                {
                    Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
                    Blocks[i] = StringToBinaryFormat(Blocks[i]);
                }
            }
            static void CutBinaryStringIntoBlocks(string input)
            {
                Blocks = new string[input.Length / sizeOfBlock];

                int lengthOfBlock = input.Length / Blocks.Length;

                for (int i = 0; i < Blocks.Length; i++)
                    Blocks[i] = input.Substring(i * lengthOfBlock, lengthOfBlock);
            }
            static string StringToBinaryFormat(string input)
            {
                string output = "";

                for (int i = 0; i < input.Length; i++)
                {
                    string char_binary = Convert.ToString(input[i], 2);

                    while (char_binary.Length < sizeOfChar)
                        char_binary = "0" + char_binary;

                    output += char_binary;
                }

                return output;
            }
            static string CorrectKeyWord(string input, int lengthKey)
            {
                if (input.Length > lengthKey)
                    input = input.Substring(0, lengthKey);
                else
                    while (input.Length < lengthKey)
                        input = "0" + input;

                return input;
            }
            static string EncodeDES_One_Round(string input, string key)
            {
                string L = input.Substring(0, input.Length / 2);
                string R = input.Substring(input.Length / 2, input.Length / 2);

                return (R + XOR(L, f(R, key)));
            }
            static string DecodeDES_One_Round(string input, string key)
            {
                string L = input.Substring(0, input.Length / 2);
                string R = input.Substring(input.Length / 2, input.Length / 2);

                return (XOR(f(L, key), R) + L);
            }
            static string XOR(string s1, string s2)
            {
                string result = "";

                for (int i = 0; i < s1.Length; i++)
                {
                    bool a = Convert.ToBoolean(Convert.ToInt32(s1[i].ToString()));
                    bool b = Convert.ToBoolean(Convert.ToInt32(s2[i].ToString()));

                    if (a ^ b)
                        result += "1";
                    else
                        result += "0";
                }
                return result;
            }
            static string f(string s1, string s2)
            {
                return XOR(s1, s2);
            }
            static string KeyToNextRound(string key)
            {
                for (int i = 0; i < shiftKey; i++)
                {
                    key = key[key.Length - 1] + key;
                    key = key.Remove(key.Length - 1);
                }

                return key;
            }
            static string KeyToPrevRound(string key)
            {
                for (int i = 0; i < shiftKey; i++)
                {
                    key = key + key[0];
                    key = key.Remove(0, 1);
                }

                return key;
            }
            static string StringFromBinaryToNormalFormat(string input)
            {
                string output = "";

                while (input.Length > 0)
                {
                    string char_binary = input.Substring(0, sizeOfChar);
                    input = input.Remove(0, sizeOfChar);

                    int a = 0;
                    int degree = char_binary.Length - 1;

                    foreach (char c in char_binary)
                        a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

                    output += ((char)a).ToString();
                }

                return output;
            }
            public static void Encript(string keyword)
            {
                
                if (keyword.Length > 0)
                {
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    string s = "";

                    string key = keyword;

                    StreamReader sr = new StreamReader("in.txt");

                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }

                    sr.Close();

                    s = StringToRightLength(s);

                    CutStringIntoBlocks(s);

                    key = CorrectKeyWord(key, s.Length / (2 * Blocks.Length));
                    key = StringToBinaryFormat(key);

                    for (int j = 0; j < quantityOfRounds; j++)
                    {
                        for (int i = 0; i < Blocks.Length; i++)
                            Blocks[i] = EncodeDES_One_Round(Blocks[i], key);

                        key = KeyToNextRound(key);
                        
                    }
                    stopwatch.Stop();
                    Console.WriteLine("Время зашифрования " + stopwatch.ElapsedMilliseconds + " ms");
                    key = KeyToPrevRound(key);

                    keydesc = StringFromBinaryToNormalFormat(key);

                    string result = "";

                    for (int i = 0; i < Blocks.Length; i++)
                        result += Blocks[i];
                    //Console.WriteLine(result);
                    Console.WriteLine(StringFromBinaryToNormalFormat(result));
                    StreamWriter sw = new StreamWriter("out1.txt");
                    sw.WriteLine(StringFromBinaryToNormalFormat(result));
                    sw.Close();

                    //Process.Start("out1.txt");
                }
                else
                    Console.WriteLine("Введите ключевое слово!");
            }
            public static void Decipher()
            {
                if (keydesc.Length > 0)
                {
                    Stopwatch stopwatch2 = new Stopwatch();
                    stopwatch2.Start();
                    string s = "";

                    string key = StringToBinaryFormat(keydesc);

                    StreamReader sr = new StreamReader("out1.txt");

                    while (!sr.EndOfStream)
                    {
                        s += sr.ReadLine();
                    }

                    sr.Close();

                    s = StringToBinaryFormat(s);

                    CutBinaryStringIntoBlocks(s);

                    for (int j = 0; j < quantityOfRounds; j++)
                    {
                        for (int i = 0; i < Blocks.Length; i++)
                            Blocks[i] = DecodeDES_One_Round(Blocks[i], key);

                        key = KeyToPrevRound(key);
                    }

                    key = KeyToNextRound(key);

                    //keyword = StringFromBinaryToNormalFormat(key);

                    string result = "";

                    for (int i = 0; i < Blocks.Length; i++)
                        result += Blocks[i];
                    Console.WriteLine(StringFromBinaryToNormalFormat(result));
                    Console.WriteLine("Время расшифрования " + stopwatch2.ElapsedMilliseconds + " ms");
                    StreamWriter sw = new StreamWriter("out2.txt");
                    sw.WriteLine(StringFromBinaryToNormalFormat(result));
                    sw.Close();

                    //Process.Start("out2.txt");
                }
              
            }
        }
        
        
        
        

        
    }
  
}
