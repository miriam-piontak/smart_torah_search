namespace GUI
{
    partial class Form4
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            הסגולהלToolStripMenuItem = new ToolStripMenuItem();
            חיפושיםמתקדמיםToolStripMenuItem = new ToolStripMenuItem();
            חיפושטקסטבתורהToolStripMenuItem = new ToolStripMenuItem();
            ההסטוריהשליToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            lstHistory = new ListView();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { הסגולהלToolStripMenuItem, חיפושיםמתקדמיםToolStripMenuItem, חיפושטקסטבתורהToolStripMenuItem, ההסטוריהשליToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // הסגולהלToolStripMenuItem
            // 
            הסגולהלToolStripMenuItem.Name = "הסגולהלToolStripMenuItem";
            הסגולהלToolStripMenuItem.Size = new Size(83, 20);
            הסגולהלToolStripMenuItem.Text = "לסגולה שלך";
            הסגולהלToolStripMenuItem.Click += הסגולהלToolStripMenuItem_Click;
            // 
            // חיפושיםמתקדמיםToolStripMenuItem
            // 
            חיפושיםמתקדמיםToolStripMenuItem.Name = "חיפושיםמתקדמיםToolStripMenuItem";
            חיפושיםמתקדמיםToolStripMenuItem.Size = new Size(114, 20);
            חיפושיםמתקדמיםToolStripMenuItem.Text = "חיפושים מתקדמים";
            חיפושיםמתקדמיםToolStripMenuItem.Click += חיפושיםמתקדמיםToolStripMenuItem_Click;
            // 
            // חיפושטקסטבתורהToolStripMenuItem
            // 
            חיפושטקסטבתורהToolStripMenuItem.Name = "חיפושטקסטבתורהToolStripMenuItem";
            חיפושטקסטבתורהToolStripMenuItem.Size = new Size(122, 20);
            חיפושטקסטבתורהToolStripMenuItem.Text = "חיפוש טקסט בתורה";
            חיפושטקסטבתורהToolStripMenuItem.Click += חיפושטקסטבתורהToolStripMenuItem_Click;
            // 
            // ההסטוריהשליToolStripMenuItem
            // 
            ההסטוריהשליToolStripMenuItem.Name = "ההסטוריהשליToolStripMenuItem";
            ההסטוריהשליToolStripMenuItem.Size = new Size(94, 20);
            ההסטוריהשליToolStripMenuItem.Text = "ההסטוריה שלי";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(633, 67);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 1;
            label1.Text = "הסטוריה";
            // 
            // lstHistory
            // 
            lstHistory.Location = new Point(201, 85);
            lstHistory.Name = "lstHistory";
            lstHistory.RightToLeft = RightToLeft.Yes;
            lstHistory.RightToLeftLayout = true;
            lstHistory.Size = new Size(530, 402);
            lstHistory.TabIndex = 2;
            lstHistory.UseCompatibleStateImageBehavior = false;
            lstHistory.MouseDoubleClick += lstHistory_MouseDoubleClick;
            // 
            // Form4
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(lstHistory);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form4";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form4";
            Load += Form4_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem הסגולהלToolStripMenuItem;
        private ToolStripMenuItem חיפושיםמתקדמיםToolStripMenuItem;
        private ToolStripMenuItem חיפושטקסטבתורהToolStripMenuItem;
        private ToolStripMenuItem ההסטוריהשליToolStripMenuItem;
        private Label label1;
        private ListView lstHistory;
    }
}