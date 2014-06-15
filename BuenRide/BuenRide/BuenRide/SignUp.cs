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

namespace BuenRide.And
{
	[Activity (Label = "SignUp")]			
	public class SignUp : Activity
	{
		EditText name;
		EditText email;
		EditText username;
		EditText password;
		EditText telephone;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_sign_up);
			// Create your application here
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
			var client = new RestClient ("http://www.buenrideapp.com");
			var request = new RestRequest ("api/usuarios/", RestSharp.Method.POST);
			request.AddParameter("username", username.Text);
			request.AddParameter("password", password.Text);
			request.AddParameter("name", name.Text);
			request.AddParameter("email", email.Text);
			request.AddParameter("phone", telephone.Text);
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
			});
		}
		void HandleLogin(object sender, EventArgs e)
		{
			if (username.Text != "" && password.Text != "" && name.Text != "" && email.Text != "" && telephone.Text != "") {
				insert ();
				var activity2 = new Intent (this, typeof(PrincipalMenu));
				StartActivity (activity2);
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

