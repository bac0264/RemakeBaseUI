using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailInfoPopup : BasePopup<Resource>
{
    [SerializeField] private Text currencyTxt;
    [SerializeField] private Text decribeTxt;
    [SerializeField] private Image icon;

    [SerializeField] Button backBtn;
    [SerializeField] Button closeBtn;

    private Action noCallBack;
    private Action yesCallBack;

    private void Awake()
    {
        backBtn.onClick.AddListener(delegate { noCallBack?.Invoke(); });
        closeBtn.onClick.AddListener(delegate { noCallBack?.Invoke(); });
    }
    public override void SetupData(Resource _data = null, List<Resource> data = null, string message = null, Action noCallBack = null, Action yesCallBack = null)
    {
        currencyTxt.text = Localize.LocalizeWithKey("currency");
        decribeTxt.text = Localize.LocalizeWithKey("decribe_" + _data.TYPE + "_" + _data.ID);
        icon.sprite = LoadResourceController.LoadIconWithMoneyType(_data.ID);
        this.noCallBack = noCallBack;
        this.yesCallBack = yesCallBack;
    }
}
