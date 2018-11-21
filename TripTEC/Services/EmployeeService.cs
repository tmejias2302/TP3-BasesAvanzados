using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripTEC.Models;
using TripTEC.DataAccess;

namespace TripTEC.Services
{
    public class EmployeeService
    {
        public string login(loginModel model)
        {
            EmployeeDAO Dao = new EmployeeDAO();
            String result = Dao.loginEmployee(model.login, model.password);
            return result;
        }

        public bool insertEmployee(EmployeeModel model)
        {

            EmployeeDAO Dao = new EmployeeDAO();
            bool result = Dao.insertEmployee(model.login, model.clave, model.nombre);
            return result;
        }

    }
}