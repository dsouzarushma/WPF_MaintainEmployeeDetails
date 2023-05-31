using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;
using WPF_MaintainEmployeeDetails.Models;

namespace WPF_MaintainEmployeeDetails.Repositories
{
    public class EmployeeRepository: IEmployeeRepository
    {
        HttpClient client;
        public EmployeeRepository(HttpClient _client)
        {
            client = _client;
        }
        public async Task<List<Employee>> LoadEmployeeDetails(string uri)
        {
            var response = await client.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<List<Employee>>(response).ToList();
        }

        public List<Employee> GetEmployeeDetails(string uri,string strSearch)
        {
            var response = client.GetStringAsync(uri +"?first_name="+ strSearch);
            return JsonConvert.DeserializeObject<List<Employee>>(response.Result).ToList(); ;
        }
        public Task<HttpResponseMessage> AddEmployee(string uri, Employee employee)
        {
            var response = client.PostAsJsonAsync(uri, employee);
            return response;
        }
        public Task<HttpResponseMessage> UpdateEmployeeDetails(string uri, Employee employee)
        {
            var response = client.PutAsJsonAsync(uri +"/"+ employee.id, employee);
            return response;
        }
        public Task<HttpResponseMessage> DeleteEmployeeDetails(string uri, int employeeId)
        {
            var response = client.DeleteAsync(uri + "/" + employeeId);
            return response ;
        }
    }
}