using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ElGamalCryptosystem
{
    public partial class Main : Form 
    {
        private OpenFileDialog openFile;
        private SaveFileDialog saveFile;
        private string extension = "";
        private const int OUTUP_ELEM = 25;
        private byte[] plainBytes, cipherBytes;

        private string errorEmptyFile = "The file is empty and does not contain any text for encryption / decryption.",
            errorCaption = "Error!",
            errorFormat = "The input string was not in the correct format.",
            invalidBreak = "Invalid public key!!!";

        public Main()
        {
            InitializeComponent();
            openFile = InitializeOpenFile();
            
        }

        private OpenFileDialog InitializeOpenFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "All files(*.*)|*.*";
            file.AddExtension = true;
            file.Title = "Open text";
            return file;
        }

        private SaveFileDialog InitializeSaveFile()
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = $"*{extension}|*{extension}";
            file.AddExtension = true;
            file.Title = "Save text";
            return file;
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            saveFile = InitializeSaveFile();
            if (saveFile.ShowDialog(this) == DialogResult.OK)
            {
                File.WriteAllBytes(saveFile.FileName, cipherBytes);
            }
        }

        private void buttonEncrypt_Click(object sender, EventArgs e)
        {
            cipherTextBox.Text = "";
           
            if (EncryptRadioButton.Checked)
            {
                
                Crypt(int.Parse(pTextBox.Text), int.Parse(GComboBox.Text),
                    int.Parse(xTextBox.Text));
                int countOutputElement = (cipherBytes.Length > OUTUP_ELEM*2)
                    ? OUTUP_ELEM * 2
                    : cipherBytes.Length;
                for (int i = 0; i < countOutputElement; i = i + 2)
                    cipherTextBox.Text += TransformToShort(cipherBytes[i], cipherBytes[i + 1]) + "  ";
            }
            else
            {
                Decrypt(int.Parse(pTextBox.Text), int.Parse(xTextBox.Text));
                int countOutputElement = (cipherBytes.Length > OUTUP_ELEM)
                    ? OUTUP_ELEM
                    : cipherBytes.Length;

                for (int i = 0; i < countOutputElement; i++)
                    cipherTextBox.Text += cipherBytes[i] + "  ";
            }
            
        }

        private void Crypt(int p, int g, int x)
        {
            int y = FastExp(g,x,p);
            yTextBox.Text = y.ToString();

            int j;
            cipherBytes = new byte[plainBytes.Length * 4];
            Random random = new Random();
            for (int i = 0; i < plainBytes.Length; i++)
            {
                int k = random.Next(2, p - 2);
                while (Gcd(k, p - 1) != 1)
                {
                    k = random.Next(2, p - 2);
                }

                j = 4 * i;
                //a = g^k % p
                int a = FastExp(g, k, p);
                int b = (FastExp(y, k, p)*(plainBytes[i]%p))%p;

                byte[] mas = TransformToBytes(a);
                cipherBytes[j] = mas[0];
                cipherBytes[j + 1] = mas[1];

                mas = TransformToBytes(b);
                cipherBytes[j + 2] = mas[0];
                cipherBytes[j + 3] = mas[1];
            }
        }

        private int Gcd(int a, int b)
        {
            if (b == 0)
            {
                return a;
            }
            else
            {
                return Gcd(b, a % b);
            }
        }

        private void Decrypt(int p, int x)
        {
            cipherBytes = new byte[plainBytes.Length/4];

            int j;
            for (int i = 0; i < cipherBytes.Length; i++)
            {
                j = i * 4;
                int a = TransformToShort(plainBytes[j],plainBytes[j + 1]);
                int b = TransformToShort(plainBytes[j + 2], plainBytes[j + 3]);
                if (a > 0 && b > 0)
                {
                    int buf = ((b%p) * FastExp(a, p - 1 - x, p)) % p;
                    cipherBytes[i] = (byte)buf;
                }
                else
                {
                    cipherBytes[i] = 0;
                }
                
            }
        }
        
        //x = a^b % n
        private int FastExp(int a, int b, int n)
        {
            int aTemp = a, bTemp = b, x = 1;
            while (bTemp != 0)
            {
                while ((bTemp % 2) == 0)
                {
                    bTemp = bTemp / 2;
                    aTemp = (aTemp * aTemp) % n;
                }

                bTemp--;
                x = (x * aTemp) % n;
            }

            return x;
        }

        private List<string> RootG(int p)
        {
            List<int> list = new List<int>();
            for (int i = 2; i <= p - 2; i++)
            {
                if ((Gcd(p - 1, i) == i) && SimpleNumber(Gcd(p - 1, i)))
                    list.Add(Gcd(p-1,i));
            }

            List<string> result =  new List<string>();
            int r = 1;
            while (r < p - 1)
            {
                r++;
                bool isRoot = true;
                for (int i = 0; i < list.Count; i++)
                    if (FastExp(r,(p-1)/list[i],p) == 1)
                    {
                        isRoot = false;
                    }

                if (isRoot)
                {
                    result.Add(r.ToString());
                }
            }

            return result;
        }

        private bool SimpleNumber(int number)
        {
            for(int i = 2; i < Math.Sqrt(number); i++)
                if (number % i == 0)
                    return false;
            return true;
        }

        public delegate List<string> GetList(int p);
        private void AddItemInComboBox(GetList del,ComboBox comboBox, int p)
        {
            List<string> list = del?.Invoke(p);
            comboBox.Items.Clear();
            foreach (var g in list)
            {
                comboBox.Items.Add(g);
            }
        }

        private void plainTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (pTextBox.Text != "")
                {
                    int p = int.Parse(pTextBox.Text);

                    if (p >= 256 && p <= 3000 && SimpleNumber(p))
                    {
                        AddItemInComboBox(RootG,GComboBox,p);
                    }
                    else
                    {
                        GComboBox.Items.Clear();
                    }
                }
            }
            catch (Exception exception)
            {
            }

        }

        private void pTextBox_KeyPress(object sender, KeyPressEventArgs e)
       {
            
           char symbol = e.KeyChar;
           if (!(symbol >= '0' && symbol <= '9' || Convert.ToInt32(symbol) == Convert.ToInt32(Keys.Back)))
               e.Handled = true;

       }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFile.ShowDialog(this) == DialogResult.OK)
            {
                extension = Path.GetExtension(openFile.FileName);

                plainTextBox.Text = "";
                plainBytes = File.ReadAllBytes(openFile.FileName);
                if (plainBytes.Length != 0)
                {
                    
                    if (EncryptRadioButton.Checked)
                    {
                        int countOutputElement = (plainBytes.Length > OUTUP_ELEM)
                            ? OUTUP_ELEM
                            : plainBytes.Length;
                        for (int i = 0; i < countOutputElement; i++)
                            plainTextBox.Text += plainBytes[i] + "  ";
                    }
                    else
                    {
                        int countOutputElement = (plainBytes.Length > OUTUP_ELEM*2)
                            ? OUTUP_ELEM*2
                            : plainBytes.Length;
                        for (int i = 0; i < countOutputElement; i = i+2)
                            plainTextBox.Text += TransformToShort(plainBytes[i], plainBytes[i + 1]) + "  ";
                    }
                }
                else
                    EmptyFile();
            }
        }

        private void GComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int p = int.Parse(pTextBox.Text);
                int x = int.Parse(xTextBox.Text);
                bool cor = (p >= 256 && p <= 3000  && SimpleNumber(p) && plainBytes.Length != 0 && GComboBox.SelectedIndex > -1);
                buttonRun.Enabled = cor  && x < p - 1 && x > 1;
                buttonBreak.Enabled = cor && yTextBox.Text != "";
            }
            catch (Exception exception)
            {
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            int p = int.Parse(pTextBox.Text);
            int y = int.Parse(yTextBox.Text);
            int g = int.Parse(GComboBox.Text);

            int x = 0;
            for (int i = 2; i < p-1; i++)
            {
                if ((y % p) == (FastExp(g, i, p)))
                {
                    x = i;
                    break;
                }
            }

            xTextBox.Text = x.ToString();
            GComboBox.Text = g.ToString();
            if (x != 0)
            {
                Decrypt(p, x);

                int countOutputElement = (cipherBytes.Length > OUTUP_ELEM)
                    ? OUTUP_ELEM
                    : cipherBytes.Length;
                cipherTextBox.Text = "";
                for (int i = 0; i < countOutputElement; i++)
                    cipherTextBox.Text += cipherBytes[i] + "  ";
            }
            else
            {
                MessageBox.Show(invalidBreak, errorCaption, MessageBoxButtons.OK);
            }
        }

        private int TransformToShort(int a, int b)
        {
            int result = a;
            result = result << 8;
            result += b;

            return result;
        }

        private byte[] TransformToBytes(int number)
        {
            byte a = (byte) number;
            byte b  = (byte)(number >> 8);

            return new[] {b, a};
        }

        private void EmptyFile()
        {
            MessageBox.Show(errorEmptyFile, errorCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
