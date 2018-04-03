using System.Threading.Tasks;

namespace forms_geolocation
{
    public interface ILocationService
    {
        bool LocationGranted();
        bool GPSEnabled();
        Task<LocationResult> GetCurrentLocation();
    }
}
