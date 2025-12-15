using System;

namespace Clientes.WebApi.Models
{
    public class Cliente
    {
        public long ID { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime Nascimento { get; set; }
        public string Email { get; set; }
        public DateTime CriadoEm { get; set; }
    }
}