using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Locations;
using Android.Gms.Maps;

namespace BuenRide
{
	[Activity (Label = "CalculateCost")]			
	public class CalculateCost : Activity
	{
		int startLat;
		int startLong;
		int endLat;
		int endLong;
		int daysPerW;
		int profite;
		int gasPerL;
	

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_calculate_main);
			var btnStartCurrentLocation = FindViewById<View> (Resource.Id.RectangleCurrentLocation);
			var btnStartCurrentMap = FindViewById<View> (Resource.Id.RectangleByMap);

			btnStartCurrentLocation.Click += HandleStartCurrentLocationMap;
			btnStartCurrentMap.Click += HandleStartCurrentLocation;
			// Create your application here
		}
		void HandleStartCurrentLocationMap(object sender, EventArgs e)
		{
			var activityShare = new Intent (this, typeof(ShowMap));
			StartActivity (activityShare); 
		}
		void HandleStartCurrentLocation(object sender, EventArgs e)
		{	
			var activity = new Intent (this, typeof(WaitForGPS));
			StartActivity (activity); 
		}
	}
}

