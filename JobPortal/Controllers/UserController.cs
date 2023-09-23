using DatabaseLayer;
using JobPortal.common;
using JobPortal.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace JobPortal.Controllers
{
    public class UserController : Controller
    {
        private JobPortalDbEntities Db = new JobPortalDbEntities();

        /// <summary>
        /// Registration
        /// </summary>
        /// <returns></returns>
        public ActionResult NewUser()
        {
            return View(new UserMV());
        }

        /// <summary>
        /// Registration(Post)
        /// </summary>
        /// <param name="userMV"></param>
        /// <param name="imageFile"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewUser(UserMV userMV, HttpPostedFileBase imageFile)
        {
            if (ModelState.IsValid)
            {
                var checkemail = Db.UserTables.Where(u => u.EmailAddress == userMV.EmailAddress).FirstOrDefault();
                if (checkemail != null)
                {
                    ModelState.AddModelError("EmailAddress", "E-mail is already registered!!!");
                    return View(new UserMV());
                }

                var checkuser = Db.UserTables.Where(u => u.UserName == userMV.UserName ).FirstOrDefault();
                if (checkuser != null)
                {
                    ModelState.AddModelError("UserName", "Username is already registered!!!");
                    return View(new UserMV());
                }
                var checknumber = Db.UserTables.Where(u => u.ContactNo == userMV.ContactNo ).FirstOrDefault();
                if (checknumber != null)
                {
                    ModelState.AddModelError("ContactNo", "Contact-no is already registered!!!");
                    return View(new UserMV());
                }
                var checkcompanynumber = Db.CompanyTables.Where(u => u.PhoneNo == userMV.Company.PhoneNo ).FirstOrDefault();
                if (checkcompanynumber != null)
                {
                    ModelState.AddModelError(string.Empty, "Company phone-no is already registered!!!");
                    return View(new UserMV());
                }
                var checkcompanymail = Db.CompanyTables.Where(u => u.EmailAddress == userMV.Company.EmailAddress).FirstOrDefault();
                if (checkcompanymail != null)
                {
                    ModelState.AddModelError(string.Empty, "Company e-mail is already registered!!!");
                    return View(new UserMV());
                }
                
                using (var trans = Db.Database.BeginTransaction())
                {
                    try
                    {
                        var user = new UserTable();
                        Password EncryptData=new Password();
                        EncryptData.Encode(userMV.Password);
                        user.UserName = userMV.UserName;
                        user.Password = EncryptData.Encode(userMV.Password);
                        user.ContactNo = userMV.ContactNo;
                        user.EmailAddress = userMV.EmailAddress;
                        //user.Image = userMV.Image;
                        user.UserTypeID = userMV.AreYouProvider == true ? 2 : 3;
                        Db.UserTables.Add(user);
                        Db.SaveChanges();
                        //
                            if (userMV.FileUpload != null && userMV.FileUpload.ContentLength > 0)
                            {
                                // Read and save the uploaded file to the database
                                using (var binaryReader = new BinaryReader(userMV.FileUpload.InputStream))
                                {
                                    var fileBytes = binaryReader.ReadBytes(userMV.FileUpload.ContentLength);
                                    user.FileData = fileBytes;
                                    Db.SaveChanges();
                                }
                            }
                        if (imageFile != null && imageFile.ContentLength > 0)
                        {
                            using (var binaryReader = new BinaryReader(imageFile.InputStream))
                            {
                                var imageBytes = binaryReader.ReadBytes(imageFile.ContentLength);
                                var base64String = Convert.ToBase64String(imageBytes);
                                user.Image = base64String;
                                Db.SaveChanges();
                            }
                        }
                        //
                        if (userMV.AreYouProvider == true)
                        {
                            var company = new CompanyTable();
                            company.UserID = user.UserID;
                            company.EmailAddress = userMV.Company.EmailAddress;
                            company.CompanyName = userMV.Company.CompanyName;
                            company.ContactNo = userMV.ContactNo;
                            company.PhoneNo = userMV.Company.PhoneNo;
                            company.Logo = "~/Content/assets/img/logo/logo.png";
                            company.Description = userMV.Company.Description;
                            Db.CompanyTables.Add(company);
                            Db.SaveChanges();
                        }
                        //
                      
                        //
                        trans.Commit();
                        return RedirectToAction("Login");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, "please enter all details!!!");
                        trans.Rollback();
                    }
                }
            }
            return View(new UserMV());
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View(new UserLoginMV());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLoginMV userLoginMV)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Password EncryptData = new Password();
                    var pas= EncryptData.Encode(userLoginMV.Password);
                    // var user = Db.UserTables.Where(u => u.EmailAddress == userLoginMV.EmailAddress && u.Password == userLoginMV.Password).FirstOrDefault();
                    var user = Db.UserTables.Where(u => u.EmailAddress == userLoginMV.EmailAddress && (u.Password == pas.ToString()||u.Password== userLoginMV.Password)).FirstOrDefault();
                    if (user == null)
                    {
                        ModelState.AddModelError(string.Empty, "username or password is incorrect");
                        return View(new UserLoginMV());
                    }
                    Session["UserID"] = user.UserID;
                    Session["UserName"] = user.UserName;
                    Session["UserTypeID"] = user.UserTypeID;
                    if (user.UserTypeID == 2)
                    {
                        Session["CompanyID"] = user.CompanyTables.FirstOrDefault().CompanyID;
                    }
                    return RedirectToAction("Index", "Home");
                }
                return View(userLoginMV);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "please enter all details!!!");
            }
            return View(new UserLoginMV());
        }

        ///Logout <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session["UserID"] = string.Empty;
            Session["UserName"] = string.Empty;
            Session["CompanyID"] = string.Empty;
            Session["UserTypeID"] = string.Empty;
            return RedirectToAction("Index", "Home");
        }

        ///To View All Registered Users <summary>
        /// To View All Registered Users
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public ActionResult AllUsers()
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(Session["UserTypeID"])))
                {
                    return RedirectToAction("Login", "User");
                }
                var users = Db.UserTables.ToList();
                return View(users);
            }
            catch (Exception ex)
            {
                throw new Exception("error " + ex.Message);
            }
        }

        
        /// Forgot Password <summary>
        /// Forgot Password
        /// </summary>
        /// <returns></returns>
        
        public ActionResult Forgot()
        {
            return View(new ForgotPasswordMV());
        }

        /// Forgot Password(Post) <summary>
        /// Forgot Password(Post)
        /// </summary>
        /// <param name="forgotPasswordMV"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Forgot(ForgotPasswordMV forgotPasswordMV)
        {
            //Password EncryptData = new Password();
            //var pas = EncryptData.Encode(forgotPasswordMV.Password);
            try
            {
                var user = Db.UserTables.Where(u => u.EmailAddress == forgotPasswordMV.Email).FirstOrDefault();
                if (user != null)
                {
                    string userandpassword = "User Name : " + user.EmailAddress + "\nPassword : " + user.Password;
                    //string userandpassword = "User Name : " + user.EmailAddress + "\nPassword : " + EncryptData.Encode(user.Password);
                    string body = userandpassword;
                    bool isSendEmail = JobPortal.Forgot.Email.Emailsend(user.EmailAddress, "Account Details", body, true);
                    if (isSendEmail)
                    {
                        ModelState.AddModelError(string.Empty, "Username and password send successfully");
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "Your Email is Registered!");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is no registered!!!");
                }
                return View(forgotPasswordMV);
            }
            catch (Exception ex)
            {
                throw new Exception("error in code:" + ex.Message);
            }
        }
        /// <summary>
        /// to download the resume
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DownloadResume(int id)
        {
            try
            {
                var user = Db.UserTables.Find(id);
                if (user != null && user.FileData != null)
                {
                    var content = user.FileData;
                    var fileName = user.UserName + "_Resume.pdf"; 
                    return File(content, "application/pdf", fileName);
                }
                else
                {
                    // Handle the case where the resume is not found or is null
                    return HttpNotFound("Resume not found");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("error in code:" + ex.Message);
            }
        }
        /// <summary>
        /// to view the resume
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ViewResume(int id)
        {
            try
            {
                var user = Db.UserTables.FirstOrDefault(u => u.UserID == id);
                if (user != null && user.FileData != null)
                {
                    var fileBytes = user.FileData;
                    string contentType = "application/pdf"; 

                    // Return the file data as a FileResult
                    return File(fileBytes, contentType);
                }
                return HttpNotFound();
            }
            catch (Exception ex)
            {
                throw new Exception("error in code:" + ex.Message);
            }
        }
    }
}