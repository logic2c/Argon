using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeckCardUI : MonoBehaviour
{
    [Header("UI 元素")]
    public Image cardImage;
    public TextMeshProUGUI cardNameText;
    public TextMeshProUGUI countText;
    public Button increaseButton;
    public Button decreaseButton;

    private CardData cardData;

    public void Initialize(CardData data, int count)
    {
        cardData = data;

        // 更新UI
        cardImage.sprite = data.icon;
        cardNameText.text = data.cardName;
        countText.text = count.ToString();

        // 设置按钮事件
        increaseButton.onClick.AddListener(IncreaseCount);
        decreaseButton.onClick.AddListener(DecreaseCount);
    }

    private void IncreaseCount()
    {
        DeckManager.Instance.AddCardToDeck(cardData);
    }

    private void DecreaseCount()
    {
        DeckManager.Instance.RemoveCardFromDeck(cardData);
    }
}