using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Utils
{
    public class Utils
    {

        public static bool IsBase64(string base64)
        {
            if(string.IsNullOrWhiteSpace(base64)) return false;

            try
            {
                Convert.FromBase64String(base64);
                return true;
            }
            catch
            {
                return false;
            }


        }

    }
}
