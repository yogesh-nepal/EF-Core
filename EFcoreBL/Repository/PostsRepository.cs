<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
=======
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class PostsRepository : GenericRepository<APostWithMultipleImageModel>, IPosts
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<APostWithMultipleImageModel> postsTable;
        public PostsRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            postsTable = _databaseContext.Set<APostWithMultipleImageModel>();
        }
<<<<<<< HEAD

        public void DeleteFromPostsTable(int id)
        {
            try
            {
                var data = GetDataFromPosts(id);
                postsTable.Remove(data);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public APostWithMultipleImageModel GetDataFromPosts(int id)
        {
            return postsTable.Find(id);
        }

        public async Task<IEnumerable<APostWithMultipleImageModel>> GetPostWithFiles()
        {
            var posts = await postsTable.Include(p => p.MultipleImageData).ToListAsync();
            return posts;
        }

        public void InsertIntoPostsTable(APostWithMultipleImageModel model)
        {
            // APostWithMultipleImageModel mpModel = new APostWithMultipleImageModel();
            // mpModel.ImageTitle = model.ImageTitle;
            // mpModel.ImageDescription = model.ImageDescription;
            // mpModel.MultipleImageData = model.MultipleImageData;
            // mpModel.ImagePath = model.ImagePath;
            try
            {
                postsTable.Add(model);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public void SavePostsTable()
        {
            databaseContext.SaveChanges();
        }
=======
        public void InsertIntoPostsTable(APostWithMultipleImageModel model)
        {
            APostWithMultipleImageModel mpModel = new APostWithMultipleImageModel();
            mpModel.ImageTitle = model.ImageTitle;
            mpModel.ImageDescription = model.ImageDescription;
            mpModel.MultipleImageData = model.MultipleImageData;
            try
            {
                postsTable.Add(mpModel);
            }
            catch (System.Exception ex)
            {
                
                throw ex;
            }
        }
>>>>>>> ea941148eddb45598dd2392b9b31454ee9217953
    }
}