<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IPosts : IGenericRepository<APostWithMultipleImageModel>
    {
         void InsertIntoPostsTable(APostWithMultipleImageModel model);
<<<<<<< HEAD
         void DeleteFromPostsTable(int id);
         void SavePostsTable();
         APostWithMultipleImageModel GetDataFromPosts(int id);
         Task<IEnumerable<APostWithMultipleImageModel>> GetPostWithFiles();
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
    }
}