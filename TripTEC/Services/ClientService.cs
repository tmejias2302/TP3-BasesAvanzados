using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripTEC.Models;
using TripTEC.DataAccess;
using MongoDB.Bson;

namespace TripTEC.Services
{
    public class ClientService
    {
        public List<ClientModel> getClients()
        {
            ClientDAO Dao = new ClientDAO();
            List<BsonDocument> DaoResult = Dao.getAllClients();
            return buildClients(DaoResult);
        }

        public ClientModel getClient(string _id)
        {
            ClientDAO Dao = new ClientDAO();
            BsonDocument DaoResult = Dao.getClientById(_id);
            return buildClient(DaoResult);
        }

        private List<ClientModel> buildClients(List<BsonDocument> DaoResult)
        {
            List<ClientModel> ViewList = new List<ClientModel>();

            foreach (BsonDocument row in DaoResult)
            {
                ViewList.Add(buildClient(row));
            }
            return ViewList;
        }

        private ClientModel buildClient(BsonDocument row)
        {
            ClientModel x = new ClientModel();
            if (row.Contains("cedula") && !row["cedula"].IsBsonNull) { x.cedula = row["cedula"].ToString(); }
            if (row.Contains("_id") && !row["_id"].IsBsonNull) { x._id = row["_id"].ToString(); }
            if (row.Contains("login") && !row["login"].IsBsonNull) { x.login = row["login"].ToString(); }     
            if (row.Contains("nombre") && !row["nombre"].IsBsonNull) { x.nombre = row["nombre"].ToString(); }
            if (row.Contains("apellidos") && !row["apellidos"].IsBsonNull) { x.apellidos = row["apellidos"].ToString(); }
            if (row.Contains("correo") && !row["correo"].IsBsonNull) { x.correo = row["correo"].ToString(); }
            if (row.Contains("fechaNacimiento") && !row["fechaNacimiento"].IsBsonNull) { x.fechaNacimiento = row["fechaNacimiento"].ToString(); }

            if (row.Contains("telefono") && !row["telefono"].IsBsonNull) { x.telefono = row["telefono"].ToString(); }

            return x;
        }

        public string login(loginModel model)
        {
            ClientDAO Dao = new ClientDAO();
            String result = Dao.loginClient(model.login, model.password);
            return result;


        }

        public bool insertClient(ClientModel model,string nombreArchivo)
        {

            ClientDAO Dao = new ClientDAO();
            
            bool result = Dao.insertClient(model.login, model.clave, model.nombre, model.apellidos, model.correo, model.fechaNacimiento, model.telefono,
                model.cedula);
            return result;
           

        }

        public bool editClient(ClientModel model, String _id)
        {

            ClientDAO Dao = new ClientDAO();
            bool result = Dao.updateClient(_id, model.nombre, model.apellidos, model.correo, model.fechaNacimiento, model.telefono,
                model.cedula);

            return result;


        }
    }
}