namespace Signage.Models.CSR
{
    public class TopInfo
    {
        public List<CSRModel> csrList;
        public List<CSRModel> quote;
        public StatsModel todayModel;
        public StatsModel mtdModel;

        public TopInfo()
        {
            csrList = new List<CSRModel>();
            quote = new List<CSRModel>();
            todayModel = new StatsModel();
            mtdModel = new StatsModel();
        }

    }
}
