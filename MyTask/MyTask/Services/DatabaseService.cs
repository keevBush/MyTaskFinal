using System;
using System.IO;
using LiteDB;
using LiteDB.Engine;
using MyTask.Models;
using MyTask.Utils;
using MyTasks.Core.Data.Interfaces;
using Xamarin.Essentials;
using Task = System.Threading.Tasks.Task;

namespace MyTask.Services
{
    public class DatabaseService
    {
        private static LiteDatabase _database;

        public static LiteDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = InitializeDatabase().Result;
                }
                return _database;
            }
        }
        
        private static async System.Threading.Tasks.Task<LiteDatabase> InitializeDatabase()
        {
            string password = await SecureStorage.GetAsync(Keys.KeyUser);
            
            if (string.IsNullOrEmpty(password))
                return null;
            var test = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                $"database.db");
            var connectionString = new ConnectionString()
            {
                Filename = System.IO.Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.Personal),$"database.db"),
                Password = password
            };
            
            return  new LiteDatabase(connectionString, BsonMapper.Global);

        }

        
        
    }
}
