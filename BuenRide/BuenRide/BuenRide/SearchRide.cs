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
//using BuenRide.Portable;
using Newtonsoft.Json;
using RestSharp;
using BuenRide.Portable;

namespace BuenRide.And
{
	[Activity (Label = "SearchRide")]			
	public class SearchRide : ListActivity
	{
		List<string> reviewsToShow = new List<string>();
		protected override void OnCreate(Bundle bundle)
		{
			/*	base.OnCreate(bundle);
			string content = Intent.GetStringExtra ("contentJson") ?? "";
			List<Reviews> review = JsonConvert.DeserializeObject<List<Portable.RideUser>>(content);
			Console.WriteLine (review.Count);
			for (int i = 0; i < review.Count; i++) {
				reviewsToShow.Add (" Calification: " + review [i].calificacion + "\n Comment: " + review [i].comentario);
			}
			ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, reviewsToShow.ToArray());*/
		}
	}}
