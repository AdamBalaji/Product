using Product.Model;

namespace Product.Repository.IRepository
{
    public interface ISubCategoryRepository : IGenericRepository<SubCategory>
    {
       
        Task Update(SubCategory entity);

    }
}
