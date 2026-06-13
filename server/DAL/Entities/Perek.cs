using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Perek
    {
        public int Id { get; set; }//קוד מזהה

        public string Name { get; set; }//שם

        public string ParashaName { get; set; }//שם הפרשה

        public string ChumashName { get; set; }//שם החומש

        public List<Pasuk> Psukim { get; set; }//רשימת פסוקים

    }
}
