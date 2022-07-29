using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Phoenix.Models
{
    public class EC2Response
    {
        public HttpStatusCode Status
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }
}
