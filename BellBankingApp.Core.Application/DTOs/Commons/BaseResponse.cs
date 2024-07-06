using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.DTOs.Commons
{
    public class BaseResponse
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
