using UnityEngine;

public class GPSManager : MonoBehaviour
{
    [Header("Setting")]
    public bool onStartGPS;

    // Cache
    private LocationService _locationService;

    //__________________________________________________ Awake
    private void Awake()
    {
        initialize();
    }
    private void initialize()
    {
        _locationService = Input.location;
    }

    //__________________________________________________ Start
    private void Start()
    {
        if(onStartGPS) StartGPS();
    }

    //__________________________________________________ GPS
    public void StartGPS()
    {
        setLocationService(true);
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

    //__________________________________________________ Util
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