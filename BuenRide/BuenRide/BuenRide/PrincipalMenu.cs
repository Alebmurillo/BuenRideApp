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
	[Activity (Label = "PrincipalMenu", Icon="@drawable/car")]			
	public class PrincipalMenu : Activity
	{
		string apikey = "";
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_menu);
			apikey = Intent.GetStringExtra ("token") ?? "";

			var btnSearchRide = FindViewById<View> (Resource.Id.RectangleSearchRide);
			var btnAddRide = FindViewById<View> (Resource.Id.RectangleAddRide);
			var btnProfile = FindViewById<View> (Resource.Id.RectangleProfile);

			var btnCost = FindViewById<View> (Resource.Id.RectangleCalculateCost);
			var btnShare = FindViewById<View> (Resource.Id.RectangleShare);
			var btnLogOut = FindViewById<View> (Resource.Id.RectangleLogout);

			btnSearchRide.Click += HandleSearchRide;
			btnAddRide.Click += HandleAddRide;
			btnProfile.Click += HandleGoToProfile;
			btnShare.Click += HandleShare;
			btnCost.Click += HandleCalculation;
			btnLogOut.Click += HandleLogOut;
		}
		void HandleSearchRide(object sender, EventArgs e)
		{
			var activity2 = new Intent (this, typeof(SearchRideMenu));
			StartActivity (activity2);
			/*	var client = new RestClient ("http://www.buenrideapp.com");
			var request = new RestRequest ("api/rides", RestSharp.Method.GET);
			List<Portable.Ride> rides = new List<Portable.Ride>();
			client.ExecuteAsync (request, response => {
				var content = response.Content; // raw content as string
				var activitySearch = new Intent (this, typeof(SearchRideByMap));
				activitySearch.PutExtra ("contentJson", content);
				StartActivity (activitySearch);
			});*/
		}
		void HandleAddRide(object sender, EventArgs e)
		{
			var activityAddRide = new Intent (this, typeof(AddRide));
			activityAddRide.PutExtra ("token", apikey);
			StartActivity (activityAddRide); 
		}
		void HandleGoToProfile(object sender, EventArgs e)
		{
			var activityProfile = new Intent (this, typeof(Profile));
			StartActivity (activityProfile); 
		}
		void HandleCalculation (object sender, EventArgs e)
		{
			var activityCost = new Intent (this, typeof(CalculateCost));
			//activity2.PutExtra ("Nombre", (string)result["name"]);
			   StartActivity (activityCost); 
		}
		void HandleShare(object sender, EventArgs e)
		{
			var activityShare = new Intent (this, typeof(ShareActivity));
			StartActivity (activityShare); 
		}
		void HandleLogOut(object sender, EventArgs e)
		{
			BackendConnection backend = BackendConnection.Instance;
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/usuarios/logout", RestSharp.Method.GET);
			request.AddHeader ("apikey", backend.apikey);
			request.AddHeader ("token", backend.token);
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
				Wait.Set();
			});
			Wait.WaitOne();
			Finish ();
		}
	}
}

