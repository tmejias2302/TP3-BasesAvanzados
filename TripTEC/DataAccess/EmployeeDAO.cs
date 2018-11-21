
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace TripTEC.DataAccess
{
    public class EmployeeDAO
    {
        DataAcess context;

        public EmployeeDAO()
        {
            this.context = new DataAcess();
        }

        public string loginEmployee(string login, string clave)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("login", login) & builder.Eq("clave", clave);
            List<BsonDocument> loginList = this.context.getAllFromCollectionWithFilter("Empleado", filter);
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
        public List<BsonDocument> getAllEmployee()
        {
            return this.context.getAllFromCollection("Empleado");
        }

        public BsonDocument getEmployeeById(string id)
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            List<BsonDocument> a = this.context.getAllFromCollectionWithFilter("Empleado", filter);
            return a.ElementAt(0);
        }

        public bool insertEmployee(string login, string clave, string nombre)
        {

            BsonDocument employee = new BsonDocument {
                { "login", login},
                {"clave",clave },
                { "nombre", nombre}
            };

            return this.context.insertCollection(employee, "Empleado");
        }
    }
}
