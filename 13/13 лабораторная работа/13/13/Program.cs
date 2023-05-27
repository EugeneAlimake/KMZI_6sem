using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace _13
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Лабораторная работа 13");
            Console.WriteLine("ИССЛЕДОВАНИЕ КРИПТОГРАФИЧЕСКИХ АЛГОРИТМОВ НА ОСНОВЕ ЭЛЛИПТИЧЕСКИХ КРИВЫХ");
            Console.WriteLine("1.1. Найти точки ЭК для значений х");
            Stopwatch stopwatch1 = new Stopwatch();
            stopwatch1.Start();
            int a = -1, b = 1, p = 751, k = 7, l = 7, d = 19;
            int Xmin = 500, Xmax = 620;
            int temp = Xmin;
            int xtemp, ytemp;
            while (temp <= Xmax)
            {
                Console.WriteLine($"y = {temp} x^3-x+1 mod 751= {(temp * temp * temp - temp + 1) % 751} ");
                Console.WriteLine($"x = {temp} y^2 mod 751= {(temp * temp) % 751} ");
                temp++;
            }
             temp = Xmin;
            while (temp <= Xmax)
            {
               
                xtemp=temp;
                ytemp = ((temp * temp) % 751);
                Console.Write($"({xtemp},{ytemp}); ");
                temp++;
            }
            Console.WriteLine();
            stopwatch1.Stop();
            Console.WriteLine("Вычисление Хmin= 500, Xmax=620 :" + stopwatch1.ElapsedMilliseconds + " ms");
            Console.WriteLine("1.2. Разработать приложение для выполнения операций над точками кривой");
            Console.WriteLine("а)kР; б)Р+Q; в)kР+lQ–R; г)Р–Q+R. ");
            Console.WriteLine("k=7 P=(59, 365), Q=(59, 386), R=(105, 382), a=-1,b=1");
            int xP = 59;
            int yP = 365;
            int xQ = 58;
            int yQ = 382;
            int xR = 105;
            int yR = 382;
            Console.Write("1)kP=");  
            Console.WriteLine('(' + ForFirst(k, xP, yP) + ')');
            Console.Write("\n2)P+Q= ");
            Console.WriteLine('(' + Summa(xP, xQ, yP, yQ) + ')');
            Console.Write("\n3)kP+lQ-R=");
            string[] Expr1 = ForFirst(k, xP, yP).Split(',');
            string[] Expr2 = ForFirst(l, xQ, yQ).Split(',');
            string[] Expr3 = Summa(int.Parse(Expr1[0]), int.Parse(Expr2[0]), int.Parse(Expr1[1]), int.Parse(Expr2[1])).Split(',');
            Console.WriteLine('(' + Summa(int.Parse(Expr3[0]), xR, int.Parse(Expr3[1]), mod(-yR, p)) + ')' + " ");
            Console.Write("\n4)P-Q+R=");
            string[] Expr = Summa(xP, xQ, yP, mod(-yQ, p)).Split(',');
            string ResultExpr2 = '(' + Summa(int.Parse(Expr[0]), xR, int.Parse(Expr[1]), yR) + ')';
            Console.WriteLine('(' + Summa(int.Parse(Expr[0]), xR, int.Parse(Expr[1]), yR) + ')' + " ");
            Console.WriteLine("Задание 2.зашифровать/расшифровать собственное имя на основе ЭК, указанной в задании 1, для генерирующей точки G = (0, 1)");
            Dictionary<char, (int x, int y)> hash = new Dictionary<char, (int x, int y)>();
            Dictionary<(int x, int y), char> obrhash = new Dictionary<(int x, int y), char>();
            hash.Add('Е', (194, 205));
            hash.Add('В', (189, 454));
            hash.Add('Г', (192, 719));
            hash.Add('Н', (203, 427));
            hash.Add('И', (198, 224));
            hash.Add('Я', (227, 452));
            obrhash.Add((194, 205), 'Е');
            obrhash.Add((189, 454), 'В');
            obrhash.Add((192, 719), 'Г');
            obrhash.Add((203, 427), 'Н');
            obrhash.Add((198, 224), 'И');
            obrhash.Add((227, 452), 'Я');
            string text = "ЕВГЕНИЯ НИГИИИИ";
            string TextToDecrypt = "";
            (int x, int y) cort;
            int xG = 0;
            int yG = 1;
            string[] numbersQ = ForFirst(d, xG, yG).Split(',');
            Console.WriteLine("Зpначение открытого ключа Q (" + numbersQ[0] +","+ numbersQ[1]+")");
            Console.Write("Шифрование:");
            int xQ5 = int.Parse(numbersQ[0]);
            int yQ5 = int.Parse(numbersQ[1]);
            foreach (char s in text)
            {
                hash.TryGetValue(s, out cort);
                string numbersC1 = ForFirst(k, xG, yG);
                string[] numbersExpr1 = ForFirst(k, xQ5, yQ5).Split(',');
                string numbersC2 = Summa(cort.x, int.Parse(numbersExpr1[0]), cort.y, int.Parse(numbersExpr1[1]));
                TextToDecrypt += numbersC1 + ' ' + numbersC2 + ' ';
            }
            TextToDecrypt = TextToDecrypt.Remove(TextToDecrypt.Length - 1);
            Console.WriteLine(TextToDecrypt);
            Console.Write("Расшифровка:");
            string[] text2 = TextToDecrypt.Split(' ');
            string TextToEncrypt = "";
            for (int i = 0; i < text2.Length; i += 2)
            {
                string[] numbe1 = text2[i].ToString().Split(',');
                string[] numbe2 = text2[i + 1].ToString().Split(',');
                string[] numbersC1 = ForFirst(d, int.Parse(numbe1[0]), int.Parse(numbe1[1])).Split(',');
                string[] result = Summa(int.Parse(numbe2[0]), int.Parse(numbersC1[0]), int.Parse(numbe2[1]), mod(-int.Parse(numbersC1[1]), p)).Split(',');
                (int x, int y) cort2 = (int.Parse(result[0]), int.Parse(result[1]));
                char s;
                obrhash.TryGetValue(cort2, out s);
                TextToEncrypt += s;
            }
            Console.WriteLine(TextToEncrypt);
            Console.ReadLine();
        }
        public static string Summa(int xP, int xQ, int yP, int yQ)
        {
            BigInteger lyambda;
            int p = 751;
            int rX = xQ - xP;
            int rY = yQ - yP;
            if (rX < 0){rX += p;}
            if (rY < 0){rY += p;}
            if (xP == 0 & yP == 0){return xQ.ToString() + ',' + yQ.ToString();}
            if (xQ == 0 & yQ == 0){return xP.ToString() + ',' + yP.ToString();}
            BigInteger xR = 0, yR = 0;
                if (xP == xQ && yP == yQ){lyambda = (3 * BigInteger.Pow(xP, 2) - 1) * (Foo(2 * yP, p));}
                else{lyambda = (rY) * Foo(rX, p);}
                xR = (BigInteger.Pow(lyambda, 2) - xP - xQ);
                yR = yP + lyambda * (xR - xP);
                xR = xR % p < 0 ? (xR % p) + p : xR % p;
                yR = -yR % p < 0 ? (-yR % p) + p : (-yR % p);
           
            string Result = xR.ToString() + ',' + yR.ToString();
            return Result;
        }
        public static int Foo(int a, int m)
        {
            int x, y;
            int g = GCD(a, m, out x, out y);
            if (g != 1)
                throw new ArgumentException();
            return (x % m + m) % m;
        }
        public static int GCD(int a, int b, out int x, out int y)
        {
            int p = 751;
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
            return d % p;
        }
        public static string ForFirst(int k, int xP, int yP)
        {
            string[] numbers = { "", "" };
            int xQ = xP;
            int yQ = yP;
            string[] result = { "" };
            string[] addend = { xQ.ToString(), yQ.ToString() };
            while (k > 0)
            {
                if ((k & 1) > 0)
                {
                    if (result.Length == 2){result = Summa(int.Parse(result[0]),xQ, int.Parse(result[1]), yQ).Split(','); }
                    else{result = addend;}
                }
                addend = Summa(xQ, xQ, yQ, yQ).Split(',');
                k >>= 1;
            }
            return result[0] + "," + result[1];
        }
        public static int mod(int k, int n) { return ((k %= n) < 0) ? k + n : k; }
    }
}
