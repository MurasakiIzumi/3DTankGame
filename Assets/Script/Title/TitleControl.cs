using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TitleControl : MonoBehaviour
{
    [SerializeField] GameObject titleCamera;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject selectUI;
    [SerializeField] GameObject DataMessenger;
    [SerializeField] float SmoothTime = 0.3f;
    [SerializeField] Vector3[] cameraPos;
    [SerializeField] GameObject[] panzerText; 

    private bool seleteStart;
    private int PanzerIndexMax;
    private int panzerIndex;
    private Vector3 Velocity = Vector3.zero;

    void Start()
    {
        seleteStart = false;
        PanzerIndexMax = cameraPos.Length;
        panzerIndex = 0;
    }

    void FixedUpdate()
    {
        CameraMove();
    }

    private void CameraMove()
    {
        if (seleteStart)
        {
            Vector3 targetPosition = cameraPos[panzerIndex];

            Vector3 smoothPosition = Vector3.SmoothDamp(titleCamera.transform.position,targetPosition, ref Velocity, SmoothTime);

            smoothPosition.y = Mathf.Lerp(titleCamera.transform.position.y, targetPosition.y, Time.deltaTime * SmoothTime * 10);

            titleCamera.transform.position = smoothPosition;

            titleCamera.transform.rotation = Quaternion.Slerp(titleCamera.transform.rotation, Quaternion.Euler(new Vector3(8f, 59f, 0f)), Time.deltaTime);
        }
    }

    private void TextChange()
    {
        for (int i = 0; i < panzerText.Length; i++)
        {
            panzerText[i].SetActive(false);
        }

        panzerText[panzerIndex].SetActive(true);
    }

    public void SetTitleOver()
    {
        seleteStart = true;
        titleUI.SetActive(false);

        StartCoroutine("UIChange", 2);
    }

    public void LeftButton()
    {
        panzerIndex--;
        panzerIndex = Mathf.Max(panzerIndex, 0);
        TextChange();
    }

    public void RightButton()
    {
        panzerIndex++;
        panzerIndex = Mathf.Min(panzerIndex, PanzerIndexMax - 1);
        TextChange();
    }

    public void SelectButton()
    {
        GameObject Data = Instantiate(DataMessenger, transform.position, Quaternion.identity);
        DataMessenger data = Data.GetComponent<DataMessenger>();
        data.SetPanzerIndex(panzerIndex);

        SceneManager.LoadScene("GameScene");
    }

    IEnumerator UIChange(int Sce)
    {
        yield return new WaitForSecondsRealtime(Sce);

        selectUI.SetActive(true);
    }
}
