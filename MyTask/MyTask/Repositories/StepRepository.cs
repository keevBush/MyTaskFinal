using System;
using MyTask.Services;

namespace MyTask.Repositories
{
    public class StepRepository:GenericRepository<Models.Step>
    {
        private readonly DatabaseService _databaseService;

        public StepRepository(DatabaseService databaseService):base(databaseService)
        {
            _databaseService = databaseService;
        }
    }
}
