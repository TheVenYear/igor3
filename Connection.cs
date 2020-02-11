using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karpenko3
{
    class Connection
    {
        private List<Person> Accounts = new List<Person>();
        public Person AuthorizedAcc;
        public Connection()
        {
            Accounts.Add(new Person("admin", "228-1337", "admin") { Power = 15});
        }
        public int Register(string login,string cellPhone, string password)
        {
            var newPerson = new Person(login,cellPhone, password);
            if (password == cellPhone)
            {
                return 2;
            }
            else if (FindByName(login) == null)
            {
                Accounts.Add(newPerson);
                AuthorizedAcc = newPerson;
                return 1;
            }
            return 0;

        }

        public bool Auth(string login, string password)
        {
            Person currentAcc;
            if (FindByName(login) == null)
            {
                return false;
            }
            else
            {
                currentAcc = FindByName(login);
                if (currentAcc.Password == password)
                {
                    AuthorizedAcc = currentAcc;
                    return true;
                }
            }
            return false;

        }

        public bool CheckCellPhone(string cellPhone)
        {
            var status = true;
            foreach (var item in cellPhone)
            {
                if (char.IsNumber(item) || item == '-')
                {
                    status = true;
                }
                else
                {
                    status = false;
                    break;
                }
            }
            return status;
        }

        private Person FindByName(string login)
        {
            foreach (var acc in Accounts)
            {
                if (acc.Login == login)
                {
                    return acc;
                }
            }
            return null;
        }

        public bool Delete(string login)
        {
            if (AuthorizedAcc.Power > 10 && FindByName(login) != AuthorizedAcc)
            {
                Accounts.Remove(FindByName(login));
                return true;
            }
            return false;
        }

        public bool SetLevel(string login, string power)
        {
            int toPower = 0;
            if (int.TryParse(power, out toPower) && toPower >= 0)
            {
                FindByName(login).Power = toPower;
                return true;
            }
            return false;

        }

        public Dictionary<string, string> ShowAccounts()
        {
            if (AuthorizedAcc.Power > 5)
            {
                var dict = new Dictionary<string, string>();
                foreach (var acc in Accounts)
                {
                    dict.Add(acc.Login, acc.Password);
                }
                return dict;
            }
            return null;
        }

    }
}
