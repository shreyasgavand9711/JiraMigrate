using ViewModel;
using DataAcess;
using DataAcess.entits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogic
{
    public class BL_AddTeam
    {
        public void SaveTeam(TeamsViewModel model)
        {
            Team AddTeam = new Team();
            AddTeam.TeamName = model.TeamName;
            AddTeam.TeamLeder = model.TeamLeder;

            DataTable Members = new DataTable();
            Members.Columns.Add("userid", typeof(int));

            foreach (var item in model.TeamMember1)
            {
                Members.Rows.Add(item);
            }
            AddTeam.TeamMember = Members;
            new DAL_AddTeam().SaveTeam(AddTeam);

        }

        public List<CommonDropdownType> GetUserList()
        {

            List<CommonDropdownType> UserList = new List<CommonDropdownType>();
            foreach (var item in new DAL_AddTeam().UserList())
            {
                CommonDropdownType User = new CommonDropdownType();

                User.id = item.Id;
                User.text = item.FirstName;

                UserList.Add(User);
            }
            return UserList;



        }

        public List<TeamsViewModel> TeamsList()
        {
            List<TeamsViewModel> TeamList = new List<TeamsViewModel>();
            List<Team> TeamsList = new DAL_AddTeam().TeamsList();


            foreach (var item in TeamsList)
            {
                TeamsViewModel TeamsViewModel = new TeamsViewModel();


                TeamsViewModel.TeamID = item.TeamID;
                TeamsViewModel.TeamName = item.TeamName;
                TeamsViewModel.LeaderName = item.FirstName+ ' ' +item.LastName;
                TeamsViewModel.MemberCount = item.MemberCount + 1;


                TeamList.Add(TeamsViewModel);
            }



            return TeamList;
        }

        public List<TeamsViewModel> MemberList(int TeamID)
        {
            List<TeamsViewModel> MemberList = new List<TeamsViewModel>();
            List<TeamMember> model = new DAL_AddTeam().MemberList(TeamID);
        

            foreach (var item in model)
            {
                TeamsViewModel TeamsViewModel = new TeamsViewModel();

                TeamsViewModel.Id = item.Id;
                TeamsViewModel.TeamLeder = item.TeamLeder;
                TeamsViewModel.TeamName = item.TeamName;
                TeamsViewModel.LeaderName = item.FirstName + ' ' + item.LastName;
                


                MemberList.Add(TeamsViewModel);
            }

            return MemberList;
        }





    }
}
