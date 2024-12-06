using UnityEngine;

public class CameraChangeControl : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject subCamera;

    private bool isZoom;
    private Camera main;
    private Camera sub;
    private AudioListener mainLister;
    private AudioListener subLister;

    void Start()
    {
        isZoom = false;
        main=mainCamera.GetComponent<Camera>();
        sub=subCamera.GetComponent<Camera>();
        mainLister=mainCamera.GetComponent<AudioListener>();
        subLister=subCamera.GetComponent<AudioListener>();

        main.depth = 1;
        mainLister.enabled = true;
        sub.depth = -1;
        subLister.enabled = false;

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
        }
        else
        {
            main.depth = 1;
            mainLister.enabled = true;
            sub.depth = -1;
            subLister.enabled = false;
            isZoom = false;
        }
    }

    public bool GetisZoom()
    {
        return isZoom;
    }
}
