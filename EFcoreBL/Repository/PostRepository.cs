using System.Collections.Generic;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;

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
            throw new System.NotImplementedException();
        }

        public IEnumerable<PostModel> GetAllPosts()
        {
            throw new System.NotImplementedException();
        }

        public PostModel GetPostDataFromId(int id)
        {
            throw new System.NotImplementedException();
        }

        public void InsertIntoPostTable(PostModel model)
        {
            PostModel pm = new PostModel();
            pm.Title = model.Title;
            pm.FullDescription = model.FullDescription;
            pm.ShortDescription = model.ShortDescription;
            pm.ImageURL = model.ImageURL;
            pm.CategoryID = model.CategoryID;
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
                
                throw;
            }
            
        }

        public void UpdatePostTable(PostModel model)
        {
            PostModel pm = new PostModel();
            pm.PostID = model.PostID;
            pm.Title = model.Title;
            pm.FullDescription = model.FullDescription;
            pm.ShortDescription = model.ShortDescription;
            pm.ImageURL = model.ImageURL;
            pm.CategoryID = model.CategoryID;
            pm.IsActive = model.IsActive;
            pm.Tag = model.Tag;
            pm.Remarks = model.Remarks;
            pm.UpdatedTime= model.UpdatedTime;
            pm.UpdatedBy = model.UpdatedBy;
            try
            {
                postTable.Attach(pm);
                databaseContext.Entry(pm).State = EntityState.Modified;
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
            
        }
    }
}