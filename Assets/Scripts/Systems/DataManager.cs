using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : PersistentSingleton<DataManager>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public BattleData BattleData { get; private set; }
    public PlayerData PCData { get; private set; }

}
