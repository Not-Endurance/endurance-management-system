using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics.Android;
using Microsoft.AppCenter.Crashes.Android;

namespace Endurance.Gateways.Witness;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
	private const string EMS_WITNESS_ANDROID = "63fa46a6-4808-4d0a-8e7e-c645b0252bd2";

	protected override void OnCreate(Bundle? savedInstanceState)
	{
		var secret = EMS_WITNESS_ANDROID;
		AppCenter.Start(secret, typeof(Analytics), typeof(Crashes));
		base.OnCreate(savedInstanceState);
	}
}
