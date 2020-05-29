using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;

namespace EFcoreBL.Repository
{
    public class MenuRepository : GenericRepository<MenuModel>,IMenu
    {
        private readonly DatabaseContext databaseContext;
        public MenuRepository(DatabaseContext _databaseContext): base(_databaseContext)
        {
            databaseContext = _databaseContext;
        }
    }
}