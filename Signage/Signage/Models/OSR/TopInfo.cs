using Signage2.Models.OSR;
namespace Signage.Models.OSR

{
    public class TopInfo
    {
        public List<MTDModel> osrList;
        public List<YTDModel> ytdList;
        public StatsModel todayModel;
        public StatsModel mtdModel;

        public TopInfo()
        {
            osrList = new List<MTDModel>();
            ytdList = new List<YTDModel>();
            todayModel = new StatsModel();
            mtdModel = new StatsModel();
        }

    }
}
