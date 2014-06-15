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
using RestSharp;
using Newtonsoft.Json;
using BuenRide.Portable;


namespace BuenRide.And
{
	[Activity (Label = "BuenRide", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private const string AppId = "238624846333637";


		private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream";
		string apikey;
		EditText passLI;
		EditText emailLI;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_login);
			PackageInfo info = this.PackageManager.GetPackageInfo ("BuenRide.BuenRide", PackageInfoFlags.Signatures);

			var btnLoginFB = FindViewById<ImageView> (Resource.Id.imageViewFbLogin);
			var btnLogin = FindViewById<View> (Resource.Id.RectangleSignIn);
			var btnSignUp = FindViewById<View> (Resource.Id.RectangleSignUp);
			btnSignUp.Click += HandleSignIn;
			btnLogin.Click += HandleLogIn;
			passLI = FindViewById<EditText> (Resource.Id.passwordLI);
			emailLI = FindViewById<EditText> (Resource.Id.emailLI);
			btnLoginFB.Click += (sender, e) => {
				var webAuth = new Intent (this, typeof (FBWebViewAuthActivity));
				webAuth.PutExtra ("AppId", AppId);
				webAuth.PutExtra ("ExtendedPermissions", ExtendedPermissions);
				StartActivityForResult (webAuth, 0);
			};

		}
		void HandleLogIn(object sender, EventArgs e)
		{
			var client = new RestClient ("http://www.buenrideapp.com");
			var request = new RestRequest ("api/usuarios/login", RestSharp.Method.POST);
			request.AddParameter("password",passLI.Text);
			request.AddParameter("email", emailLI.Text);
			client.ExecuteAsync (request, response => {
				try{
					var content = response.Content; // raw content as string
					Usuario user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
					Console.WriteLine (user.apikey);
					var activity2 = new Intent (this, typeof(PrincipalMenu));
					activity2.PutExtra ("apikey",user.apikey);
					StartActivity (activity2);
				}
				catch{
					Alert ("Failed to Log In", "Bad user or password", false, (res) => {} );
				}
				Console.WriteLine (response.Content);
			});
		}
		void HandleSignIn(object sender, EventArgs e)
		{
			var activity = new Intent (this, typeof(SignUp));
			StartActivity(activity); 
		}
		protected override void OnActivityResult (int requestCode, Result resultCode, Intent data)
		{
				base.OnActivityResult (requestCode, resultCode, data);

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
			}
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


