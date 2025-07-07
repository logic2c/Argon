// DeckManager.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    // 当前编辑的卡组
    [SerializeField] private Deck currentDeck;

    // 卡组容量
    public int minDeckSize = 30;
    public int maxDeckSize = 60;

    // 同名卡牌最大数量限制
    public int maxCommonCopies = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 初始化新卡组
            if (currentDeck == null)
            {
                currentDeck = new Deck();
                currentDeck.deckName = "新卡组";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 卡组数据结构
    [System.Serializable]
    public class Deck
    {
        public string deckName;
        public List<DeckCard> cards = new List<DeckCard>();

        public int TotalCardCount
        {
            get
            {
                int count = 0;
                foreach (var card in cards)
                {
                    count += card.count;
                }
                return count;
            }
        }

        // 获取卡组中特定卡牌的数量
        public int GetCardCount(CardData cardData)
        {
            var deckCard = cards.Find(c => c.cardData == cardData);
            return deckCard?.count ?? 0;
        }
    }

    [System.Serializable]
    public class DeckCard
    {
        public CardData cardData;
        public int count;

        public DeckCard(CardData cardData, int count)
        {
            this.cardData = cardData;
            this.count = count;
        }
    }

    // 添加卡牌到当前卡组
    public bool AddCardToDeck(CardData cardData)
    {
        // 检查卡组是否已满
        if (currentDeck.TotalCardCount >= maxDeckSize)
        {
            Debug.LogWarning("卡组已满！");
            return false;
        }

        // 检查同名卡数量限制
        int maxAllowed = 3;
        int currentCount = currentDeck.GetCardCount(cardData);

        if (currentCount >= maxAllowed)
        {
            Debug.LogWarning($"该卡最多只能放入{maxAllowed}张");
            return false;
        }

        // 查找是否已在卡组中
        var existingCard = currentDeck.cards.Find(c => c.cardData == cardData);

        if (existingCard != null)
        {
            existingCard.count++;
        }
        else
        {
            currentDeck.cards.Add(new DeckCard(cardData, 1));
        }

        // 触发卡组更新事件
        OnDeckUpdated?.Invoke(currentDeck);
        return true;
    }

    // 从卡组中移除一张卡
    public bool RemoveCardFromDeck(CardData cardData)
    {
        var existingCard = currentDeck.cards.Find(c => c.cardData == cardData);

        if (existingCard != null)
        {
            existingCard.count--;

            if (existingCard.count <= 0)
            {
                currentDeck.cards.Remove(existingCard);
            }

            OnDeckUpdated?.Invoke(currentDeck);
            return true;
        }

        return false;
    }

    // 设置卡组名称
    public void SetDeckName(string newName)
    {
        currentDeck.deckName = newName;
        OnDeckUpdated?.Invoke(currentDeck);
    }

    // 清空当前卡组
    public void ClearDeck()
    {
        currentDeck.cards.Clear();
        OnDeckUpdated?.Invoke(currentDeck);
    }

    // 卡组更新事件
    public event System.Action<Deck> OnDeckUpdated;

    // 获取当前卡组
    public Deck GetCurrentDeck()
    {
        return currentDeck;
    }

    // 保存卡组到PlayerPrefs
    public void SaveDeck()
    {
        string deckJson = JsonUtility.ToJson(currentDeck);
        PlayerPrefs.SetString("CurrentDeck", deckJson);
        PlayerPrefs.Save();
        Debug.Log("卡组已保存");
    }

    // 从PlayerPrefs加载卡组
    public void LoadDeck()
    {
        if (PlayerPrefs.HasKey("CurrentDeck"))
        {
            string deckJson = PlayerPrefs.GetString("CurrentDeck");
            currentDeck = JsonUtility.FromJson<Deck>(deckJson);
            OnDeckUpdated?.Invoke(currentDeck);
            Debug.Log("卡组已加载");
        }
        else
        {
            Debug.Log("没有找到保存的卡组");
        }
    }

    // 导出卡组为字符串
    public string ExportDeck()
    {
        return JsonUtility.ToJson(currentDeck);
    }

    // 从字符串导入卡组
    public bool ImportDeck(string deckString)
    {
        try
        {
            currentDeck = JsonUtility.FromJson<Deck>(deckString);
            OnDeckUpdated?.Invoke(currentDeck);
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("导入卡组失败: " + e.Message);
            return false;
        }
    }
}