namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.WishList;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWishList
    {
        Task<List<WishListResponse>> GetById(string id);
        Task<bool> Create(WishListRequest request);
        Task<bool> Delete(string id);
    }
}
