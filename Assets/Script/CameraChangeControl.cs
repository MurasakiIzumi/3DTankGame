using UnityEngine;

public class CameraChangeControl : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject subCamera;

    private bool isZoom;

    void Start()
    {
        mainCamera.SetActive(true);
        subCamera.SetActive(false);
        isZoom = false;

        Cursor.visible = false;
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
            mainCamera.SetActive(false);
            subCamera.SetActive(true);
            isZoom = true;
        }
        else
        {
            mainCamera.SetActive(true);
            subCamera.SetActive(false);
            isZoom = false;
        }
    }
}
