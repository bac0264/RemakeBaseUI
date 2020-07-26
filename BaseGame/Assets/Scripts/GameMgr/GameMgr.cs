using UnityEngine;
using System.Collections;
using UnityEditor.VersionControl;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Ins;
    public Transform uiMain;
    public PopupManager popupManager;
    public UIMainManager uiMainManager;
    public DataPlayer dataPlayer;
    private void Awake()
    {
        if (Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(this);
        }     
        else Destroy(this);
        InitDataPlayer();
    }
    public T InstantiateHelper<T>(T objectType) where T : Object
    {
        return Instantiate(objectType, uiMain);
    }
    public void InitDataPlayer()
    {
        popupManager = new PopupManager();
        dataPlayer = new DataPlayer();
        uiMainManager = new UIMainManager();
    }
    public void ShowHelloWorld()
    {
       // popupManager.ShowPopupWithNoData(PopupType.InitTest);
        uiMainManager.Show(ScreenType.UIDailyReward);
    }

    public void NoAction()
    {
        Debug.Log("No action");
    }
    public void YesAction()
    {
        Debug.Log("Yes action");
    }
}
