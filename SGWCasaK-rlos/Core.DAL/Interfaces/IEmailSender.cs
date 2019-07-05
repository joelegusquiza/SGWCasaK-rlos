using Core.DTOs.Emails;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL.Interfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(EmailModel model);
    }
}
