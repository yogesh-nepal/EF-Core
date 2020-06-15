using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IPosts : IGenericRepository<APostWithMultipleImageModel>
    {
         void InsertIntoPostsTable(APostWithMultipleImageModel model);
         void DeleteFromPostsTable(int id);
         void SavePostsTable();
         APostWithMultipleImageModel GetDataFromPosts(int id);
         Task<IEnumerable<APostWithMultipleImageModel>> GetPostWithFiles();
    }
}