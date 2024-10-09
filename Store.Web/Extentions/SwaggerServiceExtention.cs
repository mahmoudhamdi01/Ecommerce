using Microsoft.OpenApi.Models;

namespace Store.Web.Extentions
{
    public static class SwaggerServiceExtention
    {
        public static IServiceCollection AddSwaggerDocumintation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Store.Api",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "John Walkner",
                        Email = "johnAgmail.com",
                        Url = new Uri("https://twitter.com/jwalkner"),
                    }

                });
                var securityScheme = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "beare",
                    Reference = new OpenApiReference
                    { 
                        Id = "bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                options.AddSecurityDefinition("bearer", securityScheme);
                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {securityScheme, new[] {"bearer"} }
                };
                options.AddSecurityRequirement(securityRequirements);
            });
            return services;
        }
    }
}
