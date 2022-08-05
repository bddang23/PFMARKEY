namespace Intranet.Models.Warehouse
{
    public class Veriship
    {
        public int id;
        public double shippingCharges;
        public double avgCostPerPackage;
        public double costPerPound;
        public double surcharges;
        public double auditSaving;
        public DateTime dateFor;

        public Veriship(string id, string shippingCharges, string avgCostPerPackage, string costPerPound, string surcharges, string auditSaving, string dateFor)
        {
            this.id = int.Parse(id);
            this.shippingCharges = double.Parse(shippingCharges);
            this.avgCostPerPackage = double.Parse(avgCostPerPackage);
            this.costPerPound = double.Parse(costPerPound);
            this.surcharges = double.Parse(surcharges);
            this.auditSaving = double.Parse(auditSaving);
            this.dateFor = DateTime.Parse(dateFor);
        }

        public Veriship() { }
    }
}
