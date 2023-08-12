using UnityEngine;
using UnityEngine.UI;

public class WebCamModule : MonoBehaviour
{
    [Header("Setting")]
    public Vector2 requestedRatio;
    public int requestedFPS;

    public bool onStartWebCam;

    [Header("Component")]
    [SerializeField] private RawImage _webCamRawImage;
    private RectTransform _webCamRect;

    // Cache
    private WebCamTexture _webCamTexture;

    //__________________________________________________ Awake
    private void Awake()
    {
        initialize();
    }
    private void initialize()
    {
        _webCamRect = _webCamRawImage.GetComponent<RectTransform>();
    }

    //__________________________________________________ Start
    private void Start()
    {
        if (onStartWebCam) SetWebCam(true);
    }
    public void SetWebCam(bool start)
    {
        if(start) createWebCamTexture();
        else destroyWebCamTexture();
    }

    //__________________________________________________ WebCamTexture
    private void createWebCamTexture()
    {
        destroyWebCamTexture();

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

            _webCamTexture = new WebCamTexture(webCamDevices[backCamIndex].name, requestedWidth, requestedHeight, requestedFPS);
            _webCamTexture.filterMode = FilterMode.Trilinear;
            _webCamTexture.Play();

            _webCamRawImage.texture = _webCamTexture;
        }
    }
    private void destroyWebCamTexture()
    {
        if (_webCamTexture)
        {
            Destroy(_webCamTexture);
            _webCamTexture = null;
        }

        _webCamRawImage.texture = null;
    }

    //__________________________________________________ Update
    private void Update()
    {
        updateWebCamImage();
    }
    private void updateWebCamImage()
    {
        if (!_webCamTexture) return;

        int videoRotAngle = _webCamTexture.videoRotationAngle;
        _webCamRect.localEulerAngles = new Vector3(0, 0, -videoRotAngle);

        int width = Definition.screenWidth;
        int height = Definition.screenWidth * _webCamTexture.width / _webCamTexture.height;
        if (Mathf.Abs(videoRotAngle) % 180 != 0f) swap(ref width, ref height);
        _webCamRect.sizeDelta = new Vector2(width, height);
    }

    //__________________________________________________ Util
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