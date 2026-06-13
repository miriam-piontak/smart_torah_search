using BLL;
using DAL.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Web_API.Controllers
{
    [Route("api/[controller]")]
    public class ToraController : Controller
    {
        private readonly ToraManager _manager;

        // השרת מזריק לכאן את המנג'ר באופן אוטומטי
        public ToraController(ToraManager manager)
        {
            _manager = manager;
        }

        //שליפת ההסטוריה
        [HttpGet("get-history")]
        public async Task<IActionResult> GetSearchHistoryAsync()
        {
            var results = await _manager.GetSearchHistoryAsync();
            if (results == null||results.Count == 0)
                return NotFound("לא נמצאו פעולות אחרונות");
            return Ok(results);
        }
        //שליפת שמות החומשים

        [HttpGet("chumash-names")]
        public async Task<IActionResult> GetComashNamesAsync2()
        {
            var results = await _manager.GetComashNamesAsync();
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו חומשים במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }


        //שליפת הפרשות עבור חומש
        [HttpGet("get-parashas-by-chumash/{chumashName}")]
        public async Task<IActionResult> GetParashasNamesByChumashAsync2(string chumashName)
        {
            var results= await _manager.GetParashasNamesByChumashAsync(chumashName);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו חומשים במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }

        //שליפת שמות הפרשות
        [HttpGet("parashas-names")]
        public async Task<IActionResult> GetAllParashasNamesAsync2()
        {
            var results=await _manager.GetAllParashasNamesAsync();
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פרשות במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }

        //שליפת כל הפרקים
        [HttpGet("pereks-names")]
        public async Task<IActionResult> GetAllPerekNamesAsync2()
        {
            var results=await _manager.GetAllPerekNamesAsync();
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פרקים במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }

        //שליפת הפרקים לפי פרשה וחומש
        [HttpGet("pereks-by-parasha/{chumashName}/{parashaName}")]
        public async Task<IActionResult> GetPereksNamesByParashaAsync2(string chumashName, string parashaName)
        {
            var results = await _manager.GetPereksNamesByParashaAsync(chumashName, parashaName);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פרקים במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }


        //שליפת רשימת פרקים עבור חומש
        [HttpGet("pereks-by-chumash/{chumashName}")]
        public async Task<IActionResult> GetPereksNamesByChumashAsync2(string chumashName)
        {
            var results = await _manager.GetPereksNamesByChumashAsync(chumashName);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פרקים במערכת");
            return Ok(results); // מחזיר JSON לריאקט
        }

        //כתיבת שורה להסטוריה
        [HttpGet("line-to-history/{type}/{word}/{chumash}/{parasha}/{perek}")]
        public async Task<IActionResult> GetHistoryDescription2(string type, string word, string chumash, string parasha, string perek)
        {
            var results =  _manager.GetHistoryDescriptionAsync(type, word, chumash, parasha,perek);
            return Ok(results); // מחזיר JSON לריאקט

        }

        //מחזירה את החומש לפי הפרשה
        [HttpGet("chumash-to-parasha/{parasha}")]
        public async Task<IActionResult> GetChumashByParashaAsync2(string parasha)
        {
            var result = await _manager.GetChumashByParashaAsync(parasha);

            if (string.IsNullOrEmpty(result))
            {
                return NotFound("הפרשה לא נמצאה, לכן לא ניתן לשייך אותה לחומש");
            }

            return Ok(result);
        }

        // מחזירה את שמות כל הפסוקים לפי הפרמטרים שהוכנסו
        [HttpGet("all-psukim-by-all/{chumashName}/{parashaName}/{perekName}")]
        public async Task<IActionResult> GetPasukimNamesAsync2(string chumashName, string parashaName, string perekName)
        {
            var results = await _manager.GetPasukimNamesAsync(chumashName, parashaName, perekName);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פסוקים במערכת");
            return Ok(results);
        }

        //מחזירה את כל הפסוקים בתנ"ך
        [HttpGet("all-psukim/")]
        public async Task<IActionResult> GetAllPasuksAsync2()
        {
            var results = await _manager.GetAllPasuksAsync();
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פסוקים במערכת");
            return Ok(results);
        }

        //חיפוש מילה בתורה
        [HttpGet("search-word/{word}")]
        public async Task<IActionResult> SearchByWordAsync2(string word)
        {
            var results=await _manager.SearchByWordAsync(word);
            if (results == null || results.Count == 0)
                return NotFound("המילה לא נמצאה");
            return Ok(results);
        }

        //חיפוש מילה בתורה במדויק
        [HttpGet("exact-search-word/{word}")]
        public async Task<IActionResult> SearchByWholeWordAsync(string word)
        {
            var results = await _manager.SearchByWholeWordAsync(word);
            if (results == null || results.Count == 0)
                return NotFound("המילה לא נמצאה");
            return Ok(results);
        }

        //פונקצית חיפוש לפי מיקום
        [HttpGet("filtered-by-location/{chumash}/{parasha}/{perek}")]
        public async Task<IActionResult> GetPasuksByLocationAsync2(string chumash, string parasha, string perek)
        {
            var results = await _manager.GetPasuksByLocationAsync(chumash, parasha,perek);
            if (results == null || results.Count == 0)
                return NotFound("הנתונים לא נטענו");
            return Ok(results);
        }

        //פונקצית מציאת סגולה לפי שם
        [HttpGet("pasuk-by-name-sgula/{word}")]
        public async Task<IActionResult> GetPasuksByNameAsync2(string word)
        {
            var results = await _manager.GetPasuksByNameAsync(word);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצא פסוק מתאים");
            return Ok(results);

        }

        //פונקציה למציאת גימטריה לפי מילה
        [HttpGet("get-gimatria-to-word/{word}")]
        public async Task<IActionResult> getGimatria2(string word)
        {
            var result =  _manager.getGimatria(word);
            if (result == null)
                return NotFound("אירעה שגיאה");
            return Ok(result);

        }

        //מחזירה את כל הפסוקים בצנ"ך שמכילים מילה עם ערך גימטריה זהה
        [HttpGet("gimeria-by-number/{number}")]
        public async Task<IActionResult> GetByNumberAsync(int number)
        {
            var results = await _manager.getAllGimetriaAsync(number);
            return Ok(results); // מחזיר JSON לריאקט
        }

        //מחזירה את כל הפסוקים שמהווים את ר"ת של  המילה
        [HttpGet("rashei-tevot-by-word/{word}")]
        public async Task <IActionResult> getRasheiTevotAsync2(string word)
        {
            var results = await _manager.getRasheiTevotAsync(word);
            if(results == null || results.Count==0)
                return NotFound("לא נמצאו פסוקים מתאימים");
            return Ok(results);

        }

        //מחזירה את כל הפסוקים שמהווים את סופי התבות של המילה
        [HttpGet("sofei-tevot-by-word/{word}")]
        public async Task<IActionResult> getSofeiTevotAsync2(string word)
        {
            var results = await _manager.GetSofeiTevotAsync(word);
            if (results == null || results.Count == 0)
                return NotFound("לא נמצאו פסוקים מתאימים");
            return Ok(results);
        }




    }
}
