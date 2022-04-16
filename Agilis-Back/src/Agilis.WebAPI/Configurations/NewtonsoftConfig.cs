using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Agilis.WebAPI.Configurations
{
    public static class NewtonsoftConfig
    {

        /// <summary>
        /// Configura data, enum e camelcase
        /// </summary>
        /// <param name="builder">An interface for configuring MVC services.</param>
        /// <returns>builder atualizado</returns>
        public static IMvcBuilder AddNewtonsoftConfig(this IMvcBuilder builder)
        {
            builder.AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver
                    = new CamelCasePropertyNamesContractResolver();

                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;

                options.SerializerSettings.Converters.Add(new StringEnumConverter());

                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            return builder;
        }
    }
}
