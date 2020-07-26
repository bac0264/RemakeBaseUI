using UnityEngine;
using System.Collections;

public class DataPlayer
{
    public PlayerMoney playerMoney;
    public PlayerDailyReward playerDailyReward;
    public DataPlayer()
    {
        playerMoney = new PlayerMoney();
        playerDailyReward = new PlayerDailyReward();
    }
}
