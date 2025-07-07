using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class CardDataCollector
{
    // �洢���п��Ƶ��ֵ�
    private static Dictionary<string, CardData> _cardDictionary;
    private static List<CardData> _allCards;

    // ���Ʒ��༯��
    private static List<MonsterCardData> _monsterCards;
    private static List<SpellCardData> _spellCards;
    private static List<TrapCardData> _trapCards;

    // ������������
    public static IReadOnlyDictionary<string, CardData> CardDictionary => _cardDictionary;
    public static IReadOnlyList<CardData> AllCards => _allCards;
    public static IReadOnlyList<MonsterCardData> MonsterCards => _monsterCards;
    public static IReadOnlyList<SpellCardData> SpellCards => _spellCards;
    public static IReadOnlyList<TrapCardData> TrapCards => _trapCards;

    // ��ʼ����־
    private static bool _isInitialized = false;

    // ��ʼ������
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

    // ɨ����Ŀ�еĿ���
    private static void ScanForCards()
    {
#if UNITY_EDITOR
        // �����������
        _cardDictionary.Clear();
        _allCards.Clear();
        _monsterCards.Clear();
        _spellCards.Clear();
        _trapCards.Clear();

        // ��ȡ����CardData�ʲ�
        string[] guids = AssetDatabase.FindAssets("t:CardData");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            CardData card = AssetDatabase.LoadAssetAtPath<CardData>(path);

            if (card != null)
            {
                // ��ӵ����б�
                _allCards.Add(card);

                // ��ӵ��ֵ�
                if (!_cardDictionary.ContainsKey(card.cardName))
                {
                    _cardDictionary.Add(card.cardName, card);
                }
                else
                {
                    Debug.LogWarning($"�ظ��Ŀ�������: {card.cardName} at {path}");
                }

                // ����洢
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

        Debug.Log($"����ɨ�����. �ܼ�: {_allCards.Count}�� " +
                 $"(����: {_monsterCards.Count}, ����: {_spellCards.Count}, ����: {_trapCards.Count})");
#endif
    }

    // �����ƻ�ȡ����
    public static CardData GetCardByName(string cardName)
    {
        if (!_isInitialized) Initialize();

        if (_cardDictionary.TryGetValue(cardName, out CardData card))
        {
            return card;
        }

        Debug.LogWarning($"�Ҳ�������: {cardName}");
        return null;
    }

    // ������ɸѡ����
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

    // ������ɸѡ����
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

    // �༭���˵���
#if UNITY_EDITOR
    [MenuItem("Card Game/Refresh Card Database")]
    private static void RefreshCardDatabase()
    {
        _isInitialized = false;
        Initialize();
        Debug.Log("�������ݿ���ˢ��");
    }
#endif
}