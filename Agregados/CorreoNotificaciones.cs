//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Agregados
{
    using System;
    using System.Collections.Generic;
    
    public partial class CorreoNotificaciones
    {
        public int IdCorreo { get; set; }
        public string Correo { get; set; }
        public string Contrasennia { get; set; }

        public bool SendEmail(string SendTo, string Subject, string Message)
        {
            bool R = false;

            try
            {

                if (!string.IsNullOrEmpty(SendTo) && !string.IsNullOrEmpty(Subject) && !string.IsNullOrEmpty(Message))
                {
                    System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();

                    email.From = new System.Net.Mail.MailAddress(this.Correo);
                    email.Subject = Subject;
                    email.Body = Message;
                    email.To.Add(SendTo);

                    email.IsBodyHtml = false;

                    System.Net.Mail.SmtpClient server = new System.Net.Mail.SmtpClient();
                    server.Host = "smtp.gmail.com";
                    server.Port = 587;

                    server.EnableSsl = true;
                    server.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;


                    Crypto crypto = new Crypto();
                    string pass = crypto.DesEncriptarPassword(this.Contrasennia);


                    server.Credentials = new System.Net.NetworkCredential(this.Correo, pass);
                    server.Send(email);
                    R = true;
                }

            }
            catch (Exception)
            {

                throw;
            }

            return R;

        }


    }
}
