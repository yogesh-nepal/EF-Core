using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;

namespace EFcoreBL.Repository
{
    public class CategoryRepository : GenericRepository<CategoryModel>,ICategory
    {
        private DatabaseContext databaseContext;
        public CategoryRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            this.databaseContext = _databaseContext;
        }
    }
}