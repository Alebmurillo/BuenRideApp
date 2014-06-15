package buenride.and;


public class SearchRideMenu_TabByUserFragment_ListUserFragment
	extends android.app.ListFragment
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onActivityCreated:(Landroid/os/Bundle;)V:GetOnActivityCreated_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("BuenRide.And.SearchRideMenu/TabByUserFragment/ListUserFragment, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SearchRideMenu_TabByUserFragment_ListUserFragment.class, __md_methods);
	}


	public SearchRideMenu_TabByUserFragment_ListUserFragment () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SearchRideMenu_TabByUserFragment_ListUserFragment.class)
			mono.android.TypeManager.Activate ("BuenRide.And.SearchRideMenu/TabByUserFragment/ListUserFragment, BuenRide, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onActivityCreated (android.os.Bundle p0)
	{
		n_onActivityCreated (p0);
	}

	private native void n_onActivityCreated (android.os.Bundle p0);

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
