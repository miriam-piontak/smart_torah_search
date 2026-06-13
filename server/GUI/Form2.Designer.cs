namespace GUI
{
    partial class Form2
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
            label1 = new Label();
            label2 = new Label();
            txtName = new TextBox();
            btnSearchName = new Button();
            rtbResults = new RichTextBox();
            menuStrip1 = new MenuStrip();
            לחיפושטקסטבתורהToolStripMenuItem = new ToolStripMenuItem();
            לחיפושיםמתקדמיםToolStripMenuItem = new ToolStripMenuItem();
            לחיפושטקסטבתורהToolStripMenuItem1 = new ToolStripMenuItem();
            ההסטריהשליToolStripMenuItem = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(367, 42);
            label1.Name = "label1";
            label1.Size = new Size(109, 15);
            label1.TabIndex = 0;
            label1.Text = "הפסוק שלך לסגולה";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(641, 87);
            label2.Name = "label2";
            label2.Size = new Size(82, 15);
            label2.TabIndex = 1;
            label2.Text = "הכנס את שמך";
            // 
            // txtName
            // 
            txtName.Location = new Point(515, 84);
            txtName.Name = "txtName";
            txtName.RightToLeft = RightToLeft.Yes;
            txtName.Size = new Size(100, 23);
            txtName.TabIndex = 2;
            txtName.TextChanged += txtName_TextChanged;
            // 
            // btnSearchName
            // 
            btnSearchName.Location = new Point(378, 84);
            btnSearchName.Name = "btnSearchName";
            btnSearchName.Size = new Size(131, 23);
            btnSearchName.TabIndex = 3;
            btnSearchName.Text = "חפש לי פסוק";
            btnSearchName.UseVisualStyleBackColor = true;
            btnSearchName.Click += btnSearchName_Click;
            // 
            // rtbResults
            // 
            rtbResults.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbResults.Location = new Point(273, 127);
            rtbResults.Name = "rtbResults";
            rtbResults.RightToLeft = RightToLeft.Yes;
            rtbResults.Size = new Size(454, 426);
            rtbResults.TabIndex = 4;
            rtbResults.Text = "";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { לחיפושטקסטבתורהToolStripMenuItem, לחיפושיםמתקדמיםToolStripMenuItem, לחיפושטקסטבתורהToolStripMenuItem1, ההסטריהשליToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // לחיפושטקסטבתורהToolStripMenuItem
            // 
            לחיפושטקסטבתורהToolStripMenuItem.Name = "לחיפושטקסטבתורהToolStripMenuItem";
            לחיפושטקסטבתורהToolStripMenuItem.Size = new Size(83, 20);
            לחיפושטקסטבתורהToolStripMenuItem.Text = "לסגולה שלך";
            לחיפושטקסטבתורהToolStripMenuItem.Click += לחיפושטקסטבתורהToolStripMenuItem_Click;
            // 
            // לחיפושיםמתקדמיםToolStripMenuItem
            // 
            לחיפושיםמתקדמיםToolStripMenuItem.Name = "לחיפושיםמתקדמיםToolStripMenuItem";
            לחיפושיםמתקדמיםToolStripMenuItem.Size = new Size(114, 20);
            לחיפושיםמתקדמיםToolStripMenuItem.Text = "חיפושים מתקדמים";
            לחיפושיםמתקדמיםToolStripMenuItem.Click += לחיפושיםמתקדמיםToolStripMenuItem_Click;
            // 
            // לחיפושטקסטבתורהToolStripMenuItem1
            // 
            לחיפושטקסטבתורהToolStripMenuItem1.Name = "לחיפושטקסטבתורהToolStripMenuItem1";
            לחיפושטקסטבתורהToolStripMenuItem1.Size = new Size(122, 20);
            לחיפושטקסטבתורהToolStripMenuItem1.Text = "חיפוש טקסט בתורה";
            לחיפושטקסטבתורהToolStripMenuItem1.Click += לחיפושטקסטבתורהToolStripMenuItem1_Click;
            // 
            // ההסטריהשליToolStripMenuItem
            // 
            ההסטריהשליToolStripMenuItem.Name = "ההסטריהשליToolStripMenuItem";
            ההסטריהשליToolStripMenuItem.Size = new Size(94, 20);
            ההסטריהשליToolStripMenuItem.Text = "ההסטוריה שלי";
            ההסטריהשליToolStripMenuItem.Click += ההסטריהשליToolStripMenuItem_Click;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(rtbResults);
            Controls.Add(btnSearchName);
            Controls.Add(txtName);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            Load += Form2_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtName;
        private Button btnSearchName;
        private RichTextBox rtbResults;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem לחיפושטקסטבתורהToolStripMenuItem;
        private ToolStripMenuItem לחיפושיםמתקדמיםToolStripMenuItem;
        private ToolStripMenuItem לחיפושטקסטבתורהToolStripMenuItem1;
        private ToolStripMenuItem ההסטריהשליToolStripMenuItem;
    }
}