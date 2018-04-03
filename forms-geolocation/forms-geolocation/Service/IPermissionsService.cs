using System.Threading.Tasks;

namespace forms_geolocation
{
    public interface IPermissionsService
    {
        bool IsGranted(CrossPermission permission);
        Task<bool> RequestPermission(CrossPermission permission);
    }
}
