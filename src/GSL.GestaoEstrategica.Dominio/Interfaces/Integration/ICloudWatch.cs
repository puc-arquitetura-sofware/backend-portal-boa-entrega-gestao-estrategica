using Amazon.CloudWatch.Model;
using System.Threading;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Dominio.Interfaces.Integration
{
    public interface ICloudWatch
    {
        Task<PutMetricDataResponse> RegistrarMetrica(PutMetricDataRequest requisicao, CancellationToken token);
    }
}
