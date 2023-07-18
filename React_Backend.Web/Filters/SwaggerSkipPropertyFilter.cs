using Microsoft.OpenApi.Models;
using React_Backend.Web.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace React_Backend.Web.Filters
{
    public class SwaggerSkipPropertyFilter:ISchemaFilter
    {

        public void Apply(OpenApiSchema schema,SchemaFilterContext context)
        {
            if (schema?.Properties == null)
            {
                return;
            }
            var skipProperties = context.Type.GetProperties().Where(t => t.GetCustomAttributes<SwaggerIgnoreAttribute>() != null);
            foreach ( var skipProperty in skipProperties)
            {
                var propertyToSkip=schema.Properties.Keys.SingleOrDefault(x=>string.Equals(x,skipProperty.Name,StringComparison.OrdinalIgnoreCase));
                if (propertyToSkip != null)
                {
                    schema.Properties.Remove(propertyToSkip);
                }
            }
        }
    }
}
