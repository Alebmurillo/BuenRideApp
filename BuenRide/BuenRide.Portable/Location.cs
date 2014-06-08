using System;

namespace BuenRide.Portable
{
	public class Location
	{
		public string latitude {
			get;
			set;
		}
		public string longitude {
			get;
			set;
		}

		public Location (string lat, string longi)
		{
			latitude = lat;
			longitude = longi;
		}

		public Location ()
		{
		}
	}
}

