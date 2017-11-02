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
            AddItem(new UserVO("fgehmlich", "Frederic", "Gehmlich", "frederic@web123.com", "TESTPASSWORD"));
            AddItem(new UserVO("tschmidt", "Thorsten", "Schmidt", "smitty@web123.com", "TESTPASSWORD"));
            AddItem(new UserVO("wwaldemar", "Waldi", "Waldemar", "wwaldemar@web123.com", "TESTPASSWORD"));
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
