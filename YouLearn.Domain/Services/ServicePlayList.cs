using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.PlayList;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServicePlayList : Notifiable, IServicePlayList
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryPlayList _repositoryPlayList;
        private readonly IRepositoryVideo _repositoryVideo;

        public ServicePlayList(IRepositoryUsuario repositoryUsuario, IRepositoryPlayList repositoryPlayList)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryPlayList = repositoryPlayList;
        }

        public PlayListResponse Add(AddPlayListRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositoryUsuario.Obter(idUsuario);
            PlayList playList = new PlayList(request.Nome, usuario);

            AddNotifications(playList);

            if (this.IsInvalid()) return null;

            playList = _repositoryPlayList.Add(playList);

            return (PlayListResponse)playList;
        }

        public Response Delete(Guid idPlayList)
        {
            bool exist = _repositoryVideo.ExistePlayListAssociada(idPlayList);

            if (exist)
            {
                AddNotification("PlayList", Msg.NAO_E_POSSIVEL_EXCLUIR_UMA_X0_ASSOCIADA_A_UM_X1.ToFormat("PlayList", "Video"));
                return null;
            }

            PlayList playList = _repositoryPlayList.Obter(idPlayList);

            if (playList == null)
            {
                AddNotification("PlayList", Msg.DADOS_NAO_ENCONTRADOS);
            }

            if (this.IsInvalid()) return null;

            _repositoryPlayList.Delete(playList);

            return new Response() { Mensagem = Msg.OPERACAO_REALIZADA_COM_SUCESSO };
        }

        public IEnumerable<PlayListResponse> Listar(Guid idUsuario)
        {
            IEnumerable<PlayList> playListsCollection = _repositoryPlayList.Listar(idUsuario);
            // conversar explicita
            var response = playListsCollection.ToList().Select(x => (PlayListResponse)x);

            return response;
        }
    }
}
