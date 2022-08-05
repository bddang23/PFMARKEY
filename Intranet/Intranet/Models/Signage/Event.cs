namespace Intranet.Models.Signage
{
    public class Event
    { 
        public int id { get; set; }
        public string table { get; set; }

        public String title { get; set; }

        public String start { get; set; }

        public String end { get; set; }

        public bool allDay { get; set; }


        public String color { get; set; }

        public int getID()
        {
            return id;
        }
        public Event(int id, string table,string title, string start, string end, string allDay)
        {
            this.id = id;

            this.title = title;
            this.start = start;

            if (String.IsNullOrEmpty(end))
                this.end = CopyStart(start);
            else
                this.end = end;

            if (int.Parse(allDay) == 1) this.allDay = true;
            else this.allDay = false;
            this.color = "default";

            this.id = id;
            this.table = table;
        }

        private string CopyStart(string start)
        {
            string result = "";
            if (start.Contains('/'))
            {

                DateTime dt = DateTime.Parse(start);
                dt = dt.AddMinutes(719);
                result = dt.ToString("G");

            }
            return result;
        }

        public Event(string title, string date)
        {
            this.id = 0;
            this.table = "BD/Anni";

            this.title = title;
            start = date;
            end = CopyStart(start);

            allDay = true;
            if (title.Contains("Birthday"))
                this.color = "pink";
            else if (title.Contains("Anniversary"))
                this.color = "green";
            else
                this.color = "default";


        }

    }

}
