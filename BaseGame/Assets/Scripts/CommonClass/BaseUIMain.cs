using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using DG.Tweening;
using System.Collections;

public enum ScreenType
{
    UIDailyReward = 0,
}
public enum ShowType
{
    NoAnimation = 0,
    ScaleAnimation = 1,

    Other = 100,
}
public class BaseUIMain : MonoBehaviour
{
    [SerializeField] private Image backGround;
    public ScreenType type;
    public ShowType animationType;
    private Action<Transform> otherCallBack = null;
    private void OnValidate()
    {
        //name = type.ToString();
    }
    public virtual void SetData(Action<Transform> otherCallBack = null)
    {
        this.otherCallBack = otherCallBack;
    }
    public virtual void OnShow(bool firstTime = false)
    {
        if (firstTime) StartCoroutine(ShowForTheFirst(true));
        ShowAnim(true);
    }

    public virtual void OnHide()
    {
        ShowAnim(false);
    }
    IEnumerator ShowForTheFirst(bool isShow)
    {
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        ShowAnim(isShow);
    }
    private void ShowAnim(bool isShow)
    {
        if (animationType == ShowType.NoAnimation) NoAnim(isShow);
        else if (animationType == ShowType.ScaleAnimation) ScaleAnim(isShow);
        else if (animationType == ShowType.Other) otherCallBack?.Invoke(this.transform);
    }
    private void ScaleAnim(bool isShow)
    {
        Tween tween = null;
        if (isShow)
        {
            transform.localScale = Vector3.zero;
            gameObject.SetActive(true);
            tween = transform.DOScale(new Vector3(1, 1, 1), 0.3f);
            tween.OnComplete(delegate
            {

            });
        }
        else
        {
            tween = transform.DOScale(Vector3.zero, 0.3f);
            tween.OnComplete(delegate
            {
                gameObject.SetActive(false);
            });
        }
    }
    private void NoAnim(bool isShow)
    {
        gameObject.SetActive(isShow);
    }
}