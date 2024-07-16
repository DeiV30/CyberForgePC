namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICategories
    {
        Task<List<CategoryResponse>> GetAll();
        Task<CategoryResponse> GetById(string id);
        Task<bool> Create(CategoryRequest request);
        Task<bool> Update(string id, CategoryUpdateRequest request);
    }
}
