using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{

    public static CameraHandler Instance;

    public Texture2D PhotoTaken { get; private set; }

    private WebCamTexture mWebCamTexture;

    public CameraHandler(Texture2D photoTaken)
    {
        PhotoTaken = photoTaken;
    }

    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }

        mWebCamTexture = new WebCamTexture(Application.isMobilePlatform ? WebCamTexture.devices[1].name : WebCamTexture.devices[0].name);
  
    }

    public void LaunchCamera()
    {
        mWebCamTexture.Play();
    }

    public void StopCamera()
    {
        mWebCamTexture.Stop();
    }

    public void TakePicture()
    {
        if (mWebCamTexture.isPlaying)
        {
            PhotoTaken = new Texture2D(mWebCamTexture.width, mWebCamTexture.height);
            PhotoTaken.SetPixels(mWebCamTexture.GetPixels());
            PhotoTaken.Apply();
        }
    }
}
