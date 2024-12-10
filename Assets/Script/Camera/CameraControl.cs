using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraControl : MonoBehaviour
{
    [SerializeField] bool isMainCamera;
    [SerializeField] float SmoothTime = 0.3f;

    private GameObject cameraSystem;
    private GameObject player;
    private GameObject main2CameraPoint;
    private CameraChangeControl cameraChange;
    private Vector3 distance;
    private Vector3 Velocity = Vector3.zero;
    private float rotateSpeed;
    private bool isZoom;

    void Start()
    {
        cameraSystem = GameObject.FindWithTag("CameraSystem");

        if (gameObject.tag == "2ndMainCamera")
        {
            main2CameraPoint = GameObject.FindWithTag("2ndMainCameraPoint");
        }

        player = GameObject.FindWithTag("Player");
        rotateSpeed = player.GetComponent<PanzerMoveControl>().GetRotateSpeed();

        if (isMainCamera)
        {
            distance = transform.position - cameraSystem.transform.position;
            isZoom = false;
        }
        else
        {
            distance = transform.position - player.transform.position;
        }

        cameraChange = GameObject.FindWithTag("CameraSystem").GetComponent<CameraChangeControl>();
        isZoom = false;
    }

    void Update()
    {
        isZoom = cameraChange.GetisZoom();

        if (!isMainCamera)
        {
            if (!player)
            {
                player = null;
                StartCoroutine("Restart", 5);
            }
        }
    }

    void FixedUpdate()
    {
        if (!isMainCamera)
        {
            if (!player)
            {
                return;
            }
        }

        if (isMainCamera)
        {
            CameraRotate();

            if (gameObject.tag == "2ndMainCamera")
            {
                if (!main2CameraPoint)
                {
                    return;
                }

                transform.position = main2CameraPoint.transform.position;
            }
        }
        else
        {
            SmoothMove();
        }
    }

    private void SmoothMove()
    {
        Vector3 targetPosition = player.transform.position + distance;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), ref Velocity, SmoothTime);

        smoothPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, Time.deltaTime * SmoothTime * 10);

        transform.position = smoothPosition;
    }

    private void CameraRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");

        if (Mathf.Abs(MouseX) > 0.001f)
        {
            transform.RotateAround(cameraSystem.transform.position, Vector3.up, MouseX);
        }

        if (isZoom)
        {
            MouseX= Input.GetAxis("Horizontal");
            transform.RotateAround(cameraSystem.transform.position, Vector3.up, MouseX * rotateSpeed);
        }
    }

    IEnumerator Restart(int Sce)
    {
        yield return new WaitForSecondsRealtime(Sce);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
