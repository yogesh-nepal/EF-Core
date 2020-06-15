using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class PostCategoryRepository : GenericRepository<APostCategoryModel>, IPostCategory
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<APostCategoryModel> postCategoryTable;
        public PostCategoryRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            postCategoryTable = _databaseContext.Set<APostCategoryModel>();
        }

        public void DeleteFromPostCategoryTable(APostCategoryModel model)
        {
            // var allData= GetAllFromTable();
            // var catData = allData.Where(p=> p.CategoryID == model.CategoryID);
            var data = from item in databaseContext.PostCategoryTable.ToList()
                       where item.CategoryID.Equals(model.CategoryID) && item.PostID.Equals(model.PostID)
                       select item;
            // var postCategoryData = GetDataFromId(id);
            foreach (var item in data)
            {
                postCategoryTable.Remove(item);
            }
            // postCategoryTable.Remove(data.FirstOrDefault());

        }

        public void DeletePostFromCategoryTable(int id)
        {
            var data = from item in databaseContext.PostCategoryTable.ToList()
                       where item.PostID.Equals(id)
                       select item;
            foreach (var item in data)
            {
                postCategoryTable.Remove(item);
            }
        }

        public void InsertIntoPostCategoryTable(APostCategoryModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}