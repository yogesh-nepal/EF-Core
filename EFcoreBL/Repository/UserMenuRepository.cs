using System.Collections.Generic;
using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class UserMenuRepository : GenericRepository<UserMenuModel>, IUserMenu
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<UserMenuModel> mTable;
        public UserMenuRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            mTable = _databaseContext.Set<UserMenuModel>();
        }
        public void DeleteMenuForUser(int id)
        {
            var data = databaseContext.UserMenuTable.Where(u => u.MenuID == id).FirstOrDefault();
            mTable.Remove(data);
        }
    }
}