using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YouLearn.Api.Controllers
{
    public class InfoController
    {
        [HttpGet]
        [Route("RetornaVersao")]
        public object RetornaVersao()
        {
            return new { dev = "Guilherme Souza", versao = "0.0.1" };
        }

        [HttpGet]
        [Route("Listar/Usuario")]
        public object ListarUsuario()
        {
            return new { dev = "Guilherme Souza", versao = "0.0.1" };
        }
    }
}
