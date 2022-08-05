namespace Signage.Models.Bottom
{
    public class EmployeeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }
        public string anniversary { get; set; }
        public int yearWorked { get; set; }

        public EmployeeModel(string id, string fname, string lname, string bd, string anni)
        {
            this.id = Convert.ToInt32(id);
            name = fname + " " + lname;

            birthday = Convert.ToDateTime(bd).ToString("MMMM d");
            anniversary = Convert.ToDateTime(anni).ToString("MMMM d");
            yearWorked = DateTime.Now.Year - Convert.ToDateTime(anni).Year;
        }

        public EmployeeModel(string id, string fname, string lname)
            {
            this.id = Convert.ToInt32(id);
            name = fname + " " + lname;
        }
    }
}
