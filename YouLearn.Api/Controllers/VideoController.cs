using Microsoft.AspNetCore.Authorization;
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
using YouLearn.Domain.Args.Video;
using YouLearn.Domain.Interfaces.Services;
using YouLearn.Infra.Transaction;

namespace YouLearn.Api.Controllers
{
    public class VideoController : BaseController
    {
        private readonly IServiceVideo _serviceVideo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VideoController(IUnitOfWork unitOfWork, IServiceVideo serviceVideo , IHttpContextAccessor httpContextAccessor) : base(unitOfWork)
        {
            _serviceVideo = serviceVideo;
            _httpContextAccessor = httpContextAccessor;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar")]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var response = _serviceVideo.ListarVideos();
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{tags}")]
        public async Task<IActionResult> Listar(string tags)
        {
            try
            {
                var response = _serviceVideo.ListarVideos(tags);
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/v1/Video/Listar/{idPlayList:Guid}")]
        public async Task<IActionResult> Listar(Guid idPlayList)
        {
            try
            {
                var response = _serviceVideo.ListarVideos(idPlayList);
                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }

        [HttpPost]
        [Route("api/v1/Video/Adicionar")]
        public async Task<IActionResult> Adicionar([FromBody]AddVideoRequest request)
        {
            try
            {
                string usuarioClaims = _httpContextAccessor.HttpContext.User.FindFirst("Usuario").Value;
                AutenticarUsuarioResponse usuarioResponse = JsonConvert.DeserializeObject<AutenticarUsuarioResponse>(usuarioClaims);

                var response = _serviceVideo.AddVideo(request, usuarioResponse.Id);

                return await ResponseAsync(response, _serviceVideo);
            }
            catch (Exception ex)
            {
                return await ResponseExceptionAsync(ex);
            }
        }
    }
}
