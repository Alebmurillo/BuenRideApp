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
using Xamarin.Social.Services;
using Xamarin.Social;

namespace BuenRide.And
{
	[Activity (Label = "ShareActivity", Icon="@drawable/car")]			
	public class ShareActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.activity_share);
			var facebookButton = FindViewById<View>(Resource.Id.RectangleShareFacebook);
			facebookButton.Click += (sender, args) => ShareFacebook();
			var twitterButton = FindViewById<View>(Resource.Id.RectangleShareTwitter);
			twitterButton.Click += (sender, args) => ShareTwitter();
		}
		void ShareFacebook ()
		{
			// 1. Create the service
			var facebook = new FacebookService { ClientId = "238624846333637",
				RedirectUrl = new System.Uri ("https://www.facebook.com/connect/login_success.html")};

			// 2. Create an item to share
			var item = new Item { Text = "Buen Ride is the bomb.com." };
			item.Links.Add (new Uri ("https://www.facebook.com/connect/login_success.html"));

			// 3. Present the UI on Android
			var shareIntent = facebook.GetShareUI (this, item, result => {
				// result lets you know if the user shared the item or canceled
			});
			StartActivityForResult (shareIntent, 42);
		}
		void ShareTwitter()
		{
			var twitter = new TwitterService { ConsumerKey= "jt8zcYZSrrwuVn6cgHuW6mcou",
				ConsumerSecret ="APh7LVOQBHw6J8DVmCcC9tRGUBQbitn5o9CzNN3mlvzG7ZNKJr",
				CallbackUrl = new Uri ("https://api.twitter.com/1.1/")};       
			var item = new Item { Text = "Buen Ride is the bomb.com." };
			var shareIntent = twitter.GetShareUI(this, item, result => {
				// result lets you know if the user shared the item or canceled
			});

			StartActivityForResult (shareIntent, 42);

		}
	}
}








