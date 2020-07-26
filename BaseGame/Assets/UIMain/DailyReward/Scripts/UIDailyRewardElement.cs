using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIDailyRewardElement : MonoBehaviour
{
    [SerializeField]
    private Image backGround;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private Text day;
    [SerializeField]
    private Text value;
    [SerializeField]
    private GameObject locked;
    [SerializeField]
    private GameObject opened;
    [SerializeField]
    private GameObject recieved;

    private Resource _resource;
    public void Init(Resource resource, DailyRewardElementData data)
    {
        _resource = resource;
        //backGround.sprite = LoadResourceController.LoadBackgroundResource(_resource.TYPE, _resource.ID);
        icon.sprite = LoadResourceController.LoadResourceIconWithType(_resource.TYPE, _resource.ID);
        day.text = Localize.LocalizeWithKey("day") + " " + data.day;
        value.text = resource.VALUE.ToString();

        locked.SetActive(!data.opened);
        opened.SetActive(data.opened);
        recieved.SetActive(data.recieved);
    }
    public void Refresh(DailyRewardElementData data)
    {
        locked.SetActive(!data.opened);
        opened.SetActive(data.opened);
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