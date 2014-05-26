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

namespace BuenRide
{
	[Activity (Label = "PrincipalMenu")]			
	public class PrincipalMenu : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_menu);
			//		var layName = FindViewById<TextView> (Resource.Id.textName);
			//	string text = Intent.GetStringExtra ("Nombre") ?? "Data not available";

			//			layName.Text =text;
			// Create your application here

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
			var activitySearch = new Intent (this, typeof(SearchRide));
			StartActivity(activitySearch); 
		}
		void HandleAddRide(object sender, EventArgs e)
		{
			var activityAddRide = new Intent (this, typeof(AddRide));
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
			//log out from facebook
		}
	}
}

