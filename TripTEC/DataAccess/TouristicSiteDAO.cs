using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripTEC.DataAccess
{
    public class TouristicSiteDAO
    {
        DataAcess context;
       
        public TouristicSiteDAO()
        {
            this.context = new DataAcess();
           
        }

        public List<BsonDocument> getAllTouristicSites()
        {
            
            return this.context.getAllFromCollection("SitiosTuristicos");
        }

        public BsonDocument getTouristicSiteById(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            List<BsonDocument> a = this.context.getAllFromCollectionWithFilter("SitiosTuristicos", filter);
            return a.ElementAt(0);
        }

        public bool insertTouristicSite(string nombre, int precio, string descripcion, string actividades)
        {
            BsonDocument touristicSite = new BsonDocument {
                { "nombre", nombre},
                { "precio", precio},
                { "descripcion", descripcion},
                { "actividades", actividades}
            };

            return this.context.insertCollection(touristicSite, "SitiosTuristicos");
        }

        public bool updateTouristicSite(string id, string nombre, int precio, string descripcion, string actividades)
        {
            var update = Builders<BsonDocument>.Update
                .Set("nombre", nombre)
                .Set("precio", precio)
                .Set("descripcion", descripcion)
                .Set("actividades", actividades);

            this.context.updateCollection(id, "SitiosTuristicos", update);
            return true;
        }

        public List<BsonDocument> getReservationsPerClient(string clientId)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("clienteId", clientId);
            List<BsonDocument> reservaciones = this.context.getAllFromCollectionWithFilter("Reservaciones", filter);
            return reservaciones;
        }


    }
}
