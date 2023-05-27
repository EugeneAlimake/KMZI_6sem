using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _14
{
    public partial class Form1 : Form
    {
        public int len = 1;
        public int CountText = 0;
        public Form1()
        {
           
            InitializeComponent(); 
            pictureBox2.Image = Image.FromFile(@"C:/Users/thesi/Desktop/6/Крипта/14/14 лабораторная работа/14/14/enc1.bmp");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private BitArray ByteToBit(byte src)
        {
            BitArray bitArray = new BitArray(8);
            bool st = false;
            for (int i = 0; i < 8; i++)
            {
                if ((src >> i & 1) == 1)
                {
                    st = true;
                }
                else st = false;
                bitArray[i] = st;
            }
            return bitArray;
        }

        private byte BitToByte(BitArray scr)
        {
            byte num = 0;
            for (int i = 0; i < scr.Count; i++)
                if (scr[i] == true)
                    num += (byte)Math.Pow(2, i);
            return num;
        }

        private bool isEncryption(Bitmap scr)
        {
            byte[] rez = new byte[1];
            Color color = scr.GetPixel(0, 0);
            BitArray colorArray = ByteToBit(color.R);
            BitArray messageArray = ByteToBit(color.R);
            messageArray[0] = colorArray[0];
            messageArray[1] = colorArray[1];

            colorArray = ByteToBit(color.G);
            messageArray[2] = colorArray[0];
            messageArray[3] = colorArray[1];
            messageArray[4] = colorArray[2];

            colorArray = ByteToBit(color.B);
            messageArray[5] = colorArray[0];
            messageArray[6] = colorArray[1];
            messageArray[7] = colorArray[2];
            rez[0] = BitToByte(messageArray);
            string m = Encoding.GetEncoding(1251).GetString(rez);
            if (m == "/")
            {
                return true;
            }
            else return false;
        }

        private void WriteCountText(int count, Bitmap src)
        {
            byte[] Count = Encoding.GetEncoding(1251).GetBytes(count.ToString());
            for (int i = 0; i < 3; i++)
            {
                BitArray bitCount = ByteToBit(Count[i]); 
                Color pColor = src.GetPixel(0, i + 1);
                BitArray bitsCurColor = ByteToBit(pColor.R);
                bitsCurColor[0] = bitCount[0];
                bitsCurColor[1] = bitCount[1];
                byte R = BitToByte(bitsCurColor);

                bitsCurColor = ByteToBit(pColor.G);
                bitsCurColor[0] = bitCount[2];
                bitsCurColor[1] = bitCount[3];
                bitsCurColor[2] = bitCount[4];
                byte G = BitToByte(bitsCurColor);

                bitsCurColor = ByteToBit(pColor.B);
                bitsCurColor[0] = bitCount[5];
                bitsCurColor[1] = bitCount[6];
                bitsCurColor[2] = bitCount[7];
                byte B = BitToByte(bitsCurColor);

                Color nColor = Color.FromArgb(R, G, B);
                src.SetPixel(0, i + 1, nColor);
            }
        }

        private int ReadCountText(Bitmap src)
        {
            byte[] rez = new byte[3]; 
            for (int i = 0; i < 3; i++)
            {
                Color color = src.GetPixel(0, i + 1); 
                BitArray colorArray = ByteToBit(color.R); 
                BitArray bitCount = ByteToBit(color.R); 
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = ByteToBit(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = ByteToBit(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];
                rez[i] = BitToByte(bitCount);
            }
            string m = Encoding.GetEncoding(1251).GetString(rez);
            return Convert.ToInt32(m, 10);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string FilePic= @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\enc.bmp";
            string FileText= @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\in.txt";
            FileStream rFile;
            try{rFile = new FileStream(FilePic, FileMode.Open);}
            catch (IOException){MessageBox.Show("Ошибка открытия файла");return;}
            Bitmap bPic = new Bitmap(rFile);
            FileStream rText;
            try{rText = new FileStream(FileText, FileMode.Open);}
            catch (IOException){MessageBox.Show("Ошибка открытия файла");return;}
            BinaryReader bText = new BinaryReader(rText, Encoding.ASCII);
            List<byte> bList = new List<byte>();
            while (bText.PeekChar() != -1){ bList.Add(bText.ReadByte());}
            int CountText = bList.Count;
            bText.Close();
            rFile.Close();
            if (CountText > (bPic.Width * bPic.Height)) {MessageBox.Show("Выбранная картинка мала для размещения выбранного текста"); return;
            }
            if (isEncryption(bPic))
            {
                MessageBox.Show("Файл уже зашифрован");
                return;
            }
            byte[] Symbol = Encoding.GetEncoding(1251).GetBytes("/");
            BitArray ArrBeginSymbol = ByteToBit(Symbol[0]);
            Color curColor = bPic.GetPixel(0, 0);
            BitArray tempArray = ByteToBit(curColor.R);
            tempArray[0] = ArrBeginSymbol[0];
            tempArray[1] = ArrBeginSymbol[1];
            byte nR = BitToByte(tempArray);
            tempArray = ByteToBit(curColor.G);
            tempArray[0] = ArrBeginSymbol[2];
            tempArray[1] = ArrBeginSymbol[3];
            tempArray[2] = ArrBeginSymbol[4];
            byte nG = BitToByte(tempArray);
            tempArray = ByteToBit(curColor.B);
            tempArray[0] = ArrBeginSymbol[5];
            tempArray[1] = ArrBeginSymbol[6];
            tempArray[2] = ArrBeginSymbol[7];
            byte nB = BitToByte(tempArray);
            Color nColor = Color.FromArgb(nR, nG, nB);
            bPic.SetPixel(0, 0, nColor);
            WriteCountText(CountText, bPic);
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == bList.Count)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(bList[index]);
                    colorArray[0] = messageArray[0];
                    colorArray[1] = messageArray[1];
                    byte newR = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.G);
                    colorArray[0] = messageArray[2];
                    colorArray[1] = messageArray[3];
                    colorArray[2] = messageArray[4];
                    byte newG = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.B);
                    colorArray[0] = messageArray[5];
                    colorArray[1] = messageArray[6];
                    colorArray[2] = messageArray[7];
                    byte newB = BitToByte(colorArray);
                    Color newColor = Color.FromArgb(newR, newG, newB);
                    bPic.SetPixel(i, j, newColor);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            pictureBox1.Image = bPic;
            string sFilePic = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\dec.bmp"; 
            FileStream wFile;
            try{wFile = new FileStream(sFilePic, FileMode.Create);}
            catch (IOException) {MessageBox.Show("Ошибка открытия файла на запись"); return; }
            bPic.Save(wFile, System.Drawing.Imaging.ImageFormat.Bmp);
            wFile.Close(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FilePic = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\dec.bmp";
            FileStream rFile;
            try{rFile = new FileStream(FilePic, FileMode.Open);}
            catch (IOException){MessageBox.Show("Ошибка открытия файла");return;}
            Bitmap bPic = new Bitmap(rFile);
            if (!isEncryption(bPic)) { MessageBox.Show("В файле нет зашифрованной информации");return;}
            int countSymbol = ReadCountText(bPic);
            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(pixelColor.R); ;
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = ByteToBit(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = ByteToBit(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];
                    message[index] = BitToByte(messageArray);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            string strMessage = Encoding.GetEncoding(1251).GetString(message);
            string sFileText= @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\out.txt";
            FileStream wFile;
            try{wFile = new FileStream(sFileText, FileMode.Create);}
            catch (IOException){MessageBox.Show("Ошибка открытия файла на запись");return;}
            StreamWriter wText = new StreamWriter(wFile, Encoding.Default);
            wText.Write(strMessage);
            MessageBox.Show("Текст записан в файл");
            wText.Close();
            wFile.Close();
        }
        public enum State
        {
            Hiding,
            FillingWithZeros
        };

        private static int ReverseBits(int n)
        {
            int result = 0;
            for (int i = 0; i < 8; i++)
            {
                result = result * 2 + n % 2;
                n /= 2;
            }
            return result;
        }

        public static Bitmap HideText(string text, Bitmap bmp)
        {
            State state = State.Hiding;
            int charIndex = 0;
            int charValue = 0;
            long pixelElementIndex = 0;
            int zeros = 0;
            int R = 0, G = 0, B = 0;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    R = pixel.R - pixel.R % 2;
                    G = pixel.G - pixel.G % 2;
                    B = pixel.B - pixel.B % 2;
                    for (int n = 0; n < 3; n++)
                    {
                        if (pixelElementIndex % 8 == 0)
                        {
                            if (state == State.FillingWithZeros && zeros == 8)
                            {
                                if ((pixelElementIndex - 1) % 3 < 2)
                                {
                                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                }
                                return bmp;
                            }
                            if (charIndex >= text.Length)
                            {
                                state = State.FillingWithZeros;
                            }
                            else
                            {
                                charValue = text[charIndex++];
                            }
                        }
                        switch (pixelElementIndex % 3)
                        {
                            case 0:
                                if (state == State.Hiding)
                                {
                                    R += charValue % 2;
                                    charValue /= 2;
                                }
                                break;
                            case 1:
                                if (state == State.Hiding)
                                {
                                    G += charValue % 2;
                                    charValue /= 2;
                                }
                                break;
                            case 2:
                                if (state == State.Hiding)
                                {
                                    B += charValue % 2;
                                    charValue /= 2;
                                }
                                bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                                break;
                        }
                        pixelElementIndex++;
                        if (state == State.FillingWithZeros)
                        {
                            zeros++;
                        }
                    }
                }
            }
            return bmp;
        }

        public static string ExtractText(Bitmap bmp)
        {
            int colorUnitIndex = 0;
            int charValue = 0;
            string extractedText = "";
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    for (int n = 0; n < 3; n++)
                    {
                        switch (colorUnitIndex % 3)
                        {
                            case 0:
                                {
                                    charValue = charValue * 2 + pixel.R % 2;
                                }
                                break;
                            case 1:
                                {
                                    charValue = charValue * 2 + pixel.G % 2;
                                }
                                break;
                            case 2:
                                {
                                    charValue = charValue * 2 + pixel.B % 2;
                                }
                                break;
                        }
                        colorUnitIndex++;
                        if (colorUnitIndex % 8 == 0)
                        {
                            charValue = ReverseBits(charValue);
                            if (charValue == 0)
                            {
                                return extractedText;
                            }
                            char c = (char)charValue;
                            extractedText += c.ToString();
                        }
                    }
                }
            }
            return extractedText;
        }

        public static Bitmap CreateMatrix(Bitmap bmp)
        {
            int R = 0, G = 0, B = 0;
            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    Color pixel = bmp.GetPixel(j, i);
                    StringBuilder binR = new StringBuilder(Convert.ToString(pixel.R, 2));
                    if (binR[binR.Length - 1] == '0')
                        R = 0;
                    else
                        R = 255;

                    StringBuilder binG = new StringBuilder(Convert.ToString(pixel.G, 2));
                    if (binG[binG.Length - 1] == '0')
                        G = 0;
                    else
                        G = 255;

                    StringBuilder binB = new StringBuilder(Convert.ToString(pixel.B, 2));
                    if (binB[binB.Length - 1] == '0')
                        B = 0;
                    else
                        B = 255;
                    bmp.SetPixel(j, i, Color.FromArgb(R, G, B));
                }
            }
            return bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string FilePic = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\enc.bmp";
            string FileText = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\in.txt";
            FileStream rFile;
            try { rFile = new FileStream(FilePic, FileMode.Open); }
            catch (IOException) { MessageBox.Show("Ошибка открытия файла"); return; }
            Bitmap bPic = new Bitmap(rFile);
            FileStream rText;
            try { rText = new FileStream(FileText, FileMode.Open); }
            catch (IOException) { MessageBox.Show("Ошибка открытия файла"); return; }
            BinaryReader bText = new BinaryReader(rText, Encoding.ASCII);
            List<byte> bList = new List<byte>();
            while (bText.PeekChar() != -1) { bList.Add(bText.ReadByte()); }
            int CountText = bList.Count;
            bText.Close();
            rFile.Close();
            if (CountText > (bPic.Width * bPic.Height))
            {
                MessageBox.Show("Выбранная картинка мала для размещения выбранного текста"); return;
            }
            if (isEncryption(bPic))
            {
                MessageBox.Show("Файл уже зашифрован");
                return;
            }
            byte[] Symbol = Encoding.GetEncoding(1251).GetBytes("/");
            BitArray ArrBeginSymbol = ByteToBit(Symbol[0]);
            Color curColor = bPic.GetPixel(0, 0);
            BitArray tempArray = ByteToBit(curColor.R);
            tempArray[0] = ArrBeginSymbol[0];
            tempArray[1] = ArrBeginSymbol[1];
            byte nR = BitToByte(tempArray);
            tempArray = ByteToBit(curColor.G);
            tempArray[0] = ArrBeginSymbol[2];
            tempArray[1] = ArrBeginSymbol[3];
            tempArray[2] = ArrBeginSymbol[4];
            byte nG = BitToByte(tempArray);
            tempArray = ByteToBit(curColor.B);
            tempArray[0] = ArrBeginSymbol[5];
            tempArray[1] = ArrBeginSymbol[6];
            tempArray[2] = ArrBeginSymbol[7];
            byte nB = BitToByte(tempArray);
            Color nColor = Color.FromArgb(nR, nG, nB);
            bPic.SetPixel(0, 0, nColor);
            WriteCountText(CountText, bPic);
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == bList.Count)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(bList[index]);
                    colorArray[0] = messageArray[0];
                    colorArray[1] = messageArray[1];
                    byte newR = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.G);
                    colorArray[0] = messageArray[2];
                    colorArray[1] = messageArray[3];
                    colorArray[2] = messageArray[4];
                    byte newG = BitToByte(colorArray);

                    colorArray = ByteToBit(pixelColor.B);
                    colorArray[0] = messageArray[5];
                    colorArray[1] = messageArray[6];
                    colorArray[2] = messageArray[7];
                    byte newB = BitToByte(colorArray);
                    Color newColor = Color.FromArgb(newR, newG, newB);
                    bPic.SetPixel(i, j, newColor);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            pictureBox1.Image = bPic;
            string sFilePic = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\dec.bmp";
            FileStream wFile;
            try { wFile = new FileStream(sFilePic, FileMode.Create); }
            catch (IOException) { MessageBox.Show("Ошибка открытия файла на запись"); return; }
            bPic.Save(wFile, System.Drawing.Imaging.ImageFormat.Bmp);
            wFile.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string FilePic = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\dec.bmp";
            FileStream rFile;
            try { rFile = new FileStream(FilePic, FileMode.Open); }
            catch (IOException) { MessageBox.Show("Ошибка открытия файла"); return; }
            Bitmap bPic = new Bitmap(rFile);
            if (!isEncryption(bPic)) { MessageBox.Show("В файле нет зашифрованной информации"); return; }
            int countSymbol = ReadCountText(bPic);
            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            for (int i = 4; i < bPic.Width; i++)
            {
                for (int j = 0; j < bPic.Height; j++)
                {
                    Color pixelColor = bPic.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        st = true;
                        break;
                    }
                    BitArray colorArray = ByteToBit(pixelColor.R);
                    BitArray messageArray = ByteToBit(pixelColor.R); ;
                    messageArray[0] = colorArray[0];
                    messageArray[1] = colorArray[1];

                    colorArray = ByteToBit(pixelColor.G);
                    messageArray[2] = colorArray[0];
                    messageArray[3] = colorArray[1];
                    messageArray[4] = colorArray[2];

                    colorArray = ByteToBit(pixelColor.B);
                    messageArray[5] = colorArray[0];
                    messageArray[6] = colorArray[1];
                    messageArray[7] = colorArray[2];
                    message[index] = BitToByte(messageArray);
                    index++;
                }
                if (st)
                {
                    break;
                }
            }
            string strMessage = Encoding.GetEncoding(1251).GetString(message);
            string sFileText = @"C:\Users\thesi\Desktop\6\Крипта\14\14 лабораторная работа\14\14\out.txt";
            FileStream wFile;
            try { wFile = new FileStream(sFileText, FileMode.Create); }
            catch (IOException) { MessageBox.Show("Ошибка открытия файла на запись"); return; }
            StreamWriter wText = new StreamWriter(wFile, Encoding.Default);
            wText.Write(strMessage);
            MessageBox.Show("Текст записан в файл");
            wText.Close();
            wFile.Close();
        }
    }
}
