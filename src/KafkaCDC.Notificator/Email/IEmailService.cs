﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaCDC.Email
{
    public interface IEmailService
    {
        Task<bool> SendMessage(MailModel model);
    }
}