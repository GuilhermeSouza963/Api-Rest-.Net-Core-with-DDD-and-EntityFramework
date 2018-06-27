using prmToolkit.NotificationPattern;
using prmToolkit.NotificationPattern.Extensions;
using System;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Entities;
using YouLearn.Domain.Interfaces.Repositories;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Domain.Resources;
using YouLearn.Domain.ValueObjects;

namespace YouLearn.Domain.Services
{
    public class ServiceUsuario : Notifiable, IServiceUsuario
    {
        // Dependencias do Serviços usando injeção de dependencia
        private readonly IRepositoryUsuario _repositoryUsuario;

        //Construtor
        public ServiceUsuario(IRepositoryUsuario repositoryUsuario)
        {
            _repositoryUsuario = repositoryUsuario;
        }

        public AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request)
        {
            if (request == null)
            {
                // notification pattern e resources
                AddNotification("AdicionarUsuarioRequest", Msg.OBJETO_X0_E_OBRIGATORIO.ToFormat("AdicionarUsuarioRequest"));
                return null;
            }
            // cria entidade
            Nome nome = new Nome(request.PrimeiroNome, request.SegundoNome);

            Email email = new Email(request.Email);

            Usuario usuario = new Usuario(nome, email, request.Senha);
            // validações
            AddNotifications(usuario);

            if (this.IsInvalid()) return null;
            // persiste no banco de dados
            _repositoryUsuario.Salvar(usuario);

            return new AdicionarUsuarioResponse(usuario.Id);
        }

        public AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request)
        {
            if (request == null)
            {
                // notification pattern e resources
                AddNotification("AutenticarUsuarioRequest", Msg.OBJETO_X0_E_OBRIGATORIO.ToFormat("AutenticarUsuarioRequest"));
                return null;
            }
            // na entidade foi feita a blindagem de codigo e validação por meio do construtor
            var email = new Email(request.Email);
            var usuario = new Usuario(email, request.Senha);
            AddNotifications(usuario);

            if (this.IsInvalid()) return null;

            usuario = _repositoryUsuario.Obter(usuario.Email.Endereco, usuario.Senha);

            if (usuario == null)
            {
                //notification pattern e resources
                AddNotification("Usuario", Msg.DADOS_NAO_ENCONTRADOS);
                return null;
            }

            // foi feito a conversao explicita

            //var response = new AutenticarUsuarioResponse()
            //{
            //    Id = usuario.Id,
            //    PrimeiroNome = usuario.Nome.PrimeiroNome
            //};

            //conversao explicita
            var response = (AutenticarUsuarioResponse)usuario;

            return response;
        }
    }
}
