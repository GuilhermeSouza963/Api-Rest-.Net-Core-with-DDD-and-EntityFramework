using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using YouLearn.Domain.Args.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServiceCanal : Notifiable, IServiceCanal
    {
        private readonly IRepositoryUsuario _repositoryUsuario;
        private readonly IRepositoryCanal _repositoryCanal;
        private readonly IRepositoryVideo _repositoryVideo;

        public ServiceCanal(IRepositoryUsuario repositoryUsuario, IRepositoryCanal repositoryCanal, IRepositoryVideo repositoryVideo)
        {
            _repositoryUsuario = repositoryUsuario;
            _repositoryCanal = repositoryCanal;
            _repositoryVideo = repositoryVideo;
        }

        public CanalResponse AddCanal(AdicionarCanalRequest request, Guid idUsuario)
        {
            Usuario usuario = _repositoryUsuario.Obter(idUsuario);
            Canal canal = new Canal(request.Nome, request.UrlLogo, usuario);

            AddNotifications(canal);

            if (this.IsInvalid()) return null;

            canal = _repositoryCanal.Add(canal);

            return (CanalResponse)canal;
        }

        public Response DeleteCanal(Guid idCanal)
        {
            bool existe = _repositoryVideo.ExisteCanalAssociado(idCanal);

            if (existe)
            {
                AddNotification("Canal", Msg.NAO_E_POSSIVEL_EXCLUIR_UM_X0_ASSOCIADO_A_UM_X1.ToFormat("canal", "vídeo"));
                return null;
            }

            Canal canal = _repositoryCanal.Obter(idCanal);

            if (canal == null)
            {
                AddNotification("Canal", Msg.DADOS_NAO_ENCONTRADOS);
            }

            if (IsInvalid()) return null;

            _repositoryCanal.Delete(canal);

            return new Response() { Mensagem = Msg.OPERACAO_REALIZADA_COM_SUCESSO };
        }

        public IEnumerable<CanalResponse> Listar(Guid idUsuario)
        {
            IEnumerable<Canal> canalCollection = _repositoryCanal.Listar(idUsuario);
            var response = canalCollection.ToList().Select(x => (CanalResponse)x);

            return response;
        }
    }
}
