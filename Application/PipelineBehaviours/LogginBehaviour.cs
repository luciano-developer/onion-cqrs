using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.PipelineBehaviours
{
    public class LogginBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LogginBehaviour<TRequest, TResponse>> logger;

        public LogginBehaviour(ILogger<LogginBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            //REQUEST 

            logger.LogInformation($"Handling {typeof(TRequest).Name}");

            Type myType = request.GetType();

            IList<PropertyInfo> properties = new List<PropertyInfo>(myType.GetProperties());
            foreach (PropertyInfo property in properties) { 
                object propValue = property.GetValue(request, null);
                logger.LogInformation("{Property} : {@Value}", property.Name, propValue);
            }

            var response = await next();


            //RESPONSE

            logger.LogInformation($"Handler {typeof(TResponse).Name}");
            return response;

        }
    }
}
