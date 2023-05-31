using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_MaintainEmployeeDetails.Models;
using WPF_MaintainEmployeeDetails.Repositories;

namespace WPF_MaintainEmployeeDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client;
        IEmployeeRepository employeeRepository;
        string applicationURI= ConfigurationManager.AppSettings["ApiUrl"].ToString();
        public MainWindow(IEmployeeRepository _employeeRepository,HttpClient _client)
        {
            InitializeComponent();
            this.employeeRepository = _employeeRepository;
            this.client = _client;
            LoadEmployeeDetails();
        }
     
        public void ApiConfiguration()
        {
            client.BaseAddress = new Uri(applicationURI);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(ConfigurationManager.AppSettings["AuthenticationKey"]);
        }

        public async void LoadEmployeeDetails()
        {
           dgEmployeeDtls.ItemsSource = await employeeRepository.LoadEmployeeDetails(applicationURI);
        }
        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Validate.RequiredField("Employee Name", txtName.Text) &&
             Validate.RequiredField("Employee Email", txtEmail.Text))
            {
                var user = new Employee()
                {
                    id = Convert.ToInt32(txtId.Text),
                    name = txtName.Text,
                    email = txtEmail.Text,
                    gender = Convert.ToString(cbGender.SelectedItem),
                    status = (bool)rbActive.IsChecked ? rbActive.Content.ToString() : rbInactive.Content.ToString(),
                };
                if (user.id == 0)
                {
                    HttpResponseMessage response = employeeRepository.AddEmployee(applicationURI,user).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                        MessageBox.Show("Employee details saved successfully!!!");
                    else
                        MessageBox.Show("Error Response from API: " + response.ReasonPhrase);
                    
                }
                else
                {
                    HttpResponseMessage response = employeeRepository.UpdateEmployeeDetails(applicationURI, user).Result;
                    if (response.StatusCode == HttpStatusCode.OK)
                        MessageBox.Show("Employee details updated successfully!!!");
                    else
                        MessageBox.Show("Error Response from API: " + response.ReasonPhrase);
                }
            }
        }

        void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            txtId.Text = Convert.ToString(employee.id);
            txtName.Text=employee.name;
            txtEmail.Text=employee.email;
            cbGender.SelectedIndex = Convert.ToString(employee.gender).ToLower() == "female" ? 1 : 0;
            if (employee.status == "active")
                rbActive.IsChecked = true;
            else if (employee.status == "inactive") 
                rbInactive.IsChecked = true;
        }

        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = ((FrameworkElement)sender).DataContext as Employee;
            HttpResponseMessage response = employeeRepository.DeleteEmployeeDetails(applicationURI,employee.id).Result;
            if (response.StatusCode == HttpStatusCode.OK) { MessageBox.Show("Record deleted successfully!!!"); } else { MessageBox.Show("Error Response from API: " + response.ReasonPhrase); }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            List<Employee> lstSearchData = new List<Employee>();
            lstSearchData = employeeRepository.GetEmployeeDetails(applicationURI, txtSearch.Text);
            if (lstSearchData.Count > 0)
                dgEmployeeDtls.ItemsSource = lstSearchData;
            else
                MessageBox.Show("Data not found");
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = string.Empty;
            txtName.Text = string.Empty;    
            txtEmail.Text = string.Empty;
            cbGender.SelectedIndex = 0;
            rbActive.IsChecked = true;
            rbInactive.IsChecked = false;
            txtId.Text=string.Empty;
        }
    }
}