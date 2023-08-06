using UnityEngine;
using UnityEngine.Android;
using System.Collections.Generic;

public class PermissionManager : MonoBehaviour
{
    [Header("Permission")]
    public bool requestCamera;
    public bool requestMicrophone;
    public bool requestFineLocation;
    public bool requestCoarseLocation;
    public bool requestExternalStorageRead;
    public bool requestExternalStorageWrite;

    private void Awake()
    {
        requestPermissions();
    }
    private void requestPermissions()
    {
        List<string> permissions = new();
        if (requestCamera) permissions.Add(Permission.Camera);
        if (requestMicrophone) permissions.Add(Permission.Microphone);
        if (requestFineLocation) permissions.Add(Permission.FineLocation);
        if (requestCoarseLocation) permissions.Add(Permission.CoarseLocation);
        if (requestExternalStorageRead) permissions.Add(Permission.ExternalStorageRead);
        if (requestExternalStorageWrite) permissions.Add(Permission.ExternalStorageWrite);

        Permission.RequestUserPermissions(permissions.ToArray());
    }
}