using Foundation;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;

namespace EMS.Witness;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	private const string EMS_WITNESS_IOS = "8a64ff1f-469b-43c0-a4fd-dcfe6c4d8b20";

	protected override MauiApp CreateMauiApp()
	{
		var secret = EMS_WITNESS_IOS;
		AppCenter.Start(secret, typeof(Analytics), typeof(Crashes));

		return MauiProgram.CreateMauiApp();
	} 
}