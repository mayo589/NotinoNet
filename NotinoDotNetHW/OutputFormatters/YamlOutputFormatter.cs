using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NotinoDotNetHW.OutputFormatters
{
    /// <summary>
    /// Output formatter class for YAML type responses
    /// </summary>
    public class YamlOutputFormatter : TextOutputFormatter
    {
        private readonly Serializer _serializer;

        public YamlOutputFormatter(Serializer serializer)
        {
            _serializer = serializer;

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/yaml"));
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/yaml"));
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (selectedEncoding == null)
            {
                throw new ArgumentNullException(nameof(selectedEncoding));
            }

            var response = context.HttpContext.Response;
            using (var writer = context.WriterFactory(response.Body, selectedEncoding))
            {
                WriteObject(writer, context.Object);

                await writer.FlushAsync();
            }
        }

        private void WriteObject(TextWriter writer, object value)
        {
            if (writer == null)
            {
                throw new ArgumentNullException(nameof(writer));
            }

            _serializer.Serialize(writer, value);
        }
    }
}
