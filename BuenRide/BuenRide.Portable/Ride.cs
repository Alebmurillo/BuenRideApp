using System;

namespace BuenRide.Portable
{
	public class Ride
	{
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
		public Ride (string StartLat, string StartLong, string EndLat, string EndLong, string Observations)
		{
			startPointLat = StartLat;
			startPointLong = StartLong;
			destPointLat = EndLat;
			destPointLong = EndLong;
			observations = Observations;
		}

		public Ride ()
		{
		}
	}
}

