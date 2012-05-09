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

        // GET: /Account/LogOn
        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(LogOnViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                User user = UserBusiness.GetUserByEmail(model.Email);
                //// MODIFY FOR DATABASE USE!

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

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Dashboard");
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            { 
                User user = new User();
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

                    //WRITE OUT MODEL REGISTRATION ERROR
                    //ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        /// <summary>
        /// Get Edit user
        /// </summary>
        /// <returns>Edit view</returns>
        [CustomAuthorize]
        public ActionResult Edit()
        {
            User user = UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name);
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

        /// <summary>
        /// POST Edit user
        /// </summary>
        /// <returns>Edit view</returns>
        [CustomAuthorize]
        [HttpPost]
        public ActionResult Edit(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = BusinessLayer.UserBusiness.GetUserByEmail(model.Email);
                newUser.Area = model.Area;
                newUser.BirthDate = model.BirthDate;
                newUser.FirstName = model.FirstName;
                newUser.Gender = model.Gender;
                newUser.LastName = model.LastName;
                newUser.PhoneNumber = model.PhoneNumber;
                newUser.State = model.State;
                newUser.StreetAddress = model.StreetAddress;
                BusinessLayer.UserBusiness.Update(newUser);
                return View();
            }
            return View();
        }



        //
        // GET: /Account/ChangePassword

        ////[Authorize]
        //public ActionResult ChangePassword()
        //{
        //    return View();
        //}

        //
        // POST: /Account/ChangePassword

        [CustomAuthorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Domain.User newUser = new User();
                newUser.Password = model.NewPassword;
                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                try
                {
                    User currentUser = UserBusiness.GetUserById((int)Session["UserId"]);
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

        /// <summary>
        /// Get Delete user
        /// </summary>
        /// <returns>Edit view</returns>
        [CustomAuthorize]
        public ActionResult Delete()
        {
                User userToBeDeleted = UserBusiness.GetUserByEmail(HttpContext.User.Identity.Name);
                UserBusiness.Delete(userToBeDeleted);
                FormsAuthentication.SignOut();
                return RedirectToAction("LogOn");
        }
        //
        // GET: /Account/ChangePasswordSuccess

        //public ActionResult ChangePasswordSuccess()
        //{
        //    return View();
        //}


        private static string CreatePasswordHash(string pwd)
        {
            string saltAndPwd = String.Concat(pwd, 8083327);
            string hashedPwd =
             FormsAuthentication.HashPasswordForStoringInConfigFile(
             saltAndPwd, "sha1");

            return hashedPwd;
        }

        #region Status Codes
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
