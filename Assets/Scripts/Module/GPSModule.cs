using UnityEngine;

public class GPSModule : MonoBehaviour
{
    [Header("Static")]
    public static GPSModule instance;

    [Header("Setting")]
    public bool setOnStart;

    // Cache
    private LocationService _locationService;

    //__________________________________________________ Awake
    private void Awake()
    {
        if (instance == null)
            instance = this;

        initialize();
    }
    private void initialize()
    {
        _locationService = Input.location;
    }

    //__________________________________________________ Start
    private void Start()
    {
        if(setOnStart) SetLocationService(true);
    }

    //__________________________________________________ GPS
    public void SetLocationService(bool start)
    {
        if (start) _locationService.Start();
        else _locationService.Stop();
    }
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