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

using System;
using System.Collections.Generic;

namespace XeroTechnicalTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Xero Tech Test!");

            try
            {
                Console.WriteLine("Creating invoice with one item...");
                CreateInvoiceWithOneItem();

                Console.WriteLine("Creating invoice with multiple items and quantities...");
                CreateInvoiceWithMultipleItemsAndQuantities();

                Console.WriteLine("Removing items...");
                RemoveItem();

                Console.WriteLine("Merging invoices...");
                MergeInvoices();

                Console.WriteLine("Cloning invoices...");
                CloneInvoice();

                Console.WriteLine("Printing invoices...");
                InvoiceToString();

                Console.WriteLine("Press Entert to stop");
                Console.ReadLine();

            }
            catch(XeroException ex)
            {
                Console.WriteLine("Error happend, please see log file");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error happend, please see log file");
            }
        }

        private static void CreateInvoiceWithOneItem()
        {
            var invoice = new Invoice();

            int invoiceLineId = 0;
            Int32.TryParse("1", out invoiceLineId);

            if (invoiceLineId == 0)
            {
                throw new Exception("InvoiceLine Id must be greater than zero");
            }

            invoice.AddInvoiceLine(new InvoiceLine()
            {
                InvoiceLineId = invoiceLineId,
                Cost = (decimal)6.99,
                Quantity = 1,
                Description = "Apple"
            });

            Console.WriteLine(invoice.GetTotal());
        }

        private static void CreateInvoiceWithMultipleItemsAndQuantities()
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

            Console.WriteLine(invoice.GetTotal());
        }

        private static void RemoveItem()
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
            Console.WriteLine(invoice.GetTotal());
        }

        private static void MergeInvoices()
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
            Console.WriteLine(invoice1.GetTotal());
        }

        private static void CloneInvoice()
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
            Console.WriteLine(clonedInvoice.GetTotal());
        }

        private static void InvoiceToString()
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

            Console.WriteLine(invoice.ToString());
        }
    }
}