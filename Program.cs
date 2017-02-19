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
            Console.WriteLine("Welcome to Xero Tech Test!");

            try
            {

                Trace.TraceInformation(string.Format("Application starts at {0}",DateTime.Now.ToString()));
                Setup();

                using (var scope = _container.BeginLifetimeScope())
                {
                    var service = scope.Resolve<IInvoiceService>();

                    Console.WriteLine("Creating invoice with one item...");
                    var totalNumberOne = service.CreateInvoiceWithOneItem();
                    Console.WriteLine(string.Format("Finish creating invoice with one item. The total value is {0}.",totalNumberOne));

                    Console.WriteLine("Creating invoice with multiple items and quantities...");
                    var totalNumberMultiple = service.CreateInvoiceWithMultipleItemsAndQuantities();
                    Console.WriteLine(string.Format("Finish creating invoice with multiple items. The total value is {0}.", totalNumberMultiple));

                    Console.WriteLine("Removing items...");
                    var totalNumberRemove = service.RemoveItem();
                    Console.WriteLine(string.Format("Finish removing invoice. The total value is {0}.", totalNumberRemove));

                    Console.WriteLine("Merging invoices...");
                    var totalNumberMerge = service.MergeInvoices();
                    Console.WriteLine(string.Format("Finish merging invoices. The total value is {0}.", totalNumberMerge));

                    Console.WriteLine("Cloning invoices...");
                    var totalNumberClone = service.CloneInvoice();
                    Console.WriteLine(string.Format("Finish cloning invoice. The total value is {0}.", totalNumberClone));

                    Console.WriteLine("Printing invoices...");
                    var resultString = service.InvoiceToString();
                    Console.WriteLine(string.Format("Finish printing invoice. {0}.", resultString));

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

        //set up code for the application
        static void Setup()
        {
            //register service
            var builder = new ContainerBuilder();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>();
            _container = builder.Build();
        }

     
    }
}