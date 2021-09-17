using Amazon.CloudWatch;
using Amazon.CloudWatch.Model;
using GSL.GestaoEstrategica.Dominio.Interfaces.Integration;
using System.Threading;
using System.Threading.Tasks;

namespace GSL.GestaoEstrategica.Integration
{
    public class CloudWatch : ICloudWatch
    {
        private readonly IAmazonCloudWatch _client;
        public CloudWatch(IAmazonCloudWatch client) => 
            _client = client;

        public async Task<PutMetricDataResponse> RegistrarMetrica(PutMetricDataRequest requisicao, CancellationToken token) =>
            await _client.PutMetricDataAsync(requisicao, token);
    }
}
