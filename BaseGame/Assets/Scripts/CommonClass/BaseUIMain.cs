using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum ScreenType
{
    UIDailyReward = 0,
}
public class BaseUIMain : MonoBehaviour
{
    public ScreenType type;

    private void OnValidate()
    {
        //name = type.ToString();
    }
    public virtual void SetupData(string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
    }
    public virtual void OnShow()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnHide()
    {
        gameObject.SetActive(false);
    }
}