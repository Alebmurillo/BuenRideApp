using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Google.Maps;
using MonoTouch.FacebookConnect;

namespace BuenRide.iPhone
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations

		const string MapsApiKey = "AIzaSyBKLJ6pOaSzWJ7_w7msvmLsRzRXBgo_G6s";

		// Get your own App ID at developers.facebook.com/apps
		const string FacebookAppId = "238624846333637";
		const string DisplayName = "buenrideapp";
		
		public override UIWindow Window {
			get;
			set;
		}

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			MapServices.ProvideAPIKey (MapsApiKey);
			FBSettings.DefaultAppID = FacebookAppId;
			FBSettings.DefaultDisplayName = DisplayName;
			return true;
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}

		public override bool OpenUrl (UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
		{
			// We need to handle URLs by passing them to FBSession in order for SSO authentication
			// to work.
			return FBSession.ActiveSession.HandleOpenURL(url);
		}

		public override void OnActivated (UIApplication application)
		{
			// We need to properly handle activation of the application with regards to SSO
			// (e.g., returning from iOS 6.0 authorization dialog or from fast app switching).
			FBSession.ActiveSession.HandleDidBecomeActive();
		}
	}
}

