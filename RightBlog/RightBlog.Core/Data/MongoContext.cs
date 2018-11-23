using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace RightBlog.Core.Data
{
    public class MongoContext 
    {
        public IMongoDatabase database;
        public IGridFSBucket gridFS; //File repository
        public IConfiguration Configuration { get; }
        public MongoContext (IConfiguration configuration) 
        {
            Configuration = configuration;

            string connectionString = Configuration.GetConnectionString("MongoDb");
            var connection = new MongoUrlBuilder(connectionString);

            MongoClient client = new MongoClient(connectionString);

            database = client.GetDatabase(connection.DatabaseName);
            gridFS = new GridFSBucket(database);
        }
    }
}
