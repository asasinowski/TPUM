using System.Collections.Generic;
using System.Threading;

namespace Data.Model
{
    public class Employee
    {
        public string name { get; }
        public bool isBusy { get; set; }

        public Employee(string name)
        {
            this.name = name;
            this.isBusy = false;
        }

        public Employee(string name, bool isBusy)
        {
            this.name = name;
            this.isBusy = isBusy;
        }

        public override string ToString()
        {
            string employeeInfo = "\n";
            employeeInfo += "\tName       : " + name;
            employeeInfo += "\tIs Busy    : " + isBusy;
            return employeeInfo;
        }

        public override bool Equals(object value)
        {
            if ((value == null) || !this.GetType().Equals(value.GetType()))
            {
                return false;
            }

            Employee employee = value as Employee;

            return (employee != null)
                && (name == employee.name);
        }

        public override int GetHashCode()
        {
            var hashCode = -1068211290;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(name);
            hashCode = hashCode * -1521134295 + isBusy.GetHashCode();
            return hashCode;
        }
    }
}
