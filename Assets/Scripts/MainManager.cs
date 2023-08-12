using UnityEngine;

public class MainManager : MonoBehaviour
{
    private void Start()
    {
        PermissionModule permission = PermissionModule.instance;
        permission.onPermissionGranted += onPermissionGranted;
        permission.RequestPermissions();
    }

    private void onPermissionGranted(string permissionName)
    {
        WebCamModule webCam = WebCamModule.instance;
        GPSModule gps = GPSModule.instance;

        webCam.SetWebCam(true);
        gps.SetLocationService(true);
    }
}