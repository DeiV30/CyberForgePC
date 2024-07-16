namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using cyberforgepc.Models.Product;
    using cyberforgepc.Models.WishList;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class WishLists : IWishLists
    {
        private readonly IUnitOfWork unitOfWork;

        public WishLists(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<WishListResponse>> GetById(string id)
        {
            var wishList = await unitOfWork.WishList.GetWhere(x => x.UserId.Equals(id), y => y.Product.Category);

            if (wishList == null)
                throw new MessageException("No se han encontrado resultados.");

            var response = new List<WishListResponse>();

            foreach (var item in wishList)
            {
                response.Add(new WishListResponse
                {
                    Id = item.Id,
                    Product = new ProductResponse
                    {
                        Id = item.Product.Id,
                        Name = item.Product.Name,
                        Description = item.Product.Description,
                        Price = item.Product.Price,
                        Image = item.Product.Image,
                        Category = new CategoryResponse
                        {
                            Id = item.Product.Category.Id,
                            Name = item.Product.Category.Name,
                        }
                    }
                });
            }

            return response;
        }

        public async Task<bool> Create(WishListRequest request)
        {
            var wishLists = await unitOfWork.WishList.FindWhere(x => x.UserId.Equals(request.UserId) && x.ProductId.Equals(request.ProductId));

            if (wishLists != null)
                return false;

            var wishListCreate = new WishList
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = request.ProductId,
                UserId = request.UserId,
                Created = DateTime.Now,
            };

            unitOfWork.WishList.Add(wishListCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var wishLists = await unitOfWork.WishList.FindWhere(x => x.Id.Equals(id));

            unitOfWork.WishList.Remove(wishLists);
            await unitOfWork.Save();

            return true;
        }

    }
}
