using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Interfaces.Base;

namespace YouLearn.Domain.Interfaces.Services
{
    public interface IServiceUsuario : IServiceBase
    {
       // e criado uma classe response que e a resposta devolvida da api e uma classe request que e a passada para api
        AdicionarUsuarioResponse AdicionarUsuario(AdicionarUsuarioRequest request);

        AutenticarUsuarioResponse AutenticarUsuario(AutenticarUsuarioRequest request);

    }
}
