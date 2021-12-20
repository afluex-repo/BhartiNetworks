using BhartiNetwork.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace BhartiNetwork.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult AdminDashBoard()
        {
            Admin model = new Admin();
            List<Admin> lstVendor = new List<Admin>();
            DataSet ds = model.GetVendorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.VendorId = dr["PK_VendorId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Mobile = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Circle = dr["Circle"].ToString();
                    obj.OrganizationName = dr["OrganizationName"].ToString();
                    obj.StartingofOrganization = dr["OrganizationStarting"].ToString();
                    obj.AccountNo = dr["AccountNo"].ToString();
                    obj.Branch = dr["Branch"].ToString();
                    obj.Deposit = dr["Deposit"].ToString();
                    obj.OrganizationType = dr["OrganisationType"].ToString();
                    obj.PanNo = dr["PanNumber"].ToString();
                    obj.GSTNo = dr["GSTNo"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    lstVendor.Add(obj);
                }
                model.lstVendor = lstVendor;

                ViewBag.TotalVendor = double.Parse(ds.Tables[0].Compute("sum(PK_VendorId)", "").ToString()).ToString();
            }

            //return View(model);

            //Admin model = new Admin();
            List<Admin> lstContact = new List<Admin>();
            DataSet ds1 = model.GetContactDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.ContactId = dr["PK_ContactId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Subject = dr["Subject"].ToString();
                    obj.Address = dr["Address"].ToString();
                    lstContact.Add(obj);
                }
                model.lstContact = lstContact;
            }

            List<Admin> lstDashBoard = new List<Admin>();
            DataSet ds2 = model.GetDetailsOfDashBoard();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds2.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    ViewBag.TotalProjects = dr["TotalProjects"].ToString();
                    ViewBag.TotalVendors = dr["TotalVendors"].ToString();
                    ViewBag.TotalEnquiry = dr["TotalEnquiry"].ToString();
                    lstDashBoard.Add(obj);
                }
                model.lstDashBoard = lstDashBoard;
            }
            return View(model);
        }



        public ActionResult Project(string Id)
        {
            Admin model = new Admin();
            if (Id != null)
            {
                model.ProjectId = Id;
                DataSet ds = model.GetProjectDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    //model.Date = ds.Tables[0].Rows[0]["Date"].ToString();
                    model.Details = ds.Tables[0].Rows[0]["Details"].ToString();
                    model.Image = "/FileUpload/" + ds.Tables[0].Rows[0]["ImageFile"].ToString();
                }
            }

            return View(model);
        }

        [HttpPost]
        [ActionName("Project")]
        public ActionResult Project(Admin model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (model.ProjectId == null)
                {
                    if (postedFile != null)
                    {
                        model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                    //obj.DDChequeDate = string.IsNullOrEmpty(obj.DDChequeDate) ? null : Common.ConvertToSystemDate(obj.DDChequeDate, "dd/MM/yyyy");
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.SaveProject();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Project"] = "Project save successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Project"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["Project"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    if (postedFile != null)
                    {
                        model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                    model.Date = string.IsNullOrEmpty(model.Date) ? null : Comman.ConvertToSystemDate(model.Date, "dd/MM/yyyy");
                    model.AddedBy = "1";
                    DataSet ds = model.UpdateProject();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Project"] = "Project update successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Project"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["Project"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Project"] = ex.Message;
            }
            return RedirectToAction("Project", "Admin");

        }

        public ActionResult GetProjectList()
        {
            Admin model = new Admin();
            List<Admin> lstProject = new List<Admin>();
            DataSet ds = model.GetProjectDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
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



        public ActionResult ProjectDelete(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.ProjectId = Id;
                model.AddedBy = "1";
                DataSet ds = model.DeleteProject();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Record"] = "Record deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Record"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Record"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Record"] = ex.Message;
            }
            return RedirectToAction("GetProjectList", "Admin");
        }


        public ActionResult GetContactList()
        {
            //Admin model = new Admin();
            //List<Admin> lstContact = new List<Admin>();
            //DataSet ds = model.GetContactDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.ContactId = dr["PK_ContactId"].ToString();
            //        obj.Name = dr["Name"].ToString();
            //        obj.Email = dr["Email"].ToString();
            //        obj.Subject = dr["Subject"].ToString();
            //        obj.Address = dr["Address"].ToString();
            //        lstContact.Add(obj);
            //    }
            //    model.lstContact = lstContact;
            //}
            return View();
        }

        [HttpPost]
        [ActionName("GetContactList")]
        public ActionResult GetContactList(Admin model)
        {
            List<Admin> lstContact = new List<Admin>();
            DataSet ds = model.GetContactDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.ContactId = dr["PK_ContactId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Subject = dr["Subject"].ToString();
                    obj.Address = dr["Address"].ToString();
                    lstContact.Add(obj);
                }
                model.lstContact = lstContact;
            }
            return View(model);
        }


        public ActionResult ContactDelete(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.ContactId = Id;
                model.AddedBy = "1";
                DataSet ds = model.DeleteContact();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Contact"] = "Record deleted successfully";
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
            return RedirectToAction("GetContactList", "Admin");
        }


        public ActionResult GetCareerList()
        {
            //Admin model = new Admin();
            //List<Admin> lstCareer = new List<Admin>();
            //DataSet ds = model.GetCareerDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.CareerId = dr["PK_CareerId"].ToString();
            //        obj.Name = dr["Name"].ToString();
            //        obj.Mobile = dr["Mobile"].ToString();
            //        obj.Email = dr["Email"].ToString();
            //        obj.Designation = dr["Designation"].ToString();
            //        obj.Qualification = dr["Qualification"].ToString();
            //        obj.Location = dr["Location"].ToString();
            //        obj.Experience = dr["Experience"].ToString();
            //        obj.Image = dr["Resume"].ToString();
            //        lstCareer.Add(obj);
            //    }
            //    model.lstCareer = lstCareer;
            //}
            return View();
        }

        [HttpPost]
        [ActionName("GetCareerList")]
        public ActionResult GetCareerList(Admin model)
        {
            //Admin model = new Admin();
            List<Admin> lstCareer = new List<Admin>();
            DataSet ds = model.GetCareerDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.CareerId = dr["PK_CareerId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Mobile = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    obj.Qualification = dr["Qualification"].ToString();
                    obj.Location = dr["Location"].ToString();
                    obj.Experience = dr["Experience"].ToString();
                    obj.Image = dr["Resume"].ToString();
                    lstCareer.Add(obj);
                }
                model.lstCareer = lstCareer;
            }
            return View(model);
        }


        public ActionResult DeleteCareer(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.CareerId = Id;
                model.AddedBy = "1";
                DataSet ds = model.DeleteCareer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Career"] = "Record deleted successfully";
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
            return RedirectToAction("GetCareerList", "Admin");
        }


        public ActionResult SaveClient(string Id)
        {
            Admin model = new Admin();
            if (Id != null)
            {
                model.ClientId = Id;
                DataSet ds = model.GetClientDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                    model.Image = "/FileUpload/" + ds.Tables[0].Rows[0]["ImageFile"].ToString();
                    model.Date = ds.Tables[0].Rows[0]["Date"].ToString();
                }
            }

            return View(model);
        }



        [HttpPost]
        [ActionName("SaveClient")]
        public ActionResult SaveClient(Admin model, HttpPostedFileBase postedFile)
        {

            try
            {

                if (model.ClientId == null)
                {
                    if (postedFile != null)
                    {
                        model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                    // model.Date = string.IsNullOrEmpty(model.Date) ? null : Comman.ConvertToSystemDate(model.Date, "dd/MM/yyyy");
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.SaveClient();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Client"] = "Client save successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Client"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["Client"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    if (postedFile != null)
                    {
                        model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                        postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                    }
                    model.Date = string.IsNullOrEmpty(model.Date) ? null : Comman.ConvertToSystemDate(model.Date, "dd/MM/yyyy");
                    model.AddedBy = Session["Pk_AdminId"].ToString();
                    DataSet ds = model.UpdateClient();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "1")
                        {
                            TempData["Client"] = "Client update successfully";
                        }
                        else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                        {
                            TempData["Client"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else
                    {
                        TempData["Client"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }



            }

            catch (Exception ex)
            {
                TempData["Client"] = ex.Message;
            }
            return RedirectToAction("SaveClient", "Admin");

        }

        public ActionResult GetClientDetails()
        {
            //Admin model = new Admin();
            //List<Admin> lst = new List<Admin>();
            //DataSet ds = model.GetClientDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.ClientId = dr["PK_ClientId"].ToString();
            //        obj.Name = dr["Name"].ToString();
            //        obj.Image = dr["ImageFile"].ToString();
            //        obj.Date = dr["Date"].ToString();
            //        lst.Add(obj);
            //    }
            //    model.lstClient = lst;
            //}
            return View();
        }


        [HttpPost]
        [ActionName("GetClientDetails")]
        public ActionResult GetClientDetails(Admin model)
        {
            //Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetClientDetailsForAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.ClientId = dr["PK_ClientId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    obj.Date = dr["Date"].ToString();
                    lst.Add(obj);
                }
                model.lstClient = lst;
            }
            return View(model);
        }



        public ActionResult GetVendorDetails()
        {
            //Admin model = new Admin();
            //List<Admin> lstVendor = new List<Admin>();
            //DataSet ds = model.GetVendorDetails();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.VendorId = dr["PK_VendorId"].ToString();
            //        obj.LoginId = dr["LoginId"].ToString();
            //        obj.Password = dr["Password"].ToString();
            //        obj.Name = dr["Name"].ToString();
            //        obj.Mobile = dr["Mobile"].ToString();
            //        obj.Email = dr["Email"].ToString();
            //        obj.Address = dr["Address"].ToString();
            //        obj.Circle = dr["Circle"].ToString();
            //        obj.OrganizationName = dr["OrganizationName"].ToString();
            //        obj.StartingofOrganization = dr["OrganizationStarting"].ToString();
            //        obj.AccountNo = dr["AccountNo"].ToString();
            //        obj.Branch = dr["Branch"].ToString();
            //        obj.Deposit = dr["Deposit"].ToString();
            //        obj.OrganizationType = dr["OrganisationType"].ToString();
            //        obj.PanNo = dr["PanNumber"].ToString();
            //        obj.GSTNo = dr["GSTNo"].ToString();
            //        obj.Designation = dr["Designation"].ToString();
            //        lstVendor.Add(obj);
            //    }
            //    model.lstVendor = lstVendor;
            //}
            return View();
        }


        [HttpPost]
        [ActionName("GetVendorDetails")]
        public ActionResult GetVendorDetails(Admin model)
        {
            List<Admin> lstVendor = new List<Admin>();
            DataSet ds = model.GetVendorDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.VendorId = dr["PK_VendorId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Mobile = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Circle = dr["Circle"].ToString();
                    obj.OrganizationName = dr["OrganizationName"].ToString();
                    obj.StartingofOrganization = dr["OrganizationStarting"].ToString();
                    obj.AccountNo = dr["AccountNo"].ToString();
                    obj.Branch = dr["Branch"].ToString();
                    obj.Deposit = dr["Deposit"].ToString();
                    obj.OrganizationType = dr["OrganisationType"].ToString();
                    obj.PanNo = dr["PanNumber"].ToString();
                    obj.GSTNo = dr["GSTNo"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    obj.Image = dr["VendorPic"].ToString();
                    lstVendor.Add(obj);
                }
                model.lstVendor = lstVendor;
            }
            return View(model);
        }


        public ActionResult DeleteVendor(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.VendorId = Id;
                model.AddedBy = "1";
                DataSet ds = model.DeleteVendor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "Record deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Vendor"] = ex.Message;
            }
            return RedirectToAction("GetVendorDetails", "Admin");
        }







        public ActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword(Admin model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Error"] = "Password changed  successfully";
                    }
                    else
                    {
                        TempData["Error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ChangePassword", "Admin");
        }


        public ActionResult ClientDelete(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.ClientId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ClientDelete();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Career"] = "Record deleted successfully";
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
            return RedirectToAction("GetClientDetails", "Admin");
        }

        public ActionResult AproveVendor(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.VendorId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.AproveVendor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "Record aproved successfully";
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();

                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                                model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                                //mailbody = "Dear,  <br/>" + model.Name + " <br/> Your record has been  approved";
                                mailbody = "Dear" + " " + model.Name + ", <br/> Your registration request has been  approved by " + model.AdminName + " now you can login your pannel your login credencial and url mention bellow.";

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
                                    Subject = "Registration Approvel",
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
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Vendor"] = ex.Message;
            }
            return RedirectToAction("GetVendorDetails", "Admin");
        }


        public ActionResult DeclineVendor(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.VendorId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeclineVendor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "Record declined successfully";
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();

                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                                model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                                //mailbody = "Dear,  <br/>" + model.Name + " <br/> Your record has been  Declined";
                                mailbody = "Dear" + " " + model.Name + ", <br/> Your registration request has been  declined by " + model.AdminName + "";


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
                                    Subject = "Registration Approvel",
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
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Vendor"] = ex.Message;
            }
            return RedirectToAction("GetVendorDetails", "Admin");
        }


        public ActionResult PurcheseOrder()
        {
            Admin model = new Admin();
            List<Admin> lstVendor = new List<Admin>();
            DataSet ds = model.PurcheseOrderList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.VendorId = dr["PK_VendorId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.OrganizationName = dr["OrganizationName"].ToString();
                    obj.PONumber = dr["PONumber"].ToString();
                    obj.file = dr["UploadFile"].ToString();
                    obj.PODate = dr["PODate"].ToString();
                    lstVendor.Add(obj);
                }
                model.lstVendor = lstVendor;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("PurcheseOrder")]
        public ActionResult PurcheseOrder(Admin model)
        {
            List<Admin> lstVendor = new List<Admin>();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.Name = model.Name == "0" ? null : model.Name;
            DataSet ds = model.PurcheseOrderList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.VendorId = dr["PK_VendorId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.OrganizationName = dr["OrganizationName"].ToString();
                    obj.PONumber = dr["PONumber"].ToString();
                    obj.file = dr["UploadFile"].ToString();
                    obj.PODate = dr["PODate"].ToString();


                    lstVendor.Add(obj);
                }
                model.lstVendor = lstVendor;
            }
            return View(model);
        }



        //[HttpPost]
        public ActionResult AddProfile(HttpPostedFileBase file, string Id, string po)
        {
            Admin userDetail = new Admin();

            try
            {

                userDetail.VendorId = Id;
                if (file != null)
                {
                    userDetail.file = "../VendorFileUpload/" + Guid.NewGuid() + Path.GetExtension(file.FileName);
                    file.SaveAs(Path.Combine(Server.MapPath(userDetail.file)));
                }
                userDetail.PONumber = po;
                userDetail.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = userDetail.UploadVendorFile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "PO upload successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Vendor"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }

            catch (Exception ex)
            {
                TempData["Vendor"] = ex.Message;
            }
            return Json(userDetail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeList()
        {
            //Admin model = new Admin();
            //List<Admin> lstVendor = new List<Admin>();
            //DataSet ds = model.GetEmployeeList();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        Admin obj = new Admin();
            //        obj.Employeeid = dr["PK_EmployeeId"].ToString();
            //        obj.LoginId = dr["LoginId"].ToString();
            //        obj.Password = dr["Password"].ToString();
            //        obj.Name = dr["Name"].ToString();
            //        obj.Mobile = dr["Mobile"].ToString();
            //        obj.Email = dr["Email"].ToString();
            //        obj.Address = dr["Address"].ToString();
            //        obj.Date = dr["DOB"].ToString();
            //        obj.Designation = dr["Designation"].ToString();
            //        obj.Status = dr["Status"].ToString();
            //        lstVendor.Add(obj);
            //    }
            //    model.lstVendor = lstVendor;
            //}
            return View();
        }


        [HttpPost]
        [ActionName("EmployeeList")]
        public ActionResult EmployeeList(Admin model)
        {
            List<Admin> lstVendor = new List<Admin>();
            DataSet ds = model.GetEmployeeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.Employeeid = dr["PK_EmployeeId"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Password = dr["Password"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.Mobile = dr["Mobile"].ToString();
                    obj.Email = dr["Email"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Date = dr["DOB"].ToString();
                    obj.Designation = dr["Designation"].ToString();
                    obj.BloodGroup = dr["BloodGroup"].ToString();
                    obj.Status = dr["Status"].ToString();
                    obj.Image = dr["ProfilePic"].ToString();
                    lstVendor.Add(obj);
                }
                model.lstVendor = lstVendor;
            }
            return View(model);
        }

        public ActionResult DeleteEmployee(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.Employeeid = Id;
                model.AddedBy = "1";
                DataSet ds = model.DeleteEmployee();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Employee"] = "Record deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["Employee"] = ex.Message;
            }
            return RedirectToAction("EmployeeList", "Admin");
        }





        public ActionResult Invoice()
        {
            Admin model = new Admin();
            List<Admin> lstInvoice = new List<Admin>();
            DataSet ds = model.GetInvoiceDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.InvoiceId = dr["PK_InvoiceId"].ToString();
                    obj.ExpectedPaymentDate = dr["ExpectedPaymentDate"].ToString();
                    obj.ApproveDeclineDate = dr["ApproveDeclineDate"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Status = dr["Status"].ToString();
                    obj.Image = dr["ImageFile"].ToString();
                    obj.PaymentDate = dr["PaymentDate"].ToString();
                    obj.PaymentStatus = dr["PaymentStatus"].ToString();
                    obj.Remark = dr["Remarks"].ToString();
                    obj.Name = dr["Name"].ToString();
                    lstInvoice.Add(obj);
                }
                model.lstInvoice = lstInvoice;
            }
            return View(model);
        }


        //public ActionResult EmployeeList()
        //{
        //    Admin model = new Admin();
        //    List<Admin> lstVendor = new List<Admin>();
        //    DataSet ds = model.GetEmployeeList();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            Admin obj = new Admin();
        //            obj.InvoiceId = dr["PK_InvoiceId"].ToString();
        //            obj.ExpectedPaymentDate = dr["ExpectedPaymentDate"].ToString();
        //            obj.ApproveDeclineDate = dr["ApproveDeclineDate"].ToString();
        //            obj.LoginId = dr["LoginId"].ToString();
        //            obj.Status = dr["Status"].ToString();
        //            obj.PaymentDate = dr["PaymentDate"].ToString();
        //            obj.PaymentStatus = dr["PaymentStatus"].ToString();
        //            obj.Remark = dr["Remarks"].ToString();
        //            obj.Name = dr["Name"].ToString();
        //            lstInvoice.Add(obj);
        //        }
        //        model.lstInvoice = lstInvoice;
        //    }

        //    return View(model);
        //}


        //[HttpPost]
        //[ActionName("Invoice")]
        //public ActionResult PaymentInvoice(Admin model)
        //{
        //    try
        //    {
        //        model.AddedBy = Session["Pk_AdminId"].ToString();
        //        DataSet ds = model.UpdatePaymentInvoice();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            if (ds.Tables[0].Rows[0][0].ToString() == "1")
        //            {
        //                TempData["Invoice"] = "Payment Done successfully";
        //            }
        //            else if (ds.Tables[0].Rows[0][0].ToString() == "0")
        //            {
        //                TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //            }
        //        }
        //        else
        //        {
        //            TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["Invoice"] = ex.Message;
        //    }
        //    return RedirectToAction("Invoice", "Admin");
        //}


        [HttpPost]
        [ActionName("Invoice")]
        public ActionResult Invoice(Admin model)
        {
            List<Admin> lstInvoice = new List<Admin>();
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.Status = model.Status == "0" ? null : model.Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetInvoiceDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.InvoiceId = dr["PK_InvoiceId"].ToString();
                    obj.ExpectedPaymentDate = dr["ExpectedPaymentDate"].ToString();
                    obj.ApproveDeclineDate = dr["ApproveDeclineDate"].ToString();
                    obj.LoginId = dr["LoginId"].ToString();
                    obj.Status = dr["Status"].ToString();
                    obj.PaymentDate = dr["PaymentDate"].ToString();
                    obj.PaymentStatus = dr["PaymentStatus"].ToString();
                    obj.Remark = dr["Remarks"].ToString();
                    obj.Name = dr["Name"].ToString();
                    lstInvoice.Add(obj);
                }
                model.lstInvoice = lstInvoice;
            }
            return View(model);
        }




        public ActionResult AproveInvoice(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.InvoiceId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Invoice"] = "Record aproved successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
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
            return RedirectToAction("Invoice", "Admin");
        }


        public ActionResult DeclineInvoice(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.InvoiceId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeclineInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Invoice"] = "Record Decline successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
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
            return RedirectToAction("Invoice", "Admin");
        }


        public ActionResult Update(Admin model)
        {
            try
            {
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.UpdatePaymentInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Invoice"] = "Payment Done successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
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
            return RedirectToAction("Invoice", "Admin");
        }



        public ActionResult EmployeeIdCard(string Id)
        {
            Admin model = new Admin();
            model.Employeeid = Id;
            DataSet ds = model.GetEmployeeList();
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

        public ActionResult EmployeeIdCards(string Id)
        {
            Admin model = new Admin();
            model.Employeeid = Id;
            DataSet ds = model.GetEmployeeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                ViewBag.Designation = ds.Tables[0].Rows[0]["Designation"].ToString();
                ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                ViewBag.BloodGroup = ds.Tables[0].Rows[0]["BloodGroup"].ToString();
                ViewBag.DOB = ds.Tables[0].Rows[0]["DOB"].ToString();
                ViewBag.ApproveDeclineDate = ds.Tables[0].Rows[0]["ApproveDeclineDate"].ToString();
                ViewBag.ExpiaryDate = ds.Tables[0].Rows[0]["ExpiaryDate"].ToString();
                ViewBag.Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                ViewBag.Image = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
            }
            return View(model);
        }


        public ActionResult ApproveEmployee(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.Employeeid = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveEmployee();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Employee"] = "Record aproved successfully";
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();

                        if (model.Email != null)
                        {
                            string mailbody = "";
                            try
                            {
                                model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                                model.AdminName = ds.Tables[0].Rows[0]["AdminName"].ToString();
                                mailbody = "Dear" + " " + model.Name + ", <br/> Your registration request has been  approved by " + model.AdminName + " now you can login your pannel and download your id card your login credencial and url mention bellow.";

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
                                    Subject = "Registration Approvel",
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
                        TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Employee"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Employee"] = ex.Message;
            }
            return RedirectToAction("EmployeeList", "Admin");
        }




        public ActionResult DeleteInvoice(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.InvoiceId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Invoice"] = "Invoice delete successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
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
            return RedirectToAction("Invoice", "Admin");
        }


        //public ActionResult VendorInvoiceDetails()
        //{
        //    List<Admin> VendorInvoicelst = new List<Admin>();
        //    DataSet ds1 = model.SelectInvoiceDetails();
        //    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds1.Tables[0].Rows)
        //        {
        //            Admin obj = new Admin();
        //            obj.InvoiceId = dr["PK_InvoiceId"].ToString();
        //            obj.InvoiceNo = dr["InvoiceNo"].ToString();
        //            obj.Image = dr["ImageFile"].ToString();
        //            obj.AddedOn = dr["AddedOn"].ToString();
        //            VendorInvoicelst.Add(obj);
        //        }
        //        model.VendorInvoicelst = VendorInvoicelst;
        //    }
        //    return View();
        //}

        //[HttpPost]
        //[ActionName("VendorInvoiceDetails")]
        //public ActionResult VendorInvoiceDetails(Admin model)
        //{
        //    List<Admin> VendorInvoicelst = new List<Admin>();
        //    DataSet ds1 = model.SelectInvoiceDetails();
        //    if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in ds1.Tables[0].Rows)
        //        {
        //            Admin obj = new Admin();
        //            obj.InvoiceId = dr["PK_InvoiceId"].ToString();
        //            obj.InvoiceNo = dr["InvoiceNo"].ToString();
        //            obj.Image = dr["ImageFile"].ToString();
        //            obj.AddedOn = dr["AddedOn"].ToString();
        //            VendorInvoicelst.Add(obj);
        //        }
        //        model.VendorInvoicelst = VendorInvoicelst;
        //    }
        //    return View(model);
        //}


        public ActionResult DeleteVendorInvoice(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.InvoiceId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteVendorInvoice();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Invoice"] = "Invoice delete successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Invoice"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
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
            return RedirectToAction("VendorInvoiceDetails", "Admin");
        }

        public ActionResult PoList()
        {
            //    Admin model = new Admin();
            //    List<Admin> lstPo = new List<Admin>();
            //    DataSet ds = model.PoList();
            //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //    {
            //        foreach (DataRow dr in ds.Tables[0].Rows)
            //        {
            //            Admin obj = new Admin();
            //            obj.PK_PoId = dr["PK_PoId"].ToString();
            //            obj.PONumber = dr["Po_Number"].ToString();
            //            obj.AddedOn = dr["AddedOn"].ToString();
            //            lstPo.Add(obj);
            //        }
            //        model.lstPo = lstPo;
            //    }
            return View();
        }
        [HttpPost]
        [ActionName("PoList")]
        public ActionResult PoList(Admin model)
        {
            List<Admin> lstPo = new List<Admin>();
            model.PK_PoId = model.PK_PoId == "0" ? null : model.PK_PoId;
            model.PONumber = model.PONumber == "0" ? null : model.PONumber;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Comman.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Comman.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.PoList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PK_PoId = dr["PK_PoId"].ToString();
                    obj.Name = dr["Name"].ToString();
                    obj.PONumber = dr["Po_Number"].ToString();
                    obj.file = dr["PoFile"].ToString();
                    obj.AddedOn = dr["AddedOn"].ToString();
                    lstPo.Add(obj);
                }
                model.lstPo = lstPo;
            }
            return View(model);
        }


        public ActionResult DeletePo(string Id)
        {
            Admin model = new Admin();
            try
            {
                model.PK_PoId = Id;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeletePo();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["PO"] = "Po delete successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PO"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PO"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }

            }
            catch (Exception ex)
            {
                TempData["PO"] = ex.Message;
            }
            return RedirectToAction("PoList", "Admin");
        }

        public ActionResult PurchaseOrderForm()
        {
            Admin model = new Admin();
            int count = 0;
            List<SelectListItem> ddlVendor = new List<SelectListItem>();
            DataSet ds1 = model.GetVendorName();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlVendor.Add(new SelectListItem { Text = "-Select-", Value = "0" });
                    }
                    ddlVendor.Add(new SelectListItem { Text = r["Name"].ToString(), Value = r["PK_VendorId"].ToString() });
                    count = count + 1;
                }
            }
            ViewBag.ddlVendor = ddlVendor;

            return View(model);
        }

        [HttpPost]
        public ActionResult GetAddress(string PK_VendorId)
        {
            Admin model = new Admin();
            try
            {
                model.PK_VendorId = PK_VendorId;
                DataSet ds = model.GetAddress();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Result = "yes";
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                }
                else
                {
                    model.Address = "";
                    model.Result = "no";
                }
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult AddProfile(Admin formData)
        {
            //userDetail

            var profile = Request.Files;
            bool status = false;
            var datavalue = Request["dataValue"];

            var jss = new JavaScriptSerializer();
            var jdv = jss.Deserialize<dynamic>(Request["dataValue"]);
            DataTable PurchaseOrderDetails = new DataTable();
            PurchaseOrderDetails = JsonConvert.DeserializeObject<DataTable>(jdv["AddData"]);
            formData.DtPurchaseOrderDetails = PurchaseOrderDetails;
            formData.AddedBy = Session["Pk_AdminId"].ToString();
            formData.DeliveryDate = string.IsNullOrEmpty(formData.DeliveryDate) ? null : Comman.ConvertToSystemDate(formData.DeliveryDate, "dd/MM/yyyy");
            DataSet ds = new DataSet();

            ds = formData.SavePurchaseOrder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    TempData["PurchageOrder"] = "Purchage Order Details saved successfully";
                    status = true;
                }
                else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {
                    TempData["PurchageOrder"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            else
            {
                TempData["PurchageOrder"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
            }

            return new JsonResult { Data = new { status = status } };
            //return Json(userDetail, JsonRequestBehavior.AllowGet);
        }



        public ActionResult PrintPurchaseOrderForm()
        {
            Admin model = new Admin();
            try
            {
                DataSet ds = model.GetPurchaseOrderDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    ViewBag.Destination = ds.Tables[0].Rows[0]["Destination"].ToString();
                    ViewBag.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    ViewBag.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                    ViewBag.PartNo = ds.Tables[0].Rows[0]["PartNo"].ToString();
                    ViewBag.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    ViewBag.HSNSACNo = ds.Tables[0].Rows[0]["HSN_SACNo"].ToString();
                    ViewBag.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                    ViewBag.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    ViewBag.UnitPrice = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
                    ViewBag.TaxableTotal = ds.Tables[0].Rows[0]["TaxableTotal"].ToString();
                    ViewBag.GSTValue = ds.Tables[0].Rows[0]["GSTValue"].ToString();
                    ViewBag.TotalValue = ds.Tables[0].Rows[0]["TotalValue"].ToString();
                    ViewBag.DeliveryDate = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    ViewBag.Remark = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                    ViewBag.CGSTRate = ds.Tables[0].Rows[0]["CGST_Rate"].ToString();
                    ViewBag.SGSTRate = ds.Tables[0].Rows[0]["SGST_Rate"].ToString();
                    ViewBag.IGSTRate = ds.Tables[0].Rows[0]["IGST_Rate"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["PurchageOrder"] = ex.Message;
            }
            return View(model);
        }


        public ActionResult PrintPurchaseOrder()
        {
            Admin model = new Admin();
            try
            {
                DataSet ds = model.GetPurchaseOrderDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    ViewBag.Destination = ds.Tables[0].Rows[0]["Destination"].ToString();
                    ViewBag.Type = ds.Tables[0].Rows[0]["Type"].ToString();
                    ViewBag.Item = ds.Tables[0].Rows[0]["Item"].ToString();
                    ViewBag.PartNo = ds.Tables[0].Rows[0]["PartNo"].ToString();
                    ViewBag.Description = ds.Tables[0].Rows[0]["Description"].ToString();
                    ViewBag.HSNSACNo = ds.Tables[0].Rows[0]["HSN_SACNo"].ToString();
                    ViewBag.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
                    ViewBag.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
                    ViewBag.UnitPrice = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
                    ViewBag.TaxableTotal = ds.Tables[0].Rows[0]["TaxableTotal"].ToString();
                    ViewBag.GSTValue = ds.Tables[0].Rows[0]["GSTValue"].ToString();
                    ViewBag.TotalValue = ds.Tables[0].Rows[0]["TotalValue"].ToString();
                    ViewBag.DeliveryDate = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
                    ViewBag.Remark = ds.Tables[0].Rows[0]["REMARKS"].ToString();
                    ViewBag.CGSTRate = ds.Tables[0].Rows[0]["CGST_Rate"].ToString();
                    ViewBag.SGSTRate = ds.Tables[0].Rows[0]["SGST_Rate"].ToString();
                    ViewBag.IGSTRate = ds.Tables[0].Rows[0]["IGST_Rate"].ToString();

                    ViewBag.PONumber = ds.Tables[0].Rows[0]["PONumber"].ToString();
                    ViewBag.PoDate = ds.Tables[0].Rows[0]["PoDate"].ToString();
                    ViewBag.VendorName = ds.Tables[0].Rows[0]["VendorName"].ToString();
              
                }
            }
            catch (Exception ex)
            {
                TempData["PurchageOrder"] = ex.Message;
            }
            return View(model);

        }


        //public ActionResult PrintPo()
        //{
        //    Admin model = new Admin();
        //    try
        //    {
        //        DataSet ds = model.GetPurchaseOrderDetails();
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
        //            ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
        //            ViewBag.Destination = ds.Tables[0].Rows[0]["Destination"].ToString();
        //            ViewBag.Type = ds.Tables[0].Rows[0]["Type"].ToString();
        //            ViewBag.Item = ds.Tables[0].Rows[0]["Item"].ToString();
        //            ViewBag.PartNo = ds.Tables[0].Rows[0]["PartNo"].ToString();
        //            ViewBag.Description = ds.Tables[0].Rows[0]["Description"].ToString();
        //            ViewBag.HSNSACNo = ds.Tables[0].Rows[0]["HSN_SACNo"].ToString();
        //            ViewBag.Unit = ds.Tables[0].Rows[0]["Unit"].ToString();
        //            ViewBag.Quantity = ds.Tables[0].Rows[0]["Quantity"].ToString();
        //            ViewBag.UnitPrice = ds.Tables[0].Rows[0]["UnitPrice"].ToString();
        //            ViewBag.TaxableTotal = ds.Tables[0].Rows[0]["TaxableTotal"].ToString();
        //            ViewBag.GSTValue = ds.Tables[0].Rows[0]["GSTValue"].ToString();
        //            ViewBag.TotalValue = ds.Tables[0].Rows[0]["TotalValue"].ToString();
        //            ViewBag.DeliveryDate = ds.Tables[0].Rows[0]["DeliveryDate"].ToString();
        //            ViewBag.Remark = ds.Tables[0].Rows[0]["REMARKS"].ToString();
        //            ViewBag.CGSTRate = ds.Tables[0].Rows[0]["CGST_Rate"].ToString();
        //            ViewBag.SGSTRate = ds.Tables[0].Rows[0]["SGST_Rate"].ToString();
        //            ViewBag.IGSTRate = ds.Tables[0].Rows[0]["IGST_Rate"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["PurchageOrder"] = ex.Message;
        //    }
        //    return View(model);

        //}






        public ActionResult PurchaseOrderList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            model.PurchaseOrderId = model.PurchaseOrderId == "0" ? null : model.PurchaseOrderId;
            DataSet ds = model.GetPurchaseOrderDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.PurchaseOrderId = dr["PK_PurchageOrderId"].ToString();
                    obj.Address = dr["Address"].ToString();
                    obj.Destination = dr["Destination"].ToString();
                    obj.Type = dr["Type"].ToString();
                    obj.PanNo = dr["PanNumber"].ToString();
                    obj.Item = dr["Item"].ToString();
                    obj.PartNo = dr["PartNo"].ToString();
                    obj.Description = dr["Description"].ToString();
                    obj.HSNSACNo = dr["HSN_SACNo"].ToString();
                    obj.Unit = dr["Unit"].ToString();
                    obj.Quantity = dr["Quantity"].ToString();
                    obj.UnitPrice = dr["UnitPrice"].ToString();
                    obj.TaxableTotal = dr["TaxableTotal"].ToString();
                    obj.GSTValue = dr["GSTValue"].ToString();
                    obj.TotalValue = dr["TotalValue"].ToString();
                    obj.DeliveryDate = dr["DeliveryDate"].ToString();
                    obj.Remark = dr["REMARKS"].ToString();
                    obj.CGSTRate = dr["CGST_Rate"].ToString();
                    obj.SGSTRate = dr["SGST_Rate"].ToString();
                    obj.IGSTRate = dr["IGST_Rate"].ToString();
                    lst.Add(obj);
                }
                model.lstPurchaseorder = lst;
            }
            return View(model);
        }




    }
}



