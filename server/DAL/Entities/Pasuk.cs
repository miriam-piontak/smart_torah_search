namespace DAL.Entities
{
    public class Pasuk
    {
        public int Id { get; set; }//קוד מזהה
        public string Name { get; set; }//שם

        public string ParashaName { get; set; }//שם הפרשה

        public string ChumashName { get; set; }//שם החומש

        public string PerekName { get; set; }//שם הפרק

        public string Text { get; set; }//טקסט הפסוק
        
    }
}
