using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMainManager
{

    //private Transform container;
    readonly Dictionary<string, BaseUIMain> uiMainList = new Dictionary<string, BaseUIMain>();

    public UIMainManager()
    {

    }

    public bool Show(ScreenType type)
    {
        if (uiMainList.ContainsKey(type.ToString()))
        {
            BaseUIMain popup = uiMainList[type.ToString()];
            if (popup != null)
            {
                popup.transform.SetAsLastSibling();
                popup.OnShow();
                return true;
            }
            return false;

        }
        bool check = InitPopup(type);
        return check;
    }
    private bool InitPopup(ScreenType type)
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
            popup.OnShow();
            return true;
        }
        return false;
    }
}
