using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WPF_MaintainEmployeeDetails.Utilities
{
    public static class Common
    {
        public static bool IsNumeric(this string input)=>
            int.TryParse(input, out _);
    }
}
