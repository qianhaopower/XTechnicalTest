namespace XeroTechnicalTest
{
    public class InvoiceLine
    {
        public int InvoiceLineId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }


        // public double Cost { get; set; }

        /// <summary>
        /// For money, always decimal. It's why it was created.
        /// http://stackoverflow.com/questions/1165761/decimal-vs-double-which-one-should-i-use-and-when
        /// </summary>
        public decimal Cost { get; set; }
    }
}