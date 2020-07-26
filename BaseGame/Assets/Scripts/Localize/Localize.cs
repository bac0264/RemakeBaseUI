using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Localize 
{
    private static LocalizeCollection localizeCollection = null;
    private static Dictionary<string, string> dic = null;
    public static string LocalizeWithKey(string key)
    {
        if (localizeCollection == null) localizeCollection = LoadResourceController.GetLocalizeCollection();
        if (dic == null) dic = localizeCollection.GetData();
        return GetKey(key);
    }
    private static string GetKey(string key)
    {
        if (dic.ContainsKey(key))
            return dic[key];
        return "#" + key;
    }
}
