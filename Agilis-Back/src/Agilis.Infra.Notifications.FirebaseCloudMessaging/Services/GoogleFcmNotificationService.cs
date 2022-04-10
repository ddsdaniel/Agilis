using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Agilis.Core.Domain.Abstractions.Services;
using Agilis.Core.Domain.Extensions;
using Agilis.Core.Domain.Models.ValueObjects;
using Agilis.Infra.Notifications.FirebaseCloudMessaging.Models.ValueObjects;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Agilis.Core.Domain.Abstractions.Models.ValueObjects;

namespace Agilis.Infra.Notifications.FirebaseCloudMessaging.Services
{
    public class GoogleFcmNotificationService : INotificationService
    {
        private const string URL = "https://fcm.googleapis.com/fcm/send";
        private readonly IMapper _mapper;
        private readonly IAppSettings _appSettings;
        private readonly ILogger<GoogleFcmNotificationService> _logger;

        public GoogleFcmNotificationService(IMapper mapper, IAppSettings appSettings, ILogger<GoogleFcmNotificationService> logger)
        {
            _mapper = mapper;
            _appSettings = appSettings;
            _logger = logger;
        }


        public async Task NotificarAsync(Notificacao notificacao)
        {
            var messageRoot = _mapper.Map<GoogleFcmMessage>(notificacao);
            var paginas = messageRoot.Registration_ids.Split(1000);

            foreach (var ids in paginas)
            {
                messageRoot.Registration_ids = ids.ToArray();
                await NotificarAsync(messageRoot);
            }
        }        

        private async Task NotificarAsync(GoogleFcmMessage message)
        {
            try
            {
                var serializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore
                };

                var json = JsonConvert.SerializeObject(message, serializerSettings);
                var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("key", $"={_appSettings.GoogleFcm.ApiKey}");
                httpClient.DefaultRequestHeaders.Add("Sender", $"id={_appSettings.GoogleFcm.SenderId}");

                var response = await httpClient.PostAsync(URL, stringContent);

                if (!response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    _logger.LogError($"Falha ao enviar notificação: {result}");
                }
            }
            catch (Exception erro)
            {
                _logger.LogError($"Falha ao enviar notificação: {erro.Message}");
            }
        }
    }
}
