namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Models.Coupon;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoupons
    {
        Task<List<CouponResponse>> GetAll();
        Task<CouponResponse> GetById(string id);
        Task<CouponResponse> GetByIdAdmin(string id);
        Task<bool> Create(CouponRequest request);
        Task<bool> Update(string id, CouponUpdateRequest request);
    }
}
