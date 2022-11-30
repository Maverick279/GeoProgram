using System.Net.Http.Headers;

namespace _6_轨迹数据压缩算法
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "轨迹数据压缩算法";
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.ReadOnly = true;
        }

        List<string> points = new List<string>();
        List<double> locx = new List<double>();
        List<double> locy = new List<double>();
        List<string> newpoints = new List<string>();

        public void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择轨迹数据文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                string path = dialog.FileName;
                string[] text = File.ReadAllLines(path);
                for(int i=0;i<text.Length;i++)
                {
                    textBox1.AppendText(text[i]+"\r\n");
                    points.Add(text[i].Split("，")[0]);
                    locx.Add(double.Parse(text[i].Split("，")[1]));
                    locy.Add(double.Parse(text[i].Split("，")[2]));
                }
            }
        }

        void DP(int start,int end,double valve, double[] distance)
        {
            double A = locy[end] - locy[start];
            double B = locx[start] - locx[end];
            double C = locx[end] * locy[start] - locx[start] * locy[end];//1、求出直线方程Ax+By+C=0
            int mid = 0;
            double dmax = 0.0;
            for (int i=start;i <= end ;i++)
            {
                distance[i] = Math.Abs(A * locx[i] + B * locy[i] + C) / Math.Sqrt(Math.Pow(A, 2) + Math.Pow(B, 2));//2、求出各点到直线距离
                if (distance[i]>dmax)
                {
                    mid = i;
                    dmax = distance[i];
                }
            }
            if (dmax >= valve)//3、按dmax与valve关系判断
            {
                DP(start, mid, valve, distance);
                DP(mid, end, valve, distance);
            }
            else
            {
                if (newpoints.IndexOf(points[start]) == -1)
                    newpoints.Add(points[start]);
                if (newpoints.IndexOf(points[end]) == -1)
                    newpoints.Add(points[end]);
            }        
        }

        List<string> sort(List<int> pointsid,List<string> newpoints)
        {
            for (int i = 0; i < newpoints.Count; i++)
            {
                pointsid.Add(int.Parse(newpoints[i].Substring(1)));
            }
            int temp=0;
            string tempstring;
            for(int i=0;i<pointsid.Count-1;i++)
            {
                for(int j=0;j<pointsid.Count-i-1;j++)
                {
                    if (pointsid[j] > pointsid[j+1])
                    {
                        temp=pointsid[j];
                        tempstring = newpoints[j];
                        pointsid[j] = pointsid[j+1];
                        newpoints[j] = newpoints[j+1];
                        pointsid[j+1]=temp;
                        newpoints[j+1]=tempstring;
                    }
                }
            }
            return newpoints;
        }

        void showresult(List<string> newpoints)
        {
            for (int i = 0; i < newpoints.Count; i++)
            {
                textBox1.AppendText(newpoints[i].ToString() + "，");
                textBox1.AppendText(locx[points.IndexOf(newpoints[i])].ToString("0.000") + "，");
                textBox1.AppendText(locy[points.IndexOf(newpoints[i])].ToString("0.000") + "\r\n");
            }
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            double valve = 5.0;//阈值
            double[] distance = new double[points.Count];   
            int start = 0;
            int end = points.Count - 1;
            List<int> pointsid = new List<int>();
            DP(start, end, valve, distance);
            textBox1.AppendText("-------压缩结果（阈值：5.000）-------\r\n");
            newpoints = sort(pointsid, newpoints);
            showresult(newpoints);
            valve = 8.0;
            newpoints.Clear();
            pointsid.Clear();
            DP(start, end, valve, distance);
            textBox1.AppendText("-------压缩结果（阈值：8.000）-------\r\n");
            newpoints = sort(pointsid, newpoints);
            showresult(newpoints);
        }

        private void ToolSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Title = "选择保存路径";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            if (dialog.FileName != "")
            {
                string path = dialog.FileName;
                File.WriteAllText(path, textBox1.Text);
                textBox1.Clear();
                textBox1.AppendText("已将结果保存至" + path + "\r\n");
            }
        }

        private void ToolHelp_Click(object sender, EventArgs e)
        {
            MessageBox.Show("KUST\r\n测绘程序设计\r\n4_最短路径计算");
        }
    }
}