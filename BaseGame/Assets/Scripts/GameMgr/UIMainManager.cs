using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class UIMainManager
{

    //private Transform container;
    readonly Dictionary<string, BaseUIMain> uiMainList = new Dictionary<string, BaseUIMain>();

    public UIMainManager()
    {

    }

    public bool Show(ScreenType type, Action<Transform> otherAnimation = null)
    {
        if (uiMainList.ContainsKey(type.ToString()))
        {
            BaseUIMain popup = uiMainList[type.ToString()];
            if (popup != null)
            {
                popup.transform.SetAsLastSibling();
                popup.SetData(otherAnimation);
                popup.OnShow();
                return true;
            }
            return false;

        }
        bool check = InitPopup(type, otherAnimation);
        return check;
    }
    private bool InitPopup(ScreenType type, Action<Transform> otherAnimation = null)
    {
        // UpdateContainer();
        string path = string.Format(ResourcesConstant.UI_MAIN_PATH, type.ToString());
        BaseUIMain popupPrefab = Resources.Load<BaseUIMain>(path);
        if (popupPrefab == null) return false;
        BaseUIMain popup = GameMgr.Ins.InstantiateHelper(popupPrefab);
        uiMainList.Add(popup.type.ToString(), popup);
        if (popup != null)
        {
            popup.transform.SetAsLastSibling();
            popup.SetData(otherAnimation);
            popup.OnShow(true);
            return true;
        }
        return false;
    }
}
