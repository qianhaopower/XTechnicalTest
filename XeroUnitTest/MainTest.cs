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
        //auto fac container for creating the service
        private static IContainer _container { get; set; }

        

        public MainTest()
        {
            //register service
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoiceBasicService>().As<IInvoiceBasicService>();
            _container = builder.Build();
        }

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
    }
}

