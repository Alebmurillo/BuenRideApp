using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Maps;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Facebook;
using Java.Security;
using Newtonsoft.Json;
using RestSharp;
using BuenRide.Portable;
using System.Threading;

namespace BuenRide.And
{
	[Activity (Label = "BuenRide", MainLauncher = true, Icon="@drawable/car")]
	public class MainActivity : Activity
	{
		private const string AppId = "238624846333637";
		private BackendConnection backend;
		private const string ExtendedPermissions = "user_about_me,read_stream,publish_stream";
		string apikey;
		EditText passLI;
		EditText emailLI;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_login);
			PackageInfo info = this.PackageManager.GetPackageInfo ("BuenRide.BuenRide", PackageInfoFlags.Signatures);
			backend = BackendConnection.Instance;
			backend.apikey="apikey";
			backend.url = "http://www.buenrideapp.com";
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
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/usuarios/login", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddParameter("password",passLI.Text);
			request.AddParameter("email", emailLI.Text);
			string content = "";
			client.ExecuteAsync (request, response => {
				content = response.Content; // raw content as string
				Wait.Set();
			});
			Wait.WaitOne();
			Usuario user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
			try {if (user.token == null){ 
				Alert ("Failed to Log In", "Wrong user or password", false, (res) => {} );
			}
			else{
				backend.token = user.token;
				Console.WriteLine (user.token);
				var activity2 = new Intent (this, typeof(PrincipalMenu));
				StartActivity (activity2);
			}
			}	
			catch {
				Alert ("Failed to Log In", "No connection found", false, (res) => {} );
			}
		}
		bool SignIn(string mail,string psswd) {
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/usuarios/login", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddParameter("password",psswd);
			request.AddParameter("email", mail);
			string content = "";
			client.ExecuteAsync (request, response => {
				content = response.Content; // raw content as string
				Wait.Set();
			});
			Wait.WaitOne();
			Usuario user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
			if (user.token == null){ 
					return false;
					Alert ("Failed to Log In", "Wrong user or password", false, (res) => {} );
				}
			else{
				backend.token = user.token;
				Console.WriteLine (user.token);
				var activity2 = new Intent (this, typeof(PrincipalMenu));
				activity2.PutExtra ("token",user.token);
				StartActivity (activity2);
				return true;
			}
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

				string accessToken = data.GetStringExtra ("AccessToken");
				string userId = data.GetStringExtra ("UserId");
				string error = data.GetStringExtra ("Exception");

				FacebookClient fb = new FacebookClient (accessToken);

				fb.GetTaskAsync ("me").ContinueWith( t => {
					if (!t.IsFaulted) {

						var result = (IDictionary<string, object>)t.Result;
						string profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", userId, "square", accessToken);
						var bm = BitmapFactory.DecodeStream (new Java.Net.URL(profilePictureUrl).OpenStream());
						string profileName = (string)result["name"];
						Console.WriteLine(result.ToString());
						if ( !SignIn( (string)result["link"],(string)result["id"]) ){
							Console.WriteLine("CREANDO USUARIO DE FACEBOOK");
							createNewUser((string)result["name"],(string)result["id"], (string)result["first_name"], (string)result["link"]);
							SignIn( (string)result["link"],(string)result["id"]);
						}


					} else {
						Alert ("Failed to Log In", "Reason: " + error, false, (res) => {} );
					}
				});

				break;
			case Result.Canceled:
				Alert ("Failed to Log In", "User Cancelled", false, (res) => {} );
				break;
			default:
				break;
			}
		}

		public void createNewUser(string username,string password, string name, string email){
				EventWaitHandle Wait = new AutoResetEvent(false);
				var client = new RestClient (backend.url);
				var request = new RestRequest ("api/usuarios/registrar", RestSharp.Method.POST);
				request.AddHeader ("apikey", backend.apikey);
				request.AddParameter("username", username);
				request.AddParameter("password", password);
				request.AddParameter("name", name);
				request.AddParameter("email", email);
				client.ExecuteAsync (request, response => {
					Console.WriteLine (response.Content);
					Wait.Set();
				});
				Wait.WaitOne();
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


/*
 * 
 * if (isLoggedIn) {
				
				fb.GetTaskAsync ("me").ContinueWith (t => {
					if (!t.IsFaulted) {

						var result = (IDictionary<string, object>)t.Result;
						
						string data = "Name: " + (string)result["name"] + "\n" + 
							"First Name: " + (string)result["first_name"] + "\n" +
								"Last Name: " + (string)result["last_name"] + "\n" +
								"Profile Url: " + (string)result["link"];
						RunOnUiThread ( () => {
							Alert ("Your Info", data, false, (res) => {} );
						});
					}
				});		Alert ("Not Logged In", "Please Log In First", false, (res) => { });
			}*/