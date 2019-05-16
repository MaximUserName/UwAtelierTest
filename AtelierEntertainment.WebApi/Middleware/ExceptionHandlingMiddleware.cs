using System;
using System.Net;
using System.Threading.Tasks;
using AtelierEntertainment.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace AtelierEntertainment.WebApi.Middleware
{
	/// <summary>
	/// Central error/exception handler Middleware
	/// </summary>
	public class ExceptionHandlerMiddleware
	{
		private readonly RequestDelegate _request;

		/// <summary>
		/// Initializes a new instance of the <see cref="ExceptionHandlerMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next.</param>
		public ExceptionHandlerMiddleware(RequestDelegate next)
		{
			this._request = next;
		}

		/// <summary>
		/// Invokes the specified context.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		public async Task Invoke(HttpContext context,
			ILogger<ExceptionHandlerMiddleware> logger
			//, IExceptionConvertStrategy exceptionConvertStrategy
		)
		{
			try
			{
				await this._request(context);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
				Console.WriteLine(exception.Message);
				await HandleExceptionAsync(context, exception);
			}
		}

		private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
		{
			if (exception is EntityNotFound)
			{
				var ex = exception as EntityNotFound;
				var httpResponse = context.Response;
				httpResponse.Clear();
				httpResponse.StatusCode = StatusCodes.Status404NotFound;
				httpResponse.ContentType = "application/json";
				var jsonResponse = JObject.FromObject(new{ message = ex.Message });
				await httpResponse.WriteAsync(jsonResponse.ToString());
			}
			await Task.CompletedTask;
		}
	}
}