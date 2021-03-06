﻿using System;

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
		public string token {
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
		public string email {
			get;
			set;
		}
		public string telefono {
			get;
			set;
		}
		public Usuario (int Id, string Username, string Password, string token, DateTime CreatedAt, DateTime UpdatedAt, string email, string telefono)
		{
			id = Id;
			telefono = telefono;
			username = Username;
			password = Password;
			token = token;
			createdAt = CreatedAt;
			updatedAt = UpdatedAt;
			email = email;
		}

		public Usuario ()
		{
		}
	}
}

