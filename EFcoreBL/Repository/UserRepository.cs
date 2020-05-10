using System.Collections.Generic;
using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;

namespace EFcoreBL.Repository
{
    public class UserRepository : GenericRepository<UserModel>, IUser
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
        }

        public IEnumerable<UserModel> GetUserWithRole()
        {
            var data = from u in databaseContext.UserTable.ToList()
                       join r in databaseContext.RoleTable.ToList()
                       on u.RoleID equals r.RoleID
                       select new UserModel()
                       {
                           roles = r,
                           UserID = u.UserID,
                           UserFullName = u.UserFullName,
                           UserEmailID = u.UserEmailID,
                           UserAddress = u.UserAddress,
                           UserGender = u.UserGender,
                           UserPhone = u.UserPhone,
                           IsActive = u.IsActive
                       };
            return data;
        }
    }
}