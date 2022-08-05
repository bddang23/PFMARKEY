using Intranet.Models.AutoCrib;

namespace Intranet.Controllers.AutoCrib
{
    public class AssignSpecialistPage
    {
       public List<Company> companies;
        public Specialist selSpecialist;
        public List<Specialist> specialists;
        public List<Company> assignedCompany;


        public AssignSpecialistPage()
        {
            companies = new List<Company>();
            assignedCompany = new List<Company>();
            specialists = new List<Specialist>();
            selSpecialist = new Specialist();
        }
       

    }
}
