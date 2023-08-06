using UnityEngine;
using UnityEngine.Android;

public class GPSController : MonoBehaviour
{
    [Header("Setting")]
    public bool onStartGPS;

    // Cache
    private LocationService _locationService;

    /** Awake **/
    private void Awake()
    {
        initialize();
    }
    private void initialize()
    {
        _locationService = Input.location;
    }

    /** Start */
    private void Start()
    {
        if(onStartGPS) StartGPS();
    }

    /** GPS **/
    public void StartGPS(string permissionName = null)
    {
        if (Permission.HasUserAuthorizedPermission(Permission.FineLocation))
        {
            setLocationService(true);
        }
        else
        {
            PermissionCallbacks permissionCallbacks = new();
            permissionCallbacks.PermissionGranted += StartGPS;
            Permission.RequestUserPermission(Permission.FineLocation, permissionCallbacks);
        }
    }
    public void StopGPS()
    {
        setLocationService(false);
    }

    private void setLocationService(bool start)
    {
        if (start) _locationService.Start();
        else _locationService.Stop();
    }

    /** Util **/
    public bool GetGPSLocation(out LocationServiceStatus status, out float latitude, out float longitude)
    {
        latitude = longitude = default;
        status = _locationService.status;

        if (!_locationService.isEnabledByUser) return false;

        switch (status)
        {
            case LocationServiceStatus.Stopped:
            case LocationServiceStatus.Failed:
            case LocationServiceStatus.Initializing:
                return false;
            default:
                LocationInfo lInfo = _locationService.lastData;
                latitude = lInfo.latitude;
                longitude = lInfo.longitude;
                return true;
        }
    }
}