using System;

namespace BuenRide.Portable
{
	public class RideUser
	{
		public string usuario_id{
			get;
			set;
		}
		public string startPointLat {
			get;
			set;
		}
		public string startPointLong {
			get;
			set;
		}
		public string destPointLat {
			get;
			set;
		}
		public string destPointLong {
			get;
			set;
		}
		public string observations {
			get;
			set;
		}
		public RideUser (string StartLat, string StartLong, string EndLat, string EndLong, string Observations, string usuario_id)
		{
			startPointLat = StartLat;
			startPointLong = StartLong;
			destPointLat = EndLat;
			destPointLong = EndLong;
			observations = Observations;
			usuario_id = usuario_id;
		}
		public RideUser ()
		{
		}
	}
}

