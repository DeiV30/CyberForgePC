namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Category;
    using cyberforgepc.Models.DashBoard;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILayout
    {
        Task<LayoutResponse> GetCountResume();
        Task<List<InventoryTransactionResponse>> GetAll();
    }
}
