using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroTechnicalTest
{
    /// <summary>
    /// Service that perform all invoices related calculation
    /// </summary>
    public class InvoiceAdvancedService : BaseService, IInvoiceAdvancedService
    {
        #region Constructor
        public InvoiceAdvancedService()
        {

        }
        #endregion
        #region Public methods
        public Invoice CloneInvoice(Invoice toClone)
        {
            return toClone.Clone();
        }

        public Invoice CreateInvoice(IEnumerable<InvoiceLine> lines,int invoiceNumber, DateTime invoiceDate)
        {
            return new Invoice() {
                InvoiceNumber = invoiceNumber,
                InvoiceDate = invoiceDate,
                LineItems = lines.ToList(),
            };

        }

        public InvoiceLine CreateInvoiceLine(int id, int quantity, string description, decimal cost)
        {
            return new InvoiceLine() {
               InvoiceLineId = id,
               Quantity = quantity,
               Description = description,
               Cost=  cost,
            };
        }

        public string invoiceToString(Invoice invoice)
        {
            return invoice.ToString();
        }

        public Invoice MergeInvoice(Invoice original, Invoice toMerge)
        {
            original.MergeInvoices(toMerge);
            return original;
        }

        public Invoice RemoveLine(Invoice invoice, int lineId)
        {
            invoice.RemoveInvoiceLine(lineId);
            return invoice;
        }
        #endregion


    }
}
