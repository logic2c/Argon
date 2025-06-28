using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    public Battle Battle { get; private set; }


    void Start()
    {
        Setup();

    }

    private void Setup()
    {
        BattleData battleData = DataManager.Instance.BattleData;
        PlayerData PCData = DataManager.Instance.PCData;
        if (battleData == null || PCData == null)
        {
            Debug.LogError("Battle data or PC data is not set in DataManager.");
            return;
        }

        Battle = BattleFactory.CreateBasicBattleWithExtraBattlerData(battleData, new List<PlayerData> { PCData }, new List<EnemyData>());
        // x-对战开始-对战中-对战结束-x
        // 先后手, queue.Enqueue();
    }

    void Update()
    {
        
    }
}

public enum CardPosition
{
    Hand,
    Deck,
    Graveyard,
    Field
}
public class  CardPositionBase
{
    public CardPosition type;
}
