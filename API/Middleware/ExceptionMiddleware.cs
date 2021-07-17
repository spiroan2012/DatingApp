using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _nextm;
		private readonly ILogger<ExceptionMiddleware> _logger;
		private readonly IHostEnvironment _env;

		public ExceptionMiddleware(RequestDelegate nextm, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
		{
			_nextm = nextm;
			_logger = logger;
			_env = env;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _nextm(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = _env.IsDevelopment()
					? new ApiException(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
					: new ApiException(httpContext.Response.StatusCode, "Internal Server Error");

				var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

				var json = JsonSerializer.Serialize(response, options);
				await httpContext.Response.WriteAsync(json);	
			}
		}
	}
}
