using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArticoliWebService.Dtos
{
    public class InfoMsg
    {
        public DateTime data { get; set; }
        public string message { get; set; }

        public InfoMsg(DateTime Data, String Message)
        {
            this.data = Data;
            this.message = Message;
        }
    }
}