namespace Signage.Models.AutoCribs
{
    public class InventoryStats
    {
        public string name;
        public int items;
        public int outOfStock;
        public int red;
        public int yellow;
        public double inStock;

        public InventoryStats(string name, string items, string outOfStock, string inventory, string deadInventory, string inStock)
        {
            this.name = name;
            this.items = int.Parse(items);
            this.outOfStock = int.Parse(outOfStock);
            this.red = int.Parse(inventory);
            this.yellow = int.Parse(deadInventory);
            this.inStock = double.Parse(inStock);
        }
    }
}
