using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Args.PlayList
{
    public class PlayListResponse
    {
        public Guid idPlayList { get; set; }
        public string Nome { get; set; }

        public static explicit operator PlayListResponse(Entities.PlayList entidade)
        {
            return new PlayListResponse()
            {
                idPlayList = entidade.Id,
                Nome = entidade.Nome
            };

        }
    }
}
