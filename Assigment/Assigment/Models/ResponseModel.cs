using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assigment.Models
{
    public class ResponseModel
    {
        public ResponseModel(object Data = null, string UserMsg = "", string SysMsg = "", int StatusCode = 200)
        {
            data = Data;
            userMsg = UserMsg;
            sysMsg = SysMsg;
            statusCode = StatusCode;
        }

        public object data { get; set; }
        public string userMsg { get; set; }
        public string sysMsg { get; set; }
        public int statusCode { get; set; }
    }
}
