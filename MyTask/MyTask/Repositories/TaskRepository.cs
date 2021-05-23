using MyTask.Services;

namespace MyTask.Repositories
{
    public class TaskRepository:GenericRepository<MyTask.Models.Task>
    {
        private readonly DatabaseService _databaseService;

        public TaskRepository(DatabaseService databaseService):base(databaseService)
        {
            _databaseService = databaseService;
        }
    }
}