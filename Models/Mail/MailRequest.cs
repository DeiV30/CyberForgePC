using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace  cyberforgepc.Models.Mail
{
    public class MailRequest
    {
        public string Email { get; set; }
        public Dictionary<string, object> Model { get; set; }
    }
}
