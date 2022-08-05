namespace Signage.Models.Bottom
{
    public class Event
    {
        public String title { get; set; }
        public String date { get; set; }

        public Event(string title, string date)
        {
            this.title = title;
            this.date = date;
        }
    }
}
