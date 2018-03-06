using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.CodeRule
{
    /*
    /// <summary>
    /// 基于数据库的种子仓储。
    /// </summary>
    public sealed class DatabaseSeedStore : ISeedStore
    {
        private string _connectionStringName;
        private string _connectionString;
        private string _providerName;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public DatabaseSeedStore(string connectionStringName)
        {
            _connectionStringName = connectionStringName;
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public DatabaseSeedStore(string connectionString, string providerName)
        {
            _connectionString = connectionString;
            _providerName = providerName;
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public int NextSeed(string seedKey)
        {
            var db = this.CreateDatabase();
            var seed = db.SingleOrDefault<Seed>("SELECT * FROM [Seeds] WHERE [SeedKey]=@0", seedKey);
            if (seed == null)
            {
                db.Insert("Seeds", "SeedId", new { SeedKey = seedKey, SeedValue = 1 });

                return 1;
            }
            else
            {
                seed.SeedValue++;
                var result = db.Execute("UPDATE [Seeds] SET [SeedValue]=@0 WHERE [SeedId]=@1 AND [Version]=@2", seed.SeedValue, seed.SeedId, seed.Version);
                if (result == 0)
                {
                    return this.NextSeed(seedKey);
                }

                return seed.SeedValue;
            }
        }

        private Database CreateDatabase()
        {
            if (_connectionStringName != null)
            {
                return new Database(_connectionStringName);
            }

            return new Database(_connectionString, _providerName);
        }

        private class Seed
        {
            public int SeedId { get; set; }

            public string SeedKey { get; set; }

            public int SeedValue { get; set; }

            public byte[] Version { get; set; }
        }
    }
     * */
}
