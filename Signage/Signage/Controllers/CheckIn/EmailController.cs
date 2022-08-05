using System;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;


namespace Signage.Controllers
{
    public class EmailController : Controller
    {
        ExchangeService service = new ExchangeService(ExchangeVersion.Exchange2013);

        public EmailController()
        {
            service.Url = new Uri("https://mail.pfmarkey.com/ews/exchange.asmx");
            service.UseDefaultCredentials = false;

            service.Credentials = new WebCredentials("notifications", "2880Pfmi48603");
        }

        public void sendMessage(string email, string emps, string visitorName, string visitorComp)
        {
            string recipient;
            EmailMessage msg = new EmailMessage(service);
            string msgBody;
            


            msgBody =  emps + ",<br/><br/>You have a visitor. " + visitorName + " from " + visitorComp + " is waiting for you in the front lobby!<br/><br/>Regards,<br/>IT Teams.";

            msg.Subject = "A Visitor Is Here To See You!";
            msg.Body = msgBody;

         
            msg.ToRecipients.Add(email);

            if (msg.ToRecipients.Count > 0)
            {
                //Send the message and save a copy in the sent folder
                msg.SendAndSaveCopy();
            }
        }

    }
}