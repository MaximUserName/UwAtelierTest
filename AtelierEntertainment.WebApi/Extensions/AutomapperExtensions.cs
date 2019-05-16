using AtelierEntertainment.BusinessLogic.MappingProfiles;
using AutoMapper;
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

}
