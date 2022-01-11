using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public string ClientId { get; set; }
        public string FatherName { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string BloodGroup { get; set; }

        public string OrganizationName { get; set; }
        public string ServicesOffered { get; set; }
        public string OrganizationType { get; set; }
        public string StartingofOrganization { get; set; }
        public string Circle { get; set; }
        public string PK_InvoiceId { get; set; }

        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string Deposit { get; set; }
        public string PanNo { get; set; }
        public string GSTNo { get; set; }

        public string PK_OrganisationTypeId { get; set; }
        public string PK_DesignationId { get; set; }
        public string FK_OrganisationTypeId { get; set; }
        public string FK_DesignationId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryCode { get; set; }


        public List<Home> lstClient { get; set; }
        public List<Home> lstProject { get; set; }
        public List<SelectListItem> ddlOrganizationType { get; set; }
        public List<SelectListItem> ddlDesignation { get; set; }
        public List<SelectListItem> ddlCountryCode { get; set; }

        

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
                                //new SqlParameter("@Date",Date),
                                new SqlParameter("@Details",Details),
                                new SqlParameter("@PostedFile",Image)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetProjectDetails", para);
            return ds;

        }

        public DataSet GetClientDetails()
        {
        //    SqlParameter[] para ={new SqlParameter ("@ClientId",ClientId),
        //                        new SqlParameter("@Date",Date),
        //                        new SqlParameter("@PostedFile",Image)
        //                         };
            DataSet ds = Connection.ExecuteQuery("GetClientDetails");
            return ds;

        }

        public DataSet GetOrganizationType()
        {
            DataSet ds = Connection.ExecuteQuery("GetOrganizationType");
            return ds;

        }
        public DataSet GetDesignation()
        {
            DataSet ds = Connection.ExecuteQuery("GetDesignation");
            return ds;

        }
        public DataSet SaveVendor()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Password",Password),
                new SqlParameter ("@Name",Name),
                                new SqlParameter("@Mobile",Mobile),
                                  new SqlParameter("@Email",Email),
                                     new SqlParameter ("@Address",Address),
                                new SqlParameter("@Circle",Circle),
                                  new SqlParameter("@OrganizationName",OrganizationName),
                                new SqlParameter("@OrganizationStarting",StartingofOrganization),
                                new SqlParameter("@AccountNo",AccountNo),
                                new SqlParameter("@Branch",Branch),
                                   new SqlParameter("@Deposit",Deposit),
                                new SqlParameter("@FK_OrganisationTypeId",PK_OrganisationTypeId),
                                new SqlParameter("@PanNumber",PanNo),
                                 new SqlParameter("@GSTNo",GSTNo),
                                 new SqlParameter("@FK_DesignationId",PK_DesignationId),
                                 new SqlParameter("@FK_InvoiceId",PK_InvoiceId),
                                   new SqlParameter("@VendorFile",Image),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveVendor", para);
            return ds;
        }
        public DataSet EmpRegistration()
        {
            SqlParameter[] para ={
                new SqlParameter ("@Password",Password),
                new SqlParameter ("@Name",Name),
                new SqlParameter("@Mobile",Mobile),
                new SqlParameter("@Email",Email),
                new SqlParameter ("@Address",Address),
                new SqlParameter("@FatherName",FatherName),
                new SqlParameter("@Gender",Gender),
                new SqlParameter("@DOB",DOB),
                new SqlParameter("@BloodGroup",BloodGroup),
                new SqlParameter("@Designation",Designation),
                 new SqlParameter("@PostedFile",Image)
                                 };
            DataSet ds = Connection.ExecuteQuery("EmployeeRegistration", para);
            return ds;
        }



        public DataSet GetCountryCode()
        {
            DataSet ds = Connection.ExecuteQuery("GetCountryCode");
            return ds;
        }
    }
}