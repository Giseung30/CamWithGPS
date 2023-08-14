using UnityEngine;

public class GPSModule : MonoBehaviour
{
    [Header("Static")]
    public static GPSModule instance;

    [Header("Setting")]
    public bool setOnStart;
    public float desiredAccuracyInMeters;
    public float updateDistanceInMeters;

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
        if (setOnStart) SetLocationService(true);
    }

    //__________________________________________________ GPS
    public void SetLocationService(bool start)
    {
        if (start) _locationService.Start(desiredAccuracyInMeters, updateDistanceInMeters);
        else _locationService.Stop();
    }
    public bool GetGPSLocation(out LocationServiceStatus status, out LocationInfo locationInfo)
    {
        locationInfo = default;
        status = _locationService.status;

        if (!_locationService.isEnabledByUser) return false;

        switch (status)
        {
            case LocationServiceStatus.Stopped:
            case LocationServiceStatus.Failed:
            case LocationServiceStatus.Initializing:
                return false;
            default:
                locationInfo = _locationService.lastData;
                return true;
        }
    }
}