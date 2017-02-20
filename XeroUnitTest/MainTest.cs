using XeroTechnicalTest;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Globalization;
using Autofac;

namespace XeroTechnicalTest.Tests
{
    [TestClass()]
    public class MainTest
    {
        #region Property
        //auto fac container for creating the service
        private static IContainer _container { get; set; }
        #endregion

        #region Constructor
        public MainTest()
        {
            //register service
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoiceBasicService>().As<IInvoiceBasicService>();
            builder.RegisterType<InvoiceAdvancedService>().As<IInvoiceAdvancedService>();
            _container = builder.Build();
        }
        #endregion

        #region Basic service Test
        [TestMethod()]
        public void CreateInvoiceWithOneItemTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var totalNumberOne = service.CreateInvoiceWithOneItem();
                Assert.AreEqual(totalNumberOne, 6.99m);
            }
        }

        [TestMethod()]
        public void CreateInvoiceWithMultipleItemsAndQuantitiesTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var totalNumberMultiple = service.CreateInvoiceWithMultipleItemsAndQuantities();
                Assert.AreEqual(totalNumberMultiple, 72.1m);
            }     
        }

        [TestMethod()]
        public void RemoveItemTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var totalNumberRemove = service.RemoveItem();
                Assert.AreEqual(totalNumberRemove, 43.96m);
            }
        }

        [TestMethod()]
        public void MergeInvoicesTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var totalNumberMerge = service.MergeInvoices();
                Assert.AreEqual(totalNumberMerge, 65.35m);
            }
        }

        [TestMethod()]
        public void CloneInvoiceTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var totalNumberClone = service.CloneInvoice();
                Assert.AreEqual(totalNumberClone, 25.8m);
            }
        }


        [TestMethod()]
        public void ToStringTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceBasicService>();
                var result = service.InvoiceToString();
                var dateNow = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Assert.AreEqual(result, string.Format("Invoice Number: 1000, InvoiceDate: {0}, LineItemCount: 1", dateNow));
            }
            
        }
        #endregion

        #region Advanced Service Test
        [TestMethod()]
        public void CreateInvoiceWithOneItemAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine = service.CreateInvoiceLine(1, 3, "Peach", 4.67m);
                var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine }, "1235", DateTime.Now);
                var totalNumberOne = invoice.GetTotal();
                Assert.AreEqual(totalNumberOne, 14.01m);
            }
        }

        [TestMethod()]
        public void CreateInvoiceWithMultipleItemsAndQuantitiesAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);

                var totalNumberOne = invoice.GetTotal();
                Assert.AreEqual(totalNumberOne, 18.93m);
            }
        }

        [TestMethod()]
        public void RemoveItemAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);
                service.RemoveLine(invoice,1);
                var totalNumberOne = invoice.GetTotal();
                Assert.AreEqual(totalNumberOne, 4.92m);
            }
        }

        [TestMethod()]
        public void MergeInvoicesAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                var invoicePeach = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);


                var invoiceLine3 = service.CreateInvoiceLine(1, 5, "Big Mango", 14.7m);
                var invoiceLine4 = service.CreateInvoiceLine(2, 7, "Small Mango", 11.3m);

                var invoiceMango = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine3, invoiceLine4 }, "1236", DateTime.Now);


                service.MergeInvoice(invoicePeach, invoiceMango);
                var totalNumberOne = invoicePeach.GetTotal();
                Assert.AreEqual(totalNumberOne, 171.53m);
            }
        }

        [TestMethod()]
        public void CloneInvoiceAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);
              
                var totalNumberOne = service.CloneInvoice(invoice).GetTotal();
                Assert.AreEqual(totalNumberOne, 18.93m);
            }
        }


        [TestMethod()]
        public void ToStringAdvancedTest()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                var service = scope.Resolve<IInvoiceAdvancedService>();
                var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);
                var result = service.invoiceToString(invoice);
                var dateNow = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                Assert.AreEqual(result, string.Format("Invoice Number: 1236, InvoiceDate: {0}, LineItemCount: 2", dateNow));
            }

        }
        #endregion

        #region Negative test

        [TestMethod()]
        public void DuplicateLineId()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                try {
                    var service = scope.Resolve<IInvoiceAdvancedService>();

                    //same line id twice
                    var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                    var invoiceLine2 = service.CreateInvoiceLine(1, 4, "Small Peach", 1.23m);

                    var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);

                    var totalNumberOne = invoice.GetTotal();
                }
                catch (XeroException ex)
                {
                    Assert.AreEqual(ex.Message, "Invoice Line Id 1 already existed");
                }
                catch (Exception)
                {
                    // not the right kind of exception
                    Assert.Fail();
                }
                
               
            }
        }

        [TestMethod()]
        public void RemoveNonExistingLineId()
        {
            using (var scope = _container.BeginLifetimeScope())
            {
                try
                {
                    var service = scope.Resolve<IInvoiceAdvancedService>();

                    //same line id twice
                    var invoiceLine1 = service.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
                    var invoiceLine2 = service.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

                    var invoice = service.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);

                    //line 3 does not exist
                    service.RemoveLine(invoice, 3);
                    var totalNumberOne = invoice.GetTotal();
                }
                catch (XeroException ex)
                {
                    Assert.AreEqual(ex.Message, "Can not find invoice with id 3");
                }
                catch (Exception)
                {
                    // not the right kind of exception
                    Assert.Fail();
                }


            }
        }
        #endregion
    }
}

