using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : PersistentSingleton<DataManager>
{
    public BattleData BattleData { get; set; }
    public PlayerData PCData { get; set; }
    void Start()
    {
        
    }

    void Update()
    {
        
    }


}
