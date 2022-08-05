namespace Intranet.Models.Warehouse
{
    public class Shipments
    {
        public int id;
        public int Packages_Received;
        public int Packages_Shipped;
        public int Deliveries_Made;
        public int Mispacks;
        public DateTime DateTracked;

        public Shipments(string id, string packages_Received, string packages_Shipped, string deliveries_Made, string mispacks, string dateTracked)
        {
            this.id = int.Parse(id);
            Packages_Received = int.Parse(packages_Received);
            Packages_Shipped = int.Parse(packages_Shipped);
            Deliveries_Made = int.Parse(deliveries_Made);
            Mispacks = int.Parse(mispacks);
            DateTracked = DateTime.Parse(dateTracked);
        }

        public Shipments( string dateTracked)
        {
            this.id = -1;
            Packages_Received = 0;
            Packages_Shipped = 0;
            Deliveries_Made = 0;
            Mispacks = 0;
            DateTracked = DateTime.Parse(dateTracked);
        }

        public Shipments()
        {        }
    }
}
