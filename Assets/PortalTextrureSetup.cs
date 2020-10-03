using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextrureSetup : MonoBehaviour
{
    public Camera cameraPortal1;
    public Camera cameraPortal2;

    public Material camera1Mat;
    public Material camera2Mat;

    // Start is called before the first frame update
    void Start()
    {
        if(cameraPortal1.targetTexture != null)
        {
            cameraPortal1.targetTexture.Release();
        }
        if (cameraPortal2.targetTexture != null)
        {
            cameraPortal2.targetTexture.Release();
        }
        cameraPortal1.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraPortal2.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);

        camera1Mat.mainTexture = cameraPortal2.targetTexture;
        camera2Mat.mainTexture = cameraPortal1.targetTexture;
    }

}
