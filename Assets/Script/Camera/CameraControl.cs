using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    [SerializeField] bool isMainCamera;
    [SerializeField] float SmoothTime = 0.3f;

    private GameObject Target;
    private CameraChangeControl cameraChange;
    private Vector3 distance;
    private Vector3 Velocity = Vector3.zero;
    private float rotateSpeed;
    private bool isZoom;

    void Start()
    {
        if (isMainCamera)
        {
            Target = GameObject.FindWithTag("CameraSystem");
        }
        else
        {
            Target = GameObject.FindWithTag("Player");
            rotateSpeed = Target.GetComponent<PanzerMoveControl>().GetRotateSpeed();
        }

        cameraChange=GameObject.FindWithTag("CameraSystem").GetComponent<CameraChangeControl>();

        distance = transform.position - Target.transform.position;
        isZoom = false;
    }

    void Update()
    {
        isZoom = cameraChange.GetisZoom();

        if (!Target)
        {
            Target = null;
            StartCoroutine("Restart", 5);
        }
    }

    void FixedUpdate()
    {
        if (!Target)
        {
            return;
        }

        if (isMainCamera)
        {
            CameraRotate();
        }
        else
        {
            SmoothMove();
        }
    }

    private void SmoothMove()
    {
        Vector3 targetPosition = Target.transform.position + distance;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z), ref Velocity, SmoothTime);

        smoothPosition.y = Mathf.Lerp(transform.position.y, targetPosition.y, Time.deltaTime * SmoothTime * 10);

        transform.position = smoothPosition;
    }

    private void CameraRotate()
    {
        float MouseX = Input.GetAxis("Mouse X");

        if (Mathf.Abs(MouseX) > 0.001f)
        {
            transform.RotateAround(Target.transform.position, Vector3.up, MouseX);
        }

        if (isZoom)
        {
            MouseX= Input.GetAxis("Horizontal");

            if (Mathf.Abs(MouseX) > 0.001f)
            {
                transform.RotateAround(Target.transform.position, Vector3.up, MouseX * rotateSpeed);
            }
        }
    }

    IEnumerator Restart(int Sce)
    {
        yield return new WaitForSecondsRealtime(Sce);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
