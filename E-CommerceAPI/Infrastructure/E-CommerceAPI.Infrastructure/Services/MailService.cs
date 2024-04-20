using E_CommerceAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            await SendMailAsync(new[] { to }, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new();
            mail.IsBodyHtml = isBodyHtml;
            foreach(var to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new(_configuration["Mail:Username"], "E_commerce",System.Text.Encoding.UTF8);
            SmtpClient smpt = new();
            smpt.Credentials = new NetworkCredential(_configuration["Mail:Username"], _configuration["Mail:Password"]);
            smpt.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smpt.EnableSsl = true;
            smpt.Host = _configuration["Mail:Host"];
            await smpt.SendMailAsync(mail);
        }

        public async Task SendPassworResetMailAsync(string to,string userId,string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine("Hello</br>If you have requested a new password, you can renew it from the link below.</br><strong><a target=\"_blank\"href=\"");
            mail.AppendLine(_configuration["AngularClientUrl"]);
            mail.AppendLine("/update-reset/");
            mail.AppendLine(userId);
            mail.AppendLine("/");
            mail.AppendLine(resetToken);
            mail.AppendLine("\">New Password Link..</a></strong><br><br><span style=\"font-size:12px;\">Not: Eğer bu mail tarafınızca geçekleştirilmediyse ciddye almayınız bu maili.</span><br>Saygılarımzla<br><br><br>E_commerce");
           await SendMailAsync(to,"şefre yenileme",mail.ToString());
        }
        public async Task SendComletedOrdeMailAsync(string to, string orderCode, DateTime orderDate, string userName)
        {
            string mail=$"Sayin {userName} Merhaba<br>"+$"{orderDate} vermiş olduğunuz {orderCode} kodlu spariş tamamlanmış ve kargoya veilmiştir..<br>" + $"hayrını gör..";
            await SendMailAsync(to, $"{orderCode} sipariş numaralı sipariş tamamlandı",mail);

        }
    }
}
