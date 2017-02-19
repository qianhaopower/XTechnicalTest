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
    public interface IInvoiceService
    {


       
         decimal CreateInvoiceWithOneItem();
         decimal CreateInvoiceWithMultipleItemsAndQuantities();
         decimal RemoveItem();
         decimal MergeInvoices();
         decimal CloneInvoice();
         string InvoiceToString();
       
    }
}
