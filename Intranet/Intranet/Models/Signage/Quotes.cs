namespace Intranet.Models.Signage
{
    public class Quotes
    {
        public string Id;
        public string quote { get; set; }
        public string submissioner { get; set; }
        public int status;

        public Quotes()
        {
            Id = "-1";
            quote = "\"Hello, Please enter your quote here!\"";
            submissioner = "Your name here!";
            status = 0;
        }

        public Quotes(string Id, string quote, string submissioner)
        {
            this.Id = Id;
            this.quote = quote;
            this.submissioner = submissioner;
            status = 0;
        }
    }
}
