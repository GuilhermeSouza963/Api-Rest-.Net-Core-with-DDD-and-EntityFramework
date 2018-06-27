using System;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Args.Usuario
{
    public class AutenticarUsuarioResponse
    {
        public Guid Id { get; set; }
        public string PrimeiroNome { get; set; }

        // conversao explicita do servico usuario
        public static explicit operator AutenticarUsuarioResponse(Entities.Usuario entidade)
        {
            return new AutenticarUsuarioResponse()
            {
                Id = entidade.Id,
                PrimeiroNome = entidade.Nome.PrimeiroNome
            };
        }
    }
}
