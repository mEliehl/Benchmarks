using Microsoft.Extensions.Options;
using Mvc.Models;
using MVC.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;

namespace MVC.Data
{
    public class RawDb
    {
        private readonly DbProviderFactory _dbProviderFactory;
        private readonly string _connectionString;

        public RawDb(DbProviderFactory dbProviderFactory, IOptions<AppSettings> appSettings)
        {
            _dbProviderFactory = dbProviderFactory;
            _connectionString = appSettings.Value.ConnectionString;
        }

        public async Task<IEnumerable<Fortune>> LoadFortunesRows()
        {
            var result = new List<Fortune>();

            using (var db = _dbProviderFactory.CreateConnection())
            using (var cmd = db.CreateCommand())
            {
                cmd.CommandText = "SELECT id, message FROM fortune";

                db.ConnectionString = _connectionString;
                await db.OpenAsync();

                using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection))
                {
                    while (await rdr.ReadAsync())
                    {
                        result.Add(new Fortune
                        {
                            Id = rdr.GetInt32(0),
                            Message = rdr.GetString(1)
                        });
                    }
                }
            }

            result.Add(new Fortune { Message = "Additional fortune added at request time." });
            result.Sort();

            return result;
        }

    }
}