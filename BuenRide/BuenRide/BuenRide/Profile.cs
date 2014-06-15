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
using System.Threading;
using RestSharp;
using BuenRide.Portable;
 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BuenRide.And
{
	[Activity (Label = "Profile", Icon="@drawable/car")]			
	public class Profile : Activity
	{
		BackendConnection backend = BackendConnection.Instance;
		string revList = "";
		string id = "0";
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_profile);

			string textID = Intent.GetStringExtra ("id") ?? "true";
			string lat = Intent.GetStringExtra ("lat") ?? "false";
			string lon = Intent.GetStringExtra ("long") ?? "false";

			View wazeView = FindViewById<View> (Resource.Id.RectangleViewRides);
			TextView wazeTextView = FindViewById<TextView> (Resource.Id.textView_viewRides);
			if (lat != "false") {
				wazeView.Click += (object sender, EventArgs e) => {
					String url = "waze://?ll="+lat+","+lon+"&z=10";
					var intent = new Intent (Intent.ActionView,Android.Net.Uri.Parse ( url));
					StartActivity( intent );
				};
			} else {
				wazeView.Visibility = ViewStates.Invisible;
				wazeTextView.Visibility = ViewStates.Invisible;
			}

			TextView textMail = FindViewById<TextView> (Resource.Id.textView_exampleMail);
			TextView textPhone = FindViewById<TextView> (Resource.Id.textView_examplePhone);
			TextView textName = FindViewById<TextView> (Resource.Id.textViewNombreApellido);
			var AddReview = FindViewById<View> (Resource.Id.RectangleAddReview);
			AddReview.Click += (sender, e) => {
				 var transaction = FragmentManager.BeginTransaction();
				 var dialogFragment = new MyDialogFragment();
				 dialogFragment.Show(transaction, "dialog_fragment");
			};

			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			string content = "";

			if (textID != "true") {
				var request = new RestRequest ("api/usuarios/getUsuario_by_id", RestSharp.Method.POST);
				request.AddHeader ("apikey", backend.apikey);
				request.AddHeader ("token", backend.token);
				request.AddParameter ("id", textID);
				client.ExecuteAsync (request, response => {
					content = response.Content; // raw content as string
					Wait.Set ();
				});
			} else {
				var request = new RestRequest ("api/usuarios/myUsuario", RestSharp.Method.GET);
				request.AddHeader ("apikey", backend.apikey);
				request.AddHeader ("token", backend.token);
				client.ExecuteAsync (request, response => {
					content = response.Content; // raw content as string
					Wait.Set ();
				});
			}
			Wait.WaitOne();
			Usuario user = JsonConvert.DeserializeObject<Portable.Usuario>(content);
			textMail.Text = user.email;
			textPhone.Text = user.telefono;
			textName.Text = user.username;
			id = user.id + "";
			getReviews();
		}

		public void getReviews(){
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/reviews/getReviews_by_id", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddHeader ("token", backend.token);
			request.AddParameter ("id", id);
			string content = "";
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
				content = response.Content;
				Wait.Set();
			});
			Wait.WaitOne();
			revList = content;
			var newFragment = new ShowReview ();
			var ft = FragmentManager.BeginTransaction ();
			var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragment_container_reviews);
			if (fragment != null)
				ft.Remove(fragment);    
			ft.Add (Resource.Id.fragment_container_reviews, newFragment);
			ft.Commit ();
		}

		public class ShowReview : ListFragment
		{

			public override void OnActivityCreated(Bundle savedInstanceState)
			{
				base.OnActivityCreated(savedInstanceState);
				var myActivity = (Profile) this.Activity;
				string content = myActivity.revList;
				Console.WriteLine (content);
				List<string> reviewsUsername = new List<string>();
				JArray obj = JArray.Parse (content);
				foreach(var item in obj.Children())
				{
					var itemProperties = item.Children<JProperty>();
					//you could do a foreach or a linq here depending on what you need to do exactly with the value
					var myElement = itemProperties.FirstOrDefault(x => x.Name == "submitted_by");
					var element = myElement.ElementAt (0).ElementAt(1);
					string name = element.ElementAt (0).ToString();
					reviewsUsername.Add (name);
					//		var myElementValue = JsonConvert.DeserializeObject<Usuario>(myElement.Value.ToString); ////This is a JValue type
				}

				Console.WriteLine ("@@@@@@ " + content);
				List<Reviews> review = JsonConvert.DeserializeObject<List<Portable.Reviews>>(content);
				List<string> reviewsToShow = new List<string>();
				Console.WriteLine ("Content" + content);
				for (int i = 0; i < review.Count; i++) {
					reviewsToShow.Add (" Comment: " + review [i].comentario + "\n Rating: " + review [i].calificacion+ " by: "+reviewsUsername[i]);
				}
				Console.WriteLine (content);
				this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1, reviewsToShow.ToArray());
			}
		}


		public class MyDialogFragment : DialogFragment
		{
			private int _clickCount;
			public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				base.OnCreate (savedInstanceState);
				var myActivity = (Profile) this.Activity;
				TextView nombre = myActivity.FindViewById<TextView> (Resource.Id.textViewNombreApellido);
				var view = inflater.Inflate(Resource.Layout.dialogAddReview, container, false);
				EditText comment = view.FindViewById<EditText> (Resource.Id.editTextReview);
				RatingBar rb = view.FindViewById<RatingBar> (Resource.Id.ratingBar1);
				view.FindViewById<Button>(Resource.Id.buttonAddReview).Click += delegate
				{
					BackendConnection backend = BackendConnection.Instance;
					EventWaitHandle Wait = new AutoResetEvent(false);
					var client = new RestClient (backend.url);
					var request = new RestRequest ("api/reviews/setReview", RestSharp.Method.POST);
					request.AddHeader ("apikey", backend.apikey);
					request.AddHeader ("token", backend.token);
					//username

					request.AddParameter("comentario",comment.Text);
					request.AddParameter("username", nombre.Text);
					request.AddParameter("calificacion", rb.Rating+"");
					string content = "";
					client.ExecuteAsync (request, response => {
						content = response.Content; 
						Console.WriteLine(content);
						Wait.Set();
					});
					Wait.WaitOne();
					myActivity.getReviews();
					Dismiss();
				};
				// Set up a handler to dismiss this DialogFragment when this button is clicked.
				view.FindViewById<Button>(Resource.Id.buttonCancelReview).Click += (sender, args) => Dismiss();
				return view;
			}}
	}
}

