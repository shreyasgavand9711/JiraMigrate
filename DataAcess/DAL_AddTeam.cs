using Dapper;
using DataAcess.entits;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess
{
    public class DAL_AddTeam
    {
        string connectionString = ConfigurationManager.ConnectionStrings["devconnection"].ConnectionString;
        public void SaveTeam(Team AddTeam)
        {



            //string sql = @"INSERT INTO [dbo].[Teams]
            //                ([TeamName]
            //                ,[TeamLeder])
            //            VALUES
            //                (@TeamName
            //                ,@TeamLeder)";


            string sql = @"EXEC upsC_addTeam @TeamName,@TeamLeder,@TeamMember";
            object team = new
            {
                TeamName = AddTeam.TeamName,
                TeamLeder = AddTeam.TeamLeder,
                TeamMember = AddTeam.TeamMember.AsTableValuedParameter("AddMembers")
            };


            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                db.Query(sql, team);

                db.Close();
            }


            
            //foreach(var item in AddTeam.TeamMember) 
            //{
            //    string sql2 = @"INSERT INTO [dbo].[TeamMembers]
            //                ([TeamMember],
            //                  [TeamId]   )
            //            VALUES
            //                (@TeamMemberId)";
            //    object teamMember = new
            //    {
                  
            //        TeamMemberId = item
            //    };
            //    using (var db = new SqlConnection(connectionString))
            //    {
            //        db.Open();

            //        db.Query(sql2, teamMember);
            //        db.Close();
            //    }

            //}




        }
        public List<User> UserList()
        {

            string sql = "Select * from User";
            using (var db = new SqlConnection(connectionString))
            {
                List<User> UserList = db.Query<User>(sql).AsList();
                return UserList;

            }

        }
        public List<TeamMember> MemberList(int TeamID)
        {
            
            string sql = @"EXEC [dbo].[Usp_GetMemberList] @TeamId ";

            using (var db = new SqlConnection(connectionString))
            {
                db.Open();
                List<TeamMember> MemberList = db.Query <TeamMember>(sql, new {TeamId = TeamID}).AsList();
                db.Close();
                return MemberList;
            }
        } 

        public List<Team> TeamsList()
        {
            string sql = @"EXEC [dbo].[Usp_TeamListWithLeaderName] ";
            using (var db = new SqlConnection(connectionString))
            {
                List<Team> TeamsList = db.Query<Team>(sql).AsList();
                return TeamsList;
            }
        }

    }
}
