using System.Collections.Generic;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IUser : IGenericRepository<UserModel>
    {
         IEnumerable<UserModel> GetUserWithRole();
         IEnumerable<UserModel> ShowUserMenu();
         UserModel UserLogin(string UserEmailID,string UserPassword);
         void InsertFromCSV(List<UserModel> model);
    }
}