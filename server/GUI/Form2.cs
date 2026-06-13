using BLL;
using DAL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GUI
{
    public partial class Form2 : Form
    {
        ToraManager _manager = new ToraManager();

        public Form2()
        {
            InitializeComponent();
        }

        private async void btnSearchName_Click(object sender, EventArgs e)
        {
            //ישמור את השם שנכתב בתיבת טקסט
            string name = txtName.Text;
            PerformSegulaSearch(name, true);

        }



        // פונקציה להוספת טקסט עם צבע וירידת שורה
        private void AppendTextWithStyle(string text, Color color, bool isBold)
        {
            rtbResults.SelectionStart = rtbResults.TextLength;
            rtbResults.SelectionLength = 0;
            rtbResults.SelectionColor = color;

            // אם רוצים שהפסוק יהיה מודגש (Bold)
            if (isBold)
                rtbResults.SelectionFont = new Font(rtbResults.Font, FontStyle.Bold);
            else
                rtbResults.SelectionFont = new Font(rtbResults.Font, FontStyle.Regular);

            rtbResults.AppendText(text + Environment.NewLine);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void לחיפושטקסטבתורהToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void לחיפושיםמתקדמיםToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. יצירת המופע של הדף החדש
            Form3 nextForm = new Form3();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }

        private void לחיפושטקסטבתורהToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // 1. יצירת המופע של הדף החדש
            Form1 nextForm = new Form1();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }

        private void ההסטריהשליToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. יצירת המופע של הדף החדש
            Form4 nextForm = new Form4();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }
        // פונקציה שמאפשרת לפורם אחר (כמו פורם ההיסטוריה) להריץ חיפוש
        public async void ExternalSearch(string word)
        {
            // 1. נשים את המילה בתיבת הטקסט של השם
            txtName.Text = word;

            PerformSegulaSearch(word, false);
        }
        private async void PerformSegulaSearch(string name, bool isToSave)
        {


            if (isToSave)
            {
                string description = $"חיפוש סגולה: {name}";
                await ToraRepository.SaveSearchHistoryAsync(description, name, "הכל", "הכל", "הכל");
            }
            rtbResults.Clear();

            // 1. קבלת התוצאות מה-BLL
            var results = await _manager.GetPasuksByNameAsync(name);

            // 2. ניקוי התיבה לפני הצגת תוצאות חדשות
            rtbResults.Clear();

            if (results == null || results.Count == 0)
            {
                // הודעה למשתמש במידה ולא נמצא כלום
                AppendTextWithStyle($"שלום {name}, אין לך פסוק מתאים בתורה.", Color.Red, true);
                return; // עוצר כאן ולא ממשיך ללולאה
            }
            //רץ על כל הפסוקים
            foreach (var pasuk in results)
            {
                // שמירת המיקום הנוכחי לפני הוספת הפסוק
                int startPos = rtbResults.TextLength;

                // הוספת הפסוק למסך (עם מרכאות)
                // השתמשנו ב-Trim() כדי לוודא שאין רווחים מיותרים בסוף הפסוק שיפריעו לצביעה
                string plainText = pasuk.Text.Trim();
                string textWithQuotes = $"\u0022{plainText}\u0022";

                AppendTextWithStyle(textWithQuotes, Color.Black, true);

                // --- צביעת האות הראשונה של הפסוק ---
                // המיקום הוא startPos + 1 (כי התו הראשון הוא גרשיים)
                rtbResults.SelectionStart = startPos + 1;
                rtbResults.SelectionLength = 1;
                rtbResults.SelectionBackColor = Color.Yellow;

                // --- צביעת האות האחרונה של הפסוק ---
                // המיקום הוא startPos + אורך הטקסט פחות 2 (תו אחד לפני הגרשיים הסוגרים)
                rtbResults.SelectionStart = startPos + textWithQuotes.Length - 2;
                rtbResults.SelectionLength = 1;
                rtbResults.SelectionBackColor = Color.Yellow;

                // איפוס צבע הרקע להמשך הכתיבה
                rtbResults.SelectionStart = rtbResults.TextLength;
                rtbResults.SelectionBackColor = rtbResults.BackColor;

                // הדפסת כותרת הפסוק (חומש, פרק, פסוק)
                string header = $"{pasuk.ChumashName} | {pasuk.ParashaName} | פרק {pasuk.PerekName} | פסוק {pasuk.Name}";
                AppendTextWithStyle(header, Color.Blue, true);



                // הוספת מרווח בין פסוקים
                rtbResults.AppendText(Environment.NewLine);


            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
