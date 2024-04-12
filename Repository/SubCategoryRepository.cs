using Microsoft.EntityFrameworkCore;
using Product.Data;
using Product.DTO.Category;
using Product.DTO.SubCategory;
using Product.Model;
using Product.Repository.IRepository;

namespace Product.Repository
{
    public class SubCategoryRepository : GenericRepository<SubCategory>,ISubCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public SubCategoryRepository(ApplicationDbContext DbContext): base(DbContext) 
        {
            _dbContext = DbContext;
        }
       
        public async Task Update(SubCategory entity)
        {
            _dbContext.SubCategories.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}

