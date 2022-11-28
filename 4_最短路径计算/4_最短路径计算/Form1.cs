namespace _4_最短路径计算
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "最短路径计算(Dijkstra)";
            this.StartPosition = FormStartPosition.CenterScreen;
            textBox1.ReadOnly = true;
        }

        string[] text, start, end;
        List<string> name = new List<string>();
        int[] cost;

        public void ToolOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择路径数据文件";
            dialog.Filter = "文本文件|*.txt";
            dialog.ShowDialog();
            if(dialog.FileName!="")
            {
                string file = dialog.FileName;
                text = File.ReadAllLines(file);
                start = new string[text.Length];
                end = new string[text.Length];
                cost = new int[text.Length];
                for(int i=0; i < text.Length; i++)
                {
                    textBox1.AppendText(text[i] + "\r\n");
                    start[i] = text[i].Split("，")[0];//起点
                    end[i] = text[i].Split("，")[1];//终点
                    cost[i] = int.Parse(text[i].Split("，")[2]);//成本
                }
            }
        }

        public void ToolCalculate_Click(object sender, EventArgs e)
        {
            int flag = 0;
            int infinity = 9999;//无对应路径即给出“无穷大”
            for(int i=0;i<start.Length;i++)
            {
                if (!(name.Contains(start[i])))
                {
                    name.Add(start[i]);
                    flag++;
                }
            }//name:{武大,地大,光谷,图书城,华科}
            int[,] edge = new int[name.Count,name.Count];//邻接矩阵edge[5,5]
            for (int i=0;i<name.Count;i++)//5
            {
                for(int j=0;j<name.Count;j++)//5
                {
                    for(int k=0;k<start.Length;k++)//10
                    {
                        if (i == j)
                        {
                            edge[i, j] = 0;
                            break;
                        }
                        else if (name[i] == start[k] && name[j] == end[k])
                        {
                            edge[i, j] = cost[k];
                            break;
                        }
                        edge[i,j] = infinity;
                    }
                }
            }//创建邻接矩阵

            int v = 0;//出发点：武大
            int[] traverse = new int[name.Count];//存放已遍历点
            int[] dist = new int[name.Count];
            string[] path = new string[name.Count];
            for(int i=0;i<name.Count;i++)
                dist[i] = edge[v, i];
            traverse[v] = 1;//先将出发点放入traverse数组
            for (int i=1;i<name.Count;i++)
            {
                int min = infinity;
                for (int j=0;j<name.Count;j++)//查找当前最短路径点
                {
                    if ((traverse[j] != 1) && dist[j]<min)
                    {
                        min = dist[j];
                        v = j;
                    }
                }
                traverse[v] = 1;
                for(int k=0;k<name.Count;k++)//更新dist[]
                {
                    if ((traverse[k] != 1) && dist[k] > dist[v] + edge[v,k])
                    {
                        dist[k] = dist[v] + edge[v,k];
                    }
                }
            }
            textBox1.Clear();
            textBox1.AppendText("-----最短路径计算结果-----\r\n");
            for(int i=0;i<dist.Length;i++)
            {
                textBox1.AppendText(name[i].ToString()+"，"+dist[i].ToString()+"\r\n");
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
            MessageBox.Show("2022-11-28晚\r\nKUST\r\n测绘程序设计\r\n4_最短路径计算");
        }
    }
}