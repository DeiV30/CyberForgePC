namespace  cyberforgepc.Domain.UnitOfWork
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Database.Context;
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.Repository;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        #region "IUnitOfWork Properties"

        public IRepository<Products> Products { get; set; }
        public IRepository<Users> Users { get; set; }
        public IRepository<Categories> Categories { get; set; }
        public IRepository<Coupons> Coupons { get; set; }
        public IRepository<WishLists> WishLists { get; set; }
        public IRepository<Orders> Orders { get; set; }

        #endregion

        protected CyberforgepcContext context;

        private readonly ILogger logger;

        public UnitOfWork(CyberforgepcContext context, ILogger<Startup> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<bool> Save()
        {
            bool returnValue = true;
            var dbContextTransaction = context.Database.BeginTransaction();

            try
            {
                await context.SaveChangesAsync();
                dbContextTransaction.Commit();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.InnerException.ToString());

                returnValue = false;
                dbContextTransaction.Rollback();
            }

            return returnValue;
        }

        #region IDisposable Support

        private bool isDisposing = false;

        public void Dispose()
        {
            if (!isDisposing)
            {
                context.Dispose();
                isDisposing = true;
            }
        }

        #endregion
    }
}
