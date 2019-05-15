using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CRMCore.Helpers
{
    public static class DecimalHelper
    {
        public static string DoPrice(decimal price)
        {
            if((price % 1) == 0) {
                return price.ToString("### ### ###", CultureInfo.CurrentCulture);
            }
            else
            {
                return price.ToString("### ### ###.00", CultureInfo.CurrentCulture);
            }
            
        }
    }
}
