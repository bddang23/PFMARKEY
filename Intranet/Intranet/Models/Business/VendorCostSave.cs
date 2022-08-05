namespace Intranet.Models.Business
{
    public class VendorCostSave
    {
        public List<int> Id { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }  
        public Dictionary<DateTime, decimal> VendorCost { get; set; }


        public VendorCostSave(string vendorId, string vendorName)
        {
            Id = new List<int>();
            VendorId = int.Parse(vendorId);
            VendorName = vendorName;
            VendorCost = new Dictionary<DateTime, decimal>();
        }

        public override bool Equals(object? obj)
        {
            return obj is VendorCostSave save &&
                   VendorId == save.VendorId;
        }
    }
}
