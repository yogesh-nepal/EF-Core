using System.Collections.Generic;
using System.Threading.Tasks;
using EFmodels;

namespace EFcoreBL.Interface
{
    public interface IMultipleImageData : IGenericRepository<MultipleImageData>
    {
        void InsertIntoMultipleImageDataTable(MultipleImageData model);
        void DeleteFromMultipleImageDataTable(int id);
        Task<IEnumerable<MultipleImageData>> GetDataFromImageTable();
    }
}