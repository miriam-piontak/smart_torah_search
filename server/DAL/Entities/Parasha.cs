using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Parasha
    {
        public string Name { get; set; }//שם

        public string ChumashName { get; set; }//שם החומש
        public List<Perek> Prakim { get; set; }//רשימת פרקים

        public int Start { get; set; }//הקוד של הפס' הראשון בפרשה 
         
        public int End { get; set; }//הקוד של הפס' האחרון בפרשה

    }
}
    