using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMessenger: MonoBehaviour
{
    private int PanzerIndex;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void SetPanzerIndex(int index)
    {
        PanzerIndex = index;
    }

    public int GetPanzerIndex()
    {
        return PanzerIndex;
    }
}
