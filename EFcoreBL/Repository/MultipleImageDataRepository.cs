using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using EFcoreDAL;
using EFmodels;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace EFcoreBL.Repository
{
    public class MultipleImageDataRepository : GenericRepository<MultipleImageData>, IMultipleImageData
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<MultipleImageData> imageTable;
        public MultipleImageDataRepository(DatabaseContext _databaseContext) : base(_databaseContext)
        {
            databaseContext = _databaseContext;
            imageTable = _databaseContext.Set<MultipleImageData>();
        }

        public void DeleteFromMultipleImageDataTable(int id)
        {
            var data = imageTable.Where(p => p.APost.MultipleImagePostID == id);
            foreach (var item in data)
            {
                var imgURL = item.ImagePathData;
                if (imgURL != null)
                {
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resource/", imgURL);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                imageTable.Remove(item);
            }
        }

        public void InsertIntoMultipleImageDataTable(MultipleImageData model)
        {
            try
            {
                imageTable.Add(model);
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<MultipleImageData>> GetDataFromImageTable()
        {
            var imgData = await imageTable.Include(p => p.APost).ToListAsync();//.Include(p => p.APost)
            return imgData;
        }

    }
}