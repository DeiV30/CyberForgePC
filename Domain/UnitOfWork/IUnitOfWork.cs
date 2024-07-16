namespace  cyberforgepc.Domain.UnitOfWork
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.Repository;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> Product { get; set; }
        IRepository<User> User { get; set; }
        IRepository<Category> Category { get; set; }
        IRepository<Coupon> Coupon { get; set; }
        IRepository<WishList> WishList { get; set; }
        IRepository<Order> Order { get; set; }
        Task<bool> Save();
    }
}
