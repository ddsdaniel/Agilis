using Agilis.Domain.Abstractions.ValueObjects;

namespace Agilis.Domain.Models.ValueObjects
{
    public class AppSettings : IAppSettings
    {
        public string Segredo { get; set; }
    }
}
