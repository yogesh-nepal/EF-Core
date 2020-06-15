<<<<<<< HEAD
using System.Net.Cache;
using System.IO;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
using System.Collections.Generic;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.Data.SqlClient;
using System.Linq;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953

namespace EFcoreBL.Repository
{
    public class PostRepository : GenericRepository<PostModel>, IPost
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<PostModel> postTable;
        public PostRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            postTable = _databaseContext.Set<PostModel>();
        }
        public void DeleteFromPostTable(int id)
        {
<<<<<<< HEAD
            var data = GetPostDataFromId(id);
            var imgURL = data.ImageURL;
            if (imgURL != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resource/", imgURL);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            postTable.Remove(data);
=======
            throw new System.NotImplementedException();
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
        }

        public IEnumerable<PostModel> GetAllPosts()
        {
<<<<<<< HEAD
            /* Join tbl_Post, tbl_Category and tbl_PostCategory */
            var postData = from p in databaseContext.PostTable.ToList()
                           join pc in databaseContext.PostCategoryTable.ToList()
                           on p.PostID equals pc.PostID
                           join c in databaseContext.CategoryTable.ToList()
                           on pc.CategoryID equals c.CategoryID
                           select new PostModel
                           {
                               catagories = c,
                               PostID = p.PostID,
                               Title = p.Title,
                               ShortDescription = p.ShortDescription,
                               FullDescription = p.FullDescription,
                               PublishDate = p.PublishDate,
                               IsActive = p.IsActive,
                               Remarks = p.Remarks,
                               AuthorName = p.AuthorName,
                               Tag = p.Tag,
                               ImageURL = p.ImageURL,
                               UpdatedTime = p.UpdatedTime,
                               UpdatedBy = p.UpdatedBy
                           };
            return postData;
=======
            throw new System.NotImplementedException();
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
        }

        public PostModel GetPostDataFromId(int id)
        {
<<<<<<< HEAD
            var data = postTable.Find(id);
            return data;
=======
            throw new System.NotImplementedException();
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
        }

        public void InsertIntoPostTable(PostModel model)
        {
            PostModel pm = new PostModel();
            pm.Title = model.Title;
            pm.FullDescription = model.FullDescription;
            pm.ShortDescription = model.ShortDescription;
            pm.ImageURL = model.ImageURL;
<<<<<<< HEAD
            // pm.CategoryID = model.CategoryID;
=======
            pm.CategoryID = model.CategoryID;
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
            pm.IsActive = model.IsActive;
            pm.AuthorName = model.AuthorName;
            pm.Tag = model.Tag;
            pm.PublishDate = model.PublishDate;
            pm.Remarks = model.Remarks;
            try
            {
                postTable.Add(pm);
            }
            catch (System.Exception)
            {
<<<<<<< HEAD

                throw;
            }

        }

        /*
        ERROR WHILE UPDATING = The instance of entity type 'PostModel' cannot be tracked
        because another instance with the same key value for {'PostID'} is already being tracked. 
        */
=======
                
                throw;
            }
            
        }

>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
        public void UpdatePostTable(PostModel model)
        {
            PostModel pm = new PostModel();
            pm.PostID = model.PostID;
            pm.Title = model.Title;
            pm.FullDescription = model.FullDescription;
            pm.ShortDescription = model.ShortDescription;
            pm.ImageURL = model.ImageURL;
<<<<<<< HEAD
            // pm.CategoryID = model.CategoryID;
            pm.IsActive = model.IsActive;
            pm.Tag = model.Tag;
            pm.Remarks = model.Remarks;
            pm.UpdatedTime = model.UpdatedTime;
=======
            pm.CategoryID = model.CategoryID;
            pm.IsActive = model.IsActive;
            pm.Tag = model.Tag;
            pm.Remarks = model.Remarks;
            pm.UpdatedTime= model.UpdatedTime;
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
            pm.UpdatedBy = model.UpdatedBy;
            try
            {
                postTable.Attach(pm);
                databaseContext.Entry(pm).State = EntityState.Modified;
            }
<<<<<<< HEAD
            catch (SqlException ex)
            {

                throw ex;
            }

=======
            catch (System.Exception ex)
            {
                
                throw ex;
            }
            
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
        }
    }
}