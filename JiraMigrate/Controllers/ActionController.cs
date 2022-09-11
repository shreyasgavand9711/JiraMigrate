using BusinessLogic;
using BusinessLogic.Authentication;
using JiraMigrate.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace JiraMigrate.Controllers
{
    public class ActionController : Controller
    {
        private BL_Action ActionBL;
        private BL_Authentication AuthenticationBL;
        public ActionController(BL_Action bL_Action, BL_Authentication bL_Authentication)
        {
            this.ActionBL = bL_Action;

            this.AuthenticationBL = bL_Authentication;
        }

        //[SessionBaseAuthorize]
       
        //[ActionAccessValidation(actionId = 11)]
        [HttpGet]
        public ActionResult ActionList()
        {
            List<UserActions> model = ActionBL.GetActionList();
            return View("ActionList", model);
        }
        public ActionResult SetAction()
        {
           
            UserActions modal = new UserActions(); 
          //  List<UserActions> Actions = new BL_Authentication().GetActionList((int)HttpContext.Session.GetInt32["RoleId"]);
            IEnumerable<UserActions> Actions = AuthenticationBL.GetActionList((int)HttpContext.Session.GetInt32("RoleId"));
            //  Session["Actions"] = Actions;
            HttpContext.Session.SetObject("Actions", ((object)Actions));
            return View(modal);
        }
        [HttpPost]

       // [WithModelValidation]
        public ActionResult SetAction(UserActions model)
        {
            ActionBL.SetAction(model);
            return RedirectToAction("SetAction");
        }




    }
}
