using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.Args.Usuario
{
    public class AdicionarUsuarioResponse
    {
        public AdicionarUsuarioResponse(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
