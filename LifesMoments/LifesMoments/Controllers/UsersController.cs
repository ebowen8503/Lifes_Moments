using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LifesMoments.Models;
using LifesMoments.Controllers.DataControllers;

namespace LifesMoments.Controllers
{
    public class UsersController : Controller
    {
        // GET: userLogin
        public ActionResult userLogin()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(loginModel loginCredentials)
        {
            ////create an instance of the user
            userModel currentUser = new userModel();
            ////create instance of userDataController
            lmUserDataController userDataController = new lmUserDataController("");
            ////send login credentials to userDataController and set returned user to our current instance
            currentUser = userDataController.GetLogin(loginCredentials);
            ////call LoggedIn

            return LoggedIn(currentUser);
        }
        /// <summary>
        /// if user login is not in the system return back to current view if not continue logging in
        /// </summary>

        public ActionResult LoggedIn(userModel currentUser)
        {
            if (currentUser == null)
            {
                return View("userLogin");
            }
            else

            {
                return RedirectToAction("scrapbookHome", "Scrapbook");

            }

        }

        
         //Begin Register New User
        
        //GET:/Users/userRegister

        public ActionResult userRegister()
        {

            return View();
        }

        //POST: /Users/userRegister
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(FormCollection register)
        {
            userModel usModel = new userModel();
            lmUserDataController currentUser = new lmUserDataController("");


            usModel.firstName = register["userModel.firstName"].ToString(); ;
            usModel.lastName = register["userModel.lastName"].ToString();
            usModel.emailAddress = register["userModel.emailAddress"].ToString();
            usModel.userPassword = register["userModel.userPassword"].ToString();

            usModel = currentUser.CreateUser(usModel);

            return Register(currentUser);
        }

        public ActionResult Register(userModel currentUser)
        {
            if (currentUser == null)
            {
                return View("scrapbookHome");
            }
            else

            {
                return View("Home");

            }



        }



    }
}