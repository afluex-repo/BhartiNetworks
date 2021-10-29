using BhartiNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
            Vendor model = new Vendor();
            model.VendorId = Session["PK_VendorId"].ToString();
            DataSet ds = model.GetVendorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.VendorId = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Password = ds.Tables[0].Rows[0]["Password"].ToString(); 
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
                ViewBag.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.OrganizationName = ds.Tables[0].Rows[0]["OrganizationName"].ToString();
                ViewBag.OrganizationType = ds.Tables[0].Rows[0]["OrganisationType"].ToString();
            }
            return View(model);
        }


    }
}