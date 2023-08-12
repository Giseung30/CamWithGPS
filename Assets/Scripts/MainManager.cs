using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class MainManager : MonoBehaviour
{
    [Header("Component")]
    public Text gpsText;

    private void Start()
    {
        PermissionModule permission = PermissionModule.instance;
        permission.onPermissionGranted += onPermissionGranted;
        permission.RequestPermissions();
    }

    private void onPermissionGranted(string permissionName)
    {
        switch (permissionName)
        {
            case Permission.Camera:
                WebCamModule.instance.SetWebCam(true);
                break;
            case Permission.FineLocation:
                GPSModule.instance.SetLocationService(true);
                break;

        }
    }

    private void Update()
    {
        GPSModule gps = GPSModule.instance;
        if (gps.GetGPSLocation(out LocationServiceStatus status, out float latitude, out float longitude))
            gpsText.text = $"status : {status}, latitude : {latitude}, longitude : {longitude}";
        else
            gpsText.text = $"Available GPS";
    }
}