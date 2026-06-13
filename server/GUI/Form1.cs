using BLL;
using DAL.Entities;
using DAL.Services;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class Form1 : Form
    {
        //משתנים שיכילו את הפרמטרים בשביל ההסטוריה
        public string chumash2 { get; set; }
        public string parasha2 { get; set; }
        public string perek2 { get; set; }

        ToraManager _manager = new ToraManager();

        public Form1()
        {
            InitializeComponent();
        }
        // --- פונקציה חדשה: מאפשרת להפעיל חיפוש מתוך דף ההיסטוריה ---
        public async void ExternalSearch(string word, string chumash, string parasha, string perek)
        {
            // 1. אתחול הנתונים אם צריך
            await _manager.InitializeAsync();

            // 2. עדכון הממשק (UI) - החזרת הערכים לתיבות הסינון ולתיבת הטקסט
            txtSearchWord.Text = word;
            cbChumash.Text = chumash;
            cbParasha.Text = parasha;
            cbPerek.Text = perek;

            // 3. ביצוע החיפוש בפועל
            // שימי לב: שלחנו 'false' בפרמטר isToSave כדי שהחיפוש המשוחזר לא יישמר שוב כחיפוש חדש בהיסטוריה
            PerformSearch(word, false, false, chumash, parasha, perek);
        }
        public async void ExternalSearch2(string word, string chumash, string parasha, string perek)
        {
            await _manager.InitializeAsync();
            // 2. עדכון הממשק (UI) - החזרת הערכים לתיבות הסינון ולתיבת הטקסט
            txtSearchWord.Text = word;
            cbChumash.Text = chumash;
            cbParasha.Text = parasha;
            cbPerek.Text = perek;            // מפעיל את פונקציית החיפוש הרגילה (חיפוש מדויק במקרה הזה)

            PerformSearch(word, true, false, chumash, parasha, perek);
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

        // פונקציה שסורקת את כל הטקסט וצובעת רק את המילה שחיפשנו בצהוב
        private void HighlightWord(string word, Color highlightColor)
        {
            if (string.IsNullOrWhiteSpace(word)) return;

            int startIndex = 0;
            while ((startIndex = rtbResults.Find(word, startIndex, RichTextBoxFinds.None)) != -1)
            {
                rtbResults.SelectionStart = startIndex;
                rtbResults.SelectionLength = word.Length;
                rtbResults.SelectionBackColor = highlightColor; // המרקר הצהוב

                startIndex += word.Length;
            }
        }

        private async void btnSearchWord1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            try
            {
                // טעינה חד פעמית של כל התורה לזיכרון
                await _manager.InitializeAsync();

                //טעינת שמות החומשים
                List<string> ChumashesList = await _manager.GetComashNamesAsync();
                // הוספת האופציה "הכל" במיקום 0 (תחילת הרשימה)
                ChumashesList.Insert(0, "הכל");
                //אם יש ברשימה לפחות איבר אחד כלומר לא ריקה
                if (ChumashesList.Any())
                {
                    cbChumash.DataSource = ChumashesList;

                    // כדי לוודא שזה גם הפריט שנבחר אוטומטית כברירת מחדל:
                    cbChumash.SelectedIndex = 0;
                }

                //טעינת הפרשות
                List<string> ParashotList = await _manager.GetAllParashasNamesAsync();
                // הוספת האופציה "הכל" במיקום 0 (תחילת הרשימה)
                ParashotList.Insert(0, "הכל");
                //אם יש ברשימה לפחות איבר אחד כלומר לא ריקה
                if (ParashotList.Any())
                {
                    cbParasha.DataSource = ParashotList;

                    // כדי לוודא שזה גם הפריט שנבחר אוטומטית כברירת מחדל:
                    cbParasha.SelectedIndex = 0;
                }

                //טעינת הפרקים
                List<string> PrakimList = await _manager.GetAllPerekNamesAsync();
                // הוספת האופציה "הכל" במיקום 0 (תחילת הרשימה)
                PrakimList.Insert(0, "הכל");
                //אם יש ברשימה לפחות איבר אחד כלומר לא ריקה
                if (PrakimList.Any())
                {
                    cbPerek.DataSource = PrakimList;
                    cbPerek.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("שגיאה בטעינת הנתונים: " + ex.Message);
            }

        }



        private void tsmNevigate_Click(object sender, EventArgs e)
        {

            // 1. יצירת המופע של הדף החדש
            Form2 nextForm = new Form2();

            // 2. הצגת הדף החדש
            nextForm.Show();

            // 3. הסתרת הדף הנוכחי (זה שבו הכפתור נמצא)
            this.Hide();

        }

        private void tsmWidthSearch_Click(object sender, EventArgs e)
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

        private async void btnSearchWord_Click(object sender, EventArgs e)
        {

            // 1. קבלת המילה מהתיבה
            string wordToSearch = txtSearchWord.Text;
            //פרמטרי הסינון
            string chumash = cbChumash.SelectedItem?.ToString() ?? "הכל";
            string parasha = cbParasha.SelectedItem?.ToString() ?? "הכל";
            string perek = cbPerek.SelectedItem?.ToString() ?? "הכל";

            if (chumash == "הכל" && parasha == "הכל" && perek == "הכל" && string.IsNullOrWhiteSpace(wordToSearch))
            {
                MessageBox.Show("אנא הזן מילה לחיפוש או בחר מיקום ספציפי.", "חיפוש כללי מדי", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // עוצר את הפונקציה כאן ולא ממשיך לחיפוש            

            }
            //פרק--פרשה--חומש--לשמור להסטוריה--האם חיפוש מדויק--מילת החיפוש--
            PerformSearch(wordToSearch, false, true, chumash, parasha, perek);
        }

        private async void btnSearchWordComp_Click_1(object sender, EventArgs e)
        {
            // 1. קבלת המילה מהתיבה ובדיקה שהיא לא ריקה
            string wordToSearch = txtSearchWord.Text;

            //פרמטרי הסינון
            string chumash = cbChumash.SelectedItem?.ToString() ?? "הכל";
            string parasha = cbParasha.SelectedItem?.ToString() ?? "הכל";
            string perek = cbPerek.SelectedItem?.ToString() ?? "הכל";

            if (chumash == "הכל" && parasha == "הכל" && perek == "הכל" && string.IsNullOrWhiteSpace(wordToSearch))
            {
                MessageBox.Show("אנא הזן מילה לחיפוש או בחר מיקום ספציפי.", "חיפוש כללי מדי", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return; // עוצר את הפונקציה כאן ולא ממשיך לחיפוש            

            }
            PerformSearch(wordToSearch, true, true, chumash, parasha, perek);

        }
        private async void PerformSearch(string word, bool isExact, bool isToSave, string chumash, string parasha, string perek)
        {
            // 1. בדיקה ראשונית (הגנה)
            if (string.IsNullOrWhiteSpace(word) && chumash == "הכל" && parasha == "הכל" && perek == "הכל")
            {
                MessageBox.Show("אנא הזן מילה לחיפוש או בחר מיקום ספציפי.", "חיפוש כללי מדי", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // עדכון משתני המחלקה (בשביל ה-Getters)
            chumash2 = chumash;
            parasha2 = parasha;
            perek2 = perek;

            // 2. טעינת נתונים במידת הצורך
            await _manager.InitializeAsync();
            List<DAL.Entities.Pasuk> results;

            // 3. שמירה להיסטוריה (רק אם התבקשנו - למשל בלחיצת כפתור ולא בשחזור)
            // 3. עדכון היסטוריה
            if (isToSave)
            {
                string type = isExact ? "חיפוש טקסט מדויק" : "חיפוש טקסט";

                // קבלת התיאור הנקי מה-BLL
                string description = await _manager.GetHistoryDescriptionAsync(type, word, chumash, parasha, perek);

                // שליחה ל-DAL (5 פרמטרים כפי שמוגדר אצלך)
                await ToraRepository.SaveSearchHistoryAsync(description, word, chumash, parasha, perek);
            }

            // 4. הבאת הפסוקים לפי המיקום שנבחר
            results = await _manager.GetPasuksByLocation(chumash, parasha, perek);

            // 5. סינון לפי המילה (אם קיימת)
            if (!string.IsNullOrWhiteSpace(word))
            {
                if (isExact)
                {
                    string pattern = $@"\b{Regex.Escape(word)}\b";
                    results = results.Where(p => Regex.IsMatch(p.Text, pattern)).ToList();
                }
                else
                {
                    results = results.Where(p => p.Text.Contains(word)).ToList();
                }
            }

            // 6. תצוגת התוצאות (החלק שהיה חסר!)
            rtbResults.Clear();

            if (results == null || results.Count == 0)
            {
                string noResultsMsg = isExact ? $"הטקסט '{word}' לא נמצא במדויק במיקום שנבחר." : "לא נמצאו תוצאות לחיפוש זה.";
                AppendTextWithStyle(noResultsMsg, Color.Red, true);
            }
            else
            {
                foreach (var pasuk in results)
                {
                    // הדפסת גוף הפסוק
                    AppendTextWithStyle(pasuk.Text, Color.Black, false);

                    // הדפסת כותרת הפסוק (חומש, פרק, פסוק)
                    string header = $"{pasuk.ChumashName} | {pasuk.ParashaName} | פרק {pasuk.PerekName} | פסוק {pasuk.Name}";
                    AppendTextWithStyle(header, Color.Blue, true);


                    // הוספת מרווח בין פסוקים
                    rtbResults.AppendText(Environment.NewLine);
                }

                // 7. הדגשת המילה בצהוב (מרקר) בתוך כל התוצאות
                if (!string.IsNullOrWhiteSpace(word))
                {
                    HighlightWord(word, Color.Yellow);
                }
            }
        }

        private async void cbChumash_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedChumash = cbChumash.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedChumash)) return;

            List<string> listParasha;

            if (selectedChumash == "הכל")
            {
                listParasha = await _manager.GetAllParashasNamesAsync();
            }
            else
            {
                listParasha = await _manager.GetParashasNamesByChumashAsync(selectedChumash);
            }
            if (selectedChumash != "בראשית")
                listParasha.RemoveAt(0);
            listParasha.Insert(0, "הכל");


            // ניתוק זמני של האירוע כדי למנוע קפיצות מיותרות בזמן עדכון הנתונים
            cbParasha.SelectedIndexChanged -= cbParasha_SelectedIndexChanged;
            cbParasha.DataSource = listParasha;
            cbParasha.SelectedIndex = 0;
            cbParasha.SelectedIndexChanged += cbParasha_SelectedIndexChanged;

            // עדכון הפרקים בהתאם למצב החדש
            UpdatePerekList();
        }

        private async void UpdatePerekList()
        {
            string chumash = cbChumash.SelectedItem?.ToString() ?? "הכל";
            string parasha = cbParasha.SelectedItem?.ToString() ?? "הכל";

            List<string> listPerek;

            if (parasha != "הכל")
            {
                // אם יש פרשה - נביא פרקים לפי פרשה
                string currentChumash = await _manager.GetChumashByParashaAsync(parasha);
                listPerek = await _manager.GetPereksNamesByParashaAsync(currentChumash, parasha);
            }
            else if (chumash != "הכל")
            {
                // אם אין פרשה אבל יש חומש - נביא את כל הפרקים של החומש
                listPerek = await _manager.GetPereksNamesByChumashAsync(chumash);
            }
            else
            {
                // הכל ריק - כל הפרקים
                listPerek = await _manager.GetAllPerekNamesAsync();
            }

            listPerek.Insert(0, "הכל");
            cbPerek.DataSource = listPerek;
            cbPerek.SelectedIndex = 0;
        }


        private async void cbParasha_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePerekList();

        }

        private void txtSearchWord_TextChanged(object sender, EventArgs e)
        {

            btnSearchWordComp.Enabled = !string.IsNullOrWhiteSpace(txtSearchWord.Text);
        }


    }
}



