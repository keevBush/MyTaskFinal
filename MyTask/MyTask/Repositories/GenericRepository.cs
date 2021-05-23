using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MyTask.Services;
using MyTasks.Core.Data.Interfaces;

namespace MyTask.Repositories
{
    public abstract class GenericRepository<TModel>  : IGenericRespository<TModel> where TModel: class
    {
        private readonly DatabaseService _databaseService;
        
        public GenericRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        
        public async Task<TModel[]> CreateAsync(params TModel[] data)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                _databaseService.Database
                    .GetCollection<TModel>($"{nameof(TModel)}s").InsertBulk(data);
            });
            
            return data;
        }

        public async Task<IEnumerable<TModel>> GetAllAync()
        {
           var data = await System.Threading.Tasks.Task.Run(() =>  _databaseService.Database
                    .GetCollection<TModel>($"{nameof(TModel)}s").FindAll()
            );

           return data;
        }

        public async Task<IEnumerable<TModel>> GetAsync(Expression<Func<TModel, bool>> @where)
        {
            var data = await System.Threading.Tasks.Task.Run(() =>  _databaseService.Database
                .GetCollection<TModel>($"{nameof(TModel)}s").Find(@where)
            );

            return data;
        }

        public async Task<TModel> UpdateAsync(string id, TModel model)
        {
            var isUpdate = await System.Threading.Tasks.Task.Run(() => 
                _databaseService.Database.GetCollection<TModel>($"{nameof(TModel)}s").Update(id, model)
            );
            if (!isUpdate)
                throw new ApplicationException($"{nameof(TModel)} not found");
            
            return model;
        }

        public async Task DeleteAsync(Expression<Func<TModel, bool>> @where)
        {
            var isDeleted = await System.Threading.Tasks.Task.Run(() => 
                _databaseService.Database.GetCollection<TModel>($"{nameof(TModel)}s").DeleteMany(@where)
            );

            if (isDeleted == 0)
                throw new ApplicationException("No data has deleted");
        }
    }
}