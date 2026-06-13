using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class Form4 : Form
    {
        ToraManager _manager = new ToraManager(); // יצירת המנג'ר
        public Form4()
        {
            InitializeComponent();
        }

        private void הסגולהלToolStripMenuItem_Click(object sender, EventArgs e)
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
            // 1. יצירת המופע של הדף החדש
            Form3 nextForm = new Form3();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();
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

        private async Task DisplayHistoryAsync()
        {
            lstHistory.Items.Clear();

            // הגדרות עיצוב לתצוגה של שתי שורות
            lstHistory.View = View.Tile;
            lstHistory.TileSize = new Size(lstHistory.Width - 30, 45); // גובה של 45 ייתן מקום ל-2 שורות

            List<string> history = await _manager.GetSearchHistoryAsync();

            if (history != null)
            {
                foreach (string fullLine in history)
                {
                    string[] parts = fullLine.Split(';');
                    if (parts.Length < 5) continue;

                    // שורה ראשונה: התאריך והתיאור הראשי (למשל: 10/03 - חיפוש טקסט: "משה")
                    ListViewItem item = new ListViewItem(parts[0]);

                    // שורה שנייה: פירוט המיקום (חומש, פרשה וכו')
                    // נבנה מחרוזת יפה רק מה שאינו "הכל"
                    List<string> loc = new List<string>();
                    if (parts[2] != "הכל") loc.Add(parts[2]);
                    if (parts[3] != "הכל") loc.Add(parts[3]);
                    if (parts[4] != "הכל") loc.Add(parts[4]);

                    string subText = loc.Count > 0 ? string.Join(" > ", loc) : "כל התורה";
                    item.SubItems.Add(subText); // זה יוצג מתחת לשורה הראשונה במצב Tile

                    item.Tag = fullLine; // שומרים את המידע המלא ללחיצה
                    lstHistory.Items.Add(item);
                }
            }
        }
        private async void Form4_Load(object sender, EventArgs e)
        {
            await DisplayHistoryAsync();
        }
        private void lstHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lstHistory.SelectedItems.Count > 0)
            {
                // שליפת השורה המלאה ששמרנו ב-Tag
                string fullLine = lstHistory.SelectedItems[0].Tag.ToString();

                // פיצול כל הפרמטרים: [0]תיאור, [1]מילה, [2]חומש, [3]פרשה, [4]פרק
                string[] parts = fullLine.Split(';');

                if (parts.Length < 5) return; // הגנה למקרה של שורה לא תקינה

                string description = parts[0];
                string word = parts[1];
                string chumash = parts[2];
                string parasha = parts[3];
                string perek = parts[4];

                // 1. בדיקה אם זה חיפוש סגולה
                if (description.Contains("חיפוש סגולה"))
                {
                    Form2 f2 = new Form2();
                    f2.Show();
                    f2.ExternalSearch(word); // בסגולה מספיקה המילה
                    this.Hide(); // עדיף Hide מאשר Close אם את רוצה לחזור להיסטוריה אחר כך
                }
                // 2. חיפוש טקסט בתורה
                else
                {
                    Form1 f1 = new Form1();
                    f1.Show();

                    bool isExact = description.Contains("מדויק");

                    if (isExact)
                        f1.ExternalSearch2(word, chumash, parasha, perek); // שולח את כל המיקום!
                    else
                        f1.ExternalSearch(word, chumash, parasha, perek);

                    this.Hide();
                }
            }
        }

        // פונקציית עזר לחילוץ המילה מתוך המחרוזת
        private string ExtractWord(string fullLine, string prefix)
        {
            try
            {
                // מוצא איפה הקידומת נגמרת ולוקח את כל מה שאחריה
                int startIndex = fullLine.IndexOf(prefix) + prefix.Length;
                if (startIndex < prefix.Length) return ""; // הקידומת לא נמצאה

                return fullLine.Substring(startIndex).Trim();
            }
            catch
            {
                return "";
            }
        }
    }
        }


    