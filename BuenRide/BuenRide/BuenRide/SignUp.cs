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
using System.Threading;
using BuenRide.Portable;
using Newtonsoft.Json;

namespace BuenRide.And
{
	[Activity (Label = "SignUp", Icon="@drawable/car")]			
	public class SignUp : Activity
	{
		EditText name;
		EditText email;
		EditText username;
		EditText password;
		EditText telephone;
		BackendConnection backend;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.activity_sign_up);
			backend = BackendConnection.Instance;
			var btnCancel = FindViewById<View> (Resource.Id.RectangleCancelSignUp);
			var btnSignUp = FindViewById<View> (Resource.Id.RectangleSignUp);
			name = FindViewById<EditText> (Resource.Id.name);
			email = FindViewById<EditText> (Resource.Id.email);
			username = FindViewById<EditText> (Resource.Id.username);
			password = FindViewById<EditText> (Resource.Id.password);
			telephone = FindViewById<EditText> (Resource.Id.telephone);
			btnSignUp.Click += HandleLogin;
			btnCancel.Click += HandleCancel;

		}
		void insert(){
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/usuarios/registrar", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddParameter("username", username.Text);
			request.AddParameter("password", password.Text);
			request.AddParameter("name", name.Text);
			request.AddParameter("email", email.Text);
			request.AddParameter("phone", telephone.Text);
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
				Wait.Set();
			});
			Wait.WaitOne();
			request = new RestRequest ("api/usuarios/login", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddParameter("password",password.Text);
			request.AddParameter("email", email.Text);
			string content = @"{}";
			client.ExecuteAsync (request, response => {
				content = response.Content; // raw content as string
				Wait.Set();
			});
			Wait.WaitOne();
			Usuario user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
			try {if (user.token == null){ 
				Alert ("Failed to Log In", "Error creating user", false, (res) => {} );
			}
			else{
				Console.WriteLine (user.token);
				var activity2 = new Intent (this, typeof(PrincipalMenu));
				backend.token = user.token;
				StartActivity (activity2);
				}	
		}catch{
				Alert ("Failed to Log In", "No connection found", false, (res) => {} );
			}
		}
		void HandleLogin(object sender, EventArgs e)
		{
			if (username.Text != "" && password.Text != "" && name.Text != "" && email.Text != "" && telephone.Text != "") {
				insert ();
			} else {
				Alert ("Alert", "Please fill all the spaces", false, (res) => {} );
			}
		}
		
		void HandleCancel(object sender, EventArgs e)
		{
			Finish ();
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

