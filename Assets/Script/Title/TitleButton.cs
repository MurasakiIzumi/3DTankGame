using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] TitleControl titleControl;

    public void StartCut()
    {
        titleControl.SetTitleOver();

    }
    public void GoLeft()
    {
        titleControl.LeftButton();
    }

    public void GoRight()
    {
        titleControl.RightButton();
    }

    public void Select()
    {
        titleControl.SelectButton();
    }
}
