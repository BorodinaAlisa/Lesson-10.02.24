using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Lesson_10._02._24
{
    public partial class Form1 : Form
    {
        private Color fontColor = Color.Black;
        public Form1()
        {
            InitializeComponent();
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void светлаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.Black;
            richTextBox1.BackColor = Color.White;
            menuStrip1.BackColor = Color.White;
        }
        private void темнаяТемаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.ForeColor = Color.White;
            richTextBox1.BackColor = Color.DimGray;
            menuStrip1.BackColor = Color.DarkGray;
        }
        private void ШрифтToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog Font1 = new FontDialog();
            if (Font1.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.Font = Font1.Font;
            }
        }
        private void цветШрифтаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cDialog = new ColorDialog();
            if (cDialog.ShowDialog() == DialogResult.OK)
            {
                fontColor = cDialog.Color;
                richTextBox1.ForeColor = fontColor;
            }
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }
        private void открытьToolStripMenuItem1_Click(object sender, EventArgs e)
        { 
            OpenFileDialog Fdialog = new OpenFileDialog();
            Fdialog.Filter = "All files (*.*)|*.*";
            if (Fdialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    richTextBox1.Text = File.ReadAllText(Fdialog.FileName);
                    MessageBox.Show("Файл успешно открыт.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось открыть файл: " + ex.Message);
                }
            }
        }
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "All files (*.*)|*.*";
            try 
            {
                File.WriteAllText(saveFileDialog.FileName, richTextBox1.Text);
                MessageBox.Show("Файл успешно сохранен.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить файл: " + ex.Message);
            }
        }
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog Fdialog = new SaveFileDialog();
            Fdialog.Filter = "All files (*.*)|*.*";
            if (Fdialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (!string.IsNullOrEmpty(Fdialog.FileName))
                    {
                        File.WriteAllText(Fdialog.FileName, richTextBox1.Text);
                        MessageBox.Show("Файл успешно сохранен.");
                    }
                    else    
                    {
                        MessageBox.Show("Не указан путь к файлу.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось сохранить файл: " + ex.Message);
                }
            }
        }
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintDocument pDocument = new PrintDocument();
            pDocument.PrintPage += PrintPageH;
            PrintDialog pDialog = new PrintDialog();
            pDialog.Document = pDocument;
            if (pDialog.ShowDialog() == DialogResult.OK)
            {
                try 
                {
                    pDialog.Document.Print();
                    MessageBox.Show("Файл успешно напечатан.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Не удалось напечатать файл: " + ex.Message);
                }
            }
        }
        public void PrintPageH(object sender, PrintPageEventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    using (SolidBrush brush = new SolidBrush(colorDialog.Color))
                    {
                        float x = 20; // отступ слева
                        float y = 20; // отступ сверху
                        e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, brush, x, y);
                    }
                }
            }
        }
        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Название программы: Блокнот\nАвтор: Бородина Алиса", "О программе", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void вырезатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
                richTextBox1.SelectedText = "";
            }
        }
        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.SelectedText != "")
            {
                Clipboard.SetText(richTextBox1.SelectedText);
            }
        }
        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                richTextBox1.SelectedText = Clipboard.GetText();
            }
        }
    } 
}
