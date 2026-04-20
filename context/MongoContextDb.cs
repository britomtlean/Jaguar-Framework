using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JaguarFramework.Models;
using MongoDB.Driver;

// CLASSE PARA CONEXÃO COM MONGODB

namespace JaguarFramework.Context
{
    public class MongoContextDb
    {
        protected readonly IMongoDatabase _database;

        public MongoContextDb()
        {
            DotNetEnv.Env.Load();
            var database = Environment.GetEnvironmentVariable("DATABASE");
            var cluster = Environment.GetEnvironmentVariable("CLUSTER");


            if (string.IsNullOrWhiteSpace(database))
            {
                throw new Exception("DATABASE não definida.");
            }

            if (string.IsNullOrWhiteSpace(cluster))
            {
                throw new Exception("CLUSTER não definido.");
            }

            //Console.WriteLine(database);
            //Console.WriteLine(cluster);

            var client = new MongoClient(database);
            _database = client.GetDatabase(cluster);
        }
    }
}
