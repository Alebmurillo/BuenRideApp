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

namespace BuenRide
{
	[Activity (Label = "ShowMap")]			
	public class ShowMap : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.ShowMap);
			var mapFragment = new MapFragment ();
			FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction ();
			fragmentTx.Add (Resource.Id.linearLayout1, mapFragment);
			fragmentTx.Commit ();
			// Create your application here
		}
	}
}

