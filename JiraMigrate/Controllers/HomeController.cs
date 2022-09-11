using JiraMigrate.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using BusinessLogic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ViewModel;
using Microsoft.AspNetCore.Http;

namespace JiraMigrate.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private BL_Todo TodoBL;
        public HomeController(BL_Todo bL_Todo)
        {
            this.TodoBL = bL_Todo;


        }



        //  [ActionAccessValidation(actionId = 4)]
        public IActionResult Todo()
        {
             int userId = (int)HttpContext.Session.GetInt32("UserId");

            List<TaskTableViewModel> Todo = TodoBL.FilterTaskList(userId, 1);
            foreach (var item in Todo)
            {
                item.taskStatusList = TodoBL.GetStatusList();
                item.userList = TodoBL.GetUserList();
            }


            return View("Todo", Todo);


        }



        //[ActionAccessValidation(actionId = 5)]
        public IActionResult Active()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");

            List<TaskTableViewModel> Active = TodoBL.FilterTaskList(userId, 2);
            foreach (var item in Active)
            {
                item.taskStatusList = TodoBL.GetStatusList();
                item.userList = TodoBL.GetUserList();

            }

            return View("Active", Active);



        }



        // [ActionAccessValidation(actionId = 6)]
        public IActionResult Completed()
        {
            //   int userId = (int)HttpContext.Session.SetString["UserId"];
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            List<TaskTableViewModel> Completed = TodoBL.FilterTaskList(userId, 3);

            return View("Completed", Completed);


        }



        public IActionResult ShowHomePage()
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
           // int userId = 1;
            List<TaskTableViewModel> Todo = TodoBL.FilterTaskList(userId, 1);

            foreach (var item in Todo)
            {
                item.taskStatusList = TodoBL.GetStatusList();
                item.userList = TodoBL.GetUserList();
            }
            List<TaskTableViewModel> Active = TodoBL.FilterTaskList(userId, 2);
            foreach (var item in Active)
            {
                item.taskStatusList = TodoBL.GetStatusList();
                item.userList = TodoBL.GetUserList();
            }
            List<TaskTableViewModel> Completed = TodoBL.FilterTaskList(userId, 3);
            TaskTableDataModel TaskLists = new TaskTableDataModel();
            TaskLists.Todo = Todo;
            TaskLists.Active = Active;
            TaskLists.Completed = Completed;

           
            return View("Home", TaskLists);
        }






        [HttpPost]
        public IActionResult AddTeam(TaskTableViewModel model)
        {




            return View(model);
        }



        // Form show
        [HttpGet]
        public IActionResult AddTask()
        {
            ViewBag.Message = "Your application description page.";

            TaskTableViewModel model = new TaskTableViewModel();

            model.taskStatusList = TodoBL.GetStatusList();
            model.userList = TodoBL.GetUserList();
            List<CommonDropdownType> FlagList = new List<CommonDropdownType>();
            foreach (var item in TodoBL.GetFlagList())
            {
                CommonDropdownType flag = new CommonDropdownType();

                flag.id = item.FlagId;
                flag.text = item.FlagName;


                FlagList.Add(flag);
            }

            List<CommonDropdownType> LabeList = new List<CommonDropdownType>();
            foreach (var item in TodoBL.GetLableListAll())
            {
                CommonDropdownType lable = new CommonDropdownType();

                lable.id = item.LableId;
                lable.text = item.LableName;


                LabeList.Add(lable);
            }

            model.labelList = LabeList;
            model.sprintList = TodoBL.GetAllSprintList();
            model.FlagList = FlagList;



            return View(model);
        }

        [HttpPost]

        //  [ActionAccessValidation(actionId = 3)]
        public IActionResult AddTask(TaskTableViewModel model)
        {
            //  create one
            if (!ModelState.IsValid)
            {

                return View("AddTask", model);
            }

            TodoBL.SaveTodo(model);
            if (model.taskStatus == 1)
            {
                return RedirectToAction("Todo");
            }
            else if (model.taskStatus == 2)
            {
                return RedirectToAction("Active");
            }
            else if (model.taskStatus == 3)
            {
                return RedirectToAction("Completed");
            }

            return RedirectToAction("AddTask");

        }

        public IActionResult MoveInToToDo(int Id)
        {

            TodoBL.MoveToToDo(Id);

            return RedirectToAction("ShowHomePage");
        }

        public IActionResult MoveInToActive(int Id)
        {

            TodoBL.MoveToActive(Id);

            return RedirectToAction("ShowHomePage");
        }

        public IActionResult MoveInToCompleted(int Id)
        {
            TodoBL.MoveToCompleted(Id);

            return RedirectToAction("ShowHomePage");
        }

        public IActionResult Delete(int id)
        {


            try
            {
                TodoBL.Delete(id);
                return Json(new
                {
                    data = true

                });

            }
            catch (Exception)
            {

                return Json(new
                {
                    data = false

                });

            }


        }


        // [HttpPost]
        //public IActionResult UpdateTask(TaskTableViewModel model)
        //{
        //    try
        //    {
        //        if (model.assigneeId != null)
        //        {
        //            TodoBL.UpdateTask(model);
        //            return Json(new
        //            {
        //                data = true

        //            }, JsonRequestBehavior.AllowGet);
        //        }
        //        else
        //        {
        //            return Json(new
        //            {
        //                data = false

        //            }, JsonRequestBehavior.AllowGet);
        //        }




        //    }
        //    catch (Exception)
        //    {

        //        return Json(new
        //        {
        //            data = false

        //        }, JsonRequestBehavior.AllowGet);

        //    }


        //}
        //[ActionAccessValidation(actionId = 14)]
        [HttpGet]
        public IActionResult UserManagement()
        {
            List<UserSignupViewModel> users = TodoBL.GetAllUserList();
            return View("UserManagement", users);
        }

        public IActionResult EditUser(int Id)
        {
            UserSignupViewModel model = new UserSignupViewModel();
            List<UserSignupViewModel> user = TodoBL.GetEditUserList(Id);
            foreach (var item in user)
            {

                model.Id = item.Id;
                model.RoleId = item.RoleId;
                model.Username = item.Username;
                model.FirstName = item.FirstName;
                model.LastName = item.LastName;
                model.Password = item.Password;
                model.RoleIdList = TodoBL.GetRoleList();
            }


            return View(model);
        }
        [HttpPost]

        //public IActionResult EditUser(UserSignupViewModel model)
        //{
        //    try
        //    {
        //        TodoBL.UpdateUser(model);
        //        return Json(new
        //        {
        //            data = true

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

        //public IActionResult GetUserCount(int id)
        //{
        //    try
        //    {
        //        int count = TodoBL.TaskList(id);
        //        return Json(new
        //        {
        //            data = true,
        //            count = count

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

        //public IActionResult DeleteUser(int id)
        //{
        //    try
        //    {
        //        TodoBL.DeleteUser(id);
        //        return Json(new
        //        {
        //            data = true

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

        //public IActionResult ViewTask(int id)
        //{


        //    try
        //    {
        //        List<TaskTableViewModel> model = TodoBL.UserTaskList(id);
        //        return Json(new
        //        {
        //            data = RenderPartialToString("ViewTask", model)

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

        //public IActionResult UnassigneeUser(int id)
        //{
        //    try
        //    {
        //        TodoBL.UnassigneeUser(id);
        //        return Json(new
        //        {
        //            data = true

        //        }, JsonRequestBehavior.AllowGet);

        //    }
        //    catch (Exception)
        //    {

        //        return Json(new
        //        {
        //            data = false

        //        }, JsonRequestBehavior.AllowGet);

        //    }

        //    // return RedirectToAction("ViewTask");
        //}



        /*===========FLAG AND REPORTS==============================*/
        //  [ActionAccessValidation(actionId = 10)]
        public IActionResult FlagList(FlagViewModel model)
        {


            return View("FlagList", model);
        }


        public IActionResult FlagList()
        {
            List<FlagViewModel> model = TodoBL.GetFlagList();



            return View("FlagList", model);
        }


        public IActionResult AddFlag()
        {
            TodoBL.GetFlagList();

            return View();

        }

        [HttpPost]
        public IActionResult AddFlag(FlagViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View("AddFlag", model);
            }

            TodoBL.AddFlag(model);

            return RedirectToAction("FlagList");
        }

        //   [ActionAccessValidation(actionId = 12)]

        public IActionResult LableListView()
        {
            List<LableListViewModel> model = TodoBL.GetLableList();
            return View("LableListView", model);
        }
        public IActionResult AddLable()
        {
            return View("AddLable");
        }
        [HttpPost]
        public IActionResult AddLable(LableListViewModel model)
        {
            TodoBL.SaveLable(model);
            return View("LableListView");
        }


        //    [ActionAccessValidation(actionId = 13)]
        public IActionResult SprintList()
        {
            List<SprintListViewModel> model = TodoBL.GetSprintList();
            return View("SprintListView", model);

        }


        public IActionResult AddSprint()
        {
            return View("AddSprint");
        }
        [HttpPost]
        public IActionResult AddSprint(SprintListViewModel model)
        {
            TodoBL.SaveSprint(model);
            return View("SprintListView");
        }






        //public IIActionResult Index()
        //{
        //    return View();
        //}

        //public IIActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IIActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
