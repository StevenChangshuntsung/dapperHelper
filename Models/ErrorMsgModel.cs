using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dapperHelper.Models
{
    public class ErrorMsgModel
    {
        public string Message { get; set; }
        public int LineNumber { get; set; }
        public string Source { get; set; }
        public string Procedure { get; set; }
    }
}
