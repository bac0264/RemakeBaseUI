using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DailyRewardDataCollection", menuName = "data/DailyReward/DailyRewardDataCollection)", order = 1)]
public class DailyRewardDataCollection : ScriptableObject
{
    public List<DailyRewardData> dataList = new List<DailyRewardData>();

    public void ParseData(List<Dictionary<string, string>> dataCSV)
    {
        dataList.Clear();
        for(int i = 0; i < dataCSV.Count; i++)
        {
            DailyRewardData data = new DailyRewardData(dataCSV[i]);
            dataList.Add(data);
        }
    }
}

[System.Serializable]
public class DailyRewardData
{
    public Resource resource;

    public DailyRewardData(Dictionary<string, string> data)
    {
        resource = new Resource(data);
    }
}
