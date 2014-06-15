using System;

namespace BuenRide.Portable
{
	public sealed class BackendConnection
	{
		private static readonly BackendConnection BEinstance = new BackendConnection();

		static BackendConnection ()
		{
		}
		private BackendConnection ()
		{
		}
		public string url {
			get;
			set;
		}
		public string token {
			get;
			set;
		}
		public string apikey {
			get;
			set;
		}
		public static BackendConnection Instance
		{
			get
			{
				return BEinstance;
			}
		}
	}
}


	
