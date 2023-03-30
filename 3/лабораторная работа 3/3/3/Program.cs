using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("3 ЛАБОРАТОРНАЯ РАБОТА");
            Console.WriteLine("ОСНОВЫ ТЕОРИИ ЧИСЕЛ И ИХ ИСПОЛЬЗОВАНИЕ В КРИПТОГРАФИИ");
            TrialDivision(18);
            Console.WriteLine("1. Нахождение НОД для 2 чисел:");
            Console.WriteLine("a = ");
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("b = ");
            int b = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Наибольший общий делитель чисел  "+a+"  и "+b+" равен " + GCD(a, b));
            Console.WriteLine("\n1.2. Нахождение НОД для 3 чисел:");
            Console.WriteLine("c = ");
            int c = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("d = ");
            int d = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("e = ");
            int e = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Наибольший общий делитель чисел "+c+"," +d +" и "+ e+" равен " + GCD(GCD(c, d), e));
            Console.WriteLine("\n1.2. выполнять поиск простых чисел:");
            Console.WriteLine("1. Поиск простых чисел на интервале.Сравнить это число с n/ln(n)");
          
                int count = 0;
                Console.WriteLine("начало интервала 2 ");
                int start = 2;
                Console.WriteLine("Введите конец интервала: ");
                int finish = int.Parse(Console.ReadLine());
                Console.WriteLine("Простые числа на интервале [{0}, {1}]:", start, finish);
                for (int i = start; i <=finish; i++)
                {
                    if (IsPrimeNumber(i))
                    {
                        count++;
                        Console.WriteLine(i);
                    }
                }
                Console.WriteLine("Количество простых чисел:{0}", count.ToString());
                Console.WriteLine("n/ln(n):"+ finish/ Math.Log(finish));
                Console.WriteLine("Округление:"+ Math.Round(finish / Math.Log(finish)));
            Console.WriteLine("Введите начало интервала: ");
            uint start1 = uint.Parse(Console.ReadLine());
            Console.WriteLine("Введите конец интервала: ");
            uint finish1 = uint.Parse(Console.ReadLine());
            Console.WriteLine("2. Повторить п. 1 для интервала [m, n]. Сравнить полученные результаты с «ручными» вычислениями,используя «решето Эратосфена» ");
            var primeNumbers = SieveEratosthenes(start1, finish1+1);
            
            Console.WriteLine(string.Join(", ", primeNumbers));
           


         
            Console.WriteLine("Первое число: ");
            int con1 = int.Parse(Console.ReadLine());
            Console.WriteLine("Второе число: ");
            int con2 = int.Parse(Console.ReadLine());
            int newNumber = Convert.ToInt32(string.Format("{0}{1}", con1, con2));
            Console.WriteLine(newNumber);
            proctnumber(newNumber);
            Console.ReadLine();

        }
        public static bool IsPrimeNumber(int n)
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
        static List<uint> SieveEratosthenes(uint n1,uint n)
        {
            var numbers = new List<uint>();
            //заполнение списка числами от 2 до n-1
            for (var i = 2u; i < n; i++)
            {
                numbers.Add(i);
            }

            for (var i = 0; i < numbers.Count; i++)
            {
                for (var j = 2u; j < n; j++)
                {
                    //удаляем кратные числа из списка
                    numbers.Remove(numbers[i] * j);
                }
            }
            for (int i = 0; i < numbers.Count(); i++)
            {
                if (numbers[i] < n1)
                {
                    numbers.RemoveAt(i);
                    i--;
                }
            }

            return numbers;
        }
        static List<uint> TrialDivision(uint n)
        {
            uint numd = n;
            var divides = new List<uint>();
            var div = 2u;
            while (n > 1)
            {
                if (n % div == 0)
                {
                    divides.Add(div);
                    n /= div;
                }
                else
                {
                    div++;
                }
            }
            int cout3=0;
            int cout2=0;
            int cout5=0;
            int cout7=0;
            string masseg="";
            foreach (var numbers in divides)
            {
                if(numbers==3)
                {
                    cout3++;
                }
                if (numbers == 2)
                {
                    cout2++;
                }
                if (numbers == 5)
                {
                    cout5++;
                }
                if (numbers == 7)
                {
                    cout7++;
                }
            }
            if (cout2 > 1) {
                masseg =masseg+ "2 ^" + cout2 +" * ";
            }
            if (cout3 > 1)
            {
                masseg = masseg + "3 ^" + cout3 + " * ";
            }
            if (cout5 > 1)
            {
                masseg = masseg + "5 ^" + cout5 + " * ";
            }
            if (cout7 > 1)
            {
                masseg = masseg + "5 ^" + cout7 + " * ";
            }
            if(cout2==1)
            {
                masseg = masseg + "2 * ";
            }
            if (cout3 == 1)
            {
                masseg = masseg + "3 * ";
            }
            if (cout5 == 1)
            {
                masseg = masseg + "5 * ";
            }
            if (cout7 == 1)
            {
                masseg = masseg + "7 * ";
            }

            masseg = masseg.TrimEnd(' ');
            masseg = masseg.TrimEnd('*');
            masseg = masseg + " = "+numd ;
            Console.WriteLine(masseg);
            return divides;
        }
        //циклич.алгоритм нахождения нод
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
        public static void proctnumber(int num)
        {
            bool prost = true;
         
            for (int i = 2; i <= num / 2; i++)
            {
                if (num % i == 0)
                {
                    prost = false;
                    break;
                }
            }
            if (prost)
            {
                Console.WriteLine("Число простое");
            }
            else
            {
                Console.WriteLine("Число не простое");
            }
        }
    }
}
