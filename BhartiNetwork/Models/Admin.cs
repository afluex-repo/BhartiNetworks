using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
namespace BhartiNetwork.Models
{
    public class Admin : Comman
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string ProjectId { get; set; }
        public string Image { get; set; }
        public string AddedBy { get; set; }

        public string ContactId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }

        public List<Admin> lstProject { get; set; }
        public List<Admin> lstContact { get; set; }
        
        public DataSet SaveProject()
        {
            SqlParameter[] para ={new SqlParameter ("@Name",Name),
                                new SqlParameter("@Date",Date),
                                new SqlParameter("@Details",Details),
                                new SqlParameter("@PostedFile",Image),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveProject", para);
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
            DataSet ds = Connection.ExecuteQuery("GetProjectDetails",para);
            return ds;
            
        }

        public DataSet DeleteProject()
        {
            SqlParameter[] para ={new SqlParameter ("@ProjectId",ProjectId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteProject", para);
            return ds;
        }

        public DataSet UpdateProject()
        {
            SqlParameter[] para ={
                 new SqlParameter ("@ProjectId",ProjectId),
                             new SqlParameter ("@Name",Name),
                                new SqlParameter("@Date",Date),
                                new SqlParameter("@Details",Details),
                                new SqlParameter("@PostedFile",Image),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("UpdateProject", para);
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

        public DataSet UpdateContact()
        {
            SqlParameter[] para ={
                                new SqlParameter ("@ContactId",ContactId),
                                new SqlParameter ("@Name",Name),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Subject",Subject),
                                new SqlParameter("@Address",Address),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("UpdateContact", para);
            return ds;
        }


        public DataSet GetContactDetails()
        {
            SqlParameter[] para ={new SqlParameter ("@ContactId",ContactId),
                                 new SqlParameter ("@Name",Name),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Subject",Subject),
                                new SqlParameter("@Address",Address)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetContactDetails", para);
            return ds;

        }


        public DataSet DeleteContact()
        {
            SqlParameter[] para ={new SqlParameter ("@ContactId",ContactId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteContact", para);
            return ds;
        }

        public DataSet GetCareerDetails()
        {
            SqlParameter[] para ={new SqlParameter ("@ContactId",ContactId),
                                 new SqlParameter ("@Name",Name),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Subject",Subject),
                                new SqlParameter("@Address",Address)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetContactDetails", para);
            return ds;

        }

    }
}