using BusinessLogic.Authentication;
using JiraMigrate.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ViewModel;

namespace jiraDamy.Controllers
{
    public class AuthenticationController : Controller
    {

        /*Actions actions = new Actions()*/
        #region constructor
        private BL_Authentication AuthenticationBL;
        public AuthenticationController(BL_Authentication bL_Authentication)
        {
            this.AuthenticationBL = bL_Authentication;
        }
        #endregion

        // GET: Authentication
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("todo", "Home");
            }
            return View();
        }

        [HttpPost]
        //[WithModelValidation]
        public IActionResult Login(UserLoginViewModel userLoginViewModel)
        {
            UserLoginViewModel authenticatedUserModel = AuthenticationBL.GetAutheticatedUser(userLoginViewModel);
            if (authenticatedUserModel == null)
            {
                return View(authenticatedUserModel);
            }
            HttpContext.Session.SetString("Username", authenticatedUserModel.Username);
            HttpContext.Session.SetInt32("UserId", authenticatedUserModel.Id);
            HttpContext.Session.SetInt32("RoleId", authenticatedUserModel.RoleId);
            IEnumerable<UserActions> Actions = AuthenticationBL.GetActionList(authenticatedUserModel.RoleId);
            HttpContext.Session.SetObject("Actions", ((object)Actions));
            return RedirectToAction("ShowHomePage", "Home");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Username", null);
            return RedirectToAction("Login");
        }
        public IActionResult Unathorized()
        {
            return View("Unathorized");
        }
        //[SessionBaseAuthorize]
        //[ActionAccessValidation(actionId = 1)]
        public IActionResult SignUp()

        {
            UserSignupViewModel model = new UserSignupViewModel();
            model.RoleIdList = AuthenticationBL.GetRoleList();
            return View(model);
        }
        [HttpPost]
        public IActionResult SignUp(UserSignupViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("SignUp");
            }
            AuthenticationBL.SaveUserData(model);
            return RedirectToAction("Login");
        }

        //    public IActionResult GetRoleIdTypeList()
        //    {
        //        return new BL_Authentication().GetRoleList();
        //}
    }
}