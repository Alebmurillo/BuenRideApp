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
		public string submittedBy {
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

		public Reviews (int Id, string Comentario, string Calificacion, string SubmittedBy, DateTime CreatedAt, DateTime UpdatedAt)
		{
			id = Id;
			comentario = Comentario;
			submittedBy = SubmittedBy;
			calificacion = Calificacion;
			createdAt = CreatedAt;
			updatedAt = UpdatedAt;
		}

		public Reviews ()
		{
		}
	}
}

