using BhartiNetwork.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

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
                    model.Date = ds.Tables[0].Rows[0]["Date"].ToString();
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
            Admin model = new Admin();
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
            Admin model = new Admin();
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


        public ActionResult SaveClient()
        {

            return View();
        }

        [HttpPost]
        [ActionName("SaveClient")]
        public ActionResult SaveClient(Admin model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../FileUpload/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
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
            catch (Exception ex)
            {
                TempData["Client"] = ex.Message;
            }
            return RedirectToAction("SaveClient", "Admin");

        }

        public ActionResult GetClientDetails()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            DataSet ds = model.GetClientDetails();
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
                model.AddedBy = "1";
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
                model.AddedBy = "1";
                DataSet ds = model.AproveVendor();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Vendor"] = "Record aprove successfully";
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





    }
}