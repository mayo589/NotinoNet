using Microsoft.AspNetCore.Mvc.Formatters;
using MsgPack.Serialization;
using NotinoDotNetHW.Models;

namespace NotinoDotNetHW.OutputFormatters
{
    /// <summary>
    /// Output formatter class for message type responses
    /// </summary>
    public class MessagePackOutputFormatter : OutputFormatter
    {
        public MessagePackOutputFormatter()
        {
            SupportedMediaTypes.Add("application/x-msgpack");
            SupportedMediaTypes.Add("application/msgpack");
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Object != null)
            {
                using (var msgPackStream = new MemoryStream())
                {
                    var serializer = MessagePackSerializer.Get<List<Document>>();
                    serializer.Pack(msgPackStream, context.Object);

                    var response = context.HttpContext.Response;
                    response.ContentLength = msgPackStream.Length;
                    return response.Body.WriteAsync(msgPackStream.ToArray()).AsTask();
                }
            }
            else 
            { 
                return Task.CompletedTask;
            }
        }
    }
}
