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
using RestSharp;
using BuenRide.Portable;
using System.Threading;

namespace BuenRide.And
{
	[Activity (Label = "AddRide",Icon="@drawable/car")]			
	public class AddRide : Activity
	{
		string apikey = "";
		double startLat = 0;
		double startLong = 0;
		bool startPos;
		double endLat = 0;
		double endLong = 0;
		EditText observations;
		private BackendConnection backend;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_add_ride);
			var btnStartMap = FindViewById<View> (Resource.Id.RectangleByMapStart);
			var btnStartGPS = FindViewById<View> (Resource.Id.RectangleCurrentLocationGPStart);
			apikey = Intent.GetStringExtra ("token") ?? "";
			backend = BackendConnection.Instance;

			var btnEndGPS = FindViewById<View> (Resource.Id.RectangleEndMapRide);
			var btnEndMap = FindViewById<View> (Resource.Id.RectangleEndGPSRide);

			var btnCancel = FindViewById<View> (Resource.Id.RectangleCancelAddRide);
			var btnAddRide = FindViewById<View> (Resource.Id.RectangleAddRide);

			observations = FindViewById<EditText> (Resource.Id.observations);

			btnStartGPS.Click += HandleStartMap;
			btnStartMap.Click += HandleStartGPS;
			btnEndMap.Click += HandleEndMap;
			btnEndGPS.Click += HandleEndGPS;

			btnCancel.Click += HandleCancel;
			btnAddRide.Click += HandleAddRide;
			// Create your application here
		}
		void HandleStartMap(object sender, EventArgs e)
		{
			startPos = true;
			var activity = new Intent (this, typeof(WaitForGPS));
			activity.PutExtra ("window", "addRide");
			StartActivityForResult (activity, 0);
		}
		void HandleStartGPS(object sender, EventArgs e)
		{
			startPos = true;
			var activity = new Intent (this, typeof(ShowMap));
			activity.PutExtra ("window", "addRide");
			StartActivityForResult (activity, 0);
		}
		void HandleEndMap(object sender, EventArgs e)
		{
			startPos = false;
			var activity = new Intent (this, typeof(WaitForGPS));
			activity.PutExtra ("window", "addRide");
			StartActivityForResult (activity, 0);
		}
		void HandleEndGPS(object sender, EventArgs e)
		{
			startPos = false;
			var activity = new Intent (this, typeof(ShowMap));
			activity.PutExtra ("window", "addRide");
			StartActivityForResult (activity, 0);
		}
		void HandleCancel(object sender, EventArgs e)
		{
			Finish();
		}
		void HandleAddRide(object sender, EventArgs e)
		{
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/rides/", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddHeader("token", backend.token);
			request.AddParameter("observations", observations.Text);
			request.AddParameter("startPointLat", ""+startLat);
			request.AddParameter("startPointLong", ""+startLong);
			request.AddParameter("destPointLat", ""+endLat);
			request.AddParameter("destPointLong", ""+ endLong);
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
				Wait.Set();
			});
			Wait.WaitOne();
			Finish ();
		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			if (resultCode == Result.Ok) {
				if (startPos) {
					startLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
					startLong = Convert.ToDouble (data.GetStringExtra ("longitude"));
				} else {

					endLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
					endLong = Convert.ToDouble (data.GetStringExtra ("longitude"));

				}
			}
		}
	}
}

