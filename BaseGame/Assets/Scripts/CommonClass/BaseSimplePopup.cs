using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public enum PopupType
{
    HelloWorldPopup,
    TestPopup,
    InitTest,
}
public class BasePopup<T> : BasePopupSimple
{
    public virtual void SetupData(T _data = default, List<T> data = null, string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
    }
    public override void SetupData(string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        base.SetupData(message, noCallBack, yesCallBack);
    }
}
public class BasePopupSimple : MonoBehaviour
{
    public PopupType type;

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