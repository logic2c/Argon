using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DeckManager;

public class DeckBuilderUI : MonoBehaviour
{
    [Header("UI ����")]
    public TMP_InputField deckNameInput;
    public Transform cardCollectionContainer;
    public Transform deckListContainer;
    public TextMeshProUGUI deckStatsText;
    public GameObject cardPrefab;
    public GameObject deckCardPrefab;

    [Header("ɸѡѡ��")]
    //public TMP_Dropdown typeDropdown;  
    //public TMP_Dropdown raceDropdown;  
    //public TMP_InputField searchInput;  
    //public Slider costSlider;  
    //public TextMeshProUGUI costRangeText;  

    //private List<CardData> filteredCards = new List<CardData>();  

    private CardDatabase cardDatabase;
    private List<CardData> filteredCards;

    private void Awake()
    {
        // ��ʼ�� cardDatabase �� filteredCards  
        cardDatabase = Resources.Load<CardDatabase>("CardDatabase");
        Debug.Assert(cardDatabase != null, "CardDatabase not found in Resources folder.");
        filteredCards = cardDatabase.allCards;
    }

    private void Start()
    {
        // ��ʼ��UI  
        //InitializeDropdowns();  
        //UpdateCostRangeText();  

        // ��������仯  
        DeckManager.Instance.OnDeckUpdated += UpdateDeckUI;

        // ���س�ʼ����  
        DeckManager.Instance.LoadDeck();

        // ˢ��UI  
        FilterCards();  
        UpdateDeckUI(DeckManager.Instance.GetCurrentDeck());
    }

    //private void InitializeDropdowns()
    //{
    //    // ��ʼ�����������˵�
    //    typeDropdown.ClearOptions();
    //    typeDropdown.options.Add(new TMP_Dropdown.OptionData("��������"));
    //    foreach (CardType type in System.Enum.GetValues(typeof(CardType)))
    //    {
    //        typeDropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
    //    }

    //    // ��ʼ�����������˵�
    //    raceDropdown.ClearOptions();
    //    raceDropdown.options.Add(new TMP_Dropdown.OptionData("��������"));
    //    foreach (CardRace race in System.Enum.GetValues(typeof(CardRace)))
    //    {
    //        raceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
    //    }
    //}

    //private void UpdateCostRangeText()
    //{
    //    costRangeText.text = $"����ֵ: 0 - {costSlider.value}";
    //}

    // ɸѡ���Ʋ�����UI
    public void FilterCards()
    {
        // ��ȡɸѡ����
        //CardType? selectedType = typeDropdown.value > 0 ?
        //    (CardType)(typeDropdown.value - 1) : (CardType?)null;

        //CardRace? selectedRace = raceDropdown.value > 0 ?
        //    (CardRace)(raceDropdown.value - 1) : (CardRace?)null;

        //string searchText = searchInput.text;
        //int maxCost = Mathf.RoundToInt(costSlider.value);

        // ��յ�ǰ����
        foreach (Transform child in cardCollectionContainer)
        {
            Destroy(child.gameObject);
        }

        // ��ȡ����ʾɸѡ��Ŀ���
        //filteredCards = CardDatabase.Instance.FilterCards(
        //    type: selectedType,
        //    race: selectedRace,
        //    searchString: searchText,
        //    maxCost: maxCost
        //);

        // �Ƴ����в������������У�
        var existingLayout = cardCollectionContainer.GetComponent<LayoutGroup>();
        if (existingLayout != null) Destroy(existingLayout);

        // ������񲼾�
        GridLayoutGroup grid = cardCollectionContainer.gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(200, 300);
        grid.spacing = new Vector2(20, 20);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = Mathf.FloorToInt(Screen.width / 220); // ������Ļ��ȼ���ÿ������

        // ������ݳߴ�����
        ContentSizeFitter fitter = cardCollectionContainer.gameObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        foreach (var cardData in filteredCards)
        {
            if (cardData == null) continue;

            Debug.Log($"Adding card: {cardData.cardName} to UI");

            // ʵ����Ԥ����򴴽��¶���
            GameObject cardObject;

            if (cardPrefab != null)
            {
                // ʹ��Ԥ����
                cardObject = Instantiate(cardPrefab, cardCollectionContainer);
            }
            else
            {
                // û��Ԥ����ʱ�Զ������¶���
                cardObject = new GameObject($"Card_{cardData.cardName}");
                cardObject.transform.SetParent(cardCollectionContainer);

                // ��ӱ�Ҫ��RectTransform�������UIԪ�أ�
                var rectTransform = cardObject.AddComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(100, 150); // ����Ĭ�ϳߴ�
            }

            // ��ȡ�����CardUI���
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            if (cardUI == null)
            {
                cardUI = cardObject.AddComponent<CardUI>();
                Debug.Log($"�Զ����CardUI����� {cardObject.name}");
            }

            // ��ʼ������UI
            try
            {
                cardUI.Initialize(cardData, true);

                // �Զ����û���UI��������û�У�
                if (cardUI.GetComponentInChildren<Image>() == null)
                {
                    var bg = new GameObject("Background").AddComponent<Image>();
                    bg.transform.SetParent(cardObject.transform);
                    bg.color = Color.gray; // Ĭ�ϱ���ɫ
                    bg.rectTransform.anchorMin = Vector2.zero;
                    bg.rectTransform.anchorMax = Vector2.one;
                    bg.rectTransform.offsetMin = Vector2.zero;
                    bg.rectTransform.offsetMax = Vector2.zero;
                }
                
            }
            catch (System.Exception e)
            {
                Debug.LogError($"��ʼ������UIʧ��: {e.Message}");
                continue;
            }
        }
    }

    // ���¿���UI
    private void UpdateDeckUI(DeckManager.Deck deck)
    {
        //// ���¿�������
        //deckNameInput.text = deck.deckName;

        //// ��յ�ǰ����
        //foreach (Transform child in deckListContainer)
        //{
        //    Destroy(child.gameObject);
        //}

        //// ���¿����б�
        //foreach (var deckCard in deck.cards)
        //{
        //    var deckCardUI = Instantiate(deckCardPrefab, deckListContainer).GetComponent<DeckCardUI>();
        //    deckCardUI.Initialize(deckCard.cardData, deckCard.count);
        //}

        //// ����ͳ����Ϣ
        //UpdateDeckStats(deck);
    }

    // ���¿���ͳ����Ϣ
    private void UpdateDeckStats(DeckManager.Deck deck)
    {
        int monsterCount = 0;
        int spellCount = 0;
        int trapCount = 0;
        int totalCost = 0;

        foreach (var deckCard in deck.cards)
        {
            switch (deckCard.cardData.cardType)
            {
                case CardType.Monster:
                    monsterCount += deckCard.count;
                    break;
                case CardType.Spell:
                    spellCount += deckCard.count;
                    break;
                case CardType.Trap:
                    trapCount += deckCard.count;
                    break;
            }

            totalCost += deckCard.cardData.cost * deckCard.count;
        }

        float avgCost = deck.TotalCardCount > 0 ?
            (float)totalCost / deck.TotalCardCount : 0;

        deckStatsText.text = $@"��������: {deck.TotalCardCount}/{DeckManager.Instance.maxDeckSize}
���￨: {monsterCount} | ������: {spellCount} | ���忨: {trapCount}
ƽ������ֵ: {avgCost.ToString("F1")}";
    }

    // UI ��ť�¼�
    public void OnDeckNameChanged(string newName)
    {
        DeckManager.Instance.SetDeckName(newName);
    }

    public void OnSaveDeckClicked()
    {
        DeckManager.Instance.SaveDeck();
    }

    public void OnClearDeckClicked()
    {
        DeckManager.Instance.ClearDeck();
    }

    //public void OnCostSliderChanged(float value)
    //{
    //    UpdateCostRangeText();
    //    FilterCards();
    //}

    //public void OnFilterChanged(int _)
    //{
    //    FilterCards();
    //}

    //public void OnSearchValueChanged(string _)
    //{
    //    FilterCards();
    //}
}