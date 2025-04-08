using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Models.Email;

namespace TaskManager.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(Models.Email.Email email);
}
