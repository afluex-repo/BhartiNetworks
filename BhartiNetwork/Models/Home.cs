using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BhartiNetwork.Models
{
    public class Home
    {
        public string CareerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public string Qualification { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
        public string Image { get; set; }
        public string Address { get; set; }
        public string AddedBy { get; set; }
        public string Subject { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string ProjectId { get; set; }
        

        public List<Home> lstProject { get; set; }
        

        public DataSet SaveCareer()
        {
            SqlParameter[] para ={new SqlParameter ("@Name",Name),
                                new SqlParameter("@Mobile",Mobile),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Designation",Designation),
                                new SqlParameter("@Qualification",Qualification),
                                new SqlParameter("@Location",Location),
                                new SqlParameter("@Experience",Experience),
                                new SqlParameter("@Resume",Image),
                                new SqlParameter("@Address",Address),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveCareer", para);
            return ds;
        }


        public DataSet SaveContact()
        {
            SqlParameter[] para ={new SqlParameter ("@Name",Name),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Subject",Subject),
                                new SqlParameter("@Address",Address),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveContact", para);
            return ds;
        }


        public DataSet GetProjectDetails()
        {
            SqlParameter[] para ={new SqlParameter ("@ProjectId",ProjectId),
                                 new SqlParameter ("@Name",Name),
                                new SqlParameter("@Date",Date),
                                new SqlParameter("@Details",Details),
                                new SqlParameter("@PostedFile",Image)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetProjectDetails", para);
            return ds;

        }
    }
}