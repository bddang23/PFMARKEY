namespace Signage.Models.Warehouse
{
    public class TopInfo
    {
        public WHStats ytd;
        public WHStats mtd;
        public WHStats today;
        public List<CostingInfo> info;

        public TopInfo()
        {
            ytd = new WHStats();
            mtd = new WHStats();
            today = new WHStats();
            info = new List<CostingInfo>();
        }

    }
}
