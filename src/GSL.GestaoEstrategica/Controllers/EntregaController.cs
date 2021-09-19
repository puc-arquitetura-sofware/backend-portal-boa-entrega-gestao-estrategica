using GSL.GestaoEstrategica.Dominio.Interfaces.Integration;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Amazon.CloudWatch.Model;
using GSL.GestaoEstrategica.Aplicacao.Fakes;
using GSL.GestaoEstrategica.Aplicacao.Entrega;

namespace GSL.GestaoEstrategica.Api.Controllers
{
    [Route("api/entrega")]
    [ApiController]
    public class EntregaController : MainController
    {
        private readonly ICloudWatch _cloudWatch;

        public EntregaController(ICloudWatch cloudWatch)
        {
            _cloudWatch = cloudWatch;
        }

        [HttpPost()]
        [ProducesResponseType(typeof(PutMetricDataResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<IActionResult> Salvar([FromBody] EntregaViewModel entrega)
        {
            var gestaoEntrega = new GestaoEntregas(_cloudWatch);
            var resposta = await gestaoEntrega.IncluirEntrega(entrega);
            return CustomResponse(resposta);
        }

        [HttpPost("carregar-dados")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CarregarDados([FromBody] DadosFakeViewModel dados)
        {
            var gestaoEntrega = new GestaoEntregas(_cloudWatch);
            var listaEntregas = EntregaFake.GerarFake(dados);

            foreach (var entrega in listaEntregas)
                await gestaoEntrega.IncluirEntrega(entrega);

            return CustomResponse("registros carregados com sucesso");
        }
    }
}
