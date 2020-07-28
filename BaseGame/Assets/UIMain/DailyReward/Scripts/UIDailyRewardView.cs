using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDailyRewardView : BaseUIMain
{
    [SerializeField] private UIDailyRewardElement prefab = null;
    [SerializeField] private List<UIDailyRewardElement> dailyRewardElements = new List<UIDailyRewardElement>();
    [SerializeField] private DailyRewardDataCollection dailyReward = null;
    [SerializeField] private PlayerDailyReward dataPlayer = null;
    [SerializeField] private PlayerMoney dataMoney = null;
    [SerializeField] private Transform elementParents;
    [SerializeField] private Transform timeViewParents;
    [SerializeField] private TimeView timeView = null;
    [SerializeField] private Button claimBtn = null;
    private int currentDay = -1;
    private List<DailyRewardElementData> dataCondition = null;
    private void Awake()
    {
        InitButton();
        InitData();
        InitElements();
    }

    public void InitButton()
    {
        claimBtn.onClick.AddListener(OnClickClaim);
    }
    public void InitData()
    {
        dailyReward = LoadResourceController.GetDailyRewardDataCollection();
        dataPlayer = GameMgr.Ins.dataPlayer.playerDailyReward;
        dataMoney = GameMgr.Ins.dataPlayer.playerMoney;
        currentDay = dataPlayer.GetCurrentDay();
        dataCondition = dataPlayer.GetDailyRewardElementData();
        timeView = Instantiate(timeView, timeViewParents);
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

    private void SetPositionTimeView(Transform parent)
    {
        timeView.transform.position = parent.position;
        timeView.transform.SetParent(parent);
        timeView.transform.SetAsLastSibling();
    }

    IEnumerator SetTimeView()
    {
        yield return new WaitForEndOfFrame();
        SetPositionTimeView(dailyRewardElements[currentDay].transform);
        timeView.StartTime(OnFinishDay, true);
    }

    public void OnFinishDay()
    {
        dataCondition[currentDay].opened = true;
        dailyRewardElements[currentDay].SetElementState(dataCondition[currentDay]);
        currentDay++;
        dataPlayer.SetCurrentDay(ref currentDay);
        SetPositionTimeView(dailyRewardElements[currentDay].transform);
        dataPlayer.Save();
        if (currentDay == dataCondition.Count - 1)
        {

        }
        else
        {
            timeView.StartTime(OnFinishDay, true);
        }
    }

    public void OnClickClaim()
    {
        List<Resource> resourceClaims = new List<Resource>();
        bool isEmpty = true;
        for (int i = 0; i < dataCondition.Count && i < dailyRewardElements.Count; i++)
        {
            if (dataCondition[i].opened && !dataCondition[i].recieved)
            {
                resourceClaims.Add(dailyRewardElements[i].Resource);
                dataCondition[i].recieved = true;
                dailyRewardElements[i].SetElementState(dataCondition[i]);
                isEmpty = false;
            }
            else if (!dataCondition[i].opened)
            {
                break;
            }
        }
        if (isEmpty) return;
        // if daily complete, it will reset
        if (dataPlayer.Complete())
        {
            // SetElementState for all element
            for (int i = 0; i < dataCondition.Count && i < dailyRewardElements.Count; i++)
            {
                dailyRewardElements[i].SetElementState(dataCondition[i]);
            }
        }
        dataMoney.AddManyMoney(resourceClaims);
        dataPlayer.Save();
    }

}
