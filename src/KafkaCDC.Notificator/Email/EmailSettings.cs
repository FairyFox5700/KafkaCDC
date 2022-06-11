
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaCDC.Email
{
    public class EmailSettings
    {
        public int MailPort { get; set; }
        public string MailServer { get; set; }
        public string AdminEmail { get; set; }
        public string AdminPassword { get; set; }
        public bool UseSsl { get; set; }
    }
}
