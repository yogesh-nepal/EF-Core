using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace EFcoreBL.Interface
{
    public interface IGenericRepository<NameOfYourModelClass> where NameOfYourModelClass:class
    {
         IEnumerable<NameOfYourModelClass> GetAllFromTable();
         NameOfYourModelClass GetDataFromId(int id);
         void InsertIntoTable(NameOfYourModelClass model);
         void UpdateTable(NameOfYourModelClass model);
         void DeleteFromTable(int id);
         void Save();
         IEnumerable<NameOfYourModelClass> SearchFromTable(Func<NameOfYourModelClass,bool> predicate);
    }
}