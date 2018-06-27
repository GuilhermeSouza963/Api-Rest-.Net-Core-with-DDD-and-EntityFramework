using System;
using System.Collections;
using System.Collections.Generic;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Args.Video;
using YouLearn.Domain.Interfaces.Base;

namespace YouLearn.Domain.Interfaces.Services
{
    public interface IServiceVideo : IServiceBase
    {
        AddVideoResponse AddVideo(AddVideoRequest request, Guid idUsuario);
        IEnumerable<VideoResponse> ListarVideos(string tags);
        IEnumerable<VideoResponse> ListarVideos();
        IEnumerable<VideoResponse> ListarVideos(Guid idPlayList);

    }
}
