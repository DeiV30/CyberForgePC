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

    public class InventoryTransactions : IInventoryTransactions
    {
        private readonly IUnitOfWork unitOfWork;

        public InventoryTransactions(IUnitOfWork unitOfWork) => this.unitOfWork = unitOfWork;

        public async Task<List<CategoryResponse>> GetAll()
        {
            var category = await unitOfWork.Category.GetAll();

            var response = new List<CategoryResponse>();

            category.ToList().ForEach(c =>
            {
                response.Add(new CategoryResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                });
            });

            return response;
        }

        public async Task<CategoryResponse> GetById(string id)
        {
            var category = await unitOfWork.Category.FindWhere(c => c.Id.Equals(id));

            if (category == null)
                throw new MessageException("No se han encontrado resultados.");

            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };

            return response;
        }

        public async Task<bool> Create(CategoryRequest request)
        {
            var categoryCreate = new Category
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                Description = request.Description,
                Created = DateTime.Now,
            };

            unitOfWork.Category.Add(categoryCreate);
            await unitOfWork.Save();

            return true;
        }

        public async Task<bool> Update(string id, CategoryUpdateRequest request)
        {
            var categoryUpdate = await unitOfWork.Category.FindWhere(l => l.Id.Equals(id));

            if (categoryUpdate == null)
                throw new MessageException("No se han encontrado resultados.");

            categoryUpdate.Name = request.Name;
            categoryUpdate.Description = request.Description;
            categoryUpdate.Updated = DateTime.Now;

            unitOfWork.Category.Update(categoryUpdate);
            await unitOfWork.Save();

            return true;
        }

    }
}
