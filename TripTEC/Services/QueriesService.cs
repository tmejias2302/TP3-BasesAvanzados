using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripTEC.Models;
using TripTEC.DataAccess;
using MongoDB.Bson;

namespace TripTEC.Services
{
    public class QueriesService
    {
        public List<ReservationModel> getReservationsPerClient(String nombreCliente)
        {
            Neo4jDAO Dao = new Neo4jDAO();
            List<Reservacion> DaoResult = Dao.getReservationsByName(nombreCliente);
            return buildNeo4jReservations(DaoResult);
        }

        public List<TouristicSiteModel> getTouristicSites()
        {
            Neo4jDAO Dao = new Neo4jDAO();
            List<Sitio> DaoResult = Dao.getAllSites();
            return buildNeo4jSites(DaoResult);
        }

        public List<TouristicSiteModel> getMostVisitedSites()
        {
            Neo4jDAO Dao = new Neo4jDAO();
            List<Sitio> DaoResult = Dao.getMostVisitedSites();
            return buildNeo4jSites(DaoResult);
        }

        public List<ClientModel> getCommonClients(String nombreCliente)
        {
            Neo4jDAO Dao = new Neo4jDAO();
            List<Cliente> DaoResult = Dao.getAllClientsWithCommonReservationByName(nombreCliente);
            return buildNeo4jClients(DaoResult);
        }

        public void makeMigration()
        {
            DataAcess context = new DataAcess();
            context.migrateMongoDBToNeo4j();
        }

        private List<ReservationModel> buildNeo4jReservations(List<Reservacion> DaoResult)
        {
            List<ReservationModel> ViewList = new List<ReservationModel>();

            foreach (Reservacion reservacion in DaoResult)
            {
                ViewList.Add(buildNeo4jReservation(reservacion));
            }
            return ViewList;
        }

        private ReservationModel buildNeo4jReservation(Reservacion reservacion)
        {
            ReservationModel x = new ReservationModel();
            x.reservacionId = reservacion.id;
            x.nombreCliente = reservacion.nombreCliente;
            x.cantidadPersonas = reservacion.cantidadPersonas;
            x.fechaLlegada = reservacion.fechaLlegada;
            x.fechaSalida = reservacion.fechaSalida;
            x.sitioTuristico = reservacion.nombreSitio;
            return x;
        }

        private List<TouristicSiteModel> buildNeo4jSites(List<Sitio> DaoResult)
        {
            List<TouristicSiteModel> ViewList = new List<TouristicSiteModel>();

            foreach (Sitio sitio in DaoResult)
            {
                ViewList.Add(buildNeo4jTouristicSite(sitio));
            }
            return ViewList;
        }

        private TouristicSiteModel buildNeo4jTouristicSite(Sitio sitio)
        {
            TouristicSiteModel x = new TouristicSiteModel();
            x.nombre = sitio.nombre;
            x.descripcion = sitio.descripcion;
            return x;
        }

        private List<ClientModel> buildNeo4jClients(List<Cliente> DaoResult)
        {
            List<ClientModel> ViewList = new List<ClientModel>();

            foreach (Cliente cliente in DaoResult)
            {
                ViewList.Add(buildNeo4jClient(cliente));
            }
            return ViewList;
        }

        private ClientModel buildNeo4jClient(Cliente cliente)
        {
            ClientModel x = new ClientModel();
            x.nombre = cliente.nombre;
            x.cedula = cliente.cedula;
            return x;
        }
    }
}