namespace GUI
{
    partial class Form3
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
            לסגולהשלךToolStripMenuItem = new ToolStripMenuItem();
            חיפושיםמתקדמיםToolStripMenuItem = new ToolStripMenuItem();
            חיפושטקסטבתורהToolStripMenuItem = new ToolStripMenuItem();
            ההסטוריהשליToolStripMenuItem = new ToolStripMenuItem();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtGimat = new TextBox();
            rtbResultsGimat = new RichTextBox();
            btnSearchGimat = new Button();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { לסגולהשלךToolStripMenuItem, חיפושיםמתקדמיםToolStripMenuItem, חיפושטקסטבתורהToolStripMenuItem, ההסטוריהשליToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // לסגולהשלךToolStripMenuItem
            // 
            לסגולהשלךToolStripMenuItem.Name = "לסגולהשלךToolStripMenuItem";
            לסגולהשלךToolStripMenuItem.Size = new Size(83, 20);
            לסגולהשלךToolStripMenuItem.Text = "לסגולה שלך";
            לסגולהשלךToolStripMenuItem.Click += לסגולהשלךToolStripMenuItem_Click;
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
            ההסטוריהשליToolStripMenuItem.Click += ההסטוריהשליToolStripMenuItem_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(344, 48);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 1;
            label1.Text = "חיפושים מתקדמים";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(708, 91);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 2;
            label2.Text = "חיפוש גימטריה";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(679, 132);
            label3.Name = "label3";
            label3.Size = new Size(112, 15);
            label3.TabIndex = 3;
            label3.Text = "הכנס מילה או מספר";
            // 
            // txtGimat
            // 
            txtGimat.Location = new Point(573, 129);
            txtGimat.Name = "txtGimat";
            txtGimat.Size = new Size(100, 23);
            txtGimat.TabIndex = 4;
            // 
            // rtbResultsGimat
            // 
            rtbResultsGimat.Location = new Point(400, 167);
            rtbResultsGimat.Name = "rtbResultsGimat";
            rtbResultsGimat.Size = new Size(415, 436);
            rtbResultsGimat.TabIndex = 5;
            rtbResultsGimat.Text = "";
            // 
            // btnSearchGimat
            // 
            btnSearchGimat.Location = new Point(483, 128);
            btnSearchGimat.Name = "btnSearchGimat";
            btnSearchGimat.Size = new Size(75, 23);
            btnSearchGimat.TabIndex = 6;
            btnSearchGimat.Text = "חיפוש";
            btnSearchGimat.UseVisualStyleBackColor = true;
            btnSearchGimat.Click += btnSearchGimat_ClickAsync;
            // 
            // Form3
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(btnSearchGimat);
            Controls.Add(rtbResultsGimat);
            Controls.Add(txtGimat);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form3";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form3";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem לסגולהשלךToolStripMenuItem;
        private ToolStripMenuItem חיפושיםמתקדמיםToolStripMenuItem;
        private ToolStripMenuItem חיפושטקסטבתורהToolStripMenuItem;
        private ToolStripMenuItem ההסטוריהשליToolStripMenuItem;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtGimat;
        private RichTextBox rtbResultsGimat;
        private Button btnSearchGimat;
    }
}