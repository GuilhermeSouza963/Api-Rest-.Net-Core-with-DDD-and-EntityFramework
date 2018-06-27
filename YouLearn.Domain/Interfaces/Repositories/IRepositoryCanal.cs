using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Interfaces.Repositories
{
    public interface IRepositoryCanal
    {
        IEnumerable<Canal> Listar(Guid idUsuario);

        Canal Add(Canal canal);

        Canal Obter(Guid idCanal);

        void Delete(Canal canal);

    }
}
