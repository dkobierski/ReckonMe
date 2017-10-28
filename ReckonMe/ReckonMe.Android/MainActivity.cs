using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace ReckonMe.Droid
{
    [Activity(Label = "@string/app_name", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            AndroidEnvironment.UnhandledExceptionRaiser += HandleAndroidExceptions;

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }

        private static void HandleAndroidExceptions(object sender, RaiseThrowableEventArgs e)
        {
            e.Handled = true;
        }
    }
}