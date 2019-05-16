using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AtelierEntertainment.WebApi.Controllers
{
	[ApiController]
	[Produces("application/json")]
	//[Route("api/[controller]")]
	public abstract class CustomApiControllerBase : ControllerBase
	{
	}
}
