using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerDailyReward
{
    private DataSaveDailyReward dailyRewardData = new DataSaveDailyReward();

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
            if (TimeManager.Ins != null) TimeManager.Ins.SaveTime();
            Save();
        }
        else
        {
            dailyRewardData = JsonUtility.FromJson<DataSaveDailyReward>(stringData);
            if (TimeManager.Ins != null) TimeManager.Ins.UpdateCurrentTime(CheckTime);
        }
    }

    private void CheckTime(bool canCheck, long currentTime)
    {
        if (canCheck)
        {
            if (long.TryParse(PlayerPrefs.GetString(KeySave.TIME_QUIT_GAME), out long lastTime))
            {
                bool check = TimeHelper.IsNextDay(lastTime, currentTime);
                if(check)
                {
                    if(dailyRewardData.currentDay < dailyRewardData.dataList.Count)
                    dailyRewardData.currentDay++;
                    else Log.Info(LogicCode.FULL_DAY);
                }
                else
                {
                    Log.Info(LogicCode.FULL_DAY);
                }
            }
            else
            {
                // Show Popup fail
                Log.Info(LogicCode.LAST_TIME_ONLINE_INVALID);
            }
        }
        else
        {
            Log.Info(LogicCode.CAN_NOT_GET_TIME);
        }
    }
    public void Save()
    {
        PlayerPrefs.SetString(KeySave.DAILY_REWARD_DATA, JsonUtility.ToJson(dailyRewardData));
    }


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

    public bool Complete()
    {
        int count = dailyRewardData.dataList.Count;
        if (count > 0)
        {
            if (dailyRewardData.dataList[count - 1].recieved)
            {
                Reset();
                return true;
            }
        }
        return false;
    }
    private void Reset()
    {
        for(int i = 0; i < dailyRewardData.dataList.Count; i++)
        {
            bool opened = i == 0 ? true : false;
            bool recieved = false;
            dailyRewardData.dataList[i].opened = opened;
            dailyRewardData.dataList[i].recieved = recieved;
        }
    }
}

[System.Serializable]
public class DataSaveDailyReward : DataSave<DailyRewardElementData>
{
    public int currentDay;
}
