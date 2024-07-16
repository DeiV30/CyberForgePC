namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Coupon;
    using cyberforgepc.Models.Order;
    using cyberforgepc.Models.Product;
    using cyberforgepc.Models.User;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Orders : IOrders
    {
        private readonly IUnitOfWork unitOfWork;

        public Orders(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<OrderResponse>> GetAll()
        {
            var order = await unitOfWork.Order.GetAll(x => x.OrderItems, y => y.User, z => z.Coupon);

            return order.Select(x => new OrderResponse
            {
                User = new UserResponse
                {
                    Id = x.User.Id,
                    Name = x.User.Name,
                    Email = x.User.Email,
                },
                Coupon = new CouponResponse
                {
                    Id = x.Coupon.Id,
                    Code = x.Coupon.Code,
                    Discount = x.Coupon.Discount,
                },
                Products = new List<ProductResponse>
                {
                    //new ProductResponse
                    //{
                    //    Id = x.Product.Id,
                    //    Name = x.Product.Name,
                    //    Description = x.Product.Description,
                    //    Price = x.Product.Price,
                    //    Image = x.Product.Image,
                    //}
                },
                SubTotal = x.SubTotal,
                Total = x.Total,
                Created = x.Created,
            }).ToList();
        }

        public async Task<OrderResponse> GetById(string id)
        {
            var order = await unitOfWork.Order.FindWhere(x => x.Id.Equals(id), y => y.OrderItems, z => z.User, w => w.Coupon);

            if (order == null)
                throw new MessageException("No se han encontrado resultados.");

            var response = new OrderResponse
            {
                User = new UserResponse
                {
                    Id = order.User.Id,
                    Name = order.User.Name,
                    Email = order.User.Email,
                },
                Coupon = new CouponResponse
                {
                    Id = order.Coupon.Id,
                    Code = order.Coupon.Code,
                    Discount = order.Coupon.Discount,
                },
                Products = new List<ProductResponse>
                {
                    //new ProductResponse
                    //{
                    //    Id = order.Product.Id,
                    //    Name = order.Product.Name,
                    //    Description = order.Product.Description,
                    //    Price = order.Product.Price,
                    //    Image = order.Product.Image,
                    //}
                },
                SubTotal = order.SubTotal,
                Total = order.Total,
                Created = order.Created,
            };

            return response;
        }

        public async Task<bool> Create(OrderRequest request)
        {
            var orderCreate = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                CouponId = request.CouponId,
                SubTotal = request.SubTotal,
                Total = request.Total,
                Created = DateTime.Now,
            };

            unitOfWork.Order.Add(orderCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> State(string id, string state)
        {
            var order = await unitOfWork.Order.FindWhere(x => x.Id.Equals(id));

            order.Updated = System.DateTime.UtcNow;
            order.State = state;

            unitOfWork.Order.Update(order);
            await unitOfWork.Save();

            return true;
        }

    }
}
