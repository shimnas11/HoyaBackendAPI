using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoya.Inventory.Infrastructure.Mongo
{
    using MongoDB.Driver;
    using Microsoft.Extensions.Options;
    using Hoya.Inventory.Domain.Entities;

    public class MongoDbContext
    {
        public IMongoDatabase Database { get; }
        public IMongoClient Client { get; }

        public MongoDbContext(IOptions<Domain.Configurations.MongoDbSettings> settings)
        {
            Client = new MongoClient(settings.Value.ConnectionString);
            Database = Client.GetDatabase(settings.Value.DatabaseName);
        }

    }
}
