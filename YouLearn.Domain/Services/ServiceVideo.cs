using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Args.Video;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServiceVideo : Notifiable, IServiceVideo
    {
        private readonly IRepositoryPlayList _repositoryPlayList;
        private readonly IRepositoryCanal _repositoryCanal;
        private readonly IRepositoryVideo _repositoryVideo;
        private readonly IRepositoryUsuario _repositoryUsuario;

        public ServiceVideo(IRepositoryPlayList repositoryPlayList, IRepositoryCanal repositoryCanal, IRepositoryVideo repositoryVideo, IRepositoryUsuario repositoryUsuario)
        {
            _repositoryPlayList = repositoryPlayList;
            _repositoryCanal = repositoryCanal;
            _repositoryVideo = repositoryVideo;
            _repositoryUsuario = repositoryUsuario;
        }

        public AddVideoResponse AddVideo(AddVideoRequest request, Guid idUsuario)
        {
            if (request == null)
            {
                AddNotification("AddVideoRequest", Msg.OBJETO_X0_E_OBRIGATORIO.ToFormat("AddVideoRequest"));
            }

            Usuario usuario = _repositoryUsuario.Obter(idUsuario);
            if (usuario == null)
            {
                AddNotification("Usuario", Msg.X0_NAO_INFORMADO.ToFormat("Usuario"));
            }
            Canal canal = _repositoryCanal.Obter(request.IdCanal);
            if (canal == null)
            {
                AddNotification("Canal", Msg.X0_NAO_INFORMADO.ToFormat("Canal"));
            }

            PlayList playList = null;
            if (request.IdPlayList != Guid.Empty)
            {
                playList = _repositoryPlayList.Obter(request.IdPlayList);
                if (playList == null)
                {
                    AddNotification("PlayList", Msg.X0_NAO_INFORMADA.ToFormat("playList"));
                    return null;
                }
            }

            Video video = new Video(canal, playList, request.Titulo, request.Descricao, request.Tags, request.OrdemPlayList, request.IdVideoYoutube, usuario);

            AddNotifications(video);

            if (this.IsInvalid()) return null;

            _repositoryVideo.Add(video);

            return (AddVideoResponse)video;
        }
        public IEnumerable<VideoResponse> ListarVideos()
        {

            IEnumerable<Video> videoCollection = _repositoryVideo.ListaVideos();

            var response = videoCollection.ToList().Select(x => (VideoResponse)x);

            return response;
        }

        public IEnumerable<VideoResponse> ListarVideos(string tags)
        {

            IEnumerable<Video> videoCollection = _repositoryVideo.ListaVideos(tags);

            var response = videoCollection.ToList().Select(x => (VideoResponse)x);

            return response;
        }

        public IEnumerable<VideoResponse> ListarVideos(Guid idPlayList)
        {
            IEnumerable<Video> videoCollection = _repositoryVideo.ListaVideos(idPlayList);

            var response = videoCollection.OrderBy(x => x.OrdemNaPlayList).ToList().Select(x => (VideoResponse)x);

            return response;
        }
    }
}
