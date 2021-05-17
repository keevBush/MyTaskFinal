using System;
using System.Threading.Tasks;
//using MyTask.Core.Data.Interfaces;
//using MyTask.Core.Data.Models;
using MyTask.Utils;
using MyTasks.Core.Data.Interfaces;
using MyTasks.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MyTask.Repositories
{
   
    public class UserRepository: IUserRepository
    {

        /// <summary>
        /// Get current user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Current user saved into secure storage</returns>
        public async Task<User> CreateUser(User user)
        {
            await SecureStorage.SetAsync(Keys.CurrentUserKey, JsonConvert.SerializeObject(user));
            await SecureStorage.SetAsync(Keys.KeyUser, user.Id);

            return user;
        }

        /// <summary>
        /// Get current user
        /// </summary>
        /// <returns>Current user saved into secure storage</returns>
        public async Task<User> GetCurrentUser()
        {
            try
            {
                var currentUserStr = await SecureStorage.GetAsync(Keys.CurrentUserKey);
                if (string.IsNullOrEmpty(currentUserStr) || string.IsNullOrWhiteSpace(currentUserStr))
                    return null;

                return JsonConvert.DeserializeObject<User>(currentUserStr);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
