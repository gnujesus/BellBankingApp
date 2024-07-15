using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Helpers
{
    public static class AccountNumberHelper
    {
        public static string Generate() 
        {
            int number;

            Random random = new ();
            number = random.Next(1111, 999999999);

            return number.ToString();
        }
    }
}
