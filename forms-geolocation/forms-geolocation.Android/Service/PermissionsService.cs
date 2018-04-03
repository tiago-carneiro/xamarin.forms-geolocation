
using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using forms_geolocation.Droid;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(PermissionsService))]

namespace forms_geolocation.Droid
{
    public class PermissionsService : IPermissionsService
    {
        public delegate void PermissionsServiceEventHandler(bool allowed);

        Activity Activity => MainActivity.Instance;

        private TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool>();

        public PermissionsService() { }

        public bool IsGranted(CrossPermission permission)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.M)
                return true;
            return CheckSelfPermission(permission);
        }      

        public Task<bool> RequestPermission(CrossPermission permission)
        {
            RequestPermissions(permission);
            return tcs.Task;
        }

        private void RequestPermissions(CrossPermission permission)
        {
            SetPermissionsServiceResult();
            Activity.RequestPermissions(GetAndroidPermission(permission), (int)permission);
        }

        private bool CheckSelfPermission(CrossPermission permission)
        {
            foreach (var item in GetAndroidPermission(permission))
            {
                var status = Activity.CheckSelfPermission(item);
                if (status == Permission.Denied)
                    return false;
            }
            return true;
        }

        private string[] GetAndroidPermission(CrossPermission permission)
        {
            switch (permission)
            {
                case CrossPermission.Location:
                    return new string[] { Manifest.Permission.AccessFineLocation };
                default:
                    return new string[] { string.Empty };
            }
        }

        private void SetPermissionsServiceResult()
        {
            if (Activity is MainActivity)
            {
                ((MainActivity)Activity).OnPermissionsServiceResult -= OnPermissionsServiceResult;
                ((MainActivity)Activity).OnPermissionsServiceResult += OnPermissionsServiceResult;
            }
        }

        private void OnPermissionsServiceResult(bool allowed)
        {
            tcs.SetResult(allowed);
        }
    }
}