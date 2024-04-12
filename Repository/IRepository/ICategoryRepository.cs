using Product.Model;

namespace Product.Repository.IRepository
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task Update(Category entity);
        
    }
}
