using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace BhartiNetwork.Models
{
    public class Employee
    {
        public string EmployeeId { get; set; }

        public DataSet GetEmployeeDetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@EmployeeId",EmployeeId)
            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeList", para);
            return ds;
        }
    }
}