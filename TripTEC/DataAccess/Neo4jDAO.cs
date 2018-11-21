using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Neo4jClient;

namespace TripTEC.DataAccess
{
    public class Cliente
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string cedula { get; set; }
    }

    public class Reservacion
    {
        public string id { get; set; }
        public int cantidadPersonas { get; set; }
        public string fechaLlegada { get; set; }
        public string fechaSalida { get; set; }
        public string nombreCliente { get; set; }
        public string nombreSitio { get; set; }
    }

    public class Sitio
    {
        public string id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
    }

    public class Queries
    {
        public IEnumerable<Reservacion> reservations { get; set; }
        public IEnumerable<Sitio> sites { get; set; }
        public IEnumerable<Cliente> clients { get; set; }
    }

    public class Neo4jDAO
    {
        DataAcess context;

        public Neo4jDAO()
        {
            this.context = new DataAcess();
        }

        public List<Reservacion> getReservationsByName(string name)
        {
            IEnumerable<Queries> queriesEnumerable = this.context.getAllReservationsByClient(name);

            List<Reservacion> reservaciones = new List<Reservacion>();
            foreach (var reservationsEnumerable in queriesEnumerable)
            {
                IEnumerable<Reservacion> reservations = reservationsEnumerable.reservations;
                foreach (Reservacion reservacion in reservations)
                {
                    reservaciones.Add(reservacion);
                }
                
            }
            return reservaciones;
        }

        public List<Sitio> getAllSites()
        {
            IEnumerable<Queries> queriesEnumerable = this.context.getAllSites();

            List<Sitio> sitios = new List<Sitio>();
            foreach (var sitesEnumerable in queriesEnumerable)
            {
                IEnumerable<Sitio> sites = sitesEnumerable.sites;
                foreach (Sitio sitio in sites)
                {
                    sitios.Add(sitio);
                }

            }
            return sitios;
        }

        public List<Sitio> getMostVisitedSites()
        {
            IEnumerable<Queries> queriesEnumerable = this.context.getMostVisitedSites();

            List<Sitio> sitios = new List<Sitio>();
            foreach (var sitesEnumerable in queriesEnumerable)
            {
                IEnumerable<Sitio> sites = sitesEnumerable.sites;
                foreach (Sitio sitio in sites)
                {
                    sitios.Add(sitio);
                }

            }
            return sitios;
        }

        public List<Cliente> getAllClientsWithCommonReservationByName(string name)
        {
            IEnumerable<Queries> queriesEnumerable = this.context.getAllClientsWithCommonReservation(name);

            List<Cliente> clientes = new List<Cliente>();
            foreach (var clientsEnumerable in queriesEnumerable)
            {
                IEnumerable<Cliente> clients = clientsEnumerable.clients;
                foreach (Cliente cliente in clients)
                {
                    clientes.Add(cliente);
                }

            }
            return clientes;
        }
    }
}