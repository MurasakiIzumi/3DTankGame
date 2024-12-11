using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] Transform startPoint;
    [SerializeField] GameObject[] Panzer;

    private GameObject Data;
    private int panzerIndex;
    private int enemyNum;

    void Awake()
    {
        GetData();
        CreatePanzer();
        enemyNum = 0;
    }
    private void GetData()
    {
        Data = GameObject.FindWithTag("Data");
        DataMessenger data = Data.GetComponent<DataMessenger>();

        panzerIndex = data.GetPanzerIndex();

        data.DestroySelf();
    }

    private void CreatePanzer()
    {
        Instantiate(Panzer[panzerIndex], startPoint.position, startPoint.rotation);
    }

    public void AddEnemy()
    {
        enemyNum++;
    }

    public void EnemyDestroy()
    {
        enemyNum--;

        if (enemyNum == 0)
        {
            StartCoroutine("GameClear", 5);
        }
    }

    IEnumerator GameClear(int Sce)
    {
        yield return new WaitForSecondsRealtime(Sce);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("TitleScene");
    }
}
