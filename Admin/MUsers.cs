using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    class MUsers
    {
        public string name;
        public string password;
        public string role;
        public MUsers(string name, string password)
        {
            this.name = name;
            this.password = password;
        }
        public MUsers(string name, string password, string role)
        {
            this.name = name;
            this.password = password;
            this.role = role;
        }
        public bool IsAdmin()
        {
            if (role == "Admin")
            {
                return true;
            }
            return false;
        }

    }
}
