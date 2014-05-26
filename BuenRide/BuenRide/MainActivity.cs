using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Facebook;
using System.Collections.Generic;
using Android.Graphics;
using Android.Content.PM;
using Java.Security;

namespace BuenRide
{
	[Activity (Label = "BuenRide", MainLauncher = true)]
	public class MainActivity : Activity
	{
		// Replace here you own Facebook App Id, if you don't have one go to
		// https://developers.facebook.com/apps
		private const string AppId = "238624846333637";

		/// <summary>
		/// Extended permissions is a comma separated list of permissions to ask the user.
		/// </summary>
		/// <remarks>
		/// For extensive list of available extended permissions refer to 
		/// https://developers.facebook.com/docs/reference/api/permissions/
		/// </remarks>
		private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream";

		FacebookClient fb;
		string accessToken;
		bool isLoggedIn;
		string lastMessageId;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_login);
			PackageInfo info = this.PackageManager.GetPackageInfo ("BuenRide.BuenRide", PackageInfoFlags.Signatures);

			var btnLogin = FindViewById<ImageView> (Resource.Id.imageViewFbLogin);
			var btnSignUp = FindViewById<View> (Resource.Id.RectangleSignUp);
			btnSignUp.Click += HandleSignIn;
			btnLogin.Click += (sender, e) => {
				//cambiar
				var activity2 = new Intent (this, typeof(PrincipalMenu));
				StartActivity (activity2);

				//var webAuth = new Intent (this, typeof (FBWebViewAuthActivity));
				//webAuth.PutExtra ("AppId", AppId);
				//webAuth.PutExtra ("ExtendedPermissions", ExtendedPermissions);
				//StartActivityForResult (webAuth, 0);
			};

		}
		void HandleSignIn(object sender, EventArgs e)
		{
			var activity = new Intent (this, typeof(SignUp));
			StartActivity(activity); 
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
			/*	base.OnActivityResult (requestCode, resultCode, data);

			switch (resultCode) {
			case Result.Ok:
					var activity2 = new Intent (this, typeof(PrincipalMenu));
					StartActivity (activity2);
				break;
			case Result.Canceled:
				Alert ("Failed to Log In", "User Cancelled", false, (res) => {} );
				break;
			default:
				break;
			}*/
		}

		public void Alert (string title, string message, bool CancelButton , Action<Result> callback)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder(this);
			builder.SetTitle(title);
			builder.SetMessage(message);

			builder.SetPositiveButton("Ok", (sender, e) => {
				callback(Result.Ok);
			});

			if (CancelButton) {
				builder.SetNegativeButton("Cancel", (sender, e) => {
					callback(Result.Canceled);
				});
			}

			builder.Show();
		}

	}
}


