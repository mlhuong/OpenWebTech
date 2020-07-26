using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Any;

namespace OpenWebTech.Contacts.Web
{
    public class SwaggerHeaderParameter : IOperationFilter
    {
        

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters = operation.Parameters ?? new List<OpenApiParameter>();
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = "access token",
                Required = false,
                Schema = new OpenApiSchema { 
                    Type = "String",
                    Default = new OpenApiString("Bearer ")
                }
            });
        }
    }
}
