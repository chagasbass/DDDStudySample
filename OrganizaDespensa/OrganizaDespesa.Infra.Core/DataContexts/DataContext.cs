using CategoryOperations.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using ProductOperations.Core.Entities;
using System;
using UserOperations.Core.Entities;

namespace OrganizaDespensa.Infra.Core.DataContexts
{
    public class DataContext:IDataContext
    {
        private readonly IConfiguration Configuration;
        private readonly string DatabaseName = "despensa_db";
        public IMongoDatabase MongoConnection { get; private set; }

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Conect();
        }

        public void Conect() => ConfigureConnections();

        private void ConfigureConnections()
        {
            var connectionString = Configuration.GetConnectionString("stringConexaoBanco");
            var configurations = MongoClientSettings.FromConnectionString(connectionString);
            configurations.MaxConnectionIdleTime = TimeSpan.FromSeconds(30);
            configurations.UseTls = false;
            configurations.RetryWrites = false;

            var client = new MongoClient(configurations);

            MongoConnection = client.GetDatabase(DatabaseName);
        }

        #region Coleções

        public IMongoCollection<User> Users => MongoConnection.GetCollection<User>("Users");
        public IMongoCollection<Category> Categories => MongoConnection.GetCollection<Category>("Categories");
        public IMongoCollection<Product> Products => MongoConnection.GetCollection<Product>("Products");
        //public IMongoCollection<UnidadeDeMedida> UnidadesDeMedida => ConexaoMongo.GetCollection<UnidadeDeMedida>("UnidadesDeMedida");
        //public IMongoCollection<DetalhesDoProblema> DetalhesDoProblema => ConexaoMongo.GetCollection<DetalhesDoProblema>("DetalhesDoProblema");

        #endregion
    }
}
