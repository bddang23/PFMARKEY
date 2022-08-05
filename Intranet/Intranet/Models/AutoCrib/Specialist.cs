namespace Intranet.Models.AutoCrib
{
    public class Specialist
    {
        public int id;
        public string name;
        public int numOfAcc;
        public int items;
        public int outOfStock;
        public int red;
        public int yellow;
        public decimal inStock;
        public bool selected;

        public Specialist(string id, string name, string count, string Items, string OofS, string Red, string Yellow, string inS)
        {
            this.id = int.Parse(id);
            this.name = name;
            numOfAcc = int.Parse(count);
            items = int.Parse(Items);
            outOfStock = int.Parse(OofS);
            red = int.Parse(Red);
            yellow = int.Parse(Yellow);
            inStock = decimal.Parse(inS);
        }
        public Specialist(string id, string name)

        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                this.id = 0;
                this.name = "";
            }
            else
            {
                this.id = int.Parse(id);
                this.name = name;
            }
        }

        public Specialist()
        {
        }
    }
}
