using System;

namespace XeroTechnicalTest
{
    /// <summary>
    /// base class for all services. i.e. Invoice service, receipt service...
    /// </summary>
    public abstract class BaseService : IBaseService
    {
        public virtual void Dispose()
        {
            //dispose any unmanaged resource here
        }
    }
}