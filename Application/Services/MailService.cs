using Application.Dto;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MailService : IMailService
    {

        public string GetChangeEmailBody(string link, string email)
        {

            return string.Format(@"<!DOCTYPE html>
<html>
<head>

  <meta charset='utf-8'>
  <meta http-equiv='x-ua-compatible' content='ie=edge'>
  <title> Email Confirmation </title>
  <meta name='viewport' content='width=device-width, initial-scale=1'>

</head>
<body style='background-color: #e9ecef;'>
 

   <!--start preheader-->
 
   <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
    Witaj! Otrzymaliśmy prośbe zmianę adresu e-mail.
  </div>
  <!-- end preheader -->

  <!-- start body -->
  <table border='0' cellpadding='0' cellspacing='0' width='100%'>

    <!-- start logo -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' valign='top' style='padding: 36px 24px;'>
              
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end logo -->

    <!-- start hero -->
    <tr>
      <td align='center' style='border-radius: 10px;' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' bgcolor='#ffffff' style='padding: 36px 24px 0; border-radius: 10px 10px 0px 0px;'>
                <h1 style='font-size: 48px; font-weight: 400; margin: 2;'> Zmiana adresu e-mail </h1> <img src='https://img.icons8.com/clouds/100/000000/handshake.png' width='125' height='120' style='display: block; border: 0px;' />
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end hero -->

    <!-- start copy block -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; border-radius: 0px 0px 4px 4px;'>

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Dostaliśmy prośbę zmianę adresu email na: {1}.<br> Jeżeli wysłałeś/aś prośbę o zmianę adresu e-mail. Kliknij przycisk poniżej.</p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start button -->
          <tr>
            <td align='left' bgcolor='#ffffff'>
              <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                <tr>
                  <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                    <table border='0' cellpadding='0' cellspacing='0'>
                      <tr>
                        <td align='center' bgcolor='#ec43434' style='border-radius: 6px;'>
                          <a href='{0}' target='_blank' style='display: inline-block; padding: 16px 36px; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Potwierdź</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <!-- end button -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Jeśli przycisk nie działa, skopiuj link i wklej do przeglądarki:</p>
              <p style='margin: 0;'><a href='{0}' target='_blank'>{0}</a></p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px; border-radius: 0px 0px 10px 10px; border-bottom: 3px solid #d4dadf'>
                <p style='margin: 0;'> Miłego korzystania z aplikacji,<br> Zespół BertMat</p>
            </td>
          </tr>
          <!-- end copy -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end copy block -->

    <!-- start footer -->
    <tr>
      <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

          <!-- start permission -->
          <tr>
            <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-size: 14px; line-height: 20px; color: #666;'>
              <p style='margin: 0;'> Jeśli masz pytania, nie krępuj się i wyślij do nas maila!</p>
            </td>
          </tr>
          <!-- end permission -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end footer -->

  </table>
  <!-- end body -->

</body>
</html>", link, email);
        }
        public string GetMailBody(string link, string email)
        {

            return string.Format(@"<!DOCTYPE html>
<html>
<head>

  <meta charset='utf-8'>
  <meta http-equiv='x-ua-compatible' content='ie=edge'>
  <title> Email Confirmation </title>
  <meta name='viewport' content='width=device-width, initial-scale=1'>

</head>
<body style='background-color: #e9ecef;'>
 

   <!--start preheader-->
 
   <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
    Witaj w ChildScheduler! Cieszymy się, że nam zaufałeś!
  </div>
  <!-- end preheader -->

  <!-- start body -->
  <table border='0' cellpadding='0' cellspacing='0' width='100%'>

    <!-- start logo -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' valign='top' style='padding: 36px 24px;'>
              
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end logo -->

    <!-- start hero -->
    <tr>
      <td align='center' style='border-radius: 10px;' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' bgcolor='#ffffff' style='padding: 36px 24px 0; border-radius: 10px 10px 0px 0px;'>
                <h1 style='font-size: 48px; font-weight: 400; margin: 2;'> Witaj w ChildScheduler! </h1> <img src='https://img.icons8.com/clouds/100/000000/handshake.png' width='125' height='120' style='display: block; border: 0px;' />
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end hero -->

    <!-- start copy block -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; border-radius: 0px 0px 4px 4px;'>

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Cieszymy się, że chcesz poprawić życie swojej rodziny i nam zaufałeś/aś. <br>Aby potwierdzić rejestrację konta-kliknij przycisk poniżej.</p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start button -->
          <tr>
            <td align='left' bgcolor='#ffffff'>
              <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                <tr>
                  <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                    <table border='0' cellpadding='0' cellspacing='0'>
                      <tr>
                        <td align='center' bgcolor='#ec43434' style='border-radius: 6px;'>
                          <a href='{0}' target='_blank' style='display: inline-block; padding: 16px 36px; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Potwierdź</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <!-- end button -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Jeśli przycisk nie działa, skopiuj link i wklej do przeglądarki:</p>
              <p style='margin: 0;'><a href='{0}' target='_blank'>{0}</a></p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px; border-radius: 0px 0px 10px 10px; border-bottom: 3px solid #d4dadf'>
                <p style='margin: 0;'> Miłego korzystania z aplikacji,<br> Zespół BertMat</p>
            </td>
          </tr>
          <!-- end copy -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end copy block -->

    <!-- start footer -->
    <tr>
      <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

          <!-- start permission -->
          <tr>
            <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-size: 14px; line-height: 20px; color: #666;'>
              <p style='margin: 0;'> Jeśli masz pytania, nie krępuj się i wyślij do nas maila!</p>
            </td>
          </tr>
          <!-- end permission -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end footer -->

  </table>
  <!-- end body -->

</body>
</html>", link, email);/*
            return string.Format(@"<div style='text-align:center;'>
                                    <h1>Witamy w ChildScheduler</h1>
                                    <h3>Kliknij na poniższy przycisk aby potwierdzić utworzenie konta.</h3>
                                    <h5>Jeśli to nie Ty utworzyłeś konto, zignrouj tę wiadomość..</h5>
                                    <form method='post' action='{0}' style='display: inline;'>
                                      <button type = 'submit' style=' display: block;
                                                                    text-align: center;
                                                                    font-weight: bold;
                                                                    background-color: #008CBA;
                                                                    font-size: 16px;
                                                                    border-radius: 10px;
                                                                    color:#ffffff;
                                                                    cursor:pointer;
                                                                    width:100px;
                                                                    padding:10px;'>
                                        Potwierdź
                                      </button>
                                    </form>
                                </div>", link, email);*/
        }
        public string GetInvitationBody(string link, string email, string familyName)
        {

            return string.Format(@"<!DOCTYPE html>
<html>
<head>

  <meta charset='utf-8'>
  <meta http-equiv='x-ua-compatible' content='ie=edge'>
  <title> Email Confirmation </title>
  <meta name='viewport' content='width=device-width, initial-scale=1'>

</head>
<body style='background-color: #e9ecef;'>
 

   <!--start preheader-->
 
   <div class='preheader' style='display: none; max-width: 0; max-height: 0; overflow: hidden; font-size: 1px; line-height: 1px; color: #fff; opacity: 0;'>
    Witaj {1}! Zostałeś zaproszony do rodziny!
  </div>
  <!-- end preheader -->

  <!-- start body -->
  <table border='0' cellpadding='0' cellspacing='0' width='100%'>

    <!-- start logo -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' valign='top' style='padding: 36px 24px;'>
              
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end logo -->

    <!-- start hero -->
    <tr>
      <td align='center' style='border-radius: 10px;' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>
          <tr>
            <td align='center' bgcolor='#ffffff' style='padding: 36px 24px 0; border-radius: 10px 10px 0px 0px;'>
                <h1 style='font-size: 48px; font-weight: 400; margin: 2;'> Witaj w ChildScheduler! </h1> <img src='https://img.icons8.com/clouds/100/000000/handshake.png' width='125' height='120' style='display: block; border: 0px;' />
            </td>
          </tr>
        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end hero -->

    <!-- start copy block -->
    <tr>
      <td align='center' bgcolor='#e9ecef'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px; border-radius: 0px 0px 4px 4px;'>

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Cieszymy się, że chcesz poprawić życie swojej rodziny i nam zaufałeś/aś. <br>Zostałeś/aś zaproszony do rodziny {2}.</p>
              <p style='margin: 0;'> Aby zaakceptować zaproszenie kliknij przycisk poniżej.</p>              
            </td>
          </tr>
          <!-- end copy -->

          <!-- start button -->
          <tr>
            <td align='left' bgcolor='#ffffff'>
              <table border='0' cellpadding='0' cellspacing='0' width='100%'>
                <tr>
                  <td align='center' bgcolor='#ffffff' style='padding: 12px;'>
                    <table border='0' cellpadding='0' cellspacing='0'>
                      <tr>
                        <td align='center' bgcolor='#ec43434' style='border-radius: 6px;'>
                          <a href='{0}' target='_blank' style='display: inline-block; padding: 16px 36px; font-size: 16px; color: #ffffff; text-decoration: none; border-radius: 6px;'>Potwierdź</a>
                        </td>
                      </tr>
                    </table>
                  </td>
                </tr>
              </table>
            </td>
          </tr>
          <!-- end button -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px;'>
              <p style='margin: 0;'> Jeśli przycisk nie działa, skopiuj link i wklej do przeglądarki:</p>
              <p style='margin: 0;'><a href='{0}' target='_blank'>{0}</a></p>
            </td>
          </tr>
          <!-- end copy -->

          <!-- start copy -->
          <tr>
            <td align='left' bgcolor='#ffffff' style='padding: 24px; font-size: 16px; line-height: 24px; border-radius: 0px 0px 10px 10px; border-bottom: 3px solid #d4dadf'>
                <p style='margin: 0;'> Miłego korzystania z aplikacji,<br> Zespół BertMat</p>
            </td>
          </tr>
          <!-- end copy -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end copy block -->

    <!-- start footer -->
    <tr>
      <td align='center' bgcolor='#e9ecef' style='padding: 24px;'>
        <!--[if (gte mso 9)|(IE)]>
        <table align='center' border='0' cellpadding='0' cellspacing='0' width='600'>
        <tr>
        <td align='center' valign='top' width='600'>
        <![endif]-->
        <table border='0' cellpadding='0' cellspacing='0' width='100%' style='max-width: 600px;'>

          <!-- start permission -->
          <tr>
            <td align='center' bgcolor='#e9ecef' style='padding: 12px 24px; font-size: 14px; line-height: 20px; color: #666;'>
              <p style='margin: 0;'> Jeśli masz pytania, nie krępuj się i wyślij do nas maila!</p>
            </td>
          </tr>
          <!-- end permission -->

        </table>
        <!--[if (gte mso 9)|(IE)]>
        </td>
        </tr>
        </table>
        <![endif]-->
      </td>
    </tr>
    <!-- end footer -->

  </table>
  <!-- end body -->

</body>
</html>", link, email, familyName);/*
            return string.Format(@"<div style='text-align:center;'>
                                    <h1>Witamy w ChildScheduler</h1>
                                    <h3>Kliknij na poniższy przycisk aby potwierdzić utworzenie konta.</h3>
                                    <h5>Jeśli to nie Ty utworzyłeś konto, zignrouj tę wiadomość..</h5>
                                    <form method='post' action='{0}' style='display: inline;'>
                                      <button type = 'submit' style=' display: block;
                                                                    text-align: center;
                                                                    font-weight: bold;
                                                                    background-color: #008CBA;
                                                                    font-size: 16px;
                                                                    border-radius: 10px;
                                                                    color:#ffffff;
                                                                    cursor:pointer;
                                                                    width:100px;
                                                                    padding:10px;'>
                                        Potwierdź
                                      </button>
                                    </form>
                                </div>", link, email);*/
        }

        public async Task<string> SendMail(MailClass mailClass)
        {
            try
            {
                using(MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(mailClass.FromMailId, "ChildScheduler");
                    mailClass.ToMailIds.ForEach(p => mail.To.Add(p));
                    mail.Subject = mailClass.Subject;
                    mail.Body = mailClass.Body;
                    mail.IsBodyHtml = mailClass.IsBodyHtml;
                    mailClass.Attachments.ForEach(p => mail.Attachments.Add(new Attachment(p)));

                    using(SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(mailClass.FromMailId, mailClass.FromMailIdPassword);
                        smtp.EnableSsl = true;
                        await smtp.SendMailAsync(mail);
                        return "Mail sent";
                    }
                }
            }
            catch(Exception ex)
            { return ex.Message; }
        }
    }
}
