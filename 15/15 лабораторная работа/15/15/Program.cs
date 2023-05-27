using Aspose.Words;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
namespace _15
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ИССЛЕДОВАНИЕ МЕТОДОВ ТЕКСТОВОЙ СТЕГАНОГРАФИИ ");
            Console.WriteLine("Модификация расстояния между строками;");
            Console.WriteLine("Шифрование:");
            ChangingthelengtheEncryption();
            Console.WriteLine("Расшифрование:");
            ChangingthelengtheDecryption();
            Console.WriteLine("Модификация кернинга;");
            Console.WriteLine("Шифрование:");
            kerningEncryption();
            Console.WriteLine("Расшифрование:");
            kerningDecryption();
        }

        public static void ChangingthelengtheDecryption()
        {
            Document document = new Document("encryptedChangingthelengthe.docx");
            int lines_count = document.Sections[0].Body.Paragraphs.Count;
            String arr = "";
            int size = 0;
            for (int i = 0; i < lines_count; i++)
            {
                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.Contains("  "))
                {
                    size = i;
                    break;
                }

                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Text.EndsWith(" "))
                {
                    arr += '1';
                }
                else
                {
                    arr += '0';
                }
            }



            Console.WriteLine("Полученное сообщение: " + BinaryToString(arr));
            Console.ReadLine();
        }

        public static void kerningDecryption()
        {
            Document document = new Document("kerning.docx");
            int lines_count = document.Sections[0].Body.Paragraphs.Count;
            String arr = "";
            int size = 0;
            string containerText = " ";
            string secretMessage=" ";
            int containerLength = containerText.Length;
            int messageLength = secretMessage.Length;
            // Модифицируем кернинг для каждого символа сообщения
            string steganographicText = "";
            for (int i = 0; i < messageLength; i++)
            {
                char containerChar = containerText[i];
                char messageChar = secretMessage[i];
                int modifiedChar = containerChar + messageChar;

                steganographicText += (char)modifiedChar;
            }
            // Добавляем оставшуюся часть контейнерного текста
            steganographicText += containerText.Substring(messageLength);

            for (int i = 0; i < lines_count; i++)
            {
                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1 && document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.B == 1)
                {
                    size = i;
                    break;
                }

                if (document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color.G == 1)
                {
                    arr += '1';
                }
                else
                {
                    arr += '0';
                }
            }



            Console.WriteLine("Полученное сообщение: " + BinaryToString(arr));
            Console.ReadLine();
        }

        public static void ChangingthelengtheEncryption()
        {
            Document Changingthelengthe = new Document("firsttext.docx");
            Console.WriteLine("Впишите сообщение:");
            String text = Console.ReadLine();
            String bin = StringToBinary(text);
            for (int i = 0; i < bin.Length; i++)
            {
                String additional = bin[i] == '0' ? "" : " ";
                Changingthelengthe.Sections[0].Body.Paragraphs[i].Runs[0].Text += additional;

                if (i + 1 == bin.Length)
                {
                    Changingthelengthe.Sections[0].Body.Paragraphs[i + 1].Runs[0].Text += "  ";
                }
            }
            Changingthelengthe.Save("encryptedChangingthelengthe.docx");
        }

        public static void kerningEncryption()
        {
            Document document = new Document("firsttext.docx");
            double lines_count = document.Sections[0].Body.Paragraphs.Count;
            Console.WriteLine("Впишите сообщение:");
            String data = Console.ReadLine();
            String bin = StringToBinary(data);
            string steganographicText = "";
            // Реализация метода извлечения сообщения из стеганографического текста
            int steganographicLength = steganographicText.Length;

            string extractedMessage = "";
            for (int i = 0; i < steganographicLength; i++)
            {
                char steganographicChar = steganographicText[i];
                char originalChar = (char)(steganographicChar - steganographicText[i]);

                extractedMessage += originalChar;
            }
            for (int i = 0; i < bin.Length; i++)
            {
                int additional = bin[i] == '0' ? 0 : 1;

                Color newC = Color.FromArgb(0, additional, 0);
                document.Sections[0].Body.Paragraphs[i].Runs[0].Font.Color = newC;

                if (i + 1 == bin.Length)
                {
                    System.Drawing.Color color = document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Font.Color;
                    System.Drawing.Color new_color = Color.FromArgb(0, 1, 1);

                    document.Sections[0].Body.Paragraphs[i + 1].Runs[0].Font.Color = new_color;
                }
            }

            document.Save("kerning.docx");
        }

        public static string StringToBinary(string data)
        {
            String sb = "";

            foreach (char c in data.ToCharArray())
            {
                sb += Convert.ToString(c, 2).PadLeft(8, '0');
            }

            while (sb.Length % 8 != 0)
            {
                sb = "0" + sb;
            }

            return sb;
        }

        //Binary to String
        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();
            for (int i = 0; i + 8 - 1 <= data.Length; i += 8){byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));}
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

    }
}
