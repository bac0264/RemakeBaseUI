using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ResourceType
{
    MONEY = 0,
    CHARACTER = 1,
}

public enum MoneyType
{
    GOLD = 0,
    GEM = 1,
    EXP = 2,
    STAMINA = 3,
}

public enum CHARACTER
{

}
[System.Serializable]
public class Resource
{
    public int ID;
    public long VALUE;
    public int TYPE;
    public Resource(int TYPE, int ID,long value)
    {
        this.VALUE = value;
        this.ID = ID;
        this.TYPE = TYPE;
    }
    public Resource(Dictionary<string, string> data)
    {
        if (data.ContainsKey(CsvKeyConstant.RES_ID))
            int.TryParse(data[CsvKeyConstant.RES_ID], out ID);
        if (data.ContainsKey(CsvKeyConstant.RES_VALUE))
            long.TryParse(data[CsvKeyConstant.RES_VALUE], out VALUE);
        if (data.ContainsKey(CsvKeyConstant.RES_TYPE))
            int.TryParse(data[CsvKeyConstant.RES_TYPE], out TYPE);
    }


    public virtual bool Add(long value) {
        if (value < 0) return false;
        VALUE += value;
        return true; 
    }
    public virtual bool Sub(long value) {
        if (value < 0) return false;
        if (VALUE - value < 0) return false;
        VALUE -= value;
        return false;
    }
    public virtual void Set(long value) {
        if (value < 0) value = 0;
        VALUE = value;
    }
}
[System.Serializable]
public class DataSave<T>
{
    public List<T> dataList;
    public DataSave()
    {
        dataList = new List<T>();
    }

    public void AddData(T t)
    {
        dataList.Add(t);
    }
}