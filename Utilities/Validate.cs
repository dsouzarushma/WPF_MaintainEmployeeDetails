using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_MaintainEmployeeDetails.Models
{
   public static class Validate
    {
        public static bool RequiredField(string fieldName, string fieldValue)
        {
            if (string.IsNullOrEmpty(fieldValue))
            {
                MessageBox.Show(fieldName + " cannot be blank.");
                return false;
            }
            else 
                return true;
        }
    }
}
