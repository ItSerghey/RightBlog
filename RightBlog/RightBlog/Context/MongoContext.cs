using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace RightBlog.Context
{
    public class MongoContext
    {
       public IMongoDatabase database;
       public IGridFSBucket gridFS; //File repository


        public MongoContext()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MongoDb"].ConnectionString;
            var connection = new MongoUrlBuilder(connectionString);

            MongoClient client = new MongoClient(connectionString);

            database = client.GetDatabase(connection.DatabaseName);
            gridFS = new GridFSBucket(database);
        }
    }
}