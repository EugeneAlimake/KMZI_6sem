using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] scottish = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U' };
            char[] chukchi = { 'А', 'Б', 'В', 'Г', 'д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Ӄ', 'Л', 'Ԓ', 'М', 'Н', 'Ӈ', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Я', '’' };
            char[] binary = { '1', '0' };
            Console.WriteLine("Лабораторная работа № 2");
            Console.WriteLine("1 -2 ЗАДАНИЕ - Рассчитать энтропию двух алфавитов+в бинарном");
            Console.WriteLine("Выберете алфавит");
            Console.WriteLine("1-scottish, 2-chukchi, 3-binary");
            int choose_alphabet = 0;
            int.TryParse(Console.ReadLine(), out choose_alphabet);
            while (choose_alphabet > 3 || choose_alphabet<0)
            {
                Console.WriteLine("Выберете 1,2 или 3");
                int.TryParse(Console.ReadLine(), out choose_alphabet);
            }
            switch (choose_alphabet)
            {
                case (1):
                    calculate_entropy("Dhfhalbh Murchadh is Mionachag a bhuain shuibheagan,ach mar a bhuaineadh Murchadh dhitheadh Mionachag Dhfhalbh Murchadh a dhiarraidh slait gu gabhail air Mionachag, s i g itheadh a chuid shuibheagan Penno.", scottish);

                    break;
                case (2):
                     calculate_entropy("К’ол гитлин к’орагынрэтыльын Выквын, Канчалангыпы, горатвален тинук’к’эмэк а’к’эмлёёчгык рээн вак. Ӄ’оӇыры игыр а’тавратан вэлыткорак к’онпы варкын а’к’эмлыгытлеянва тэгъен’у лынъё э’к’эмимыл, ыныкит гаманэта гатвата. Игыр тэгмигчирэтыльыт, к’нур кытооркэнамэл, нымэльэв мынгыкватыркыт, ытръэч кытоорк’ай ампан’ъэвн’ытоы’лёк ниилк’ин ынк’эн э’к’имыл.Игыр-ым а’тавратан, к’эйвэлым ныкитэ, варкыт гэмгэнымык э’к’имлывилыткульыт, чит руссильымил «спекулянто» нынныльыт, лыгивну лынъёт рэмкэ. К’ынвэр - ым Выквын а’к’арэтылян’н’огъэ.Рэты ытлён нивын: «К’ыйъогын инэнмэлевэтыльын Татьяна, к’ымн’ылёгын ыллён, мин’кри нытэйкык’инэт кэлит, заявленият гырголрамкэты, н’энри энмэн гыт тэнмаквъэ».Мэткиит а’тчанэн рывантатъё малавран, нылин’илюльэтк’ин. ", chukchi);
                    break;
                case (3):
                    Console.WriteLine(Binary("Hello World"));
                    calculate_entropy(Binary("Hello World"), binary);
                    break;
                default:
                    break;
            }
            Console.WriteLine("3 Задание - посчитать количество информации");
            Console.WriteLine("Николаева Евгения");
            Console.WriteLine("Nikolaeva Evgenia");
            Console.WriteLine("Scottish");
            double a1 = calculate_entropy("Nikolaeva Evgenia", scottish);
            Console.WriteLine("Количество информации:    " + CountInfo("Nikolaeva Evgenia", a1));
            Console.WriteLine("Chukchi");
            double a2 = calculate_entropy("Николаева Евгения", chukchi);
            Console.WriteLine("Количество информации:    " + CountInfo("Nikolaeva Evgenia", a2));
            Console.WriteLine("Binary");
            double a3 = calculate_entropy(Binary("Nikolaeva Evgenia"), binary);
            Console.WriteLine("Количество информации:    " + CountInfo(Binary("Nikolaeva Evgenia"), a3));

            Console.WriteLine("4 задание");
            double b1 = WithError(0.1);
            Console.WriteLine("0.1: " + WithError(0.1));
            Console.WriteLine("Количество информации:    " + CountInfo(Binary("Nikolaeva Evgenia"), b1));
            double b2 = WithError(0.5);
            Console.WriteLine("0.5: " + WithError(0.5));
            Console.WriteLine("Количество информации:    " + CountInfo(Binary("Nikolaeva Evgenia"), b2));
            double b3 = WithError(0.9999999999999999);
            Console.WriteLine("1.0: " + WithError(0.9999999999999999));
            Console.WriteLine("Количество информации:    " + CountInfo(Binary("Nikolaeva Evgenia"), b3));
            Console.WriteLine();
            Console.WriteLine("Для шотландского");
            double s1 = WithError(0.1);
            Console.WriteLine("0.1: " + WithError(0.1));
            Console.WriteLine("Количество информации:    " + CountInfo("Nikolaeva Evgenia", s1));
            double s2 = WithError(0.5);
            Console.WriteLine("0.5: " + WithError(0.5));
            Console.WriteLine("Количество информации:    " + CountInfo("Nikolaeva Evgenia", s2));
            double s3 = WithError(0.9999999999999999);
            Console.WriteLine("1.0: " + WithError(0.9999999999999999));
            Console.WriteLine("Количество информации:    0");
            Console.ReadLine();
        }
        public static double calculate_entropy(string text, char[] alphaabet)
        {
            double result = 0;
            for (int i = 0; i < alphaabet.Length; i++)
            {
                int count = Regex.Matches(text, alphaabet[i].ToString(), RegexOptions.IgnoreCase).Count;
                double probability = (double)count / text.Length;
                if (probability != 0)
                {
                    result += probability * Math.Log(probability, 2);
                  
                }
                if(alphaabet[i]== 'Ӄ')
                {
                    Console.WriteLine("К[q]" + "     " + probability);
                }
                else if (alphaabet[i] == 'Ӈ')
                {
                    Console.WriteLine("Н[нг]" + "     " + probability);
                }
                else if (alphaabet[i] == 'Ԓ')
                {
                    Console.WriteLine("Л[ль]" + "     " + probability);
                }
                else
                    Console.WriteLine(alphaabet[i].ToString() + "     " + probability);
            }
            Console.WriteLine("Энтопия:");
            Console.WriteLine(-result);
            return -result;
        }
        public static string Binary(string text)
        {
             byte[] buf =Encoding.ASCII.GetBytes(text);
             StringBuilder sb = new StringBuilder(buf.Length * 8);
             foreach (byte b in buf)
             {
                 sb.Append(Convert.ToString(b, 2).PadLeft(8, '0'));
             }
             string binaryStr = sb.ToString();
           // Console.WriteLine(binaryStr);
             return binaryStr;
        }
        static double CountInfo(string text, double resultentropy)
        {
            return text.Length * resultentropy;
        }

        static double WithError(double error)
        {

            double puk = (double)(1 - (-error * Math.Log(error, 2) - (1 - error) * Math.Log((1 - error), 2)));
            return puk;
        }
    }
}
