using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyTasks.Core.Data.Interfaces
{
    public interface IGenericRespository<TModel> where TModel:class
    {
        Task<TModel[]> CreateAsync(params TModel[] data);
        Task<IEnumerable<TModel>> GetAllAync();
        Task<IEnumerable<TModel>> GetAsync(Expression< Func<TModel, bool>> where);
        Task<TModel> UpdateAsync(string id, TModel model);
        Task DeleteAsync(Expression<Func<TModel, bool>> where);
    }
}