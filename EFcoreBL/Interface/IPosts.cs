using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IPosts : IGenericRepository<APostWithMultipleImageModel>
    {
         void InsertIntoPostsTable(APostWithMultipleImageModel model);
    }
}