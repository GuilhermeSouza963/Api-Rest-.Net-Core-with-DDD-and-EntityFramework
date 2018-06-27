using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Args.Canal
{
    public class CanalResponse
    {
        public Guid IdCanal { get; set; }

        public string Nome { get; set; }

        public string UrlCanal { get; set; }

        public static explicit operator CanalResponse(Entities.Canal entidade)
        {
            return new CanalResponse()
            {
                IdCanal = entidade.Id,
                Nome = entidade.Nome,
                UrlCanal = entidade.UrlLogo
            };
        }
    }
}
