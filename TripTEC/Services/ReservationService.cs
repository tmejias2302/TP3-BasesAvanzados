using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripTEC.Models;
using TripTEC.DataAccess;
using MongoDB.Bson;

namespace TripTEC.Services
{
    public class ReservationService
    {
        public List<ReservationModel> getReservations(String idCliente)
        {
            ReservationDAO Dao = new ReservationDAO();
            List<BsonDocument> DaoResult = Dao.getReservationsById(idCliente);
            return buildReservations(DaoResult);
        }

        private List<ReservationModel> buildReservations(List<BsonDocument> DaoResult)
        {
            List<ReservationModel> ViewList = new List<ReservationModel>();

            foreach (BsonDocument row in DaoResult)
            {
                ViewList.Add(buildReservation(row));
            }
            return ViewList;
        }

        private ReservationModel buildReservation(BsonDocument row)
        {
            ReservationModel x = new ReservationModel();
            if (row.Contains("reservationId") && !row["reservationId"].IsBsonNull) { x.reservacionId = row["reservationId"].ToString(); }
            if (row.Contains("clientId") && !row["clientId"].IsBsonNull) { x.clienteId = row["clientId"].ToString(); }
            if (row.Contains("nombreCliente") && !row["nombreCliente"].IsBsonNull) { x.nombreCliente = row["nombreCliente"].ToString(); }
            if (row.Contains("cantidadPersonas") && !row["cantidadPersonas"].IsBsonNull) { x.cantidadPersonas = Convert.ToInt32(row["cantidadPersonas"]); }
            if (row.Contains("fechaLlegada") && !row["fechaLlegada"].IsBsonNull) { x.fechaLlegada = row["fechaLlegada"].ToString(); }
            if (row.Contains("fechaSalida") && !row["fechaSalida"].IsBsonNull) { x.fechaSalida = row["fechaSalida"].ToString(); }
            if (row.Contains("sitioTuristico") && !row["sitioTuristico"].IsBsonNull) { x.sitioTuristico = row["sitioTuristico"].ToString(); }
            return x;
        }


    }
}