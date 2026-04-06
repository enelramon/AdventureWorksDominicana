using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

public class SmtpEmailSender : IEmailSender<AdventureWorksDominicana.Data.Models.AspNetUser>
{
    // PON AQUÍ TUS CREDENCIALES
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _fromEmail = "joelescanoguzman@gmail.com";
    private readonly string _password = "ninc rrrs niut rxav"; // App Password de 16 chars (los espacios son válidos)


    // Aquí está el diseño mágico HTML
    private string ObtenerPlantillaChula(string titulo, string mensaje, string textoBoton, string link)
    {
        return $@"
        <div style='font-family: Arial, sans-serif; max-width: 600px; margin: 0 auto; border: 1px solid #e0e0e0; border-radius: 10px; overflow: hidden; box-shadow: 0 4px 8px rgba(0,0,0,0.05);'>
            <div style='background-color: #004b87; padding: 25px; text-align: center;'>
                <h2 style='color: #ffffff; margin: 0; letter-spacing: 1px;'>AdventureWorks</h2>
            </div>
            <div style='padding: 30px; background-color: #ffffff; text-align: center;'>
                <h3 style='color: #333333;'>{titulo}</h3>
                <p style='color: #555555; font-size: 16px; line-height: 1.6; margin-bottom: 30px;'>
                    {mensaje}
                </p>
                <div>
                    <a href='{link}' style='background-color: #e31837; color: white; padding: 14px 28px; text-decoration: none; border-radius: 6px; font-weight: bold; font-size: 16px; display: inline-block;'>
                        {textoBoton}
                    </a>
                </div>
                <hr style='border: none; border-top: 1px solid #eeeeee; margin: 30px 0;' />
                <p style='color: #999999; font-size: 12px;'>Si no puedes hacer clic en el botón, copia y pega el siguiente enlace en tu navegador:</p>
                <p style='color: #004b87; font-size: 11px; word-break: break-all;'>{link}</p>
            </div>
            <div style='background-color: #f7f7f7; padding: 15px; text-align: center; color: #aaaaaa; font-size: 12px;'>
                &copy; {DateTime.Now.Year} AdventureWorks Dominicana. No respondas a este correo automátizado.
            </div>
        </div>";
    }

    public async Task SendConfirmationLinkAsync(AdventureWorksDominicana.Data.Models.AspNetUser user, string email, string confirmationLink)
    {
        string htmlMessage = ObtenerPlantillaChula(
            titulo: "¡Bienvenido a la familia!",
            mensaje: "Gracias por registrarte en nuestra plataforma de compras. Solo estás a un paso de acceder a nuestro inventario exclusivo. Por favor, verifica tu dirección de correo.",
            textoBoton: "Confirmar mi Cuenta",
            link: confirmationLink
        );

        await SendEmailAsync(email, "Confirma tu cuenta de AdventureWorks", htmlMessage);
    }

    public async Task SendPasswordResetLinkAsync(AdventureWorksDominicana.Data.Models.AspNetUser user, string email, string resetLink)
    {
        string htmlMessage = ObtenerPlantillaChula(
            titulo: "Recuperación de Acceso",
            mensaje: "Hemos recibido una solicitud para restablecer tu contraseña. Si fuiste tú, haz clic en el botón de abajo para cambiarla de forma segura. Si no solicitaste esto, puedes ignorar este correo.",
            textoBoton: "Restablecer Contraseña",
            link: resetLink
        );

        await SendEmailAsync(email, "Recuperación de Contraseña - AdventureWorks", htmlMessage);
    }

    public Task SendPasswordResetCodeAsync(AdventureWorksDominicana.Data.Models.AspNetUser user, string email, string resetCode)
    {
        // Esta interfaz se usa raramente, pero es requerida. La dejamos completada vacía.
        return Task.CompletedTask;
    }

    private async Task SendEmailAsync(string toEmail, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(_fromEmail, "AdventureWorks Bot"),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true // CRÍTICO: Esto hace que el correo no se vea como puro texto
        };
        mailMessage.To.Add(toEmail);

        // Cliente SMTP para inyectarse a los servidores de Google
        using var smtpClient = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_fromEmail, _password),
            EnableSsl = true
        };

        await smtpClient.SendMailAsync(mailMessage);
    }
}
