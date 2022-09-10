using Dapper;
using DataAcess.entits;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAcess
{
    public class DAL_Todo
    {
        string connectionString = ConfigurationManager.ConnectionStrings["devconnection"].ConnectionString;
        public void SaveTodo(TaskDataTable AddTask)
        {
            //string sql = @"INSERT INTO [dbo].[taskDataTable]
            //                ([taskName]
            //                ,[description]
            //                ,[taskStatus]
            //                 ,[assigneeId])
            //            VALUES
            //                (@taskName
            //                ,@description
            //                ,@taskStatus
            //                 ,@assigneeId)";
            string sql = @"EXEC [dbo].[usp_Addtask] @taskName, @description, @taskStatus, @assigneeId, @reporterId,
                        @FlagList,@SprintName,@LableName";
            object ts = new
            {
                taskName = AddTask.taskName,
                description = AddTask.description,
                taskStatus = AddTask.taskStatus,
                assigneeId = AddTask.assigneeId,
                reporterId = AddTask.reporterId,
                SprintName = AddTask.sprintName,
                FlagList = AddTask.FlagList.AsTableValuedParameter("AddMembers"),
                LableName = AddTask.LableList.AsTableValuedParameter("AddMembers")

            };
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, ts);
                db.Close();
            }
        }

        //public void SaveActive(int id)
        //{
        //    //using (var db = new taskDatabaseEntities())
        //    //{


        //    //    foreach (var item in db.taskDataTables)
        //    //    {
        //    //        if (item.taskId == id)
        //    //        {
        //    //            item.taskStatus = 2;
        //    //        }
        //    //    }
        //    //    db.SaveChanges();


        //    //}

        //}



        public List<TaskDataTable> TaskList(int userId,int id)
        {

            string sql = "EXEC [dbo].[usp_getFilterTaskList] @userId,@taskStatus";
            //string sql = "select t.taskId,t.taskName,t.taskStatus,t.description,u.userName,t.assigneeId from taskDataTable t left join [user] u on t.assigneeId = u.Id  where [taskStatus]=@taskStatus";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<TaskDataTable> TaskList = db.Query<TaskDataTable>(sql, new { userId = userId,taskStatus = id }).AsList();
                db.Close();
                return TaskList;

            }

        }


        public void MoveNext(int Id, int Status)
        {
            string sql = "update taskDataTable set taskStatus = @Status Where taskId = @Id";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { Id = Id, Status = Status });
                db.Close();
            }
            //using (var db = new taskDatabaseEntities())
            //{


            //    foreach (var item in db.taskDataTables)
            //    {
            //        if (item.taskId == Id)
            //        {
            //            item.taskStatus = Status;
            //        }
            //    }
            //    db.SaveChanges();


            //}

            //using (var db = new taskDatabaseEntities())
            //{

            //    db.taskDataTables.Add(AddTask);
            //    db.SaveChanges();

            //}
        }
        public void DeleteRecord(int Id)
        {
            string sql = "delete from taskDataTable Where taskId = @Id";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { Id = Id });
                db.Close();
            }
        }
        public List<StatusTable> StatusList()
        {

            string sql = "Select * from statusTable";
            using (var db = new SqlConnection(connectionString))
            {
                List<StatusTable> StatusList = db.Query<StatusTable>(sql).AsList();
                return StatusList;

            }

        }


        public List<User> UserList()
        {

            string sql = "Select * from [dbo].[User]";
            using (var db = new SqlConnection(connectionString))
            {
                List<User> UserList = db.Query<User>(sql).AsList();
                return UserList;

            }

        }




        public void UpdateTask(TaskDataTable updateTable)
        {
            string sql = "update TaskDataTable set assigneeId=@assigneeId where taskId=@taskId ";
            object ts = new
            {
                taskId = updateTable.taskId,
                assigneeId = updateTable.assigneeId
            };
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, ts);
                db.Close();
            }

        }

        public List<User> GetAllUserList()
        {
            string sql = "select u.Id,u.Username,u.FirstName,u.LastName, r.Name from [user] u join [Role] r on u.RoleId = r.Id";
            using (var db = new SqlConnection(connectionString))
            {
                List<User> userList = db.Query<User>(sql).AsList();
                return userList;

            }

        }

        public List<Role> RoleList()
        {

            string sql = "Select * from Role";
            using (var db = new SqlConnection(connectionString))
            {
                List<Role> RoleList = db.Query<Role>(sql).AsList();
                return RoleList;


            }
        }

        public List<User> GetEditUserList(int id)
        {
            string sql = "select  u.RoleId, u.Id,u.Username,u.FirstName,u.LastName,u.[Password], r.Name from [user] u join [Role] r on u.RoleId = r.Id where u.Id= @Id";
            using (var db = new SqlConnection(connectionString))
            {
                List<User> userList = db.Query<User>(sql, new { Id = id }).AsList();
                return userList;

            }

        }

        public void UpdateUser(User user)
        {
            string sql = @"update [User]
                            set Username = @Username,
                            [Password]= @Password,
                            RoleId=@RoleId,
                            FirstName=@FirstName, 
                            LastName=@LastName 
                        from TaskDataTable 
                        where Id=@Id";
            object ts = new
            {
                Username = user.Username,
                Password = user.Password,
                RoleId = user.RoleId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
            };
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, ts);
                db.Close();
            }

        }

        public List<TaskDataTable> GetTaskList(int id)
        {
            string sql = "exec  GetUserCount @id ";
            using (var db = new SqlConnection(connectionString))
            {
                List<TaskDataTable> taskList = db.Query<TaskDataTable>(sql, new { Id = id }).AsList();

                return taskList;

            }

        }

        public void DeleteUser(int id)
        {
            string sql = "exec DeleteUser @Id";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { Id = id });
                db.Close();
            }

        }

        public List<TaskDataTable> GetUserTaskList(int id)
        {
            string sql = "exec ViewTask @id ";
            using (var db = new SqlConnection(connectionString))
            {
                List<TaskDataTable> taskList = db.Query<TaskDataTable>(sql, new { Id = id }).AsList();

                return taskList;

            }

        }

        public void UnassigneeUser(int id)
        {
            string sql = "Update [TaskDataTable] set assigneeId=null where taskId= @id";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { Id = id });
                db.Close();
            }

        }

        public void AddFlag(Flag Flag)
        {

            string sql = @"INSERT INTO [dbo].[FlagTable]
                            ([FlagName])
                        VALUES
                            (@FlagName )";
            using (var db = new SqlConnection(connectionString))
            {


                db.Open();
                db.Query(sql, new { FlagName = Flag.FlagName });
                db.Close();
            }
        }
        public List<Flag> GetFlagList()
        {

            string sql = @"select * from [dbo].[FlagTable]";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<Flag> FlagList = db.Query<Flag>(sql).AsList();
                db.Close();

                return FlagList;
            }
        }
        public void SaveLable(LableList lableList)
        {
            string sql = "insert into LableList(LableName) values(@LableName)";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { LableName = lableList.LableName });
                db.Close();
            }


        }

        public List<LableList> GetLableList()
        {
            string sql = "select * from [dbo].[LableList]";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<LableList> LableList = db.Query<LableList>(sql).AsList();
                db.Close();
                return LableList;
            }



        }

        public void SaveSprint(SprintList sprintList)
        {
            string sql = "insert into sprinttable(SprintName) values(@SprintName)";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, new { SprintName = sprintList.SprintName });
                db.Close();
            }


        }

        public List<SprintList> GetSprintList()
        {
            string sql = "select * from sprinttable";
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<SprintList> sprintList = db.Query<SprintList>(sql).AsList();
                db.Close();
                return sprintList;
            }



        }


       

       



    }

}
