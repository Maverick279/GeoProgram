using System.Reflection;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1_出租车轨迹数据计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "出租车轨迹数据计算";
        }

        string[] lines, split;

        public void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择数据文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            string file = dialog.FileName;
            lines = File.ReadAllLines(file);
            textBox1.Clear();
            textBox1.AppendText("成功读取文件，内容如下：\r\n");
            for (int i = 0; i < lines.Length; i++)
            {
                textBox1.AppendText(lines[i] + "\r\n");
            }
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "请选择保存路径";
            dialog.Filter = "文本文件|*.txt|所有文件|*.*";
            dialog.DefaultExt = "文本文件|*.txt";
            dialog.ShowDialog();
            string file = dialog.FileName;
            if(file!="")
            {
                File.WriteAllText(file, textBox1.Text);
                textBox1.Clear();
                textBox1.AppendText("保存成功，已保存至"+file);
            }
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.AppendText("------------速度和方位角计算结果------------\r\n");
            string[] split;
            int[] N = new int[lines.Length];
            int[] Y = new int[lines.Length];
            int[] R = new int[lines.Length];
            int[] H = new int[lines.Length];
            int[] M = new int[lines.Length];
            int[] S = new int[lines.Length];
            double[] MJD = new double[lines.Length];
            float[] locx = new float[lines.Length];
            float[] locy = new float[lines.Length];
            double[] alpha = new double[lines.Length];
            double[] distance = new double[lines.Length];
            double totaldistance = 0.0;
            double[] velocity = new double[lines.Length];
            int[] time = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                split = lines[i].Split('，');//[0]：车辆标识、[1]：运营状态、[2]：北京时间、[3]：x坐标、[4]：y坐标
                locx[i] = float.Parse(split[3]);
                locy[i] = float.Parse(split[4]);
                N[i] = int.Parse(split[2].Substring(0, 4));
                Y[i] = int.Parse(split[2].Substring(4, 2));
                R[i] = int.Parse(split[2].Substring(6, 2));
                H[i] = int.Parse(split[2].Substring(8, 2));
                M[i] = int.Parse(split[2].Substring(10, 2));
                S[i] = int.Parse(split[2].Substring(12, 2));
                MJD[i] = 367 * N[i] - 678987 - (int)(7.0 / 4.0 * (N[i] + (int)((Y[i] + 9.0) / 12.0))) + (int)(275.0 * Y[i] / 9.0) + R[i] + (H[i] / 24.0) + (M[i] / 1440.0) + (S[i] / 86400.0);
                if (i > 0)
                {
                    double resultalpha = Math.Atan((locy[i] - locy[i - 1]) / (locx[i] - locx[i-1]))/Math.PI*180;
                    if (resultalpha < 0)
                        alpha[i] = resultalpha + 360;
                    else if (resultalpha > 360)
                        alpha[i] = resultalpha - 360;
                    else
                        alpha[i] = resultalpha;
                    distance[i] = Math.Sqrt(Math.Pow(locx[i] - locx[i - 1], 2)+Math.Pow(locy[i] - locy[i-1],2));
                    totaldistance += distance[i];
                    time[i] = S[i] - S[i - 1] + (M[i] - M[i - 1]) * 60 + (H[i] - H[i - 1]) * 3600;//秒
                    velocity[i] = distance[i] / time[i] *3.6;
                }
            }
            for (int j=1;j<lines.Length;j++)
            {
                if(j<=10)
                    textBox1.AppendText("0"+(j-1)+" , "+MJD[j-1].ToString("0.00000") + " - " + MJD[j].ToString("0.00000") + " , " + velocity[j].ToString("0.000") + " , " + alpha[j].ToString("0.000") + "\r\n");
                else
                    textBox1.AppendText((j-1) + " , " + MJD[j - 1].ToString("0.00000") + " - " + MJD[j].ToString("0.00000") + " , " + velocity[j].ToString("0.000") + " , " + alpha[j].ToString("0.000") + "\r\n");
            }
            double fldistance = Math.Sqrt(Math.Pow(locx[0] - locx[lines.Length - 1], 2) + Math.Pow(locy[0] - locy[lines.Length - 1], 2));
            textBox1.AppendText("----------距离计算结果----------\r\n");
            textBox1.AppendText("累积距离：" + (totaldistance/1000).ToString("0.000")+"（km）"+"\r\n");
            textBox1.AppendText("首尾直线距离：" + (fldistance / 1000).ToString("0.000") + "（km）");
        }
    }
}