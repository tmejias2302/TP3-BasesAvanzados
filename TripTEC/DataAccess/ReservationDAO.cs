using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace TripTEC.DataAccess
{
    public class ReservationDAO
    {
        DataAcess context;

        public ReservationDAO()
        {
            this.context = new DataAcess();
        }

        public List<BsonDocument> getAllReservations()
        {
            return this.context.getAllFromCollection("Reservaciones");
        }

        public List<BsonDocument> getReservationsById(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("clientId", id);
            List<BsonDocument> events = this.context.getAllFromCollectionWithFilter("Reservaciones", filter);
            return events;
        }

        public bool insertReservation(string reservationId,string clientId, int cantidadPersonas, string fechaLlegada, string fechaSalida, string sitioTuristico)
        {
            // Get client name
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(clientId));
            List<BsonDocument> a = this.context.getAllFromCollectionWithFilter("Clientes", filter);
            var clientName = a.ElementAt(0)["nombre"].ToString();

            BsonDocument reservacion = new BsonDocument {
                { "reservationId", reservationId},
                { "clientId", clientId},
                { "cantidadPersonas", cantidadPersonas},
                { "fechaLlegada", fechaLlegada},
                { "fechaSalida", fechaSalida},
                { "sitioTuristico", sitioTuristico},
                { "nombreCliente", clientName}
            };

            return this.context.insertCollection(reservacion, "Reservaciones");
        }
    }
}
