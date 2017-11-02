using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureWPF.Mvc.Model.Vo
{
    /// <summary>
    /// Value Object for User
    /// </summary>
    public class UserVO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }


        public string GivenName => LastName + ", " + FirstName;



        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName); }
        }

        public UserVO(string userName, string firstName, string lastName, string email, int id = 0)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;

        }
    }
}
