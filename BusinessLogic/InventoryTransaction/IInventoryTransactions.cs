namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IInventoryTransactions
    {
        Task<List<InventoryTransactionResponse>> GetById(string id);
    }
}
