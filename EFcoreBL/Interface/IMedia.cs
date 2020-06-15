using System.Collections.Generic;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IMedia : IGenericRepository<MediaPostModel>
    {
         IEnumerable<MediaPostModel> GetAllMedia();
         MediaPostModel GetMediaFromId(int id);
         void InsertIntoMediaTable(MediaPostModel model);
         void UpdateMediaTable(MediaPostModel model);
         void DeleteFromMediaTable(int id);
    }
}