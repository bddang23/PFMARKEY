namespace Signage.Models.Front_Lobby
{
    public class TopInfo
    {
        public List<VendorCostSaving> LastYearList;
        public List<VendorCostSaving> YTDList;
        public List<string> welcomeMsg;
        public string totalLastYearCostSave;
        public string totalThisYearCostSave;

        public TopInfo()
        {
            LastYearList = new List<VendorCostSaving>();
            YTDList = new List<VendorCostSaving>();
            totalLastYearCostSave = "To Be Determined!";
            totalThisYearCostSave = "To Be Determined!";
            welcomeMsg = new List<string>();
            welcomeMsg.Add("Welcome to PF Markey!");
        }
    }
}
