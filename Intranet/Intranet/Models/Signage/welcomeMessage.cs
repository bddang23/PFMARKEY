namespace Intranet.Models.Signage
{
    public class welcomeMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public welcomeMessage(string id, string message, string startTime, string endTime)
        {
            Id = id;
            Message = message;
            StartTime = DateTime.Parse(startTime);
            EndTime = DateTime.Parse(endTime);
        }
    }
}
