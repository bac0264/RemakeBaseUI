using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDailyRewardElement : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private Image icon;

    [SerializeField] private Text day;
    [SerializeField] private Text value;
    [SerializeField] private Text nameTxt;

    [SerializeField] private GameObject locked;
    [SerializeField] private GameObject opened;
    [SerializeField] private GameObject recieved;

    public Resource Resource { set; get; }
    public void Init(Resource resource, DailyRewardElementData data)
    {
        Resource = resource;
        //backGround.sprite = LoadResourceController.LoadBackgroundResource(_resource.TYPE, _resource.ID);
        icon.sprite = LoadResourceController.LoadIconWithMoneyType(Resource.ID);
        day.text = data.day.ToString();
        value.text = resource.VALUE.ToString();
        nameTxt.text = Localize.LocalizeWithKey("resource_" + Resource.ID);

        SetElementState(data);
    }
    public void SetElementState(DailyRewardElementData data)
    {
        locked.SetActive(!data.opened);
        opened.SetActive(data.opened && !data.recieved);
        recieved.SetActive(data.recieved);
    }
}

[System.Serializable]
public class DailyRewardElementData
{
    public int day;
    public bool opened;
    public bool recieved;

    public DailyRewardElementData()
    {
        opened = false;
        recieved = false;
    }
    public DailyRewardElementData(int day, bool opened, bool recieved)
    {
        this.day = day;
        this.opened = opened;
        this.recieved = recieved;
    }
}