namespace Signage.Models.Bottom
{
    public class BottomInfo
    {
        public List<EmployeeModel> employees;
        public string quote;
        public List<Event> events;

        public BottomInfo()
        {
            employees = new List<EmployeeModel>();
            events = new List<Event>();
            quote = "";
        }
    }
}
