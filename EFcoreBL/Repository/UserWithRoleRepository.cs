using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class UserWithRoleRepository : GenericRepository<UserWithRoleModel>, IUserWithRole
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<UserWithRoleModel> uTable;
        public UserWithRoleRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            uTable = _databaseContext.Set<UserWithRoleModel>();
        }

        public void DeleteRoleForUser(int id)
        {
            var data = databaseContext.UserRoleTable.Where(u => u.RoleID == id).FirstOrDefault();
            uTable.Remove(data);
        }

        public void InsertRoleForUser(UserWithRoleModel model)
        {
            UserWithRoleModel urm = new UserWithRoleModel();
            urm.UserID = model.UserID;
            urm.RoleID = model.RoleID;
            uTable.Add(urm);
        }
    }
}