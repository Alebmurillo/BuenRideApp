// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace BuenRide.iPhone
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView Login { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollSignUp { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView SignIn { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView Splash { get; set; }

		[Action ("UIButton189_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton189_TouchUpInside (UIButton sender);

		[Action ("UIButton194_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton194_TouchUpInside (UIButton sender);

		[Action ("UIButton203_TouchUpInside:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void UIButton203_TouchUpInside (UIButton sender);

		void ReleaseDesignerOutlets ()
		{
			if (Login != null) {
				Login.Dispose ();
				Login = null;
			}
			if (ScrollSignUp != null) {
				ScrollSignUp.Dispose ();
				ScrollSignUp = null;
			}
			if (SignIn != null) {
				SignIn.Dispose ();
				SignIn = null;
			}
			if (Splash != null) {
				Splash.Dispose ();
				Splash = null;
			}
		}
	}
}
