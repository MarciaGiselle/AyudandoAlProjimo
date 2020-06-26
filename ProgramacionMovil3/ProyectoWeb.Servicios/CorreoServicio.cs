
using System.Net.Mail;

namespace ProyectoWeb.Servicios
{
    public class CorreoServicio
    {
        public bool enviarActivador(string email, string token)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(email);
            mail.From = new MailAddress("equipoMobile@gmail.com", "equipoMobile@gmail.com", System.Text.Encoding.UTF8);
            mail.Subject = "Validacion de usuario-Aplicacion Ayudando al Projimo";
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = @"<a href='http://localhost:54725/Perfil/ValidarUsuario?token="+ token + "' >Validar Cuenta</a>";
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("solucionesproxy@gmail.com", "Taller123!");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Send(mail);
            return false;
        }
    }
}
