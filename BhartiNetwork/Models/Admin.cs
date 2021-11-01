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
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }
        public string ProjectId { get; set; }
        public string Image { get; set; }
        public string file { get; set; }
        public string PONumber { get; set; }
        

        public string AddedBy { get; set; }

        public string ContactId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public string VendorId { get; set; }
        public string CareerId { get; set; }
        public string Mobile { get; set; }
        public string Designation { get; set; }
        public string Qualification { get; set; }
        public string Location { get; set; }
        public string Experience { get; set; }
        public string Resume { get; set; }
        public string Circle { get; set; }
        public string OrganizationName { get; set; }
        public string StartingofOrganization { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string Deposit { get; set; }
        public string OrganizationType { get; set; }
        public string PanNo { get; set; }
        public string GSTNo { get; set; }

        public string ClientId { get; set; }

        public List<Admin> lstClient { get; set; }
        public List<Admin> lstCareer { get; set; }
        public List<Admin> lstProject { get; set; }
        public List<Admin> lstVendor { get; set; }
        public List<Admin> lstContact { get; set; }
        public List<Admin> lstDashBoard { get; set; }


        public DataSet SaveProject()
        {
            SqlParameter[] para ={new SqlParameter ("@Name",Name),
                                //new SqlParameter("@Date",Date),
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
                                //new SqlParameter("@Date",Date),
                                new SqlParameter("@Details",Details),
                                new SqlParameter("@PostedFile",Image)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetProjectDetails", para);
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
                                //new SqlParameter("@Date",Date),
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
            SqlParameter[] para ={new SqlParameter ("@CareerId",CareerId),
                                 new SqlParameter ("@Name",Name),
                                  new SqlParameter("@Mobile",Mobile),
                                new SqlParameter("@Email",Email),
                                new SqlParameter("@Designation",Designation),
                                new SqlParameter("@Qualification",Qualification),
                                new SqlParameter ("@Location",Location),
                                 new SqlParameter ("@Experience",Experience),
                                new SqlParameter("@Resume",Image)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetCareerDetails", para);
            return ds;

        }


        public DataSet DeleteCareer()
        {
            SqlParameter[] para ={new SqlParameter ("@CareerId",CareerId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteCareer", para);
            return ds;
        }

        public DataSet SaveClient()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Name",Name),
                new SqlParameter ("@PostedFile",Image),
                                new SqlParameter("@Date",Date),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveClient", para);
            return ds;
        }

        public DataSet UpdateClient()
        {
            SqlParameter[] para ={
                new SqlParameter ("@ClientId",ClientId),
                new SqlParameter ("@Name",Name),
                new SqlParameter ("@PostedFile",Image),
                                new SqlParameter("@Date",Date),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("UpdateClient", para);
            return ds;
        }

        public DataSet GetVendorDetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@VendorId",VendorId)
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorDetails", para);
            return ds;
        }

        public DataSet DeleteVendor()
        {
            SqlParameter[] para ={new SqlParameter ("@VendorId",VendorId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteVendor", para);
            return ds;
        }


        public DataSet GetDetailsOfDashBoard()
        {
            DataSet ds = Connection.ExecuteQuery("GetDetailsOfDashBoard");
            return ds;
        }

        public DataSet Login()
        {
            SqlParameter[] para = { new SqlParameter("@LoginID",LoginId),
                                         new SqlParameter("@Password",Password)

            };
            DataSet ds = Connection.ExecuteQuery("Login", para);
            return ds;
        }


        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",AddedBy)
            };
            DataSet ds = Connection.ExecuteQuery("ChangePasswordForAdmin", para);
            return ds;

        }


        public DataSet GetClientDetails()
        {
            SqlParameter[] para = {new SqlParameter("@Name",Name),
                                   new SqlParameter("@ClientId",ClientId),
                                    new SqlParameter("@PostedFile",Image),
                                     new SqlParameter("@Date",Date)
            };
            DataSet ds = Connection.ExecuteQuery("GetClientDetails", para);
            return ds;

        }


        public DataSet ClientDelete()
        {
            SqlParameter[] para ={new SqlParameter ("@ClientId",ClientId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("ClientDelete", para);
            return ds;
        }


        public DataSet AproveVendor()
        {
            SqlParameter[] para ={new SqlParameter ("@FK_VendorId",VendorId),
                                 new SqlParameter("@UpdatedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("ApproveVendor", para);
            return ds;
        }

        public DataSet DeclineVendor()
        {
            SqlParameter[] para ={new SqlParameter ("@FK_VendorId",VendorId),
                                 new SqlParameter("@UpdatedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeclinedVendor", para);
            return ds;
        }


        public DataSet PurcheseOrderList()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                 new SqlParameter("@Name",Name)
                                 };
            DataSet ds = Connection.ExecuteQuery("PurcheseOrderList",para);
            return ds;
        }



        public DataSet UploadVendorFile()
        {
            SqlParameter[] para ={
                new SqlParameter ("@VendorId",VendorId),
                 new SqlParameter ("@PoNumber",PONumber),
                new SqlParameter ("@PostedFile",file),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("UploadVendorFile", para);
            return ds;

            
        }
    }
}