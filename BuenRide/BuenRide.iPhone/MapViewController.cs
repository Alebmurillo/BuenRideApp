using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;
using Google.Maps;

namespace BuenRide.iPhone
{
	partial class MapViewController : UIViewController
	{
		MapView mapView;
		Portable.Location gpsLocation;

		public MapViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			mapView.LongPress += (sender, e) => {
				var marker = new Marker () {
					Position = e.Coordinate,
					AppearAnimation = MarkerAnimation.Pop,
					Map = mapView
				};
			};

			mapView.TappedMarker = (aMapView, aMarker) => {
				// Animate to the marker

				gpsLocation = new Portable.Location(aMarker.Position.Latitude.ToString(), aMarker.Position.Longitude.ToString());
				NavigationController.PopViewControllerAnimated (true);
				// The Tap has been handled so return YES
				return true;
			};
		}
			
		public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
		{
			base.PrepareForSegue (segue, sender);

			// do first a control on the Identifier for your segue
			if (segue.Identifier.Equals("your_identifier")) {

				var destination = (MainViewController)segue.DestinationViewController;
				destination.gpsLocation = gpsLocation;
			}
		}

		public override void LoadView ()
		{
			base.LoadView ();

			var camera = CameraPosition.FromCamera (latitude: 37.785834, 
				longitude: -122.406417, 
				zoom: 6);
			mapView = MapView.FromCamera (System.Drawing.RectangleF.Empty, camera);
			mapView.MyLocationEnabled = true;
			mapView.Settings.CompassButton = true;
			mapView.Settings.MyLocationButton = true;
			View = mapView;
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			mapView.StartRendering ();
		}

		public override void ViewWillDisappear (bool animated)
		{	
			mapView.StopRendering ();
			base.ViewWillDisappear (animated);
		}
	}
}
