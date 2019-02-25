using GoPay.Models;
using GoPay.Service;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GoPay.Factories
{
    public interface IConnectorFactory
    {
        GPConnector Create(GoPayBaseModel model);
    }

    public class ConnectorFactory : IConnectorFactory
    {
        private readonly GoPayConfig _config;
        private readonly ILogger<RecurrenceService> _logger;

        public ConnectorFactory(IOptions<GoPayConfig> config, ILogger<RecurrenceService> logger)
        {
            this._config = config.Value;
            this._logger = logger;
        }

        public GPConnector Create(GoPayBaseModel model)
        {
            var connector = new GPConnector(_config.GoPayApi, model.ClientId, model.ClientSecret);
            connector.GetAppToken();

            return connector;
        }
    }
}
