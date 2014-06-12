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
		public string endPointLat {
			get;
			set;
		}
		public string endPointLong {
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
			endPointLat = EndLat;
			endPointLong = EndLong;
			observations = Observations;
		}

		public Ride ()
		{
		}
	}
}

