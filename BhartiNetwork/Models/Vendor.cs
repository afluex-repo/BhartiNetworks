using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhartiNetwork.Models
{
    public class Vendor
    {
        public string VendorId { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string Details { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string Image { get; set; }
        public List<Vendor> lstVendor { get; set; }
        public List<Vendor> lstVendorPOList { get; set; }
        public List<Vendor> Invoicelst { get; set; }
        public string ContactId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
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
        public string file { get; set; }

        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string ExpectedPaymentDate { get; set; }
        public string AddedBy { get; set; }
        public string InvoiceId { get; set; }
        public string PaymentDate { get; set; }
        
         public List<SelectListItem> ddlPONumber { get; set; }




        public DataSet GetVendorDetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@VendorId",VendorId)
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorDetails",para);
            return ds;
        }

        public DataSet GetVendorPODetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorPODetails",para);
            return ds;
        }

        public DataSet SaveInvoice()
        {
            SqlParameter[] para ={
                new SqlParameter ("@ImageFile",Image),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveInvoice", para);
            return ds;
        }

        public DataSet SelectInvoce()
        {
            DataSet ds = Connection.ExecuteQuery("SelectInvoce");
            return ds;
        }
        public DataSet SelectInvoiceDetails()
        {
            SqlParameter[] para ={
                new SqlParameter ("@InvoiceId",InvoiceId)
                                 };
            DataSet ds = Connection.ExecuteQuery("SelectInvoiceDetails",para);
            return ds;
        }

        public DataSet GetPoNumber()
        {
            DataSet ds = Connection.ExecuteQuery("GetPoNumber");
            return ds;
        }

    }

}