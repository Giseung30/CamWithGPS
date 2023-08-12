using UnityEngine;
using UnityEngine.Android;
using System;
using System.Collections.Generic;

public class PermissionModule : MonoBehaviour
{
    [Header("Static")]
    public static PermissionModule instance;

    [Header("Setting")]
    public bool requestOnStart;

    [Header("Permission")]
    public bool requestCamera;
    public bool requestMicrophone;
    public bool requestFineLocation;
    public bool requestCoarseLocation;
    public bool requestExternalStorageRead;
    public bool requestExternalStorageWrite;

    [Header("Action")]
    public Action<string> onPermissionGranted;

    //__________________________________________________ Awake
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    //__________________________________________________ Start
    private void Start()
    {
        if (requestOnStart) RequestPermissions();
    }

    //__________________________________________________ Permission
    public void RequestPermissions()
    {
        List<string> permissions = new();
        if (requestCamera) permissions.Add(Permission.Camera);
        if (requestMicrophone) permissions.Add(Permission.Microphone);
        if (requestFineLocation) permissions.Add(Permission.FineLocation);
        if (requestCoarseLocation) permissions.Add(Permission.CoarseLocation);
        if (requestExternalStorageRead) permissions.Add(Permission.ExternalStorageRead);
        if (requestExternalStorageWrite) permissions.Add(Permission.ExternalStorageWrite);

        PermissionCallbacks callbacks = new();
        callbacks.PermissionGranted += onPermissionGranted;

        Permission.RequestUserPermissions(permissions.ToArray(), callbacks);
    }
}