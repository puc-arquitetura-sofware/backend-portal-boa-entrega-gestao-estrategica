using GSL.GestaoEstrategica.Dominio.Interfaces.Integration;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using Amazon.CloudWatch.Model;
using System.Collections.Generic;
using System;
using Amazon.CloudWatch;

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
        public async Task<IActionResult> Salvar([FromBody] EntregaViewModel EntregaViewModel)
        {
            var dimensao = new Dimension 
            { 
                Name = "Metricas de Entrega",
                Value = EntregaViewModel.Status == Dominio.Enums.StatusEntrega.Pendente ? "Entregas a Realizar" : "Entregas Realizadas"
            };

            var metrica = new MetricDatum
            {
                Dimensions = new List<Dimension>(),
                MetricName = "Quantidade",
                StatisticValues = new StatisticSet(),
                TimestampUtc = Convert.ToDateTime(EntregaViewModel.Datahora),
                Unit = StandardUnit.Count,
                Value = 1
            };

            var requisicao = new PutMetricDataRequest
            {
                MetricData = new List<MetricDatum>() { metrica },
                Namespace = "Gestao Estrategica"
            };

            var resposta = await _cloudWatch.RegistrarMetrica(requisicao, new CancellationToken());
            return CustomResponse(resposta);
        }
    }
}
