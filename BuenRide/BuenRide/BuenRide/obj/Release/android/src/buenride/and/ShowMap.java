package buenride.and;


public class ShowMap
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer,
		com.google.android.gms.maps.GoogleMap.OnMarkerDragListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onMarkerDrag:(Lcom/google/android/gms/maps/model/Marker;)V:GetOnMarkerDrag_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IOnMarkerDragListenerInvoker, GooglePlayServicesLib\n" +
			"n_onMarkerDragEnd:(Lcom/google/android/gms/maps/model/Marker;)V:GetOnMarkerDragEnd_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IOnMarkerDragListenerInvoker, GooglePlayServicesLib\n" +
			"n_onMarkerDragStart:(Lcom/google/android/gms/maps/model/Marker;)V:GetOnMarkerDragStart_Lcom_google_android_gms_maps_model_Marker_Handler:Android.Gms.Maps.GoogleMap/IOnMarkerDragListenerInvoker, GooglePlayServicesLib\n" +
			"";
		mono.android.Runtime.register ("BuenRide.And.ShowMap, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ShowMap.class, __md_methods);
	}


	public ShowMap () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ShowMap.class)
			mono.android.TypeManager.Activate ("BuenRide.And.ShowMap, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onMarkerDrag (com.google.android.gms.maps.model.Marker p0)
	{
		n_onMarkerDrag (p0);
	}

	private native void n_onMarkerDrag (com.google.android.gms.maps.model.Marker p0);


	public void onMarkerDragEnd (com.google.android.gms.maps.model.Marker p0)
	{
		n_onMarkerDragEnd (p0);
	}

	private native void n_onMarkerDragEnd (com.google.android.gms.maps.model.Marker p0);


	public void onMarkerDragStart (com.google.android.gms.maps.model.Marker p0)
	{
		n_onMarkerDragStart (p0);
	}

	private native void n_onMarkerDragStart (com.google.android.gms.maps.model.Marker p0);

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
