namespace Intranet.Models.Signage.ManageEmployee
{
    public class Employee
    {
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDate { get; set; }
        public string devision { get; set; }
        public string title { get; set; }
        public string role { get; set; }
        public string location { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        public Employee(string id, string firstName, string lastName, string email, string birthday, string startDate, string devision, string title, string role, string location, string phone)
        {
            Id = int.Parse(id);
            this.firstName = firstName;
            this.lastName = lastName;
            if(!string.IsNullOrEmpty(birthday))
                Birthday = DateTime.Parse(birthday);

            if (!string.IsNullOrEmpty(startDate))
                StartDate = DateTime.Parse(startDate);

           this.devision = devision;
            this.title = title;
            this.role = (role);
            this.location = location;
            this.phone = phone;
            this.email = email;

        }
    }
}
