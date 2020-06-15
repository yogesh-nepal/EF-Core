using System;
using System.Collections.Generic;
using System.Linq;
using EFcoreBL.Interface;
using EFcoreDAL;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace EFcoreBL.Repository
{
    public class GenericRepository<NameOfYourModelClass> : IGenericRepository<NameOfYourModelClass> where NameOfYourModelClass : class
    {
        private readonly DatabaseContext databaseContext;
        public DbSet<NameOfYourModelClass> table;
        public GenericRepository(DatabaseContext _databaseContext)
        {
            this.databaseContext = _databaseContext;
            table = _databaseContext.Set<NameOfYourModelClass>();
            
        }
        public void DeleteFromTable(int id)
        {
            var data = GetDataFromId(id);
            table.Remove(data);
        }

        public IEnumerable<NameOfYourModelClass> GetAllFromTable()
        {
            
            return table.ToList();
        }

        public NameOfYourModelClass GetDataFromId(int id)
        {
            return table.Find(id);
        }

        public void InsertIntoTable(NameOfYourModelClass model)
        {
            try
            {
                table.Add(model);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void Save()
        {
            try
            {
                databaseContext.SaveChanges();
            }
            catch (SqlException ex)
            {
                
                throw ex;
            }
            
        }

        public IEnumerable<NameOfYourModelClass> SearchFromTable(Func<NameOfYourModelClass, bool> predicate)
        {
            return table.Where(predicate);
        }

        public void UpdateTable(NameOfYourModelClass model)
        {
            table.Attach(model);
            databaseContext.Entry(model).State = EntityState.Modified;
        }
    }
}
