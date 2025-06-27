using System.Collections.Generic;

public class Battle
{
    public BattleData Data { get; set; }
    public List<Player> Players { get; private set; }
    public List<Enemy> Enemies { get; private set; }
    public Queue<Turn> Turns { get; private set; }

    public Battle(BattleData data)
    {
        Data = data;
        Players = new List<Player>();
        Enemies = new List<Enemy>();
        Turns = new Queue<Turn>();
    }   
}