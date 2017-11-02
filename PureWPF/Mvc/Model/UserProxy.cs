using PureMVC.Interfaces;
using PureMVC.Patterns.Proxy;
using PureWPF.Mvc.Model.Vo;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PureWPF.Mvc.Model
{
    public class UserProxy : Proxy, IProxy
    {
        public new const string NAME = "UserProxy";

        public UserProxy() : base(NAME, new ObservableCollection<UserVO>())
        {
            AddItem(new UserVO("pgriffin", "Peter", "Griffin", "peter@web123.com", "TESTPASSWORD"));
            AddItem(new UserVO("lgriffin", "Lois", "Griffin", "lgriffin@web123.com", "TESTPASSWORD"));
            AddItem(new UserVO("jswanson", "Joe", "Swanson", "jswanson@web123.com", "TESTPASSWORD"));
            AddItem(new UserVO("cbrown", "Cleveland", "Brown", "cleveland@web123.com", "TESTPASSWORD"));
        }

        public IList<UserVO> Users
        {
            get { return (IList<UserVO>) Data; }
        }

        public void AddItem(UserVO user)
        {
            Users.Add(user);
        }

        public void UpdateItem(UserVO user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName.Equals(user.UserName))
                {
                    Users[i] = user;
                    break;
                }
            }

        }
        public void DeleteItem(UserVO user)
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserName.Equals(user.UserName))
                {
                    Users.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
