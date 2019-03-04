using System;
using System.IO;

namespace Couchbase.Lite.Mapping.Sample.Core.Respositories
{
    public abstract class BaseRepository<T>
    {
        string DatabaseName { get; set; }

        DatabaseConfiguration _databaseConfig;
        DatabaseConfiguration DatabaseConfig
        {
            get
            {
                if (_databaseConfig == null)
                {
                    _databaseConfig = new DatabaseConfiguration
                    {
                        Directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), 
                                        AppInstance.User.Username)
                    };
                }

                return _databaseConfig;
            }
        }

        Database _database;
        Database Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new Database(DatabaseName, DatabaseConfig);
                }

                return _database;
            }
        }

        protected BaseRepository(string databaseName)
        {
            DatabaseName = databaseName;
        }

        public virtual T Get(string id)
        {
            T obj = default(T);

            var document = Database.GetDocument(id);

            if (document != null)
            {
                obj = document.ToObject<T>();
            }

            return obj;
        }

        public virtual void Set(T obj, string id = null)
        {
            var document = obj?.ToMutableDocument(id);

            if (document != null)
            {
                Database.Save(document);
            }
        }
    }
}
