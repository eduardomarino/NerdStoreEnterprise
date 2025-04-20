using NSE.Core.DomainObjects;

namespace NSE.Cliente.API.Models
{
    public class Cliente : Entity, IAggregateRoot
    {
        public Cliente(string nome, string email, string cpf)
        {
            Nome = nome;
            Email = email;
            Cpf = cpf;
            Excluido = false;
        }

        // EF Relation
        protected Cliente() { }

        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public bool Excluido { get; private set; }
        public Endereco Endereco { get; private set; }
    }
}
//public static string ApenasNumeros(this string str, string input)
//{
//    if (!str.All(char.IsDigit))
//    {
//        // String only contains numbers
//        throw new ArgumentException("Não contém apenas numeros");
//    }

//    return new string(input.Where(char.IsDigit).ToArray());
//}