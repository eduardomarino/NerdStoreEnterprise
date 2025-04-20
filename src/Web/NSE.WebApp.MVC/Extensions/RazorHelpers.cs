using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc.Razor;

namespace NSE.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string HashEmailForGravatar(this RazorPage page, string email)
        {
            // O serviço Gravatar utiliza o hash do e-mail para exibir avatares personalizados.
            // Ex: https://www.gravatar.com/avatar/23463b99b62a72f26ed677cc556c44e8
            var md5Hasher = MD5.Create(); // Cria uma instância do algoritmo de hash MD5.
            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(email)); // Converte o e-mail em um array de bytes, necessário para calcular o hash. E calcula o hash MD5 do array de bytes.
            var sBuilder = new StringBuilder(); // Usado para construir a string final do hash.
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2")); // Converte cada byte em uma representação hexadecimal de 2 dígitos. E adiciona cada byte convertido à string final.
            }
            return sBuilder.ToString(); // Retorna o hash MD5 como uma string hexadecimal.
        }

        public static string FormatoMoeda(this RazorPage page, decimal valor)
        {
            return valor > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", valor) : "Gratuito";
        }

        public static string MensagemEstoque(this RazorPage page, int quantidade)
        {
            return quantidade > 0 ? $"Apenas {quantidade} em estoque!" : "Produto esgotado!";
        }
    }
}