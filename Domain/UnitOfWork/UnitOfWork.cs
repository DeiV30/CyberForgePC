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

        public IRepository<Product> Product { get; set; }
        public IRepository<User> User { get; set; }
        public IRepository<Category> Category { get; set; }
        public IRepository<Coupon> Coupon { get; set; }
        public IRepository<WishList> WishList { get; set; }
        public IRepository<Order> Order { get; set; }
        public IRepository<OrderItem> OrderItem { get; set; }
        public IRepository<InventoryTransaction> InventoryTransaction { get; set; }

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
