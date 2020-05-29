using System.Collections.Generic;
using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;

namespace EFcoreBL.Repository
{
    public class RoleRepository : GenericRepository<RoleModel>, IRole
    {
        private readonly DatabaseContext databaseContext;

        public RoleRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
        }

    }
}