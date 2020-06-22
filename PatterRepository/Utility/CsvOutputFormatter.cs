
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
namespace PatterRepository.Utility
{
    public class CsvOutputFormatter: TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }
        protected override bool CanWriteType(Type type)
        {
            if (typeof(CategoriaDto).IsAssignableFrom(type) || typeof(IEnumerable<CategoriaDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<CategoriaDto>)
            {
                foreach (var categoria in (IEnumerable<CategoriaDto>)context.Object)
                {
                    FormatCsv(buffer, categoria);
                }
            }
            else
            {
                FormatCsv(buffer, (CategoriaDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, CategoriaDto categoria)
        {
            buffer.AppendLine($"{categoria.Id},\"{categoria.Nombre},\"{categoria.Descripcion}\"");
        }
    }
}
