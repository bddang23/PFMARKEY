using Signage.Models.AutoCribs;

namespace Signage.Models.AutoCrib
{
    public class TopInfo
    {
        public List<Specialist> autocribSpecialist;
        public List<InventoryStats> daysOfSupply;

        public TopInfo()
        {
            autocribSpecialist = new List<Specialist>();
            daysOfSupply = new List<InventoryStats>();
        }

    }
}
