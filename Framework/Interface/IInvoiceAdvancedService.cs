using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroTechnicalTest
{
    /// <summary>
    /// Interface that all invoice service should conform
    /// </summary>
    public interface IInvoiceAdvancedService : IBaseService
    {

        InvoiceLine CreateInvoiceLine(int id, int quantity, string description, decimal cost);

        Invoice CreateInvoice(IEnumerable<InvoiceLine> lines, string invoiceNumber, DateTime invoiceDate);

        Invoice RemoveLine(Invoice invoice, int lineId);

        Invoice MergeInvoice(Invoice original, Invoice toMerge);

        Invoice CloneInvoice(Invoice toClone);

        string invoiceToString(Invoice invoice);

    }
}
