namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using cyberforgepc.Models.DashBoard;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Layout : ILayout
    {
        private readonly IUnitOfWork unitOfWork;

        public Layout(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<LayoutResponse> GetCountResume()
        {
            var userCount = await unitOfWork.User.CountWhere(x => x.Discriminator.Equals("Client"));
            var orderCount = await unitOfWork.Order.GetAll();

            var response = new LayoutResponse()
            {
                TotalUser = userCount.ToString(),
                TotalOrder = orderCount.Count().ToString()
            };

            return response;
        }

        public async Task<List<InventoryTransactionResponse>> GetAll()
        {
            var inventoryTransactionResponse = await unitOfWork.InventoryTransaction.GetAll(p=> p.Product);

            if (inventoryTransactionResponse == null)
                throw new MessageException("No se han encontrado resultados.");

            var response = inventoryTransactionResponse.Select(c => new InventoryTransactionResponse
            {
                Id = c.Id,
                ProductName = c.Product.Name,
                Quantity = c.Quantity,
                TransactionType = c.TransactionType,
                Created = c.Created
            }).ToList();

            return response;
        }

    }
}
