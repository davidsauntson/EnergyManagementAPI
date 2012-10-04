using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using emAPI.ClassLibrary;
using EnergyManagerWebApp.Models;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using EnergyManagerWebApp.Helpers;

namespace EnergyManagerWebApp.Controllers
{
    public class AccountController : Controller
    {
        emAPIService.IemAPIClient emAPI = new emAPIService.IemAPIClient();
        
        


//* * * HTTP GET & POST METHODS

        //
        // GET: /Account/LogOn

        public ActionResult LogOn()
        {
            return View();
        }

        //
        // POST: /Account/LogOn

        [HttpPost]
        public ActionResult LogOn(User user, string returnUrl)
        {
            try
            {
                string cryptedPassword = encryptPassword(user.Password, user.Username);

                int userId = ResponseReader.convertTo<int>(emAPI.validateUser(user.Username, cryptedPassword));

                if (userId != 0)
                {
                    ///FormsAuthentication.SetAuthCookie(user.Username, false);
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        ModelState.AddModelError("", "The user name or password provided is incorrect.");
                        return View(user);
                    }
                    else
                    {
                        ///store userId in cookie
                        FormsAuthentication.SetAuthCookie(userId.ToString(), false);

                        ///take the user to their homepage
                        return RedirectToAction("UserHome", "Home", new { id = userId });
                    }
                }
                else
                {
                    return View(user);
                }
            }
            catch
            {
                // If we got this far, something failed, redisplay form
                return View("Error");
            }
        }

        //
        // GET: /Account/LogOff

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
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
        public ActionResult Register(UserViewModel model)
        {
                ///check if username & email are unique
                ///
                bool userNameIsUnique = false;
                bool emailIsUnique = false;

                userNameIsUnique = ResponseReader.convertTo<bool>(emAPI.usernameIsUnique(model.User.Username));
                emailIsUnique = ResponseReader.convertTo<bool>(emAPI.emailIsUnique(model.User.Email));

                if (userNameIsUnique)
                {
                    if (emailIsUnique)
                    {
                        if (model.ConfirmNewPassword == model.NewPassword  && model.NewPassword != null)
                        {
                            ///ok to create the user
                            ///hash the password
                            string cryptedPassword = encryptPassword(model.NewPassword, model.Username);

                            int newUserId = 0;
                            try
                            {
                                FormsAuthentication.SignOut();

                                ///get user id from emAPI
                                newUserId = ResponseReader.convertTo<int>(emAPI.createUser(model.User.Username, model.User.Forename,
                                                                            model.User.Surname, cryptedPassword, model.User.Email));
                                if (newUserId != 0)
                                {
                                    ///login
                                    FormsAuthentication.SetAuthCookie(newUserId.ToString(), false);
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                            catch
                            {
                                ModelState.AddModelError("", "There has been an error registering your details.  Please try again.");
                                return View(model);
                            }                            

                            if (model.CreateAPropertyNow)
                            {
                                ///redirect to create a property
                                return RedirectToAction("Create", "Property", new { userId = newUserId });
                            }
                            else
                            {
                                ///redirect to home
                                return RedirectToAction("UserHome", "Home", new { id = newUserId });
                            }
                        }
                        else
                        {
                            ///the passwords do not match
                            ModelState.AddModelError("", "Confirmation password does not match");
                        }
                    }
                    else
                    {
                        ///the email is not unique
                        ModelState.AddModelError("", ErrorCodeToString(MembershipCreateStatus.DuplicateEmail));
                    }
                }
                else
                {
                    ///the username is not unique
                    ModelState.AddModelError("", ErrorCodeToString(MembershipCreateStatus.DuplicateUserName));
                }


                return View(model);
            }

  
        ////
        //// GET: /Account/ChangePassword 

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Account/ChangePassword

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(UserViewModel user)
        {
            bool changePasswordSucceeded;
            try
            {
                if (user.ConfirmNewPassword == user.NewPassword)
                {
                    string cryptedPassword = encryptPassword(user.OldPassword, user.Username);
                    int userId = ResponseReader.convertTo<int>(emAPI.validateUser(user.Username, cryptedPassword));


                    if (userId == int.Parse(User.Identity.Name))
                    {
                        cryptedPassword = encryptPassword(user.NewPassword, user.Username);
                        changePasswordSucceeded = ResponseReader.convertTo<bool>(emAPI.updatePassword(userId, cryptedPassword));
                    }
                    else
                    {
                        changePasswordSucceeded = false;
                        ModelState.AddModelError("", "There has been an error, please try again");
                    }
                }
                else
                {
                    changePasswordSucceeded = false;
                    ModelState.AddModelError("", "New password does not match new password confirmation");
                }
            }
            catch (Exception)
            {
                changePasswordSucceeded = false;
            }

            if (changePasswordSucceeded)
            {
                return RedirectToAction("ChangePasswordSuccess");
            }
            else
            {
                ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
            }

            // If we got this far, something failed, redisplay form
            return View(user);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }


//* * * NON-HTTP METHODS

        private string encryptPassword(string plaintextPassword, string username)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(username + plaintextPassword, "MD5");
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
