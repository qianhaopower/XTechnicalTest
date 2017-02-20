/*
    Welcome to the Xero technical excercise!
    ---------------------------------------------------------------------------------
    The test consists of a small invoice application that has a number of issues.

    Your job is to fix them and make sure you can perform the functions in each method below.

    Note your first job is to get the solution compiling! 
	
    Rules
    ---------------------------------------------------------------------------------
    * The entire solution must be written in C# (any version)
    * You can modify any of the code in this solution, split out classes, add projects etc
    * You can modify Invoice and InvoiceLine, rename and add methods, change property types (hint) 
    * Feel free to use any libraries or frameworks you like as long as they are .net based
    * Feel free to write tests (hint) 
    * Show off your skills! 

    Good luck :) 

    When you have finished the solution please zip it up and email it back to the recruiter or developer who sent it to you
*/

using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace XeroTechnicalTest
{
    public class Program
    {
        //auto fac container for creating the service
        private static IContainer _container { get; set; }

        static void Main(string[] args)
        {
           
            try
            {
                Trace.TraceInformation(string.Format("Application starts at {0}",DateTime.Now.ToString()));
                Setup();

                using (var scope = _container.BeginLifetimeScope())
                {
                    Console.WriteLine("Welcome to Xero Tech Test!");
                    RunBasicCases(scope);
                    RunAdvancedCases(scope);
                    Console.WriteLine("Press Entert to stop");
                    Console.ReadLine();
                }
            }
            catch(XeroException ex)//catch all business logic related error
            {
                Trace.TraceError(string.Format("Error found in business logic. The message is: {0}", ex.Message));
                Console.WriteLine("Error found in business logic, please see log file.");
            }
            catch (Exception ex)//catch all other errors. Ideally should catch each type of error. i.e. connection etc.
            {
                Trace.TraceError(string.Format("Error found. The message is: {0}", ex.Message));
                Console.WriteLine("Error found , please see log file.");
            }
        }

        /// <summary>
        /// Original cases
        /// </summary>
        /// <param name="scope"></param>
        static void RunBasicCases(ILifetimeScope scope)
        {
            var basicService = scope.Resolve<IInvoiceBasicService>();

            Console.WriteLine("Creating invoice with one item...");
            var totalNumberOne = basicService.CreateInvoiceWithOneItem();
            Console.WriteLine(string.Format("Finish creating invoice with one item. The total value is {0}.", totalNumberOne));

            Console.WriteLine("Creating invoice with multiple items and quantities...");
            var totalNumberMultiple = basicService.CreateInvoiceWithMultipleItemsAndQuantities();
            Console.WriteLine(string.Format("Finish creating invoice with multiple items. The total value is {0}.", totalNumberMultiple));

            Console.WriteLine("Removing items...");
            var totalNumberRemove = basicService.RemoveItem();
            Console.WriteLine(string.Format("Finish removing invoice. The total value is {0}.", totalNumberRemove));

            Console.WriteLine("Merging invoices...");
            var totalNumberMerge = basicService.MergeInvoices();
            Console.WriteLine(string.Format("Finish merging invoices. The total value is {0}.", totalNumberMerge));

            Console.WriteLine("Cloning invoices...");
            var totalNumberClone = basicService.CloneInvoice();
            Console.WriteLine(string.Format("Finish cloning invoice. The total value is {0}.", totalNumberClone));

            Console.WriteLine("Printing invoices...");
            var resultString = basicService.InvoiceToString();
            Console.WriteLine(string.Format("Finish printing invoice. {0}.", resultString));
        }

        /// <summary>
        /// Advanced service can take invoice and invoiceline as parameter.
        /// </summary>
        /// <param name="scope"></param>
        static void RunAdvancedCases(ILifetimeScope scope)
        {
            var advancedService = scope.Resolve<IInvoiceAdvancedService>();

            Console.WriteLine("(Advanced) Creating invoice with one item...");
            var invoiceLine = advancedService.CreateInvoiceLine(1, 3, "Peach", 4.67m);
            var invoice = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLine }, "1235", DateTime.Now);
            var totalNumberOne = invoice.GetTotal();
            Console.WriteLine(string.Format("Finish creating invoice with one item. The total value is {0}.", totalNumberOne));


            Console.WriteLine("(Advanced) Creating invoice with multiple items and quantities...");
            var invoiceLine1 = advancedService.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
            var invoiceLine2 = advancedService.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);
            var invoiceMultiple = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLine1, invoiceLine2 }, "1236", DateTime.Now);
            var totalNumberMultiple = invoiceMultiple.GetTotal();
            Console.WriteLine(string.Format("Finish creating invoice with multiple items. The total value is {0}.", totalNumberMultiple));


            Console.WriteLine("(Advanced) Removing items...");
            var invoiceLine1ForRemove = advancedService.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
            var invoiceLine2ForRemove = advancedService.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);
            var invoiceForRemove = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLine1ForRemove, invoiceLine2ForRemove }, "1236", DateTime.Now);
            advancedService.RemoveLine(invoiceForRemove, 1);
            var totalNumberRemove = invoiceForRemove.GetTotal();
            Console.WriteLine(string.Format("Finish removing invoice. The total value is {0}.", totalNumberRemove));

            Console.WriteLine("(Advanced) Merging invoices...");
            var invoiceLinePeach1 = advancedService.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
            var invoiceLinePeach2 = advancedService.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);
            var invoicePeach = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLinePeach1, invoiceLinePeach2 }, "1236", DateTime.Now);
            var invoiceLineMango1 = advancedService.CreateInvoiceLine(1, 5, "Big Mango", 14.7m);
            var invoiceLineMango2 = advancedService.CreateInvoiceLine(2, 7, "Small Mango", 11.3m);
            var invoiceMango = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLineMango1, invoiceLineMango2 }, "1236", DateTime.Now);
            advancedService.MergeInvoice(invoicePeach, invoiceMango);
            var totalNumberMerge = invoicePeach.GetTotal();
            Console.WriteLine(string.Format("Finish merging invoices. The total value is {0}.", totalNumberMerge));


            Console.WriteLine("(Advanced) Cloning invoices...");
            var invoiceLine1Clone = advancedService.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
            var invoiceLine2Clone = advancedService.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);
            var invoiceClone = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLine1Clone, invoiceLine2Clone }, "1236", DateTime.Now);
            var totalNumberClone = advancedService.CloneInvoice(invoiceClone).GetTotal();
            
            Console.WriteLine(string.Format("Finish cloning invoice. The total value is {0}.", totalNumberClone));

            Console.WriteLine("(Advanced) Printing invoices...");
            var invoiceLine1Print = advancedService.CreateInvoiceLine(1, 3, "Big Peach", 4.67m);
            var invoiceLine2Print = advancedService.CreateInvoiceLine(2, 4, "Small Peach", 1.23m);

            var invoicePrint = advancedService.CreateInvoice(new List<InvoiceLine>() { invoiceLine1Print, invoiceLine2Print }, "1236", DateTime.Now);
            var result = advancedService.invoiceToString(invoicePrint);
            Console.WriteLine(string.Format("Finish printing invoice. {0}.", result));
        }

        //set up code for the application
        static void Setup()
        {
            //register service
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoiceBasicService>().As<IInvoiceBasicService>();
            builder.RegisterType<InvoiceAdvancedService>().As<IInvoiceAdvancedService>();
            _container = builder.Build();
        }

     
    }
}