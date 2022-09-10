using DataAcess.Authentication;
using System.Collections.Generic;
using DataAcess.entits;
using ViewModel;

namespace BusinessLogic.Authentication
{
    public class BL_Authentication
    {
        DAL_Authentication authentication;
        public BL_Authentication(string connectionString)
        {
            this.authentication = new DAL_Authentication(connectionString);
        }
        public void SaveUserData(UserSignupViewModel model)
        {
            User addData = new User();
            addData.Id = model.Id;
            addData.Username = model.Username;
            addData.Password = model.Password;
            addData.RoleId = model.RoleId;
            addData.FirstName = model.FirstName;
            addData.LastName = model.LastName;
            authentication.SaveUserData(addData);
        }
        public UserLoginViewModel GetAutheticatedUser(UserLoginViewModel userLoginViewModel)
        {
            User user = authentication
                .GetSingleWithUsernameAndPassword(userLoginViewModel.Username ,userLoginViewModel.Password);
            //UserLoginViewModel authenticated =  AutoMapper.Mapper.Map<User,UserLoginViewModel>(user);
            if (user == null)
            {
                return null;
            }
            UserLoginViewModel authenticated = new UserLoginViewModel()
            {
                Id = user.Id,
                Username = user.Username,
                Password = user.Password,
                RoleId = user.RoleId
            };
            return authenticated;
        }
        public List<CommonDropdownType> GetRoleList()
        {
            List<Role> roleList = authentication.RoleList();
            List<CommonDropdownType> roleDropdownList = new List<CommonDropdownType>();
            foreach (var item in roleList)
            {
                CommonDropdownType role = new CommonDropdownType();

                role.id = item.Id;
                role.text = item.Name;

                roleDropdownList.Add(role);
            }

            return roleDropdownList;
        }
        public List<UserActions> GetActionList(int RoleId)
        {
            List<Actions> actions = authentication.GetActionList(RoleId);
            List<UserActions> userActionList = new List<UserActions>();
            foreach (var action in actions)
            {
                UserActions userAction = new UserActions()
                {
                    ActionId = action.ActionId,
                    ControllerName = action.ControllerName,
                    DisplayName = action.DisplayName,
                    ActionName = action.ActionName,
                    ShowInSideBar = action.ShowInSideBar
                };
                userActionList.Add(userAction);
            }
            return userActionList;
        }
    }
}
