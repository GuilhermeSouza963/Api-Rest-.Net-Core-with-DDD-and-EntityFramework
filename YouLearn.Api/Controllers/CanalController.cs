using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouLearn.Api.Controllers.Base;
using YouLearn.Domain.Args.Canal;
using YouLearn.Domain.Args.Usuario;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transaction;

namespace YouLearn.Api.Controllers
{
    public class CanalController : BaseController
    {
        private readonly IServiceCanal _serviceCanal;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CanalController(IUnitOfWork unitOfWork, IServiceCanal serviceCanal , IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _serviceCanal = serviceCanal;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("api/v1/Canal/ListarCanal")]
        public async Task<IActionResult> ListarCanal()
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceCanal.Listar(usuarioResponse.Id);

                return await ResponseAsync(response,_serviceCanal);
            }
            catch (Exception ex)
            {

                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/Canal/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AdicionarCanalRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceCanal.AddCanal(request, usuarioResponse.Id);

                return await ResponseAsync(response, _serviceCanal);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpDelete]
        [Route("api/v1/Canal/Excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var response = _serviceCanal.DeleteCanal(id);

                return await ResponseAsync(response, _serviceCanal);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
