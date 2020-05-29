using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IUserWithRole : IGenericRepository<UserWithRoleModel>
    {
         void DeleteRoleForUser(int id);
         void InsertRoleForUser(UserWithRoleModel model);
    }
}