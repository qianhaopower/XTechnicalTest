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
    public class InvoiceService : IInvoiceService
    {
        #region Constructor
        public InvoiceService()
        {

        }
        #endregion

        #region Public methods
        public decimal CreateInvoiceWithOneItem()
        {
            var invoice = new Invoice();

            int invoiceLineId = 0;
            Int32.TryParse("1", out invoiceLineId);

            if (invoiceLineId == 0)
            {
                throw new XeroException("InvoiceLine Id must be greater than zero");
            }

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = invoiceLineId,
                Cost = (decimal)6.99,
                Quantity = 1,
                Description = "Apple"
            });

            return invoice.GetTotal();
        }

        public decimal CreateInvoiceWithMultipleItemsAndQuantities()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = 10.21m,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = (decimal)5.21,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = (decimal)5.21,
                Quantity = 5,
                Description = "Pineapple"
            });
            return invoice.GetTotal();
        }

        public decimal RemoveItem()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = (decimal)5.21,
                Quantity = 1,
                Description = "Orange"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = (decimal)10.99,
                Quantity = 4,
                Description = "Banana"
            });

            invoice.RemoveInvoiceLine(1);
            return invoice.GetTotal();
        }

        public decimal MergeInvoices()
        {
            var invoice1 = new Invoice();

            invoice1.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = (decimal)10.33,
                Quantity = 4,
                Description = "Banana"
            });

            var invoice2 = new Invoice();

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = (decimal)5.22,
                Quantity = 1,
                Description = "Orange"
            });

            invoice2.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 3,
                Cost = (decimal)6.27,
                Quantity = 3,
                Description = "Blueberries"
            });

            invoice1.MergeInvoices(invoice2);
            return invoice1.GetTotal();
        }

        public decimal CloneInvoice()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = (decimal)6.99,
                Quantity = 1,
                Description = "Apple"
            });

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 2,
                Cost = (decimal)6.27,
                Quantity = 3,
                Description = "Blueberries"
            });

            var clonedInvoice = invoice.Clone();
            return clonedInvoice.GetTotal();
        }

        public string InvoiceToString()
        {
            var invoice = new Invoice()
            {
                InvoiceDate = DateTime.Now,
                InvoiceNumber = 1000,
                LineItems = new List<InvoiceLine>()
                {
                    new InvoiceLine()
                    {
                        InvoiceLineId = 1,
                        Cost = (decimal)6.99,
                        Quantity = 1,
                        Description = "Apple"
                    }
                }
            };

            return invoice.ToString();
        }
        #endregion
    }
}
