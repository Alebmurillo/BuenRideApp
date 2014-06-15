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
using Android.Gms.Maps;

namespace BuenRide.And
{
	[Activity (Label = "CalculateCost", Icon="@drawable/car")]			
	public class CalculateCost : Activity
	{
		double startLat = 0;
		double startLong = 0;
		bool startPos;
		double endLat = 0;
		double endLong = 0;
		int daysPerW = 0;
		int profite = 0;
		int gasPerL = 0;
		TextView ByDay;
		TextView ByWeek;
		TextView ByMonth;
		NumberPicker numberPickerGass;
		NumberPicker numberPickerDays;
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			startPos = true;
			SetContentView (Resource.Layout.activity_calculate_cost_main);
			var btnStartCurrentLocation = FindViewById<View> (Resource.Id.RectangleCurrentLocation);
			var btnStartCurrentMap = FindViewById<View> (Resource.Id.RectangleByMap);
			var btnEndCurrentMap = FindViewById<View> (Resource.Id.RectangleCurrentLocationGPS);
			var btnEndCurrentLocation = FindViewById<View> (Resource.Id.RectangleByMapEnd);
			var btnCalculate = FindViewById<View> (Resource.Id.RectangleCalculate);

			ByDay = FindViewById<TextView> (Resource.Id.textView_calcDayValue);
			ByWeek = FindViewById<TextView> (Resource.Id.textView_calcWeekValue);
			ByMonth = FindViewById<TextView> (Resource.Id.textView_calcMonthValue);
			SeekBar _seekBar = FindViewById<SeekBar>(Resource.Id.seekBarProfite);
			_seekBar.ProgressChanged += (object sender, SeekBar.ProgressChangedEventArgs e) => {
				if (e.FromUser)
				{
					profite = e.Progress;
				}
			};

			numberPickerGass = FindViewById<NumberPicker>(Resource.Id.numberPickerGass);
			numberPickerGass.MaxValue = 200000;
			numberPickerGass.MinValue = 0;
			numberPickerGass.Value = 3000;

			numberPickerDays = FindViewById<NumberPicker>(Resource.Id.numberPickerDays);
			numberPickerDays.MaxValue = 7;
			numberPickerDays.MinValue = 0;
			numberPickerDays.Value = 5;

			btnStartCurrentLocation.Click += HandleStartCurrentLocation;
			btnStartCurrentMap.Click += HandleStartCurrentLocationMap;
			btnEndCurrentMap.Click += HandleEndCurrentLocation;
			btnEndCurrentLocation.Click += HandleEndCurrentLocationMap; 
			btnCalculate.Click += HandleCalculate;
			// Create your application here
		}
		void HandleStartCurrentLocationMap(object sender, EventArgs e)
		{
			startPos = true;
			var activity = new Intent (this, typeof(ShowMap));
			StartActivityForResult (activity, 0);
		}
		void HandleEndCurrentLocationMap(object sender, EventArgs e)
		{
			startPos = false;
			var activity = new Intent (this, typeof(ShowMap));
			StartActivityForResult (activity, 0);
		}
		void HandleStartCurrentLocation(object sender, EventArgs e)
		{	
			startPos = true;
			var activity = new Intent (this, typeof(WaitForGPS));
			StartActivityForResult (activity, 0);
		}
		void HandleEndCurrentLocation(object sender, EventArgs e)
		{	
			startPos = false;
			var activity = new Intent (this, typeof(WaitForGPS));
			StartActivityForResult (activity, 0);
		}
		void HandleCalculate(object sender, EventArgs e){

			gasPerL = numberPickerGass.Value;
			daysPerW = numberPickerDays.Value;
			double factor = Math.Abs(startLat-endLat)+ Math.Abs(startLong-endLong);
			double cost = gasPerL*factor + (gasPerL * factor * profite);
			ByDay.Text = " "+ (cost/4)/ daysPerW;
			ByWeek.Text = " " + cost/4;
			ByMonth.Text = " " + cost;
		}
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
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

