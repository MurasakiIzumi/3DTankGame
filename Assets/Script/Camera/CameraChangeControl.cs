using UnityEngine;
using UnityEngine.UI;

public class CameraChangeControl : MonoBehaviour
{
    private GameObject mainCamera;
    private GameObject subCamera;
    private GameObject sight;

    private bool isZoom;
    private Camera main;
    private Camera sub;
    private AudioListener mainLister;
    private AudioListener subLister;

    private Image sightImage;

    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera");
        subCamera = GameObject.FindWithTag("SubCamera");
        sight = GameObject.FindWithTag("Sight");

        isZoom = false;
        main=mainCamera.GetComponent<Camera>();
        sub=subCamera.GetComponent<Camera>();
        mainLister=mainCamera.GetComponent<AudioListener>();
        subLister=subCamera.GetComponent<AudioListener>();

        sightImage=sight.GetComponent<Image>();

        main.depth = 1;
        mainLister.enabled = true;
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
            CameraChange();
        }
    }

    private void CameraChange()
    {

        if (!isZoom)
        {
            main.depth = -1;
            mainLister.enabled = false;
            sub.depth = 1;
            subLister.enabled = true;
            isZoom = true;
            sightImage.enabled = true;
        }
        else
        {
            main.depth = 1;
            mainLister.enabled = true;
            sub.depth = -1;
            subLister.enabled = false;
            isZoom = false;
            sightImage.enabled = false;
        }
    }

    public bool GetisZoom()
    {
        return isZoom;
    }
}
