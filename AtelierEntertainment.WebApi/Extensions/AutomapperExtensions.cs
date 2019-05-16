using System.Net;
using AtelierEntertainment.BusinessLogic.MappingProfiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AtelierEntertainment.WebApi.Extensions
{
	public static class AutomapperExtensions
	{
		public static void AddAutomapperProfiles(this IServiceCollection services)
		{
			Mapper.Reset();
			services.AddAutoMapper(typeof(OrderProfile));
		}
	}

	public static class ApiControllerExtensions
	{
		public static IActionResult Created(this ControllerBase controller, object createdObject)
		{
			return new ObjectResult(createdObject)
			{
				StatusCode = (int)HttpStatusCode.Created,
				//Value = createdObject
			};
		}
	}

}
