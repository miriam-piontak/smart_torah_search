using Newtonsoft.Json;//שימוש בקובץ ג'ייסון
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;// נדרש עבור Encoding.UTF8
using System.Threading.Tasks; // נדרש עבור Task


namespace DAL.Entities
{
    public static class Tora
    {
        public static Chomash[] Chomashes { get; set; }//מערך חמישה חומשים
    }
}