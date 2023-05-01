using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin
{
    class EmployeesData
    {
        public string Emp_name;
        public string Emp_type;
     
        public EmployeesData()
        {
            string Emp_name;
            string Emp_type;
        }
      
        public EmployeesData(string Emp_name, string Emp_type)
        {
            this.Emp_name = Emp_name;
            this.Emp_type = Emp_type;
        }

    }
}
