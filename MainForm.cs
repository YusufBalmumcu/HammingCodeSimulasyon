using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HammingCodeProject
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        //Veri eklemek i�in kullan�lan buton
        private void button1_Click(object sender, EventArgs e)
        {
            string inputData = textBox1.Text;

            // Girilen veriyi kontrol eder (4, 8 veya 16 bitlik binary veri)
            if (InputKontrol(inputData, new int[] { 4, 8, 16 }))
            {
                MessageBox.Show($"Girilen veri: {inputData}");
                string hamingCode = HammingCodeHesapla(inputData);
                textBox2.Text = hamingCode;
            }
            else
            {
                MessageBox.Show("L�tfen 4, 8 veya 16 bitlik bir binary veri girin.", "Ge�ersiz Veri", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string hammingCode = textBox2.Text;

            if (string.IsNullOrEmpty(hammingCode))
            {
                MessageBox.Show("L�tfen �nce bir Hamming kodu olu�turun.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Kullan�c�dan bozulacak bit pozisyonunu ister
            int bitPosition;
            string input = Microsoft.VisualBasic.Interaction.InputBox("Bozmak istedi�iniz bit pozisyonunu girin (1'den ba�layarak):", "Bit Bozma", "1");

            if (int.TryParse(input, out bitPosition) && bitPosition >= 1 && bitPosition <= hammingCode.Length)
            {
                char[] hammingCodeArray = hammingCode.ToCharArray();

                // Belirtilen bit pozisyonunu bozar (1'den ba�layarak oldu�u i�in 1 azalt�yoruz)
                int indexToCorrupt = bitPosition - 1;
                hammingCodeArray[indexToCorrupt] = hammingCodeArray[indexToCorrupt] == '0' ? '1' : '0';

                string corruptedCode = new string(hammingCodeArray);
                textBox3.Text = corruptedCode;

                // Sendrom kelimesini hesaplayar
                string syndrome = CalculateSyndrome(hammingCode, corruptedCode);
                textBox4.Text = syndrome;

                // Hatal� bit pozisyonunu belirleyin ve kullan�c�ya g�sterir
                int errorPosition = Convert.ToInt32(syndrome, 2);
                label5.Text = $"Hatal� bit pozisyonu: {errorPosition}";
            }
            else
            {
                MessageBox.Show("Ge�ersiz bit pozisyonu. L�tfen ge�erli bir pozisyon girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        // Girilen verinin do�ru formatta olup olmad���n� kontrol eder
        private bool InputKontrol(string input, int[] validLength)
        {
            if (Regex.IsMatch(input, "^[01]+$") && Array.Exists(validLength, length => length == input.Length))
            {
                return true;
            }
            return false;
        }

        // Hamming kodunu hesaplayan fonksiyon
        private string HammingCodeHesapla(string data)
        {
            // Al�nan verinin uzunlu�u
            int m = data.Length;
            int r = 0;

            // Gerekli parity bit say�s�n� hesaplar
            while (Math.Pow(2, r) < m + r + 1)
            {
                r++;
            }

            int n = m + r; // Toplam bit say�s�
            char[] hammingCode = new char[n];


            // Veriyi ve parity bitlerini yerle�tirir
            int j = 0;
            for (int i = 0; i < n; i++)
            {
                if (IsPowerOfTwo(i + 1))
                {
                    hammingCode[i] = 'P'; // Ge�ici olarak parity bit yerlerini atar
                }
                else
                {
                    hammingCode[i] = data[j];
                    j++;
                }
            }

            // Parity bitlerini hesapla ve yerle�tirir
            for (int i = 0; i < r; i++)
            {
                int parityPos = (int)Math.Pow(2, i);
                int parity = 0;

                for (int k = parityPos - 1; k < n; k += 2 * parityPos)
                {
                    for (int l = 0; l < parityPos; l++)
                    {
                        if (k + l < n && hammingCode[k + l] != 'P')
                        {
                            parity ^= (hammingCode[k + l] - '0');
                        }
                    }
                }

                hammingCode[parityPos - 1] = (char)(parity + '0');
            }

            return new string(hammingCode);
        }

        private bool IsPowerOfTwo(int x)
        {
            return (x & (x - 1)) == 0;
        }

        private string CalculateSyndrome(string originalCode, string corruptedCode)
        {
            int n = originalCode.Length;
            int r = 0;

            // Gerekli parity bit say�s�n� hesaplar
            while (Math.Pow(2, r) < n + 1)
            {
                r++;
            }

            // Sendrom kelimesini hesaplar
            int syndrome = 0;

            for (int i = 0; i < r; i++)
            {
                int parityPos = (int)Math.Pow(2, i) - 1;
                int parityValue = 0;

                for (int j = parityPos; j < n; j += 2 * (parityPos + 1))
                {
                    for (int k = j; k < j + parityPos + 1 && k < n; k++)
                    {
                        parityValue ^= (corruptedCode[k] - '0');
                    }
                }

                if (parityValue == 1)
                {
                    syndrome += parityPos + 1;
                }
            }

            return Convert.ToString(syndrome, 2).PadLeft(r, '0');
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // A��lacak linki tutar
            string url = "https://github.com/YusufBalmumcu/HammingCodeSimulasyon";

            // Linki a�ar
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Link acilamadi: " + ex.Message);
            }
        }
    }
}


