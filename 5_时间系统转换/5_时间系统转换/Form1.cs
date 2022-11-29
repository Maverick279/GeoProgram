namespace _5_时间系统转换
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "时间系统转换";
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.ReadOnly = true;
        }

        string[] text;
        int[] year, month, day, hour, minute;
        double[] second;

        public void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择时间数据文件";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                string path = dialog.FileName;
                text = File.ReadAllLines(path);
                year = new int[text.Length];
                month = new int[text.Length];
                day = new int[text.Length];
                hour = new int[text.Length];
                minute = new int[text.Length];
                second = new double[text.Length];
                for (int i = 0; i < text.Length; i++)
                {
                    textBox1.AppendText(text[i] + "\r\n");
                    year[i] = int.Parse(text[i].Split(" ")[0]);
                    month[i] = int.Parse(text[i].Split(" ")[1]);
                    day[i] = int.Parse(text[i].Split(" ")[2]);
                    hour[i] = int.Parse(text[i].Split(" ")[3]);
                    minute[i] = int.Parse(text[i].Split(" ")[4]);
                    second[i] = double.Parse(text[i].Split(" ")[5]);
                }
            }
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            double[] JD = new double[text.Length];
            textBox1.AppendText("-------JD-------\r\n");
            for(int i=0;i<year.Length;i++)//公历转儒略日
            {
                JD[i] = 1721013.5+ 367 * year[i] - (int)(7.0 / 4.0 * (year[i] + (int)((month[i] + 9) / 12.0))) + (int)(275 * month[i] / 9.0) + day[i] + (hour[i] / 24.0) + (minute[i] / 1440.0) + (second[i] / 86400.0);
                textBox1.AppendText(JD[i].ToString("0.00000")+"\r\n");
            }
            textBox1.AppendText("-------公历（年 月 日 时：分：秒）-------\r\n");
            for(int j=0;j<JD.Length;j++)
            {
                double a = (int)(JD[j] + 0.5);
                double b = a + 1537;
                double c = (int)((b - 122.1) / 365.25);
                double d = (int)(365.25 * c);
                double ee = (int)((b - d) / 30.600);
                double D = b - d - (int)(30.6001 * ee) + 1 / (JD[j]+0.5);
                int M = (int)(ee - 12.0 * (int)(ee / 14.0)-1.0);//不多说了，留下一串省略号......
                int Y = (int)(c - 4715.0 - (int)((7.0 + M) / 10.0));
                textBox1.AppendText(Y.ToString("0000")+" "+M.ToString("00")+" "+D.ToString("00")+" " + hour[j].ToString("00") + ":" + minute[j].ToString("00") + ":" + second[j].ToString("00.000000")+"\r\n");
            }
            //年积日
            textBox1.AppendText("-------年积日-------\r\n");       
            int[] monthdays = {31,28,31,30,31,30,31,31,30,31,30,31};//平年
            int[] cumulativeday = new int[year.Length];
            int[] workorrest = new int[year.Length];
            int check = 0;
            for(int i = 0; i < year.Length;i++)
            {
                if (year[i] % 4 == 0 && year[i] % 100 != 0 || year[i] % 400 == 0)
                    monthdays[1] = 29;//闰年改二月为29天
                else
                    monthdays[1] = 28;
                for(int j = 0; j < month[i]-1;j++)
                {
                    cumulativeday[i] += 1 * monthdays[j];
                }
                cumulativeday[i] += day[i];
                textBox1.AppendText(cumulativeday[i].ToString() + "\r\n");
                if (year[i] == 2016)
                    check = cumulativeday[i] % 5;   
                else
                    check = (cumulativeday[i] + 366) % 5;
                if (check == 1 || check == 2 || check == 3)
                    workorrest[i] = 1;//打鱼
                else
                    workorrest[i] = 0;//晒网
            }
            textBox1.AppendText("-------三天打鱼两天晒网-------\r\n");
            for(int i = 0; i < year.Length;i++)
            {
                if(workorrest[i] == 1)
                    textBox1.AppendText(year[i]+" " + month[i].ToString("00")+" " + day[i].ToString("00")+"，"+"打鱼日\r\n");
                else
                    textBox1.AppendText(year[i] + " " + month[i].ToString("00") + " " + day[i].ToString("00") + "，" + "晒网日\r\n");
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
                string path = dialog.FileName;
                File.WriteAllText(path, textBox1.Text);
                textBox1.Clear();
                textBox1.AppendText("已将结果保存至" + path);
            }
        }

        private void ToolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("2022-11-29\r\nKUST\r\n测绘程序设计\r\n5_时间系统转换");
        }
    }
}