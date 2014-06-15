
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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using System.Threading;
using RestSharp;
using BuenRide.Portable;
using Newtonsoft.Json;
using Android.Support.V4.App;

namespace BuenRide.And
{
	[Activity (Label = "SearchRideMenu", Icon="@drawable/car")]			
	public class SearchRideMenu : Activity, GoogleMap.IOnInfoWindowClickListener 
	{
		MapFragment mapFrag;
		List<string> id_tags;
		List<int> id_rides;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			id_tags = new List<string> ();
			id_rides = new List<int> ();
			ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
			SetContentView(Resource.Layout.layout_SearchRide);
			ActionBar.Tab tab = ActionBar.NewTab();
			tab.SetText("By Start");
			tab.SetIcon(Resource.Layout.rectangle);
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {   
				var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
				if (fragment != null)
					e.FragmentTransaction.Remove(fragment);       
				e.FragmentTransaction.Add (Resource.Id.fragmentContainer,
					new TabByStartFragment ());
			};
			ActionBar.AddTab(tab);
			tab = ActionBar.NewTab();
			tab.SetText("By destination");
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {        
				var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
				if (fragment != null)
					e.FragmentTransaction.Remove(fragment);       
				e.FragmentTransaction.Add (Resource.Id.fragmentContainer,
					new  TabByDestFragment());
			};
			ActionBar.AddTab(tab);
			tab = ActionBar.NewTab();
			tab.SetText("Other");
			tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e) {     
				var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
				if (fragment != null)
					e.FragmentTransaction.Remove(fragment);       
				e.FragmentTransaction.Add (Resource.Id.fragmentContainer,
					new TabByUserFragment ());
			};
			ActionBar.AddTab(tab);

		}
		//lo que quiero que el marker haga
		public void OnInfoWindowClick( Marker marker)
		{
			var activity2 = new Intent (this, typeof(Profile));

			int index = id_tags.IndexOf (marker.Title);
			int id = id_rides [index];
			Console.WriteLine (id);
			activity2.PutExtra ("id", ""+id);
			activity2.PutExtra ("lat", "0");
			activity2.PutExtra ("long", "0");
			StartActivity (activity2);
		}
		public void SearchOnMap(int miles, bool bydestination, string latS, string longS, string latE, string longE){
			id_tags = new List<string> ();
			id_rides = new List<int> ();			
			float color= BitmapDescriptorFactory.HueCyan;
			mapFrag = (MapFragment) FragmentManager.FindFragmentById(Resource.Id.ride_mapfragment_container);
			GoogleMap map = mapFrag.Map;
			map.Clear ();
			map.MoveCamera(CameraUpdateFactory.NewLatLngZoom (new LatLng (Convert.ToDouble (latS), Convert.ToDouble (longS)), 9));
			if (bydestination) {
				color = BitmapDescriptorFactory.HueOrange;
				map.MoveCamera(CameraUpdateFactory.NewLatLngZoom (new LatLng (Convert.ToDouble (latE), Convert.ToDouble (longE)),9));
			}
			Console.WriteLine ("show by miles");
			//Consulta
			BackendConnection backend = BackendConnection.Instance;
			EventWaitHandle Wait = new AutoResetEvent(false);
			var client = new RestClient (backend.url);
			var request = new RestRequest ("api/rides/find_by_route", RestSharp.Method.POST);
			request.AddHeader ("apikey", backend.apikey);
			request.AddHeader ("token", backend.token);
			request.AddParameter("radio",""+ miles);
			request.AddParameter("startLatitud",latS);
			request.AddParameter("startLongitud",longS);
			request.AddParameter("destLatitud",latE);
			request.AddParameter("destLongitud",longE);
			string content = "";
			client.ExecuteAsync (request, response => {
				content = response.Content; 
				Console.WriteLine(content);
				Wait.Set();
			});
			Wait.WaitOne();
			try{
			List<Portable.RideUser> rides = JsonConvert.DeserializeObject<List<Portable.RideUser>>(content);
			Console.WriteLine (rides.Count);
			for (int i = 0; i < rides.Count; i++) {
				string ridestr = "Ride" + i + ": \n" + " Lat i:" + rides [i].startPointLat + " Lon i:" + rides [i].startPointLong + "\n Lat f:" + rides [i].destPointLat + "Lon f:" + rides [i].destPointLong + "\n Comments:" + rides [i].observations + "User" + rides[i].usuario_id;
				Console.WriteLine (ridestr);
				MarkerOptions markerOpt1 = new MarkerOptions ();
					if (bydestination) {
						markerOpt1.SetPosition (new LatLng (Convert.ToDouble(rides [i].destPointLat), Convert.ToDouble(rides [i].destPointLong)));
					}
					else{
						markerOpt1.SetPosition (new LatLng (Convert.ToDouble(rides [i].startPointLat), Convert.ToDouble(rides [i].startPointLong)));
					}
				markerOpt1.InvokeIcon(BitmapDescriptorFactory.DefaultMarker (color));
				markerOpt1.SetTitle ("Ride "+ i);
				id_tags.Add ("Ride "+ i);
				id_rides.Add (Convert.ToInt32(rides [i].usuario_id));
				map.AddMarker (markerOpt1);
				map.SetOnInfoWindowClickListener(this);

				}}
			catch{
				Console.WriteLine ("ERROR DE CONEXION");
			}
		}

		class TabByStartFragment: Android.App.Fragment
		{           
				int miles = 0;
				double startLat = 0;
				double startLong = 0;
				bool startPos;
				double endLat = 0;
				double endLong = 0;
				public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
				{
					base.OnCreateView (inflater, container, savedInstanceState);
					var view = inflater.Inflate (Resource.Layout.LayoutSearchbyMap, container, false);
					var myActivity = (SearchRideMenu) this.Activity;

					RadioButton radio_five = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
					RadioButton radio_twe = view.FindViewById<RadioButton>(Resource.Id.radioButton2);
					RadioButton radio_forth = view.FindViewById<RadioButton>(Resource.Id.radioButton3);

					var btnStartCurrentLocation = view.FindViewById<View> (Resource.Id.startbuttonbyGPSSeach);
					var btnStartCurrentMap = view.FindViewById<View> (Resource.Id.startbuttonbyMapSearch);
					var btnEndCurrentMap = view.FindViewById<View> (Resource.Id.endbuttonbyGPSSearch);
					var btnEndCurrentLocation = view.FindViewById<View> (Resource.Id.endbuttonbyMapSearch);

					btnStartCurrentLocation.Click += HandleStartCurrentLocation;
					btnStartCurrentMap.Click += HandleStartCurrentLocationMap;
					btnEndCurrentMap.Click += HandleEndCurrentLocation;
					btnEndCurrentLocation.Click += HandleEndCurrentLocationMap; 

					var okButton = view.FindViewById<Button>(Resource.Id.buttonOKMap);
					radio_five.Click += (sender, e) => {miles = 5;};
					radio_twe.Click += (sender, e) => {miles = 20;};
					radio_forth.Click += (sender, e) => {miles = 40;};
				okButton.Click += (sender, e) => {myActivity.SearchOnMap(miles, false, ""+startLat,""+startLong, ""+endLat,""+endLong);};
					return view;
				}
				void HandleStartCurrentLocationMap(object sender, EventArgs e)
				{
					startPos = true;
					var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(ShowMap));
					StartActivityForResult (activity, 0);
				}
				void HandleEndCurrentLocationMap(object sender, EventArgs e)
				{
					startPos = false;
					var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(ShowMap));
					StartActivityForResult (activity, 0);
				}
				void HandleStartCurrentLocation(object sender, EventArgs e)
				{	
					startPos = true;
					var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(WaitForGPS));
					StartActivityForResult (activity, 0);
				}
				void HandleEndCurrentLocation(object sender, EventArgs e)
				{	
					startPos = false;
					var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(WaitForGPS));
					StartActivityForResult (activity, 0);
				}
				override public void OnActivityResult(int requestCode, Result resultCode, Intent data)
				{
					base.OnActivityResult(requestCode, resultCode, data);
					if (resultCode == Result.Ok) {
						if (startPos) {
							startLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
							startLong = Convert.ToDouble (data.GetStringExtra ("longitude"));
						} else {

							endLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
							endLong = Convert.ToDouble (data.GetStringExtra ("longitude"));

						}
					}
				}
		}

		class TabByUserFragment: Android.App.Fragment
		{  
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				base.OnCreateView (inflater, container, savedInstanceState);
				var view = inflater.Inflate (Resource.Layout.layout_search_other, container, false);

				var btnShowAll = view.FindViewById<View> (Resource.Id.buttonAll);
				var btnSearchByUser = view.FindViewById<View> (Resource.Id.buttonSearch);

				btnShowAll.Click += HandleShowAll;
				btnSearchByUser.Click += HandleSearchByUser;
				return view;
			}
			void HandleShowAll(object sender, EventArgs e)
			{
				var myActivity = (SearchRideMenu) this.Activity;
				myActivity.SearchOnMap(0, true, "0","0","0","0");
			}

			void HandleSearchByUser(object sender, EventArgs e)
			{	
				var activity = new Intent (this.Activity, typeof(searchByUserActivity));
				StartActivity (activity);
			}

		}


		class TabByDestFragment: Android.App.Fragment
		{            
			int miles = 0;
			double startLat = 0;
			double startLong = 0;
			bool startPos;
			double endLat = 0;
			double endLong = 0;
			public override View OnCreateView (LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
			{
				base.OnCreateView (inflater, container, savedInstanceState);
				var view = inflater.Inflate (Resource.Layout.LayoutSearchbyMap, container, false);
				var myActivity = (SearchRideMenu) this.Activity;

				RadioButton radio_five = view.FindViewById<RadioButton>(Resource.Id.radioButton1);
				RadioButton radio_twe = view.FindViewById<RadioButton>(Resource.Id.radioButton2);
				RadioButton radio_forth = view.FindViewById<RadioButton>(Resource.Id.radioButton3);

				var btnStartCurrentLocation = view.FindViewById<View> (Resource.Id.startbuttonbyGPSSeach);
				var btnStartCurrentMap = view.FindViewById<View> (Resource.Id.startbuttonbyMapSearch);
				var btnEndCurrentMap = view.FindViewById<View> (Resource.Id.endbuttonbyGPSSearch);
				var btnEndCurrentLocation = view.FindViewById<View> (Resource.Id.endbuttonbyMapSearch);

				btnStartCurrentLocation.Click += HandleStartCurrentLocation;
				btnStartCurrentMap.Click += HandleStartCurrentLocationMap;
				btnEndCurrentMap.Click += HandleEndCurrentLocation;
				btnEndCurrentLocation.Click += HandleEndCurrentLocationMap; 

				var okButton = view.FindViewById<Button>(Resource.Id.buttonOKMap);
				radio_five.Click += (sender, e) => {miles = 5;};
				radio_twe.Click += (sender, e) => {miles = 20;};
				radio_forth.Click += (sender, e) => {miles = 40;};
				okButton.Click += (sender, e) => {
					myActivity.SearchOnMap(miles, true, ""+startLat,""+startLong, ""+endLat,""+endLong);};
				return view;
			}
			void HandleStartCurrentLocationMap(object sender, EventArgs e)
			{
				startPos = true;
				var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(ShowMap));
				StartActivityForResult (activity, 0);
			}
			void HandleEndCurrentLocationMap(object sender, EventArgs e)
			{
				startPos = false;
				var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(ShowMap));
				StartActivityForResult (activity, 0);
			}
			void HandleStartCurrentLocation(object sender, EventArgs e)
			{	
				startPos = true;
				var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(WaitForGPS));
				StartActivityForResult (activity, 0);
			}
			void HandleEndCurrentLocation(object sender, EventArgs e)
			{	
				startPos = false;
				var activity = new Intent ( (SearchRideMenu) this.Activity, typeof(WaitForGPS));
				StartActivityForResult (activity, 0);
			}
			override public void OnActivityResult(int requestCode, Result resultCode, Intent data)
			{
				base.OnActivityResult(requestCode, resultCode, data);
				if (resultCode == Result.Ok) {
					if (startPos) {
						startLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
						startLong = Convert.ToDouble (data.GetStringExtra ("longitude"));
					} else {

						endLat = Convert.ToDouble (data.GetStringExtra ("latitude"));
						endLong = Convert.ToDouble (data.GetStringExtra ("longitude"));

					}
				}
			}
		}
	}
}

