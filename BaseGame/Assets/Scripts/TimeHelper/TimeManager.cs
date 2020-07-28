using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public static TimeManager Ins = null;
    public DateTime currentTime;
    public bool check;
    //make sure there is only one instance of this always.
    void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
        }
        else if (Ins != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        UpdateTime();
    }
    public DateTime GetNetTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
                                   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                   CultureInfo.InvariantCulture.DateTimeFormat,
                                   DateTimeStyles.AssumeUniversal);
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveTime();
        }
    }
    private void OnApplicationQuit()
    {
        SaveTime();
    }
    public void SaveTime()
    {
        StartCoroutine(getTime(callBack2: SaveTime));
    }
    public void UpdateCurrentTime(Action<bool, long> callBack = null, Action<bool> callBack2 = null)
    {
        StartCoroutine(getTime(callBack, callBack2));
    }
    private void SaveTime(bool check)
    {
        if (check)
            PlayerPrefs.SetString(KeySave.TIME_QUIT_GAME, PlayerPrefs.GetString(KeySave.TIME_QUIT_GAME));
        PlayerPrefs.SetString(KeySave.TIME_QUIT_GAME, currentTime.TotalSecondTimeStamp().ToString());
    }
    public IEnumerator getTime(Action<bool, long> callBack = null, Action<bool> callBack2 = null)
    {
        UnityWebRequest www = UnityWebRequest.Get("https://www.microsoft.com");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Log.Info("TIME" + www.error);
            check = false;
        }
        else
        {
            if (www.isHttpError)
            {
                www = UnityWebRequest.Get("https://www.google.com");
                yield return www.SendWebRequest();
                if (www.isHttpError)
                {
                    Log.Info(www.error);
                    check = false;
                }
                else
                {
                    string date = www.GetResponseHeader("date");
                    yield return new WaitForSecondsRealtime(0.1f);
                    currentTime = DateTime.ParseExact(date,
                                               "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                               CultureInfo.InvariantCulture.DateTimeFormat,
                                               DateTimeStyles.AssumeUniversal);
                    check = true;
                }
            }
            else
            {
                string date = www.GetResponseHeader("date");
                yield return new WaitForSecondsRealtime(0.1f);
                currentTime = DateTime.ParseExact(date,
                                           "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                           CultureInfo.InvariantCulture.DateTimeFormat,
                                           DateTimeStyles.AssumeUniversal);
                check = true;
            }
        }

        callBack?.Invoke(check, currentTime.TotalSecondTimeStamp());
        callBack2?.Invoke(check);
    }
    public void UpdateTime()
    {
        StartCoroutine(getTime());
    }
}


public static class TimeHelper
{
    public static bool IsNextDay(long lastTimeOnline, long currentTime)
    {
        DateTime lastTime = lastTimeOnline.NextMidNight();
        long lastTimeSecond = lastTime.TotalSecondTimeStamp();
        long rangeTime = currentTime - lastTimeSecond;
        if (rangeTime < 0) return false;
        return true;
    }
    public static IEnumerator TimeCountDown(Text timeTxt, long currentTime, Action callBack)
    {
        long deadTime = currentTime.NextMidNightTimeStamp();
        long temp = deadTime - currentTime;
        while (temp > 0)
        {
            yield return new WaitForSeconds(1);
            temp--;
            timeTxt.text = ToTimeSpanString(temp);
        }
        callBack?.Invoke();
    }
    public static long TotalSecondTimeStamp(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }

    public static DateTime ToDate(this long secondTimeStamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(secondTimeStamp);
    }

    public static DateTime NextMidNight(this long secondTimeStamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(secondTimeStamp).Date.AddDays(1).AddSeconds(-1);
    }

    public static long NextMidNightTimeStamp(this long secondTimeStamp)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        return origin.AddSeconds(secondTimeStamp).Date.AddDays(1).AddSeconds(-1).TotalSecondTimeStamp();
    }

    public static string ToTimeSpanString(this TimeSpan timeSpan)
    {
        return timeSpan.ToString(@"hh\:mm\:ss");
    }

    public static string ToTimeSpanString(this long timeSpan)
    {
        var timeSpanConverted = TimeSpan.FromSeconds(timeSpan);
        if (timeSpanConverted.Days > 0)
        {
            return string.Format("{0} {1} {2}", timeSpanConverted.Days,"day",
                timeSpanConverted.ToString(@"hh\:mm\:ss"));
        }
        else
        {
            return timeSpanConverted.ToString(@"hh\:mm\:ss");
        }
    }


    public static string ToTimeSpanStringFull(this long timeSpan)
    {
        var timeSpanConverted = TimeSpan.FromSeconds(timeSpan);
        return timeSpanConverted.ToString(@"d\d\:hh\h\:mm\m\:ss\s");
    }

    public static long TotalDays(this DateTime dateTime)
    {
        return (long)(dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalDays;
    }


    public static (int, int) CalculateTimeBySecond(int totalSecond)
    {
        var timeCalculated = (minute: totalSecond / 60, second: totalSecond % 60);
        return timeCalculated;
    }

    public static string WithColorTag(this string origin, string colorHex)
    {
        return $"<color=#{colorHex}>{origin}</color>";
    }
}