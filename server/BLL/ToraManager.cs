using DAL.Entities;
using DAL.Services;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BLL
{
    public class ToraManager
    {

        //משתנה שישמור לי רשימת פסוקים
        private List<Pasuk> _allPsukim = new List<Pasuk>();
        // רשימה שתשמור את מילות החיפוש האחרונות
        private static List<string> _searchHistory = new List<string>();
        //מילון לכל האותיות
        private readonly Dictionary<char, int> _gematriaMap = new Dictionary<char, int>
        {
            {'א', 1}, {'ב', 2}, {'ג', 3}, {'ד', 4}, {'ה', 5}, {'ו', 6}, {'ז', 7}, {'ח', 8}, {'ט', 9},
            {'י', 10}, {'כ', 20}, {'ך', 20}, {'ל', 30}, {'מ', 40}, {'ם', 40}, {'נ', 50}, {'ן', 50},
            {'ס', 60}, {'ע', 70}, {'פ', 80}, {'ף', 80}, {'צ', 90}, {'ץ', 90}, {'ק', 100}, {'ר', 200},
            {'ש', 300}, {'ת', 400}
        };
        //קונסטרקטור
        public ToraManager() { }

        //פונקציה אסינכרונית
        public async Task InitializeAsync()
        {
            //טוענת מהדאל
            await ToraRepository.InitAsync();

            // 2. רק עכשיו משטח את הפסוקים
            _allPsukim = await GetAllPasuksAsync();
        }

        //פונקציה לשליפת ההסטוריה
        public async Task<List<string>> GetSearchHistoryAsync()
        {
            return await ToraRepository.GetSearchHistoryAsync();
        }

        //שליפת שמות החומשים
        public async Task<List<string>> GetComashNamesAsync()
        {// הגנה: אם הרשימה ריקה ב-Repository, נריץ את הטעינה מה-txt/json
            if (ToraRepository.Chomashes == null || ToraRepository.Chomashes.Length == 0)
            {
                await InitializeAsync();
            }

            // הרצה במשימה נפרדת כדי לא לחסום את ממשק המשתמש (UI)
            return await Task.Run(() =>
            {
                return ToraRepository.Chomashes
                    .Select(item => item.Name)
                    .ToList();
            });
        }

        //עבור כל חומש - שליפת שמות הפרשות
        public async Task<List<string>> GetParashasNamesByChumashAsync(string chumashName)
        {
            //המשתמש עוד לא בחר חומש
            if (string.IsNullOrEmpty(chumashName))
                //יחזיר רשימה ריקה
                return new List<string>();
            //מציאת אוביקט בחומש שבחר
            var selectedChumash = ToraRepository.Chomashes.FirstOrDefault(c => c.Name == chumashName);
            //אם לא מצאנו
            if (selectedChumash == null)
                //יחזיר רשימה ריקה
                return new List<string>();
            //יחזיר רשימת שמות הפרשות מתוך החומש
            return await Task.Run(() =>
            {
                return selectedChumash
                .Parashas
                .Select(p => p.Name)//יקח רק שם הפרשה
                .ToList();

            });
        }

        //שליפת כל הפרשות בכללי אם לא בחר חומש ספציפי
        public async Task<List<string>> GetAllParashasNamesAsync()
        {
            if (_allPsukim == null)
                return new List<string>();

            // כאן אנחנו פשוט מוציאים את השמות ושומרים על הסדר המקורי
            return await Task.Run(() =>
            {
                return _allPsukim
                        .Select(p => p.ParashaName)
                        .Distinct()                 // לוקח את המופע הראשון של כל פרשה לפי הסדר
                        .ToList();
            });
        }


        //שליפת שמות כל הפרקים גם אם לא בחר חומש או פרשה ספציפית
        public async Task<List<string>> GetAllPerekNamesAsync()
        {
            if (_allPsukim == null)
                return new List<string>();

            return await Task.Run(() =>
            {
                return _allPsukim
                .Select(p => p.PerekName)
                .Distinct()
                .ToList();
            });

        }

        //שולף את רשימת הפרקים של הפרשה שהתקבלה
        public async Task<List<string>> GetPereksNamesByParashaAsync(string chumashName, string parashaName)
        {
            //המשתמש עוד לא בחר חומש או פרשה
            if (string.IsNullOrEmpty(chumashName) || string.IsNullOrEmpty(parashaName))
                //יחזיר רשימה ריקה
                return new List<string>();

            // 2. הבדיקה הקריטית: האם הנתונים בכלל קיימים בזיכרון?
            if (ToraRepository.Chomashes == null || ToraRepository.Chomashes.Length == 0)
            {
                // אם לא, אנחנו חייבים לקרוא לפונקציית הטעינה (נניח שקראת לה InitializeAsync)
                await InitializeAsync();
            }
            //מציאת החומש שבחר
            var selectedChumash = ToraRepository.Chomashes.FirstOrDefault(c => c.Name == chumashName);
            //אם לא מצאנו
            if (selectedChumash == null)
                //יחזיר רשימה ריקה
                return new List<string>();
            //מציאת הפרשה שבחר
            var selectedParasha = selectedChumash.Parashas.FirstOrDefault(p => p.Name == parashaName);
            //אם לא מצאנו
            if (selectedParasha == null)
                //יחזיר רשימה ריקה
                return new List<string>();
            //שליפת שמות הפרקים מתוך הפרשה
            return await Task.Run(() =>
            {
                return selectedParasha
                    .Prakim
                    .Select(k => k.Name)
                    .ToList();
            });
        }

        //שולפת רשימת פרקים עבור חומש
        public async Task<List<string>> GetPereksNamesByChumashAsync(string chumashName)
        {
            // הוספת בדיקת טעינה
            if (ToraRepository.Chomashes == null) await InitializeAsync();

            //המשתמש עוד לא בחר חומש או פרשה
            if (string.IsNullOrEmpty(chumashName))
                //יחזיר רשימה ריקה
                return new List<string>();

            return await Task.Run(() =>
            {
                var chumash = ToraRepository.Chomashes.FirstOrDefault(c => c.Name == chumashName);
                if (chumash == null) return new List<string>();



                return chumash.Parashas
                    .SelectMany(p => p.Prakim)
                    .Select(pr => pr.Name)
                    .Distinct()
                    .ToList();
            });

        }
        //פונקציה שכותבת מחרוזת להסטוריה
        public async Task<string> GetHistoryDescriptionAsync(string type, string word, string chumash, string parasha, string perek)
        {
            string description = "";

            // 1. שורת כותרת: מה חיפשנו?
            if (!string.IsNullOrWhiteSpace(word))
            {
                description = $"{type}: \"{word}\"";
            }
            else
            {
                description = "סינון תוכן בתורה";
            }

            // 2. שורת מיקום: רק מה שאינו "הכל"
            List<string> locationParts = new List<string>();
            if (chumash != "הכל") locationParts.Add($"חומש {chumash}");
            if (parasha != "הכל") locationParts.Add($"{parasha}");
            if (perek != "הכל") locationParts.Add($"פרק {perek}");

            if (locationParts.Count > 0)
            {
                description += " | " + string.Join(", ", locationParts);
            }

            return description;
        }

        //פונקציה המקבלת פרשה ומחזירה את החומש אליו הפרשה שייכת
        public async Task<string> GetChumashByParashaAsync(string Parasha)
        {


            // 2. הבדיקה הקריטית: האם הנתונים בכלל קיימים בזיכרון?
            if (ToraRepository.Chomashes == null || ToraRepository.Chomashes.Length == 0)
            {
                // אם לא, אנחנו חייבים לקרוא לפונקציית הטעינה (נניח שקראת לה InitializeAsync)
                await InitializeAsync();
            }
            if (ToraRepository.Chomashes == null) return null;

            // חיפוש החומש שמכיל את הפרשה הזו
            var chumash = ToraRepository.Chomashes.FirstOrDefault(c =>
                c.Parashas.Any(p => p.Name == Parasha));

            return chumash?.Name; // יחזיר את שם החומש או null אם לא נמצא
        }

        //פונקציה למציאת כל הפסוקים
        public async Task<List<Pasuk>> GetPasukimNamesAsync(string chumashName, string parashaName, string perekName)
        {
            // א. חייבים לוודא שהנתונים נטענו מה-JSON/TXT לפני שמתחילים לחפש!
            if (ToraRepository.Chomashes == null || ToraRepository.Chomashes.Length == 0)
            {
                await InitializeAsync();
            }

            // ב. חיפוש החומש
            var chumash = ToraRepository.Chomashes.FirstOrDefault(c => c.Name == chumashName);

            // ג. חיפוש הפרשה
            var parasha = chumash?.Parashas.FirstOrDefault(p => p.Name == parashaName);

            // ד. חיפוש הפרק
            var perek = parasha?.Prakim.FirstOrDefault(pr => pr.Name == perekName);

            // ה. החזרת הפסוקים (אם משהו בדרך היה null, תחזור רשימה ריקה)
            return perek?.Psukim ?? new List<Pasuk>();
        }



        //פונקציה שתשטח את המבנים וכך תווצר לי רשימת פסוקים
        public async Task<List<Pasuk>> GetAllPasuksAsync()
        {

            // 1. ודואים שהנתונים נטענו ומחכים שהטעינה תסתיים באמת
            if (ToraRepository.Chomashes == null || ToraRepository.Chomashes.Length == 0)
            {
                await InitializeAsync();
            }

            // 2. בדיקה נוספת למקרה שהטעינה נכשלה (למשל הקובץ לא נמצא)
            if (ToraRepository.Chomashes == null)
                return new List<Pasuk>();


            //ישטח את המבנה
            return ToraRepository.Chomashes
                .SelectMany(c => c.Parashas)
                .SelectMany(p => p.Prakim)
                .SelectMany(s => s.Psukim)
                .ToList();
        }


        //פונקציה לחיפוש מילה בתורה
        public async Task<List<Pasuk>> SearchByWordAsync(string word)
        {
            // אם המילה ריקה או רק רווחים - תחזיר רשימה ריקה מיד בלי לחפש בתורה
            if (string.IsNullOrWhiteSpace(word)) return new List<Pasuk>();
            //שמירת הפעולה בהסטוריה
            //יחזיק רשימה של כל הפסוקים המכילים את המילה שהתקבלה
            return _allPsukim.Where(p => p.Text.Replace(":", "").Replace("-", "").Contains(word)).ToList();
        }

        //פונקציה לחיפוש מילה שלמה בתורה
        public async Task<List<Pasuk>> SearchByWholeWordAsync(string word)
        {
            // אם המילה ריקה או רק רווחים - תחזיר רשימה ריקה מיד בלי לחפש בתורה
            if (string.IsNullOrWhiteSpace(word)) return new List<Pasuk>();
            //שמירה להיסטוריה
            //יצור מילה המכילה את המילה שהתקבלה רק עם שמירת רווחים בצדדיה
            string word2 = $@"\b{Regex.Escape(word)}\b";
            //יחזיר רשימה של כל הפסוקים המכילים את המילה שהתקבלה בצורה שלמה
            return _allPsukim.Where(p => Regex.IsMatch(p.Text, word2)).ToList();

        }
        //פונקצית חיפוש לפי מיקום
        public async Task<List<Pasuk>> GetPasuksByLocationAsync(string chumash, string parasha, string perek)
        {
            //נסנן את הרשימה בשלבים
            var query = _allPsukim.AsEnumerable();
            //המשתמש הכניס  שם חומש
            if (!string.IsNullOrEmpty(chumash) && chumash != "הכל")
            {
                query = query.Where(p => p.ChumashName == chumash);
            }
            //המשתמש הכניס  שם פרשה
            if (!string.IsNullOrEmpty(parasha) && parasha != "הכל")
            {
                query = query.Where(s => s.ParashaName == parasha);

            }
            //המשתמש הכניס  שם פרק
            if (!string.IsNullOrEmpty(perek) && perek != "הכל")
            {
                query = query.Where(r => r.PerekName == perek);

            }
            return query.ToList();
        }


        //פונקציה לחיפוש פסוק מתאים לשם שהתקבל כפרמטר בשביל הסגולה
        public async Task<List<Pasuk>> GetPasuksByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return new List<Pasuk>();

            // טעינת הגנה אם הרשימה ריקה
            if (_allPsukim == null || _allPsukim.Count == 0) await InitializeAsync();

            string newName = name.Trim();
            string firstLetter = newName.Substring(0, 1);
            string lastLetter = newName.Substring(newName.Length - 1);

            // תווים שאנחנו רוצים להוריד מהקצוות כדי להגיע לאות נטו
            char[] extraChars = { ' ', ':', '.', '!', '}', '{', '\r', '\n', '\t' };

            return _allPsukim.Where(p =>
            {
                if (string.IsNullOrWhiteSpace(p.Text)) return false;

                // ניקוי יסודי של הפסוק
                string cleanedText = p.Text.Trim(extraChars);

                // בדיקה עם הדפסה לדיבאג (תוכלי להסיר אחרי שיעבוד)
                bool match = cleanedText.StartsWith(firstLetter) && cleanedText.EndsWith(lastLetter);

                return match;
            }).ToList();
        }

        public async Task GetPereksNamesByParashaOrChumashAsync(string? v, Func<string?> toString)
        {
            throw new NotImplementedException();
        }

        //פונקציה המקבלת מילה ובודקת את הגימטריה שלה בערך מספרי
        public int getGimatria(string word)
        {
            // ניקוי תווים שאינם אותיות עבריות לפני החישוב
            var cleanWord = new string(word.Where(c => _gematriaMap.ContainsKey(c)).ToArray());
            return cleanWord.Sum(c => _gematriaMap[c]);
        }

        //פונקציה המקבלת מספר ומחזירה את כל המילים בתנך שהגימטריה שלהם זהה למילה שהתקבלה
        public async Task<List<Pasuk>> getAllGimetriaAsync(int num)
        {
            if (num == 0) return new List<Pasuk>();

            // הגדרת תווים מפרידים נפוצים בתנ"ך
            char[] separators = { ' ', '-', '־', ':', '.', ',' };

            return await Task.Run(() =>
            {
                return _allPsukim.Where(p =>
                    p.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                    .Any(word => getGimatria(word) == num)
                ).ToList();
            });
        }

        //פונקציה המקבלת מילה ומחזירה את כל הפסוקים המכילים את ר"ת של המילה
        public async Task<List<Pasuk>> getRasheiTevotAsync(string word)
        {

            return _allPsukim.Where(p =>
            {
                var words = p.Text.Split(new[] { ' ', '-', '־' }, StringSplitOptions.RemoveEmptyEntries);

                // יצירת טווח אינדקסים ובדיקה האם קיים (Any) אינדקס שמתחיל רצף מתאים
                return Enumerable.Range(0, words.Length - word.Length + 1)
                    .Any(i => words.Skip(i)                 // דלג לתחילת הרצף הנוכחי
                                   .Take(word.Length) // קח את כמות המילים המתאימה
                                   .Select(w => w[0])       // תוציא מכל מילה רק את האות הראשונה
                                   .SequenceEqual(word) // תשווה לרצף האותיות שחיפשנו
                    );
            }).ToList();
        }
        //פונקציה המקבלת מילה ומחזירה את כל הפסוקים המכילים את ס"ת של המילה

        // פונקציה ראשית - מחזירה את כל הפסוקים המכילים את סופי התיבות של המילה
        public async Task<List<Pasuk>> GetSofeiTevotAsync(string word)
        {
            // 1. הגנה: ודואים שהנתונים נטענו לזיכרון
            if (_allPsukim == null || _allPsukim.Count == 0)
            {
                _allPsukim = await GetAllPasuksAsync();
            }

            // 2. סינון הפסוקים לפי פונקציית העזר
            // שימוש ב-Task.Run כדי שהחיפוש הכבד לא יתקע את השרת
            return await Task.Run(() =>
            {
                return _allPsukim
                    .Where(p => IsMatchSofeiTevot(p.Text, word))
                    .ToList();
            });
        }

        // פונקציית עזר פרטית - הלוגיקה של החיפוש
        private bool IsMatchSofeiTevot(string text, string target)
        {
            // פיצול הפסוק למילים (כולל טיפול במקפים)
            var words = text.Split(new[] { ' ', '-', '־' }, StringSplitOptions.RemoveEmptyEntries);

            // נירמול מילת החיפוש (הפיכת אותיות סופיות לרגילות)
            string normalizedTarget = NormalizeHebrew(target);

            // רצים על המילים ב"חלון צף" באורך המילה שחיפשנו
            for (int i = 0; i <= words.Length - normalizedTarget.Length; i++)
            {
                // מוציאים את האות האחרונה מכל מילה ברצף הנוכחי
                var currentChars = words.Skip(i)
                                        .Take(normalizedTarget.Length)
                                        .Select(w => w.Trim().Last());

                // יוצרים מחרוזת ומנרמלים אותה להשוואה
                string currentSequence = NormalizeHebrew(new string(currentChars.ToArray()));

                if (currentSequence == normalizedTarget) return true;
            }
            return false;
        }

        // פונקציית עזר לנירמול אותיות סופיות (ץ -> צ)
        private string NormalizeHebrew(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;
            return input.Replace('ך', 'כ')
                        .Replace('ם', 'מ')
                        .Replace('ן', 'נ')
                        .Replace('ף', 'פ')
                        .Replace('ץ', 'צ');
        }

        public async Task<List<Pasuk>> GetPasuksByLocation(string chumash, string parasha, string perek)
        {
            throw new NotImplementedException();
        }
    }
}
