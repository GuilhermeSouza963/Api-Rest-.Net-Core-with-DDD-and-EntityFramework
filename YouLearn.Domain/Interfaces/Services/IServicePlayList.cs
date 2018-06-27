using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.PlayList;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Interfaces.Base;

namespace YouLearn.Domain.Interfaces.Services
{
    public interface IServicePlayList : IServiceBase
    {
        IEnumerable<PlayListResponse> Listar(Guid idUsuario);
        PlayListResponse Add(AddPlayListRequest request, Guid idUsuario);
        Response Delete(Guid idPlayList);

    }
}
