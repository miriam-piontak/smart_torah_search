using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Chomash
    {
        public string Name { get; set; }//שם
        public List<Parasha> Parashas { get; set; }//רשימת פרשות

    }
}
