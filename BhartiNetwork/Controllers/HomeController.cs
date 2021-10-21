using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhartiNetwork.Models;
using System.Data;
using System.IO;

namespace BhartiNetwork.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Home model = new Home();
            List<Home> lstClient = new List<Home>();
            DataSet ds = model.GetClientDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    obj.ClientId = dr["PK_ClientId"].ToString();
                    obj.Date = dr["Date"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    lstClient.Add(obj);
                }
                model.lstClient = lstClient;
            }
            return View(model);
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Telecom()
        {
            return View();
        }
        public ActionResult ManpowerOutsourcing()
        {
            return View();
        }
        public ActionResult Career()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Career")]
        //[OnAction(ButtonName="")]
        public ActionResult Career(Home model,HttpPostedFileBase postedFile)
        {
            try
            {


                if (postedFile != null)
                {
                    model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.AddedBy = "1";
                DataSet ds = model.SaveCareer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Career"] = "Career save successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Career"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Career"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Career"] = ex.Message;
            }
            return RedirectToAction("Career", "Home");
 
        }


        public ActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ContactUs")]
        public ActionResult ContactUs(Home model)
        {
            try
            {
                model.AddedBy = "1";
                DataSet ds = model.SaveContact();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Contact"] = "Contact save successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Contact"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Contact"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Contact"] = ex.Message;
            }
            return RedirectToAction("ContactUs", "Home");
        }

        public ActionResult vendor()
        {
            return View();
        }

        public ActionResult newuser()
        {
            return View();
        }

        public ActionResult Project()
        {
            Home model = new Home();
            List<Home> lstProject = new List<Home>();
            DataSet ds = model.GetProjectDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Home obj = new Home();
                    obj.ProjectId = dr["PK_ProjectId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Date = dr["Date"].ToString();
                    obj.Details = dr["Details"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    lstProject.Add(obj);
                }
                model.lstProject = lstProject;
            }
            return View(model);
        }


        //public ActionResult ClientList()
        //{
        //    Home model = new Home();
        //    List<Home> lstClient = new List<Home>();
        //    DataSet ds = model.GetClientDetails();
        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Home obj = new Home();
        //            obj.ClientId = dr["PK_ClientId"].ToString();
        //            obj.Date = dr["Date"].ToString();
        //            obj.Image = dr["ImageFile"].ToString();
        //            lstClient.Add(obj);
        //        }
        //        model.lstClient = lstClient;
        //    }
        //    return View(model);
        //}


    }
}