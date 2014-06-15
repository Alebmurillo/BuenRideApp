using System;

namespace BuenRide.Portable
{
	public class Reviews
	{
		public int id {
			get;
			set;
		}
		public string comentario {
			get;
			set;
		}
		public string calificacion {
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
		public Usuario Usuario {
			get;
			set;
		}
		public Reviews (int Id, string Comentario, string Calificacion, DateTime CreatedAt, DateTime UpdatedAt, Usuario usuario)
		{
			id = Id;
			Usuario = usuario;
			comentario = Comentario;
			calificacion = Calificacion;
			createdAt = CreatedAt;
			updatedAt = UpdatedAt;
		}

		public Reviews ()
		{
		}
	}
}

