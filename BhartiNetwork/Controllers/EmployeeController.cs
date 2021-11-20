using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhartiNetwork.Models;
using System.Data;

namespace BhartiNetwork.Controllers
{
    public class EmployeeController : EmployeebaseController
    {
        // GET: Employee
        public ActionResult EmployeeDashboard()
        {
           

            Employee model = new Employee();
            model.EmployeeId = Session["PK_EmployeeId"].ToString();
            DataSet ds = model.GetEmployeeDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.VendorId = ds.Tables[0].Rows[0]["PK_EmployeeId"].ToString();
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.Image = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                //ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.BloodGroup = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                ViewBag.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
            }
            return View(model);



        }
        public ActionResult EmployeeIdCards(Employee model)
        {
            
            model.EmployeeId = Session["PK_EmployeeId"].ToString();
            DataSet ds = model.GetEmployeeDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.BloodGroup = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                ViewBag.ApproveDeclineDate = ds.Tables[0].Rows[0]["ApproveDeclineDate"].ToString();
                ViewBag.ExpiaryDate = ds.Tables[0].Rows[0]["ExpiaryDate"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Image = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
            }
            return View(model);
        }
    }
}