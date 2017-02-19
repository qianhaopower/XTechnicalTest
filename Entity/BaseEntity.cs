using System;

namespace XeroTechnicalTest
{
    public class BaseEntity
    {
        public DateTime CreateDateTime { get; set; }

        public BaseEntity()
        {
            this.CreateDateTime = System.DateTime.Now;
        }
    }
}