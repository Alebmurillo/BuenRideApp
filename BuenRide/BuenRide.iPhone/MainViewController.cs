using MonoTouch.UIKit;
using System;
using MonoTouch.Foundation;

using Xamarin.Social;
using Xamarin.Social.Services;

using System.Collections.Generic;

using MonoTouch.CoreLocation;

using RestSharp;
using MonoTouch.FacebookConnect;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BuenRide.iPhone
{
	public partial class MainViewController : UIViewController
	{
	
		public Portable.Location gpsLocation {
			get;
			set;
		}

		public Portable.Location startLocation {
			get;
			set;
		}

		public Portable.Location destLocation {
			get;
			set;
		}

		public Portable.Usuario user {
			get;
			set;
		}

		// For extensive list of available extended permissions refer to 
		// https://developers.facebook.com/docs/reference/api/permissions/
		private string [] ExtendedPermissions = new [] { "user_about_me", "read_stream"};

		RestClient client = new RestClient ("http://www.buenrideapp.com");
		FBLoginView loginView;
		IFBGraphUser FbUser;

		private int FB_Y;

		public MainViewController (IntPtr handle) : base (handle)
		{
			// Custom initialization
			FB_Y = 600;
			this.View.InsertSubview (new UIImageView (UIImage.FromBundle ("background2.jpg")), 0);
			gpsLocation = new Portable.Location("0","0");
			startLocation = new Portable.Location("0","0");
			destLocation = new Portable.Location("0","0");
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
			
			// Create the Facebook LogIn View with the needed Permissions
			// https://developers.facebook.com/ios/login-ui-control/
			loginView = new FBLoginView (ExtendedPermissions) {
				Frame = new System.Drawing.RectangleF (85, FB_Y, 151, 43)

			};

			// Hook up to FetchedUserInfo event, so you know when
			// you have the user information available
			loginView.FetchedUserInfo += (sender, e) => {
				FB_Y = 600;
				FbUser = e.User;
				Console.WriteLine("fbuser is "+user.ToString());
			};

			// Add views to main view
			View.AddSubview (loginView);
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

		partial void AR_CurrentLoc1_TouchUpInside (UIButton sender)
		{
			startLocation = getLocation();
		}

		partial void AR_CurrentLoc2_TouchUpInside (UIButton sender)
		{
			destLocation = getLocation();
		}

		partial void AR_AddButton_TouchUpInside (UIButton sender)
		{
			addRide();
		}

		partial void UIButton135_TouchUpInside (UIButton sender)
		{
			logIn ();
		}	
		partial void SU_SignUpButton_TouchUpInside (UIButton sender)
		{
			addUser ();
		}

		

		public void addRide() {
			var request = new RestRequest("api/rides/", Method.POST);
			request.RequestFormat = DataFormat.Json;

			request.AddParameter ("apikey", user.apikey);
			request.AddParameter ("observations", this.AR_ObservTextField.Text);
			request.AddParameter ("startPointLat", startLocation.latitude);
			request.AddParameter ("startPointLong", startLocation.longitude);
			request.AddParameter ("destPointLat", destLocation.latitude);
			request.AddParameter ("destPointLong", destLocation.longitude);
			// execute the request
			IRestResponse response = client.Execute(request);
			var content = response.Content; // raw content as string
			Console.WriteLine (content);
		}

		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			// do first a control on the Identifier for your segue
			if (segue.Identifier.Equals("menu2addRide")) {

				var view = (MainViewController)segue.DestinationViewController;
				view.user = user;
			}
			else if (segue.Identifier.Equals("menu2addRide")) {
				var view = (MainViewController)segue.DestinationViewController;
				view.user = user;
			}
		}

		public void logIn() {
			var request = new RestRequest("api/usuarios/login/", Method.POST);
			request.RequestFormat = DataFormat.Json;

			request.AddParameter ("email", this.LI_EmailTextField.Text);
			request.AddParameter ("password", this.LI_PasswordTextField.Text);
			// execute the request
			IRestResponse response = client.Execute(request);
			var content = response.Content; // raw content as string
			Console.WriteLine (content);
			if (!content.Equals("{\"error\":\"user doesnt exist\"}") && !content.Equals("{\"error\":\"password incorrect \"}")) {
				user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
				Console.WriteLine (user.nombre);
				Console.WriteLine (user.apikey);
				UIStoryboard board = UIStoryboard.FromName ("MainStoryboard", null);
				UINavigationController ctrl = (UINavigationController)board.InstantiateViewController ("MenuNavController");
				ctrl.ModalTransitionStyle = UIModalTransitionStyle.CrossDissolve;
				this.PresentViewController (ctrl, true, null);
			} else {
				Console.WriteLine (content);
			}
		}
			
		public void addUser() {
			var request = new RestRequest("api/usuarios/", Method.POST);
			request.RequestFormat = DataFormat.Json;
			request.AddParameter ("name", this.SU_NameTextField.Text);
			request.AddParameter ("email", this.SU_EmailTextField.Text);
			request.AddParameter ("username", this.SU_UsernameTextField.Text);
			request.AddParameter ("password", this.SU_PasswordTextField.Text);
			request.AddParameter ("phone", this.SU_PhoneTextField.Text);

			// execute the request
			IRestResponse response = client.Execute(request);
			var content = response.Content; // raw content as string
			Console.WriteLine (content);
		}

		public void getUsers() {
			var request = new RestRequest (String.Format ("api/usuarios/", Method.GET));
			IRestResponse response2 = client.Execute(request);
			Console.WriteLine (response2.Content);


			//client.ExecuteAsync<Portable.Usuario> (request, response => { Console.WriteLine (response.Data.username); });
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

		public Portable.Location getLocation() 
		{
			var LocMgr = new CLLocationManager();
			if (CLLocationManager.LocationServicesEnabled) {
				LocMgr.StartMonitoringSignificantLocationChanges ();
			} else {
				Console.WriteLine ("Location services not enabled, please enable this in your Settings");
			}
			Portable.Location location = new Portable.Location ();
			LocMgr.LocationsUpdated += (o, e) => {
				location.latitude = LocMgr.Location.Coordinate.Latitude.ToString (); 
				location.longitude = LocMgr.Location.Coordinate.Longitude.ToString (); 
				Console.WriteLine ("Location "+ location.latitude + ", "+location.longitude);

			};
			return location;
		}
	}
}

