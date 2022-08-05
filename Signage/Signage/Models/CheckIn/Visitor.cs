namespace Signage.Models.CheckIn
{
    public class Visitor
    {
        public int id;
        public string name;
        public string company;

        public Visitor(string id, string name)
        {
            this.id = int.Parse(id);
            this.name = name;
        }

        public Visitor(int flag, string name, string company)
        {
            this.name = name;
            this.company = company;
        }
    }
}
