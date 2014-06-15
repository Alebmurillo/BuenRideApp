package buenride.and;


public class SearchRideMenu
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.OnInfoWindowClickListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onInfoWindowClick:(Lcom/google/android/gms/maps/model/Marker;)V:GetOnInfoWindowClick_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IOnInfoWindowClickListenerInvoker, GooglePlayServicesLib\n" +
			"";
		mono.android.Runtime.register ("BuenRide.And.SearchRideMenu, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SearchRideMenu.class, __md_methods);
	}


	public SearchRideMenu () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SearchRideMenu.class)
			mono.android.TypeManager.Activate ("BuenRide.And.SearchRideMenu, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onInfoWindowClick (com.google.android.gms.maps.model.Marker p0)
	{
		n_onInfoWindowClick (p0);
	}

	private native void n_onInfoWindowClick (com.google.android.gms.maps.model.Marker p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
