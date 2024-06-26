﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

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
        public string Employeeid { get; set; }
        public string Status { get; set; }
        public string AddedBy { get; set; }

        public string ContactId { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Address { get; set; }
        public string VendorId { get; set; }
        public string InvoiceId { get; set; }
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
        public string AdminName { get; set; }

        public string ClientId { get; set; }

        public string PaymentStatus { get; set; }
        public string ExpectedPaymentDate { get; set; }
        public string ApproveDeclineDate { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PODate { get; set; }
        public string AddedOn { get; set; }
        public string PK_PoId { get; set; }
        public string DOB { get; set; }

        public List<Admin> lstClient { get; set; }
        public List<Admin> lstCareer { get; set; }
        public List<Admin> lstProject { get; set; }
        public List<Admin> lstVendor { get; set; }
        public List<Admin> lstContact { get; set; }
        public List<Admin> lstDashBoard { get; set; }
        public List<Admin> lstInvoice { get; set; }
        public List<Admin> VendorInvoicelst { get; set; }
        public List<Admin> lstPo { get; set; }
        public List<SelectListItem> ddlVendor { get; set; }

        public string InvoiceNo { get; set; }
        public string Remark { get; set; }
        public string PaymentDate { get; set; }
        public string BloodGroup { get; set; }
        public string Destination { get; set; }
        public string Result { get; set; }

        public string PK_VendorId { get; set; }




        public string Item { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string HSNSACNo { get; set; }
        public string CGSTSGST { get; set; }
        public string IGSTRate { get; set; }
        public string Unit { get; set; }
        public string Quantity { get; set; }
        public string UnitPrice { get; set; }
        public string TaxableTotal { get; set; }
        public string GSTValue { get; set; }
        public string TotalValue { get; set; }
        public string DeliveryDate { get; set; }
        public DataTable DtPurchaseOrderDetails { get; set; }

        public string Type { get; set; }
        public string CGST { get; set; }
        public string SGST { get; set; }
        public string IGST { get; set; }
        public string InterStatus { get; set; }
        public string GstType { get; set; }

        public string CGSTRate { get; set; }
        public string SGSTRate { get; set; }
        public string PurchaseOrderId { get; set; }
        public List<Admin> lstPurchaseorder { get; set; }
        public string PaymentTerms { get; set; }
        public string PK_PurchageOrderId { get; set; }

        public DataTable DtAcademicDetails { get; set; }
        public string Father { get; set; }
        public string CNIC { get; set; }
        public string PostalAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string Domicile { get; set; }
        public string CellNo { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Degree { get; set; }
        public string Discipline { get; set; }
        public string Semester { get; set; }
        public string CGPA { get; set; }
        public string RegistrationNo { get; set; }
        public string RollNo { get; set; }
        public string UniverSity { get; set; }
        public string FinalYearProject { get; set; }
        public string FatherMother { get; set; }
        public string PINO { get; set; }
        public string FYGRPDEPT { get; set; }
        public string ProPoSedInternShipFromDate { get; set; }
        public string ProPoSedInternShipToDate { get; set; }
        public string PassingYear { get; set; }
        public string MarksObtainedTotalMarksGPA { get; set; }
        public string Division { get; set; }
        public string Board { get; set; }
        public string Certificate { get; set; }
        public string InterShipId { get; set; }
        public string AcademicId { get; set; }
        public List<Admin> lstInterShip { get; set; }
        public List<Admin> lstAccadamic { get; set; }
        public string PostedFile { get; set; }
        public string CertificateNo { get; set; }
        

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
            SqlParameter[] para ={
                //new SqlParameter ("@CareerId",CareerId),
                                 new SqlParameter ("@Name",Name),
                                //  new SqlParameter("@Mobile",Mobile),
                                //new SqlParameter("@Email",Email),
                                //new SqlParameter("@Designation",Designation),
                                new SqlParameter("@Qualification",Qualification)
                                //new SqlParameter ("@Location",Location),
                                // new SqlParameter ("@Experience",Experience),
                                //new SqlParameter("@Resume",Image)
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
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetVendorDetails", para);
            return ds;
        }
        public DataSet GetEmployeeList()
        {
            SqlParameter[] para ={
                new SqlParameter("@EmployeeId",Employeeid),
                 new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = Connection.ExecuteQuery("GetEmployeeList", para);
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
        public DataSet DeleteEmployee()
        {
            SqlParameter[] para ={new SqlParameter ("@Employeeid",Employeeid),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteEmployee", para);
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
                                     new SqlParameter("@FromDate",FromDate),
                                      new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetClientDetailsForAdmin", para);
            return ds;

        }

        public DataSet GetClientDetailsForAdmin()
        {
            SqlParameter[] para = {new SqlParameter("@Name",Name),
                                     new SqlParameter("@FromDate",FromDate),
                                       new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = Connection.ExecuteQuery("GetClientDetailsForAdmin", para);
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
            DataSet ds = Connection.ExecuteQuery("PurcheseOrderList", para);
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

        public DataSet GetInvoiceDetails()
        {
            SqlParameter[] para = {new SqlParameter("@LoginId",LoginId),
                                     new SqlParameter("@FromDate",FromDate),
                                       new SqlParameter("@ToDate",ToDate),
                                       new SqlParameter("@Status",Status)
            };
            DataSet ds = Connection.ExecuteQuery("GetInvoiceDetails", para);
            return ds;
        }


        //public DataSet SelectInvoiceDetails()
        //{
        //    SqlParameter[] para ={
        //        new SqlParameter ("@InvoiceId",InvoiceId),
        //          new SqlParameter ("@InvoiceNo",InvoiceNo)
        //                         };
        //    DataSet ds = Connection.ExecuteQuery("SelectInvoiceDetails", para);
        //    return ds;
        //}


        public DataSet ApproveInvoice()
        {
            SqlParameter[] para ={new SqlParameter ("@InvoiceId",InvoiceId),
                                 new SqlParameter("@UpdatedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("ApproveInvoice", para);
            return ds;
        }

        public DataSet DeclineInvoice()
        {
            SqlParameter[] para ={new SqlParameter ("@InvoiceId",InvoiceId),
                                 new SqlParameter("@UpdatedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeclineInvoice", para);
            return ds;
        }

        public DataSet UpdatePaymentInvoice()
        {
            SqlParameter[] para ={new SqlParameter ("@InvoiceId",InvoiceId),
                 new SqlParameter("@PaymentDate",PaymentDate),
                  new SqlParameter("@Remarks",Remark),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("UpdatePaymentInvoice", para);
            return ds;
        }


        public DataSet ApproveEmployee()
        {
            SqlParameter[] para ={new SqlParameter ("@Employeeid",Employeeid),
                                 new SqlParameter("@UpdatedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("ApproveEmployee", para);
            return ds;
        }

        public DataSet DeleteInvoice()
        {
            SqlParameter[] para ={new SqlParameter ("@InvoiceId",InvoiceId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteInvoice", para);
            return ds;
        }

        public DataSet DeleteVendorInvoice()
        {
            SqlParameter[] para ={new SqlParameter ("@InvoiceId",InvoiceId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteVendorInvoiceByAdmin", para);
            return ds;
        }


        public DataSet PoList()
        {
            SqlParameter[] para ={new SqlParameter ("@PK_PoId",PK_PoId),
                                 new SqlParameter("@Po_Number",PONumber),
                                 new SqlParameter ("@FromDate",FromDate),
                                 new SqlParameter("@ToDate",ToDate)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetPoDetails", para);
            return ds;
        }



        public DataSet DeletePo()
        {
            SqlParameter[] para ={new SqlParameter ("@PK_PoId",PK_PoId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeletePoByAdmin", para);
            return ds;
        }

        public DataSet GetVendorName()
        {
            DataSet ds = Connection.ExecuteQuery("GetVendorName");
            return ds;
        }


        public DataSet GetAddress()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PK_VendorId",PK_VendorId)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetAddress", para);
            return ds;
        }



        public DataSet SavePurchaseOrder()
        {
            SqlParameter[] para ={
                                       new SqlParameter("@FK_VendorId",PK_VendorId),
                                       new SqlParameter("@Address", Address),
                                       new SqlParameter("@PaymentTerms",PaymentTerms),
                                       new SqlParameter("@Destination", Destination),
                                       new SqlParameter("@Type",Type),
                                       //new SqlParameter("@GstType",CGST),
                                       //new SqlParameter("@SGST",SGST),
                                       //new SqlParameter("@IGST",IGST),
                                       new SqlParameter("@PanNumber",PanNo),
                                       new SqlParameter("@AddedBy",AddedBy),
                                       new SqlParameter("@DtPurchaseOrderDetails",DtPurchaseOrderDetails)
                                 };
            DataSet ds = Connection.ExecuteQuery("SavePurchaseOrder", para);
            return ds;
        }

        public DataSet GetPurchaseOrderDetails()
        {
            SqlParameter[] para ={
                new SqlParameter ("@PurchaseOrderId",PurchaseOrderId)
                                 };

            DataSet ds = Connection.ExecuteQuery("GetPurchaseOrderDetails", para);
            return ds;
        }

        public DataSet DeletePoGenerator()
        {
            SqlParameter[] para ={new SqlParameter ("@PK_PurchageOrderId",PK_PurchageOrderId),
                                 new SqlParameter("@AddedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeletePoGenerator", para);
            return ds;
        }


        public DataSet SaveInternShip()
        {
            SqlParameter[] para ={
                         new SqlParameter("@Name",Name),
                         new SqlParameter("@Father", Father),
                         new SqlParameter("@CNIC",CNIC),
                         new SqlParameter("@DOB", DOB),
                         new SqlParameter("@PostalAddress",PostalAddress),
                         new SqlParameter("@PermanentAddress",PermanentAddress),
                         new SqlParameter("@Domicile",Domicile),
                         new SqlParameter("@CellNo",CellNo),
                         new SqlParameter("@MobileNo", Mobile),
                         new SqlParameter("@Gender",Gender),
                         new SqlParameter("@Religion", Religion),
                         new SqlParameter("@Email",Email),
                         new SqlParameter("@Degree",Degree),
                         new SqlParameter("@Discipline",Discipline),
                         new SqlParameter("@Semester",Semester),
                         new SqlParameter("@CGPA", CGPA),
                         new SqlParameter("@RegistrationNo",RegistrationNo),
                         new SqlParameter("@RollNo", RollNo),
                         new SqlParameter("@UniverSity",UniverSity),
                         new SqlParameter("@FinalYearProject",FinalYearProject),
                         new SqlParameter("@FatherMother",FatherMother),
                         new SqlParameter("@Designation",Designation),
                         new SqlParameter("@PINO", PINO),
                         new SqlParameter("@FYGRPDEPT",FYGRPDEPT),
                         new SqlParameter("@ProPoSedInternShipFromDate", ProPoSedInternShipFromDate),
                         new SqlParameter("@ProPoSedInternShipToDate",ProPoSedInternShipToDate),
                         new SqlParameter("@Date",Date),
                          new SqlParameter("@FileUpload",PostedFile),
                         new SqlParameter("@AddedBy",AddedBy),
                         new SqlParameter("@DtAcademicDetails",DtAcademicDetails)
                                 };
            DataSet ds = Connection.ExecuteQuery("SaveInternShip", para);
            return ds;
        }

        public DataSet GetInternShipList()
        {

            SqlParameter[] para ={
                new SqlParameter ("@Name",Name),
                     new SqlParameter ("@MobileNo",Mobile)
                                 };
            DataSet ds = Connection.ExecuteQuery("GetInternShipList", para);
            return ds;
        }


        public DataSet GetAcademicDetails()
        {
            SqlParameter[] para ={
                new SqlParameter ("@FK_InterShipId",InterShipId)
                                 };

            DataSet ds = Connection.ExecuteQuery("GetAcademicDetails", para);
            return ds;
        }

        public DataSet PrintCertificate()
        {
            SqlParameter[] para ={
                new SqlParameter ("@InterShipId",InterShipId)
                                 };

            DataSet ds = Connection.ExecuteQuery("PrintCertificate", para);
            return ds;
        }

        public DataSet DeleteInternShipDetails()
        {
            SqlParameter[] para ={new SqlParameter ("@PK_InterShipId",InterShipId),
                                 new SqlParameter("@DeletedBy",AddedBy)
                                 };
            DataSet ds = Connection.ExecuteQuery("DeleteInternShipDetails", para);
            return ds;
        }


        //==============================================================================================================================================================

       public DataTable dtTable { get; set; }
       public DataTable dtCareer { get; set; }
       public DataTable dtEmployee { get; set; }

        public DataSet DeleteMultipleContact()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@DeletedBy",AddedBy),
                                   new SqlParameter("@dtContact",dtTable)
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteMultipleContact", para);
            return ds;
        }
        public DataSet DeleteMultipleCareer()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@DeletedBy",AddedBy),
                                   new SqlParameter("@dtCareer",dtCareer)
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteMultipleCareer", para);
            return ds;
        }
        public DataSet DeleteMultipleEmployee()
        {
            SqlParameter[] para =
                            {
                                   new SqlParameter("@DeletedBy",AddedBy),
                                   new SqlParameter("@dtEmployee",dtEmployee)
                            };
            DataSet ds = Connection.ExecuteQuery("DeleteMultipleEmployee", para);
            return ds;
        }



    }
}