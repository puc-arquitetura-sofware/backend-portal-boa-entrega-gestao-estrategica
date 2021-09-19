using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using GSL.GestaoEstrategica.Dominio.Interfaces.Integration;
using GSL.GestaoEstrategica.Dominio.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Aplicacao.Entrega
{
    public class GestaoEntregas
    {
        private readonly ICloudWatch _cloudWatch;

        public GestaoEntregas(ICloudWatch cloudWatch)
        {
            _cloudWatch = cloudWatch;
        }

        public async Task<PutMetricDataResponse> IncluirEntrega(EntregaViewModel entrega)
        {
            ValidarData(entrega);

            var dimensao = new Dimension
            {
                Name = "Metricas de Entrega",
                Value = entrega.Status == Dominio.Enums.StatusEntrega.Pendente
                    ? "Entregas a Realizar"
                    : "Entregas Realizadas"
            };

            var requisicao = new PutMetricDataRequest
            {
                MetricData = new List<MetricDatum>()
                {
                    new MetricDatum
                    {
                        Dimensions = new List<Dimension>() {dimensao},
                        MetricName = "Quantidade",
                        StatisticValues = new StatisticSet(),
                        TimestampUtc = Convert.ToDateTime(entrega.Datahora),
                        Unit = StandardUnit.Count,
                        Value = 1
                    }
                },
                Namespace = "Gestao Estrategica"
            };

            return await _cloudWatch.RegistrarMetrica(requisicao, new CancellationToken());
        }

        private static void ValidarData(EntregaViewModel entrega)
        {
            if (!DateTime.TryParse(entrega.Datahora, out _)) throw new Exception("Data e hora inválida!");
            if (Convert.ToDateTime(entrega.Datahora) > DateTime.Now) throw new Exception("Data e hora não pode superior à data e hora atual!");
            if (Convert.ToDateTime(entrega.Datahora) < DateTime.Now.AddDays(-14)) throw new Exception("Data e hora não pode ser mais antiga que duas semanas atrás");
        }
    }
}
