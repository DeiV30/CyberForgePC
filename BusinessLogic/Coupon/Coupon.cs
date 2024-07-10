namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using cyberforgepc.Models.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Coupon : ICoupon
    {
        private readonly IUnitOfWork unitOfWork;

        public Coupon(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<CouponResponse>> GetAll()
        {
            var coupon = await unitOfWork.Coupons.GetAll();

            var response = new List<CouponResponse>();

            coupon.ToList().ForEach(x =>
            {
                response.Add(new CouponResponse
                {
                    Id = x.Id,
                    Code = x.Code,
                    Discount = x.Discount,
                    ExpirationDate = x.ExpirationDate,
                });
            });

            return response;
        }

        public async Task<CouponResponse> GetById(string id)
        {
            var coupon = await unitOfWork.Coupons.FindWhere(x => x.Id.Equals(id));

            if (coupon == null)
                throw new CountryNotFoundException("Country not found in the database.");

            var response = new CouponResponse
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Discount = coupon.Discount,
                ExpirationDate = coupon.ExpirationDate,
            };

            return response;
        }

        public async Task<bool> Create(CouponRequest request)
        {
            var couponCreate = new Coupons
            {
                Id = Guid.NewGuid().ToString(),
                Code = request.Code,
                Discount = request.Discount,
                ExpirationDate = request.ExpirationDate,
                Created = DateTime.Now,
            };

            unitOfWork.Coupons.Add(couponCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Update(string id, CouponUpdateRequest request)
        {
            var couponUpdate = await unitOfWork.Coupons.FindWhere(x => x.Id.Equals(id));

            if (couponUpdate == null)
                throw new LockerNotFoundException("Coupon not found in the database.");

            couponUpdate.Code = request.Code;
            couponUpdate.Discount = request.Discount;
            couponUpdate.ExpirationDate = request.ExpirationDate;
            couponUpdate.Updated = DateTime.Now;

            unitOfWork.Coupons.Update(couponUpdate);
            await unitOfWork.Save();

            return true;
        }

    }
}
