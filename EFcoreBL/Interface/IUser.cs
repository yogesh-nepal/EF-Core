using System.Collections.Generic;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IUser : IGenericRepository<UserModel>
    {
         IEnumerable<UserModel> GetUserWithRole();
    }
}