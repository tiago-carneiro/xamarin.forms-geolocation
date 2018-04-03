using System.Threading.Tasks;
using Xamarin.Forms;

namespace forms_geolocation
{
    public class MainViewModel : BaseViewModel
    {
        readonly ILocationService _locationService;
        readonly IPermissionsService _permissionsService;

        string _result = "Buscando geolocalização";
        public string Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }


        public MainViewModel()
        {
            _locationService = DependencyService.Get<ILocationService>();
            _permissionsService = DependencyService.Get<IPermissionsService>();
        }

        public async Task Init()
        {
            var locationAllowed = _permissionsService.IsGranted(CrossPermission.Location);

            if (!locationAllowed)
                locationAllowed = await _permissionsService.RequestPermission(CrossPermission.Location);

            if (!locationAllowed)
            {
                Result = "Geolocalização não autorizada pelo usuário";
                return;
            }

            var gpsEnabled = _locationService.GPSEnabled();

            if (!gpsEnabled)
            {
                Result = "GPS não habilitado";
                return;
            }

            try
            {
                var location = await _locationService.GetCurrentLocation();
                Result = $"Lat: {location.Lat} - Lon: {location.Lon}";
            }
            catch
            {
                Result = "Não foi possível encontrar a localização";
            }
        }
    }
}
