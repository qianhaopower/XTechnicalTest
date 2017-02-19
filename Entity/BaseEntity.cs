using System;

namespace XeroTechnicalTest
{
    /// <summary>
    /// Base class for all entities. Common properties go here.
    /// </summary>
    public class BaseEntity
    {
        public DateTime CreateDateTime { get; set; }

        public BaseEntity()
        {
            this.CreateDateTime = System.DateTime.Now;
        }
    }
}