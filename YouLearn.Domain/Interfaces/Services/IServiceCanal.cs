using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Interfaces.Base;

namespace YouLearn.Domain.Interfaces.Services
{
    public interface IServiceCanal : IServiceBase
    {
        IEnumerable<CanalResponse> Listar(Guid idUsuario);

        CanalResponse AddCanal(AdicionarCanalRequest request, Guid idUsuario);

        Response DeleteCanal(Guid idCanal);

    }
}
