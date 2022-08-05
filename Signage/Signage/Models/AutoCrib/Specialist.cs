namespace Signage.Models.AutoCrib
{
    public class Specialist
    {
        public string name;
        public int numOfAccount;
        public int itemCount;
        public double invValue;
        public double slow;
        public double dead;
        public double deadPercent;

        public Specialist(string name, string numOfAccount, string itemCount, string invValue, string slow, string dead, string deadPercent)
        {
            this.name = name;
            this.numOfAccount = int.Parse(numOfAccount);
            this.itemCount = int.Parse(itemCount);
            this.invValue = double.Parse(invValue);
            this.slow = double.Parse(slow);
            this.dead = double.Parse(dead);
            this.deadPercent = double.Parse(deadPercent);
        }
    }
}
