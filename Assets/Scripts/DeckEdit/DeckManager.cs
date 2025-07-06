// DeckManager.cs
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DeckManager : MonoBehaviour
{
    public static DeckManager Instance { get; private set; }

    // ��ǰ�༭�Ŀ���
    [SerializeField] private Deck currentDeck;

    // ��������
    public int minDeckSize = 30;
    public int maxDeckSize = 60;

    // ͬ�����������������
    public int maxCommonCopies = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ��ʼ���¿���
            if (currentDeck == null)
            {
                currentDeck = new Deck();
                currentDeck.deckName = "�¿���";
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // �������ݽṹ
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

        // ��ȡ�������ض����Ƶ�����
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

    // ��ӿ��Ƶ���ǰ����
    public bool AddCardToDeck(CardData cardData)
    {
        // ��鿨���Ƿ�����
        if (currentDeck.TotalCardCount >= maxDeckSize)
        {
            Debug.LogWarning("����������");
            return false;
        }

        // ���ͬ������������
        int maxAllowed = 3;
        int currentCount = currentDeck.GetCardCount(cardData);

        if (currentCount >= maxAllowed)
        {
            Debug.LogWarning($"�ÿ����ֻ�ܷ���{maxAllowed}��");
            return false;
        }

        // �����Ƿ����ڿ�����
        var existingCard = currentDeck.cards.Find(c => c.cardData == cardData);

        if (existingCard != null)
        {
            existingCard.count++;
        }
        else
        {
            currentDeck.cards.Add(new DeckCard(cardData, 1));
        }

        // ������������¼�
        OnDeckUpdated?.Invoke(currentDeck);
        return true;
    }

    // �ӿ������Ƴ�һ�ſ�
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

    // ���ÿ�������
    public void SetDeckName(string newName)
    {
        currentDeck.deckName = newName;
        OnDeckUpdated?.Invoke(currentDeck);
    }

    // ��յ�ǰ����
    public void ClearDeck()
    {
        currentDeck.cards.Clear();
        OnDeckUpdated?.Invoke(currentDeck);
    }

    // ��������¼�
    public event System.Action<Deck> OnDeckUpdated;

    // ��ȡ��ǰ����
    public Deck GetCurrentDeck()
    {
        return currentDeck;
    }

    // ���濨�鵽PlayerPrefs
    public void SaveDeck()
    {
        string deckJson = JsonUtility.ToJson(currentDeck);
        PlayerPrefs.SetString("CurrentDeck", deckJson);
        PlayerPrefs.Save();
        Debug.Log("�����ѱ���");
    }

    // ��PlayerPrefs���ؿ���
    public void LoadDeck()
    {
        if (PlayerPrefs.HasKey("CurrentDeck"))
        {
            string deckJson = PlayerPrefs.GetString("CurrentDeck");
            currentDeck = JsonUtility.FromJson<Deck>(deckJson);
            OnDeckUpdated?.Invoke(currentDeck);
            Debug.Log("�����Ѽ���");
        }
        else
        {
            Debug.Log("û���ҵ�����Ŀ���");
        }
    }

    // ��������Ϊ�ַ���
    public string ExportDeck()
    {
        return JsonUtility.ToJson(currentDeck);
    }

    // ���ַ������뿨��
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
            Debug.LogError("���뿨��ʧ��: " + e.Message);
            return false;
        }
    }
}