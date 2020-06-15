using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IPostCategory : IGenericRepository<APostCategoryModel>
    {
         void InsertIntoPostCategoryTable(APostCategoryModel model);
         void DeleteFromPostCategoryTable(APostCategoryModel model);
         void DeletePostFromCategoryTable(int id);
    }
}