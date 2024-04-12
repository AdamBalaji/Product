using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.DTO.Category;
using Product.Model;
using Product.Repository.IRepository;

namespace Product.Repository
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext DbContext) : base(DbContext) 
        {
            _dbContext = DbContext;
        }
        

        public async Task Update(Category entity)
        {
            _dbContext.Categories.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
