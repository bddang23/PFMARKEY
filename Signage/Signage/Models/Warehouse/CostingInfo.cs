namespace Signage.Models.Warehouse
{
    public class CostingInfo
    {
        public string ShippingCharges;
        public string AvgCostPerPackage;
        public string CostPerPound;
        public string Surcharges;
        public string AuditSavings;
        public string Date;
        public CostingInfo(string shippingCharges, string avgCostPerPackage, string costPerPound, string surchages, string auditSavings, string date)
        {
            ShippingCharges = String.Format("{0:C}", Double.Parse(shippingCharges));
            AvgCostPerPackage = String.Format("{0:C}", Double.Parse(avgCostPerPackage));
            CostPerPound = String.Format("{0:C}", Double.Parse(costPerPound));
            Surcharges = String.Format("{0:C}", Double.Parse(surchages));
            AuditSavings = String.Format("{0:C}", Double.Parse(auditSavings));
            Date = date;
        }
    }
}
