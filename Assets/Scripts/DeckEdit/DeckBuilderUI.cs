using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static DeckManager;

public class DeckBuilderUI : MonoBehaviour
{
    [Header("UI 引用")]
    public TMP_InputField deckNameInput;
    public Transform cardCollectionContainer;
    public Transform deckListContainer;
    public TextMeshProUGUI deckStatsText;
    public GameObject cardPrefab;
    public GameObject deckCardPrefab;

    [Header("筛选选项")]
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
        // 初始化 cardDatabase 和 filteredCards  
        cardDatabase = Resources.Load<CardDatabase>("CardDatabase");
        Debug.Assert(cardDatabase != null, "CardDatabase not found in Resources folder.");
        filteredCards = cardDatabase.allCards;
    }

    private void Start()
    {
        // 初始化UI  
        //InitializeDropdowns();  
        //UpdateCostRangeText();  

        // 监听卡组变化  
        DeckManager.Instance.OnDeckUpdated += UpdateDeckUI;

        // 加载初始卡组  
        DeckManager.Instance.LoadDeck();

        // 刷新UI  
        FilterCards();  
        UpdateDeckUI(DeckManager.Instance.GetCurrentDeck());
    }

    //private void InitializeDropdowns()
    //{
    //    // 初始化类型下拉菜单
    //    typeDropdown.ClearOptions();
    //    typeDropdown.options.Add(new TMP_Dropdown.OptionData("所有类型"));
    //    foreach (CardType type in System.Enum.GetValues(typeof(CardType)))
    //    {
    //        typeDropdown.options.Add(new TMP_Dropdown.OptionData(type.ToString()));
    //    }

    //    // 初始化种族下拉菜单
    //    raceDropdown.ClearOptions();
    //    raceDropdown.options.Add(new TMP_Dropdown.OptionData("所有种族"));
    //    foreach (CardRace race in System.Enum.GetValues(typeof(CardRace)))
    //    {
    //        raceDropdown.options.Add(new TMP_Dropdown.OptionData(race.ToString()));
    //    }
    //}

    //private void UpdateCostRangeText()
    //{
    //    costRangeText.text = $"法力值: 0 - {costSlider.value}";
    //}

    // 筛选卡牌并更新UI
    public void FilterCards()
    {
        // 获取筛选条件
        //CardType? selectedType = typeDropdown.value > 0 ?
        //    (CardType)(typeDropdown.value - 1) : (CardType?)null;

        //CardRace? selectedRace = raceDropdown.value > 0 ?
        //    (CardRace)(raceDropdown.value - 1) : (CardRace?)null;

        //string searchText = searchInput.text;
        //int maxCost = Mathf.RoundToInt(costSlider.value);

        // 清空当前容器
        foreach (Transform child in cardCollectionContainer)
        {
            Destroy(child.gameObject);
        }

        // 获取并显示筛选后的卡牌
        //filteredCards = CardDatabase.Instance.FilterCards(
        //    type: selectedType,
        //    race: selectedRace,
        //    searchString: searchText,
        //    maxCost: maxCost
        //);

        // 移除现有布局组件（如果有）
        var existingLayout = cardCollectionContainer.GetComponent<LayoutGroup>();
        if (existingLayout != null) Destroy(existingLayout);

        // 添加网格布局
        GridLayoutGroup grid = cardCollectionContainer.gameObject.AddComponent<GridLayoutGroup>();
        grid.cellSize = new Vector2(200, 300);
        grid.spacing = new Vector2(20, 20);
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = Mathf.FloorToInt(Screen.width / 220); // 根据屏幕宽度计算每行列数

        // 添加内容尺寸适配
        ContentSizeFitter fitter = cardCollectionContainer.gameObject.AddComponent<ContentSizeFitter>();
        fitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        fitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        foreach (var cardData in filteredCards)
        {
            if (cardData == null) continue;

            Debug.Log($"Adding card: {cardData.cardName} to UI");

            // 实例化预制体或创建新对象
            GameObject cardObject;

            if (cardPrefab != null)
            {
                // 使用预制体
                cardObject = Instantiate(cardPrefab, cardCollectionContainer);
            }
            else
            {
                // 没有预制体时自动创建新对象
                cardObject = new GameObject($"Card_{cardData.cardName}");
                cardObject.transform.SetParent(cardCollectionContainer);

                // 添加必要的RectTransform（如果是UI元素）
                var rectTransform = cardObject.AddComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(100, 150); // 设置默认尺寸
            }

            // 获取或添加CardUI组件
            CardUI cardUI = cardObject.GetComponent<CardUI>();
            if (cardUI == null)
            {
                cardUI = cardObject.AddComponent<CardUI>();
                Debug.Log($"自动添加CardUI组件到 {cardObject.name}");
            }

            // 初始化卡牌UI
            try
            {
                cardUI.Initialize(cardData, true);

                // 自动设置基本UI组件（如果没有）
                if (cardUI.GetComponentInChildren<Image>() == null)
                {
                    var bg = new GameObject("Background").AddComponent<Image>();
                    bg.transform.SetParent(cardObject.transform);
                    bg.color = Color.gray; // 默认背景色
                    bg.rectTransform.anchorMin = Vector2.zero;
                    bg.rectTransform.anchorMax = Vector2.one;
                    bg.rectTransform.offsetMin = Vector2.zero;
                    bg.rectTransform.offsetMax = Vector2.zero;
                }
                
            }
            catch (System.Exception e)
            {
                Debug.LogError($"初始化卡牌UI失败: {e.Message}");
                continue;
            }
        }
    }

    // 更新卡组UI
    private void UpdateDeckUI(DeckManager.Deck deck)
    {
        //// 更新卡组名称
        //deckNameInput.text = deck.deckName;

        //// 清空当前容器
        //foreach (Transform child in deckListContainer)
        //{
        //    Destroy(child.gameObject);
        //}

        //// 更新卡组列表
        //foreach (var deckCard in deck.cards)
        //{
        //    var deckCardUI = Instantiate(deckCardPrefab, deckListContainer).GetComponent<DeckCardUI>();
        //    deckCardUI.Initialize(deckCard.cardData, deckCard.count);
        //}

        //// 更新统计信息
        //UpdateDeckStats(deck);
    }

    // 更新卡组统计信息
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

        deckStatsText.text = $@"卡牌总数: {deck.TotalCardCount}/{DeckManager.Instance.maxDeckSize}
生物卡: {monsterCount} | 法术卡: {spellCount} | 陷阱卡: {trapCount}
平均法力值: {avgCost.ToString("F1")}";
    }

    // UI 按钮事件
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