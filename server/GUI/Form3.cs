using BLL;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GUI
{

    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        ToraManager _manager = new ToraManager();


        private void לסגולהשלךToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. יצירת המופע של הדף החדש
            Form2 nextForm = new Form2();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }

        private void חיפושיםמתקדמיםToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void חיפושטקסטבתורהToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. יצירת המופע של הדף החדש
            Form1 nextForm = new Form1();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }

        private void ההסטוריהשליToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 1. יצירת המופע של הדף החדש
            Form4 nextForm = new Form4();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
        }

        private async void btnSearchGimat_ClickAsync(object sender, EventArgs e)
        {

            rtbResultsGimat.Clear(); 

            string inputIn = txtGimat.Text.Trim();
            int targetNumber;
            var results = new List<Pasuk>();
            //המשתמש הכניס מספר
            if (int.TryParse(inputIn, out targetNumber))
            {
                //נשלח לפונקציה עם המספר
                results = await _manager.getAllGimetriaAsync(targetNumber);
            }
            //המשתמש הכניס מילה
            else
            {
                int number = _manager.getGimatria(inputIn);
                targetNumber = number;
                results = await _manager.getAllGimetriaAsync(number);
            }

            // בדיקה אם חזרו תוצאות
            if (results == null || results.Count == 0)
            {
                rtbResultsGimat.AppendText("לא נמצאו תוצאות.");
                return;
            }

            //רץ על כל הפסוקים
            foreach (var pasuk in results)
            {
                // שמירת המיקום הנוכחי לפני הוספת הפסוק
                int startPos = rtbResultsGimat.TextLength;

                // הוספת הפסוק למסך (עם מרכאות)
                // השתמשנו ב-Trim() כדי לוודא שאין רווחים מיותרים בסוף הפסוק שיפריעו לצביעה
                string plainText = pasuk.Text.Trim();
                string textWithQuotes = $"\u0022{plainText}\u0022";

                AppendTextWithStyle(textWithQuotes, Color.Black, true);


                // איפוס צבע הרקע להמשך הכתיבה
                rtbResultsGimat.SelectionStart = rtbResultsGimat.TextLength;
                rtbResultsGimat.SelectionBackColor = rtbResultsGimat.BackColor;

                // הדפסת כותרת הפסוק (חומש, פרק, פסוק)
                string header = $"{pasuk.ChumashName} | {pasuk.ParashaName} | פרק {pasuk.PerekName} | פסוק {pasuk.Name}";
                AppendTextWithStyle(header, Color.Blue, true);


            }
            // אחרי סיום ה-foreach של ההדפסה:
            HighlightGematria(targetNumber, Color.Yellow);
        }
        // פונקציה להוספת טקסט עם צבע וירידת שורה
        private void AppendTextWithStyle(string text, Color color, bool isBold)
        {
            rtbResultsGimat.SelectionStart = rtbResultsGimat.TextLength;
            rtbResultsGimat.SelectionLength = 0;
            rtbResultsGimat.SelectionColor = color;

            // אם רוצים שהפסוק יהיה מודגש (Bold)
            if (isBold)
                rtbResultsGimat.SelectionFont = new Font(rtbResultsGimat.Font, FontStyle.Bold);
            else
                rtbResultsGimat.SelectionFont = new Font(rtbResultsGimat.Font, FontStyle.Regular);

            rtbResultsGimat.AppendText(text + Environment.NewLine);
        }

        public void HighlightGematria(int targetNumber, Color highlightColor)
        {
            // 1. נפרק את כל הטקסט שיש בתיבה למילים
            string text = rtbResultsGimat.Text;
            string[] words = text.Split(new char[] { ' ', '\n', '\r', '.', ',', ':', '"' }, StringSplitOptions.RemoveEmptyEntries);

            int searchStart = 0;

            foreach (string word in words)
            {
                // 2. נחשב גימטריה למילה הנוכחית (דרך ה-Manager)
                if (_manager.getGimatria(word) == targetNumber)
                {
                    // 3. אם היא מתאימה - נמצא את המיקום שלה בתיבה ונצבע
                    int wordPos = rtbResultsGimat.Find(word, searchStart, RichTextBoxFinds.WholeWord);

                    if (wordPos != -1)
                    {
                        rtbResultsGimat.SelectionStart = wordPos;
                        rtbResultsGimat.SelectionLength = word.Length;
                        rtbResultsGimat.SelectionBackColor = highlightColor;

                        // נעדכן את נקודת ההתחלה לחיפוש הבא כדי לא למצוא את אותה מילה פעמיים
                        searchStart = wordPos + word.Length;
                    }
                }
            }
        }

    }
}
