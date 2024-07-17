namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Product;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProducts
    {
        Task<List<ProductResponse>> GetAll();
        Task<List<ProductResponse>> GetAllPublic();
        Task<ProductResponse> GetById(string id);
        Task<bool> Create(ProductRequest request);
        Task<bool> Update(string id, ProductRequest request);
        Task<bool> UpdateStock(string id, ProductUpdateStockRequest request);
        Task<bool> Delete(string id);
    }
}
