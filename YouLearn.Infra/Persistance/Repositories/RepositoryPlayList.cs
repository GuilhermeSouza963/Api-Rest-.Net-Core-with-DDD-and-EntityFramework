using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Infra.Persistance.EF;

namespace YouLearn.Infra.Persistance.Repositories
{
    public class RepositoryPlayList : IRepositoryPlayList
    {
        private readonly YouLearnContext _context;

        public RepositoryPlayList(YouLearnContext context)
        {
            _context = context;
        }

        public PlayList Add(PlayList playList)
        {
            _context.PlayLists.Add(playList);
            return playList;
        }

        public void Delete(PlayList playList)
        {
            _context.PlayLists.Remove(playList);
        }

        public IEnumerable<PlayList> Listar(Guid idUsuario)
        {
            return _context.PlayLists.Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public PlayList Obter(Guid idPlayList)
        {
            return _context.PlayLists.FirstOrDefault(x => x.Id == idPlayList);
        }
    }
}
