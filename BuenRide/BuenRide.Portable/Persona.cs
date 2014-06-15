using System;

namespace BuenRide.Portable
{
	public class Persona
	{
		public int id {
			get;
			set;
		}
		public string nombre {
			get;
			set;
		}
		public string email {
			get;
			set;
		}
		public string telefono {
			get;
			set;
		}
		public string home_latitud {
			get;
			set;
		}
		public string home_longitud {
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

		public Persona (int Id, string Nombre, string Email, string Telefono, string HomeLat, string HomeLong, DateTime createdAt, DateTime updatedAt)
		{
			id = Id;
			nombre = Nombre;
			email = Email;
			telefono = Telefono;
			home_latitud = HomeLat;
			home_longitud = HomeLong;
			createdAt = createdAt;
			updatedAt = updatedAt;
		}

		public Persona ()
		{
		}
	}
}

