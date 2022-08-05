namespace Intranet.Models.AutoCrib
{
    public class Company
    {
        public int id;
        public string name;
        public int items;
        public int outOfStock;
        public int red;
        public int yellow;
        public decimal inStock;
        public Specialist assignedSpecialist;

        public Company(string Id, string name, string items, string outOfStock, string red, string yellow, string inStock)
        {
            this.id = int.Parse(Id);
            this.name = name;
            this.items = int.Parse(items);
            this.outOfStock = int.Parse(outOfStock);
            this.red = int.Parse(red);
            this.yellow = int.Parse(yellow);
            this.inStock = decimal.Parse(inStock);
        }
    }
}
