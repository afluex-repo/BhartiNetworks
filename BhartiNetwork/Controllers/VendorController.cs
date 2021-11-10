using BhartiNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BhartiNetwork.Controllers
{
    public class VendorController : VendorBaseController
    {
        // GET: Vendor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VendorDashBoard()
        {
            //Vendor model = new Vendor();
            //model.VendorId = Session["PK_VendorId"].ToString();
            //DataSet ds = model.GetVendorDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ViewBag.VendorId = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
            //    ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
            //    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
            //    ViewBag.Password = ds.Tables[0].Rows[0]["Password"].ToString(); 
            //}
            //return View(model);

            Vendor model = new Vendor();
            model.VendorId = Session["PK_VendorId"].ToString();
            DataSet ds = model.GetVendorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.VendorId = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Circle = ds.Tables[0].Rows[0]["Circle"].ToString();
                ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                ViewBag.OrganizationType = ds.Tables[0].Rows[0]["OrganisationType"].ToString();
            }
            return View(model);



        }

        public ActionResult Profile()
        {

            Vendor model = new Vendor();
            model.VendorId = Session["PK_VendorId"].ToString();
            DataSet ds = model.GetVendorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.VendorId = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Circle = ds.Tables[0].Rows[0]["Circle"].ToString();
                ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                ViewBag.OrganizationType = ds.Tables[0].Rows[0]["OrganisationType"].ToString();
            }
            return View(model);
        }


        public ActionResult PurcheseOrder()
        {
            Vendor model = new Vendor();
            List<Vendor> lstVendorPOList = new List<Vendor>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetVendorPODetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Vendor obj = new Vendor();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.VendorId = dr["PONo"].ToString();
                    obj.file = dr["UploadFile"].ToString();
                    obj.Date = dr["PODate"].ToString();
                    lstVendorPOList.Add(obj);
                }
                model.lstVendorPOList = lstVendorPOList;
            }
            return View(model);
        }

        public ActionResult Invoice()
        {
            Vendor model = new Vendor();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.SelectInvoce();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                    Vendor obj = new Vendor();
                    ViewBag.Status = ds.Tables[0].Rows[0]["Status"].ToString();
                ViewBag.PaymentStatus = ds.Tables[0].Rows[0]["PaymentStatus"].ToString();
                ViewBag.ExpectedPaymentDate = ds.Tables[0].Rows[0]["ExpectedPaymentDate"].ToString();
            }
            
            List<Vendor> Invoicelst = new List<Vendor>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds1 = model.SelectInvoiceDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    Vendor obj = new Vendor();
                    obj.InvoiceId = dr["PK_InvoiceId"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    obj.Status = dr["Status"].ToString();
                    obj.PaymentStatus = dr["PaymentStatus"].ToString();
                    obj.ExpectedPaymentDate = dr["ExpectedPaymentDate"].ToString();
                    obj.PaymentDate = dr["PaymentDate"].ToString();
                    Invoicelst.Add(obj);
                }
                model.Invoicelst = Invoicelst;
            }


            List<SelectListItem> ddlPONumber = new List<SelectListItem>();
            DataSet ds2 = model.GetPoNumber();

            if (ds2 != null && ds2.Tables.Count > 0)
            {
                int count1 = 0;
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlPONumber.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPONumber.Add(new SelectListItem { Text = r["PONumber"].ToString(), Value = r["PK_VendorId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlPONumber = ddlPONumber;
            return View(model);
            
            //List<SelectListItem> ddlStatus = Status();
            //ViewBag.ddlStatus = ddlStatus;

            //List<SelectListItem> ddlPaymentStatus = PaymentStatus();
            //ViewBag.ddlPaymentStatus = ddlPaymentStatus;

            //List<SelectListItem> ddlExpectedPaymentDate = ExpectedPaymentDate();
            //ViewBag.ddlExpectedPaymentDate = ddlExpectedPaymentDate;
        
        }

        [HttpPost]
        [ActionName("Invoice")]
        public ActionResult Invoice(Vendor model, HttpPostedFileBase postedFile)
        {
            try
            {
                    if (postedFile != null)
                    {
                        model.Image = "../FileUploadInvoice/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                    //obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                    model.AddedBy = Session["PK_VendorId"].ToString();
                    DataSet ds = model.SaveInvoice();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Invoice"] = "Invoice save successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    //List<SelectListItem> ddlStatus = Status();
                    //ViewBag.ddlStatus = ddlStatus;

                    //List<SelectListItem> ddlPaymentStatus = PaymentStatus();
                    //ViewBag.ddlPaymentStatus = ddlPaymentStatus;

                    //List<SelectListItem> ddlExpectedPaymentDate = ExpectedPaymentDate();
                    //ViewBag.ddlExpectedPaymentDate = ddlExpectedPaymentDate;
                }
                    else
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            catch (Exception ex)
            {
                TempData["Invoice"] = ex.Message;
            }
            return RedirectToAction("Invoice", "Vendor");

        }

        //public static List<SelectListItem> Status()
        //{
        //    List<SelectListItem> Status = new List<SelectListItem>();
        //    //Registry.Add(new SelectListItem { Text = "Select", Value = "0" });
        //    Status.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
        //    return Status;
        //}

        //public static List<SelectListItem> PaymentStatus()
        //{
        //    List<SelectListItem> PaymentStatus = new List<SelectListItem>();
        //    //Agreement.Add(new SelectListItem { Text = "Select", Value = "0" });
        //    PaymentStatus.Add(new SelectListItem { Text = "Pending", Value = "Pending" });
        //    return PaymentStatus;
        //}

        //public static List<SelectListItem> ExpectedPaymentDate()
        //{
        //    List<SelectListItem> ExpectedPaymentDate = new List<SelectListItem>();
        //    //Notary.Add(new SelectListItem { Text = "Select", Value = "0" });
        //    ExpectedPaymentDate.Add(new SelectListItem { Text = "N/A", Value = "N/A" });
        //    return ExpectedPaymentDate;
        //}



    }
}