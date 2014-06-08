using System;

namespace BuenRide.Portable
{
	public class Lugar
	{
		public int id {
			get;
			set;
		}
		public string nombre {
			get;
			set;
		}
		public string latitud {
			get;
			set;
		}
		public string longitud {
			get;
			set;
		}
		public DateTime createdAt {
			get;
			set;
		}
		public DateTime updatedAt {
			get;
			set;
		}

		public Lugar (int Id, string Nombre, string Latitud, string Longitud, DateTime CreatedAt, DateTime UpdatedAt)
		{
			id = Id;
			nombre = Nombre;
			latitud = Latitud;
			longitud = Longitud;
			createdAt = CreatedAt;
			updatedAt = UpdatedAt;
		}

		public Lugar ()
		{
		}


	}
}

