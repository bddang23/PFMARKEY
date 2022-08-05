namespace Intranet.Models.Business
{
    public class CSRGoal
    {
        public double sales_Day;
        public double sales_Month;
        public DateTime month;

        public CSRGoal(string sales_Day, string sales_Month, string month)
        {
            this.sales_Day = double.Parse(sales_Day);
            this.sales_Month = double.Parse(sales_Month);
            this.month = DateTime.Parse(month);
        }
    }
}
