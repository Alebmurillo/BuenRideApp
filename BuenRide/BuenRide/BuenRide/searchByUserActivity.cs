
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
using BuenRide.Portable;
using Newtonsoft.Json;
using System.Threading;
using RestSharp;

namespace BuenRide.And
{
	[Activity (Label = "Search by User", Icon="@drawable/car")]			
	public class searchByUserActivity : Activity
	{
		private BackendConnection backend;
		string contentRide ="";
		List <string>autoCompleteOptions = new List<string>();
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.activity_list_rides);
			backend = BackendConnection.Instance;
			AutoCompleteTextView autocompleteTextView = FindViewById<AutoCompleteTextView> (Resource.Id.AutoCompleteInput);

			//EditText search = FindViewById<EditText>(Resource.Id.searchRide);
			autocompleteTextView.TextChanged += (object sender, Android.Text.TextChangedEventArgs e) => {
				Console.WriteLine("+++++++++++++++++++++++");
				searchAndShow(((EditText)sender).Text);
			};
			Button searchButt = FindViewById<Button> (Resource.Id.buttonSearchUser);
			searchButt.Click += (object sender, EventArgs e) => {
				autoCompleteOptions.Add(autocompleteTextView.Text);
				ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, autoCompleteOptions.ToArray());
				autocompleteTextView.Adapter = autoCompleteAdapter;
			};
		}
		public void searchAndShow(string criteria){
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/rides/find_by_user", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddHeader ("token", backend.token);
			request.AddParameter ("search",criteria );
			string content = "";
			client.ExecuteAsync (request, response => {
				Console.WriteLine (response.Content);
				content = response.Content;
				Wait.Set();
			});
			Wait.WaitOne();
			contentRide = content;
			Console.WriteLine (criteria);
			Console.WriteLine (content);
			var newFragment = new ShowSearchRides ();
			var ft = FragmentManager.BeginTransaction ();
			var fragment = this.FragmentManager.FindFragmentById(Resource.Id.ridesFC);
			if (fragment != null)
				ft.Remove(fragment);    
			ft.Add (Resource.Id.ridesFC, newFragment);
			ft.Commit ();
		}


		public class ShowSearchRides : ListFragment
		{
			List<string> usersId = new List<string> ();
			searchByUserActivity myActivity;
			public override void OnActivityCreated(Bundle savedInstanceState)
			{
				base.OnActivityCreated(savedInstanceState);
				myActivity = (searchByUserActivity) this.Activity;
				string content = myActivity.contentRide;
				Console.WriteLine (content);
			    
				List<Usuario> ride = JsonConvert.DeserializeObject<List<Usuario>>(content);
				List<string> ridesToShow = new List<string>();
				Console.WriteLine ("Content" + content);
				for (int i = 0; i < ride.Count; i++) {
					ridesToShow.Add ("Name : " + ride [i].username + "\n Phone:"+ ride[i].telefono);
					usersId.Add (ride[i].id.ToString());
				}
				Console.WriteLine (content);
				this.ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleExpandableListItem1, ridesToShow.ToArray());
			}
			public override void OnListItemClick(ListView l, View v, int index, long id)
			{
				ListView.SetItemChecked(index, true);
				var activity2 = new Intent (this.Activity, typeof(Profile));
				activity2.PutExtra ("id", usersId[index]);
				StartActivity (activity2);

			}
		}
	}
}

