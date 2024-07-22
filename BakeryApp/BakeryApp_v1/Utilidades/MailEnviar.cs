

using System.Net.Mail;
using System.Net;
using System.Text;
using System.Net.Mime;
using BakeryApp_v1.Services;
using BakeryApp_v1.Models;

namespace BakeryApp_v1.Utilidades;

public class MailEnviar : IMailEnviar
{
    private SmtpClient client;
    private readonly IWebHostEnvironment ambiente;


    public MailEnviar(IWebHostEnvironment ambiente)
    {
        this.ambiente = ambiente;
   
    }


    public void Configurar()
    {
        client = new SmtpClient("smtp.gmail.com", 587);
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential("dulce.espiga2024@gmail.com", "xmmh dnjy zdsm yhnv");
    }

    public async Task<bool> EnviarCorreo(Persona persona, string asunto, string codigoRecuperacion)
    {
        try
        {
            Configurar();
            MailMessage mensaje = new MailMessage();
            mensaje.From = new MailAddress("dulce.espiga2024@gmail.com");
            mensaje.To.Add(persona.Correo);
            mensaje.Subject = asunto;
            mensaje.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();



            mailBody.AppendFormat("<img src='cid:imagenLocal' alt='Imagen Local' />");
            mailBody.AppendFormat("<h1>Código de Recuperación  </h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat($"<h2>{codigoRecuperacion} </h2>");

            mensaje.Body = mailBody.ToString();

            string htmlBody = mailBody.ToString();


            AlternateView htmlAlternativa = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);

            string imagen = ambiente.WebRootPath;
            imagen = Path.Combine(imagen, "img");
            imagen = Path.Combine(imagen, "Logo_Transparente.png");

            LinkedResource imagenEnviar = new LinkedResource(imagen);

            imagenEnviar.ContentId = "imagenLocal";
            imagenEnviar.ContentType = new ContentType(MediaTypeNames.Image.Png);


            htmlAlternativa.LinkedResources.Add(imagenEnviar);


            mensaje.AlternateViews.Add(htmlAlternativa);

           


            
            await client.SendMailAsync(mensaje);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }


}
