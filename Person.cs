using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karpenko3
{
    public class Person
    {
        public string Login;
        public bool AuthStatus = false;
        public string Password;
        public string cellPhone;
        public int Power = 0;
        public Person(string Login, string cellPhone, string Password)
        {
            this.Login = Login;
            this.cellPhone = cellPhone;
            this.Password = Password;
        }
    }
}
