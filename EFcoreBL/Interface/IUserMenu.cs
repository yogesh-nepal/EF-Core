using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IUserMenu: IGenericRepository<UserMenuModel>
    {
         void DeleteMenuForUser(int id);
    }
}