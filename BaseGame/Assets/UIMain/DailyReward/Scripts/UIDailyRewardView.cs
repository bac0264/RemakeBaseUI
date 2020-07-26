using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDailyRewardView : BaseUIMain
{
    [SerializeField] private UIDailyRewardElement prefab = null;
    [SerializeField] private List<UIDailyRewardElement> dailyRewardElements = new List<UIDailyRewardElement>();
    [SerializeField] private DailyRewardDataCollection dailyReward = null;
    [SerializeField] private PlayerDailyReward dataPlayer = null;
    [SerializeField] private Transform elementParents;
    [SerializeField] private Transform timeViewParents;
    [SerializeField] private TimeView timeView = null;
    private int currentDay = -1;
    private List<DailyRewardElementData> dataCondition = null;
    private void Awake()
    {
        dailyReward = LoadResourceController.GetDailyRewardDataCollection();
        dataPlayer = GameMgr.Ins.dataPlayer.playerDailyReward;
        currentDay = dataPlayer.GetCurrentDay();
        dataCondition = dataPlayer.GetDailyRewardElementData();
        timeView = Instantiate(timeView, timeViewParents);
        InitElements();
    }

    public void InitElements()
    {
        int i = 0;
        for (; i < dailyReward.dataList.Count && i < dailyRewardElements.Count; i++)
        {
            dailyRewardElements[i].gameObject.SetActive(true);
            dailyRewardElements[i].Init(dailyReward.dataList[i].resource, dataCondition[i]);
        }
        for (; i < dailyReward.dataList.Count; i++)
        {
            UIDailyRewardElement element = Instantiate(prefab, elementParents);
            element.Init(dailyReward.dataList[i].resource, dataCondition[i]);
            dailyRewardElements.Add(element);
        }
        for (; i < dailyRewardElements.Count; i++)
        {
            dailyRewardElements[i].gameObject.SetActive(false);
        }
        StartCoroutine(SetTimeView());
    }
    IEnumerator SetTimeView()
    {
        yield return new WaitForEndOfFrame();
        timeView.transform.position = dailyRewardElements[currentDay].transform.position;
        timeView.StartTime(OnFinishDay);
    }
    public void OnFinishDay()
    {
        dataCondition[currentDay].opened = true;
        dailyRewardElements[currentDay].Refresh(dataCondition[currentDay]);
        currentDay++;
        dataPlayer.SetCurrentDay(ref currentDay);
        timeView.transform.position = dailyRewardElements[currentDay].transform.position;
        dataPlayer.Save();
        if (currentDay == dataCondition.Count - 1)
        {

        }
        else
        {
            timeView.StartTime(OnFinishDay);
        }
    }
    public void OnClickReward()
    {

    }

}
