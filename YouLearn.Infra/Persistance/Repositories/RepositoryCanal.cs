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
    public class RepositoryCanal : IRepositoryCanal
    {
        private readonly YouLearnContext _context;

        public RepositoryCanal(YouLearnContext context)
        {
            _context = context;
        }

        public Canal Add(Canal canal)
        {
            _context.Canais.Add(canal);
            return canal;
        }

        public void Delete(Canal canal)
        {
            _context.Canais.Remove(canal);
        }

        public IEnumerable<Canal> Listar(Guid idUsuario)
        {
            return _context.Canais.Where(x => x.Usuario.Id == idUsuario).ToList();
        }

        public Canal Obter(Guid idCanal)
        {
            return _context.Canais.FirstOrDefault(x => x.Id == idCanal);
        }
    }
}
