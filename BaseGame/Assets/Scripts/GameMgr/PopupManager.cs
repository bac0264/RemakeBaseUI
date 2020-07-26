using UnityEngine;
using System.Collections.Generic;
using System;

public class PopupManager
{
    //private Transform container;
    readonly Dictionary<string, BasePopupSimple> PopupList = new Dictionary<string, BasePopupSimple>();

    public PopupManager()
    {

    }

    #region Show Notification
    public bool ShowPopupWithNoData(PopupType type, string _message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        if (PopupList.ContainsKey(type.ToString()))
        {
            BasePopupSimple popup = PopupList[type.ToString()];
            if (popup != null)
            {
                popup.SetupData(_message, noCallBack, yesCallBack);
                popup.transform.SetAsLastSibling();
                popup.OnShow();    
                return true;
            }
            return false;

        }
        bool check = InitPopup(type, _message, noCallBack, yesCallBack);
        return check;
    }
    private bool InitPopup(PopupType type, string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        // UpdateContainer();
        BasePopupSimple popupPrefab = Resources.Load<BasePopupSimple>(string.Format(ResourcesConstant.POPUP_PATH, type.ToString()));
        if (popupPrefab == null) return false;
        BasePopupSimple popup = GameMgr.Ins.InstantiateHelper(popupPrefab);
        PopupList.Add(popup.type.ToString(), popup);
        if (popup != null)
        {
            popup.SetupData(message, noCallBack, yesCallBack);
            popup.transform.SetAsLastSibling();
            popup.OnShow();
            return true;
        }
        return false;
    }
    #endregion


    #region Show popup with data
    public bool ShowPopupWithData<T>(PopupType type, T slot = default, List<T> slots = null, string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        if (PopupList.ContainsKey(type.ToString()))
        {
            BasePopupSimple popup = PopupList[type.ToString()];
            if (popup != null)
            {
                    BasePopup<T> _popup = popup as BasePopup<T>;
                    _popup.SetupData(slot, slots, message, noCallBack, yesCallBack);
                    _popup.transform.SetAsLastSibling();
                    _popup.OnShow();
                return true;
            }
            return false;

        }
        bool check = InitPopup(type, slot, slots, message, noCallBack, yesCallBack);
        return check;
    }
    private bool InitPopup<T>(PopupType type, T slot,List<T> slots = null, string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        // UpdateContainer();
        BasePopupSimple popupPrefab = Resources.Load<BasePopupSimple>(string.Format(ResourcesConstant.POPUP_PATH, type.ToString()));
        if (popupPrefab == null) return false;
        BasePopupSimple popup = GameMgr.Ins.InstantiateHelper(popupPrefab);
        PopupList.Add(popup.type.ToString(), popup);
        if (popup != null)
        {
                BasePopup<T> _popup = popup as BasePopup<T>;
                _popup.SetupData(slot, slots, message, noCallBack, yesCallBack);
                _popup.transform.SetAsLastSibling();
                _popup.OnShow();
            return true;
        }
        return false;
    }
    #endregion

    public void HideAllPopup()
    {
        foreach(var popup in PopupList.Values)
        {
            popup.OnHide();
        }
    }

}