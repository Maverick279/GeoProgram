namespace _2_矩阵卷积计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "矩阵卷积计算";
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.ReadOnly = true;   
        }

        string[] sourcem,sourcen;
        double[,] datam, datan;
        public void ToolOpenM_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开M矩阵文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            string filem = dialog.FileName;
            if(filem!="")
            {
                sourcem = File.ReadAllLines(filem);
                textBox1.AppendText("M矩阵文件内容如下：\r\n");
            }
            datam = new double[sourcem.Length,sourcem.Length];
            for (int i=0;i<sourcem.Length;i++)
            {
                string[] lines = sourcem[i].Split(" ");
                textBox1.AppendText(sourcem[i] + "\r\n");
                for(int j=0;j<lines.Length;j++)
                {
                    double result = double.Parse(lines[j]);
                    datam[i,j] = result;   
                }
            }
        }

        public void ToolOpenN_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "打开N矩阵文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            string filem = dialog.FileName;
            if (filem != "")
            {
                sourcen = File.ReadAllLines(filem);
                textBox1.AppendText("N矩阵文件内容如下：\r\n");
            }
            datan = new double[sourcen.Length,sourcen.Length];
            for (int i = 0; i < sourcen.Length; i++)
            {
                textBox1.AppendText(sourcen[i] + "\r\n");
                string[] lines = sourcen[i].Split(" ");
                for (int j=0;j<lines.Length;j++)
                {
                    datan[i,j] = double.Parse(lines[j]);
                }
            }
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            method1();
            method2();
        }

        public void method1()
        {
            textBox1.AppendText("测试数据计算结果\r\n算法1结果：\r\n");
            double[,] V = new double[sourcen.Length,sourcen.Length];
            double sub = 0;
            double sum_m = 0;
            for(int I=0;I<sourcen.Length;I++)
            {
                for(int J=0;J<sourcen.Length;J++)
                {
                    for(int i=0;i<3;i++)
                    {
                        for(int j=0;j<3;j++)
                        {
                            if (I-i-1<0||J-j-1<0||I-i-1>9||J-j-1>9)
                                sub += 0;
                            else
                            {
                                sub += (datam[i, j] * datan[I - i - 1, J - j - 1]);
                                sum_m += datam[i, j];
                            }
                        }
                    }
                    V[I, J] = sub / sum_m;
                    sub = 0.0;
                    sum_m = 0.0;//这里要记得重置一下两个结果 否则卷积矩阵的元素值会越来越大
                    if (V[I, J] == 0.0)
                        textBox1.AppendText("NaN ");
                    else
                        textBox1.AppendText(V[I, J].ToString("0.00") + " ");
                }
                textBox1.AppendText("\r\n");
            }
        }

        public void method2()
        {
            textBox1.AppendText("算法2结果：\r\n");
            double[,] V = new double[sourcen.Length, sourcen.Length];
            double sub = 0;
            double sum_m = 0;
            for (int I = 0; I < sourcen.Length; I++)
            {
                for (int J = 0; J < sourcen.Length; J++)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (I - i - 1 < 0 || J - j - 1 < 0 || I - i - 1 > 9 || J - j - 1 > 9)
                                sub += 0;
                            else
                            {
                                sub += (datam[i, j] * datan[9-(I - i - 1),9-(J - j - 1)]);
                                sum_m += datam[i, j];
                            }
                        }
                    }
                    V[I, J] = sub / sum_m;
                    sub = 0.0;
                    sum_m = 0.0;//这里要记得重置一下两个结果 否则卷积矩阵的元素值会越来越大
                    if (V[I, J] == 0.0)
                        textBox1.AppendText("NaN ");
                    else
                        textBox1.AppendText(V[I, J].ToString("0.00") + " ");
                }
                textBox1.AppendText("\r\n");
            }
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "文本文件|*.txt";
            dialog.Title = "选择保存路径";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                string file = dialog.FileName;
                File.WriteAllText(file, textBox1.Text);
                textBox1.Clear();
                textBox1.AppendText("已将结果文件保存至" + file + "\r\n");
            }
        }

        private void ToolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2022-11-27晚\nKUST\n测绘程序设计习题\n2_矩阵卷积计算");
        }
    }
}