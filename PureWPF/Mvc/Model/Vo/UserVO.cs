using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureWPF.Mvc.Model.Vo
{
    public class UserVO
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string GivenName => LastName + ", " + FirstName;

        public UserVO(string userName, string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Email = email;
            Password = password;
        }
    }
}
