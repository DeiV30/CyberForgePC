﻿namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Helpers.Common;
    using cyberforgepc.Models.Order;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrder
    {
        Task<List<OrderResponse>> GetAll();
        Task<OrderResponse> GetById(string id);
        Task<bool> Create(OrderRequest request);
        Task<bool> State(string id, string state);
    }
}