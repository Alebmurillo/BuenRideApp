using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;

using Xamarin.Social;
using Xamarin.Social.Services;

using MonoTouch.CoreLocation;

namespace BuenRide.iPhone
{
	public partial class MainViewController : UIViewController
	{
		var LocMgr = new CLLocationManager();

		public MainViewController (IntPtr handle) : base (handle)
		{
			// Custom initialization
			this.View.InsertSubview (new UIImageView (UIImage.FromBundle ("background2.jpg")), 0);
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);
		}

		#endregion


		partial void UIButton529_TouchUpInside (UIButton sender)
		{
			shareOnTwitter();
		}

		partial void UIButton528_TouchUpInside (UIButton sender)
		{
			shareOnFacebook() ;
		}

		public void shareOnTwitter() 
		{
			// 1. Create the service
			var twitter = new TwitterService { ConsumerKey= "jt8zcYZSrrwuVn6cgHuW6mcou",
				ConsumerSecret ="APh7LVOQBHw6J8DVmCcC9tRGUBQbitn5o9CzNN3mlvzG7ZNKJr",
				CallbackUrl = new Uri ("https://twitter.com/")};       
			// 2. Create an item to share
			var item = new Item { Text = "Xamarin.Social is the bomb.com." };
			item.Links.Add (new Uri ("http://github.com/xamarin/xamarin.social"));

			var shareController = twitter.GetShareUI(item, result => {
				// result lets you know if the user shared the item or canceled
				DismissViewController (true, null);
			});
			PresentViewController (shareController, true, null);
		}

		public void shareOnFacebook() 
		{
			// 1. Create the service
			var facebook = new FacebookService {
				ClientId = "238624846333637",
			};

			// 2. Create an item to share
			var item = new Item { Text = "Xamarin.Social is the bomb.com." };
			item.Links.Add (new Uri ("http://github.com/xamarin/xamarin.social"));

			// 3. Present the UI on iOS
			var shareController = facebook.GetShareUI (item, result => {
				// result lets you know if the user shared the item or canceled
				DismissViewController (true, null);
			});
			PresentViewController (shareController, true, null);
		}
	}
}

