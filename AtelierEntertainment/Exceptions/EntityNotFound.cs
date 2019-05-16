using System;

namespace AtelierEntertainment.Exceptions
{
	public class EntityNotFound : Exception
	{
		public EntityNotFound(string message) : base(message)
		{
		}

		public EntityNotFound(string entity, int id) 
			: this($"Entity {entity} with id={id} was not found")
		{
		}
	}
}