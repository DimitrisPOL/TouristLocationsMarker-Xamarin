using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Locations.UI.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
		private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
			_logger = loggerFactory.CreateLogger<LoggerMiddleware>();
        }

		public async Task Invoke(HttpContext context)
		{
			var request = await FormatRequest(context.Request);

			_logger.LogInformation(request);

			var originalBodyStream = context.Response.Body;

			using (var responseBody = new MemoryStream())
			{
				context.Response.Body = responseBody;

				await _next(context);

				var response = await FormatResponse(context.Response);

				await responseBody.CopyToAsync(originalBodyStream);

				_logger.LogInformation(response);
			}
		}

		private async Task<string> FormatRequest(HttpRequest request)
		{
			var body = request.Body;

			request.EnableBuffering();

			var bodyAsText = await new StreamReader(request.Body).ReadToEndAsync();

			request.Body.Seek(0, SeekOrigin.Begin);

			return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
		}

		private async Task<string> FormatResponse(HttpResponse response)
		{
			response.Body.Seek(0, SeekOrigin.Begin);

			string text = await new StreamReader(response.Body).ReadToEndAsync();

			response.Body.Seek(0, SeekOrigin.Begin);

			return $"{response.StatusCode}: {text}";
		}
	}
}
