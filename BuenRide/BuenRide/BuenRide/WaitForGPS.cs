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
using Android.Locations;

namespace BuenRide.And
{
	[Activity (Label = "WaitForGPS")]			
	public class WaitForGPS : Activity,ILocationListener
	{
		Location _currentLocation;
		LocationManager _locationManager;
		TextView _locationText;
		TextView _addressText;
		String _locationProvider;
		Button btnOK;
		double lat;
		double lon;

		public void OnLocationChanged(Location location)
		{
			_currentLocation = location;
			if (_currentLocation == null)
			{
				_locationText.Text = "Unable to determine your location.";
			}
			else
			{
				_locationText.Text = "Located";//String.Format("{0},{1}", _currentLocation.Latitude, _currentLocation.Longitude);
				lat = _currentLocation.Latitude;
				lon = _currentLocation.Longitude;
				btnOK.Visibility = ViewStates.Visible;
			}
		}

		public void OnProviderDisabled(string provider) {}

		public void OnProviderEnabled(string provider) {}

		public void OnStatusChanged(string provider, Availability status, Bundle extras) {}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.activity_gps);
			btnOK = FindViewById<Button> (Resource.Id.buttonOK);
			btnOK.Visibility = ViewStates.Invisible;
			btnOK.Click += Handleok;
			_locationText = FindViewById<TextView>(Resource.Id.textView1);

			InitializeLocationManager();
			// Create your application here
		}
		void InitializeLocationManager()
		{
			_locationManager = (LocationManager)GetSystemService(LocationService);
			Criteria criteriaForLocationService = new Criteria
			{
				Accuracy = Accuracy.Fine
			};
			IList<string> acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

			if (acceptableLocationProviders.Any())
			{
				_locationProvider = acceptableLocationProviders.First();
			}
			else
			{
				_locationProvider = String.Empty;
			}
		}
		protected override void OnResume()
		{
			base.OnResume();
			_locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
		}
		protected override void OnPause()
		{
			base.OnPause();
			_locationManager.RemoveUpdates(this);
		}
		void Handleok(object sender, EventArgs e){
			Intent myIntent = new Intent (this, typeof(CalculateCost));
			myIntent.PutExtra ("latitude", ""+lat);
			myIntent.PutExtra ("longitude", ""+lon);
			SetResult (Result.Ok, myIntent);
			Finish ();
		}
	}
}

