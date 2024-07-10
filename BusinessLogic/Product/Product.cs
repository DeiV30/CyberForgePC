namespace cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using cyberforgepc.Models.Product;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Product : IProduct
    {
        private readonly IUnitOfWork unitOfWork;

        public Product(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<ProductResponse>> GetAll()
        {
            var product = await unitOfWork.Products.GetAll(x => x.Category);

            var productResponse = new List<ProductResponse>();

            product.ToList().ForEach(p =>
            {
                productResponse.Add(new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Category = new CategoryResponse
                    {
                        Id = p.Category.Id,
                        Name = p.Category.Name
                    },
                    Image = p.Image,
                    DeleteKey = p.DeleteKey,
                    Description = p.Description,
                    Created = p.Created,
                    Updated = p.Updated
                });
            });

            return productResponse;
        }

        public async Task<ProductResponse> GetById(string id)
        {
            var product = await unitOfWork.Products.FindWhere(p => p.Id.Equals(id), c => c.Category);

            if (product == null)
                throw new ProductNotFoundException("Product not found in the database.");

            var productResponse = new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Category = new CategoryResponse
                {
                    Id = product.Category.Id,
                    Name = product.Category.Name
                },
                Image  = product.Image,
                Price = product.Price,  
                Created = product.Created,
                Updated = product.Updated
            };

            return productResponse;
        }

        public async Task<bool> Create(ProductRequest requests)
        {
            var list = new Products()
            {

                Id = Guid.NewGuid().ToString(),
                Name = requests.Name,
                Description = requests.Description,
                CategoryId = requests.CategoryId,
                Price = requests.Price,
                Image = requests.Image,
                Stock = requests.Stock,
                Created = DateTime.Now
            };

            unitOfWork.Products.Add(list);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Update(string id, ProductRequest request)
        {
            var productToUpdate = await unitOfWork.Products.FindWhere(p => p.Id.Equals(id));

            if (productToUpdate == null)
                throw new ProductNotFoundException("Product not found in the database.");

            productToUpdate.Name = request.Name;
            productToUpdate.Description = request.Description;
            productToUpdate.CategoryId = request.CategoryId;
            productToUpdate.Price = request.Price;
            productToUpdate.Image = request.Image;
            productToUpdate.Stock = request.Stock;
            productToUpdate.Updated = DateTime.Now;

            unitOfWork.Products.Update(productToUpdate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Delete(string id)
        {
            var product = await unitOfWork.Products.FindWhere(p => p.Id.Equals(id));

            if (product == null)
                throw new ProductNotFoundException("Product not found in the database.");

            product.Id = Guid.NewGuid().ToString();
            product.Updated = DateTime.Now;

            unitOfWork.Products.Update(product);
            await unitOfWork.Save();

            return true;
        }
    }
}
