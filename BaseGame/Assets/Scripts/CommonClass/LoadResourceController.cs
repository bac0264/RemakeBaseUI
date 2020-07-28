using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class LoadResourceController
{
    public static Dictionary<string, Object> resourceCache = new Dictionary<string, Object>();

    public static T LoadFromResource<T>(string path, string fileName) where T : Object
    {
        string fullPath = Path.Combine(path, fileName);
        if (!resourceCache.ContainsKey(fullPath))
        {
            resourceCache.Add(fullPath, Resources.Load<T>(fullPath));
        }

        return resourceCache[fullPath] as T;
    }

    #region Localize Data

    public static LocalizeCollection GetLocalizeCollection()
    {
        return LoadFromResource<LocalizeCollection>(ResourcesConstant.SO_PATH, ResourcesConstant.FN_LOCALIZE_COLLECTION);
    }
    #endregion
    #region DailyReward
    public static Sprite LoadBackgroundResource(int type, int moneyType)
    {
        return LoadFromResource<Sprite>(ResourcesConstant.ICON_PATH, string.Format(ResourcesConstant.FN_ICON_WITH_TYPE, type, moneyType));
    }
    public static DailyRewardDataCollection GetDailyRewardDataCollection()
    {
        return LoadFromResource<DailyRewardDataCollection>(ResourcesConstant.SO_PATH, ResourcesConstant.FN_DAILY_REWARD);
    }
    public static Sprite LoadIconWithMoneyType(int moneyType)
    {
        return LoadFromResource<Sprite>(ResourcesConstant.ICON_PATH, string.Format(ResourcesConstant.FN_ICON_WITH_TYPE, moneyType));
    }
    #endregion
}
