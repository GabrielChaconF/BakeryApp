
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Cryptography;
using System.Text;


namespace BakeryApp_v1.Recursos
{
    public class Utilidades
    {

        public static string EncriptarClave(string clave)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {

                Encoding encoding = Encoding.UTF8;
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(clave));
                

                foreach (byte b in result)
                    sb.Append(b.ToString("X2"));
            }

            return sb.ToString();
        }

    }
}
