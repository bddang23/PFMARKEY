namespace Signage.Models.Front_Lobby
{
    public class VendorCostSaving
    {
        public string VendorName { get; set; }
        public double CostSaved { get; set; }

        public VendorCostSaving(string vendorName, string costSaved)
        {
            VendorName = vendorName;
            CostSaved = Convert.ToDouble(costSaved);
        }
    }

}
