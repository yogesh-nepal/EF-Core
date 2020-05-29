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
    }
}