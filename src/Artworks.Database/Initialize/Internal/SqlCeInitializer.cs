using Artworks.Database.CommonModel;
using Artworks.Database.CommonModel.Internal;
using Artworks.Database.Configuration;

namespace Artworks.Database.Initialize.Internal
{
    /// <summary>
    /// sql client initializer
    /// </summary>
    public class SqlCeInitializer : IDatabaseInitializer
    {
        private DatabaseConnectionStringManager instanceObj;

        public void InitializeDatabase()
        {
            this.instanceObj = new DatabaseConnectionStringManager();

            var key = DatabaseConnectionStringKey.Master;
            string connectionString = DatabaseRegistryConfiguration.Instance.GetValue<string>(key);
            this.instanceObj.Add(key, connectionString);

        }

        public void SetInitializeDatabase(DatabaseConnectionStringManager databaseConnectionStringManager)
        {
            this.instanceObj = databaseConnectionStringManager;
        }

        public string GetConnectionString(string key)
        {
            Guard.ArgumentNotNullOrEmpty(key, "key");
            return this.instanceObj.GetConnectionString(key);
        }


    }
}
