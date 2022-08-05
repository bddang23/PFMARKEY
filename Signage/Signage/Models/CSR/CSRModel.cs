namespace Signage.Models.CSR
{
    public class CSRModel
    {
        public String name;
        public int order;
        public int orderLine;
        public int quotes;
        public int quotesLine;
        public int orderValues;
        public int quoteValues;

        public CSRModel(String n, string ord, string ordLine, string Quotes, string qLine, string orderVal, string quoteValues)
        {
            name = n;
            order = Convert.ToInt32(ord);
            orderLine = Convert.ToInt32(ordLine);
            quotes = Convert.ToInt32(Quotes);
            quotesLine = Convert.ToInt32(qLine);
            orderValues = Convert.ToInt32(orderVal);
            this.quoteValues = Convert.ToInt32(quoteValues);
        }
    }
}
