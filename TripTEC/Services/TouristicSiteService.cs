using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripTEC.Models;
using TripTEC.DataAccess;
using MongoDB.Bson;

namespace TripTEC.Services
{
    public class TouristicSiteService
    {
        public List<TouristicSiteModel> getTouristicSites()
        {
            TouristicSiteDAO Dao = new TouristicSiteDAO();
            List<BsonDocument> DaoResult = Dao.getAllTouristicSites();
            return buildTouristicSites(DaoResult);
        }

        public TouristicSiteModel getTouristicSiteByID(String id)
        {
            TouristicSiteDAO Dao = new TouristicSiteDAO();
            BsonDocument DaoResult = Dao.getTouristicSiteById(id);
            return buildTouristicSite(DaoResult);
        }


        private List<TouristicSiteModel> buildTouristicSites(List<BsonDocument> DaoResult)
        {
            List<TouristicSiteModel> ViewList = new List<TouristicSiteModel>();

            foreach (BsonDocument row in DaoResult)
            {
                ViewList.Add(buildTouristicSite(row));
            }
            return ViewList;
        }

        private TouristicSiteModel buildTouristicSite(BsonDocument row)
        {
            TouristicSiteModel x = new TouristicSiteModel();
            if (row.Contains("_id") && !row["_id"].IsBsonNull) { x._id = row["_id"].ToString(); }
            if (row.Contains("nombre") && !row["nombre"].IsBsonNull) { x.nombre = row["nombre"].ToString(); }
            if (row.Contains("precio") && !row["precio"].IsBsonNull) { x.precio = Convert.ToInt32(row["precio"]); }
            if (row.Contains("descripcion") && !row["descripcion"].IsBsonNull) { x.descripcion = row["descripcion"].ToString(); }
            if (row.Contains("actividades") && !row["actividades"].IsBsonNull) { x.actividades = row["actividades"].ToString(); }
            return x;

        }

        public bool insertTouristicSite(TouristicSiteModel model)
        {
            TouristicSiteDAO Dao = new TouristicSiteDAO();
            bool result = Dao.insertTouristicSite(model.nombre, model.precio, model.descripcion, model.actividades);
            return result;           
        }

        public bool insertReservation(TouristicSiteModel model, String _iduser)
        {

            ReservationDAO Dao = new ReservationDAO();
            bool result = Dao.insertReservation(model._id, _iduser, model.cantidadPersonas, model.fechaLlegada.ToString(), model.fechaSalida.ToString(), model.nombre.ToString());
            return result;

        }

        public bool updateTouristicSite(TouristicSiteModel model)
        {
            TouristicSiteDAO Dao = new TouristicSiteDAO();
            bool result = Dao.updateTouristicSite(model._id,model.nombre, model.precio, model.descripcion, model.actividades);
            return result;
        }
    }
}