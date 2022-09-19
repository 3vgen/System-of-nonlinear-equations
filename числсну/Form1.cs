using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace числсну
{
    public partial class Form1 : Form
    {
        public double[,] koefs;
        int vid;
        List<Label> labels1;
        List<Label> labels2;
        List<TextBox> boxes1;
        List<TextBox> boxes2;
        public Form1()
        {
            InitializeComponent();
            ToolStripMenuItem deletemenu = new ToolStripMenuItem("Очистить");
            splitContainer1.Panel1.ContextMenuStrip = contextMenuStrip1;
            deletemenu.Click += Deletemenu_Click;
            contextMenuStrip1.Items.Add(deletemenu);
            koefs = new double[3, 4];
            vid = 1;
            labels1 = new List<Label>()
            {
              label15,
              label16,
              label17,
              label18,
              label19,
              label20,
              label21,
              label22,
              label23,
              label24,
              label25,
              label26,
            };
            boxes1 = new List<TextBox>()
            {
                textBox13,
                textBox14,
                textBox15,
                textBox16,
                textBox17,
                textBox18,
                textBox19,
                textBox20,
                textBox21,
                textBox22,
                textBox23,
                textBox24,
            };
            labels2 = new List<Label>()
            {               
                label32,
                label33,
                label35,label36,label37,label38,label39,label40,label41,label42,label43,label44,label45,label46,label33   
            };
            boxes2 = new List<TextBox>()
            {
                textBox30,textBox31,textBox33,textBox34,textBox35,textBox36,textBox37,textBox38
            };
            foreach (var item in boxes2) item.Visible = false;
            foreach (var item in labels2) item.Visible = false;
        }

        private void Deletemenu_Click(object sender, EventArgs e)
        {
            foreach (var item in boxes1) item.Text = "";
            foreach (var item in boxes2) item.Text = "";
            listBox1.Items.Clear();
            listBox2.Items.Clear();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            
        }

        public void TODoubleKoefs(double[,] koefs, int vid)
        {
            if (vid == 1)
            {
                
                koefs[0, 0] = Convert.ToDouble(textBox13.Text);
                koefs[0, 1] = Convert.ToDouble(textBox14.Text);
                koefs[0, 2] = Convert.ToDouble(textBox15.Text);
                koefs[0, 3] = Convert.ToDouble(textBox16.Text);

                koefs[1, 0] = Convert.ToDouble(textBox17.Text);
                koefs[1, 1] = Convert.ToDouble(textBox18.Text);
                koefs[1, 2] = Convert.ToDouble(textBox19.Text);
                koefs[1, 3] = Convert.ToDouble(textBox20.Text);

                koefs[2, 0] = Convert.ToDouble(textBox21.Text);
                koefs[2, 1] = Convert.ToDouble(textBox22.Text);
                koefs[2, 2] = Convert.ToDouble(textBox23.Text);
                koefs[2, 3] = Convert.ToDouble(textBox24.Text);
            }
            if (vid == 2)
            {
       
                koefs[0, 0] = Convert.ToDouble(textBox38.Text);
                koefs[0, 1] = Convert.ToDouble(textBox37.Text);
                koefs[0, 2] = Convert.ToDouble(textBox36.Text);
                koefs[0, 3] = Convert.ToDouble(textBox35.Text);

                koefs[1, 0] = Convert.ToDouble(textBox34.Text);
                koefs[1, 1] = Convert.ToDouble(textBox33.Text);
                koefs[1, 2] = Convert.ToDouble(textBox31.Text);
                koefs[1, 3] = Convert.ToDouble(textBox30.Text);

                koefs[2, 0] = 0;
                koefs[2, 1] = 0;
                koefs[2, 2] = 0;
                koefs[2, 3] = 0;
            }

        }
       

        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDlg = new FolderBrowserDialog();
            folderDlg.ShowNewFolderButton = true;
            // Show the FolderBrowserDialog.  
            DialogResult result = folderDlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBox25.Text = folderDlg.SelectedPath;
                MessageBox.Show("Допишите необходимый файл");
            }
        }

        private void button2_Click(object sender, EventArgs e) //Метод Ньютона
        {
            try
            {
                double[] f;
                double[] n;
                double[] temp;
                double[,] a;
                double[,] a1;
                listBox1.Items.Clear();
                int i = 0;
                double x0, y0, z0, eps;
                TODoubleKoefs(koefs, vid);
                f = new double[3];
                n = new double[3];
                temp = new double[3];
                a = new double[3, 3];
                a1 = new double[3, 3];

                x0 = Convert.ToDouble(textBox1.Text);
                y0 = Convert.ToDouble(textBox2.Text);
                z0 = Convert.ToDouble(textBox3.Text);
                eps = Convert.ToDouble(textBox4.Text);

                n[0] = x0;
                n[1] = y0;
                n[2] = z0;

                newton c2 = new newton();
                if(vid == 1)
                {
                    do
                    {
                        c2.fx(n[0], n[1], n[2], f, koefs, vid);
                        c2.jacobi(n[0], n[1], n[2], a, koefs, vid);
                        c2.inverse(a, a1, vid);
                        c2.mult(a1, f, temp, vid);
                        c2.minus(n, temp, n, vid);
                        i++;
                        if (double.IsNaN(n[0]) || double.IsNaN(n[1]) || double.IsNaN(n[2]))
                        {
                            MessageBox.Show("Метод не сошелся");
                            break;
                        }
                        listBox1.Items.Add($"{i}) x = {n[0]}, y = {n[1]}, z = {n[2]}");
                    }
                    while ((Math.Abs(f[0]) < eps && Math.Abs(f[1]) < eps && Math.Abs(f[2]) < eps) != true);
                    textBox9.Text = Convert.ToString(n[0]);
                    textBox12.Text = Convert.ToString(n[1]);
                    textBox10.Text = Convert.ToString(n[2]);
                    textBox11.Text = Convert.ToString(i);

                    double first = koefs[0, 0] * n[0] + koefs[0, 1] * n[0] * n[0] + koefs[0, 2] * n[2] * n[1];
                    double second = koefs[0, 0] * n[1] + koefs[0, 1] * n[1] * n[1] + koefs[0, 2] * n[0] * n[2];
                    double third = koefs[0, 0] * n[2] + koefs[0, 1] * n[2] * n[2] + koefs[0, 2] * n[0] * n[1];

                    textBox26.Text = first.ToString();
                    textBox27.Text = second.ToString();
                    textBox28.Text = third.ToString();
                }
                if(vid == 2)
                {
                    do
                    {

                        c2.fx(n[0], n[1], n[2], f, koefs, vid);
                        c2.jacobi(n[0], n[1], n[2], a, koefs, vid);
                        c2.inverse(a, a1, vid);
                        c2.mult(a1, f, temp, vid);
                        c2.minus(n, temp, n, vid);
                        i++;
                        if (double.IsNaN(n[0]) || double.IsNaN(n[1])|| i >50)
                        {
                            MessageBox.Show("Метод не сошелся");
                            break;
                        }
                        listBox1.Items.Add($"{i}) x = {n[0]}, y = {n[1]}");

                    }
                    while ((Math.Abs(f[0]) < eps && Math.Abs(f[1]) < eps)!= true);
                    textBox9.Text = Convert.ToString(n[0]);
                    textBox12.Text = Convert.ToString(n[1]);
                    textBox11.Text = Convert.ToString(i);
                    double first = koefs[1, 1] * Math.Cos(koefs[1, 2] * n[1]) + n[0] * koefs[0, 0];
                    double second = koefs[0, 0] * Math.Sin(koefs[0, 1] * x0) + koefs[0, 2] * y0;
                    textBox27.Text = second.ToString();
                    textBox26.Text = first.ToString();
                }               
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
           
        }


        private void button1_Click(object sender, EventArgs e) //ПРОСТЫЕ ИТЕРАЦИИ       
        {
            try
            {
                if (vid == 1)
                {
                    listBox2.Items.Clear();
                    TODoubleKoefs(koefs, vid);
                    int i = 1;
                    double x0, y0, z0, x, y, z, eps;
                    x0 = Convert.ToDouble(textBox1.Text);
                    y0 = Convert.ToDouble(textBox2.Text);
                    z0 = Convert.ToDouble(textBox3.Text);
                    eps = Convert.ToDouble(textBox4.Text);

                    x = (koefs[0, 3] - x0 * x0 * koefs[0, 1] - y0 * z0 * koefs[0, 2]) / koefs[0, 0];
                    y = (koefs[1, 3] - y0 * y0 * koefs[1, 1] - x0 * z0 * koefs[1, 2]) / koefs[1, 0];
                    z = (koefs[2, 3] - z0 * z0 * koefs[2, 1] - x0 * y0 * koefs[2, 2]) / koefs[2, 0];

                    while (((Math.Abs(x - x0) < eps) && (Math.Abs(y - y0) < eps) && (Math.Abs(z - z0) < eps)) != true)
                    {
                        x0 = x;
                        y0 = y;
                        z0 = z;
                        x = (koefs[0, 3] - x0 * x0 * koefs[0, 1] - y0 * z0 * koefs[0, 2]) / koefs[0, 0];
                        y = (koefs[1, 3] - y0 * y0 * koefs[1, 1] - x0 * z0 * koefs[1, 2]) / koefs[1, 0];
                        z = (koefs[2, 3] - z0 * z0 * koefs[2, 1] - x0 * y0 * koefs[2, 2]) / koefs[2, 0];
                        i++;
                        if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z) || i>150)
                        {
                            MessageBox.Show("Метод не сошелся");
                            break;
                        }
                        listBox2.Items.Add($"{i}) x = {x}, y = {y}, z = {z}");
                    }
                    textBox5.Text = Convert.ToString(x);
                    textBox6.Text = Convert.ToString(y);
                    textBox7.Text = Convert.ToString(z);
                    textBox8.Text = Convert.ToString(i);
                }
                if(vid == 2)
                {
                    listBox2.Items.Clear();
                    TODoubleKoefs(koefs, vid);
                    int i = 1;
                    double x0, y0, x, y,  eps;
                    x0 = Convert.ToDouble(textBox1.Text);
                    y0 = Convert.ToDouble(textBox2.Text);
                    eps = Convert.ToDouble(textBox4.Text);
                    x = (koefs[1, 3] - koefs[1, 1] * Math.Cos(koefs[1, 2] * y0)) / koefs[0, 0];
                    y = (-koefs[0, 0] * Math.Sin(koefs[0, 1] * x0) + koefs[0, 3]) / koefs[0, 2];

                    while (((Math.Abs(x - x0) < eps) && (Math.Abs(y - y0) < eps)) != true)
                    {
                        x0 = x;
                        y0 = y;
                        listBox2.Items.Add($"{i}) x = {x0}, y = {y0}");
                        x = (koefs[1, 3] - koefs[1, 1] * Math.Cos(koefs[1, 2] * y0)) / koefs[1, 0]; //second
                        y = (-koefs[0, 0] * Math.Sin(koefs[0, 1] * x0) + koefs[0, 3]) / koefs[0, 2]; //first
                        i++;
                        if (double.IsNaN(x) || double.IsNaN(y) || i >150)
                        {
                            MessageBox.Show("Метод не сошелся");
                            break;
                        }
                        //listBox2.Items.Add($"{i}) x = {x}, y = {y}");
                    }
                    textBox5.Text = Convert.ToString(x);
                    textBox6.Text = Convert.ToString(y);
                    textBox8.Text = Convert.ToString(i);
                    
                }
               
            }
            catch
            {
                MessageBox.Show("Проверьте правильность введенных данных");
            }
            
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string[] lines = File.ReadAllLines(textBox25.Text);
                double[,] koefs = new double[lines.Length, lines[0].Split(' ').Length]; //преобразования с файла в двумерный массив
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                        koefs[i, j] = Convert.ToDouble(temp[j]);
                }
               if(vid == 1)
                {
                    textBox13.Text = koefs[0, 0].ToString();
                    textBox14.Text = koefs[0, 1].ToString();
                    textBox15.Text = koefs[0, 2].ToString();
                    textBox16.Text = koefs[0, 3].ToString();

                    textBox17.Text = koefs[1, 0].ToString();
                    textBox18.Text = koefs[1, 1].ToString();
                    textBox19.Text = koefs[1, 2].ToString();
                    textBox20.Text = koefs[1, 3].ToString();

                    textBox21.Text = koefs[2, 0].ToString();
                    textBox22.Text = koefs[2, 1].ToString();
                    textBox23.Text = koefs[2, 2].ToString();
                    textBox24.Text = koefs[2, 3].ToString();
                }
               if(vid == 2)
                {
                    textBox38.Text = koefs[0, 0].ToString();
                    textBox37.Text = koefs[0, 1].ToString();
                    textBox36.Text = koefs[0, 2].ToString();
                    textBox35.Text = koefs[0, 3].ToString();

                    textBox34.Text = koefs[1, 0].ToString();
                    textBox33.Text = koefs[1, 1].ToString();
                    textBox31.Text = koefs[1, 2].ToString();
                    textBox30.Text = koefs[1, 3].ToString();

                  
                }
              
            }
            catch
            {
                MessageBox.Show("Вы ввели некорректный файл либо недописали его");
            }
            
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            if (vid == 1)
            {
                label34.Visible = false;
                label47.Visible = true;
                foreach (var item in boxes1) item.Visible = false;
                foreach (var item in labels1) item.Visible = false;
                foreach (var item in boxes2) item.Visible = true;
                foreach (var item in labels2) item.Visible = true;
                vid = 2;
            }
            else
            {
                label47.Visible = false;
                label34.Visible = true;
                foreach (var item in boxes1) item.Visible = true;
                foreach (var item in labels1) item.Visible = true;
                foreach (var item in boxes2) item.Visible = false;
                foreach (var item in labels2) item.Visible = false;
                vid = 1;
            }
        }

        private void textBox19_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {

        }
        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void textBox33_TextChanged(object sender, EventArgs e)
        {

        }
        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
