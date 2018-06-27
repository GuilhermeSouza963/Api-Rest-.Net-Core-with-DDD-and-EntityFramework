using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Interfaces.Repositories
{
    public interface IRepositoryVideo
    {
        
        void Add(Video video);

        IEnumerable<Video> ListaVideos();

        IEnumerable<Video> ListaVideos(string tags);

        IEnumerable<Video> ListaVideos(Guid idPlayList);

        bool ExisteCanalAssociado(Guid idCanal);

        bool ExistePlayListAssociada(Guid idPlayList);
    }
}
