using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDataHelper : MonoBehaviour
{
    public void LoadData()
    {
        LoadDailyReward();
        LoadLocalize();
    }

    public void LoadDailyReward()
    {
        DailyRewardDataCollection rewardData = LoadResourceController.GetDailyRewardDataCollection();
        var _data = CSVReader.Read(LoadResourceController.LoadFromResource<TextAsset>(CsvPath.CSV_PATH, CsvPath.FN_DAILY_REWARD));
        rewardData.ParseData(_data);
    }

    public void LoadLocalize()
    {
        LocalizeCollection rewardData = LoadResourceController.GetLocalizeCollection();
        var _data = CSVReader.Read(LoadResourceController.LoadFromResource<TextAsset>(CsvPath.CSV_PATH, CsvPath.FN_LOCALIZE));
        rewardData.ParseData(_data);
    }
}