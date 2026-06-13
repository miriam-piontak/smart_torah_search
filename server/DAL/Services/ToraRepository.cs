using DAL.Entities;
using Newtonsoft.Json;//שימוש בקובץ ג'ייסון
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;// נדרש עבור Encoding.UTF8
using System.Threading.Tasks; // נדרש עבור Task

namespace DAL.Services
{
    public static class ToraRepository
    {
        
            public static Chomash[] Chomashes { get; set; }

            //הגדרת נתיב לקובץ הג'ייסון
            private static string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tora.json");
            //ג'ייסון שישמור לי הסטורית החיפושים
            private static string historyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "history.json");

            //פונקציה שמנהלת את הטעינה בצורה אסינכרונית
            public static async Task InitAsync()
            {
                //אם קים קובץ הג'ייסון
                if (File.Exists(jsonPath))
                {

                    try
                    {   //קורא מהקובץ
                        string jsonContent = await File.ReadAllTextAsync(jsonPath, Encoding.UTF8);
                        //מכניס למערך החומשים את המידע מהקובץ ג'ייסון
                        Chomashes = JsonConvert.DeserializeObject<Chomash[]>(jsonContent);
                    }
                    catch (Exception)
                    {
                        //אם פגום מסיבה כלשהי נאפס כדי שידע לטעון מהטקסט
                        Chomashes = null;
                    }

                }
                //אם אין קובץ ג'ייסון או שהטעינה נכשלה
                if (Chomashes == null)
                {
                    //הפונקציה שמבצעת את כל הפיצולים
                    await LoadFromTextAsync();
                    //פונקציה השומרת עדכונים בקובץ ג'ייסון
                    await SaveToJsonAsync();
                }
            }

        //פונקציה לשמירת הסטורית החיפושים בג'ייסון
        public static async Task SaveSearchHistoryAsync(string description, string word, string chumash, string parasha, string perek)
        {
            try
            {
                // 1. טעינת ההיסטוריה הקיימת מהקובץ
                // 1. טעינת ההיסטוריה
                List<string> history = await GetSearchHistoryAsync();

                // 2. בדיקת כפילות חכמה (סוג + מילה)
                // 2. בדיקת כפילות חכמה (סוג + מילה)
                if (history.Count > 0)
                {
                    string lastEntry = history.First();
                    // אנחנו בודקים האם השורה מסתיימת בדיוק בביטוי שלנו
                    // למשל: האם היא נגמרת ב-"חיפוש טקסט: מרים"

                    if (lastEntry.Contains(description))
                    {
                        return; // כפילות מדויקת - צא בלי לשמור
                    }
                }

                // 3. יצירת השורה החדשה עם תאריך ושעה
                string newEntry = $"{DateTime.Now:dd/MM HH:mm} - {description} ;{word};{chumash};{parasha};{perek}";
                // 4. הוספה לראש הרשימה
                history.Insert(0, newEntry);

                // 5. ניקוי יסודי: אם יש יותר מ-10, תמחק את כל המיותרים (מהישן לחדש)
                while (history.Count > 10)
                {
                    history.RemoveAt(history.Count - 1);
                }

                // 6. שמירה סופית לקובץ ה-JSON
                string json = JsonConvert.SerializeObject(history, Formatting.Indented);
                await File.WriteAllTextAsync(historyPath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine("שגיאה בשמירת ההיסטוריה: " + ex.Message);
            }
        }

        // פונקציה שמחזירה את רשימת החיפושים האחרונים מהקובץ
        public static async Task<List<string>> GetSearchHistoryAsync()
            {
                try
                {
                    if (File.Exists(historyPath))
                    {
                        // קריאת הקובץ
                        string json = await File.ReadAllTextAsync(historyPath, Encoding.UTF8);
                        // המרה חזרה לרשימה
                        return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("שגיאה בטעינת ההיסטוריה: " + ex.Message);
                }
                return new List<string>(); // החזרת רשימה ריקה אם הקובץ לא קיים או שיש שגיאה
            }

        //פונקצית פיצולים אסינכרונית
        private static async Task LoadFromTextAsync()
        {
            string toraTextPath = Path.Combine(AppContext.BaseDirectory, "tora.txt");
            string toraText = await File.ReadAllTextAsync(toraTextPath, Encoding.UTF8);
            toraText = toraText.Replace("\r", ""); // ניקוי תווים נסתרים

            Chomashes = new Chomash[5];
            string[] ChomashesNames = { "בראשית", "שמות", "ויקרא", "במדבר", "דברים" };
            string[] ChomashimText = toraText.Split(new[] { '$' }, StringSplitOptions.RemoveEmptyEntries);

            int codePerek = 0;
            string lastPerekName = "";
            int codePasuk = 0;
            //נשמור את הפרק הנוכחי
            Perek currentPerek = null;


            //פיצול לפי פרשות
            for (int i = 0; i < 5; i++)
            {
                if (i >= ChomashimText.Length) break;
                Chomashes[i] = new Chomash { Name = ChomashesNames[i], Parashas = new List<Parasha>() };

string[] parashasText = ChomashimText[i].Split(new[] { '^' }, StringSplitOptions.RemoveEmptyEntries);                for (int j = 0; j < parashasText.Length; j++)
                {
                    Parasha parasha = new Parasha();
                    int firstLineEnd = parashasText[j].IndexOf('\n');
                    if (firstLineEnd == -1) continue;

                    parasha.Name = parashasText[j].Substring(0, firstLineEnd).Replace("\r", "").Replace("\n", "").Trim();
                    parasha.Start = codePasuk;
                    parasha.ChumashName = ChomashesNames[i];
                    parasha.Prakim = new List<Perek>();
                    Chomashes[i].Parashas.Add(parasha);


                    // פיצול לפי פרקים
                    string[] prakimText = parashasText[j].Split(new[] { '~' });
                    for (int k = 0; k < prakimText.Length; k++)
                    {
                        // 1. טיפול במקרה של תחילת פרשה (פרק שממשיך מפרשה קודמת)
                        if (k == 0 && prakimText[k].Contains('!') && !prakimText[k].Contains('-'))
                        {
                            Perek continuePerek = new Perek
                            {
                                Name = lastPerekName,
                                Id = codePerek,
                                ParashaName = parasha.Name,
                                ChumashName = parasha.ChumashName,
                                Psukim = new List<Pasuk>()
                            };
                            parasha.Prakim.Add(continuePerek);

                            // פיצול פסוקים (הקוד שלך מעולה כאן)
                            string[] psukimParts = prakimText[k].Split(new[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var part in psukimParts)
                            {
                                ParseAndAddPasuk(part, continuePerek, parasha, ref codePasuk);
                            }
                        }
                        // 2. טיפול בפרק חדש רגיל (שיש בו סימן -)
                        else if (prakimText[k].Contains('-'))
                        {
                            int dashIdx = prakimText[k].IndexOf('-');
                            Perek perek = new Perek();

                            // חילוץ שם הפרק (הקוד שלך מצוין כאן)
                            int exclamationIdx = prakimText[k].IndexOf('!', dashIdx);
                            int endOfName = (exclamationIdx != -1) ? exclamationIdx : prakimText[k].Length;
                            string rawName = prakimText[k].Substring(dashIdx + 1, endOfName - (dashIdx + 1));
                            perek.Name = rawName.Split(new[] { '\n', '!' }, StringSplitOptions.None)[0].Trim();

                            if (perek.Name != lastPerekName)
                            {
                                codePerek++;
                                lastPerekName = perek.Name;
                            }

                            perek.Id = codePerek;
                            perek.ParashaName = parasha.Name;
                            perek.ChumashName = parasha.ChumashName;
                            perek.Psukim = new List<Pasuk>();
                            parasha.Prakim.Add(perek);

                            // פיצול פסוקים בתוך הפרק החדש
                            string[] psukimParts = prakimText[k].Split(new[] { '!' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (var part in psukimParts)
                            {
                                ParseAndAddPasuk(part, perek, parasha, ref codePasuk);
                            }
                        }
                        parasha.End = codePasuk;
                    }
                }
            }
        }

        private static void ParseAndAddPasuk(string part, Perek perek, Parasha parasha, ref int codePasuk)
        {
            string cur = part.Trim();
            int open = cur.IndexOf('{'), close = cur.IndexOf('}');
            if (open != -1 && close > open)
            {
                Pasuk pasuk = new Pasuk
                {
                    Id = codePasuk++,
                    Name = cur.Substring(open + 1, close - open - 1).Trim(),
                    Text = cur.Substring(close + 1).Trim(),
                    ParashaName = parasha.Name,
                    ChumashName = parasha.ChumashName,
                    PerekName = perek.Name
                };
                perek.Psukim.Add(pasuk);
            }
        }

        //הופכת את הפיצולים לקובץ טקסט מסוג ג'ייסון בצורה אסינכרונית
        private static async Task SaveToJsonAsync()
            {
                try
                {
                    //הופכת את הנתונים - האוביקטים בזיכרון והפכת אותם למחרוזת טקסט
                    string json = JsonConvert.SerializeObject(Chomashes, Formatting.Indented);
                    //מדפיסה את המידע לתוך קובץ הג'ייסון
                    await File.WriteAllTextAsync(jsonPath, json, Encoding.UTF8);
                }
                catch (Exception ex)
                {
                    // כאן אפשר להוסיף הדפסה למסך למטרת דיבאג
                    Console.WriteLine("שגיאה בשמירת ה-JSON: " + ex.Message);
                }
            }
        }
    }




