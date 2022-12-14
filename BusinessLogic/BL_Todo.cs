using DataAcess;
using DataAcess.entits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel;
using System.Data;

namespace BusinessLogic
{
    public class BL_Todo
    {
        DAL_Todo connectionStringTodo;
        public BL_Todo(string connectionString)
        {
            this.connectionStringTodo = new DAL_Todo(connectionString);
        }
        public void SaveTodo(TaskTableViewModel model)
        {
            TaskDataTable AddTask = new TaskDataTable();
            AddTask.taskName = model.taskName;
            AddTask.description = model.description;
            AddTask.taskStatus = model.taskStatus;
            AddTask.assigneeId = model.assigneeId;
            AddTask.reporterId = model.reporterId;
            AddTask.sprintName = model.sprintName;  
           

            DataTable Flags2 = new DataTable();
            Flags2.Columns.Add("userid", typeof(int));

            foreach (var item in model.Flags)
            {
                Flags2.Rows.Add(item);
            }
            AddTask.FlagList = Flags2;

            DataTable LableList2 = new DataTable();
            LableList2.Columns.Add("userid", typeof(int));

            foreach (var item in model.LableName)
            {
                LableList2.Rows.Add(item);
            }
            AddTask.LableList = LableList2;


            connectionStringTodo.SaveTodo(AddTask);

        }

        public List<TaskTableViewModel> FilterTaskList(int userId, int id)
        {


            List<TaskTableViewModel> TaskList = new List<TaskTableViewModel>();
            List<TaskDataTable> FilterTaskList = connectionStringTodo.TaskList(userId, id);
            foreach (var item in FilterTaskList)
            {
                TaskTableViewModel taskTableViewModel = new TaskTableViewModel();


                taskTableViewModel.description = item.description;
                taskTableViewModel.taskId = item.taskId;
                taskTableViewModel.taskName = item.taskName;
                taskTableViewModel.taskStatus = item.taskStatus;
                taskTableViewModel.assigneeId = item.assigneeId;
                taskTableViewModel.userName = item.userName;




                TaskList.Add(taskTableViewModel);
            }
            return TaskList;


        }

        public TaskTableViewModel GetFilterTaskt(int userId, int taskId, int statusId)
        {
            List<TaskDataTable> FilterTaskList = connectionStringTodo.TaskList(userId, statusId);
            TaskTableViewModel taskTableViewModel = new TaskTableViewModel();
            foreach (var item in FilterTaskList)
            {
                if (item.taskId == taskId)
                {
                    taskTableViewModel.description = item.description;
                    taskTableViewModel.taskId = item.taskId;
                    taskTableViewModel.taskName = item.taskName;
                    taskTableViewModel.taskStatus = item.taskStatus;
                    taskTableViewModel.assigneeId = item.assigneeId;
                    taskTableViewModel.userName = item.userName;
                }
            }
            return taskTableViewModel;


        }

        public List<TaskTableViewModel> ActiveList(int userId, int id)
        {


            List<TaskTableViewModel> Active = new List<TaskTableViewModel>();

            List<TaskDataTable> ActiveList = connectionStringTodo.TaskList(userId, id);

            foreach (var item in ActiveList)
            {
                TaskTableViewModel taskTableViewModel = new TaskTableViewModel();

                taskTableViewModel.description = item.description;
                taskTableViewModel.taskId = item.taskId;
                taskTableViewModel.taskName = item.taskName;
                taskTableViewModel.taskStatus = item.taskStatus;
                taskTableViewModel.assigneeId = item.assigneeId;
                taskTableViewModel.userName = item.userName;

                Active.Add(taskTableViewModel);
            }
            return Active;

        }
        public List<TaskTableViewModel> CompletedList(int userId, int id)
        {

            List<TaskTableViewModel> Completed = new List<TaskTableViewModel>();

            List<TaskDataTable> CompletedList = connectionStringTodo.TaskList(userId, id);
            foreach (var item in CompletedList)
            {
                TaskTableViewModel taskTableViewModel = new TaskTableViewModel();

                taskTableViewModel.description = item.description;
                taskTableViewModel.taskId = item.taskId;
                taskTableViewModel.taskName = item.taskName;
                taskTableViewModel.taskStatus = item.taskStatus;
                taskTableViewModel.assigneeId = item.assigneeId;
                taskTableViewModel.userName = item.userName;


                Completed.Add(taskTableViewModel);
            }

            return Completed;

        }

        public void Delete(int id)
        {

            connectionStringTodo.DeleteRecord(id);
        }
        public List<CommonDropdownType> GetStatusList()
        {
            List<StatusTable> StatusList = connectionStringTodo.StatusList();
            List<CommonDropdownType> statusDropdownList = new List<CommonDropdownType>();
            foreach (var item in StatusList)
            {
                CommonDropdownType status = new CommonDropdownType();

                status.id = item.statusID;
                status.text = item.statusName;

                statusDropdownList.Add(status);
            }

            return statusDropdownList;
        }
        public void MoveToToDo(int Id)
        {

            connectionStringTodo.MoveNext(Id, 1);



        }
        public void MoveToActive(int Id)
        {

            connectionStringTodo.MoveNext(Id, 2);



        }

        public void MoveToCompleted(int Id)
        {

            connectionStringTodo.MoveNext(Id, 3);



        }

        public List<CommonDropdownType> GetUserList()
        {

            List<CommonDropdownType> userList = new List<CommonDropdownType>();
            foreach (var item in connectionStringTodo.UserList())
            {
                CommonDropdownType user = new CommonDropdownType();

                user.id = item.Id;
                user.text = item.Username;

                userList.Add(user);
            }
            return userList;



        }

        public void UpdateTask(TaskTableViewModel model)
        {
            TaskDataTable updateTable = new TaskDataTable();


            updateTable.taskId = model.taskId;
            updateTable.assigneeId = model.assigneeId;

            connectionStringTodo.UpdateTask(updateTable);


        }

        public List<UserSignupViewModel> GetAllUserList()
        {
            List<UserSignupViewModel> user = new List<UserSignupViewModel>();
            List<User> userList = connectionStringTodo.GetAllUserList();

            foreach (var item in userList)
            {
                UserSignupViewModel userViewModel = new UserSignupViewModel();

                userViewModel.Id = item.Id;
                userViewModel.Username = item.Username;
                userViewModel.Name = item.Name;
                userViewModel.FirstName = item.FirstName;
                userViewModel.LastName = item.LastName;

                user.Add(userViewModel);

            }
            return user;


        }

        public List<CommonDropdownType> GetRoleList()
        {

            List<CommonDropdownType> roleList = new List<CommonDropdownType>();
            foreach (var item in connectionStringTodo.RoleList())
            {
                CommonDropdownType role = new CommonDropdownType();

                role.id = item.Id;
                role.text = item.Name;

                roleList.Add(role);
            }
            return roleList;

        }

        public List<UserSignupViewModel> GetEditUserList(int id)
        {
            List<UserSignupViewModel> user = new List<UserSignupViewModel>();
            List<User> userList = connectionStringTodo.GetEditUserList(id);

            foreach (var item in userList)
            {
                UserSignupViewModel userViewModel = new UserSignupViewModel();

                userViewModel.Id = item.Id;
                userViewModel.Username = item.Username;
                userViewModel.RoleId = item.RoleId;
                userViewModel.FirstName = item.FirstName;
                userViewModel.LastName = item.LastName;
                userViewModel.Password = item.Password;

                user.Add(userViewModel);

            }
            return user;


        }

        public void UpdateUser(UserSignupViewModel model)
        {
            User update = new User();

            update.Username = model.Username;
            update.RoleId = model.RoleId;
            update.FirstName = model.FirstName;
            update.LastName = model.LastName;
            update.Password = model.Password;
            update.Id = model.Id;

             connectionStringTodo.UpdateUser(update);
        }

        public int TaskList(int id)
        {
            List<TaskDataTable> taskList = connectionStringTodo.GetTaskList(id);
            List<TaskDataTable> tempTasklist = new List<TaskDataTable>();
            foreach (var task in taskList)
            {
                if (task.taskStatus == 1 || task.taskStatus == 2)
                {
                    tempTasklist.Add(task);
                }
            }
            int count = tempTasklist.Count();

            return count;

        }

        public void DeleteUser(int id)
        {
            connectionStringTodo.DeleteUser(id);
        }

        public List<TaskTableViewModel> UserTaskList(int id)
        {
            List<TaskTableViewModel> users = new List<TaskTableViewModel>();
            List<TaskDataTable> taskList = connectionStringTodo.GetUserTaskList(id);
            foreach (var item in taskList)
            {
                TaskTableViewModel model = new TaskTableViewModel();

                model.taskName = item.taskName;
                model.userName = item.userName;
                model.description = item.description;
                model.taskStatus = item.taskStatus;
                model.assigneeId = item.assigneeId;
                //model.statusName = item.statusName;
                model.taskId = item.taskId;

                users.Add(model);


            }
            return users;


        }

        public void UnassigneeUser(int id)
        {
            connectionStringTodo.UnassigneeUser(id);
        }
        public void AddFlag(FlagViewModel model)
        {
            Flag Flag = new Flag();
            Flag.FlagName = model.FlagName;

            connectionStringTodo.AddFlag(Flag);
        }
        public List<FlagViewModel> GetFlagList()
        {

            List<FlagViewModel> Flaglist = new List<FlagViewModel>();
            List<Flag> Flags = connectionStringTodo.GetFlagList();

            foreach (var item in Flags)
            {
                FlagViewModel Flag = new FlagViewModel();



                Flag.FlagId = item.FlagId;
                Flag.FlagName = item.FlagName;





                Flaglist.Add(Flag);
            }


            return Flaglist;

        }
        public void SaveLable(LableListViewModel model)
        {
            LableList lableList = new LableList();
            lableList.LableName = model.LableName;

             connectionStringTodo.SaveLable(lableList);
        }

        public List<LableListViewModel> GetLableList()
        {
            List<LableListViewModel> lableListViewModels = new List<LableListViewModel>();

            List<LableList> lableList = connectionStringTodo.GetLableList();

            foreach (var item in lableList)
            {
                LableListViewModel model = new LableListViewModel();

                model.LableName = item.LableName;

                lableListViewModels.Add(model);
            }

            return lableListViewModels;
        }


        public List<SprintListViewModel> GetSprintList()
        {
            List<SprintListViewModel> listViewModels = new List<SprintListViewModel>();

            List<SprintList> lableList = connectionStringTodo.GetSprintList();

            foreach (var item in lableList)
            {
                SprintListViewModel model = new SprintListViewModel();

                model.SprintName = item.SprintName;

                listViewModels.Add(model);
            }

            return listViewModels;
        }

        public void SaveSprint(SprintListViewModel model)
        {
            SprintList sprintList = new SprintList();
            sprintList.SprintName = model.SprintName;

            connectionStringTodo.SaveSprint(sprintList);
        }

        public List<CommonDropdownType> GetAllLableList()
        {
            List<CommonDropdownType> labels = new List<CommonDropdownType>();
            foreach (var item in  connectionStringTodo.GetLableList())
            {
                CommonDropdownType label = new CommonDropdownType();

                label.id = item.LableId;
                label.text = item.LableName;

                labels.Add(label);

            }
            return labels;

        }

        public List<CommonDropdownType> GetAllSprintList()
        {
            List<CommonDropdownType> Sprints = new List<CommonDropdownType>();
            foreach (var item in connectionStringTodo.GetSprintList())
            {
                CommonDropdownType model = new CommonDropdownType();
                model.id = item.SprintId;
                model.text = item.SprintName;

                Sprints.Add(model);
            }
            return Sprints;


        }

        public List<LableListViewModel> GetLableListAll()
        {

            List<LableListViewModel> lablelist = new List<LableListViewModel>();
            List<LableList> lables = connectionStringTodo.GetLableList();

            foreach (var item in lables)
            {
                LableListViewModel lable = new LableListViewModel();



                lable.LableId = item.LableId;
                lable.LableName = item.LableName;





                lablelist.Add(lable);
            }


            return lablelist;




        }

       

       
    }
    }
