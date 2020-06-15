using System.Collections.Generic;
using System.IO;
using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class MediaRepository : GenericRepository<MediaPostModel>, IMedia
    {
        private readonly DatabaseContext databaseContext;
        private readonly DbSet<MediaPostModel> mediaTable;
        public MediaRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            mediaTable = _databaseContext.Set<MediaPostModel>();
        }

        public void DeleteFromMediaTable(int id)
        {
            var data = GetMediaFromId(id);
            var mediaURL = data.MediaPostPath;
            if (mediaURL != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Videos/", mediaURL);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            var ThumbnailURL = data.MediaThumbnail;
            if (ThumbnailURL != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Thumbnails/", ThumbnailURL);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            mediaTable.Remove(data);
        }

        public IEnumerable<MediaPostModel> GetAllMedia()
        {
            return mediaTable.ToList();
        }

        public MediaPostModel GetMediaFromId(int id)
        {
            return mediaTable.Find(id);
        }

        public void InsertIntoMediaTable(MediaPostModel model)
        {
            MediaPostModel mediaModel = new MediaPostModel();
            mediaModel.MediaTitle = model.MediaTitle;
            mediaModel.MediaPostDescription = model.MediaPostDescription;
            mediaModel.MediaSource = model.MediaSource;
            mediaModel.MediaTags = model.MediaTags;
            mediaModel.MediaPostPath = model.MediaPostPath;
            mediaModel.PublishDate = model.PublishDate;
            mediaModel.MediaThumbnail = model.MediaThumbnail;
            mediaModel.MediaAuthor = model.MediaAuthor;
            try
            {
                mediaTable.Add(mediaModel);
            }
            catch (SqlException ex)
            {

                throw ex;
            }
        }

        public void UpdateMediaTable(MediaPostModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}