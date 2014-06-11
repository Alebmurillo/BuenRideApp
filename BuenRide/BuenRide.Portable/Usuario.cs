using System;

namespace BuenRide.Portable
{
	public class Usuario
	{
		public int id {
			get;
			set;
		}
		public string username {
			get;
			set;
		}
		public string password {
			get;
			set;
		}
		public string apikey {
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
		public DateTime createdAt {
			get;
			set;
		}
		public DateTime updatedAt {
			get;
			set;
		}

		public Usuario (int Id, string Username, string Password, string Apikey, string Nombre, string Email, string Telefono, DateTime CreatedAt, DateTime UpdatedAt)
		{
			id = Id;
			username = Username;
			password = Password;
			apikey = Apikey;
			nombre = Nombre;
			email = Email;
			telefono = Telefono;
			createdAt = CreatedAt;
			updatedAt = UpdatedAt;
		}

		public Usuario ()
		{
		}
	}
}

