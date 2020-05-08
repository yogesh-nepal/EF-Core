using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;

namespace EFcoreBL.Repository
{
    public class UserRepository : GenericRepository<UserModel>,IUser
    {
        private readonly DatabaseContext databaseContext;

        public UserRepository(DatabaseContext _databaseContext):base(_databaseContext)
        {
            databaseContext = _databaseContext;
        }
    }
}