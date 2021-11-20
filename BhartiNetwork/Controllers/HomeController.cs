using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BhartiNetwork.Models;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;

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
            Session.Abandon();
            return View();
        }
        [HttpPost]
        [ActionName("vendor")]
        public ActionResult VendorLogin(Admin model)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Admin Modal = new Admin();
                DataSet ds = model.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1" && ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                        FormName = "AdminDashBoard";
                        Controller = "Admin";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1" && ds.Tables[0].Rows[0]["UserType"].ToString() == "Employee")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PK_EmployeeId"] = ds.Tables[0].Rows[0]["PK_EmployeeId"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                        FormName = "EmployeeDashBoard";
                        Controller = "Employee";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1" && ds.Tables[0].Rows[0]["UserType"].ToString() == "Vendor")
                    {
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["PK_VendorId"] = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
                        Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                        FormName = "VendorDashBoard";
                        Controller = "Vendor";

                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        FormName = "Vendor";
                        Controller = "Home";
                    }

                }
                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    FormName = "Vendor";
                    Controller = "Home";
                }
            }
            catch (Exception ex)
            { 
                TempData["Login"] = ex.Message;
                FormName = "Vendor";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);

        }

        public ActionResult newuser()
        {
            Home model = new Home();
            int count1 = 0;
            List<SelectListItem> ddlOrganizationType = new List<SelectListItem>();
            DataSet ds2 = model.GetOrganizationType();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlOrganizationType.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                    }
                    ddlOrganizationType.Add(new SelectListItem { Text = r["OrganisationType"].ToString(), Value = r["PK_OrganisationTypeId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlOrganizationType = ddlOrganizationType;


            int count2 = 0;
            List<SelectListItem> ddlDesignation = new List<SelectListItem>();
            DataSet ds3 = model.GetDesignation();
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds3.Tables[0].Rows)
                {
                    if (count2 == 0)
                    {
                        ddlDesignation.Add(new SelectListItem { Text = "--Select--", Value = "0" });
                    }
                    ddlDesignation.Add(new SelectListItem { Text = r["Designation"].ToString(), Value = r["PK_DesignationId"].ToString() });
                    count2 = count2 + 1;
                }
            }
            ViewBag.ddlDesignation = ddlDesignation;



            return View();
        }

        [HttpPost]
        [ActionName("newuser")]
        public ActionResult newuser(Home model)
        {
            try
            {
                model.AddedBy = "1";
                Random rnd = new Random();
                string Pass = rnd.Next(111111, 999999).ToString();
                model.Password = Pass;
                DataSet ds = model.SaveVendor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Registration"] = "Registration save successfully";
                        
                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                mailbody = "Dear,  <br/>" + model.Name + " <br/> Your Registration successfully completed<br/> Your LoginId is :" +model.LoginId+"<br/> Password is :"+model.Password;

                                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                {
                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = true,
                                    Credentials = new NetworkCredential("developer2.afluex@gmail.com", "devel@486")
                                };
                                using (var message = new MailMessage("developer2.afluex@gmail.com", model.Email)
                                {
                                    IsBodyHtml = true,
                                    Subject = "Successfull Message",
                                    Body = mailbody
                                })
                                    smtp.Send(message);

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Registration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Registration"] = ex.Message;
            }
            return RedirectToAction("newuser", "Home");
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
                    //obj.Date = dr["Date"].ToString();
                    obj.Details = dr["Details"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    lstProject.Add(obj);
                }
                model.lstProject = lstProject;
            }
            return View(model);
        }
        public ActionResult EmployeeRegistration()
        {
            return View();
        }
        [HttpPost]
        [ActionName("EmployeeRegistration")]
        public ActionResult Registraion(Home model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../EmployeeFileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                Random rnd = new Random();
                string Pass = rnd.Next(111111, 999999).ToString();
                model.Password = Pass;
                model.DOB = string.IsNullOrEmpty(model.DOB) ? null : Comman.ConvertToSystemDate(model.DOB, "dd/MM/yyyy");
                DataSet ds = model.EmpRegistration();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["EmpRegistration"] = "Registration  successfully you will get a notification once registration will approved !!";

                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                mailbody = "Dear,  <br/>" + model.Name + " <br/> Your Registration successfully completed<br/> Your LoginId is :" + model.LoginId + "<br/> Password is :" + model.Password;

                                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient
                                {
                                    Host = "smtp.gmail.com",
                                    Port = 587,
                                    EnableSsl = true,
                                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = true,
                                    Credentials = new NetworkCredential("developer2.afluex@gmail.com", "devel@486")
                                };
                                using (var message = new MailMessage("developer2.afluex@gmail.com", model.Email)
                                {
                                    IsBodyHtml = true,
                                    Subject = "Successfull Message",
                                    Body = mailbody
                                })
                                    smtp.Send(message);

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["EmpRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["EmpRegistration"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["EmpRegistration"] = ex.Message;
            }
            return RedirectToAction("EmployeeRegistration", "Home");
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

        //public ActionResult Login()
        //{
        //    Session.Abandon();
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("Login")]
        //public ActionResult Login(Admin model)
        //{
        //    string FormName = "";
        //    string Controller = "";
        //    try
        //    {
        //        Admin Modal = new Admin();
        //        DataSet ds = model.Login();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1" && ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
        //            {
        //                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["PK_AdminId"].ToString();
        //                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

        //                FormName = "AdminDashBoard";
        //                Controller = "Admin";
        //            }
        //            else if(ds.Tables[0].Rows[0]["Msg"].ToString() == "1" && ds.Tables[0].Rows[0]["UserType"].ToString() == "Vendor")
        //            {
        //                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
        //                Session["PK_VendorId"] = ds.Tables[0].Rows[0]["PK_VendorId"].ToString();
        //                Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

        //                FormName = "VendorDashBoard";
        //                Controller = "Vendor";
        //            }
        //            else
        //            {
        //                TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //                FormName = "Login";
        //                Controller = "Home";
        //            }

        //        }
        //        else
        //        {
        //            TempData["Login"] = "Incorrect LoginId Or Password";
        //            FormName = "Login";
        //            Controller = "Home";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Login"] = ex.Message;
        //        FormName = "Login";
        //        Controller = "Home";
        //    }

        //    return RedirectToAction(FormName, Controller);

        //}
    }
}