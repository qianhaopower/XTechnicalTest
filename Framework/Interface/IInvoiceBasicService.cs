﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XeroTechnicalTest
{
    /// <summary>
    /// Interface that all basic service should conform
    /// </summary>
    public interface IInvoiceBasicService : IBaseService
    {
         decimal CreateInvoiceWithOneItem();
         decimal CreateInvoiceWithMultipleItemsAndQuantities();
         decimal RemoveItem();
         decimal MergeInvoices();
         decimal CloneInvoice();
         string InvoiceToString();
    }
}
