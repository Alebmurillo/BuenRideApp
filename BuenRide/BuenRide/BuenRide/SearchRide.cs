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
using BuenRide.Portable;
using Newtonsoft.Json;
using RestSharp;

namespace BuenRide.And
{
	[Activity (Label = "SearchRide")]			
	public class SearchRide : ListActivity ,GoogleMap.IOnInfoWindowClickListener
	{
		List<string> items = new List<string>();
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			string content = Intent.GetStringExtra ("contentJson") ?? "";
			List<Portable.Ride> rides = JsonConvert.DeserializeObject<List<Portable.Ride>>(content);
			Console.WriteLine (content);
			Console.WriteLine (rides.Count);
			for (int i = 0; i < rides.Count; i++) {
				string ridestr = "Ride" + i + ": \n" + " Lat i:" + rides [i].startPointLat + " Lon i:" + rides [i].startPointLong + "\n Lat f:" + rides [i].endPointLat + "Lon f:" + rides [i].endPointLong + "\n Comments:" +  rides [i].observations;
				items.Add(ridestr);
			}
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, items.ToArray());


		}
	}}
