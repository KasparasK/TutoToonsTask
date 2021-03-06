﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDataHolder : MonoBehaviour
{
    private LevelsRoot levelsRoot;
    public LevelsRoot LevelsRoot
    {
        get
        {
            if (levelsRoot == null)
                LoadFromFile();

            return levelsRoot;
        }
    }

    public TextAsset json;

    public int activeLevelID;

    public List<Vector2> GetActiveLevel()
    {
        return ConvertFromRawData(LevelsRoot.levels[activeLevelID]);
    }

    List<Vector2> ConvertFromRawData(Level level)
    {
        List<Vector2> positions= new List<Vector2>();

        if (level.level_data.Count < 4)
        {
            Debug.LogError("Not enough points to form a level");
            return null;
        }

        if (level.level_data.Count % 2 != 0)
        {
            Debug.LogError("Coordinates count not even");
            return null;

        }

        for (int i = 0; i < level.level_data.Count; i+=2)
        {
            positions.Add(new Vector2(level.level_data[i]/10f, -level.level_data[i + 1]/10f));
        }

        return positions;
    }

    void LoadFromFile()
    {
        try
        {
            levelsRoot = JsonUtility.FromJson<LevelsRoot>(json.text);

        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}
[Serializable]
public class Level
{
    [SerializeField]
    public List<int> level_data;
  
}
[Serializable]
public class LevelsRoot
{
    [SerializeField]
    public List<Level> levels;
    
}

