using System;
using System.Collections.Generic;

namespace XeroTechnicalTest
{
    /// <summary>
    /// Exception for business logic compared to exception from .NET
    /// </summary>
    public class XeroException : Exception
    {
        public XeroException(string messge):base (messge)
        {
          
        }
      
    }
}