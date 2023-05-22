using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdAndServManagement.utils
{
    public static class GeneralUtils
    {

        public static bool CheckIfFileValid(string? file)
        {
            if (string.IsNullOrEmpty(file)) return false;
            return true;
        }

    }
}
