using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;

public class WebCamController : MonoBehaviour
{
    [Header("Setting")]
    public Vector2 requestedRatio;
    public int requestedFPS;

    [Header("Module")]
    [SerializeField] private RawImage webCamRawImage;
    private RectTransform webCamRect;

    // Cache
    private WebCamTexture webCamTexture;

    /** Awake **/
    private void Awake()
    {
        initialize();
    }
    private void initialize()
    {
        webCamRect = webCamRawImage.GetComponent<RectTransform>();
    }

    /** Start **/
    private void Start()
    {
        createWebCam();
    }
    private void createWebCam()
    {
        if (Permission.HasUserAuthorizedPermission(Permission.Camera))
            setWebCamImage();
        else
        {
            PermissionCallbacks permissionCallbacks = new();
            permissionCallbacks.PermissionGranted += setWebCamImage;
            Permission.RequestUserPermission(Permission.Camera, permissionCallbacks);
        }
    }

    private void setWebCamImage(string permissionName = null)
    {
        setWebCamImage(requestedRatio, requestedFPS);
    }
    private void setWebCamImage(Vector2 requestedRatio, int requestedFPS)
    {
        if (webCamTexture)
        {
            Destroy(webCamTexture);
            webCamTexture = null;
        }

        WebCamDevice[] webCamDevices = WebCamTexture.devices;
        if (webCamDevices.Length == 0) return;

        int backCamIndex = -1;
        for (int i = 0, l = webCamDevices.Length; i < l; ++i)
        {
            if (!webCamDevices[i].isFrontFacing)
            {
                backCamIndex = i;
                break;
            }
        }

        if (backCamIndex != -1)
        {
            int requestedWidth = Definition.screenWidth;
            int requestedHeight = Definition.screenHeight;
            for (int i = 0, l = webCamDevices[backCamIndex].availableResolutions.Length; i < l; ++i)
            {
                Resolution resolution = webCamDevices[backCamIndex].availableResolutions[i];
                if (getAspectRatio((int)requestedRatio.x, (int)requestedRatio.y).Equals(getAspectRatio(resolution.width, resolution.height)))
                {
                    requestedWidth = resolution.width;
                    requestedHeight = resolution.height;
                    break;
                }
            }

            webCamTexture = new WebCamTexture(webCamDevices[backCamIndex].name, requestedWidth, requestedHeight, requestedFPS);
            webCamTexture.filterMode = FilterMode.Trilinear;
            webCamTexture.Play();

            webCamRawImage.texture = webCamTexture;
        }
    }

    /** Update **/
    private void Update()
    {
        updateWebCamImage();
    }
    private void updateWebCamImage()
    {
        if (!webCamTexture) return;

        int videoRotAngle = webCamTexture.videoRotationAngle;
        webCamRect.localEulerAngles = new Vector3(0, 0, -videoRotAngle);

        int width = Definition.screenWidth;
        int height = Definition.screenWidth * webCamTexture.width / webCamTexture.height;
        if (Mathf.Abs(videoRotAngle) % 180 != 0f) swap(ref width, ref height);
        webCamRect.sizeDelta = new Vector2(width, height);
    }

    /** Util **/
    private void swap<T>(ref T a, ref T b)
    {
        T tmp = a;
        a = b;
        b = tmp;
    }
    private string getAspectRatio(int width, int height, bool allowPortrait = false)
    {
        if (!allowPortrait && width < height) swap(ref width, ref height);
        float r = (float)width / height;

        return r.ToString("F2");
    }
}