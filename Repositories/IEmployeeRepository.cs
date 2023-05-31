using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPF_MaintainEmployeeDetails.Models;

namespace WPF_MaintainEmployeeDetails.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> LoadEmployeeDetails(string uri);
        public List<Employee> GetEmployeeDetails(string uri, string strSearch);
        public Task<HttpResponseMessage> AddEmployee(string uri, Employee employee);
        public Task<HttpResponseMessage> UpdateEmployeeDetails(string uri, Employee employee);
        public Task<HttpResponseMessage> DeleteEmployeeDetails(string uri, int empID);


    }
}
