namespace Signage.Models.OSR
{
    public class MTDModel
    {
        public String name;
        public string OrderSales;
        public String InvSales;
        public int rank;
        public MTDModel(string name,string order, string sales,string rank)
        {
            this.name = name;
            this.OrderSales = order;
            this.InvSales = sales;
            this.rank = int.Parse(rank);
        }
    }
}
