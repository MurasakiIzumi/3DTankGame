using UnityEngine;
using UnityEngine.UI;

public class CameraChangeControl : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject mainCamera2;
    private GameObject subCamera;
    private GameObject sight;

    private bool isZoom;
    private int cameraPos;
    private Camera main;
    private Camera main2;
    private Camera sub;
    private AudioListener mainLister;
    private AudioListener main2Lister;
    private AudioListener subLister;

    private Image sightImage;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        mainCamera2 = GameObject.FindWithTag("2ndMainCamera");
        subCamera = GameObject.FindWithTag("SubCamera");
        sight = GameObject.FindWithTag("Sight");

        isZoom = false;
        cameraPos = 1;
        main =mainCamera.GetComponent<Camera>();
        main2 = mainCamera2.GetComponent<Camera>();
        sub =subCamera.GetComponent<Camera>();
        mainLister=mainCamera.GetComponent<AudioListener>();
        main2Lister=mainCamera2.GetComponent<AudioListener>();
        subLister =subCamera.GetComponent<AudioListener>();

        sightImage=sight.GetComponent<Image>();

        main.depth = 1;
        mainLister.enabled = true;
        main2.depth = -1;
        main2Lister.enabled = false;
        sub.depth = -1;
        subLister.enabled = false;
        sightImage.enabled = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            PosChange();
            CameraChange();
        }
    }

    private void PosChange()
    {
        cameraPos++;

        if(cameraPos>3)
        {
            cameraPos = 1;
        }
    }

    private void CameraChange()
    {
        switch (cameraPos)
        {
            case 1:
                main.depth = 1;
                mainLister.enabled = true;
                main2.depth = -1;
                main2Lister.enabled = false;
                sub.depth = -1;
                subLister.enabled = false;
                sightImage.enabled = false;
                isZoom = false;
                break;
            case 2:
                main.depth = -1;
                mainLister.enabled = false;
                main2.depth = 1;
                main2Lister.enabled = true;
                sub.depth = -1;
                subLister.enabled = false;
                sightImage.enabled = false;
                isZoom = false;
                break;
            case 3:
                main.depth = -1;
                mainLister.enabled = false;
                main2.depth = -1;
                main2Lister.enabled = false;
                sub.depth = 1;
                subLister.enabled = true;
                sightImage.enabled = true;
                isZoom = true;
                break;
        }
    }

    public bool GetisZoom()
    {
        return isZoom;
    }
}
