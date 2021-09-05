using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Travel.WebApi.Helpers
{
    public class ConfigSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigSwaggerOptions(IApiVersionDescriptionProvider provider)
            => _provider = provider;

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }

        private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Travel Tour",
                Version = description.ApiVersion.ToString(),
                Description = "Web Service for Travel Tour.",
                Contact = new OpenApiContact
                {
                    Name = "IT Department",
                    Email = "developer@traveltour.xyz",
                    Url = new Uri("https://traveltour.xyz/support")
                }
            };

            if (description.IsDeprecated)
                info.Description += " <strong>This API version of Travel Tour has been deprecated.</strong>";

            return info;
        }
    }
}
