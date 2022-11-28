namespace _3_利用线性回归模型进行卫星轨道的预报
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ToolOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolCalculate = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolSave = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolOpen,
            this.ToolCalculate,
            this.ToolSave,
            this.ToolHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ToolOpen
            // 
            this.ToolOpen.Name = "ToolOpen";
            this.ToolOpen.Size = new System.Drawing.Size(62, 21);
            this.ToolOpen.Text = "打开(&O)";
            this.ToolOpen.Click += new System.EventHandler(this.ToolOpen_Click);
            // 
            // ToolCalculate
            // 
            this.ToolCalculate.Name = "ToolCalculate";
            this.ToolCalculate.Size = new System.Drawing.Size(60, 21);
            this.ToolCalculate.Text = "计算(&C)";
            this.ToolCalculate.Click += new System.EventHandler(this.ToolCalculate_Click);
            // 
            // ToolSave
            // 
            this.ToolSave.Name = "ToolSave";
            this.ToolSave.Size = new System.Drawing.Size(59, 21);
            this.ToolSave.Text = "保存(&S)";
            this.ToolSave.Click += new System.EventHandler(this.ToolSave_Click);
            // 
            // ToolHelp
            // 
            this.ToolHelp.Name = "ToolHelp";
            this.ToolHelp.Size = new System.Drawing.Size(61, 21);
            this.ToolHelp.Text = "帮助(&H)";
            this.ToolHelp.Click += new System.EventHandler(this.ToolHelp_Click);
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(0, 25);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(800, 425);
            this.textBox1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem ToolOpen;
        private ToolStripMenuItem ToolCalculate;
        private ToolStripMenuItem ToolSave;
        private ToolStripMenuItem ToolHelp;
        private TextBox textBox1;
    }
}