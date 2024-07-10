namespace  cyberforgepc.Domain.UnitOfWork
{
    using cyberforgepc.BusinessLogic;
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.Repository;
    using System;
    using System.Threading.Tasks;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<Products> Products { get; set; }
        IRepository<Users> Users { get; set; }
        IRepository<Categories> Categories { get; set; }
        IRepository<Coupons> Coupons { get; set; }
        IRepository<WishLists> WishLists { get; set; }
        IRepository<Orders> Orders { get; set; }
        Task<bool> Save();
    }
}
