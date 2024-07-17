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
                SubTotal = x.SubTotal,
                Total = x.Total,
                Created = x.Created,
            }).ToList();
        }

        public async Task<List<OrderResponse>> GetByIdList(string id)
        {
            var orders = await unitOfWork.Order.GetWhere(x => x.UserId.Equals(id), y => y.OrderItems, w => w.Coupon);

            if (orders == null)
                throw new MessageException("No se han encontrado resultados.");

            var orderResponses = new List<OrderResponse>();

            orders.ToList().ForEach(order =>
            {
                orderResponses.Add(new OrderResponse
                {
                    Id = order.Id,
                    Coupon = new CouponResponse()
                    {
                        Discount = order.Coupon?.Discount ?? 0,
                    },
                    Total = order.Total,
                    SubTotal = order.SubTotal,
                });
            });

            return orderResponses;
        }

        public async Task<List<OrderItemResponse>> GetByIdOrderList(string id)
        {
            var orders = await unitOfWork.OrderItem.GetWhere(x => x.OrderId.Equals(id), y => y.Product.Category);

            if (orders == null)
                throw new MessageException("No se han encontrado resultados.");

           
            var response = new List<OrderItemResponse>();

            orders.ToList().ForEach(order =>
            {
                response.Add(new OrderItemResponse
                {
                    Quantity = order.Quantity,
                    Price = order.Price,
                    ProductName = order.Product.Name,
                    ProductCategory = order.Product.Category.Name,
                });
               
            });

            return response;
        }

        public async Task<bool> Create(OrderRequest request)
        {
            var orderCreate = new Order
            {
                Id = Guid.NewGuid().ToString(),
                UserId = request.UserId,
                CouponId = (String.IsNullOrEmpty(request.CouponId)) ? null : request.CouponId,
                SubTotal = request.SubTotal,
                Total = request.Total,
                State = "Pendiente",
                Created = DateTime.Now
            };

            var products = new List<OrderItem>();
            request.OrderItems.ToList().ForEach(p =>
            {
                products.Add(new OrderItem
                {
                    Id = Guid.NewGuid().ToString(),
                    OrderId = orderCreate.Id,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    Price = p.Price,
                    Created = DateTime.Now
                });
            });

            unitOfWork.Order.Add(orderCreate);
            unitOfWork.OrderItem.Add(products);

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
