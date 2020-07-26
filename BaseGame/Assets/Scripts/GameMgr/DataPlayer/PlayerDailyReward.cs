using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDailyReward
{
    private DataSaveDailyReward dailyRewardData = new DataSaveDailyReward();

    public List<DailyRewardElementData> GetDailyRewardElementData()
    {
        return dailyRewardData.dataList;
    }
    public int GetCurrentDay()
    {
        return dailyRewardData.currentDay;
    }
    public void SetCurrentDay(ref int day)
    {
        if(day < 0 || day >= dailyRewardData.dataList.Count)
        {
            day = 0;
        }
        dailyRewardData.currentDay = day;
    }
    public PlayerDailyReward()
    {
        Load();
    }
    public void Load()
    {
        string stringData = PlayerPrefs.GetString(KeySave.DAILY_REWARD_DATA);
        if (stringData.Equals(""))
        {
            int count = LoadResourceController.GetDailyRewardDataCollection().dataList.Count;
            for (int i = 0; i < count; i++)
            {
                int day = i + 1;
                bool opened = i == 0 ? true : false;
                bool recieved = false;
                DailyRewardElementData data = new DailyRewardElementData(day, opened, recieved);
                dailyRewardData.AddData(data);
            }
            dailyRewardData.currentDay = 0;
            Save();
        }
        else
        {
            dailyRewardData = JsonUtility.FromJson<DataSaveDailyReward>(stringData);
        }
    }
    public void Save() {
        PlayerPrefs.SetString(KeySave.DAILY_REWARD_DATA, JsonUtility.ToJson(dailyRewardData));
    }
}

[System.Serializable]
public class DataSaveDailyReward : DataSave<DailyRewardElementData>
{
    public int currentDay;
}
