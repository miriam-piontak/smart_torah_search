namespace GUI
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
            components = new System.ComponentModel.Container();
            btnSearchWord = new Button();
            label1 = new Label();
            labelSearchWord1 = new Label();
            txtSearchWord = new TextBox();
            label2 = new Label();
            rtbResults = new RichTextBox();
            btnSearchWordComp = new Button();
            linkLabel1 = new LinkLabel();
            menuStrip1 = new MenuStrip();
            contextMenuStrip1 = new ContextMenuStrip(components);
            menuStrip2 = new MenuStrip();
            tsmNevigate = new ToolStripMenuItem();
            tsmWidthSearch = new ToolStripMenuItem();
            חיפושטקסטבתורהToolStripMenuItem = new ToolStripMenuItem();
            ההסטוריהשליToolStripMenuItem = new ToolStripMenuItem();
            cbChumash = new ComboBox();
            cbParasha = new ComboBox();
            cbPerek = new ComboBox();
            colorDialog1 = new ColorDialog();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // btnSearchWord
            // 
            btnSearchWord.Location = new Point(544, 94);
            btnSearchWord.Name = "btnSearchWord";
            btnSearchWord.Size = new Size(75, 23);
            btnSearchWord.TabIndex = 3;
            btnSearchWord.Text = "חיפוש";
            btnSearchWord.UseVisualStyleBackColor = true;
            btnSearchWord.Click += btnSearchWord_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(350, 48);
            label1.Name = "label1";
            label1.Size = new Size(110, 15);
            label1.TabIndex = 5;
            label1.Text = "חיפוש טקסט בתורה";
            // 
            // labelSearchWord1
            // 
            labelSearchWord1.AutoSize = true;
            labelSearchWord1.Location = new Point(746, 98);
            labelSearchWord1.Name = "labelSearchWord1";
            labelSearchWord1.Size = new Size(65, 15);
            labelSearchWord1.TabIndex = 6;
            labelSearchWord1.Text = "הקלד מילה";
            // 
            // txtSearchWord
            // 
            txtSearchWord.Location = new Point(640, 95);
            txtSearchWord.Name = "txtSearchWord";
            txtSearchWord.RightToLeft = RightToLeft.Yes;
            txtSearchWord.Size = new Size(100, 23);
            txtSearchWord.TabIndex = 7;
            txtSearchWord.TextChanged += txtSearchWord_TextChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(782, 171);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 9;
            label2.Text = "תוצאות";
            // 
            // rtbResults
            // 
            rtbResults.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtbResults.Location = new Point(394, 204);
            rtbResults.Name = "rtbResults";
            rtbResults.RightToLeft = RightToLeft.Yes;
            rtbResults.Size = new Size(434, 356);
            rtbResults.TabIndex = 11;
            rtbResults.Text = "";
            // 
            // btnSearchWordComp
            // 
            btnSearchWordComp.Enabled = false;
            btnSearchWordComp.Location = new Point(432, 95);
            btnSearchWordComp.Name = "btnSearchWordComp";
            btnSearchWordComp.Size = new Size(106, 23);
            btnSearchWordComp.TabIndex = 12;
            btnSearchWordComp.Text = "חיפוש מדויק";
            btnSearchWordComp.UseVisualStyleBackColor = true;
            btnSearchWordComp.Click += btnSearchWordComp_Click_1;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(211, 18);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(0, 15);
            linkLabel1.TabIndex = 20;
            // 
            // menuStrip1
            // 
            menuStrip1.Location = new Point(0, 24);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(984, 24);
            menuStrip1.TabIndex = 21;
            menuStrip1.Text = "menuStrip1";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // menuStrip2
            // 
            menuStrip2.Items.AddRange(new ToolStripItem[] { tsmNevigate, tsmWidthSearch, חיפושטקסטבתורהToolStripMenuItem, ההסטוריהשליToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(984, 24);
            menuStrip2.TabIndex = 23;
            menuStrip2.Text = "hvhv";
            // 
            // tsmNevigate
            // 
            tsmNevigate.Name = "tsmNevigate";
            tsmNevigate.Size = new Size(83, 20);
            tsmNevigate.Text = "לסגולה שלך";
            tsmNevigate.Click += tsmNevigate_Click;
            // 
            // tsmWidthSearch
            // 
            tsmWidthSearch.Name = "tsmWidthSearch";
            tsmWidthSearch.Size = new Size(114, 20);
            tsmWidthSearch.Text = "חיפושים מתקדמים";
            tsmWidthSearch.Click += tsmWidthSearch_Click;
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
            // cbChumash
            // 
            cbChumash.FormattingEnabled = true;
            cbChumash.Location = new Point(712, 135);
            cbChumash.Name = "cbChumash";
            cbChumash.Size = new Size(121, 23);
            cbChumash.TabIndex = 24;
            cbChumash.SelectedIndexChanged += cbChumash_SelectedIndexChanged;
            // 
            // cbParasha
            // 
            cbParasha.FormattingEnabled = true;
            cbParasha.Location = new Point(573, 135);
            cbParasha.Name = "cbParasha";
            cbParasha.Size = new Size(121, 23);
            cbParasha.TabIndex = 25;
            // 
            // cbPerek
            // 
            cbPerek.FormattingEnabled = true;
            cbPerek.Location = new Point(432, 135);
            cbPerek.Name = "cbPerek";
            cbPerek.Size = new Size(121, 23);
            cbPerek.TabIndex = 26;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(984, 661);
            Controls.Add(cbPerek);
            Controls.Add(cbParasha);
            Controls.Add(cbChumash);
            Controls.Add(linkLabel1);
            Controls.Add(btnSearchWordComp);
            Controls.Add(rtbResults);
            Controls.Add(label2);
            Controls.Add(txtSearchWord);
            Controls.Add(labelSearchWord1);
            Controls.Add(label1);
            Controls.Add(btnSearchWord);
            Controls.Add(menuStrip1);
            Controls.Add(menuStrip2);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnSearchWord;
        private Label label1;
        private Label labelSearchWord1;
        private TextBox txtSearchWord;
        private Label label2;
        private RichTextBox rtbResults;
        private Button btnSearchWordComp;
        private LinkLabel linkLabel1;
        private MenuStrip menuStrip1;
        private ContextMenuStrip contextMenuStrip1;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem tsmNevigate;
        private ToolStripMenuItem tsmWidthSearch;
        private ToolStripMenuItem חיפושטקסטבתורהToolStripMenuItem;
        private ToolStripMenuItem ההסטוריהשליToolStripMenuItem;
        private ComboBox cbChumash;
        private ComboBox cbParasha;
        private ComboBox cbPerek;
        private ColorDialog colorDialog1;
    }
}
