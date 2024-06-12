using System;
using System.Net.Mail;
using System.Net;

namespace ChocoLink.Infra.EmailService
{
    public static class Email
    {
        public static void Enviar(string assunto, string corpoEmail, string destinatario, EmailConfig emailConfig)
        {
            // Verificações de nulidade e logs
            if (string.IsNullOrEmpty(assunto))
                throw new ArgumentNullException(nameof(assunto), "O assunto não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(corpoEmail))
                throw new ArgumentNullException(nameof(corpoEmail), "O corpo do e-mail não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(destinatario))
                throw new ArgumentNullException(nameof(destinatario), "O destinatário não pode ser nulo ou vazio.");
            if (emailConfig == null)
                throw new ArgumentNullException(nameof(emailConfig), "As configurações de e-mail não podem ser nulas.");
            if (string.IsNullOrEmpty(emailConfig.Host))
                throw new ArgumentNullException(nameof(emailConfig.Host), "O host do e-mail não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(emailConfig.Usuario))
                throw new ArgumentNullException(nameof(emailConfig.Usuario), "O usuário do e-mail não pode ser nulo ou vazio.");
            if (string.IsNullOrEmpty(emailConfig.Senha))
                throw new ArgumentNullException(nameof(emailConfig.Senha), "A senha do e-mail não pode ser nula ou vazia.");

            try
            {
                var client = new SmtpClient(emailConfig.Host, 2525)
                {
                    Credentials = new NetworkCredential(emailConfig.Usuario, emailConfig.Senha),
                    EnableSsl = true
                };
                client.Send("no-reply@example.com", destinatario, assunto, corpoEmail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                throw;
            }
        }
    }
}
