using UnityEngine;
using System.Collections;

public class Log 
{
    public static void Info(string content)
    {
#if UNITY_EDITOR
        Debug.Log(content);
#endif
    }
    public static void Error(string content)
    {
#if UNITY_EDITOR
        Debug.LogError(content);
#endif
    }
}
