using System.Runtime.Intrinsics.X86;

namespace _3_利用线性回归模型进行卫星轨道的预报
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "利用线性回归模型进行卫星轨道的预报";
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.ReadOnly = true;
        }

        int[] time;
        double[] locx, locy, locz;
        string[] text;
        double[] beta0 = new double[3];
        double[] beta1 = new double[3];

        public void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择轨道文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            if(dialog.FileName!="")
            {
                string file = dialog.FileName;
                text = File.ReadAllLines(file);
                time = new int[text.Length];
                locx = new double[text.Length]; 
                locy = new double[text.Length];
                locz = new double[text.Length];
                for(int i = 0; i < text.Length; i++)
                {
                    textBox1.AppendText(text[i]+"\r\n");
                    time[i] = Int32.Parse(text[i].Split("，")[0]);
                    locx[i] = double.Parse(text[i].Split("，")[1]);
                    locy[i] = double.Parse(text[i].Split("，")[2]);
                    locz[i] = double.Parse(text[i].Split("，")[3]);
                }
            }
            textBox1.AppendText("\r\n");
        }

        public void coefficient(double[] y,double avgy,int avgtime,int flag)
        {
            double numera = 0;
            double denomina = 0;
            for (int j = 0; j < text.Length; j++)
            {
                numera += (time[j] - avgtime) * (y[j] - avgy);
                denomina += Math.Pow(time[j] - avgtime, 2);
            }
            beta1[flag] = numera / denomina;
            beta0[flag] = avgy - beta1[flag] * avgtime;
            numera = 0;
            denomina = 0;
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            int sumtime = 0;
            double sumx = 0;
            double sumy = 0;
            double sumz = 0;
            for(int i=0;i<text.Length;i++)
            {
                sumtime += time[i];
                sumx += locx[i];
                sumy += locy[i];
                sumz += locz[i];
            }
            int avgtime = sumtime / text.Length;
            double avgx = sumx / text.Length;
            double avgy = sumy / text.Length;
            double avgz = sumz / text.Length;
            int flag = 0;
            coefficient(locx, avgx,avgtime,flag);
            flag++;
            coefficient(locy, avgy,avgtime,flag);
            flag++;
            coefficient(locz, avgz,avgtime,flag);
            textBox1.AppendText("计算成果\r\n\r\n回归系数：\r\n");
            for (int j=0;j<beta0.Length;j++)
            {
                textBox1.AppendText(beta0[j].ToString("0.00000") + "，" + beta1[j].ToString("0.00000") + "\r\n");
            }
            textBox1.AppendText("\r\n预报结果：\r\n");
            for(int timepredict = 4200;timepredict<=4800;timepredict+=300)
            {
                textBox1.AppendText(timepredict.ToString()+"，");
                double xpredict = beta0[0] + beta1[0] * timepredict;
                textBox1.AppendText(xpredict.ToString("0.000")+"，");
                double ypredict = beta0[1] + beta1[1] * timepredict;
                textBox1.AppendText(ypredict.ToString("0.000") + "，");
                double zpredict = beta0[2] + beta1[2] * timepredict;
                textBox1.AppendText(zpredict.ToString("0.000") + "\r\n");
            }
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "选择保存路径";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            if(dialog.FileName!="")
            {
                string file = dialog.FileName;
                File.WriteAllText(file, textBox1.Text);
                textBox1.Clear();
                textBox1.AppendText("已将结果保存至" + file);
            }
        }

        private void ToolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2022-11-27晚\r\nKUST\r\n测绘程序设计\r\n3_利用线性回归模型进行卫星轨道的预报");
        }
    }
}