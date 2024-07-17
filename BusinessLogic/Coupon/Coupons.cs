namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Coupon;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Coupons : ICoupons
    {
        private readonly IUnitOfWork unitOfWork;

        public Coupons(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<CouponResponse>> GetAll()
        {
            var coupon = await unitOfWork.Coupon.GetAll();

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

        public async Task<CouponResponse> GetByIdAdmin(string id)
        {
            var coupon = await unitOfWork.Coupon.FindWhere(x => x.Id.Equals(id));

            if (coupon == null)
                throw new MessageException("No se han encontrado resultados.");

            var response = new CouponResponse
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Discount = coupon.Discount,
                ExpirationDate = coupon.ExpirationDate,
            };

            return response;
        }


        public async Task<CouponResponse> GetById(string id)
        {
            var coupon = await unitOfWork.Coupon.FindWhere(x => x.Code.Equals(id));

            if (coupon == null)
                throw new MessageException("No se han encontrado resultados.");

            DateTime dateTimeFromDateOnly = coupon.ExpirationDate.ToDateTime(new TimeOnly(0));

            if (dateTimeFromDateOnly < DateTime.Now)
                throw new MessageException("El cupón ha expirado.");

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
            var couponCreate = new Coupon
            {
                Id = Guid.NewGuid().ToString(),
                Code = request.Code,
                Discount = request.Discount,
                ExpirationDate = request.ExpirationDate,
                Created = DateTime.Now,
            };

            unitOfWork.Coupon.Add(couponCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Update(string id, CouponUpdateRequest request)
        {
            var couponUpdate = await unitOfWork.Coupon.FindWhere(x => x.Id.Equals(id));

            if (couponUpdate == null)
                throw new MessageException("No se han encontrado resultados.");

            couponUpdate.Code = request.Code;
            couponUpdate.Discount = request.Discount;
            couponUpdate.ExpirationDate = request.ExpirationDate;
            couponUpdate.Updated = DateTime.Now;

            unitOfWork.Coupon.Update(couponUpdate);
            await unitOfWork.Save();

            return true;
        }

    }
}
