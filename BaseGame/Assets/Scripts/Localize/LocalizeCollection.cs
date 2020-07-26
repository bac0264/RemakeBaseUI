using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

public enum LanguageType
{
    English = 0,
    Vietnamese = 1,
}
[CreateAssetMenu (fileName = "LocalizeCollection", menuName = "data/Localize/LocalizeCollection", order = 1)]
[System.Serializable]
public class LocalizeCollection : ScriptableObject
{
    public DictionaryOfStringAndString currentLanguageStrings = new DictionaryOfStringAndString();
    public void ParseData(List<Dictionary<string, string>> dataCSV)
    {
        currentLanguageStrings.Clear();
        if (currentLanguageStrings == null)
            currentLanguageStrings = new DictionaryOfStringAndString();
        for (int i = 0; i < dataCSV.Count; i++)
        {
            currentLanguageStrings.Add(dataCSV[i][CsvKeyConstant.LOCALIZE_KEY], dataCSV[i][CsvKeyConstant.LOCALIIZE_VALUE]);
        }
        Debug.Log(GetData()["day"]);
    }
    public LanguageType language;

    public Dictionary<string, string> GetData()
    {
        return currentLanguageStrings;
    }
}

[System.Serializable]
public class DictionaryOfStringAndString : SerializableDictionary<string, string>
{
}
[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    public List<TKey> keys = new List<TKey>();

    public List<TValue> values = new List<TValue>();

    // save the dictionary to lists
    public void OnBeforeSerialize()
    {
        keys.Clear();
        values.Clear();
        foreach (KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    // load dictionary from lists
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
            throw new System.Exception(string.Format(
                "there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

        for (int i = 0; i < keys.Count; i++)
            this.Add(keys[i], values[i]);
    }


    public override string ToString()
    {
        var result = "";
        for (int i = 0; i < keys.Count; i++)
        {
            result += string.Format("{0}:{1}", keys[i], values[i]);
            if (i < keys.Count - 1)
            {
                result += "&";
            }
        }

        if (result == "")
        {
            result = "Empty!";
        }

        return result;
    }
}