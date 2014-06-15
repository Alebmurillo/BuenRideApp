
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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Newtonsoft.Json;

namespace BuenRide
{
	[Activity (Label = "SearchRideByMap")]			
	public class SearchRideByMap : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			// Create your application here	base.OnCreate (bundle);
			SetContentView (Resource.Layout.ShowMap);
			MapFragment mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.my_mapfragment_container);
			var btnmap =  FindViewById<Button> (Resource.Id.buttonOKMap);
			GoogleMap map = mapFrag.Map;

			/*	string content = Intent.GetStringExtra ("contentJson") ?? "";
			List<Portable.Ride> rides = JsonConvert.DeserializeObject<List<Portable.Ride>>(content);
			Console.WriteLine (content);
			Console.WriteLine (rides.Count);
			for (int i = 0; i < rides.Count; i++) {
				string ridestr = "Ride" + i + ": \n" + " Lat i:" + rides [i].startPointLat + " Lon i:" + rides [i].startPointLong + "\n Lat f:" + rides [i].destPointLat + "Lon f:" + rides [i].destPointLong + "\n Comments:" +  rides [i].observations;
				MarkerOptions markerOpt1 = new MarkerOptions();
				markerOpt1.SetPosition(new LatLng(i,i));
				markerOpt1.SetTitle("My Ride");
				map.AddMarker(markerOpt1);	
			}*/



		}
	}
}

