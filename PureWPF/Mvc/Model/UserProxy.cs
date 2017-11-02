using PureMVC.Interfaces;
using PureMVC.Patterns.Proxy;
using PureWPF.Mvc.Model.Vo;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PureWPF.Mvc.Model
{
    /// <summary>
    /// UserProxy manages all UserVO instances.
    /// For test purposes four objects are added at runtime.
    /// </summary>
    public class UserProxy : Proxy, IProxy
    {
        public new const string NAME = "UserProxy";
        private static int iterator = 1;


        public UserProxy() : base(NAME, new ObservableCollection<UserVO>())
        {
            AddItem(new UserVO("pgriffin", "Peter", "Griffin", "peter@web123.com"));
            AddItem(new UserVO("lgriffin", "Lois", "Griffin", "lgriffin@web123.com"));
            AddItem(new UserVO("jswanson", "Joe", "Swanson", "jswanson@web123.com"));
            AddItem(new UserVO("cbrown", "Cleveland", "Brown", "cleveland@web123.com"));
        }
        /// <summary>
        /// Returns a list with UserVOs
        /// </summary>
        public IList<UserVO> Users
        {
            get { return (IList<UserVO>) Data; }
        }
        /// <summary>
        /// Adds an user to the Users list.
        /// </summary>
        /// <param name="user"></param>
        public void AddItem(UserVO user)
        {
            user.Id = iterator++;
            Users.Add(user);
        }
        /// <summary>
        /// Updates existing instances.
        /// </summary>
        /// <param name="user"></param>
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
        /// <summary>
        /// Deletes an existing instance
        /// </summary>
        /// <param name="user"></param>
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
