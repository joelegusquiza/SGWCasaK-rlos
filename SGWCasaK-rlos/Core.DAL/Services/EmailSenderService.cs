using Core.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.DTOs.Emails;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net;

namespace Core.DAL.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly IConfigurationRoot _configuration;

        public EmailSenderService(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendEmailAsync(EmailModel model)
        {
            var apiKey = _configuration["SendGrid:APIKey"];
            var client = new SendGridClient(apiKey);
            var origen = new EmailAddress(model.From, model.FromName);
            var destino = new EmailAddress(model.To, model.ToName);
            var msg = MailHelper.CreateSingleEmail(origen, destino, model.Subject, model.Text, model.HtmlContent);
            var response = await client.SendEmailAsync(msg);
            return response.StatusCode == HttpStatusCode.Accepted;
        }
    }
}
