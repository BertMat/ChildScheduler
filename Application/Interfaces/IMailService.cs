using Application.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMailService
    {
        string GetMailBody(string link, string email);
        string GetChangeEmailBody(string link, string email);
        string GetInvitationBody(string link, string email, string familyName);
        Task<string> SendMail(MailClass mailClass);
    }
}
