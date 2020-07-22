using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgaShmoga
{
    public partial class MainForm : Form
    {
        int count = 0;
        Random rnd;
        char[] spec_chars = new char[] { '%', '*', '#', '?', '$', '&', '~', ')' };
        Dictionary<string, double> metrika;
        public MainForm()
        {
            InitializeComponent();
            rnd = new Random();
            metrika = new Dictionary<string, double>();
            metrika.Add("mm", 1);
            metrika.Add("cm", 10);
            metrika.Add("dm", 100);
            metrika.Add("m", 1000);
            metrika.Add("km", 1000000);
            metrika.Add("mile", 1609344);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadNotepad(2);
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmiAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\"В рот мне ноги, этож крутая программа!\" (с) Джейсон Стетхэм.", "Разработано Непомнящих А.А. в 2020 году");
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            count++;
            lblCount.Text = count.ToString();
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            count--;
            lblCount.Text = count.ToString();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            count = 0;
            lblCount.Text = count.ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        
        private void btnRandom_Click(object sender, EventArgs e)
        {
            int max = Convert.ToInt32(nudTo.Value);
            int min = Convert.ToInt32(nudFrom.Value);
            if (nudFrom.Value > nudTo.Value)
            {
                nudTo.Value = min;
                nudFrom.Value = max;
            }
            
            int n;
            n = rnd.Next(Convert.ToInt32(nudFrom.Value), Convert.ToInt32(nudTo.Value)+1);
            lblRandom.Text = n.ToString();
            if (cbRandom.Checked) 
            {
                if (tbRandom.Text.IndexOf(n.ToString()) == -1)
                {
                    tbRandom.AppendText(n + " \r\n");
                }
            }
            else
            {
                tbRandom.AppendText(n + "\r\n");
            }
        }

        private void btnRandomClear_Click(object sender, EventArgs e)
        {
            tbRandom.Clear();
            lblRandom.Text = "0";
        }

        private void btnRandomCopy_Click(object sender, EventArgs e)
        {
            if (tbRandom.Text != "")
            {
                Clipboard.SetText(tbRandom.Text);
            }
            else
            {
                MessageBox.Show("Нечего копировать!", "Что-то не так");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tsmiInsertDate_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortDateString()+"\r\n");
        }

        private void tsmiInsertTime_Click(object sender, EventArgs e)
        {
            rtbNotepad.AppendText(DateTime.Now.ToShortTimeString() + "\r\n");
        }
        void SaveNotepad()
        {
            try
            {
                rtbNotepad.SaveFile("notepad.rtf");
            }
            catch
            {
                MessageBox.Show("Ошибка при сохранении!");
            }
        }
        private void tsmiSave_Click(object sender, EventArgs e)
        {
            SaveNotepad();
        }

        void LoadNotepad(int i)
        {
            try
            {
                rtbNotepad.LoadFile("notepad.rtf");
            }
            catch
            {
                if (i == 1) MessageBox.Show("Загрузить файл не получилось, может быть файла еще нет?");
                else if (i == 2) rtbNotepad.SaveFile("notepad.rtf");
            }
        }
    
        private void tsmiLoad_Click(object sender, EventArgs e)
        {
        LoadNotepad(1);
        }

        private void tsmiEmpty_Click(object sender, EventArgs e)
        {
            rtbNotepad.Text = "";
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            const string message = "Сохранить файл?";
            const string caption = "Закрытие программы";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
                SaveNotepad();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCreatePassword_Click(object sender, EventArgs e)
        {
            if (clbPassword.CheckedItems.Count == 0) return;
            string password = "";
            for (int i=0; i < nudPassLength.Value; i++)
            {
                int n = rnd.Next(0, clbPassword.CheckedItems.Count);
                string s = clbPassword.CheckedItems[n].ToString();
                switch (s)
                {
                    case "Цифры": password += rnd.Next(10).ToString();
                        break;
                    case "Прописные буквы": password += Convert.ToChar(rnd.Next (65, 88));
                        break;
                    case "Строчные буквы": password += Convert.ToChar(rnd.Next(97, 122));
                        break;
                    default: password += spec_chars[rnd.Next(spec_chars.Length)];
                        break;
                }
                tbPassword.Text = password;
                Clipboard.SetText(password);
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            double m1 = metrika[cbFrom.Text];
            double m2 = metrika[cbTo.Text];
            try
            {
                double n = Convert.ToDouble(tbFrom.Text);
                tbTo.Text = (n * m1 / m2).ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("Укажите число для конвертирования!");
            }

        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            string t = cbFrom.Text;
            cbFrom.Text = cbTo.Text;
            cbTo.Text = t;
        }

        private void cbMetric_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbMetric.Text)
            {
                case "длина":
                    metrika.Clear();
                    metrika.Add("mm", 1);
                    metrika.Add("cm", 10);
                    metrika.Add("dm", 100);
                    metrika.Add("m", 1000);
                    metrika.Add("km", 1000000);
                    metrika.Add("mile", 1609344);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("mm");
                    cbFrom.Items.Add("cm");
                    cbFrom.Items.Add("dm");
                    cbFrom.Items.Add("m");
                    cbFrom.Items.Add("km");
                    cbFrom.Items.Add("mile");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("mm");
                    cbTo.Items.Add("cm");
                    cbTo.Items.Add("dm");
                    cbTo.Items.Add("m");
                    cbTo.Items.Add("km");
                    cbTo.Items.Add("mile");
                    cbFrom.Text = "mm";
                    cbTo.Text = "mm";
                    break;

                case "вес":
                    metrika.Clear();
                    metrika.Add("g", 1);
                    metrika.Add("kg", 1000);
                    metrika.Add("ct", 100000);
                    metrika.Add("t", 1000000);
                    metrika.Add("lb", 453.6);
                    metrika.Add("oz", 283);
                    cbFrom.Items.Clear();
                    cbFrom.Items.Add("g");
                    cbFrom.Items.Add("kg");
                    cbFrom.Items.Add("ct");
                    cbFrom.Items.Add("t");
                    cbFrom.Items.Add("lb");
                    cbFrom.Items.Add("oz");
                    cbTo.Items.Clear();
                    cbTo.Items.Add("g");
                    cbTo.Items.Add("kg");
                    cbTo.Items.Add("ct");
                    cbTo.Items.Add("t");
                    cbTo.Items.Add("lb");
                    cbTo.Items.Add("oz");
                    cbFrom.Text = "g";
                    cbTo.Text = "g";
                    break;
            }
        }
    }
    }

