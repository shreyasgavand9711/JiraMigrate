using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;

namespace JiraMigrate.Controllers
{
    public class AddTeamController : Controller
    {
        private BL_AddTeam AddTeamBL;
        private BL_Todo TodoBL;
        public AddTeamController(BL_AddTeam bL_AddTeam, BL_Todo bL_Todo)
        {
            this.AddTeamBL = bL_AddTeam;
            this.TodoBL = bL_Todo;


        }

        [HttpPost]

        public ActionResult CreateTeam(TeamsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TeamLeders = AddTeamBL.GetUserList();
                return View("CreateTeam", model);
            }
            AddTeamBL.SaveTeam(model);
            //TaskTableViewModel model = new TaskTableViewModel();

            return RedirectToAction("TeamManagement");
        }
        [HttpGet]
        public ActionResult CreateTeam()
        {
            //List<TaskTableViewModel> Completed = AddTeamBL.CompletedList(3);
            TeamsViewModel model = new TeamsViewModel();

            model.TeamLeders = TodoBL.GetUserList();
            return View("CreateTeam", model);


        }
     //   [ActionAccessValidation(actionId = 7)]
        public ActionResult TeamManagement()
        {
            List<TeamsViewModel> TeamManagement = AddTeamBL.TeamsList();


            return View("TeamManagement", TeamManagement);


        }

        //public ActionResult TeamMemberList(int TeamID)
        //{


        //    try
        //    {
        //        List<TeamsViewModel> model = AddTeamBL.MemberList(TeamID);
        //        return Json(new
        //        {
        //            data = RenderPartialToString("TeamMemberList", model)

        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception)
        //    {

        //        return Json(new
        //        {
        //            data = false

        //        }, JsonRequestBehavior.AllowGet);

        //    }

        //}

    }
}
