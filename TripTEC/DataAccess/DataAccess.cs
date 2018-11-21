using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using Neo4j.Driver.V1;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using TripTEC.DataAccess;
using MongoDB.Driver.Core.Clusters;
using Neo4jClient;


namespace TripTEC.DataAccess
{

    public class DataAcess
    {
        IMongoDatabase db;
        public DataAcess()
        {
            String connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            db = client.GetDatabase("TripTECDB");
        }

        public List<BsonDocument> getAllFromCollection(string collectionName)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(collectionName);
            return collection.Find(_ => true).ToList();
        }

        public List<BsonDocument> getAllFromCollectionWithFilter(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).ToList();
        }
        public bool insertCollection(BsonDocument document, string collectionName)
        {
            if (db == null)
            {
                return false;
            }

            var collection = db.GetCollection<BsonDocument>(collectionName);

            if (collection == null)
            {
                return false;
            }

            collection.InsertOne(document);
            return true;
        }

        public bool updateCollection(string id, string collectionName, UpdateDefinition<BsonDocument> updateSet)
        {
            var updateFilter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var collection = db.GetCollection<BsonDocument>(collectionName);
            if (updateFilter == null || collection == null)
            {
                return false;
            }

            collection.UpdateOne(updateFilter, updateSet);
            return true;
        }

        public void printCollection(List<BsonDocument> documents)
        {
            foreach (BsonDocument d in documents)
            {
                System.Diagnostics.Debug.WriteLine(d.ToString());
            }
        }

        public void migrateMongoDBToNeo4j()
        {
            // Init
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            // Create clients
            var collection = db.GetCollection<BsonDocument>("Clientes");
            var clientList = collection.Find(new BsonDocument()).ToList();

            List<object> clientes = new List<object>();
            List<object> reservaciones = new List<object>();
            List<object> sitios = new List<object>();

            // Create all clients
            foreach (var cliente in clientList)
            {
                var nombre = cliente["nombre"].As<String>();
                var cedula = cliente["cedula"].As<String>();
                var clienteId = cliente["_id"].As<String>();
                var clienteNeo4j = new Cliente() { id = clienteId, nombre = nombre, cedula = cedula };

                // Create cliente node
                client.Cypher
                    .Merge("(cliente:Cliente { Id: {id} })")
                    .OnCreate()
                    .Set("cliente = {clienteNeo4j}")
                    .WithParams(new
                    {
                        id = clienteNeo4j.id,
                        clienteNeo4j
                    })
                    .ExecuteWithoutResults();

                // Add client to list
                clientes.Add(clienteNeo4j);
            }

            // Create all reservations
            collection = db.GetCollection<BsonDocument>("Reservaciones");
            var reservationList = collection.Find(new BsonDocument()).ToList();

            foreach (var reservation in reservationList)
            {
                var reservacionId = reservation["_id"].As<String>();
                var cantidadPersonas = reservation["cantidadPersonas"].As<int>();
                var fechaLlegada = reservation["fechaLlegada"].As<String>();
                var fechaSalida = reservation["fechaSalida"].As<String>();
                var nombreCliente = reservation["nombreCliente"].As<String>();
                var nombreSitio = reservation["sitioTuristico"].As<String>();
                var reservacionNeo4j = new Reservacion() { id = reservacionId, cantidadPersonas = cantidadPersonas, fechaLlegada = fechaLlegada, fechaSalida = fechaSalida, nombreCliente = nombreCliente,
                nombreSitio = nombreSitio};

                // Create reservation node
                client.Cypher
                    .Merge("(reservacion:Reservacion { Id: {id} })")
                    .OnCreate()
                    .Set("reservacion = {reservacionNeo4j}")
                    .WithParams(new
                    {
                        id = reservacionNeo4j.id,
                        reservacionNeo4j
                    })
                    .ExecuteWithoutResults();

                // Add reservation to list
                reservaciones.Add(reservacionNeo4j);
            }

            // Create all sites
            collection = db.GetCollection<BsonDocument>("SitiosTuristicos");
            var touristicSiteList = collection.Find(new BsonDocument()).ToList();

            foreach (var touristicSite in touristicSiteList)
            {
                var sitioId = touristicSite["_id"].As<String>();
                var nombre = touristicSite["nombre"].As<String>();
                var descripcion = touristicSite["descripcion"].As<String>();
                var sitioNeo4j = new Sitio() { id = sitioId, nombre = nombre, descripcion = descripcion };

                // Create site node
                client.Cypher
                    .Merge("(sitio:SitioTuristico { Id: {id} })")
                    .OnCreate()
                    .Set("sitio = {sitioNeo4j}")
                    .WithParams(new
                    {
                        id = sitioNeo4j.id,
                        sitioNeo4j
                    })
                    .ExecuteWithoutResults();

                // Add site to list
                sitios.Add(sitioNeo4j);
            }

            // Make relationships
            makeRelationships(clientes, reservaciones, sitios);


        }

        public void makeRelationships(List<object> clientes, List<object> reservaciones, List<object> sitios)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            foreach (Cliente cliente in clientes)
            {
                var nombreCliente = cliente.nombre;
                foreach (Reservacion reservacion in reservaciones)
                {
                    if (reservacion.nombreCliente == nombreCliente)
                    {
                        client.Cypher
                        .Match("(cliente1:Cliente)", "(reservacion1:Reservacion)")
                        .Where((Cliente cliente1) => cliente1.id == cliente.id)
                        .AndWhere((Reservacion reservacion1) => reservacion1.id == reservacion.id)
                        .CreateUnique("(cliente1)-[:CREA]->(reservacion1)")
                        .ExecuteWithoutResults();
                    }
                }
            }

            foreach (Reservacion reservacion in reservaciones)
            {
                var nombreSitio = reservacion.nombreSitio;
                foreach (Sitio sitio in sitios)
                {
                    if (sitio.nombre == nombreSitio)
                    {
                        client.Cypher
                        .Match("(reservacion1:Reservacion)", "(sitio1:SitioTuristico)")
                        .Where((Reservacion reservacion1) => reservacion1.id == reservacion.id)
                        .AndWhere((Sitio sitio1) => sitio1.id == sitio.id)
                        .CreateUnique("(reservacion1)-[:UBICA]->(sitio1)")
                        .ExecuteWithoutResults();
                    }
                }
            }

        }

        public IEnumerable<Queries> getAllReservationsByClient(String nombreCliente)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            var reservations = client.Cypher
                                .Match("(cliente:Cliente)-[:CREA]->(reservacion:Reservacion)")
                                .Where((Cliente cliente) => cliente.nombre == nombreCliente)
                                .Return((reservacion) => new Queries
                                {
                                    reservations = reservacion.CollectAs<Reservacion>()
                                })
                                .Results;

            return reservations;
        }

        public IEnumerable<Queries> getAllSites()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            var sites = client.Cypher
                                .Match("(sitio:SitioTuristico)<-[:UBICA]-(reservacion:Reservacion)")
                                .With("DISTINCT sitio")
                                .ReturnDistinct((sitio) => new Queries
                                {
                                    sites = sitio.CollectAs<Sitio>()
                                })
                                .Results;

            return sites;
        }

        public IEnumerable<Queries> getMostVisitedSites()
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            var sites = client.Cypher
                                .Match("(reservacion:Reservacion)-[:UBICA]->(sitio:SitioTuristico)")
                                .With("sitio, COUNT(sitio.nombre) as cnt")
                                .OrderByDescending("cnt")
                                .Return((sitio) => new Queries
                                {
                                    sites = sitio.CollectAs<Sitio>()
                                })
                                .Limit(5)
                                .Results;

            return sites;
        }

        public IEnumerable<Queries> getAllClientsWithCommonReservation(String nombreCliente)
        {
            var client = new GraphClient(new Uri("http://localhost:7474/db/data"), "neo4j", "123");
            client.Connect();

            var clients = client.Cypher
                                .Match("(reservacion1:Reservacion{nombreCliente: {nombre}})-[:UBICA]->(sitio1:SitioTuristico),(cliente:Cliente)-[:CREA]->(reservacion2:Reservacion)-[:UBICA]->(sitio2:SitioTuristico)")
                                .WithParam("nombre", nombreCliente)
                                .Where("sitio1.nombre = sitio2.nombre")
                                .Return((cliente) => new Queries
                                {
                                    clients = cliente.CollectAs<Cliente>()
                                })
                                .Results;

            return clients;
        }
    }
}