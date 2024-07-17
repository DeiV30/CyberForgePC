namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class InventoryTransactions : IInventoryTransactions
    {
        private readonly IUnitOfWork unitOfWork;

        public InventoryTransactions(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<InventoryTransactionResponse>> GetById(string id)
        {
            var inventoryTransactionResponse = await unitOfWork.InventoryTransaction.GetWhere(c => c.ProductId.Equals(id), p => p.Product);

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
