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
            /* joining the User and Role table on RoleID */
            // var data = from u in databaseContext.UserTable.ToList()
            //            join r in databaseContext.RoleTable.ToList()
            //            on u.RoleID equals r.RoleID
            //            select new UserModel()
            //            {
            //                roles = r,
            //                UserID = u.UserID,
            //                RoleID = u.RoleID,
            //                UserFullName = u.UserFullName,
            //                UserEmailID = u.UserEmailID,
            //                UserPassword = u.UserPassword,
            //                UserAddress = u.UserAddress,
            //                UserGender = u.UserGender,
            //                UserPhone = u.UserPhone,
            //                IsActive = u.IsActive
            //            };
            var data = from u in databaseContext.UserTable.ToList()
                       join ur in databaseContext.UserRoleTable.ToList()
                       on u.UserID equals ur.UserID
                       join r in databaseContext.RoleTable.ToList()
                       on ur.RoleID equals r.RoleID
                       select new UserModel()
                       {
                           roles = r,
                           UserID = u.UserID,
                           UserFullName = u.UserFullName,
                           UserEmailID = u.UserEmailID,
                           UserPassword = u.UserPassword,
                           UserAddress = u.UserAddress,
                           UserGender = u.UserGender,
                           UserPhone = u.UserPhone,
                           IsActive = u.IsActive
                       };
            return data;
        }

        public IEnumerable<UserModel> ShowUserMenu()
        {
            var data = from u in databaseContext.UserTable.ToList()
                       join um in databaseContext.UserMenuTable.ToList()
                       on u.UserID equals um.UserID
                       join m in databaseContext.MenuTable.ToList()
                       on um.MenuID equals m.MenuID
                       select new UserModel()
                       {
<<<<<<< HEAD
                           UserFullName = u.UserFullName,
                           UserEmailID = u.UserEmailID,
                           menus = m
=======
                           UserFullName= u.UserFullName,
                           UserEmailID=u.UserEmailID,
                           menus=m
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
                       };
            return data;
        }

        public UserModel UserLogin(string UserEmailID, string UserPassword)
        {
            // var data = GetAllFromTable();
            // var loginData1 = from items in data
            //                 where items.UserEmailID.Contains(UserEmailID)
            //                 && items.UserPassword.Contains(UserPassword)
            //                 select items;
            //OR

            /* Returns all the data of user which matches Email and Password */
            var loginData = databaseContext.UserTable.Where(u => u.UserEmailID == UserEmailID
                            && u.UserPassword == UserPassword).FirstOrDefault();
            if (loginData != null)
            {
                /* join tbl_Role and tbl_UserRole on RoleID */
                var roleData = from r in databaseContext.RoleTable.ToList()
                               join u in databaseContext.UserRoleTable.ToList()
                               on r.RoleID equals u.RoleID
                               select new RoleModel
                               {
                                   uRoles = u,
                                   RoleID = r.RoleID,
<<<<<<< HEAD
                                   RoleName = r.RoleName
=======
                                   RoleName = r.RoleName 
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
                               };

                /* Returns one or more roles of user */
                var loginRole = from items in roleData
                                where items.uRoles.UserID == loginData.UserID
                                select items;
<<<<<<< HEAD

=======
                                
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
                loginData.ListOfRoles = loginRole.ToList();
            }
            return loginData;
        }
    }
}