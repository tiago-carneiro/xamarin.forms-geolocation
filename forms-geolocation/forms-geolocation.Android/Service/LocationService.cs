using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using forms_geolocation.Droid;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace forms_geolocation.Droid
{
    public class LocationService : Java.Lang.Object, ILocationService, ILocationListener
    {
        Activity Activity => MainActivity.Instance;

        LocationManager _locationManager = null;
        LocationManager LocationManager => _locationManager ?? (_locationManager = (LocationManager)Activity.ApplicationContext.GetSystemService(Context.LocationService));

         TaskCompletionSource<LocationResult> TaskLocation  = new TaskCompletionSource<LocationResult>();

        public void OnLocationChanged(Location location)
        {
            var locationResult = new LocationResult
            {
                Lat = location.Latitude,
                Lon = location.Longitude
            };

            TaskLocation.SetResult(locationResult);
            LocationManager.Dispose();
            _locationManager = null;
            TaskLocation = null;
        }

        public void OnProviderDisabled(string provider)
        {
        }

        public void OnProviderEnabled(string provider)
        {
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
        }

        public bool LocationGranted()
            => Build.VERSION.SdkInt <= BuildVersionCodes.M || Activity.CheckSelfPermission(Manifest.Permission.AccessFineLocation) == (int)Permission.Granted;

        public Task<LocationResult> GetCurrentLocation()
        {
            if (LocationGranted())
            {
                var criteria = new Criteria
                {
                    Accuracy = Accuracy.Coarse,
                    PowerRequirement = Power.High
                };

                LocationManager.RequestSingleUpdate(criteria, this, null);
            }

            return TaskLocation.Task;
        }

        public bool GPSEnabled()
             => LocationManager.IsProviderEnabled(LocationManager.GpsProvider);
    }
}