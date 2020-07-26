using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class TimeView : MonoBehaviour
{
    [SerializeField] private Text timeTxt = null;
    private Action callBack;
    
    private void OnTime(bool check, long current)
    {
        if (check)
        {
            long temp = current.NextMidNightTimeStamp() - current;
            timeTxt.text = temp.ToTimeSpanString();
            timeTxt.gameObject.SetActive(true);
            StartCoroutine(TimeHelper.TimeCountDown(timeTxt, current, callBack));
        }
        else timeTxt.gameObject.SetActive(false);
    }
    
    public void StartTime(Action callBack)
    {
        this.callBack = callBack;
        timeTxt.text = "";
        StartCoroutine(GetTime());
    }
    private IEnumerator GetTime()
    {
        yield return StartCoroutine(TimeManager.Ins.getTime(OnTime));
    }
}
