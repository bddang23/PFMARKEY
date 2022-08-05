namespace Signage2.Models.OSR
{
    public class YTDModel
    {
        public string name;
        public double YTDSales;
        public double LastYearYTDSales;
        public double grow;
        public string color;
        public int newCustomer;
        public double newCustomerSales;
        public int rank;

        public YTDModel(string name, string yTDSales, string lastYearSales, string newCustomer, string newCustomerSales, string rank)
        {
            this.name = name;
            string sales = yTDSales.Replace(",", "").Replace("$", "").Trim();
            YTDSales = Double.Parse(sales);
            string lastYear = lastYearSales.Replace(",", "").Replace("$", "").Trim();
            LastYearYTDSales = Double.Parse(lastYear);
            grow = (YTDSales - LastYearYTDSales) / LastYearYTDSales;
            if (grow >= 0) color = "green";
            else color = "red";
            this.newCustomer = int.Parse(newCustomer);
            string newCusSales = newCustomerSales.Replace(",", "").Replace("$", "").Trim();
            this.newCustomerSales = double.Parse(newCusSales);
            this.rank = int.Parse(rank);
        }



        //double costSavings;
        //double percentSales;

    }
}
