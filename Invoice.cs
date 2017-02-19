using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace XeroTechnicalTest
{
    public class Invoice
    {
        #region Private variable
        private int _invoiceNumber;
        private DateTime _invoiceDate;
        private List<InvoiceLine> _lineItems;
        #endregion

        #region Public property
        public int InvoiceNumber
        {
            get { return _invoiceNumber; }
            set { _invoiceNumber = value; }
        }

        public DateTime InvoiceDate
        {
            get { return _invoiceDate; }
            set { _invoiceDate = value; }
        }
        public List<InvoiceLine> LineItems
        {
            get {
                if(_lineItems == null)
                {
                    _lineItems = new List<InvoiceLine>();
                }
                return _lineItems;
            }
            set { _lineItems = value; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// construstor
        /// </summary>
        public Invoice()
        {

        }
        #endregion

        #region Public methods
        /// <summary>
        /// Add invoice line to the invoice
        /// </summary>
        /// <param name="invoiceLine">The invoiceline to add</param>
        public void AddInvoiceLine(InvoiceLine invoiceLine)
        {
            if (LineItems.Any(p => p.InvoiceLineId == invoiceLine.InvoiceLineId))
            {
                throw new XeroException(string.Format("Invoice Line Id {0} already existed", invoiceLine.InvoiceLineId));
            }

            LineItems.Add(invoiceLine);
        }

        /// <summary>
        /// Remove the invoice line from the invoice by id
        /// </summary>
        /// <param name="someId">the invoice line id to remove</param>
        public void RemoveInvoiceLine(int someId)//variable name shoule be lowercase leading camel case
        {
            if (LineItems.Any(p => p.InvoiceLineId == someId))
            {
                LineItems.RemoveAll(p => p.InvoiceLineId == someId);
            }
            else
            {
                throw new XeroException(string.Format("Can not find invoice with id {0}", someId));
            }
        }

        /// <summary>
        /// GetTotal should return the sum of (Cost * Quantity) for each line item
        /// </summary>
        public decimal GetTotal()
        {
            return LineItems.Sum(p => p.Cost * p.Quantity);
        }

        /// <summary>
        /// MergeInvoices appends the items from the sourceInvoice to the current invoice
        /// </summary>
        /// <param name="sourceInvoice">Invoice to merge from</param>
        public void MergeInvoices(Invoice sourceInvoice)
        {
            //we assume the merged invoice line will be given a new id
            int maxIdLocal = 0;
            if(LineItems.Any())
            {
                maxIdLocal = LineItems.Max(p => p.InvoiceLineId);
            }

            foreach (var invoice in sourceInvoice.LineItems)
            {
                this.LineItems.Add(new InvoiceLine() {
                    InvoiceLineId = maxIdLocal + 1,
                    Description = invoice.Description,
                    Quantity = invoice.Quantity,
                    Cost = invoice.Cost
                });
                maxIdLocal ++;
            }

        }

        /// <summary>
        /// Creates a deep clone of the current invoice (all fields and properties)
        /// </summary>
        public Invoice Clone()
        {
            //serialize current invoice into string, then deserialized.
            var serialized = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<Invoice>(serialized);
        }

        /// <summary>
        /// Outputs string containing the following (replace [] with actual values):
        /// Invoice Number: [InvoiceNumber], InvoiceDate: [DD/MM/YYYY], LineItemCount: [Number of items in LineItems] 
        /// </summary>
        public override string ToString()
        {
            return string.Format("Invoice Number: {0}, InvoiceDate: {1}, LineItemCount: {2}",
                this.InvoiceNumber,
                this.InvoiceDate.ToString("dd/MM/yyyy",CultureInfo.InvariantCulture),
            this.LineItems.Count());
        }

        #endregion
    }
}