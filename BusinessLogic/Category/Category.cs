namespace  cyberforgepc.BusinessLogic
{
    using cyberforgepc.Database.Models;
    using cyberforgepc.Domain.UnitOfWork;
    using cyberforgepc.Helpers.Exceptions;
    using cyberforgepc.Models.Category;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Category : ICategory
    {
        private readonly IUnitOfWork unitOfWork;

        public Category(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<CategoryResponse>> GetAll()
        {
            var category = await unitOfWork.Categories.GetAll();

            var response = new List<CategoryResponse>();

            category.ToList().ForEach(c =>
            {
                response.Add(new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name
                });
            });

            return response;
        }

        public async Task<CategoryResponse> GetById(string id)
        {
            var category = await unitOfWork.Categories.FindWhere(c => c.Id.Equals(id));

            if (category == null)
                throw new CountryNotFoundException("Country not found in the database.");

            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
            };

            return response;
        }

        public async Task<bool> Create(CategoryRequest request)
        {
            var categoryCreate = new Categories
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Created = DateTime.Now,
            };

            unitOfWork.Categories.Add(categoryCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Update(string id, CategoryUpdateRequest request)
        {
            var categoryUpdate = await unitOfWork.Categories.FindWhere(l => l.Id.Equals(id));

            if (categoryUpdate == null)
                throw new LockerNotFoundException("Category not found in the database.");

            categoryUpdate.Name = request.Name;
            categoryUpdate.Description = request.Description;
            categoryUpdate.Updated = DateTime.Now;

            unitOfWork.Categories.Update(categoryUpdate);
            await unitOfWork.Save();

            return true;
        }

    }
}
