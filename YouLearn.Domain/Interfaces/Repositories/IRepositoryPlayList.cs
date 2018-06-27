using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Interfaces.Repositories
{
    public interface IRepositoryPlayList
    {
        IEnumerable<PlayList> Listar(Guid idUsuario);

        PlayList Add(PlayList playList);

        PlayList Obter(Guid idPlayList);

        void Delete(PlayList playList);

    }
}
