using System.Collections.Generic;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IPost : IGenericRepository<PostModel>
    {
         IEnumerable<PostModel> GetAllPosts();
         PostModel GetPostDataFromId(int id);
         void InsertIntoPostTable(PostModel model);
         void UpdatePostTable(PostModel model);
         void DeleteFromPostTable(int id);
    }
}