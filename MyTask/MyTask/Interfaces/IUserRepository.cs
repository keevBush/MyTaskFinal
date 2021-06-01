using MyTask.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyTask.Core.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task<User> GetCurrentUser();
    }
}
