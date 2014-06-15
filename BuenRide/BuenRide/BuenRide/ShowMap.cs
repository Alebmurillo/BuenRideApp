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

namespace BuenRide.And
{
	[Activity (Label = "ShowMap")]			
	public class ShowMap : Activity,GoogleMap.IOnMarkerDragListener
	{
		double selectedLat = 0;
		double selectedLon = 0;
		string windowAddRide = "";
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ShowMap);
			MapFragment mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.my_mapfragment_container);
			var btnmap =  FindViewById<Button> (Resource.Id.buttonOKMap);
			btnmap.Click += HandleClose;
			GoogleMap map = mapFrag.Map;
			if (map != null) {
				MarkerOptions markerOpt1 = new MarkerOptions();
				markerOpt1.SetPosition(new LatLng(50.379444, 2.773611));
				markerOpt1.SetTitle("My Ride");
				markerOpt1.Draggable (true);
				map.AddMarker(markerOpt1);	
				map.SetOnMarkerDragListener(this);
			}
			string text = Intent.GetStringExtra ("window") ?? "";
		}
		void HandleClose(object sender, EventArgs e){
			Intent myIntent;
			if (windowAddRide == "") {
				myIntent = new Intent (this, typeof(CalculateCost));
			} else {
				myIntent = new Intent (this, typeof(AddRide));
			}
			myIntent.PutExtra ("latitude", ""+selectedLat);
			myIntent.PutExtra ("longitude", ""+selectedLon);
			SetResult (Result.Ok, myIntent);
			Finish ();
	}
	
		public void OnMarkerDragStart (Marker marker) {
			Console.WriteLine(marker.Position);
		}

		public void OnMarkerDragEnd (Marker marker) {
			selectedLat = marker.Position.Latitude;
			selectedLon = marker.Position.Longitude;
		}

		public void OnMarkerDrag(Marker marker) {
			//	mTopText.Text = ("onMarkerDrag.  Current Position: " + marker.Position);
		}
	}
}

