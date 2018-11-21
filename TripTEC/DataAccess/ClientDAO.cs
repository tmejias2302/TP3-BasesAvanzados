using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
namespace TripTEC.DataAccess
{
    public class ClientDAO
    {
        DataAcess context;

        public ClientDAO()
        {
            this.context = new DataAcess();
        }

        public BsonDocument getClientById(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            List<BsonDocument> a = this.context.getAllFromCollectionWithFilter("Clientes", filter);
            return a.ElementAt(0);
        }

        public List<BsonDocument> getAllClients()
        {
            return this.context.getAllFromCollection("Clientes");
        }

        public string loginClient(string login, string clave)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("login", login) & builder.Eq("clave", clave);
            List<BsonDocument> loginList = this.context.getAllFromCollectionWithFilter("Clientes", filter);
            if (loginList.Count >= 1)
            {
                BsonDocument user = loginList[0];
                return user["_id"].ToString();
            }
            else
            {
                return "-1";
            }

        }

        public bool insertClient(string login, string clave, string nombre, string apellidos, string correo, string fechaNacimiento
           , string telefono, string cedula)
        {
            BsonDocument cliente = new BsonDocument {
                { "login", login},
                {"clave",clave },
                { "nombre", nombre},
                { "apellidos", apellidos},
                { "correo", correo},
                { "fechaNacimiento", fechaNacimiento},
                { "telefono", telefono},
                { "cedula", cedula }
            };
            return this.context.insertCollection(cliente, "Clientes");

        }

        public bool updateClient(string id, string nombre, string apellidos, string correo, string fechaNacimiento
           , string telefono, string cedula)
        {

            var update = Builders<BsonDocument>.Update
                .Set("nombre", nombre)
                .Set("apellidos", apellidos)
                .Set("correo", correo)
                .Set("fechaNacimiento", fechaNacimiento)
                .Set("telefono", telefono)
                .Set("cedula", cedula);

            this.context.updateCollection(id, "Clientes", update);
            return true;
        }

    }
}