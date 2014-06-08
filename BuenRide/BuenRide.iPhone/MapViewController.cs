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

		public MapViewController (IntPtr handle) : base (handle)
		{
		}

		public override void LoadView ()
		{
			base.LoadView ();

			var camera = CameraPosition.FromCamera (latitude: 37.797865, 
				longitude: -122.402526, 
				zoom: 6);
			mapView = MapView.FromCamera (System.Drawing.RectangleF.Empty, camera);
			mapView.MyLocationEnabled = true;
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
