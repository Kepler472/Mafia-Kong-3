using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int score;
    public int level;
    public int lives;
    public float[] playerPosition;

    [System.Serializable]
    public struct BarrelData
    {
        public float[] position;
        public float[] velocity;
    }
    public List<BarrelData> barreList = new List<BarrelData>();
    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

public interface ISaveable
{
    void PopulateSaveData(SaveData saveData);
    void LoadFromSaveData(SaveData saveData);
}

public interface ISaveableBarrel
{
    void PopulateSaveData(SaveData.BarrelData saveData);
    void LoadFromSaveData(SaveData.BarrelData saveData);
}