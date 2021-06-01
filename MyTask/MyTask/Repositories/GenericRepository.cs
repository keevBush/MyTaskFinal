using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyTask.Models;
using MyTask.Services;
using MyTasks.Core.Data.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace MyTask.Repositories
{
    public abstract class GenericRepository<TModel>  : IGenericRespository<TModel> where TModel: class
    {

        
        public GenericRepository( )
        {
            
        }
        
        public async Task<TModel[]> CreateAsync(params TModel[] data)
        {
            var type = typeof(TModel);
            var name = type.Name;

            await System.Threading.Tasks.Task.Run(() =>
            {
                DatabaseService.Database
                    .GetCollection<TModel>($"{name}s").InsertBulk(data);
            });
            
            return data;
        }

        public async Task<IEnumerable<TModel>> GetAllAync()
        {
            var type = typeof(TModel);
            var name = type.Name;
            var collections = DatabaseService.Database.GetCollectionNames();
            var tasks = DatabaseService.Database.GetCollection("Tasks").FindAll().ToList();
           var data = await System.Threading.Tasks.Task.Run(() =>  DatabaseService.Database
                    .GetCollection<TModel>($"{name}s").FindAll().ToList()
            );

           return data;
        }

        public async Task<IEnumerable<TModel>> GetAsync(Expression <Func<TModel, bool>> @where)
        {

            var allData = (await this.GetAllAync()).ToList();
            var steps = DatabaseService.Database.GetCollection<Step>("Steps").FindAll().ToList();

            var data = allData.Where(@where.Compile());
            return data;
        }
        
       
        public async Task<TModel> UpdateAsync(string id, TModel model)
        {
            var type = typeof(TModel);
            var name = type.Name;
            var isUpdate = DatabaseService.Database.GetCollection<TModel>($"{name}s").Update(id, model);

            if (!isUpdate)
                throw new ApplicationException($"{nameof(TModel)} not found");
            
            return model;
        }

        public async Task DeleteAsync(Expression<Func<TModel, bool>> @where)
        {
            var type = typeof(TModel);
            var name = type.Name;
            var isDeleted = DatabaseService.Database.GetCollection<TModel>($"{name}s").DeleteMany(@where);

            if (isDeleted == 0)
                throw new ApplicationException("No data has deleted");
        }
    }
}