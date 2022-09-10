using Dapper;
using DataAcess.entits;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;

namespace DataAcess.Authentication
{
    public class DAL_Authentication
    {
        string connectionString;

        public DAL_Authentication(string connectionsString)
        {
            connectionString = connectionsString;
        }
        public void SaveUserData(User adddata)
        {
            string sql = @"INSERT INTO [dbo].[User]
                            ([Username]
                            ,[Password]
                            ,[RoleId]
                            ,[FirstName]
                            ,[LastName]
                            ,[CreatedAt])
                        VALUES
                            (@Username
                            ,@Password
                            ,@RoleId
                            ,@FirstName
                            ,@LastName
                            ,@CreatedAt)";

            object sp = new
            {
                Username = adddata.Username,
                Password = adddata.Password,
                RoleId = adddata.RoleId,
                FirstName = adddata.FirstName,
                LastName = adddata.LastName,
                CreatedAt = System.DateTime.Now.ToString("yyyy-MM-dd")
            };

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, sp);
                db.Close();
            }

        }

        public User GetSingleWithUsernameAndPassword(string username, string password)
        {
            string sql = "Select * from [dbo].[User] where [Username]=@username and [Password]=@password";
            //string connectionString = ConfigurationManager.ConnectionStrings["devconnection"].ConnectionString;
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<User> users = db.Query<User>(sql, new { username = username, password = password }).AsList();
                db.Close();
                return users.FirstOrDefault();
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

        public List<Actions> GetActionList(int RoleId)
        {

            string sql = "Select A.* From [dbo].[ActionAccess] AS AA JOIN [dbo].[Actions] AS A ON AA.ActionId = A.ActionId  Where AA.RoleId = @RoleId";
            //"Select R.* from [Role] AS R JOIN [User] AS U On U.RoleId = R.Id Where U.[Username] = @Username";
            using (var db = new SqlConnection(connectionString))
            {
                List<Actions> Actions = db.Query<Actions>(sql, new { RoleId = RoleId }).AsList();
                return Actions;

            }

        }
    }





}
