using XeroTechnicalTest;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;

namespace XeroTechnicalTest.Tests
{
    [TestClass()]
    public class MainTest
    {

        [TestMethod()]
        public void CreateInvoiceWithOneItemTest()
        {
            var invoice = new Invoice();

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = 1,
                Cost = (decimal)6.99,
                Quantity = 1,
                Description = "Apple"
            });

            Assert.AreEqual(invoice.GetTotal(), 6.99m);
        }

        [TestMethod()]
        public void CreateInvoiceWithMultipleItemsAndQuantitiesTest()
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
            Assert.AreEqual(invoice.GetTotal(), 72.1m);

        }

        [TestMethod()]
        public void RemoveItemTest()
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
            Assert.AreEqual(invoice.GetTotal(), 43.96m);
        }

        [TestMethod()]
        public void MergeInvoicesTest()
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
            Assert.AreEqual(invoice1.GetTotal(), 65.35m);
        }

        [TestMethod()]
        public void CloneInvoiceTest()
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
            Assert.AreEqual(clonedInvoice.GetTotal(), 25.8m);
        }


        [TestMethod()]
        public void ToStringTest()
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
            var dateNow = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            Assert.AreEqual(invoice.ToString(), string.Format("Invoice Number: 1000, InvoiceDate: {0}, LineItemCount: 1", dateNow));
        }
    }
}

