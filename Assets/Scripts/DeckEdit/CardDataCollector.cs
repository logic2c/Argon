using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class CardDataCollector
{
    // 存储所有卡牌的字典
    private static Dictionary<string, CardData> _cardDictionary;
    private static List<CardData> _allCards;

    // 卡牌分类集合
    private static List<MonsterCardData> _monsterCards;
    private static List<SpellCardData> _spellCards;
    private static List<TrapCardData> _trapCards;

    // 公共访问属性
    public static IReadOnlyDictionary<string, CardData> CardDictionary => _cardDictionary;
    public static IReadOnlyList<CardData> AllCards => _allCards;
    public static IReadOnlyList<MonsterCardData> MonsterCards => _monsterCards;
    public static IReadOnlyList<SpellCardData> SpellCards => _spellCards;
    public static IReadOnlyList<TrapCardData> TrapCards => _trapCards;

    // 初始化标志
    private static bool _isInitialized = false;

    // 初始化方法
    public static void Initialize()
    {
        if (_isInitialized) return;

        _cardDictionary = new Dictionary<string, CardData>();
        _allCards = new List<CardData>();
        _monsterCards = new List<MonsterCardData>();
        _spellCards = new List<SpellCardData>();
        _trapCards = new List<TrapCardData>();

        ScanForCards();
        _isInitialized = true;
    }

    // 扫描项目中的卡牌
    private static void ScanForCards()
    {
#if UNITY_EDITOR
        // 清空现有数据
        _cardDictionary.Clear();
        _allCards.Clear();
        _monsterCards.Clear();
        _spellCards.Clear();
        _trapCards.Clear();

        // 获取所有CardData资产
        string[] guids = AssetDatabase.FindAssets("t:CardData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CardData card = AssetDatabase.LoadAssetAtPath<CardData>(path);

            if (card != null)
            {
                // 添加到总列表
                _allCards.Add(card);

                // 添加到字典
                if (!_cardDictionary.ContainsKey(card.cardName))
                {
                    _cardDictionary.Add(card.cardName, card);
                }
                else
                {
                    Debug.LogWarning($"重复的卡牌名称: {card.cardName} at {path}");
                }

                // 分类存储
                switch (card)
                {
                    case MonsterCardData monsterCard:
                        _monsterCards.Add(monsterCard);
                        break;
                    case SpellCardData spellCard:
                        _spellCards.Add(spellCard);
                        break;
                    case TrapCardData trapCard:
                        _trapCards.Add(trapCard);
                        break;
                }
            }
        }

        Debug.Log($"卡牌扫描完成. 总计: {_allCards.Count}张 " +
                 $"(怪物: {_monsterCards.Count}, 法术: {_spellCards.Count}, 陷阱: {_trapCards.Count})");
#endif
    }

    // 按名称获取卡牌
    public static CardData GetCardByName(string cardName)
    {
        if (!_isInitialized) Initialize();

        if (_cardDictionary.TryGetValue(cardName, out CardData card))
        {
            return card;
        }

        Debug.LogWarning($"找不到卡牌: {cardName}");
        return null;
    }

    // 按类型筛选卡牌
    public static List<CardData> GetCardsByType(CardType type)
    {
        if (!_isInitialized) Initialize();

        return type switch
        {
            CardType.Monster => new List<CardData>(_monsterCards),
            CardType.Spell => new List<CardData>(_spellCards),
            CardType.Trap => new List<CardData>(_trapCards),
            _ => new List<CardData>(_allCards)
        };
    }

    // 按种族筛选卡牌
    public static List<CardData> GetCardsByRace(CardRace race)
    {
        if (!_isInitialized) Initialize();

        List<CardData> result = new List<CardData>();
        foreach (var card in _allCards)
        {
            if (card.cardRace == race)
            {
                result.Add(card);
            }
        }
        return result;
    }

    // 编辑器菜单项
#if UNITY_EDITOR
    [MenuItem("Card Game/Refresh Card Database")]
    private static void RefreshCardDatabase()
    {
        _isInitialized = false;
        Initialize();
        Debug.Log("卡牌数据库已刷新");
    }
#endif
}