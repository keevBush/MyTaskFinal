using System;
using LiteDB;
using MyTask.Utils;
using MyTasks.Core.Data.Interfaces;
using Xamarin.Essentials;

namespace MyTask.Services
{
    public class DatabaseService:IDatabaseService
    {
        private LiteDatabase _database;

        public LiteDatabase Database
        {
            get
            {
                if (_database == null)
                    InitializeDatabase();
                return _database;
            }
        }
        public DatabaseService()
        {
            InitializeDatabase();
        }

        public async void InitializeDatabase()
        {
            string password = await SecureStorage.GetAsync(Keys.KeyUser);
            
            if (string.IsNullOrEmpty(password))
                return;

            var connectionString = new ConnectionString()
            {
                Filename = System.IO.Path.Combine (Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),$"database-{password}.db"),
                Password = password,
            };

            _database = new LiteDatabase(connectionString);
        }
    }
}
