using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMoney
{
    private DataSave<Resource> resourceList = new DataSave<Resource>();
    private Dictionary<MoneyType, Resource> resourceDic = new Dictionary<MoneyType, Resource>();
    public PlayerMoney()
    {
        Load();
    }

    public bool Add(MoneyType id, long value)
    {
        if (resourceDic.ContainsKey(id))
        {
            bool check = resourceDic[id].Add(value);
            if (check) Save();
            return check;
        }
        else
        {
            Resource resource = new Resource((int)ResourceType.MONEY, (int)id, value);
            resourceDic.Add(id, resource);
            resourceList.AddData(resource);
            Save();
            return true;
        }
    }
    public bool Sub(MoneyType id, long value)
    {
        if (resourceDic.ContainsKey(id))
        {
            bool check = resourceDic[id].Sub(value);
            if (check) Save();
            return check;
        }
        else
        {
            Resource resource = new Resource((int)ResourceType.MONEY, (int)id, 0);
            resourceDic.Add(id, resource);
            resourceList.AddData(resource);
            Save();
            return true;
        }
    }
    public Resource Get(MoneyType id, long value)
    {
        if (resourceDic.ContainsKey(id))
        {
            return resourceDic[id];
        }
        else
        {
            Resource resource = new Resource((int)ResourceType.MONEY, (int)id, value);
            resourceDic.Add(id, resource);
            resourceList.AddData(resource);
            Save();
            return resource;
        }
    }

    public void Load()
    {
        resourceList = JsonUtility.FromJson<DataSave<Resource>>(PlayerPrefs.GetString(KeySave.RESOURCE_DATA));
        if (resourceList == null) resourceList = new DataSave<Resource>();
        for (int i = 0; i < resourceList.dataList.Count; i++)
        {
            resourceDic.Add((MoneyType)resourceList.dataList[i].ID, resourceList.dataList[i]);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetString(KeySave.RESOURCE_DATA, JsonUtility.ToJson(resourceList));
        Log.Info(JsonUtility.ToJson(resourceList));
    }
}
