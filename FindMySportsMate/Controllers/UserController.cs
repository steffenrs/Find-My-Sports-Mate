using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Domain;
using BusinessLayer;
using System.Security.Cryptography;
using System.Text;

namespace PresentationLayer
{
    public class UserController : Controller
    {
        [CustomAuthorize]
        public ActionResult Profile(int Id)
        {
            try
            {
                User user = BusinessLayer.UserBusiness.Get(Id);
                UserViewModel viewModel = new UserViewModel
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Gender = user.Gender,
                    BirthDate = user.BirthDate,
                    Email = user.Email,
                    StreetAddress = user.StreetAddress,
                    Area = user.Area,
                    State = user.State,
                    PhoneNumber = user.PhoneNumber
                };

                return View(viewModel);
            }
            catch (DomainException e)
            {
                return View("Error", ErrorHelper.ErrorModelForDomainException(e));
            }
        }
        
        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = UserBusiness.Get(model.Email);

                    model.Password = CreatePasswordHash(model.Password);

                    if (user != null && model.Password.SequenceEqual(user.Password))
                    {

                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddDays(1), true, user.Id.ToString());

                        // Encrypt the ticket.
                        string encTicket = FormsAuthentication.Encrypt(ticket);

                        // Create the cookie.
                        Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                            && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                    }

                }
                catch (DomainException e)
                {
                    ModelState.AddModelError("", "The user does not exist. Please register a new user."); 
                };


                
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            { 
                User user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Password = CreatePasswordHash(model.Password);
                user.StreetAddress = model.StreetAddress;
                user.Area = model.Area;
                user.State = model.State;
  
                try
                {

                    UserBusiness.Register(user);
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.Email, DateTime.Now, DateTime.Now.AddDays(1), true, user.Id.ToString());

                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    // Create the cookie.
                    Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    return RedirectToAction("Index", "Dashboard");
                }
                catch(Exception e)
                {
                    ModelState.AddModelError("", "There was an error registering the user: the user already exists");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [CustomAuthorize]
        public ActionResult Edit()
        {
            User user = UserBusiness.Get(HttpContext.User.Identity.Name);
            UserViewModel model = new UserViewModel();
            model.Area = user.Area;
            model.BirthDate = user.BirthDate;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.Gender = user.Gender;
            model.LastName = user.LastName;
            //model.Password = user.Password;
            model.PhoneNumber = user.PhoneNumber;
            model.State = user.State;
            model.StreetAddress = user.StreetAddress;

            return View(model);
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = BusinessLayer.UserBusiness.Get(model.Email);
                newUser.Area = model.Area;
                newUser.BirthDate = model.BirthDate;
                newUser.FirstName = model.FirstName;
                newUser.Gender = model.Gender;
                newUser.LastName = model.LastName;
                newUser.PhoneNumber = model.PhoneNumber;
                newUser.State = model.State;
                newUser.StreetAddress = model.StreetAddress;
                newUser.Password = CreatePasswordHash(model.Password);
                BusinessLayer.UserBusiness.Update(newUser);
                RedirectToAction("Index", "Dashboard");
            }
            return View();
        }

        [CustomAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Domain.User newUser = new User();
                newUser.Password = model.NewPassword;
             
                try
                {
                    User currentUser = UserBusiness.Get((int)Session["UserId"]);
                    if (model.OldPassword == currentUser.Password)
                    {
                        UserBusiness.Update(newUser);
                        return RedirectToAction("ChangePasswordSuccess");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private static string CreatePasswordHash(string pwd)
        {
            string saltAndPwd = String.Concat(pwd, 8083327);
            string hashedPwd =
             FormsAuthentication.HashPasswordForStoringInConfigFile(
             saltAndPwd, "sha1");

            return hashedPwd;
        }
    }
}
