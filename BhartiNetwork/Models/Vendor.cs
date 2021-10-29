using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

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

        public DataSet GetVendorDetails()
        {
            SqlParameter[] para ={
                new SqlParameter("@VendorId",VendorId)
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorDetails",para);
            return ds;
        }
    }

}