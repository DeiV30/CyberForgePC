namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Models.Order;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrders
    {
        Task<List<OrderResponse>> GetAll();
        Task<List<OrderResponse>> GetByIdList(string id);
        Task<List<OrderItemResponse>> GetByIdOrderList(string id);
        Task<bool> Create(OrderRequest request);
        Task<bool> State(string id, string state);
    }
}