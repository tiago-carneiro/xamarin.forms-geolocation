
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;

namespace forms_geolocation.Droid
{
    [Activity(Label = "forms_geolocation", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static Activity Instance { get; private set; }

        public MainActivity()
        {
            Instance = this;
        }

        public delegate void PermissionsDelegate(bool allowed);
        public event PermissionsDelegate OnPermissionsServiceResult;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (OnPermissionsServiceResult != null)
            {
                var allAllowed = true;
                foreach (var item in grantResults)
                {
                    if (item == Permission.Denied)
                        allAllowed = false;
                }
                OnPermissionsServiceResult(allAllowed);
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

