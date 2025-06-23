using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConfig
{
    public static float MasterVolume { get; set; } = 1f;
    public static DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Normal;

    // Initialize via static constructor
    static GameConfig()
    {
        // Load settings from PlayerPrefs on first access
        MasterVolume = PlayerPrefs.GetFloat("Volume", 1f);
    }
}

public enum DifficultyLevel { Easy, Normal, Hard }
