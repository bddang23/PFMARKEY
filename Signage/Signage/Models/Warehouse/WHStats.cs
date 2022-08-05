namespace Signage.Models.Warehouse
{
    public class WHStats
    {
        public int received;
        public int shipped;
        public int delivered;
        public int mispacks;

        public WHStats()
        {
            received = 0;
            shipped = 0;
            delivered = 0;
            mispacks = 0;
        }
    }
}
