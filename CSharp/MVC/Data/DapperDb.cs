using Dapper;
using Microsoft.Extensions.Options;
using Mvc.Models;
using MVC.Configuration;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace Mvc.Data
{
    public class DapperDb
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;

        public DapperDb(DbProviderFactory dbProviderFactory, IOptions<AppSettings> appSettings)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = appSettings.Value.ConnectionString;
        }

        public async Task<IEnumerable<Fortune>> LoadFortunesRows()
        {
            List<Fortune> result;

            using (var db = _dbProviderFactory.CreateConnection())
            {
                db.ConnectionString = _connectionString;

                // Note: don't need to open connection if only doing one thing; let dapper do it
                result = (await db.QueryAsync<Fortune>("SELECT id, message FROM fortune")).AsList();
            }

            result.Add(new Fortune { Message = "Additional fortune added at request time." });
            result.Sort();

            return result;
}
    }
}
