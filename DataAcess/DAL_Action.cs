using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAcess;
using DataAcess.entits;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAcess.entits
{
   public class DAL_Action
    {
        string connectionString;

        public DAL_Action(string connectionsString)
        {
            connectionString = connectionsString;
        }
        public void SetAction(Actions Actions)
        {


            string sql = @"EXEC [dbo].[AddAction]  @ActionName, @ControllerName, @ShowInSideBar, @DisplayName, @ActionAccess";
            object obj = new
            {

            ActionName = Actions.ActionName,
            ControllerName = Actions.ControllerName,
            ShowInSideBar = Actions.ShowInSideBar,
            DisplayName = Actions.DisplayName,
            ActionAccess = Actions.ActionAccess.AsTableValuedParameter("AddMembers")
            };
            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, obj);
                db.Close();
            }
        }
        public List<Actions> GetActionList()
        {

            string sql = @"select * from [dbo].[Actions]";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<Actions> ActionList = db.Query<Actions>(sql).AsList();
                db.Close();

                return ActionList;
            }
        }

    }
}
